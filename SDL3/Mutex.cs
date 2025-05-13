using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static SharpSDL3.Sdl;

namespace SharpSDL3;

public static partial class Mutex {
    // /usr/local/include/SDL3/SDL_mutex.h

    public static void BroadcastCondition(nint cond) {
        if (cond == nint.Zero) {
            throw new ArgumentNullException(nameof(cond), "Condition variable cannot be null.");
        }
        SDL_BroadcastCondition(cond);
    }

    public static nint CreateCondition() {
        var cond = SDL_CreateCondition();
        if (cond == nint.Zero) {
            throw new InvalidOperationException("Failed to create condition variable: " + GetError());
        }
        return cond;
    }

    public static nint CreateMutex() {
        var mutex = SDL_CreateMutex();
        if (mutex == nint.Zero) {
            throw new InvalidOperationException("Failed to create mutex: " + GetError());
        }
        return mutex;
    }

    public static nint CreateRWLock() {
        var rwlock = SDL_CreateRWLock();
        if (rwlock == nint.Zero) {
            throw new InvalidOperationException("Failed to create RW lock: " + GetError());
        }
        return rwlock;
    }

    public static void DestroyCondition(nint cond) {
        if (cond == nint.Zero) {
            throw new ArgumentNullException(nameof(cond), "Condition variable cannot be null.");
        }
        SDL_DestroyCondition(cond);
    }

    public static void DestroyMutex(nint mutex) {
        if (mutex == nint.Zero) {
            throw new ArgumentNullException(nameof(mutex), "Mutex cannot be null.");
        }
        SDL_DestroyMutex(mutex);
    }

    public static void DestroyRWLock(nint rwlock) {
        if (rwlock == nint.Zero) {
            throw new ArgumentNullException(nameof(rwlock), "RW lock cannot be null.");
        }
        SDL_DestroyRWLock(rwlock);
    }

    public static void LockMutex(nint mutex) {
        if (mutex == nint.Zero) {
            throw new ArgumentNullException(nameof(mutex), "Mutex cannot be null.");
        }
        SDL_LockMutex(mutex);
    }

    public static void LockRWLockForReading(nint rwlock) {
        if (rwlock == nint.Zero) {
            throw new ArgumentNullException(nameof(rwlock), "RW lock cannot be null.");
        }
        SDL_LockRWLockForReading(rwlock);
    }

    public static void LockRWLockForWriting(nint rwlock) {
        if (rwlock == nint.Zero) {
            throw new ArgumentNullException(nameof(rwlock), "RW lock cannot be null.");
        }
        SDL_LockRWLockForWriting(rwlock);
    }

    public static void SetInitialized(ref InitState state, SdlBool initialized) {
        if (state.Thread == 0) {
            Logger.LogWarn(LogCategory.System, "SetInitialized: State thread is not set.");
        }

        if (initialized.Equals(SdlBool.False)) {
            Logger.LogInfo(LogCategory.System, "SetInitialized: Marking state as uninitialized.");
        } else {
            Logger.LogInfo(LogCategory.System, "SetInitialized: Marking state as initialized.");
        }

        SDL_SetInitialized(ref state, initialized);
    }

    public static SdlBool ShouldInit(ref InitState state) {
        if (state.Status == 0) {
            Logger.LogInfo(LogCategory.System, "ShouldInit: Initialization is required.");
            return SDL_ShouldInit(ref state);
        }

        Logger.LogInfo(LogCategory.System, "ShouldInit: Already initialized.");
        return false;
    }

    public static SdlBool ShouldQuit(ref InitState state) {
        if (state.Thread == 0) {
            Logger.LogWarn(LogCategory.System, "ShouldQuit: State thread is not set.");
            return false;
        }

        SdlBool result = SDL_ShouldQuit(ref state);
        if (!result.Equals(SdlBool.True)) {
            Logger.LogInfo(LogCategory.System, "ShouldQuit: SDL_ShouldQuit returned false.");
        } else {
            Logger.LogInfo(LogCategory.System, "ShouldQuit: SDL_ShouldQuit returned true.");
        }

        return result;
    }

    public static void SignalCondition(nint cond) {
        if (cond == nint.Zero) {
            throw new ArgumentNullException(nameof(cond), "Condition variable cannot be null.");
        }
        SDL_SignalCondition(cond);
    }

    public static SdlBool TryLockMutex(nint mutex) {
        if (mutex == nint.Zero) {
            throw new ArgumentNullException(nameof(mutex), "Mutex cannot be null.");
        }
        return SDL_TryLockMutex(mutex);
    }

    public static SdlBool TryLockRWLockForReading(nint rwlock) {
        if (rwlock == nint.Zero) {
            throw new ArgumentNullException(nameof(rwlock), "RW lock cannot be null.");
        }
        return SDL_TryLockRWLockForReading(rwlock);
    }

    public static SdlBool TryLockRWLockForWriting(nint rwlock) {
        if (rwlock == nint.Zero) {
            throw new ArgumentNullException(nameof(rwlock), "RW lock cannot be null.");
        }
        return SDL_TryLockRWLockForWriting(rwlock);
    }

    public static void UnlockMutex(nint mutex) {
        if (mutex == nint.Zero) {
            throw new ArgumentNullException(nameof(mutex), "Mutex cannot be null.");
        }
        SDL_UnlockMutex(mutex);
    }

    public static void UnlockRWLock(nint rwlock) {
        if (rwlock == nint.Zero) {
            throw new ArgumentNullException(nameof(rwlock), "RW lock cannot be null.");
        }
        SDL_UnlockRWLock(rwlock);
    }

    public static void WaitCondition(nint cond, nint mutex) {
        if (cond == nint.Zero) {
            throw new ArgumentNullException(nameof(cond), "Condition variable cannot be null.");
        }

        if (mutex == nint.Zero) {
            throw new ArgumentNullException(nameof(mutex), "Mutex cannot be null.");
        }

        try {
            SDL_LockMutex(mutex); // Ensure the mutex is locked before waiting
            SDL_WaitCondition(cond, mutex);
        } finally {
            SDL_UnlockMutex(mutex); // Always unlock the mutex after waiting
        }
    }

    public static SdlBool WaitConditionTimeout(nint cond, nint mutex, int timeoutMs) {
        if (cond == nint.Zero || mutex == nint.Zero) {
            throw new ArgumentNullException(cond == nint.Zero ? nameof(cond) : nameof(mutex), "Condition or mutex cannot be null.");
        }

        if (timeoutMs < 0) {
            throw new ArgumentOutOfRangeException(nameof(timeoutMs), "Timeout must be non-negative.");
        }

        var result = SDL_WaitConditionTimeout(cond, mutex, timeoutMs);

        if (!result) {
            var error = GetError();
            if (!string.IsNullOrEmpty(error)) {
                throw new InvalidOperationException($"SDL_WaitConditionTimeout failed: {error}");
            }
        }

        return result;
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BroadcastCondition(nint cond);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateCondition();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateMutex();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateRWLock();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyCondition(nint cond);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyMutex(nint mutex);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyRWLock(nint rwlock);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LockMutex(nint mutex);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LockRWLockForReading(nint rwlock);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LockRWLockForWriting(nint rwlock);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetInitialized(ref InitState state, SdlBool initialized);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ShouldInit(ref InitState state);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ShouldQuit(ref InitState state);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SignalCondition(nint cond);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_TryLockMutex(nint mutex);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_TryLockRWLockForReading(nint rwlock);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_TryLockRWLockForWriting(nint rwlock);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnlockMutex(nint mutex);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnlockRWLock(nint rwlock);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_WaitCondition(nint cond, nint mutex);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WaitConditionTimeout(nint cond, nint mutex, int timeoutMs);
}