<<<<<<< HEAD
using SharpSDL3.Structs;
=======
﻿using SharpSDL3.Structs;
>>>>>>> main
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static SharpSDL3.Delegates;
<<<<<<< HEAD

namespace SharpSDL3;

public static partial class Sdl {
    // /usr/local/include/SDL3/SDL_timer.h

    /// <summary>Call a callback function at a future time.</summary>

    /// <param name="interval">the timer delay, in milliseconds, passed to callback.</param>
    /// <param name="callback">the SDL_TimerCallback function to call when the specified interval elapses.</param>
    /// <param name="userdata">a pointer that is passed to callback.</param>
    /// <remarks>
    /// The callback function is passed the current timer interval and the user
    /// supplied parameter from the SDL_AddTimer() call and should
    /// return the next timer interval. If the value returned from the callback is
    /// 0, the timer is canceled and will be removed.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="AddTimerNS"/>
    /// <seealso cref="RemoveTimer"/>
    /// </remarks>
    /// <returns>Returns a timer ID or 0 on failure; call <see cref="GetError()"/> for more information.</returns>

=======
using static SharpSDL3.Sdl;

namespace SharpSDL3; 
public static partial class Sdl {
    // /usr/local/include/SDL3/SDL_timer.h

>>>>>>> main
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

<<<<<<< HEAD
    /// <summary>Call a callback function at a future time.</summary>

    /// <param name="interval">the timer delay, in nanoseconds, passed to callback.</param>
    /// <param name="callback">the SDL_TimerCallback function to call when the specified interval elapses.</param>
    /// <param name="userdata">a pointer that is passed to callback.</param>
    /// <remarks>
    /// The callback function is passed the current timer interval and the user
    /// supplied parameter from the SDL_AddTimerNS() call and
    /// should return the next timer interval. If the value returned from the
    /// callback is 0, the timer is canceled and will be removed.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="AddTimer"/>
    /// <seealso cref="RemoveTimer"/>
    /// </remarks>
    /// <returns>Returns a timer ID or 0 on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
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

<<<<<<< HEAD
    /// <summary>Wait a specified number of milliseconds before returning.</summary>

    /// <param name="ms">the number of milliseconds to delay.</param>
    /// <remarks>
    /// This function waits a specified number of milliseconds before returning. It
    /// waits at least the specified time, but possibly longer due to OS
    /// scheduling.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="DelayNS"/>
    /// <seealso cref="DelayPrecise"/>
    /// </remarks>

=======
>>>>>>> main
    public static void Delay(uint ms) {
        if (ms == 0) {
            throw new ArgumentException("Delay duration must be greater than zero.", nameof(ms));
        }

        ulong start = GetTicks();
        SDL_Delay(ms);
        ulong end = GetTicks();

        if (end - start < ms) {
<<<<<<< HEAD
            throw new InvalidOperationException("Delay did not delay for the expected duration.");
        }
    }

    /// <summary>Wait a specified number of nanoseconds before returning.</summary>

    /// <param name="ns">the number of nanoseconds to delay.</param>
    /// <remarks>
    /// This function waits a specified number of nanoseconds before returning. It
    /// waits at least the specified time, but possibly longer due to OS
    /// scheduling.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="Delay"/>
    /// <seealso cref="DelayPrecise"/>
    /// </remarks>

=======
            throw new InvalidOperationException("SDL_Delay did not delay for the expected duration.");
        }
    }

>>>>>>> main
    public static void DelayNS(ulong ns) {
        if (ns == 0) {
            throw new ArgumentException("Delay duration must be greater than zero.", nameof(ns));
        }

        ulong start = GetTicksNS();
        SDL_DelayNS(ns);
        ulong end = GetTicksNS();

        if (end - start < ns) {
<<<<<<< HEAD
            throw new InvalidOperationException("DelayNS did not delay for the expected duration.");
        }
    }

    /// <summary>Wait a specified number of nanoseconds before returning.</summary>

    /// <param name="ns">the number of nanoseconds to delay.</param>
    /// <remarks>
    /// This function waits a specified number of nanoseconds before returning. It
    /// will attempt to wait as close to the requested time as possible, busy
    /// waiting if necessary, but could return later due to OS scheduling.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="Delay"/>
    /// <seealso cref="DelayNS"/>
    /// </remarks>

=======
            throw new InvalidOperationException("SDL_DelayNS did not delay for the expected duration.");
        }
    }

>>>>>>> main
    public static void DelayPrecise(ulong ns) {
        if (ns == 0) {
            throw new ArgumentException("Delay duration must be greater than zero.", nameof(ns));
        }

        ulong start = GetTicksNS();
        SDL_DelayPrecise(ns);
        ulong end = GetTicksNS();

        if (end - start < ns) {
<<<<<<< HEAD
            throw new InvalidOperationException("DelayPrecise did not delay for the expected duration.");
        }
    }

    /// <summary>Get the current value of the high resolution counter.</summary>
    /// <remarks>
    /// This function is typically used for profiling.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetPerformanceFrequency"/>
    /// </remarks>
    /// <returns>Returns the current counter value.</returns>

=======
            throw new InvalidOperationException("SDL_DelayPrecise did not delay for the expected duration.");
        }
    }

>>>>>>> main
    public static ulong GetPerformanceCounter() {
        return SDL_GetPerformanceCounter();
    }

<<<<<<< HEAD
    /// <summary>Get the count per second of the high resolution counter.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetPerformanceCounter"/>
    /// </remarks>
    /// <returns>Returns a platform-specific count per second.</returns>

=======
>>>>>>> main
    public static ulong GetPerformanceFrequency() {
        return SDL_GetPerformanceFrequency();
    }

<<<<<<< HEAD
    /// <summary>Get the number of milliseconds that have elapsed since the SDL library initialization.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns an unsigned 64‑bit integer that represents thenumber of milliseconds that have elapsed since the SDL library wasinitialized (typically via a call to SDL_Init).</returns>

=======
>>>>>>> main
    public static ulong GetTicks() {
        return SDL_GetTicks();
    }

<<<<<<< HEAD
    /// <summary>Get the number of nanoseconds since SDL library initialization.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns an unsigned 64-bit value representing the numberof nanoseconds since the SDL library initialized.</returns>

=======
>>>>>>> main
    public static ulong GetTicksNS() {
        return SDL_GetTicksNS();
    }

<<<<<<< HEAD
    /// <summary>Remove a timer created with SDL_AddTimer().</summary>

    /// <param name="id">the ID of the timer to remove.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="AddTimer"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
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
<<<<<<< HEAD

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RemoveTimer(uint id);
}
=======
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RemoveTimer(uint id);
}
>>>>>>> main
