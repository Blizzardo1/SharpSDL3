using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSDL3;

public static partial class Sdl {
    // /usr/local/include/SDL3/SDL_system.h

    /// <summary>Converts a calendar time to an SDL_Time in nanoseconds since the epoch.</summary>
    /// <param name="dt">the source SDL_DateTime.</param>
    /// <param name="ticks">the resulting SDL_Time.</param>
    /// <remarks>
    /// This function ignores the day_of_week member of the
    /// SDL_DateTime struct, so it may remain unset.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool DateTimeToTime(ref Structs.DateTime dt, nint ticks) {
        return SDL_DateTimeToTime(ref dt, ticks);
    }

    /// <summary>Gets the current value of the system realtime clock in nanoseconds since Jan 1, 1970 in Universal Coordinated Time (UTC).</summary>
    /// <param name="ticks">the SDL_Time to hold the returned tick count.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool GetCurrentTime(nint ticks) {
        return SDL_GetCurrentTime(ticks);
    }

    /// <summary>Gets the current preferred date and time format for the system locale.</summary>
    /// <param name="dateFormat">a pointer to the SDL_DateFormat to hold the returned date format, may be discarded.</param>
    /// <param name="timeFormat">a pointer to the SDL_TimeFormat to hold the returned time format, may be discarded.</param>
    /// <remarks>
    /// This might be a &quot;slow&quot; call that has to query the operating system. It's
    /// best to ask for this once and save the results. However, the preferred
    /// formats can change, usually because the user has changed a system
    /// preference outside of your program.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool GetDateTimeLocalePreferences(out DateFormat dateFormat, out TimeFormat timeFormat) {
        return SDL_GetDateTimeLocalePreferences(out dateFormat, out timeFormat);
    }

    /// <summary>Get the day of week for a calendar date.</summary>
    /// <param name="year">the year component of the date.</param>
    /// <param name="month">the month component of the date.</param>
    /// <param name="day">the day component of the date.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns a value between 0 and 6 (0 being Sunday) if the date is valid or -1 on failure; call <see cref="GetError()" /> for more information.</returns>
    public static int GetDayOfWeek(int year, int month, int day) {
        if (month is < 1 or > 12) {
            throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12.");
        }
        if (day < 1 || day > GetDaysInMonth(year, month)) {
            throw new ArgumentOutOfRangeException(nameof(day), $"Day must be valid for the month of {GetMonth(month)}");
        }
        return SDL_GetDayOfWeek(year, month, day);
    }

    /// <summary>Get the day of year for a calendar date.</summary>
    /// <param name="year">the year component of the date.</param>
    /// <param name="month">the month component of the date.</param>
    /// <param name="day">the day component of the date.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the day of year [0-365] if the date is valid or -1 on failure; call <see cref="GetError()" /> for more information.</returns>
    public static int GetDayOfYear(int year, int month, int day) {
        if (month is < 1 or > 12) {
            throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12.");
        }
        if (day < 1 || day > GetDaysInMonth(year, month)) {
            throw new ArgumentOutOfRangeException(nameof(day), "Day must be valid for the specified month.");
        }
        return SDL_GetDayOfYear(year, month, day);
    }

    /// <summary>Get the number of days in a month for a given year.</summary>
    /// <param name="year">the year.</param>
    /// <param name="month">the month [1-12].</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the number of days in the requested month or -1 on failure;call <see cref="GetError()" /> for more information.</returns>
    public static int GetDaysInMonth(int year, int month) {
        return month is < 1 or > 12 ? throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12.") : SDL_GetDaysInMonth(year, month);
    }

    public static string GetMonth(int month, bool shortCode = false) {
        if (month is < 1 or > 12) {
            throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12.");
        }

        string[] months = [
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"
        ];
        string selectedMonth = months[month - 1];
        return shortCode ? selectedMonth[..3] : selectedMonth;
    }

    /// <summary>Get the application sandbox environment, if any.</summary>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the application sandbox environment or SDL_SANDBOX_NONE if the application is not running in a sandbox environment.</returns>
    public static Sandbox GetSandbox() {
        return SDL_GetSandbox();
    }

    /// <summary>Query if the current device is a tablet.</summary>
    /// <remarks>
    /// If SDL can't determine this, it will return false.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the device is a tablet, <see langword="false" /> otherwise.</returns>
    public static bool IsTablet() {
        return SDL_IsTablet();
    }

    /// <summary>Query if the current device is a TV.</summary>
    /// <remarks>
    /// If SDL can't determine this, it will return false.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the device is a TV, <see langword="false" /> otherwise.</returns>
    public static bool IsTv() {
        return SDL_IsTV();
    }

    /// <summary>Let iOS apps with external event handling report onApplicationDidEnterBackground.</summary>
    /// <remarks>
    /// This functions allows iOS apps that have their own event handling to hook
    /// into SDL to generate SDL events. This maps directly to an iOS-specific
    /// event, but since it doesn't do anything iOS-specific internally, it is
    /// available on all platforms, in case it might be useful for some specific
    /// paradigm. Most apps do not need to use this directly; SDL's internal event
    /// code will handle all this for windows created by
    /// SDL_CreateWindow!
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    public static void OnApplicationDidEnterBackground() {
        SDL_OnApplicationDidEnterBackground();
    }

    /// <summary>Let iOS apps with external event handling report onApplicationDidBecomeActive.</summary>
    /// <remarks>
    /// This functions allows iOS apps that have their own event handling to hook
    /// into SDL to generate SDL events. This maps directly to an iOS-specific
    /// event, but since it doesn't do anything iOS-specific internally, it is
    /// available on all platforms, in case it might be useful for some specific
    /// paradigm. Most apps do not need to use this directly; SDL's internal event
    /// code will handle all this for windows created by
    /// SDL_CreateWindow!
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    public static void OnApplicationDidEnterForeground() {
        SDL_OnApplicationDidEnterForeground();
    }

    /// <summary>Let iOS apps with external event handling report onApplicationDidReceiveMemoryWarning.</summary>
    /// <remarks>
    /// This functions allows iOS apps that have their own event handling to hook
    /// into SDL to generate SDL events. This maps directly to an iOS-specific
    /// event, but since it doesn't do anything iOS-specific internally, it is
    /// available on all platforms, in case it might be useful for some specific
    /// paradigm. Most apps do not need to use this directly; SDL's internal event
    /// code will handle all this for windows created by
    /// SDL_CreateWindow!
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    public static void OnApplicationDidReceiveMemoryWarning() {
        SDL_OnApplicationDidReceiveMemoryWarning();
    }

    /// <summary>Let iOS apps with external event handling report onApplicationWillResignActive.</summary>
    /// <remarks>
    /// This functions allows iOS apps that have their own event handling to hook
    /// into SDL to generate SDL events. This maps directly to an iOS-specific
    /// event, but since it doesn't do anything iOS-specific internally, it is
    /// available on all platforms, in case it might be useful for some specific
    /// paradigm. Most apps do not need to use this directly; SDL's internal event
    /// code will handle all this for windows created by
    /// SDL_CreateWindow!
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    public static void OnApplicationWillEnterBackground() {
        SDL_OnApplicationWillEnterBackground();
    }

    /// <summary>Let iOS apps with external event handling report onApplicationWillEnterForeground.</summary>
    /// <remarks>
    /// This functions allows iOS apps that have their own event handling to hook
    /// into SDL to generate SDL events. This maps directly to an iOS-specific
    /// event, but since it doesn't do anything iOS-specific internally, it is
    /// available on all platforms, in case it might be useful for some specific
    /// paradigm. Most apps do not need to use this directly; SDL's internal event
    /// code will handle all this for windows created by
    /// SDL_CreateWindow!
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    public static void OnApplicationWillEnterForeground() {
        SDL_OnApplicationWillEnterForeground();
    }

    /// <summary>Let iOS apps with external event handling report onApplicationWillTerminate.</summary>
    /// <remarks>
    /// This functions allows iOS apps that have their own event handling to hook
    /// into SDL to generate SDL events. This maps directly to an iOS-specific
    /// event, but since it doesn't do anything iOS-specific internally, it is
    /// available on all platforms, in case it might be useful for some specific
    /// paradigm. Most apps do not need to use this directly; SDL's internal event
    /// code will handle all this for windows created by
    /// SDL_CreateWindow!
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    public static void OnApplicationWillTerminate() {
        SDL_OnApplicationWillTerminate();
    }

    /// <summary>Converts a Windows FILETIME (100-nanosecond intervals since January 1, 1601) to an SDL time.</summary>
    /// <param name="dwLowDateTime">the low portion of the Windows FILETIME value.</param>
    /// <param name="dwHighDateTime">the high portion of the Windows FILETIME value.</param>
    /// <remarks>
    /// This function takes the two 32-bit values of the FILETIME structure as
    /// parameters.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the converted SDL time.</returns>
    public static long TimeFromWindows(uint dwLowDateTime, uint dwHighDateTime) {
        return SDL_TimeFromWindows(dwLowDateTime, dwHighDateTime);
    }

    /// <summary>Converts an SDL_Time in nanoseconds since the epoch to a calendar time in the SDL_DateTime format.</summary>
    /// <param name="ticks">the SDL_Time to be converted.</param>
    /// <param name="dt">the resulting SDL_DateTime.</param>
    /// <param name="localTime">the resulting SDL_DateTime will be expressed in local time if <see langword="true" />, otherwise it will be in Universal Coordinated Time (UTC).</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool TimeToDateTime(long ticks, out Structs.DateTime dt, bool localTime = true) {
        SdlBool localTimeBool = localTime;
        return SDL_TimeToDateTime(ticks, out dt, localTimeBool);
    }

    /// <summary>Converts an SDL time into a Windows FILETIME (100-nanosecond intervals since January 1, 1601).</summary>
    /// <param name="ticks">the time to convert.</param>
    /// <param name="dwLowDateTime">a pointer filled in with the low portion of the Windows FILETIME value.</param>
    /// <param name="dwHighDateTime">a pointer filled in with the high portion of the Windows FILETIME value.</param>
    /// <remarks>
    /// This function fills in the two 32-bit values of the FILETIME structure.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    public static void TimeToWindows(long ticks, out uint dwLowDateTime, out uint dwHighDateTime) {
        SDL_TimeToWindows(ticks, out dwLowDateTime, out dwHighDateTime);
    }

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_DateTimeToTime(ref Structs.DateTime dt, nint ticks);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetCurrentTime(nint ticks);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetDateTimeLocalePreferences(out DateFormat dateFormat,
        out TimeFormat timeFormat);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetDayOfWeek(int year, int month, int day);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetDayOfYear(int year, int month, int day);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetDaysInMonth(int year, int month);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Sandbox SDL_GetSandbox();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_IsTablet();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_IsTV();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_OnApplicationDidEnterBackground();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_OnApplicationDidEnterForeground();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_OnApplicationDidReceiveMemoryWarning();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_OnApplicationWillEnterBackground();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_OnApplicationWillEnterForeground();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_OnApplicationWillTerminate();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long SDL_TimeFromWindows(uint dwLowDateTime, uint dwHighDateTime);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_TimeToDateTime(long ticks, out Structs.DateTime dt, SdlBool localTime);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_TimeToWindows(long ticks, out uint dwLowDateTime, out uint dwHighDateTime);
}