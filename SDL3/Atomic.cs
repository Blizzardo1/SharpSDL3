using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSDL3;

public static unsafe partial class Sdl {
    // /usr/local/include/SDL3/SDL_atomic.h

    /// <summary>
    /// Decrement an atomic variable used as a reference count.
    /// </summary>
    /// <param name="a">a pointer to an <see cref="AtomicInt" /> to decrement.</param>
    /// <remarks>
    /// <para><strong>Note</strong>: If you don't know what this macro is for, you shouldn't use it!</para>
    /// <para><strong>Thread Safety</strong>: It is safe top call this macro from any thread.</para>
    /// <para><strong>Version</strong>: This macro is available since SDL 3.2.0.</para>
    /// <seealso cref="AtomicIncRef" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the variable reached zero after decrementing, <see langword="false" /> otherwise.</returns>
    public static bool AtomicDecRef(ref AtomicInt a) => AddAtomicInt(ref a, -1) == 1;

    /// <summary>
    /// Increment an atomic variable used as a reference count.
    /// </summary>
    /// <param name="a">a pointer to an <see cref="AtomicInt" /> to increment.</param>
    /// <remarks>
    /// <para><strong>Note</strong>: If you don't know what this macro is for, you shouldn't use it!</para>
    /// <para><strong>Thread Safety</strong>: It is safe top call this macro from any thread.</para>
    /// <para><strong>Version</strong>: This macro is available since SDL 3.2.0.</para>
    /// <seealso cref="AtomicDecRef" />
    /// </remarks>
    /// <returns>Returns the previous value of the atomic variable.</returns>
    public static int AtomicIncRef(ref AtomicInt a) => AddAtomicInt(ref a, 1);

    /// <summary>Add to an atomic variable.</summary>
    /// <param name="a">a pointer to an SDL_AtomicInt variable to be modified.</param>
    /// <param name="v">the desired value to add.</param>
    /// <remarks>
    /// This function also acts as a full memory barrier.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="AtomicDecRef" />
    /// <seealso cref="AtomicIncRef" />
    /// </remarks>
    /// <returns>Returns the previous value of the atomic variable.</returns>
    public static int AddAtomicInt(ref AtomicInt a, int v) {
        // Validate the input parameters
        return a.Value != 0 ?
            // Attempt to add the value atomically
            SDL_AddAtomicInt(ref a, v) : 0;
    }

    /// <summary>Set an atomic variable to a new value if it is currently an old value.</summary>
    /// <param name="a">a pointer to an SDL_AtomicInt variable to be modified.</param>
    /// <param name="oldVal">the old value.</param>
    /// <param name="newVal">the new value.</param>
    /// <remarks>
    /// Note: If you don't know what this function is for, you shouldn't use it!
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAtomicInt" />
    /// <seealso cref="SetAtomicInt" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the atomic variable was set, <see langword="false" /> otherwise.</returns>
    public static bool CompareAndSwapAtomicInt(ref AtomicInt a, int oldVal, int newVal) {
        // Validate the input parameters
        return a.Value == oldVal &&
               // Attempt to swap the value atomically
               SDL_CompareAndSwapAtomicInt(ref a, oldVal, newVal).Equals(true);
    }

    /// <summary>Set a pointer to a new value if it is currently an old value.</summary>
    /// <param name="a">a pointer to a pointer.</param>
    /// <param name="oldVal">the old pointer value.</param>
    /// <param name="newVal">the new pointer value.</param>
    /// <remarks>
    /// Note: If you don't know what this function is for, you shouldn't use it!
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CompareAndSwapAtomicInt" />
    /// <seealso cref="GetAtomicPointer" />
    /// <seealso cref="SetAtomicPointer" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the pointer was set, <see langword="false" /> otherwise.</returns>
    public static bool CompareAndSwapAtomicPointer(ref nint a, nint oldVal, nint newVal) {
        // Validate the input parameters
        return a == oldVal &&
               // Attempt to swap the value atomically
               SDL_CompareAndSwapAtomicPointer(ref a, oldVal, newVal).Equals(true);
    }

    /// <summary>Set an atomic variable to a new value if it is currently an old value.</summary>
    /// <param name="a">a pointer to an SDL_AtomicU32 variable to be modified.</param>
    /// <param name="oldVal">the old value.</param>
    /// <param name="newVal">the new value.</param>
    /// <remarks>
    /// Note: If you don't know what this function is for, you shouldn't use it!
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAtomicU32" />
    /// <seealso cref="SetAtomicU32" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the atomic variable was set, <see langword="false" /> otherwise.</returns>
    public static bool CompareAndSwapAtomicU32(ref AtomicU32 a, uint oldVal, uint newVal) {
        // Validate the input parameters
        return a.Value == oldVal &&
               // Attempt to swap the value atomically
               SDL_CompareAndSwapAtomicU32(ref a, oldVal, newVal).Equals(true);
    }

    /// <summary>Get the value of an atomic variable.</summary>
    /// <param name="a">a pointer to an SDL_AtomicInt variable.</param>
    /// <remarks>
    /// Note: If you don't know what this function is for, you shouldn't use it!
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAtomicInt" />
    /// </remarks>
    /// <returns>Returns the current value of an atomic variable.</returns>
    public static int GetAtomicInt(ref AtomicInt a) {
        // Validate the input parameters
        return a.Value != 0 ?
            // Attempt to get the value atomically
            SDL_GetAtomicInt(ref a) : 0;
    }

    /// <summary>Get the value of a pointer atomically.</summary>
    /// <param name="a">a pointer to a pointer.</param>
    /// <remarks>
    /// Note: If you don't know what this function is for, you shouldn't use it!
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CompareAndSwapAtomicPointer" />
    /// <seealso cref="SetAtomicPointer" />
    /// </remarks>
    /// <returns>(void *) Returns the current value of a pointer.</returns>
    public static nint GetAtomicPointer(ref nint a) {
        // Validate the input parameters
        return a != nint.Zero ?
            // Attempt to get the value atomically
            SDL_GetAtomicPointer(ref a) : nint.Zero;
    }

    /// <summary>Get the value of an atomic variable.</summary>
    /// <param name="a">a pointer to an SDL_AtomicU32 variable.</param>
    /// <remarks>
    /// Note: If you don't know what this function is for, you shouldn't use it!
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAtomicU32" />
    /// </remarks>
    /// <returns>Returns the current value of an atomic variable.</returns>
    public static uint GetAtomicU32(ref AtomicU32 a) {
        // Validate the input parameters
        return a.Value != 0 ?
            // Attempt to get the value atomically
            SDL_GetAtomicU32(ref a) : 0u;
    }

    /// <summary>Lock a spin lock by setting it to a non-zero value.</summary>
    /// <param name="lock">a pointer to a lock variable.</param>
    /// <remarks>
    /// Please note that spinlocks are dangerous if you don't know what you're
    /// doing. Please be careful using any sort of spinlock!
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="TryLockSpinlock" />
    /// <seealso cref="UnlockSpinlock" />
    /// </remarks>
    public static void LockSpinlock(nint @lock) {
        if (!SDL_TryLockSpinlock(@lock)) {
            SDL_LockSpinlock(@lock);
        }
    }

    /// <summary>Insert a memory acquire barrier (macro version).</summary>
    /// <remarks>
    /// Please see SDL_MemoryBarrierRelease for the
    /// details on what memory barriers are and when to use them.
    /// <para><strong>Thread Safety</strong>: Obviously this macro is safe to use from any thread at any time, but if you find yourself needing this, you are probably dealing with some very sensitive code; be careful!</para>
    /// <para><strong>Version</strong>: This macro is available since SDL 3.2.0.</para>
    /// <seealso cref="MemoryBarrierRelease" />
    /// </remarks>
    public static void MemoryBarrierAcquire() {
        SDL_MemoryBarrierAcquireFunction();
    }

    /// <summary>Insert a memory release barrier (macro version).</summary>
    /// <remarks>
    /// Memory barriers are designed to prevent reads and writes from being
    /// reordered by the compiler and being seen out of order on multi-core CPUs.
    /// <para><strong>Thread Safety</strong>: Obviously this macro is safe to use from any thread at any time, but if you find yourself needing this, you are probably dealing with some very sensitive code; be careful!</para>
    /// <para><strong>Version</strong>: This macro is available since SDL 3.2.0.</para>
    /// <seealso cref="MemoryBarrierAcquire" />
    /// </remarks>
    public static void MemoryBarrierRelease() {
        SDL_MemoryBarrierReleaseFunction();
    }

    /// <summary>Set an atomic variable to a value.</summary>
    /// <param name="a">a pointer to an SDL_AtomicInt variable to be modified.</param>
    /// <param name="v">the desired value.</param>
    /// <remarks>
    /// This function also acts as a full memory barrier.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAtomicInt" />
    /// </remarks>
    /// <returns>Returns the previous value of the atomic variable.</returns>
    public static int SetAtomicInt(ref AtomicInt a, int v) {
        // Validate the input parameters
        return a.Value != v ?
            // Attempt to set the value atomically
            SDL_SetAtomicInt(ref a, v) : a.Value;
    }

    /// <summary>Set a pointer to a value atomically.</summary>
    /// <param name="a">a pointer to a pointer.</param>
    /// <param name="v">the desired pointer value.</param>
    /// <remarks>
    /// Note: If you don't know what this function is for, you shouldn't use it!
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CompareAndSwapAtomicPointer" />
    /// <seealso cref="GetAtomicPointer" />
    /// </remarks>
    /// <returns>(void *) Returns the previous value of the pointer.</returns>
    public static nint SetAtomicPointer(ref nint a, nint v) {
        // Validate the input parameters
        return a != v ?
            // Attempt to set the value atomically
            SDL_SetAtomicPointer(ref a, v) : a;
    }

    /// <summary>Set an atomic variable to a value.</summary>
    /// <param name="a">a pointer to an SDL_AtomicU32 variable to be modified.</param>
    /// <param name="v">the desired value.</param>
    /// <remarks>
    /// This function also acts as a full memory barrier.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAtomicU32" />
    /// </remarks>
    /// <returns>Returns the previous value of the atomic variable.</returns>
    public static uint SetAtomicU32(ref AtomicU32 a, uint v) {
        // Validate the input parameters
        return a.Value != v ?
            // Attempt to set the value atomically
            SDL_SetAtomicU32(ref a, v) : a.Value;
    }

    /// <summary>
    /// Try to lock a spin lock by setting it to a non-zero value
    /// </summary>
    /// <param name="lock">a pointer to a lock variable</param>
    /// <remarks>
    /// Please note that spinlocks are dangerous if you don't know what you're doing. Please be careful using any sort of spinlock!
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LockSpinlock" />
    /// <seealso cref="UnlockSpinlock" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the lock succeeded, <see langword="false" /> if the lock is already held.</returns>
    /// <exception cref="ArgumentException">If <paramref name="lock" /> is zero/null.</exception>
    public static SdlBool TryLockSpinlock(nint @lock) {
        return @lock == nint.Zero
            ? throw new ArgumentException("Lock pointer cannot be null.", nameof(@lock))
            : SDL_TryLockSpinlock(@lock);
    }

    /// <summary>Unlock a spin lock by setting it to 0.</summary>
    /// <param name="lock">a pointer to a lock variable.</param>
    /// <remarks>
    /// Always returns immediately.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LockSpinlock" />
    /// <seealso cref="TryLockSpinlock" />
    /// </remarks>
    public static void UnlockSpinlock(nint @lock) {
        if (@lock == nint.Zero) {
            throw new ArgumentException("Lock pointer cannot be null.", nameof(@lock));
        }
        SDL_UnlockSpinlock(@lock);
    }

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_AddAtomicInt(ref AtomicInt a, int v);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CompareAndSwapAtomicInt(ref AtomicInt a, int oldVal, int newVal);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CompareAndSwapAtomicPointer(ref nint a, nint oldVal, nint newVal);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CompareAndSwapAtomicU32(ref AtomicU32 a, uint oldVal, uint newVal);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetAtomicInt(ref AtomicInt a);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetAtomicPointer(ref nint a);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetAtomicU32(ref AtomicU32 a);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LockSpinlock(nint @lock);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_MemoryBarrierAcquireFunction();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_MemoryBarrierReleaseFunction();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetAtomicInt(ref AtomicInt a, int v);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_SetAtomicPointer(ref nint a, nint v);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_SetAtomicU32(ref AtomicU32 a, uint v);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_TryLockSpinlock(nint @lock);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnlockSpinlock(nint @lock);
}