using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSDL3;

public static unsafe partial class Sdl {
    // /usr/local/include/SDL3/SDL_atomic.h

    /// <summary>Add to an atomic variable.</summary>

    /// <param name="a">a pointer to an SDL_AtomicInt variable to be modified.</param>
    /// <param name="v">the desired value to add.</param>
    /// <remarks>
    /// This function also acts as a full memory barrier.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="AtomicDecRef"/>
    /// <seealso cref="AtomicIncRef"/>
    /// </remarks>
    /// <returns>Returns the previous value of the atomic variable.</returns>

    public static int AddAtomicInt(ref AtomicInt a, int v) {
        // Validate the input parameters
        if (a.Value != 0) {
            // Attempt to add the value atomically
            return SDL_AddAtomicInt(ref a, v);
        }
        return 0;
    }

    /// <summary>Set an atomic variable to a new value if it is currently an old value.</summary>

    /// <param name="a">a pointer to an SDL_AtomicInt variable to be modified.</param>
    /// <param name="oldval">the old value.</param>
    /// <param name="newval">the new value.</param>
    /// <remarks>
    /// Note: If you don't know what this function is for, you shouldn't use
    /// it!
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAtomicInt"/>
    /// <seealso cref="SetAtomicInt"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the atomic variable was set, <see langword="false" /> otherwise.</returns>

    public static bool CompareAndSwapAtomicInt(ref AtomicInt a, int oldval, int newval) {
        // Validate the input parameters
        if (a.Value == oldval) {
            // Attempt to swap the value atomically
            return SDL_CompareAndSwapAtomicInt(ref a, oldval, newval).Equals(true);
        }
        return false;
    }

    /// <summary>Set a pointer to a new value if it is currently an old value.</summary>

    /// <param name="a">a pointer to a pointer.</param>
    /// <param name="oldval">the old pointer value.</param>
    /// <param name="newval">the new pointer value.</param>
    /// <remarks>
    /// Note: If you don't know what this function is for, you shouldn't use
    /// it!
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CompareAndSwapAtomicInt"/>
    /// <seealso cref="GetAtomicPointer"/>
    /// <seealso cref="SetAtomicPointer"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the pointer was set, <see langword="false" /> otherwise.</returns>

    public static bool CompareAndSwapAtomicPointer(ref nint a, nint oldval, nint newval) {
        // Validate the input parameters
        if (a == oldval) {
            // Attempt to swap the value atomically
            return SDL_CompareAndSwapAtomicPointer(ref a, oldval, newval).Equals(true);
        }
        return false;
    }

    /// <summary>Set an atomic variable to a new value if it is currently an old value.</summary>

    /// <param name="a">a pointer to an SDL_AtomicU32 variable to be modified.</param>
    /// <param name="oldval">the old value.</param>
    /// <param name="newval">the new value.</param>
    /// <remarks>
    /// Note: If you don't know what this function is for, you shouldn't use
    /// it!
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAtomicU32"/>
    /// <seealso cref="SetAtomicU32"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the atomic variable was set, <see langword="false" /> otherwise.</returns>

    public static bool CompareAndSwapAtomicU32(ref AtomicU32 a, uint oldval, uint newval) {
        // Validate the input parameters
        if (a.Value == oldval) {
            // Attempt to swap the value atomically
            return SDL_CompareAndSwapAtomicU32(ref a, oldval, newval).Equals(true);
        }
        return false;
    }

    /// <summary>Get the value of an atomic variable.</summary>

    /// <param name="a">a pointer to an SDL_AtomicInt variable.</param>
    /// <remarks>
    /// Note: If you don't know what this function is for, you shouldn't use
    /// it!
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAtomicInt"/>
    /// </remarks>
    /// <returns>Returns the current value of an atomic variable.</returns>

    public static int GetAtomicInt(ref AtomicInt a) {
        // Validate the input parameters
        if (a.Value != 0) {
            // Attempt to get the value atomically
            return SDL_GetAtomicInt(ref a);
        }
        return 0;
    }

    /// <summary>Get the value of a pointer atomically.</summary>

    /// <param name="a">a pointer to a pointer.</param>
    /// <remarks>
    /// Note: If you don't know what this function is for, you shouldn't use
    /// it!
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CompareAndSwapAtomicPointer"/>
    /// <seealso cref="SetAtomicPointer"/>
    /// </remarks>
    /// <returns>(void *) Returns the current value of a pointer.</returns>

    public static nint GetAtomicPointer(ref nint a) {
        // Validate the input parameters
        if (a != nint.Zero) {
            // Attempt to get the value atomically
            return SDL_GetAtomicPointer(ref a);
        }
        return nint.Zero;
    }

    /// <summary>Get the value of an atomic variable.</summary>

    /// <param name="a">a pointer to an SDL_AtomicU32 variable.</param>
    /// <remarks>
    /// Note: If you don't know what this function is for, you shouldn't use
    /// it!
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAtomicU32"/>
    /// </remarks>
    /// <returns>Returns the current value of an atomic variable.</returns>

    public static uint GetAtomicU32(ref AtomicU32 a) {
        // Validate the input parameters
        if (a.Value != 0) {
            // Attempt to get the value atomically
            return SDL_GetAtomicU32(ref a);
        }
        return 0;
    }

    /// <summary>Lock a spin lock by setting it to a non-zero value.</summary>

    /// <param name="lock">a pointer to a lock variable.</param>
    /// <remarks>
    /// Please note that spinlocks are dangerous if you don't know what you're
    /// doing. Please be careful using any sort of spinlock!
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="TryLockSpinlock"/>
    /// <seealso cref="UnlockSpinlock"/>
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
    /// <para><strong>Thread Safety:</strong> Obviously this macro is safe to use from any thread at any time, but if youfind yourself needing this, you are probably dealing with some verysensitive code; be careful!</para>
    /// <para><strong>Version:</strong> This macro is available since SDL 3.2.0.</para>
    /// <seealso cref="MemoryBarrierRelease"/>
    /// <seealso cref="MemoryBarrierAcquireFunction"/>
    /// </remarks>

    public static void MemoryBarrierAcquire() {
        SDL_MemoryBarrierAcquireFunction();
    }

    /// <summary>Insert a memory release barrier (macro version).</summary>
    /// <remarks>
    /// Memory barriers are designed to prevent reads and writes from being
    /// reordered by the compiler and being seen out of order on multi-core CPUs.
    /// <para><strong>Thread Safety:</strong> Obviously this macro is safe to use from any thread at any time, but if youfind yourself needing this, you are probably dealing with some verysensitive code; be careful!</para>
    /// <para><strong>Version:</strong> This macro is available since SDL 3.2.0.</para>
    /// <seealso cref="MemoryBarrierAcquire"/>
    /// <seealso cref="MemoryBarrierReleaseFunction"/>
    /// </remarks>

    public static void MemoryBarrierRelease() {
        SDL_MemoryBarrierReleaseFunction();
    }

    /// <summary>Set an atomic variable to a value.</summary>

    /// <param name="a">a pointer to an SDL_AtomicInt variable to be modified.</param>
    /// <param name="v">the desired value.</param>
    /// <remarks>
    /// This function also acts as a full memory barrier.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAtomicInt"/>
    /// </remarks>
    /// <returns>Returns the previous value of the atomic variable.</returns>

    public static int SetAtomicInt(ref AtomicInt a, int v) {
        // Validate the input parameters
        if (a.Value != v) {
            // Attempt to set the value atomically
            return SDL_SetAtomicInt(ref a, v);
        }
        return a.Value;
    }

    /// <summary>Set a pointer to a value atomically.</summary>

    /// <param name="a">a pointer to a pointer.</param>
    /// <param name="v">the desired pointer value.</param>
    /// <remarks>
    /// Note: If you don't know what this function is for, you shouldn't use
    /// it!
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CompareAndSwapAtomicPointer"/>
    /// <seealso cref="GetAtomicPointer"/>
    /// </remarks>
    /// <returns>(void *) Returns the previous value of the pointer.</returns>

    public static nint SetAtomicPointer(ref nint a, nint v) {
        // Validate the input parameters
        if (a != v) {
            // Attempt to set the value atomically
            return SDL_SetAtomicPointer(ref a, v);
        }
        return a;
    }

    /// <summary>Set an atomic variable to a value.</summary>

    /// <param name="a">a pointer to an SDL_AtomicU32 variable to be modified.</param>
    /// <param name="v">the desired value.</param>
    /// <remarks>
    /// This function also acts as a full memory barrier.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAtomicU32"/>
    /// </remarks>
    /// <returns>Returns the previous value of the atomic variable.</returns>

    public static uint SetAtomicU32(ref AtomicU32 a, uint v) {
        // Validate the input parameters
        if (a.Value != v) {
            // Attempt to set the value atomically
            return SDL_SetAtomicU32(ref a, v);
        }
        return a.Value;
    }

    /// <summary>Unlock a spin lock by setting it to 0.</summary>

    /// <param name="lock">a pointer to a lock variable.</param>
    /// <remarks>
    /// Always returns immediately.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LockSpinlock"/>
    /// <seealso cref="TryLockSpinlock"/>
    /// </remarks>

    public static void UnlockSpinlock(nint @lock) {
        if (@lock == nint.Zero) {
            throw new ArgumentException("Lock pointer cannot be null.", nameof(@lock));
        }
        SDL_UnlockSpinlock(@lock);
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_AddAtomicInt(ref AtomicInt a, int v);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CompareAndSwapAtomicInt(ref AtomicInt a, int oldval, int newval);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CompareAndSwapAtomicPointer(ref nint a, nint oldval, nint newval);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CompareAndSwapAtomicU32(ref AtomicU32 a, uint oldval, uint newval);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetAtomicInt(ref AtomicInt a);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetAtomicPointer(ref nint a);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetAtomicU32(ref AtomicU32 a);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LockSpinlock(nint @lock);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_MemoryBarrierAcquireFunction();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_MemoryBarrierReleaseFunction();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetAtomicInt(ref AtomicInt a, int v);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_SetAtomicPointer(ref nint a, nint v);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_SetAtomicU32(ref AtomicU32 a, uint v);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_TryLockSpinlock(nint @lock);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnlockSpinlock(nint @lock);
}