using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSDL3;

public static partial class Sdl {
    // /usr/local/include/SDL3/SDL_mutex.h

    /// <summary>Restart all threads that are waiting on the condition variable.</summary>

    /// <param name="cond">the condition variable to signal.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SignalCondition"/>
    /// <seealso cref="WaitCondition"/>
    /// <seealso cref="WaitConditionTimeout"/>
    /// </remarks>

    public static void BroadcastCondition(nint cond) {
        if (cond == nint.Zero) {
            throw new ArgumentNullException(nameof(cond), "Condition variable cannot be null.");
        }
        SDL_BroadcastCondition(cond);
    }

    /// <summary>Create a condition variable.</summary>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BroadcastCondition"/>
    /// <seealso cref="SignalCondition"/>
    /// <seealso cref="WaitCondition"/>
    /// <seealso cref="WaitConditionTimeout"/>
    /// <seealso cref="DestroyCondition"/>
    /// </remarks>
    /// <returns>(SDL_Condition *) Returns a new condition variable or <see langword="null" />on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint CreateCondition() {
        nint cond = SDL_CreateCondition();
        if (cond == nint.Zero) {
            throw new InvalidOperationException($"Failed to create condition variable: {GetError()}");
        }
        return cond;
    }

    /// <summary>Create a new mutex.</summary>
    /// <remarks>
    /// All newly-created mutexes begin in the unlocked state.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="DestroyMutex"/>
    /// <seealso cref="LockMutex"/>
    /// <seealso cref="TryLockMutex"/>
    /// <seealso cref="UnlockMutex"/>
    /// </remarks>
    /// <returns>(SDL_Mutex *) Returns the initialized and unlocked mutex or<see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint CreateMutex() {
        nint mutex = SDL_CreateMutex();
        if (mutex == nint.Zero) {
            throw new InvalidOperationException($"Failed to create mutex: {GetError()}");
        }
        return mutex;
    }

    /// <summary>Create a new read/write lock.</summary>
    /// <remarks>
    /// A read/write lock is useful for situations where you have multiple threads
    /// trying to access a resource that is rarely updated. All threads requesting
    /// a read-only lock will be allowed to run in parallel; if a thread requests a
    /// write lock, it will be provided exclusive access. This makes it safe for
    /// multiple threads to use a resource at the same time if they promise not to
    /// change it, and when it has to be changed, the rwlock will serve as a
    /// gateway to make sure those changes can be made safely.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="DestroyRwLock"/>
    /// <seealso cref="LockRwLockForReading"/>
    /// <seealso cref="LockRwLockForWriting"/>
    /// <seealso cref="TryLockRwLockForReading"/>
    /// <seealso cref="TryLockRwLockForWriting"/>
    /// <seealso cref="UnlockRwLock"/>
    /// </remarks>
    /// <returns>(SDL_RWLock *) Returns the initialized and unlockedread/write lock or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint CreateRwLock() {
        nint rwlock = SDL_CreateRWLock();
        if (rwlock == nint.Zero) {
            throw new InvalidOperationException($"Failed to create RW lock: {GetError()}");
        }
        return rwlock;
    }

    /// <summary>Destroy a condition variable.</summary>

    /// <param name="cond">the condition variable to destroy.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateCondition"/>
    /// </remarks>

    public static void DestroyCondition(nint cond) {
        if (cond == nint.Zero) {
            throw new ArgumentNullException(nameof(cond), "Condition variable cannot be null.");
        }
        SDL_DestroyCondition(cond);
    }

    /// <summary>Destroy a mutex created with SDL_CreateMutex().</summary>

    /// <param name="mutex">the mutex to destroy.</param>
    /// <remarks>
    /// This function must be called on any mutex that is no longer needed. Failure
    /// to destroy a mutex will result in a system memory or resource leak. While
    /// it is safe to destroy a mutex that is unlocked, it is not safe to attempt
    /// to destroy a locked mutex, and may result in undefined behavior depending
    /// on the platform.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateMutex"/>
    /// </remarks>

    public static void DestroyMutex(nint mutex) {
        if (mutex == nint.Zero) {
            throw new ArgumentNullException(nameof(mutex), "Mutex cannot be null.");
        }
        SDL_DestroyMutex(mutex);
    }

    /// <summary>Destroy a read/write lock created with SDL_CreateRWLock().</summary>

    /// <param name="rwlock">the rwlock to destroy.</param>
    /// <remarks>
    /// This function must be called on any read/write lock that is no longer
    /// needed. Failure to destroy a rwlock will result in a system memory or
    /// resource leak. While it is safe to destroy a rwlock that is unlocked, it
    /// is not safe to attempt to destroy a locked rwlock, and may result in
    /// undefined behavior depending on the platform.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateRwLock"/>
    /// </remarks>

    public static void DestroyRwLock(nint rwlock) {
        if (rwlock == nint.Zero) {
            throw new ArgumentNullException(nameof(rwlock), "RW lock cannot be null.");
        }
        SDL_DestroyRWLock(rwlock);
    }

    /// <summary>Lock the mutex.</summary>

    /// <param name="mutex">the mutex to lock.</param>
    /// <remarks>
    /// This will block until the mutex is available, which is to say it is in the
    /// unlocked state and the OS has chosen the caller as the next thread to lock
    /// it. Of all threads waiting to lock the mutex, only one may do so at a time.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="TryLockMutex"/>
    /// <seealso cref="UnlockMutex"/>
    /// </remarks>

    public static void LockMutex(nint mutex) {
        if (mutex == nint.Zero) {
            throw new ArgumentNullException(nameof(mutex), "Mutex cannot be null.");
        }
        SDL_LockMutex(mutex);
    }

    /// <summary>Lock the read/write lock for read only operations.</summary>

    /// <param name="rwlock">the read/write lock to lock.</param>
    /// <remarks>
    /// This will block until the rwlock is available, which is to say it is not
    /// locked for writing by any other thread. Of all threads waiting to lock the
    /// rwlock, all may do so at the same time as long as they are requesting
    /// read-only access; if a thread wants to lock for writing, only one may do so
    /// at a time, and no other threads, read-only or not, may hold the lock at the
    /// same time.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LockRwLockForWriting"/>
    /// <seealso cref="TryLockRwLockForReading"/>
    /// <seealso cref="UnlockRwLock"/>
    /// </remarks>

    public static void LockRwLockForReading(nint rwlock) {
        if (rwlock == nint.Zero) {
            throw new ArgumentNullException(nameof(rwlock), "RW lock cannot be null.");
        }
        SDL_LockRWLockForReading(rwlock);
    }

    /// <summary>Lock the read/write lock for write operations.</summary>

    /// <param name="rwlock">the read/write lock to lock.</param>
    /// <remarks>
    /// This will block until the rwlock is available, which is to say it is not
    /// locked for reading or writing by any other thread. Only one thread may hold
    /// the lock when it requests write access; all other threads, whether they
    /// also want to write or only want read-only access, must wait until the
    /// writer thread has released the lock.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LockRwLockForReading"/>
    /// <seealso cref="TryLockRwLockForWriting"/>
    /// <seealso cref="UnlockRwLock"/>
    /// </remarks>

    public static void LockRwLockForWriting(nint rwlock) {
        if (rwlock == nint.Zero) {
            throw new ArgumentNullException(nameof(rwlock), "RW lock cannot be null.");
        }
        SDL_LockRWLockForWriting(rwlock);
    }

    /// <summary>Finish an initialization state transition.</summary>

    /// <param name="state">the initialization state to check.</param>
    /// <param name="initialized">the new initialization state.</param>
    /// <remarks>
    /// This function sets the status of the passed in state to
    /// SDL_INIT_STATUS_INITIALIZED or
    /// SDL_INIT_STATUS_UNINITIALIZED and allows
    /// any threads waiting for the status to proceed.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ShouldInit"/>
    /// <seealso cref="ShouldQuit"/>
    /// </remarks>

    public static void SetInitialized(ref InitState state, SdlBool initialized) {
        if (state.Thread == 0) {
            LogWarn(LogCategory.System, "SetInitialized: State thread is not set.");
        }

        if (initialized.Equals(SdlBool.False)) {
            LogInfo(LogCategory.System, "SetInitialized: Marking state as uninitialized.");
        } else {
            LogInfo(LogCategory.System, "SetInitialized: Marking state as initialized.");
        }

        SDL_SetInitialized(ref state, initialized);
    }

    /// <summary>Return whether initialization should be done.</summary>

    /// <param name="state">the initialization state to check.</param>
    /// <remarks>
    /// This function checks the passed in state and if initialization should be
    /// done, sets the status to
    /// SDL_INIT_STATUS_INITIALIZING and returns
    /// <see langword="true" />. If another thread is already modifying this state, it will wait until
    /// that's done before returning.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetInitialized"/>
    /// <seealso cref="ShouldQuit"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if initialization needs to be done, <see langword="false" /> otherwise.</returns>

    public static SdlBool ShouldInit(ref InitState state) {
        if (state.Status == 0) {
            LogInfo(LogCategory.System, "ShouldInit: Initialization is required.");
            return SDL_ShouldInit(ref state);
        }

        LogInfo(LogCategory.System, "ShouldInit: Already initialized.");
        return false;
    }

    /// <summary>Return whether cleanup should be done.</summary>

    /// <param name="state">the initialization state to check.</param>
    /// <remarks>
    /// This function checks the passed in state and if cleanup should be done,
    /// sets the status to
    /// SDL_INIT_STATUS_UNINITIALIZING and
    /// returns <see langword="true" />.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetInitialized"/>
    /// <seealso cref="ShouldInit"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if cleanup needs to be done, <see langword="false" /> otherwise.</returns>

    public static SdlBool ShouldQuit(ref InitState state) {
        if (state.Thread == 0) {
            LogWarn(LogCategory.System, "ShouldQuit: State thread is not set.");
            return false;
        }

        SdlBool result = SDL_ShouldQuit(ref state);
        if (!result.Equals(SdlBool.True)) {
            LogInfo(LogCategory.System, "ShouldQuit: SDL_ShouldQuit returned false.");
        } else {
            LogInfo(LogCategory.System, "ShouldQuit: SDL_ShouldQuit returned true.");
        }

        return result;
    }

    /// <summary>Restart one of the threads that are waiting on the condition variable.</summary>

    /// <param name="cond">the condition variable to signal.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BroadcastCondition"/>
    /// <seealso cref="WaitCondition"/>
    /// <seealso cref="WaitConditionTimeout"/>
    /// </remarks>

    public static void SignalCondition(nint cond) {
        if (cond == nint.Zero) {
            throw new ArgumentNullException(nameof(cond), "Condition variable cannot be null.");
        }
        SDL_SignalCondition(cond);
    }

    /// <summary>Try to lock a mutex without blocking.</summary>

    /// <param name="mutex">the mutex to try to lock.</param>
    /// <remarks>
    /// This works just like SDL_LockMutex(), but if the mutex is
    /// not available, this function returns <see langword="false" /> immediately.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LockMutex"/>
    /// <seealso cref="UnlockMutex"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success, <see langword="false" /> if the mutex would block.</returns>

    public static SdlBool TryLockMutex(nint mutex) {
        if (mutex == nint.Zero) {
            throw new ArgumentNullException(nameof(mutex), "Mutex cannot be null.");
        }
        return SDL_TryLockMutex(mutex);
    }

    /// <summary>Try to lock a read/write lock for reading without blocking.</summary>

    /// <param name="rwlock">the rwlock to try to lock.</param>
    /// <remarks>
    /// This works just like
    /// SDL_LockRWLockForReading(), but if the rwlock
    /// is not available, then this function returns <see langword="false" /> immediately.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LockRwLockForReading"/>
    /// <seealso cref="TryLockRwLockForWriting"/>
    /// <seealso cref="UnlockRwLock"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success, <see langword="false" /> if the lock would block.</returns>

    public static SdlBool TryLockRwLockForReading(nint rwlock) {
        if (rwlock == nint.Zero) {
            throw new ArgumentNullException(nameof(rwlock), "RW lock cannot be null.");
        }
        return SDL_TryLockRWLockForReading(rwlock);
    }

    /// <summary>Try to lock a read/write lock for writing without blocking.</summary>

    /// <param name="rwlock">the rwlock to try to lock.</param>
    /// <remarks>
    /// This works just like
    /// SDL_LockRWLockForWriting(), but if the rwlock
    /// is not available, then this function returns <see langword="false" /> immediately.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LockRwLockForWriting"/>
    /// <seealso cref="TryLockRwLockForReading"/>
    /// <seealso cref="UnlockRwLock"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success, <see langword="false" /> if the lock would block.</returns>

    public static SdlBool TryLockRwLockForWriting(nint rwlock) {
        if (rwlock == nint.Zero) {
            throw new ArgumentNullException(nameof(rwlock), "RW lock cannot be null.");
        }
        return SDL_TryLockRWLockForWriting(rwlock);
    }

    /// <summary>Unlock the mutex.</summary>

    /// <param name="mutex">the mutex to unlock.</param>
    /// <remarks>
    /// It is legal for the owning thread to lock an already-locked mutex. It must
    /// unlock it the same number of times before it is actually made available for
    /// other threads in the system (this is known as a &quot;recursive mutex&quot;).
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LockMutex"/>
    /// <seealso cref="TryLockMutex"/>
    /// </remarks>

    public static void UnlockMutex(nint mutex) {
        if (mutex == nint.Zero) {
            throw new ArgumentNullException(nameof(mutex), "Mutex cannot be null.");
        }
        SDL_UnlockMutex(mutex);
    }

    /// <summary>Unlock the read/write lock.</summary>

    /// <param name="rwlock">the rwlock to unlock.</param>
    /// <remarks>
    /// Use this function to unlock the rwlock, whether it was locked for read-only
    /// or write operations.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LockRwLockForReading"/>
    /// <seealso cref="LockRwLockForWriting"/>
    /// <seealso cref="TryLockRwLockForReading"/>
    /// <seealso cref="TryLockRwLockForWriting"/>
    /// </remarks>

    public static void UnlockRwLock(nint rwlock) {
        if (rwlock == nint.Zero) {
            throw new ArgumentNullException(nameof(rwlock), "RW lock cannot be null.");
        }
        SDL_UnlockRWLock(rwlock);
    }

    /// <summary>Wait until a condition variable is signaled.</summary>

    /// <param name="cond">the condition variable to wait on.</param>
    /// <param name="mutex">the mutex used to coordinate thread access.</param>
    /// <remarks>
    /// This function unlocks the specified mutex and waits for another thread to
    /// call SDL_SignalCondition() or
    /// SDL_BroadcastCondition() on the condition
    /// variable cond. Once the condition variable is signaled, the mutex is
    /// re-locked and the function returns.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BroadcastCondition"/>
    /// <seealso cref="SignalCondition"/>
    /// <seealso cref="WaitConditionTimeout"/>
    /// </remarks>

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

    /// <summary>Wait until a condition variable is signaled or a certain time has passed.</summary>

    /// <param name="cond">the condition variable to wait on.</param>
    /// <param name="mutex">the mutex used to coordinate thread access.</param>
    /// <param name="timeoutMS">the maximum time to wait, in milliseconds, or -1 to wait indefinitely.</param>
    /// <remarks>
    /// This function unlocks the specified mutex and waits for another thread to
    /// call SDL_SignalCondition() or
    /// SDL_BroadcastCondition() on the condition
    /// variable cond, or for the specified time to elapse. Once the condition
    /// variable is signaled or the time elapsed, the mutex is re-locked and the
    /// function returns.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BroadcastCondition"/>
    /// <seealso cref="SignalCondition"/>
    /// <seealso cref="WaitCondition"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the condition variable is signaled, <see langword="false" /> if thecondition is not signaled in the allotted time.</returns>

    public static SdlBool WaitConditionTimeout(nint cond, nint mutex, int timeoutMs) {
        if (cond == nint.Zero || mutex == nint.Zero) {
            throw new ArgumentNullException(cond == nint.Zero ? nameof(cond) : nameof(mutex), "Condition or mutex cannot be null.");
        }

        if (timeoutMs < 0) {
            throw new ArgumentOutOfRangeException(nameof(timeoutMs), "Timeout must be non-negative.");
        }

        SdlBool result = SDL_WaitConditionTimeout(cond, mutex, timeoutMs);

        if (!result) {
            string error = GetError();
            if (!string.IsNullOrEmpty(error)) {
                throw new InvalidOperationException($"WaitConditionTimeout failed: {error}");
            }
        }

        return result;
    }

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BroadcastCondition(nint cond);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateCondition();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateMutex();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateRWLock();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyCondition(nint cond);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyMutex(nint mutex);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyRWLock(nint rwlock);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LockMutex(nint mutex);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LockRWLockForReading(nint rwlock);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LockRWLockForWriting(nint rwlock);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetInitialized(ref InitState state, SdlBool initialized);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ShouldInit(ref InitState state);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ShouldQuit(ref InitState state);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SignalCondition(nint cond);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_TryLockMutex(nint mutex);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_TryLockRWLockForReading(nint rwlock);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_TryLockRWLockForWriting(nint rwlock);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnlockMutex(nint mutex);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnlockRWLock(nint rwlock);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_WaitCondition(nint cond, nint mutex);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WaitConditionTimeout(nint cond, nint mutex, int timeoutMs);
}