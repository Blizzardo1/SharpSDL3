using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using SDL3.Enums;
using SDL3.Structs;
using static SDL3.Delegates;

namespace SDL3;

public static unsafe partial class Assertion {
    public static SdlAssertionHandler GetAssertionHandler(out nint puserdata) {
        var handler = SDL_GetAssertionHandler(out puserdata);
        if (handler == null) {
            throw new InvalidOperationException("Failed to get assertion handler.");
        }
        return handler;
    }

    public static AssertData* GetAssertionReport() {
        var report = SDL_GetAssertionReport();
        if (report == null) {
            throw new InvalidOperationException("Failed to get assertion report.");
        }
        return report;
    }

    public static SdlAssertionHandler GetDefaultAssertionHandler() {
        var handler = SDL_GetDefaultAssertionHandler();
        if (handler == null) {
            throw new InvalidOperationException("Failed to get default assertion handler.");
        }
        return handler;
    }

    public static AssertState ReportAssertion(ref AssertData data, string func, string file, int line) {
        // Add validation or logging to make the wrapper less trivial
        if (data.TriggerCount == 0) {
            Console.WriteLine($"Assertion triggered in function '{func}' at {file}:{line}");
        }

        // Call the native method
        var result = SDL_ReportAssertion(ref data, func, file, line);

        // Handle the result or add additional logic
        switch (result) {
            case AssertState.Retry:
                Logger.LogInfo(LogCategory.System, "Retrying assertion...");
                break;

            case AssertState.Break:
                Logger.LogError(LogCategory.System, "Breaking on assertion...");
                break;

            case AssertState.Abort:
                Logger.LogError(LogCategory.System, "Aborting due to assertion...");
                break;

            case AssertState.Ignore:
                Logger.LogWarn(LogCategory.System, "Ignoring assertion...");
                break;

            case AssertState.AlwaysIgnore:
                Logger.LogWarn(LogCategory.System, "Always ignoring assertion...");
                break;
        }

        return result;
    }

    public static void ResetAssertionReport() {
        SDL_ResetAssertionReport();
    }

    public static void SetAssertionHandler(SdlAssertionHandler handler, nint userdata) {
        if (handler == null) {
            throw new ArgumentNullException(nameof(handler), "Assertion handler cannot be null.");
        }

        SDL_SetAssertionHandler(handler, userdata);
    }

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlAssertionHandler SDL_GetAssertionHandler(out nint puserdata);

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial AssertData* SDL_GetAssertionReport();

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlAssertionHandler SDL_GetDefaultAssertionHandler();

    [LibraryImport(Sdl.NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial AssertState SDL_ReportAssertion(ref AssertData data, string func, string file, int line);

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ResetAssertionReport();

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetAssertionHandler(SdlAssertionHandler handler, nint userdata);
}