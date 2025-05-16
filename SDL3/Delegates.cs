using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3;

public static unsafe partial class Delegates {
    private const UnmanagedType UType = UnmanagedType.LPUTF8Str;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate AppResult SdlAppEventFunc(nint appstate, nint evt);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate AppResult SdlAppInitFunc(nint appstate, int argc, nint argv);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate AppResult SdlAppIterateFunc(nint appstate);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SdlAppQuitFunc(nint appstate, AppResult result);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate AssertState SdlAssertionHandler(nint data, nint userdata);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SdlAudioPostmixCallback(nint userdata, nint spec, nint buffer, int buflen);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SdlAudioStreamCallback(nint userdata, nint stream, int additionalAmount, int totalAmount);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SdlCleanupPropertyCallback(nint userdata, nint value);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SdlClipboardCleanupCallback(nint userdata);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate nint SdlClipboardDataCallback(nint userdata, nint mimeType, nint size);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SdlDialogFileCallback(nint userdata, nint filelist, int filter);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate nint SdlEglAttribArrayCallback();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate nint SdlEglIntArrayCallback();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate EnumerationResult SdlEnumerateDirectoryCallback(nint userdata, nint dirname, nint fname);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SdlEnumeratePropertiesCallback(nint userdata, uint props, nint name);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool SdlEventFilter(nint userdata, nint evt);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SdlHashCallback(nint userdata, nint key);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SdlHashDestroyCallback(nint userdata, nint key, nint value);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SdlHashFreeCallback(nint userdata, nint key);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool SdlHashKeyMatchCallback(nint userdata, nint a, nint b);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SdlHintCallback(nint userdata, nint name, nint oldValue, nint newValue);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate HitTestResult SdlHitTest(nint win, nint area, nint data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SdlLogOutputFunction(nint userdata, LogCategory category, LogPriority priority, [MarshalAs(UType)] string message);

    // /usr/local/include/SDL3/SDL_main.h

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SdlMainFunc(int argc, nint argv);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SdlMainThreadCallback(nint userdata);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ulong SdlNsTimerCallback(nint userdata, uint timerId, ulong interval);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SdlThreadFunction(nint data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint SdlTimerCallback(nint userdata, uint timerId, uint interval);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SdlTlsDestructorCallback(nint value);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SdlTrayCallback(nint userdata, nint entry);
}