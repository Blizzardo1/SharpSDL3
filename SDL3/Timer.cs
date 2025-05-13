using SharpSDL3.Enums;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static SharpSDL3.Delegates;
using static SharpSDL3.Sdl;

namespace SharpSDL3; 
public static partial class Timer {
    // /usr/local/include/SDL3/SDL_timer.h

    public static uint AddTimer(uint interval, SdlTimerCallback callback, nint userdata) {
        if (callback == null) {
            throw new ArgumentNullException(nameof(callback), "Callback cannot be null.");
        }

        if (interval == 0) {
            throw new ArgumentException("Interval must be greater than zero.", nameof(interval));
        }

        uint timerId = SDL_AddTimer(interval, callback, userdata);

        if (timerId == 0) {
            throw new InvalidOperationException("Failed to add timer. SDL_AddTimer returned 0.");
        }

        return timerId;
    }

    public static uint AddTimerNS(ulong interval, SdlNsTimerCallback callback, nint userdata) {
        if (callback == null) {
            throw new ArgumentNullException(nameof(callback), "Callback cannot be null.");
        }

        if (interval == 0) {
            throw new ArgumentException("Interval must be greater than zero.", nameof(interval));
        }

        uint timerId = SDL_AddTimerNS(interval, callback, userdata);

        if (timerId == 0) {
            throw new InvalidOperationException("Failed to add timer. SDL_AddTimerNS returned 0.");
        }

        return timerId;
    }

    public static void Delay(uint ms) {
        if (ms == 0) {
            throw new ArgumentException("Delay duration must be greater than zero.", nameof(ms));
        }

        ulong start = GetTicks();
        SDL_Delay(ms);
        ulong end = GetTicks();

        if (end - start < ms) {
            throw new InvalidOperationException("SDL_Delay did not delay for the expected duration.");
        }
    }

    public static void DelayNS(ulong ns) {
        if (ns == 0) {
            throw new ArgumentException("Delay duration must be greater than zero.", nameof(ns));
        }

        ulong start = GetTicksNS();
        SDL_DelayNS(ns);
        ulong end = GetTicksNS();

        if (end - start < ns) {
            throw new InvalidOperationException("SDL_DelayNS did not delay for the expected duration.");
        }
    }

    public static void DelayPrecise(ulong ns) {
        if (ns == 0) {
            throw new ArgumentException("Delay duration must be greater than zero.", nameof(ns));
        }

        ulong start = GetTicksNS();
        SDL_DelayPrecise(ns);
        ulong end = GetTicksNS();

        if (end - start < ns) {
            throw new InvalidOperationException("SDL_DelayPrecise did not delay for the expected duration.");
        }
    }

    public static ulong GetPerformanceCounter() {
        return SDL_GetPerformanceCounter();
    }

    public static ulong GetPerformanceFrequency() {
        return SDL_GetPerformanceFrequency();
    }

    public static ulong GetTicks() {
        return SDL_GetTicks();
    }

    public static ulong GetTicksNS() {
        return SDL_GetTicksNS();
    }

    public static SdlBool RemoveTimer(uint id) {
        if (id == 0) {
            throw new ArgumentException("Timer ID must be greater than zero.", nameof(id));
        }

        SdlBool result = SDL_RemoveTimer(id);

        if (!result) {
            throw new InvalidOperationException($"Failed to remove timer with ID {id}.");
        }

        return result;
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_AddTimer(uint interval, SdlTimerCallback callback, nint userdata);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_AddTimerNS(ulong interval, SdlNsTimerCallback callback, nint userdata);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_Delay(uint ms);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DelayNS(ulong ns);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DelayPrecise(ulong ns);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong SDL_GetPerformanceCounter();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong SDL_GetPerformanceFrequency();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong SDL_GetTicks();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong SDL_GetTicksNS();
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RemoveTimer(uint id);
}
