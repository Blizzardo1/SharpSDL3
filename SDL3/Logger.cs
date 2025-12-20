using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static SharpSDL3.Delegates;

namespace SharpSDL3;

public static partial class Sdl {
    /// <summary>Get the default log output function.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetLogOutputFunction" />
    /// <seealso cref="GetLogOutputFunction" />
    /// </remarks>
    /// <returns>Returns the default log output callback.</returns>
    public static SdlLogOutputFunction GetDefaultLogOutputFunction() {
        SdlLogOutputFunction? callback = SDL_GetDefaultLogOutputFunction();
        return callback ?? throw new InvalidOperationException("Failed to retrieve default log output function.");
    }

    /// <summary>Get the current log output function.</summary>
    /// <param name="callback">an SDL_LogOutputFunction filled in with the current log callback.</param>
    /// <param name="userdata">a pointer filled in with the pointer that is passed to callback.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetDefaultLogOutputFunction" />
    /// <seealso cref="SetLogOutputFunction" />
    /// </remarks>
    public static void GetLogOutputFunction(out SdlLogOutputFunction callback, out nint userdata) {
        SDL_GetLogOutputFunction(out callback, out userdata);
        if (callback == null) {
            throw new InvalidOperationException("Failed to retrieve log output function.");
        }
    }

    /// <summary>Get the priority of a particular log category.</summary>
    /// <param name="category">the category to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetLogPriority" />
    /// </remarks>
    /// <returns>Returns theSDL_LogPriority for the requested category.</returns>
    public static LogPriority GetLogPriority(LogCategory category) {
        return category == 0 ? throw new ArgumentOutOfRangeException(nameof(category), "Invalid log category.") : SDL_GetLogPriority(category);
    }

    /// <summary>
    /// Log a message with <see cref="LogCategory.Application" /> and <see cref="LogPriority.Info" />.
    /// </summary>
    /// <param name="fmt">An interpolated formatted string.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function on any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <seealso cref="LogCritical" />
    /// <seealso cref="LogDebug" />
    /// <seealso cref="LogError" />
    /// <seealso cref="LogInfo" />
    /// <seealso cref="LogMessage" />
    /// <seealso cref="LogMessageV" />
    /// <seealso cref="LogTrace" />
    /// <seealso cref="LogVerbose" />
    /// <seealso cref="LogWarn" />
    /// <exception cref="ArgumentException"></exception>
    public static void Log(string fmt) {
        if (string.IsNullOrWhiteSpace(fmt)) {
            throw new ArgumentException("Log message cannot be null or empty.", nameof(fmt));
        }

        SDL_Log(fmt);
    }

    /// <summary>Log a message with SDL_LOG_PRIORITY_CRITICAL.</summary>
    /// <param name="category">the category of the message.</param>
    /// <param name="fmt">An interpolated formatted string.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="Log" />
    /// <seealso cref="LogDebug" />
    /// <seealso cref="LogError" />
    /// <seealso cref="LogInfo" />
    /// <seealso cref="LogMessage" />
    /// <seealso cref="LogMessageV" />
    /// <seealso cref="LogTrace" />
    /// <seealso cref="LogVerbose" />
    /// <seealso cref="LogWarn" />
    /// </remarks>
    public static void LogCritical(LogCategory category, string fmt) {
        if (string.IsNullOrWhiteSpace(fmt)) {
            throw new ArgumentException("Log message cannot be null or empty.", nameof(fmt));
        }
        SDL_LogCritical(category, fmt);
    }

    /// <summary>Log a message with SDL_LOG_PRIORITY_DEBUG.</summary>
    /// <param name="category">the category of the message.</param>
    /// <param name="fmt">An interpolated formatted string.</param>
    /// <param name="...">additional parameters matching % tokens in the fmt string, if any.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="Log" />
    /// <seealso cref="LogCritical" />
    /// <seealso cref="LogError" />
    /// <seealso cref="LogInfo" />
    /// <seealso cref="LogMessage" />
    /// <seealso cref="LogMessageV" />
    /// <seealso cref="LogTrace" />
    /// <seealso cref="LogVerbose" />
    /// <seealso cref="LogWarn" />
    /// </remarks>
    public static void LogDebug(LogCategory category, string fmt) {
        if (string.IsNullOrWhiteSpace(fmt)) {
            throw new ArgumentException("Log message cannot be null or empty.", nameof(fmt));
        }
        SDL_LogDebug(category, fmt);
    }

    /// <summary>Log a message with SDL_LOG_PRIORITY_ERROR.</summary>
    /// <param name="category">the category of the message.</param>
    /// <param name="fmt">An interpolated formatted string.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="Log" />
    /// <seealso cref="LogCritical" />
    /// <seealso cref="LogDebug" />
    /// <seealso cref="LogInfo" />
    /// <seealso cref="LogMessage" />
    /// <seealso cref="LogMessageV" />
    /// <seealso cref="LogTrace" />
    /// <seealso cref="LogVerbose" />
    /// <seealso cref="LogWarn" />
    /// </remarks>
    public static void LogError(LogCategory category, string fmt) {
        if (string.IsNullOrWhiteSpace(fmt)) {
            throw new ArgumentException("Log message cannot be null or empty.", nameof(fmt));
        }
        SDL_LogError(category, fmt);
    }

    /// <summary>Log a message with SDL_LOG_PRIORITY_INFO.</summary>
    /// <param name="category">the category of the message.</param>
    /// <param name="fmt">An interpolated formatted string.</param>
    /// <param name="...">additional parameters matching % tokens in the fmt string, if any.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="Log" />
    /// <seealso cref="LogCritical" />
    /// <seealso cref="LogDebug" />
    /// <seealso cref="LogError" />
    /// <seealso cref="LogMessage" />
    /// <seealso cref="LogMessageV" />
    /// <seealso cref="LogTrace" />
    /// <seealso cref="LogVerbose" />
    /// <seealso cref="LogWarn" />
    /// </remarks>
    public static void LogInfo(LogCategory category, string fmt) {
        if (string.IsNullOrWhiteSpace(fmt)) {
            throw new ArgumentException("Log message cannot be null or empty.", nameof(fmt));
        }
        SDL_LogInfo(category, fmt);
    }

    /// <summary>Log a message with the specified category and priority.</summary>
    /// <param name="category">the category of the message.</param>
    /// <param name="priority">the priority of the message.</param>
    /// <param name="fmt">An interpolated formatted string.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="Log" />
    /// <seealso cref="LogCritical" />
    /// <seealso cref="LogDebug" />
    /// <seealso cref="LogError" />
    /// <seealso cref="LogInfo" />
    /// <seealso cref="LogMessageV" />
    /// <seealso cref="LogTrace" />
    /// <seealso cref="LogVerbose" />
    /// <seealso cref="LogWarn" />
    /// </remarks>
    public static void LogMessage(LogCategory category, LogPriority priority, string fmt) {
        if (string.IsNullOrWhiteSpace(fmt)) {
            throw new ArgumentException("Log message cannot be null or empty.", nameof(fmt));
        }
        SDL_LogMessage(category, priority, fmt);
    }

    /// <summary>Log a message with SDL_LOG_PRIORITY_TRACE.</summary>
    /// <param name="category">the category of the message.</param>
    /// <param name="fmt">An interpolated formatted string.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="Log" />
    /// <seealso cref="LogCritical" />
    /// <seealso cref="LogDebug" />
    /// <seealso cref="LogError" />
    /// <seealso cref="LogInfo" />
    /// <seealso cref="LogMessage" />
    /// <seealso cref="LogMessageV" />
    /// <seealso cref="LogTrace" />
    /// <seealso cref="LogVerbose" />
    /// <seealso cref="LogWarn" />
    /// </remarks>
    public static void LogTrace(LogCategory category, string fmt) {
        if (string.IsNullOrWhiteSpace(fmt)) {
            throw new ArgumentException("Log message cannot be null or empty.", nameof(fmt));
        }
        SDL_LogTrace(category, fmt);
    }

    /// <summary>Log a message with SDL_LOG_PRIORITY_VERBOSE.</summary>
    /// <param name="category">the category of the message.</param>
    /// <param name="fmt">An interpolated formatted string.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="Log" />
    /// <seealso cref="LogCritical" />
    /// <seealso cref="LogDebug" />
    /// <seealso cref="LogError" />
    /// <seealso cref="LogInfo" />
    /// <seealso cref="LogMessage" />
    /// <seealso cref="LogMessageV" />
    /// <seealso cref="LogWarn" />
    /// </remarks>
    public static void LogVerbose(LogCategory category, string fmt) {
        if (string.IsNullOrWhiteSpace(fmt)) {
            throw new ArgumentException("Log message cannot be null or empty.", nameof(fmt));
        }
        SDL_LogVerbose(category, fmt);
    }

    /// <summary>Log a message with SDL_LOG_PRIORITY_WARN.</summary>
    /// <param name="category">the category of the message.</param>
    /// <param name="fmt">An interpolated formatted string.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="Log" />
    /// <seealso cref="LogCritical" />
    /// <seealso cref="LogDebug" />
    /// <seealso cref="LogError" />
    /// <seealso cref="LogInfo" />
    /// <seealso cref="LogMessage" />
    /// <seealso cref="LogMessageV" />
    /// <seealso cref="LogTrace" />
    /// <seealso cref="LogVerbose" />
    /// </remarks>
    public static void LogWarn(LogCategory category, string fmt) {
        if (string.IsNullOrWhiteSpace(fmt)) {
            throw new ArgumentException("Log message cannot be null or empty.", nameof(fmt));
        }
        SDL_LogWarn(category, fmt);
    }

    /// <summary>Reset all priorities to default.</summary>
    /// <remarks>
    /// This is called by <see cref="Quit" />.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetLogPriorities" />
    /// <seealso cref="SetLogPriority" />
    /// </remarks>
    public static void ResetLogPriorities() {
        SDL_ResetLogPriorities();
    }

    /// <summary>Replace the default log output function with one of your own.</summary>
    /// <param name="callback">an SDL_LogOutputFunction to call instead of the default.</param>
    /// <param name="userdata">a pointer that is passed to callback.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetDefaultLogOutputFunction" />
    /// <seealso cref="GetLogOutputFunction" />
    /// </remarks>
    public static void SetLogOutputFunction(SdlLogOutputFunction callback, nint userdata) {
        if (callback == null) {
            throw new ArgumentNullException(nameof(callback), "Log output function cannot be null.");
        }
        SDL_SetLogOutputFunction(callback, userdata);
    }

    /// <summary>
    /// Set the priority of all log categories.
    /// </summary>
    /// <param name="priority">the <see cref="LogPriority" /> to assign</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0</para>
    /// </remarks>
    /// <seealso cref="ResetLogPriorities" />
    /// <seealso cref="SetLogPriority" />
    /// <exception cref="ArgumentOutOfRangeException">If an invalid <see cref="LogPriority" /> value is assigned. This is typically thrown if a cast from <see cref="int" /> is passed.</exception>
    public static void SetLogPriorities(LogPriority priority) {
        if (priority is < LogPriority.Invalid or > LogPriority.Count) {
            throw new ArgumentOutOfRangeException(nameof(priority), "Invalid log priority.");
        }
        SDL_SetLogPriorities(priority);
    }

    /// <summary>Set the priority of a particular log category.</summary>
    /// <param name="category">the category to assign a priority to.</param>
    /// <param name="priority">the SDL_LogPriority to assign.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetLogPriority" />
    /// <seealso cref="ResetLogPriorities" />
    /// <seealso cref="SetLogPriorities" />
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">If an invalid <see cref="LogPriority" /> value is assigned. This is typically thrown if a cast from <see cref="int" /> is passed.</exception>
    public static void SetLogPriority(LogCategory category, LogPriority priority) {
        if (category == 0) {
            throw new ArgumentOutOfRangeException(nameof(category), "Invalid log category.");
        }
        if (priority is < LogPriority.Invalid or > LogPriority.Count) {
            throw new ArgumentOutOfRangeException(nameof(priority), "Invalid log priority.");
        }
        SDL_SetLogPriority(category, priority);
    }

    /// <summary>Set the text prepended to log messages of a given priority.</summary>
    /// <param name="priority">the SDL_LogPriority to modify.</param>
    /// <remarks>
    /// By default, <see cref="LogPriority.Info" /> and below have no
    /// prefix, and <see cref="LogPriority.Warn" /> and higher have
    /// a prefix showing their priority, e.g. &quot;WARNING: &quot;.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetLogPriorities" />
    /// <seealso cref="SetLogPriority" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If an invalid <see cref="LogPriority" /> value is assigned. This is typically thrown if a cast from <see cref="int" /> is passed.</exception>
    public static void SetLogPriorityPrefix(LogPriority priority) {
        if (priority is < LogPriority.Invalid or > LogPriority.Count) {
            throw new ArgumentOutOfRangeException(nameof(priority), "Invalid log priority.");
        }

        // Call the SDL_SetLogPriorityPrefix function with an empty string as the prefix
        SDL_SetLogPriorityPrefix(priority, string.Empty);
    }

    /// <summary>Set the text prepended to log messages of a given priority.</summary>
    /// <param name="priority">the <see cref="LogPriority" /> to modify.</param>
    /// <param name="prefix">the prefix to use for that log priority, or <see langword="null" /> to use no prefix.</param>
    /// <remarks>
    /// By default, <see cref="LogPriority.Info" /> and below have no
    /// prefix, and <see cref="LogPriority.Warn" /> and higher have
    /// a prefix showing their priority, e.g. &quot;WARNING: &quot;.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetLogPriorities" />
    /// <seealso cref="SetLogPriority" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static void SetLogPriorityPrefix(LogPriority priority, string prefix) {
        SDL_SetLogPriorityPrefix(priority, prefix);
    }

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlLogOutputFunction SDL_GetDefaultLogOutputFunction();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GetLogOutputFunction(out SdlLogOutputFunction callback, out nint userdata);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial LogPriority SDL_GetLogPriority(LogCategory category);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_Log(string fmt);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogCritical(LogCategory category, string fmt);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogDebug(LogCategory category, string fmt);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogError(LogCategory category, string fmt);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogInfo(LogCategory category, string fmt);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogMessage(LogCategory category, LogPriority priority, string fmt);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogTrace(LogCategory category, string fmt);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogVerbose(LogCategory category, string fmt);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogWarn(LogCategory category, string fmt);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ResetLogPriorities();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetLogOutputFunction(SdlLogOutputFunction callback, nint userdata);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetLogPriorities(LogPriority priority);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetLogPriority(LogCategory category, LogPriority priority);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetLogPriorityPrefix(LogPriority priority, string prefix);
}