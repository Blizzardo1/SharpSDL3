using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using SharpSDL3.Enums;
using SharpSDL3.Structs;
using static SharpSDL3.Delegates;

namespace SharpSDL3;

public static unsafe partial class Assertion {
    public static SdlAssertionHandler GetAssertionHandler(out nint puserdata) {
        var handler = SDL_GetAssertionHandler(out puserdata);
        return handler ?? throw new InvalidOperationException("Failed to get assertion handler.");
    }

    public static nint GetAssertionReport() {
        var report = SDL_GetAssertionReport();
        if (report == nint.Zero) {
            throw new InvalidOperationException("Failed to get assertion report.");
        }
        return report;
    }

    public static SdlAssertionHandler GetDefaultAssertionHandler() {
        var handler = SDL_GetDefaultAssertionHandler() ?? throw new InvalidOperationException("Failed to get default assertion handler.");
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
                Logger.LogError(LogCategory.Error, "Breaking on assertion...");
                break;

            case AssertState.Abort:
                Logger.LogError(LogCategory.Error, "Aborting due to assertion...");
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
    private static partial nint SDL_GetAssertionReport();

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlAssertionHandler SDL_GetDefaultAssertionHandler();

    [LibraryImport(Sdl.NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial AssertState SDL_ReportAssertion(ref AssertData data, string func, string file, int line);

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ResetAssertionReport();

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetAssertionHandler(SdlAssertionHandler handler, nint userdata);
}