# SharpSDL3 Security Analysis

## Threat Model

**Scenario**: SharpSDL3 is used as a rendering/UI layer in a security monitoring
dashboard. An attacker targets the binding library to compromise the dashboard
host, exfiltrate data, deny service, or pivot into the monitored environment.

**Attack surface**: The boundary between managed C# code and native SDL3 is the
primary attack surface. Every `nint` pointer, every `Marshal` call, every
delegate passed to native code, and the native library itself are potential
vectors.

---

## Attack 1: Native Library Hijacking (DLL Planting)

### The Attack

The library resolves SDL3 by unqualified name:

```csharp
// Sdl.cs:13
internal const string NativeLibName = "SDL3";
```

An attacker who can write to the application directory, a directory on `PATH`,
or the working directory plants a malicious `SDL3.dll` / `libSDL3.so` /
`libSDL3.dylib`. The .NET runtime loads it instead of the legitimate library.
The attacker's code runs with full process privileges.

**Impact**: Remote code execution. Full compromise of the dashboard host.

### Fix (Code)

Register a custom `NativeLibrary` resolver that pins the load path and
optionally validates a hash or signature:

```csharp
// In library initialization
NativeLibrary.SetDllImportResolver(typeof(Sdl).Assembly, (name, assembly, path) =>
{
    if (name == NativeLibName)
    {
        string expected = Path.Combine(AppContext.BaseDirectory, "lib", name);
        return NativeLibrary.Load(expected);
    }
    return IntPtr.Zero;
});
```

### Fix (Policy)

- Deploy the application with SDL3 in a directory writable only by
  administrators.
- On Windows, set `CWD` to a safe location and use
  `SetDefaultDllDirectories(LOAD_LIBRARY_SEARCH_SYSTEM32)`.
- Sign the native binary and validate the signature at startup.
- In containerized deployments, mount the library read-only.

---

## Attack 2: Memory Leak Denial of Service

### The Attack

`Render.cs:54-62` allocates unmanaged memory and never frees it:

```csharp
Event* eventPtr = (Event*)Marshal.AllocHGlobal(Marshal.SizeOf<Event>());
bool result = SDL_ConvertEventToRenderCoordinates(renderer, ref eventPtr);
@event = *eventPtr;
return result;
// Missing: Marshal.FreeHGlobal((nint)eventPtr);
```

Every call to `ConvertEventToRenderCoordinates` leaks 128+ bytes. In a
dashboard that processes mouse/touch events continuously, an attacker who can
generate input events (e.g., automated mouse movement via a compromised input
device or remote desktop session) exhausts memory over hours, crashing the
dashboard.

A similar issue exists in `JoySticks.cs:999-1005` where `AllocHGlobal` memory
is freed with `SDL_free()` instead of `Marshal.FreeHGlobal` — undefined
behavior that may silently leak or corrupt the heap.

**Impact**: Denial of service. Dashboard goes down, security events stop being
monitored.

### Fix (Code)

```csharp
public static bool ConvertEventToRenderCoordinates(nint renderer, ref Event @event) {
    if (renderer == nint.Zero)
        throw new SdlException("Renderer is null");

    Event* eventPtr = (Event*)Marshal.AllocHGlobal(Marshal.SizeOf<Event>());
    try {
        bool result = SDL_ConvertEventToRenderCoordinates(renderer, ref eventPtr);
        @event = *eventPtr;
        return result;
    } finally {
        Marshal.FreeHGlobal((nint)eventPtr);
    }
}
```

### Fix (Policy)

- Monitor process memory in production. Alert if the dashboard process grows
  beyond a threshold.
- Run the dashboard in a container with memory limits (`--memory` in Docker) so
  a leak triggers a restart rather than taking down the host.

---

## Attack 3: Callback Delegate Collection (Use-After-Free)

### The Attack

Delegates passed to native SDL3 functions are not pinned with `GCHandle`:

```csharp
// Events.cs:31
SdlBool result = SDL_AddEventWatch(filter, userdata);
// 'filter' delegate is now held only by native code — GC can collect it
```

This pattern appears in 9+ locations: `AddEventWatch`, `SetEventFilter`,
`SetLogOutputFunction`, `SetAudioPostmixCallback`, `SetAudioStreamGetCallback`,
`SetAudioStreamPutCallback`, file dialogs, directory enumeration, and hint
callbacks.

An attacker who can influence GC pressure (e.g., by triggering allocations
through the dashboard's API or WebSocket interface) causes the GC to collect a
delegate that native SDL3 still references. The next time SDL3 invokes the
callback, it jumps to freed memory — a classic use-after-free that can be
exploited for code execution.

**Impact**: Potential remote code execution via controlled heap spray +
use-after-free.

### Fix (Code)

Keep a static reference to every delegate passed to native code:

```csharp
private static readonly List<Delegate> _pinnedCallbacks = new();

public static bool AddEventWatch(SdlEventFilter filter, nint userdata) {
    if (filter == null)
        throw new ArgumentNullException(nameof(filter));
    _pinnedCallbacks.Add(filter); // prevent GC collection
    return SDL_AddEventWatch(filter, userdata);
}
```

For callbacks that are removed (e.g., `RemoveEventWatch`), remove from the list
at that point.

### Fix (Policy)

- This is purely a code fix. No policy mitigation exists for use-after-free.

---

## Attack 4: Buffer Overread via Trusted Native Counts

### The Attack

The library trusts `count` values returned by native SDL3 without bounds
checking:

```csharp
// Audio.cs:346-361
nint result = SDL_GetAudioPlaybackDevices(out int count);
int[] playbackDevicesI = new int[count];
Marshal.Copy(result, playbackDevicesI, 0, count);
```

This pattern appears in `GetAudioPlaybackDevices`, `GetAudioRecordingDevices`,
`GetAudioDeviceChannelMap`, `GetAudioStreamInputChannelMap`,
`GetAudioStreamOutputChannelMap`, `GetCameras`, `GetCameraSupportedFormats`,
`GetJoysticks`, `GetTouchDevices`, `GetTouchFingers`, and TTF functions.

If a compromised or attacker-controlled SDL3 library (see Attack 1) returns an
inflated `count`, `Marshal.Copy` or the read loop reads beyond the allocated
buffer into adjacent memory. This can leak sensitive data (cryptographic keys,
session tokens) from the dashboard process.

Additionally, `JoySticks.cs:591` uses `ReadInt32` to read pointer-sized values,
which is incorrect on 64-bit platforms — reading only 4 of 8 bytes and
misaligning subsequent reads.

**Impact**: Information disclosure. Potential crash (DoS).

### Fix (Code)

Add a reasonable upper bound before trusting native counts:

```csharp
const int MaxReasonableDeviceCount = 1024;

nint result = SDL_GetAudioPlaybackDevices(out int count);
if (count < 0 || count > MaxReasonableDeviceCount) {
    LogError(LogCategory.Error, $"Suspicious device count: {count}");
    return [];
}
```

Fix the 64-bit bug in `JoySticks.cs:591`:
```csharp
// Wrong: nint joystick = Marshal.ReadInt32(joystickArrayPtr, i * nint.Size);
// Right:
nint joystick = Marshal.ReadIntPtr(joystickArrayPtr, i * nint.Size);
```

### Fix (Policy)

- If using a third-party-compiled SDL3, verify its provenance and checksum.
- In high-security environments, compile SDL3 from source with hardened flags
  (`-fstack-protector-strong`, ASLR, CFI).

---

## Attack 5: GC Relocation of Pinned Structs (Mixer)

### The Attack

The Mixer module takes the address of managed structs and passes them to native
code:

```csharp
// Mixer.cs:569
Unsafe.AsPointer(ref chunk)  // takes address, passes to native
```

If the GC relocates the struct while native code is using the pointer, SDL3
reads/writes stale memory. An attacker who can trigger GC pressure during audio
playback (e.g., flooding the dashboard with events) can cause use-after-move
corruption.

**Impact**: Memory corruption. Potential code execution.

### Fix (Code)

Use `fixed` statements or `GCHandle.Alloc` with `GCHandleType.Pinned`:

```csharp
fixed (Chunk* ptr = &chunk) {
    NativeFunction(ptr);
}
```

### Fix (Policy)

- This is purely a code fix.

---

## Attack 6: Unvalidated Strings from Native Code

### The Attack

Strings returned from native SDL3 functions are marshalled via
`StringMarshalling.Utf8` and checked only for null/empty:

```csharp
// Camera.cs:63
string driverName = SDL_GetCameraDriver(index);
if (string.IsNullOrEmpty(driverName)) { ... }
// No further validation — driverName could contain anything
```

If a compromised SDL3 library returns strings containing control characters,
ANSI escape sequences, or format string specifiers, and those strings are
displayed in the security dashboard, an attacker achieves:

- **Log injection**: fake log entries that mask real attacks
- **Terminal escape injection**: arbitrary terminal commands if logs are
  viewed in a terminal emulator that interprets escape sequences
- **XSS**: if strings are rendered in a web-based dashboard without encoding

**Impact**: Log spoofing, potential UI manipulation, XSS in web dashboards.

### Fix (Code)

Sanitize all strings returned from native code:

```csharp
private static string SanitizeNativeString(string? input)
{
    if (string.IsNullOrEmpty(input)) return string.Empty;
    // Strip control characters except common whitespace
    return new string(input.Where(c => !char.IsControl(c) || c == '\n' || c == '\t').ToArray());
}
```

### Fix (Policy)

- Always HTML-encode native strings before rendering in web dashboards.
- Sanitize log output to strip ANSI escape sequences.
- Treat all data from native code as untrusted input at the application layer.

---

## Attack 7: Event Union Type Confusion

### The Attack

The `Event` struct is a union with all fields at `[FieldOffset(0)]`:

```csharp
[FieldOffset(0)] public EventType Type;
[FieldOffset(0)] public MouseMotionEvent Motion;
[FieldOffset(0)] public KeyboardEvent Key;
// ... 30+ overlapping fields
```

If an attacker can inject events (via a compromised input driver, USB device, or
shared memory), they set `Type` to one value but fill the payload for a different
event type. If the application reads the wrong union member based on a spoofed
type, it interprets memory as the wrong struct — potentially leaking adjacent
data or causing incorrect security decisions.

**Impact**: Data misinterpretation. A spoofed keyboard event could be read as a
gamepad event with different field semantics, causing the dashboard to
misinterpret input or ignore security-relevant events.

### Fix (Code)

Validate event type before accessing type-specific fields:

```csharp
switch (evt.Type) {
    case EventType.MouseMotion:
        HandleMouseMotion(evt.Motion); // only access Motion for MouseMotion
        break;
    // ...
}
```

### Fix (Policy)

- Restrict USB device access on the dashboard host (USBGuard on Linux).
- Use input device allowlisting.
- Run the dashboard in a locked-down kiosk mode.

---

## Summary

| # | Attack | Severity | Fix Type | Effort |
|---|--------|----------|----------|--------|
| 1 | DLL/SO hijacking | **Critical** | Code + Policy | Medium |
| 2 | Memory leak DoS | **High** | Code | Low |
| 3 | Delegate use-after-free | **High** | Code | Medium |
| 4 | Buffer overread via trusted counts | **High** | Code | Medium |
| 5 | GC relocation of pinned structs | **High** | Code | Low |
| 6 | Unvalidated native strings | **Medium** | Code + Policy | Low |
| 7 | Event type confusion | **Medium** | Code + Policy | Low |

### What Requires Code Changes

Attacks 1-5 are code defects that must be fixed in the library. No amount of
operational policy can mitigate a use-after-free or an unmanaged memory leak.

### What Requires Policy

Attacks 1, 6, and 7 have operational mitigations. Even after code fixes, defense
in depth requires:

- **Deployment hardening**: read-only library paths, signed binaries, container
  memory limits
- **Input sanitization at the application layer**: never trust native strings
  for display without encoding
- **Host hardening**: USB device control, restricted write access, process
  monitoring
- **Supply chain verification**: build SDL3 from source or verify checksums of
  pre-built binaries
