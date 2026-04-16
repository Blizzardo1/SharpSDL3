# SharpSDL3 — Top 5 Recommendations

Prioritized by risk and impact. Items 1-3 are security-critical.

---

## 1. Fix unmanaged memory leaks

`Render.cs:54-62` allocates via `Marshal.AllocHGlobal` and never frees.
`JoySticks.cs:999-1005` frees `AllocHGlobal` memory with `SDL_free()` instead
of `Marshal.FreeHGlobal` — undefined behavior. `Sdl.StructureToPointer<T>`
documents caller responsibility but provides no safe wrapper.

**What to do:**
- Audit every `AllocHGlobal` call and add `try/finally` with `FreeHGlobal`
- Fix the `JoySticks.cs` mismatched free
- Consider an `IDisposable` wrapper (e.g., `NativeBuffer<T>`) that frees on
  dispose so callers can use `using` statements

**Status:** Unit tests now cover the validation guards around these methods but
do not exercise the native allocation paths. Once fixes are in place, add tests
that verify no `AllocHGlobal` call exists without a matching `FreeHGlobal` in
the same method (a Roslyn analyzer rule would be ideal).

---

## 2. Pin delegates passed to native code

Delegates handed to `SDL_AddEventWatch`, `SDL_SetEventFilter`,
`SDL_SetLogOutputFunction`, `SDL_SetAudioPostmixCallback`, and 5+ other native
callbacks are not retained by managed code. The GC can collect them while SDL3
still holds a function pointer — a use-after-free.

**What to do:**
- Keep a `static` collection of every delegate passed to a native `Set`/`Add`
  callback function
- Remove from the collection in the corresponding `Remove`/`Unset` call
- Alternatively, pin with `GCHandle.Alloc(..., GCHandleType.Normal)`

**Status:** The current test suite validates that null delegates are rejected.
After the fix, add tests that register a callback, force a GC
(`GC.Collect(); GC.WaitForPendingFinalizers()`), and verify the delegate
is still callable.

---

## 3. Bounds-check native array counts

Functions like `GetAudioPlaybackDevices`, `GetCameras`, `GetJoysticks`, and
`GetTouchDevices` trust the `count` returned by native SDL3 and pass it
directly to `Marshal.Copy` or pointer read loops. A corrupted or malicious
SDL3 library can return an inflated count to overread memory.

Additionally, `JoySticks.cs:591` uses `Marshal.ReadInt32` for pointer-sized
values — wrong on 64-bit platforms.

**What to do:**
- Add a reasonable upper-bound check on every count from native code
  (e.g., `if (count < 0 || count > 4096) return [];`)
- Fix the `ReadInt32` → `ReadIntPtr` bug in `JoySticks.cs`
- Add a fuzz test that verifies out-of-range counts are rejected

**Status:** Fuzz tests currently cover struct marshalling and validation guards.
After the fix, extend `FuzzTests.cs` with boundary tests for array count
handling.

---

## 4. Standardize error handling

The library uses three different error patterns inconsistently:
- **Throw exceptions** (`ArgumentNullException`, `SdlException`,
  `InvalidOperationException`) — used in Render.cs, Events.cs, Mutex.cs
- **Return false/zero + log** — used in Sdl.cs surface/window functions
- **Both** — some methods throw on some inputs and return false on others

Callers cannot predict which pattern a given method uses.

**What to do:**
- Choose one contract: exceptions for programming errors (null args, invalid
  enum), return codes for SDL runtime failures
- Apply it uniformly across all public methods
- Document the contract in a `CONTRIBUTING.md` section

**Status:** The test suite already tests both patterns (exception-throwing and
return-value guards). After standardizing, update tests to match the chosen
contract — tests that currently `Assert.Throws` or `Assert.False` may need to
swap.

---

## 5. Split `Sdl.cs` into partial class files by subsystem

At 7,883 lines, `Sdl.cs` contains ~310 public methods spanning windows,
surfaces, clipboard, threads, properties, hints, pixels, and more. It is
difficult to navigate, review, and maintain.

**What to do:**
- Split into partial class files by functional area:
  `Sdl.Window.cs`, `Sdl.Surface.cs`, `Sdl.Properties.cs`, `Sdl.Thread.cs`,
  `Sdl.Clipboard.cs`, `Sdl.Pixel.cs`, etc.
- Keep the private `SDL_*` P/Invoke declarations adjacent to their public
  wrappers (not in a separate file)
- This is a refactor with no behavior change — existing tests should pass
  without modification

**Status:** The test suite covers the public API surface. It will serve as a
regression safety net during the refactor with no changes needed.
