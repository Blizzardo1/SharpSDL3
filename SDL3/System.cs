using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSDL3; 
public static partial class Sdl {
    // /usr/local/include/SDL3/SDL_system.h

    public static bool DateTimeToTime(ref SharpSDL3.Structs.DateTime dt, nint ticks) {
        return SDL_DateTimeToTime(ref dt, ticks);
    }

    public static bool GetCurrentTime(nint ticks) {
        return SDL_GetCurrentTime(ticks);
    }

    public static bool GetDateTimeLocalePreferences(out DateFormat dateFormat, out TimeFormat timeFormat) {
        return SDL_GetDateTimeLocalePreferences(out dateFormat, out timeFormat);
    }

    public static int GetDayOfWeek(int year, int month, int day) {
        if (month < 1 || month > 12) {
            throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12.");
        }
        if (day < 1 || day > GetDaysInMonth(year, month)) {

            throw new ArgumentOutOfRangeException(nameof(day), $"Day must be valid for the month of {GetMonth(month)}");
        }
        return SDL_GetDayOfWeek(year, month, day);
    }

    public static int GetDayOfYear(int year, int month, int day) {
        if (month < 1 || month > 12) {
            throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12.");
        }
        if (day < 1 || day > GetDaysInMonth(year, month)) {
            throw new ArgumentOutOfRangeException(nameof(day), "Day must be valid for the specified month.");
        }
        return SDL_GetDayOfYear(year, month, day);
    }

    public static int GetDaysInMonth(int year, int month) {
        if (month < 1 || month > 12) {
            throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12.");
        }
        return SDL_GetDaysInMonth(year, month);
    }

    public static string GetMonth(int month, bool shortCode = false) {
        if (month < 1 || month > 12) {
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

    public static Sandbox GetSandbox() {
        return SDL_GetSandbox();
    }

    public static bool IsTablet() {
        return SDL_IsTablet();
    }

    public static bool IsTV() {
        return SDL_IsTV();
    }

    public static void OnApplicationDidEnterBackground() {
        SDL_OnApplicationDidEnterBackground();
    }

    public static void OnApplicationDidEnterForeground() {
        SDL_OnApplicationDidEnterForeground();
    }

    public static void OnApplicationDidReceiveMemoryWarning() {
        SDL_OnApplicationDidReceiveMemoryWarning();
    }

    public static void OnApplicationWillEnterBackground() {
        SDL_OnApplicationWillEnterBackground();
    }

    public static void OnApplicationWillEnterForeground() {
        SDL_OnApplicationWillEnterForeground();
    }

    public static void OnApplicationWillTerminate() {
        SDL_OnApplicationWillTerminate();
    }

    public static long TimeFromWindows(uint dwLowDateTime, uint dwHighDateTime) {
        return SDL_TimeFromWindows(dwLowDateTime, dwHighDateTime);
    }

    public static bool TimeToDateTime(long ticks, out SharpSDL3.Structs.DateTime dt, bool localTime = true) {
        SdlBool localTimeBool = localTime;
        return SDL_TimeToDateTime(ticks, out dt, localTimeBool);
    }

    public static void TimeToWindows(long ticks, out uint dwLowDateTime, out uint dwHighDateTime) {
        SDL_TimeToWindows(ticks, out dwLowDateTime, out dwHighDateTime);
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_DateTimeToTime(ref SharpSDL3.Structs.DateTime dt, nint ticks);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetCurrentTime(nint ticks);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetDateTimeLocalePreferences(out DateFormat dateFormat,
        out TimeFormat timeFormat);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetDayOfWeek(int year, int month, int day);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetDayOfYear(int year, int month, int day);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetDaysInMonth(int year, int month);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Sandbox SDL_GetSandbox();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_IsTablet();
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_IsTV();
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_OnApplicationDidEnterBackground();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_OnApplicationDidEnterForeground();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_OnApplicationDidReceiveMemoryWarning();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_OnApplicationWillEnterBackground();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_OnApplicationWillEnterForeground();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_OnApplicationWillTerminate();
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long SDL_TimeFromWindows(uint dwLowDateTime, uint dwHighDateTime);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_TimeToDateTime(long ticks, out SharpSDL3.Structs.DateTime dt, SdlBool localTime);
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_TimeToWindows(long ticks, out uint dwLowDateTime, out uint dwHighDateTime);
}
