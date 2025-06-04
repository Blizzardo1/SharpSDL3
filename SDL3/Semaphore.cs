using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSDL3;

public static partial class Sdl {

    public static nint Create(uint initialValue) {
        nint sem = SDL_CreateSemaphore(initialValue);
        if (sem == 0) {
            throw new InvalidOperationException("CreateSemaphore failed");
        }
        return sem;
    }

    public static void Destroy(nint sem) {
        if (sem == 0) {
            throw new ArgumentNullException(nameof(sem), "Semaphore pointer is null");
        }
        SDL_DestroySemaphore(sem);
    }

    public static uint GetValue(nint sem) {
        if (sem == 0) {
            throw new ArgumentNullException(nameof(sem), "Semaphore pointer is null");
        }
        return SDL_GetSemaphoreValue(sem);
    }

    public static void Signal(nint sem) {
        if (sem == 0) {
            throw new ArgumentNullException(nameof(sem), "Semaphore pointer is null");
        }
        SDL_SignalSemaphore(sem);
    }

    public static bool TryWait(nint sem) {
        if (sem == 0) {
            throw new ArgumentNullException(nameof(sem), "Semaphore pointer is null");
        }
        return SDL_TryWaitSemaphore(sem);
    }

    public static void Wait(nint sem) {
        if (sem == 0) {
            throw new ArgumentNullException(nameof(sem), "Semaphore pointer is null");
        }
        SDL_WaitSemaphore(sem);
    }

    public static bool WaitTimeout(nint sem, int timeoutMs) {
        if (sem == 0) {
            throw new ArgumentNullException(nameof(sem), "Semaphore pointer is null");
        }
        return SDL_WaitSemaphoreTimeout(sem, timeoutMs);
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateSemaphore(uint initialValue);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroySemaphore(nint sem);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetSemaphoreValue(nint sem);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SignalSemaphore(nint sem);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_TryWaitSemaphore(nint sem);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_WaitSemaphore(nint sem);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WaitSemaphoreTimeout(nint sem, int timeoutMs);
}