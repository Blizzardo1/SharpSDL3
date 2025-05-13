using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using SharpSDL3.Enums;
using static SharpSDL3.Delegates;

namespace SharpSDL3;

public static unsafe partial class Logger
{
	public static SdlLogOutputFunction GetDefaultLogOutputFunction()
	{
		var callback = SDL_GetDefaultLogOutputFunction();
        return callback == null ? throw new InvalidOperationException("Failed to retrieve default log output function.") : callback;
    }

    public static void GetLogOutputFunction(out SdlLogOutputFunction callback, out nint userdata)
	{
		SDL_GetLogOutputFunction(out callback, out userdata);
		if (callback == null)
		{
			throw new InvalidOperationException("Failed to retrieve log output function.");
		}
	}

	public static LogPriority GetLogPriority(LogCategory category)
	{
		if (category == 0)
		{
			throw new ArgumentOutOfRangeException(nameof(category), "Invalid log category.");
		}
		return SDL_GetLogPriority(category);
	}

	public static void Log(string fmt)
	{
		if (string.IsNullOrWhiteSpace(fmt))
		{
			throw new ArgumentException("Log message cannot be null or empty.", nameof(fmt));
		}

		SDL_Log(fmt);
	}

	public static void LogCritical(LogCategory category, string fmt)
	{
		if (string.IsNullOrWhiteSpace(fmt))
		{
			throw new ArgumentException("Log message cannot be null or empty.", nameof(fmt));
		}
		SDL_LogCritical(category, fmt);
	}

	public static void LogDebug(LogCategory category, string fmt)
	{
		if (string.IsNullOrWhiteSpace(fmt))
		{
			throw new ArgumentException("Log message cannot be null or empty.", nameof(fmt));
		}
		SDL_LogDebug(category, fmt);
	}

	public static void LogError(LogCategory category, string fmt)
	{
		if (string.IsNullOrWhiteSpace(fmt))
		{
			throw new ArgumentException("Log message cannot be null or empty.", nameof(fmt));
		}
		SDL_LogError(category, fmt);
	}

	public static void LogInfo(LogCategory category, string fmt)
	{
		if (string.IsNullOrWhiteSpace(fmt))
		{
			throw new ArgumentException("Log message cannot be null or empty.", nameof(fmt));
		}
		SDL_LogInfo(category, fmt);
	}

	public static void LogMessage(LogCategory category, LogPriority priority, string fmt)
	{
		if (string.IsNullOrWhiteSpace(fmt))
		{
			throw new ArgumentException("Log message cannot be null or empty.", nameof(fmt));
		}
		SDL_LogMessage(category, priority, fmt);
	}

	public static void LogTrace(LogCategory category, string fmt)
	{
		if (string.IsNullOrWhiteSpace(fmt))
		{
			throw new ArgumentException("Log message cannot be null or empty.", nameof(fmt));
		}
		SDL_LogTrace(category, fmt);
	}

	public static void LogVerbose(LogCategory category, string fmt)
	{
		if (string.IsNullOrWhiteSpace(fmt))
		{
			throw new ArgumentException("Log message cannot be null or empty.", nameof(fmt));
		}
		SDL_LogVerbose(category, fmt);
	}

	public static void LogWarn(LogCategory category, string fmt)
	{
		if (string.IsNullOrWhiteSpace(fmt))
		{
			throw new ArgumentException("Log message cannot be null or empty.", nameof(fmt));
		}
		SDL_LogWarn(category, fmt);
	}

	public static void ResetLogPriorities()
	{
		SDL_ResetLogPriorities();
	}

	public static void SetLogOutputFunction(SdlLogOutputFunction callback, nint userdata)
	{
		if (callback == null)
		{
			throw new ArgumentNullException(nameof(callback), "Log output function cannot be null.");
		}
		SDL_SetLogOutputFunction(callback, userdata);
	}

	public static void SetLogPriorities(LogPriority priority)
	{
		if (priority < LogPriority.Invalid || priority > LogPriority.Count)
		{
			throw new ArgumentOutOfRangeException(nameof(priority), "Invalid log priority.");
		}
		SDL_SetLogPriorities(priority);
	}

	public static void SetLogPriority(LogCategory category, LogPriority priority)
	{
		if (category == 0)
		{
			throw new ArgumentOutOfRangeException(nameof(category), "Invalid log category.");
		}
		if (priority < LogPriority.Invalid|| priority > LogPriority.Count)
		{
			throw new ArgumentOutOfRangeException(nameof(priority), "Invalid log priority.");
		}
		SDL_SetLogPriority(category, priority);
	}

public static void SetLogPriorityPrefix(LogPriority priority) {
        if (priority < LogPriority.Invalid || priority > LogPriority.Count) {
            throw new ArgumentOutOfRangeException(nameof(priority), "Invalid log priority.");
        }

        // Call the SDL_SetLogPriorityPrefix function with an empty string as the prefix
        SDL_SetLogPriorityPrefix(priority, string.Empty);
    }

	public static void SetLogPriorityPrefix(LogPriority priority, string prefix)
	{
		if (string.IsNullOrWhiteSpace(prefix))
		{
			throw new ArgumentException("Prefix cannot be null or empty.", nameof(prefix));
		}
		SDL_SetLogPriorityPrefix(priority, prefix);
	}

	[LibraryImport(Sdl.NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlLogOutputFunction SDL_GetDefaultLogOutputFunction();

	[LibraryImport(Sdl.NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_GetLogOutputFunction(out SdlLogOutputFunction callback, out nint userdata);

	[LibraryImport(Sdl.NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial LogPriority SDL_GetLogPriority(LogCategory category);

	[LibraryImport(Sdl.NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_Log(string fmt);

	[LibraryImport(Sdl.NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_LogCritical(LogCategory category, string fmt);

	[LibraryImport(Sdl.NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_LogDebug(LogCategory category, string fmt);

	[LibraryImport(Sdl.NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_LogError(LogCategory category, string fmt);

	[LibraryImport(Sdl.NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_LogInfo(LogCategory category, string fmt);

	[LibraryImport(Sdl.NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_LogMessage(LogCategory category, LogPriority priority, string fmt);

	[LibraryImport(Sdl.NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_LogTrace(LogCategory category, string fmt);

	[LibraryImport(Sdl.NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_LogVerbose(LogCategory category, string fmt);

	[LibraryImport(Sdl.NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_LogWarn(LogCategory category, string fmt);

	[LibraryImport(Sdl.NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_ResetLogPriorities();

	[LibraryImport(Sdl.NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetLogOutputFunction(SdlLogOutputFunction callback, nint userdata);

	[LibraryImport(Sdl.NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetLogPriorities(LogPriority priority);

	[LibraryImport(Sdl.NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetLogPriority(LogCategory category, LogPriority priority);

	[LibraryImport(Sdl.NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetLogPriorityPrefix(LogPriority priority, string prefix);
}