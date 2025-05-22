using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSDL3;

public static unsafe partial class Atomic {
    // /usr/local/include/SDL3/SDL_atomic.h

    public static int AddAtomicInt(ref AtomicInt a, int v) {
        // Validate the input parameters
        if (a.Value != 0) {
            // Attempt to add the value atomically
            return SDL_AddAtomicInt(ref a, v);
        }
        return 0;
    }

    public static bool CompareAndSwapAtomicInt(ref AtomicInt a, int oldval, int newval) {
        // Validate the input parameters
        if (a.Value == oldval) {
            // Attempt to swap the value atomically
            return SDL_CompareAndSwapAtomicInt(ref a, oldval, newval).Equals(true);
        }
        return false;
    }

    public static bool CompareAndSwapAtomicPointer(ref nint a, nint oldval, nint newval) {
        // Validate the input parameters
        if (a == oldval) {
            // Attempt to swap the value atomically
            return SDL_CompareAndSwapAtomicPointer(ref a, oldval, newval).Equals(true);
        }
        return false;
    }

    public static bool CompareAndSwapAtomicU32(ref AtomicU32 a, uint oldval, uint newval) {
        // Validate the input parameters
        if (a.Value == oldval) {
            // Attempt to swap the value atomically
            return SDL_CompareAndSwapAtomicU32(ref a, oldval, newval).Equals(true);
        }
        return false;
    }

    public static int GetAtomicInt(ref AtomicInt a) {
        // Validate the input parameters
        if (a.Value != 0) {
            // Attempt to get the value atomically
            return SDL_GetAtomicInt(ref a);
        }
        return 0;
    }

    public static nint GetAtomicPointer(ref nint a) {
        // Validate the input parameters
        if (a != nint.Zero) {
            // Attempt to get the value atomically
            return SDL_GetAtomicPointer(ref a);
        }
        return nint.Zero;
    }

    public static uint GetAtomicU32(ref AtomicU32 a) {
        // Validate the input parameters
        if (a.Value != 0) {
            // Attempt to get the value atomically
            return SDL_GetAtomicU32(ref a);
        }
        return 0;
    }

    public static void LockSpinlock(nint @lock) {
        if (!SDL_TryLockSpinlock(@lock)) {
            SDL_LockSpinlock(@lock);
        }
    }

    public static void MemoryBarrierAcquire() {
        SDL_MemoryBarrierAcquireFunction();
    }

    public static void MemoryBarrierRelease() {
        SDL_MemoryBarrierReleaseFunction();
    }

    public static int SetAtomicInt(ref AtomicInt a, int v) {
        // Validate the input parameters
        if (a.Value != v) {
            // Attempt to set the value atomically
            return SDL_SetAtomicInt(ref a, v);
        }
        return a.Value;
    }

    public static nint SetAtomicPointer(ref nint a, nint v) {
        // Validate the input parameters
        if (a != v) {
            // Attempt to set the value atomically
            return SDL_SetAtomicPointer(ref a, v);
        }
        return a;
    }

    public static uint SetAtomicU32(ref AtomicU32 a, uint v) {
        // Validate the input parameters
        if (a.Value != v) {
            // Attempt to set the value atomically
            return SDL_SetAtomicU32(ref a, v);
        }
        return a.Value;
    }

    public static void UnlockSpinlock(nint @lock) {
        if (@lock == nint.Zero) {
            throw new ArgumentException("Lock pointer cannot be null.", nameof(@lock));
        }
        SDL_UnlockSpinlock(@lock);
    }

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_AddAtomicInt(ref AtomicInt a, int v);

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CompareAndSwapAtomicInt(ref AtomicInt a, int oldval, int newval);

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CompareAndSwapAtomicPointer(ref nint a, nint oldval, nint newval);

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CompareAndSwapAtomicU32(ref AtomicU32 a, uint oldval, uint newval);

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetAtomicInt(ref AtomicInt a);

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetAtomicPointer(ref nint a);

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetAtomicU32(ref AtomicU32 a);

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LockSpinlock(nint @lock);

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_MemoryBarrierAcquireFunction();

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_MemoryBarrierReleaseFunction();

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetAtomicInt(ref AtomicInt a, int v);

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_SetAtomicPointer(ref nint a, nint v);

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_SetAtomicU32(ref AtomicU32 a, uint v);

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_TryLockSpinlock(nint @lock);

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnlockSpinlock(nint @lock);
}