using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using SDL3.Enums;
using SDL3.Structs;
using static SDL3.Delegates;

namespace SDL3;

public static unsafe partial class Sdl
{

	internal const string NativeLibName = "SDL3";

	// /usr/local/include/SDL3/SDL_stdinc.h

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_malloc(nuint size);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_free(nint mem);

	// Safe wrapper methods
	public static nint Malloc(nuint size) {
        if (size == nuint.Zero) {
            Logger.LogWarn(LogCategory.System, "Malloc: Size is zero.");
            return nint.Zero;
        }

        nint res = SDL_malloc(size);
        if (res == nint.Zero) {
            Logger.LogError(LogCategory.System, "Malloc: Memory allocation failed.");
            SDL_OutOfMemory(); // Log the error using SDL's error handling mechanism.  
            return nint.Zero;
        }

        return res;
    }

  
	public static void Free(nint mem)
	{
		if (mem == nint.Zero)
		{
			Logger.LogWarn(LogCategory.System, "Free: Memory pointer is null.");
			return;
		}

		SDL_free(mem);
	}




    // /usr/local/include/SDL3/SDL_endian.h

    // /usr/local/include/SDL3/SDL_error.h

    // Private LibraryImport
    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetError(string fmt);

    // Safe wrapper method
    public static SdlBool SetError(string fmt, params object[] args) {
        if (string.IsNullOrEmpty(fmt)) {
            Logger.LogWarn(LogCategory.System, "SetError: Format string is null or empty.");
            return false;
        }

        string formatted = args.Length > 0 ? string.Format(fmt, args) : fmt;
        return SDL_SetError(formatted);
    }


    [LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_OutOfMemory();
	public static SdlBool OutOfMemory() {
        return SDL_OutOfMemory();
    }


    // Private LibraryImport
    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetError();

    // Safe wrapper method
    public static string GetError() {
        string error = SDL_GetError();
        return string.IsNullOrEmpty(error) ? "No error." : error;
    }



    [LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ClearError();

    public static SdlBool ClearError() {
        return SDL_ClearError();
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetGlobalProperties();

    public static uint GetGlobalProperties() {
        return SDL_GetGlobalProperties();
    }
    // Private LibraryImport
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_CreateProperties();

    // Safe wrapper method
    public static uint CreateProperties() {
        uint props = SDL_CreateProperties();
        if (props == 0) {
            Logger.LogError(LogCategory.System, "CreateProperties: Failed to create properties.");
            throw new InvalidOperationException("SDL_CreateProperties failed.");
        }

        return props;
    }


    [LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_CopyProperties(uint src, uint dst);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_LockProperties(uint props);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_UnlockProperties(uint props);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetPointerPropertyWithCleanup(uint props, string name, nint value,
		SdlCleanupPropertyCallback cleanup, nint userdata);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetPointerProperty(uint props, string name, nint value);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetStringProperty(uint props, string name, string value);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetNumberProperty(uint props, string name, long value);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetFloatProperty(uint props, string name, float value);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetBooleanProperty(uint props, string name, SdlBool value);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasProperty(uint props, string name);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial PropertyType SDL_GetPropertyType(uint props, string name);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetPointerProperty(uint props, string name, nint defaultValue);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetStringProperty(uint props, string name, string defaultValue);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial long SDL_GetNumberProperty(uint props, string name, long defaultValue);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial float SDL_GetFloatProperty(uint props, string name, float defaultValue);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetBooleanProperty(uint props, string name, SdlBool defaultValue);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ClearProperty(uint props, string name);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_EnumerateProperties(uint props, SdlEnumeratePropertiesCallback callback,
		nint userdata);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DestroyProperties(uint props);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateThreadRuntime(SdlThreadFunction fn, string name, nint data,
		nint pfnBeginThread, nint pfnEndThread);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateThreadWithPropertiesRuntime(uint props, nint pfnBeginThread,
		nint pfnEndThread);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetThreadName(nint thread);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ulong SDL_GetCurrentThreadID();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ulong SDL_GetThreadID(nint thread);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetCurrentThreadPriority(ThreadPriority priority);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_WaitThread(nint thread, nint status);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ThreadState SDL_GetThreadState(nint thread);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DetachThread(nint thread);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetTLS(nint id);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetTLS(nint id, nint value, SdlTlsDestructorCallback destructor);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_CleanupTLS();

	// /usr/local/include/SDL3/SDL_mutex.h

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateMutex();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_LockMutex(nint mutex);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_TryLockMutex(nint mutex);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_UnlockMutex(nint mutex);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DestroyMutex(nint mutex);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateRWLock();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_LockRWLockForReading(nint rwlock);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_LockRWLockForWriting(nint rwlock);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_TryLockRWLockForReading(nint rwlock);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_TryLockRWLockForWriting(nint rwlock);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_UnlockRWLock(nint rwlock);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DestroyRWLock(nint rwlock);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateSemaphore(uint initialValue);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DestroySemaphore(nint sem);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_WaitSemaphore(nint sem);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_TryWaitSemaphore(nint sem);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_WaitSemaphoreTimeout(nint sem, int timeoutMs);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SignalSemaphore(nint sem);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetSemaphoreValue(nint sem);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateCondition();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DestroyCondition(nint cond);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SignalCondition(nint cond);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_BroadcastCondition(nint cond);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_WaitCondition(nint cond, nint mutex);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_WaitConditionTimeout(nint cond, nint mutex, int timeoutMs);

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
	private static partial SdlBool SDL_ShouldInit(ref InitState state);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ShouldQuit(ref InitState state);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetInitialized(ref InitState state, SdlBool initialized);

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


	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_ComposeCustomBlendMode(BlendFactor srcColorFactor, BlendFactor dstColorFactor,
		BlendOperation colorOperation, BlendFactor srcAlphaFactor, BlendFactor dstAlphaFactor,
		BlendOperation alphaOperation);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetPixelFormatName(PixelFormat format);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetMasksForPixelFormat(PixelFormat format, out int bpp, out uint rmask,
		out uint gmask, out uint bmask, out uint amask);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial PixelFormat SDL_GetPixelFormatForMasks(int bpp, uint rmask, uint gmask, uint bmask,
		uint amask);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial PixelFormatDetails* SDL_GetPixelFormatDetails(PixelFormat format);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial Palette* SDL_CreatePalette(int ncolors);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetPaletteColors(nint palette, Span<Color> colors, int firstcolor,
		int ncolors);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DestroyPalette(nint palette);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_MapRGB(nint format, nint palette, byte r, byte g, byte b);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_MapRGBA(nint format, nint palette, byte r, byte g, byte b, byte a);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_GetRGB(uint pixel, nint format, nint palette, out byte r, out byte g,
		out byte b);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_GetRGBA(uint pixel, nint format, nint palette, out byte r, out byte g,
		out byte b, out byte a);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasRectIntersection(ref Rect a, ref Rect b);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetRectIntersection(ref Rect a, ref Rect b, out Rect result);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetRectUnion(ref Rect a, ref Rect b, out Rect result);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetRectEnclosingPoints(Span<Point> points, int count, ref Rect clip,
		out Rect result);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetRectAndLineIntersection(ref Rect rect, ref int x1, ref int y1, ref int x2,
		ref int y2);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasRectIntersectionFloat(ref FRect a, ref FRect b);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetRectIntersectionFloat(ref FRect a, ref FRect b, out FRect result);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetRectUnionFloat(ref FRect a, ref FRect b, out FRect result);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetRectEnclosingPointsFloat(Span<FPoint> points, int count, ref FRect clip,
		out FRect result);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetRectAndLineIntersectionFloat(ref FRect rect, ref float x1, ref float y1,
		ref float x2, ref float y2);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial Surface* SDL_CreateSurface(int width, int height, PixelFormat format);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial Surface* SDL_CreateSurfaceFrom(int width, int height, PixelFormat format, nint pixels,
		int pitch);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DestroySurface(nint surface);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetSurfaceProperties(nint surface);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetSurfaceColorspace(nint surface, Colorspace colorspace);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial Colorspace SDL_GetSurfaceColorspace(nint surface);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial Palette* SDL_CreateSurfacePalette(nint surface);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetSurfacePalette(nint surface, nint palette);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial Palette* SDL_GetSurfacePalette(nint surface);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_AddSurfaceAlternateImage(nint surface, nint image);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SurfaceHasAlternateImages(nint surface);

	public static Span<nint> GetSurfaceImages(nint surface, out int count)
	{
		nint result = SDL_GetSurfaceImages(surface, out count);
		return new Span<nint>((void*) result, count);
	}

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetSurfaceImages(nint surface, out int count);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_RemoveSurfaceAlternateImages(nint surface);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_LockSurface(nint surface);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_UnlockSurface(nint surface);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial Surface* SDL_LoadBMP_IO(nint src, SdlBool closeio);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial Surface* SDL_LoadBMP(string file);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SaveBMP_IO(nint surface, nint dst, SdlBool closeio);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SaveBMP(nint surface, string file);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetSurfaceRLE(nint surface, SdlBool enabled);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SurfaceHasRLE(nint surface);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetSurfaceColorKey(nint surface, SdlBool enabled, uint key);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SurfaceHasColorKey(nint surface);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetSurfaceColorKey(nint surface, out uint key);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetSurfaceColorMod(nint surface, byte r, byte g, byte b);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetSurfaceColorMod(nint surface, out byte r, out byte g, out byte b);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetSurfaceAlphaMod(nint surface, byte alpha);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetSurfaceAlphaMod(nint surface, out byte alpha);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetSurfaceBlendMode(nint surface, uint blendMode);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetSurfaceBlendMode(nint surface, nint blendMode);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetSurfaceClipRect(nint surface, ref Rect rect);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetSurfaceClipRect(nint surface, out Rect rect);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_FlipSurface(nint surface, FlipMode flip);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial Surface* SDL_DuplicateSurface(nint surface);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial Surface* SDL_ScaleSurface(nint surface, int width, int height, ScaleMode scaleMode);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial Surface* SDL_ConvertSurface(nint surface, PixelFormat format);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial Surface* SDL_ConvertSurfaceAndColorspace(nint surface, PixelFormat format,
		nint palette, Colorspace colorspace, uint props);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ConvertPixels(int width, int height, PixelFormat srcFormat, nint src,
		int srcPitch, PixelFormat dstFormat, nint dst, int dstPitch);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ConvertPixelsAndColorspace(int width, int height, PixelFormat srcFormat,
		Colorspace srcColorspace, uint srcProperties, nint src, int srcPitch, PixelFormat dstFormat,
		Colorspace dstColorspace, uint dstProperties, nint dst, int dstPitch);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_PremultiplyAlpha(int width, int height, PixelFormat srcFormat, nint src,
		int srcPitch, PixelFormat dstFormat, nint dst, int dstPitch, SdlBool linear);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_PremultiplySurfaceAlpha(nint surface, SdlBool linear);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ClearSurface(nint surface, float r, float g, float b, float a);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool
		SDL_FillSurfaceRect(nint dst, nint rect, uint color); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_FillSurfaceRects(nint dst, Span<Rect> rects, int count, uint color);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool
		SDL_BlitSurface(nint src, nint srcrect, nint dst, nint dstrect); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool
		SDL_BlitSurfaceUnchecked(nint src, nint srcrect, nint dst,
			nint dstrect); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_BlitSurfaceScaled(nint src, nint srcrect, nint dst, nint dstrect,
		ScaleMode scaleMode); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_BlitSurfaceUncheckedScaled(nint src, nint srcrect, nint dst, nint dstrect,
		ScaleMode scaleMode); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool
		SDL_BlitSurfaceTiled(nint src, nint srcrect, nint dst, nint dstrect); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_BlitSurfaceTiledWithScale(nint src, nint srcrect, float scale,
		ScaleMode scaleMode, nint dst, nint dstrect); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_BlitSurface9Grid(nint src, nint srcrect, int leftWidth, int rightWidth,
		int topHeight, int bottomHeight, float scale, ScaleMode scaleMode, nint dst,
		nint dstrect); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_MapSurfaceRGB(nint surface, byte r, byte g, byte b);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_MapSurfaceRGBA(nint surface, byte r, byte g, byte b, byte a);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ReadSurfacePixel(nint surface, int x, int y, out byte r, out byte g, out byte b,
		out byte a);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ReadSurfacePixelFloat(nint surface, int x, int y, out float r, out float g,
		out float b, out float a);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_WriteSurfacePixel(nint surface, int x, int y, byte r, byte g, byte b, byte a);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_WriteSurfacePixelFloat(nint surface, int x, int y, float r, float g, float b,
		float a);


	// /usr/local/include/SDL3/SDL_clipboard.h

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetClipboardText(string text);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(CallerOwnedStringMarshaller))]
	private static partial string SDL_GetClipboardText();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasClipboardText();

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetPrimarySelectionText(string text);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(CallerOwnedStringMarshaller))]
	private static partial string SDL_GetPrimarySelectionText();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasPrimarySelectionText();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetClipboardData(SdlClipboardDataCallback callback,
		SdlClipboardCleanupCallback cleanup, nint userdata, nint mimeTypes, nuint numMimeTypes);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ClearClipboardData();

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetClipboardData(string mimeType, out nuint size);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasClipboardData(string mimeType);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetClipboardMimeTypes(out nuint numMimeTypes);


	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumVideoDrivers();

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetVideoDriver(int index);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetCurrentVideoDriver();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SystemTheme SDL_GetSystemTheme();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetDisplays(out int count);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetPrimaryDisplay();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetDisplayProperties(uint displayId);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetDisplayName(uint displayId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetDisplayBounds(uint displayId, out Rect rect);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetDisplayUsableBounds(uint displayId, out Rect rect);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial DisplayOrientation SDL_GetNaturalDisplayOrientation(uint displayId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial DisplayOrientation SDL_GetCurrentDisplayOrientation(uint displayId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial float SDL_GetDisplayContentScale(uint displayId);

	public static Span<int> GetFullscreenDisplayModes(uint displayId) {
		nint result = SDL_GetFullscreenDisplayModes(displayId, out int count);
        return new Span<int>((void*)result, count);
    }

	public static Span<nint> GetFullscreenDisplayModes(uint displayId, out int count)
	{
		nint result = SDL_GetFullscreenDisplayModes(displayId, out count);
		return new Span<nint>((void*) result, count);
	}

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetFullscreenDisplayModes(uint displayId, out int count);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetClosestFullscreenDisplayMode(uint displayId, int w, int h, float refreshRate,
		SdlBool includeHighDensityModes, out DisplayMode closest);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial DisplayMode* SDL_GetDesktopDisplayMode(uint displayId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial DisplayMode* SDL_GetCurrentDisplayMode(uint displayId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetDisplayForPoint(ref Point point);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetDisplayForRect(ref Rect rect);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetDisplayForWindow(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial float SDL_GetWindowPixelDensity(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial float SDL_GetWindowDisplayScale(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetWindowFullscreenMode(nint window, ref DisplayMode mode);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial DisplayMode* SDL_GetWindowFullscreenMode(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetWindowICCProfile(nint window, out nuint size);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial PixelFormat SDL_GetWindowPixelFormat(nint window);

	public static Span<nint> GetWindows(out int count)
	{
		nint result = SDL_GetWindows(out count);
		return new Span<nint>((void*) result, count);
	}

	public static Span<nint> GetWindows() {
		nint result = SDL_GetWindows(out int count);
        return new Span<nint>((void*)result, count);
    }

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetWindows(out int count);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateWindow(string title, int w, int h, WindowFlags flags);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreatePopupWindow(nint parent, int offsetX, int offsetY, int w, int h,
		WindowFlags flags);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateWindowWithProperties(uint props);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetWindowID(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetWindowFromID(uint id);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetWindowParent(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetWindowProperties(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial WindowFlags SDL_GetWindowFlags(nint window);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetWindowTitle(nint window, string title);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetWindowTitle(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetWindowIcon(nint window, nint icon);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetWindowPosition(nint window, int x, int y);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetWindowPosition(nint window, out int x, out int y);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetWindowSize(nint window, int w, int h);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetWindowSize(nint window, out int w, out int h);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetWindowSafeArea(nint window, out Rect rect);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetWindowAspectRatio(nint window, float minAspect, float maxAspect);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetWindowAspectRatio(nint window, out float minAspect, out float maxAspect);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetWindowBordersSize(nint window, out int top, out int left, out int bottom,
		out int right);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetWindowSizeInPixels(nint window, out int w, out int h);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetWindowMinimumSize(nint window, int minW, int minH);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetWindowMinimumSize(nint window, out int w, out int h);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetWindowMaximumSize(nint window, int maxW, int maxH);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetWindowMaximumSize(nint window, out int w, out int h);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetWindowBordered(nint window, SdlBool bordered);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetWindowResizable(nint window, SdlBool resizable);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetWindowAlwaysOnTop(nint window, SdlBool onTop);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ShowWindow(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HideWindow(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RaiseWindow(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_MaximizeWindow(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_MinimizeWindow(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RestoreWindow(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetWindowFullscreen(nint window, SdlBool fullscreen);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SyncWindow(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_WindowHasSurface(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial Surface* SDL_GetWindowSurface(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetWindowSurfaceVSync(nint window, int vsync);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetWindowSurfaceVSync(nint window, out int vsync);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_UpdateWindowSurface(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_UpdateWindowSurfaceRects(nint window, Span<Rect> rects, int numrects);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_DestroyWindowSurface(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetWindowKeyboardGrab(nint window, SdlBool grabbed);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetWindowMouseGrab(nint window, SdlBool grabbed);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetWindowKeyboardGrab(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetWindowMouseGrab(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetGrabbedWindow();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetWindowMouseRect(nint window, ref Rect rect);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial Rect* SDL_GetWindowMouseRect(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetWindowOpacity(nint window, float opacity);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial float SDL_GetWindowOpacity(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetWindowParent(nint window, nint parent);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetWindowModal(nint window, SdlBool modal);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetWindowFocusable(nint window, SdlBool focusable);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ShowWindowSystemMenu(nint window, int x, int y);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetWindowHitTest(nint window, SdlHitTest callback, nint callbackData);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetWindowShape(nint window, nint shape);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_FlashWindow(nint window, FlashOperation operation);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DestroyWindow(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ScreenSaverEnabled();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_EnableScreenSaver();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_DisableScreenSaver();

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_ShowOpenFileDialog(SdlDialogFileCallback callback, nint userdata, nint window,
		Span<DialogFileFilter> filters, int nfilters, string defaultLocation, SdlBool allowMany);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_ShowSaveFileDialog(SdlDialogFileCallback callback, nint userdata, nint window,
		Span<DialogFileFilter> filters, int nfilters, string defaultLocation);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_ShowOpenFolderDialog(SdlDialogFileCallback callback, nint userdata, nint window,
		string defaultLocation, SdlBool allowMany);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_ShowFileDialogWithProperties(FileDialogType type, SdlDialogFileCallback callback,
		nint userdata, uint props);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_GUIDToString(SdlGuid guid, string pszGuid, int cbGuid);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlGuid SDL_StringToGUID(string pchGuid);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial PowerState SDL_GetPowerInfo(out int seconds, out int percent);


	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasKeyboard();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetKeyboards(out int count);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetKeyboardNameForID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetKeyboardFocus();

	public static Span<SdlBool> GetKeyboardState(out int numkeys) {
        nint result = SDL_GetKeyboardState(out numkeys);
        return new Span<SdlBool>((void*)result, numkeys);
    }

    [LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetKeyboardState(out int numkeys);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_ResetKeyboard();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial KeyMod SDL_GetModState();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetModState(KeyMod modstate);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetKeyFromScancode(ScanCode scanCode, KeyMod modstate, SdlBool keyEvent);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ScanCode SDL_GetScancodeFromKey(uint key, nint modstate);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetScancodeName(ScanCode scanCode, string name);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetScancodeName(ScanCode scanCode);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ScanCode SDL_GetScancodeFromName(string name);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetKeyName(uint key);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetKeyFromName(string name);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_StartTextInput(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_StartTextInputWithProperties(nint window, uint props);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_TextInputActive(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_StopTextInput(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ClearComposition(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetTextInputArea(nint window, ref Rect rect, int cursor);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetTextInputArea(nint window, out Rect rect, out int cursor);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasScreenKeyboardSupport();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ScreenKeyboardShown(nint window);


	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetHintWithPriority(string name, string value, HintPriority priority);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetHint(string name, string value);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ResetHint(string name);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_ResetHints();

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetHint(string name);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetHintBoolean(string name, SdlBool defaultValue);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_AddHintCallback(string name, SdlHintCallback callback, nint userdata);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_RemoveHintCallback(string name, SdlHintCallback callback, nint userdata);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_Init(InitFlags flags);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_InitSubSystem(InitFlags flags);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_QuitSubSystem(InitFlags flags);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial InitFlags SDL_WasInit(InitFlags flags);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_Quit();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_IsMainThread();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RunOnMainThread(SdlMainThreadCallback callback, nint userdata,
		SdlBool waitComplete);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetAppMetadata(string appname, string appversion, string appidentifier);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetAppMetadataProperty(string name, string value);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetAppMetadataProperty(string name);

	// /usr/local/include/SDL3/SDL_loadso.h

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_LoadObject(string sofile);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_LoadFunction(nint handle, string name);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_UnloadObject(nint handle);

	public static Span<nint> GetPreferredLocales()
	{
		nint result = SDL_GetPreferredLocales(out int count);
		return new Span<nint>((void*) result, count);
	}

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetPreferredLocales(out int count);

	// /usr/local/include/SDL3/SDL_version.h

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetVersion();

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetRevision();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetMainReady();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_RunApp(int argc, nint argv, SdlMainFunc mainFunction, nint reserved);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_EnterAppMainCallbacks(int argc, nint argv, SdlAppInitFunc AppInit,
		SdlAppIterateFunc AppIter, SdlAppEventFunc sdlAppEvent, SdlAppQuitFunc AppQuit);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_GDKSuspendComplete();
}