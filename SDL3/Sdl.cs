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

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ShouldInit(ref InitState state);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ShouldQuit(ref InitState state);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetInitialized(ref InitState state, SdlBool initialized);


	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumAudioDrivers();

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetAudioDriver(int index);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetCurrentAudioDriver();

	public static string GetAudioDriver(int index) {
        string driver = SDL_GetAudioDriver(index);
        if (string.IsNullOrEmpty(driver)) {
            Logger.LogError(LogCategory.System, "GetAudioDriver: Failed to get audio driver.");
            throw new InvalidOperationException("SDL_GetAudioDriver failed.");
        }
        return driver;
    }

	public static string GetCurrentAudioDriver() {
        string driver = SDL_GetCurrentAudioDriver();
        if (string.IsNullOrEmpty(driver)) {
            Logger.LogError(LogCategory.System, "GetCurrentAudioDriver: Failed to get current audio driver.");
            throw new InvalidOperationException("SDL_GetCurrentAudioDriver failed.");
        }
        return driver;
    }

    [LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetAudioPlaybackDevices(out int count);

	public static nint GetAudioPlaybackDevices(out int count) {
        nint result = SDL_GetAudioPlaybackDevices(out count);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetAudioPlaybackDevices: Failed to get audio playback devices.");
            throw new InvalidOperationException("SDL_GetAudioPlaybackDevices failed.");
        }
        return result;
    }

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetAudioRecordingDevices(out int count);

	public static nint GetAudioRecordingDevices(out int count) {
        nint result = SDL_GetAudioRecordingDevices(out count);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetAudioRecordingDevices: Failed to get audio recording devices.");
            throw new InvalidOperationException("SDL_GetAudioRecordingDevices failed.");
        }
        return result;
    }

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetAudioDeviceName(uint devid);

	public static string GetAudioDeviceName(uint devId) {
        string name = SDL_GetAudioDeviceName(devId);
        if (string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.System, "GetAudioDeviceName: Failed to get audio device name.");
            throw new InvalidOperationException("SDL_GetAudioDeviceName failed.");
        }
        return name;
    }

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetAudioDeviceFormat(uint devid, out AudioSpec spec, out int sampleFrames);
	public static SdlBool GetAudioDeviceFormat(uint devId, out AudioSpec spec, out int sampleFrames) {
        SdlBool result = SDL_GetAudioDeviceFormat(devId, out spec, out sampleFrames);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetAudioDeviceFormat: Failed to get audio device format.");
            throw new InvalidOperationException("SDL_GetAudioDeviceFormat failed.");
        }
        return result;
    }

	public static Span<int> GetAudioDeviceChannelMap(uint devid)
	{
		nint result = SDL_GetAudioDeviceChannelMap(devid, out int count);
		return new Span<int>((void*) result, count);
	}

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetAudioDeviceChannelMap(uint devid, out int count);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_OpenAudioDevice(uint devid, ref AudioSpec spec);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_IsAudioDevicePhysical(uint devid);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_IsAudioDevicePlayback(uint devid);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_PauseAudioDevice(uint dev);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ResumeAudioDevice(uint dev);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_AudioDevicePaused(uint dev);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial float SDL_GetAudioDeviceGain(uint devid);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetAudioDeviceGain(uint devid, float gain);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_CloseAudioDevice(uint devid);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_BindAudioStreams(uint devid, Span<nint> streams, int numStreams);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_BindAudioStream(uint devid, nint stream);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_UnbindAudioStreams(Span<nint> streams, int numStreams);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_UnbindAudioStream(nint stream);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetAudioStreamDevice(nint stream);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateAudioStream(ref AudioSpec srcSpec, ref AudioSpec dstSpec);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetAudioStreamProperties(nint stream);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetAudioStreamFormat(nint stream, out AudioSpec srcSpec,
		out AudioSpec dstSpec);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetAudioStreamFormat(nint stream, ref AudioSpec srcSpec,
		ref AudioSpec dstSpec);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial float SDL_GetAudioStreamFrequencyRatio(nint stream);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetAudioStreamFrequencyRatio(nint stream, float ratio);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial float SDL_GetAudioStreamGain(nint stream);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetAudioStreamGain(nint stream, float gain);

	private static Span<int> SDL_GetAudioStreamInputChannelMap(nint stream)
	{
		nint result = SDL_GetAudioStreamInputChannelMap(stream, out int count);
		return new Span<int>((void*) result, count);
	}

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetAudioStreamInputChannelMap(nint stream, out int count);

	private static Span<int> SDL_GetAudioStreamOutputChannelMap(nint stream)
	{
		nint result = SDL_GetAudioStreamOutputChannelMap(stream, out int count);
		return new Span<int>((void*) result, count);
	}

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetAudioStreamOutputChannelMap(nint stream, out int count);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetAudioStreamInputChannelMap(nint stream, Span<int> chmap, int count);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetAudioStreamOutputChannelMap(nint stream, Span<int> chmap, int count);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_PutAudioStreamData(nint stream, nint buf, int len);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetAudioStreamData(nint stream, nint buf, int len);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetAudioStreamAvailable(nint stream);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetAudioStreamQueued(nint stream);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_FlushAudioStream(nint stream);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ClearAudioStream(nint stream);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_PauseAudioStreamDevice(nint stream);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ResumeAudioStreamDevice(nint stream);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_AudioStreamDevicePaused(nint stream);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_LockAudioStream(nint stream);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_UnlockAudioStream(nint stream);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetAudioStreamGetCallback(nint stream, SdlAudioStreamCallback callback,
		nint userdata);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetAudioStreamPutCallback(nint stream, SdlAudioStreamCallback callback,
		nint userdata);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DestroyAudioStream(nint stream);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_OpenAudioDeviceStream(uint devid, ref AudioSpec spec,
		SdlAudioStreamCallback callback, nint userdata);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetAudioPostmixCallback(uint devid, SdlAudioPostmixCallback callback,
		nint userdata);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_LoadWAV_IO(nint src, SdlBool closeio, out AudioSpec spec,
		out nint audioBuf, out uint audioLen);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_LoadWAV(string path, out AudioSpec spec, out nint audioBuf,
		out uint audioLen);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_MixAudio(nint dst, nint src, AudioFormat format, uint len, float volume);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ConvertAudioSamples(ref AudioSpec srcSpec, nint srcData, int srcLen,
		ref AudioSpec dstSpec, nint dstData, out int dstLen);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetAudioFormatName(AudioFormat format);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetSilenceValueForFormat(AudioFormat format);

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

	private static Span<nint> SDL_GetSurfaceImages(nint surface)
	{
		nint result = SDL_GetSurfaceImages(surface, out int count);
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

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumCameraDrivers();

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetCameraDriver(int index);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetCurrentCameraDriver();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetCameras(out int count);

	private static Span<nint> SDL_GetCameraSupportedFormats(uint devid)
	{
		nint result = SDL_GetCameraSupportedFormats(devid, out int count);
		return new Span<nint>((void*) result, count);
	}

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetCameraSupportedFormats(uint devid, out int count);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetCameraName(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial CameraPosition SDL_GetCameraPosition(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_OpenCamera(uint instanceId, ref CameraSpec spec);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetCameraPermissionState(nint camera);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetCameraID(nint camera);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetCameraProperties(nint camera);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetCameraFormat(nint camera, out CameraSpec spec);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial Surface* SDL_AcquireCameraFrame(nint camera, out ulong timestampNs);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_ReleaseCameraFrame(nint camera, nint frame);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_CloseCamera(nint camera);

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

	// /usr/local/include/SDL3/SDL_cpuinfo.h

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumLogicalCPUCores();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetCPUCacheLineSize();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasAltiVec();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasMMX();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasSSE();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasSSE2();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasSSE3();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasSSE41();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasSSE42();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasAVX();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasAVX2();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasAVX512F();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasARMSIMD();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasNEON();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasLSX();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasLASX();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetSystemRAM();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nuint SDL_GetSIMDAlignment();

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

	private static Span<nint> SDL_GetFullscreenDisplayModes(uint displayId)
	{
		nint result = SDL_GetFullscreenDisplayModes(displayId, out int count);
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

	private static Span<nint> SDL_GetWindows()
	{
		nint result = SDL_GetWindows(out int count);
		return new Span<nint>((void*) result, count);
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
	private static partial SdlBool SDL_GL_LoadLibrary(string path);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GL_GetProcAddress(string proc);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_EGL_GetProcAddress(string proc);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_GL_UnloadLibrary();

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GL_ExtensionSupported(string extension);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_GL_ResetAttributes();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GL_SetAttribute(GlAttr attr, int value);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GL_GetAttribute(GlAttr attr, out int value);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GL_CreateContext(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GL_MakeCurrent(nint window, nint context);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GL_GetCurrentWindow();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GL_GetCurrentContext();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_EGL_GetCurrentDisplay();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_EGL_GetCurrentConfig();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_EGL_GetWindowSurface(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_EGL_SetAttributeCallbacks(SdlEglAttribArrayCallback platformAttribCallback,
		SdlEglIntArrayCallback surfaceAttribCallback, SdlEglIntArrayCallback contextAttribCallback, nint userdata);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GL_SetSwapInterval(int interval);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GL_GetSwapInterval(out int interval);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GL_SwapWindow(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GL_DestroyContext(nint context);

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
	private static partial nint SDL_GetSensors(out int count);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetSensorNameForID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SensorType SDL_GetSensorTypeForID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetSensorNonPortableTypeForID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_OpenSensor(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetSensorFromID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetSensorProperties(nint sensor);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetSensorName(nint sensor);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SensorType SDL_GetSensorType(nint sensor);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetSensorNonPortableType(nint sensor);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetSensorID(nint sensor);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetSensorData(nint sensor, float* data, int numValues);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_CloseSensor(nint sensor);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_UpdateSensors();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_LockJoysticks();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_UnlockJoysticks();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasJoystick();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetJoysticks(out int count);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetJoystickNameForID(uint instanceId);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetJoystickPathForID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetJoystickPlayerIndexForID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlGuid SDL_GetJoystickGUIDForID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickVendorForID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickProductForID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickProductVersionForID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial JoystickType SDL_GetJoystickTypeForID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_OpenJoystick(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetJoystickFromID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetJoystickFromPlayerIndex(int playerIndex);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_AttachVirtualJoystick(ref VirtualJoystickDesc desc);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_DetachVirtualJoystick(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_IsJoystickVirtual(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetJoystickVirtualAxis(nint joystick, int axis, short value);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetJoystickVirtualBall(nint joystick, int ball, short xrel, short yrel);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetJoystickVirtualButton(nint joystick, int button, SdlBool down);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetJoystickVirtualHat(nint joystick, int hat, byte value);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetJoystickVirtualTouchpad(nint joystick, int touchpad, int finger,
		SdlBool down, float x, float y, float pressure);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SendJoystickVirtualSensorData(nint joystick, SensorType type,
		ulong sensorTimestamp, float* data, int numValues);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetJoystickProperties(nint joystick);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetJoystickName(nint joystick);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetJoystickPath(nint joystick);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetJoystickPlayerIndex(nint joystick);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetJoystickPlayerIndex(nint joystick, int playerIndex);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlGuid SDL_GetJoystickGUID(nint joystick);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickVendor(nint joystick);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickProduct(nint joystick);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickProductVersion(nint joystick);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickFirmwareVersion(nint joystick);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetJoystickSerial(nint joystick);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial JoystickType SDL_GetJoystickType(nint joystick);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_GetJoystickGUIDInfo(SdlGuid guid, out ushort vendor, out ushort product,
		out ushort version, out ushort crc16);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_JoystickConnected(nint joystick);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetJoystickID(nint joystick);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumJoystickAxes(nint joystick);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumJoystickBalls(nint joystick);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumJoystickHats(nint joystick);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumJoystickButtons(nint joystick);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetJoystickEventsEnabled(SdlBool enabled);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_JoystickEventsEnabled();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_UpdateJoysticks();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial short SDL_GetJoystickAxis(nint joystick, int axis);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetJoystickAxisInitialState(nint joystick, int axis, out short state);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetJoystickBall(nint joystick, int ball, out int dx, out int dy);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial byte SDL_GetJoystickHat(nint joystick, int hat);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetJoystickButton(nint joystick, int button);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RumbleJoystick(nint joystick, ushort lowFrequencyRumble,
		ushort highFrequencyRumble, uint durationMs);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RumbleJoystickTriggers(nint joystick, ushort leftRumble, ushort rightRumble,
		uint durationMs);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetJoystickLED(nint joystick, byte red, byte green, byte blue);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SendJoystickEffect(nint joystick, nint data, int size);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_CloseJoystick(nint joystick);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial JoystickConnectionState SDL_GetJoystickConnectionState(nint joystick);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial PowerState SDL_GetJoystickPowerInfo(nint joystick, out int percent);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_AddGamepadMapping(string mapping);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_AddGamepadMappingsFromIO(nint src, SdlBool closeio);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_AddGamepadMappingsFromFile(string file);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ReloadGamepadMappings();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetGamepadMappings(out int count);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(CallerOwnedStringMarshaller))]
	private static partial string SDL_GetGamepadMappingForGUID(SdlGuid guid);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(CallerOwnedStringMarshaller))]
	private static partial string SDL_GetGamepadMapping(nint gamepad);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetGamepadMapping(uint instanceId, string mapping);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasGamepad();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetGamepads(out int count);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_IsGamepad(uint instanceId);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetGamepadNameForID(uint instanceId);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetGamepadPathForID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetGamepadPlayerIndexForID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlGuid SDL_GetGamepadGUIDForID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetGamepadVendorForID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetGamepadProductForID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetGamepadProductVersionForID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial GamepadType SDL_GetGamepadTypeForID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial GamepadType SDL_GetRealGamepadTypeForID(uint instanceId);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(CallerOwnedStringMarshaller))]
	private static partial string SDL_GetGamepadMappingForID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_OpenGamepad(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetGamepadFromID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetGamepadFromPlayerIndex(int playerIndex);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetGamepadProperties(nint gamepad);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetGamepadID(nint gamepad);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetGamepadName(nint gamepad);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetGamepadPath(nint gamepad);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial GamepadType SDL_GetGamepadType(nint gamepad);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial GamepadType SDL_GetRealGamepadType(nint gamepad);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetGamepadPlayerIndex(nint gamepad);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetGamepadPlayerIndex(nint gamepad, int playerIndex);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetGamepadVendor(nint gamepad);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetGamepadProduct(nint gamepad);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetGamepadProductVersion(nint gamepad);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetGamepadFirmwareVersion(nint gamepad);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetGamepadSerial(nint gamepad);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ulong SDL_GetGamepadSteamHandle(nint gamepad);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial JoystickConnectionState SDL_GetGamepadConnectionState(nint gamepad);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial PowerState SDL_GetGamepadPowerInfo(nint gamepad, out int percent);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GamepadConnected(nint gamepad);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetGamepadJoystick(nint gamepad);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetGamepadEventsEnabled(SdlBool enabled);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GamepadEventsEnabled();

	private static Span<nint> SDL_GetGamepadBindings(nint gamepad)
	{
		nint result = SDL_GetGamepadBindings(gamepad, out int count);
		return new Span<nint>((void*) result, count);
	}

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetGamepadBindings(nint gamepad, out int count);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_UpdateGamepads();

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial GamepadType SDL_GetGamepadTypeFromString(string str);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetGamepadStringForType(GamepadType type);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial GamepadAxis SDL_GetGamepadAxisFromString(string str);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetGamepadStringForAxis(GamepadAxis axis);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GamepadHasAxis(nint gamepad, GamepadAxis axis);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial short SDL_GetGamepadAxis(nint gamepad, GamepadAxis axis);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial GamepadButton SDL_GetGamepadButtonFromString(string str);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetGamepadStringForButton(GamepadButton button);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GamepadHasButton(nint gamepad, GamepadButton button);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetGamepadButton(nint gamepad, GamepadButton button);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial GamepadButtonLabel SDL_GetGamepadButtonLabelForType(GamepadType type,
		GamepadButton button);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial GamepadButtonLabel SDL_GetGamepadButtonLabel(nint gamepad, GamepadButton button);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumGamepadTouchpads(nint gamepad);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumGamepadTouchpadFingers(nint gamepad, int touchpad);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetGamepadTouchpadFinger(nint gamepad, int touchpad, int finger,
		out SdlBool down, out float x, out float y, out float pressure);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GamepadHasSensor(nint gamepad, SensorType type);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetGamepadSensorEnabled(nint gamepad, SensorType type, SdlBool enabled);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GamepadSensorEnabled(nint gamepad, SensorType type);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial float SDL_GetGamepadSensorDataRate(nint gamepad, SensorType type);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetGamepadSensorData(nint gamepad, SensorType type, float* data,
		int numValues);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RumbleGamepad(nint gamepad, ushort lowFrequencyRumble,
		ushort highFrequencyRumble, uint durationMs);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RumbleGamepadTriggers(nint gamepad, ushort leftRumble, ushort rightRumble,
		uint durationMs);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetGamepadLED(nint gamepad, byte red, byte green, byte blue);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SendGamepadEffect(nint gamepad, nint data, int size);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_CloseGamepad(nint gamepad);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetGamepadAppleSFSymbolsNameForButton(nint gamepad, GamepadButton button);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetGamepadAppleSFSymbolsNameForAxis(nint gamepad, GamepadAxis axis);

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

	private static Span<SdlBool> SDL_GetKeyboardState()
	{
		nint result = SDL_GetKeyboardState(out int numkeys);
		return new Span<SdlBool>((void*) result, numkeys);
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

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasMouse();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetMice(out int count);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetMouseNameForID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetMouseFocus();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial MouseButtonFlags SDL_GetMouseState(out float x, out float y);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial MouseButtonFlags SDL_GetGlobalMouseState(out float x, out float y);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial MouseButtonFlags SDL_GetRelativeMouseState(out float x, out float y);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_WarpMouseInWindow(nint window, float x, float y);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_WarpMouseGlobal(float x, float y);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetWindowRelativeMouseMode(nint window, SdlBool enabled);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetWindowRelativeMouseMode(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_CaptureMouse(SdlBool enabled);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateCursor(nint data, nint mask, int w, int h, int hotX, int hotY);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateColorCursor(nint surface, int hotX, int hotY);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateSystemCursor(SystemCursor id);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetCursor(nint cursor);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetCursor();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetDefaultCursor();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DestroyCursor(nint cursor);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ShowCursor();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HideCursor();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_CursorVisible();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetTouchDevices(out int count);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetTouchDeviceName(ulong touchId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial TouchDeviceType SDL_GetTouchDeviceType(ulong touchId);

	private static Span<nint> SDL_GetTouchFingers(ulong touchId)
	{
		nint result = SDL_GetTouchFingers(touchId, out int count);
		return new Span<nint>((void*) result, count);
	}

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetTouchFingers(ulong touchId, out int count);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_PumpEvents();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_PeepEvents(Span<Event> events, int numevents, EventAction action, uint minType,
		uint maxType);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasEvent(uint type);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HasEvents(uint minType, uint maxType);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_FlushEvent(uint type);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_FlushEvents(uint minType, uint maxType);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_PollEvent(out Event @event);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_WaitEvent(out Event @event);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_WaitEventTimeout(out Event @event, int timeoutMs);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_PushEvent(ref Event @event);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetEventFilter(SdlEventFilter filter, nint userdata);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetEventFilter(out SdlEventFilter filter, out nint userdata);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_AddEventWatch(SdlEventFilter filter, nint userdata);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_RemoveEventWatch(SdlEventFilter filter, nint userdata);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_FilterEvents(SdlEventFilter filter, nint userdata);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetEventEnabled(uint type, SdlBool enabled);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_EventEnabled(uint type);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_RegisterEvents(int numevents);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetWindowFromEvent(ref Event @event);

	// /usr/local/include/SDL3/SDL_filesystem.h

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetBasePath();

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(CallerOwnedStringMarshaller))]
	private static partial string SDL_GetPrefPath(string org, string app);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetUserFolder(Folder folder);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_CreateDirectory(string path);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_EnumerateDirectory(string path, SdlEnumerateDirectoryCallback callback,
		nint userdata);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RemovePath(string path);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenamePath(string oldpath, string newpath);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_CopyFile(string oldpath, string newpath);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetPathInfo(string path, out PathInfo info);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GlobDirectory(string path, string pattern, GlobFlags flags, out int count);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(CallerOwnedStringMarshaller))]
	private static partial string SDL_GetCurrentDirectory();

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GPUSupportsShaderFormats(GpuShaderFormat formatFlags, string name);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GPUSupportsProperties(uint props);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateGPUDevice(GpuShaderFormat formatFlags, SdlBool debugMode, string name);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateGPUDeviceWithProperties(uint props);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DestroyGPUDevice(nint device);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumGPUDrivers();

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetGPUDriver(int index);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetGPUDeviceDriver(nint device);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial GpuShaderFormat SDL_GetGPUShaderFormats(nint device);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateGPUComputePipeline(nint device,
		in GpuComputePipelineCreateInfo createinfo);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateGPUGraphicsPipeline(nint device,
		in GpuGraphicsPipelineCreateInfo createinfo);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateGPUSampler(nint device, in GpuSamplerCreateInfo createinfo);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateGPUShader(nint device, in GpuShaderCreateInfo createinfo);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateGPUTexture(nint device, in GpuTextureCreateInfo createinfo);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateGPUBuffer(nint device, in GpuBufferCreateInfo createinfo);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateGPUTransferBuffer(nint device,
		in GpuTransferBufferCreateInfo createinfo);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetGPUBufferName(nint device, nint buffer, string text);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetGPUTextureName(nint device, nint texture, string text);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_InsertGPUDebugLabel(nint commandBuffer, string text);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_PushGPUDebugGroup(nint commandBuffer, string name);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_PopGPUDebugGroup(nint commandBuffer);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_ReleaseGPUTexture(nint device, nint texture);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_ReleaseGPUSampler(nint device, nint sampler);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_ReleaseGPUBuffer(nint device, nint buffer);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_ReleaseGPUTransferBuffer(nint device, nint transferBuffer);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_ReleaseGPUComputePipeline(nint device, nint computePipeline);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_ReleaseGPUShader(nint device, nint shader);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_ReleaseGPUGraphicsPipeline(nint device, nint graphicsPipeline);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_AcquireGPUCommandBuffer(nint device);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_PushGPUVertexUniformData(nint commandBuffer, uint slotIndex, nint data,
		uint length);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_PushGPUFragmentUniformData(nint commandBuffer, uint slotIndex, nint data,
		uint length);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_PushGPUComputeUniformData(nint commandBuffer, uint slotIndex, nint data,
		uint length);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_BeginGPURenderPass(nint commandBuffer,
		Span<GpuColorTargetInfo> colorTargetInfos, uint numColorTargets,
		in GpuDepthStencilTargetInfo depthStencilTargetInfo);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_BindGPUGraphicsPipeline(nint renderPass, nint graphicsPipeline);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetGPUViewport(nint renderPass, in GpuViewport viewport);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetGPUScissor(nint renderPass, in Rect scissor);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetGPUBlendConstants(nint renderPass, FColor blendConstants);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetGPUStencilReference(nint renderPass, byte reference);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_BindGPUVertexBuffers(nint renderPass, uint firstSlot,
		Span<GpuBufferBinding> bindings, uint numBindings);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_BindGPUIndexBuffer(nint renderPass, in GpuBufferBinding binding,
		GpuIndexElementSize indexElementSize);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_BindGPUVertexSamplers(nint renderPass, uint firstSlot,
		Span<GpuTextureSamplerBinding> textureSamplerBindings, uint numBindings);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_BindGPUVertexStorageTextures(nint renderPass, uint firstSlot,
		Span<nint> storageTextures, uint numBindings);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_BindGPUVertexStorageBuffers(nint renderPass, uint firstSlot,
		Span<nint> storageBuffers, uint numBindings);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_BindGPUFragmentSamplers(nint renderPass, uint firstSlot,
		Span<GpuTextureSamplerBinding> textureSamplerBindings, uint numBindings);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_BindGPUFragmentStorageTextures(nint renderPass, uint firstSlot,
		Span<nint> storageTextures, uint numBindings);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_BindGPUFragmentStorageBuffers(nint renderPass, uint firstSlot,
		Span<nint> storageBuffers, uint numBindings);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DrawGPUIndexedPrimitives(nint renderPass, uint numIndices, uint numInstances,
		uint firstIndex, int vertexOffset, uint firstInstance);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DrawGPUPrimitives(nint renderPass, uint numVertices, uint numInstances,
		uint firstVertex, uint firstInstance);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DrawGPUPrimitivesIndirect(nint renderPass, nint buffer, uint offset,
		uint drawCount);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DrawGPUIndexedPrimitivesIndirect(nint renderPass, nint buffer, uint offset,
		uint drawCount);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_EndGPURenderPass(nint renderPass);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_BeginGPUComputePass(nint commandBuffer,
		Span<GpuStorageTextureReadWriteBinding> storageTextureBindings, uint numStorageTextureBindings,
		Span<GpuStorageBufferReadWriteBinding> storageBufferBindings, uint numStorageBufferBindings);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_BindGPUComputePipeline(nint computePass, nint computePipeline);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_BindGPUComputeSamplers(nint computePass, uint firstSlot,
		Span<GpuTextureSamplerBinding> textureSamplerBindings, uint numBindings);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_BindGPUComputeStorageTextures(nint computePass, uint firstSlot,
		Span<nint> storageTextures, uint numBindings);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_BindGPUComputeStorageBuffers(nint computePass, uint firstSlot,
		Span<nint> storageBuffers, uint numBindings);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DispatchGPUCompute(nint computePass, uint groupcountX, uint groupcountY,
		uint groupcountZ);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DispatchGPUComputeIndirect(nint computePass, nint buffer, uint offset);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_EndGPUComputePass(nint computePass);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_MapGPUTransferBuffer(nint device, nint transferBuffer, SdlBool cycle);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_UnmapGPUTransferBuffer(nint device, nint transferBuffer);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_BeginGPUCopyPass(nint commandBuffer);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_UploadToGPUTexture(nint copyPass, in GpuTextureTransferInfo source,
		in GpuTextureRegion destination, SdlBool cycle);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_UploadToGPUBuffer(nint copyPass, in GpuTransferBufferLocation source,
		in GpuBufferRegion destination, SdlBool cycle);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_CopyGPUTextureToTexture(nint copyPass, in GpuTextureLocation source,
		in GpuTextureLocation destination, uint w, uint h, uint d, SdlBool cycle);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_CopyGPUBufferToBuffer(nint copyPass, in GpuBufferLocation source,
		in GpuBufferLocation destination, uint size, SdlBool cycle);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DownloadFromGPUTexture(nint copyPass, in GpuTextureRegion source,
		in GpuTextureTransferInfo destination);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DownloadFromGPUBuffer(nint copyPass, in GpuBufferRegion source,
		in GpuTransferBufferLocation destination);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_EndGPUCopyPass(nint copyPass);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_GenerateMipmapsForGPUTexture(nint commandBuffer, nint texture);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_BlitGPUTexture(nint commandBuffer, in GpuBlitInfo info);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_WindowSupportsGPUSwapchainComposition(nint device, nint window,
		GpuSwapchainComposition swapchainComposition);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_WindowSupportsGPUPresentMode(nint device, nint window,
		GpuPresentMode presentMode);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ClaimWindowForGPUDevice(nint device, nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_ReleaseWindowFromGPUDevice(nint device, nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetGPUSwapchainParameters(nint device, nint window,
		GpuSwapchainComposition swapchainComposition, GpuPresentMode presentMode);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetGPUAllowedFramesInFlight(nint device, uint allowedFramesInFlight);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial GpuTextureFormat SDL_GetGPUSwapchainTextureFormat(nint device, nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_AcquireGPUSwapchainTexture(nint commandBuffer, nint window,
		out nint swapchainTexture, out uint swapchainTextureWidth, out uint swapchainTextureHeight);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_WaitForGPUSwapchain(nint device, nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_WaitAndAcquireGPUSwapchainTexture(nint commandBuffer, nint window,
		out nint swapchainTexture, out uint swapchainTextureWidth, out uint swapchainTextureHeight);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SubmitGPUCommandBuffer(nint commandBuffer);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_SubmitGPUCommandBufferAndAcquireFence(nint commandBuffer);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_CancelGPUCommandBuffer(nint commandBuffer);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_WaitForGPUIdle(nint device);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_WaitForGPUFences(nint device, SdlBool waitAll, Span<nint> fences,
		uint numFences);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_QueryGPUFence(nint device, nint fence);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_ReleaseGPUFence(nint device, nint fence);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GPUTextureFormatTexelBlockSize(GpuTextureFormat format);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GPUTextureSupportsFormat(nint device, GpuTextureFormat format,
		GpuTextureType type, GpuTextureUsageFlags usage);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GPUTextureSupportsSampleCount(nint device, GpuTextureFormat format,
		GpuSampleCount sampleCount);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_CalculateGPUTextureFormatSize(GpuTextureFormat format, uint width, uint height,
		uint depthOrLayerCount);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetHaptics(out int count);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetHapticNameForID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_OpenHaptic(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetHapticFromID(uint instanceId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetHapticID(nint haptic);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetHapticName(nint haptic);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_IsMouseHaptic();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_OpenHapticFromMouse();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_IsJoystickHaptic(nint joystick);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_OpenHapticFromJoystick(nint joystick);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_CloseHaptic(nint haptic);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetMaxHapticEffects(nint haptic);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetMaxHapticEffectsPlaying(nint haptic);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetHapticFeatures(nint haptic);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumHapticAxes(nint haptic);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HapticEffectSupported(nint haptic, ref HapticEffect effect);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_CreateHapticEffect(nint haptic, ref HapticEffect effect);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_UpdateHapticEffect(nint haptic, int effect, ref HapticEffect data);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RunHapticEffect(nint haptic, int effect, uint iterations);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_StopHapticEffect(nint haptic, int effect);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DestroyHapticEffect(nint haptic, int effect);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetHapticEffectStatus(nint haptic, int effect);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetHapticGain(nint haptic, int gain);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetHapticAutocenter(nint haptic, int autocenter);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_PauseHaptic(nint haptic);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ResumeHaptic(nint haptic);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_StopHapticEffects(nint haptic);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_HapticRumbleSupported(nint haptic);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_InitHapticRumble(nint haptic);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_PlayHapticRumble(nint haptic, float strength, uint length);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_StopHapticRumble(nint haptic);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_hid_init();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_hid_exit();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_hid_device_change_count();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial HidDeviceInfo* SDL_hid_enumerate(ushort vendorId, ushort productId);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_hid_free_enumeration(nint devs); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_hid_open(ushort vendorId, ushort productId, string serialNumber);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_hid_open_path(string path);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_hid_write(nint dev, nint data, nuint length); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int
		SDL_hid_read_timeout(nint dev, nint data, nuint length,
			int milliseconds); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_hid_read(nint dev, nint data, nuint length); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_hid_set_nonblocking(nint dev, int nonblock);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int
		SDL_hid_send_feature_report(nint dev, nint data, nuint length); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int
		SDL_hid_get_feature_report(nint dev, nint data, nuint length); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int
		SDL_hid_get_input_report(nint dev, nint data, nuint length); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_hid_close(nint dev);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_hid_get_manufacturer_string(nint dev, string @string, nuint maxlen);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_hid_get_product_string(nint dev, string @string, nuint maxlen);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_hid_get_serial_number_string(nint dev, string @string, nuint maxlen);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_hid_get_indexed_string(nint dev, int stringIndex, string @string, nuint maxlen);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial HidDeviceInfo* SDL_hid_get_device_info(nint dev);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int
		SDL_hid_get_report_descriptor(nint dev, nint buf, nuint bufSize); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_hid_ble_scan(SdlBool active);

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

	private static Span<nint> SDL_GetPreferredLocales()
	{
		nint result = SDL_GetPreferredLocales(out int count);
		return new Span<nint>((void*) result, count);
	}

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetPreferredLocales(out int count);

	

	

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ShowMessageBox(ref MessageBoxData messageboxdata, out int buttonid);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ShowSimpleMessageBox(MessageBoxFlags flags, string title, string message,
		nint window);

	// /usr/local/include/SDL3/SDL_metal.h

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_Metal_CreateView(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_Metal_DestroyView(nint view);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_Metal_GetLayer(nint view);

	// /usr/local/include/SDL3/SDL_misc.h

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_OpenURL(string url);

	// /usr/local/include/SDL3/SDL_platform.h

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetPlatform();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateProcess(nint args, SdlBool pipeStdio);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateProcessWithProperties(uint props);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetProcessProperties(nint process);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_ReadProcess(nint process, out nuint datasize, out int exitcode);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetProcessInput(nint process);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetProcessOutput(nint process);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_KillProcess(nint process, SdlBool force);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_WaitProcess(nint process, SdlBool block, out int exitcode);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DestroyProcess(nint process);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumRenderDrivers();

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetRenderDriver(int index);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_CreateWindowAndRenderer(string title, int width, int height,
		WindowFlags windowFlags, out nint window, out nint renderer);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateRenderer(nint window, string name);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateRendererWithProperties(uint props);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateSoftwareRenderer(nint surface);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetRenderer(nint window);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetRenderWindow(nint renderer);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetRendererName(nint renderer);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetRendererProperties(nint renderer);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetRenderOutputSize(nint renderer, out int w, out int h);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetCurrentRenderOutputSize(nint renderer, out int w, out int h);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial Texture* SDL_CreateTexture(nint renderer, PixelFormat format, TextureAccess access,
		int w, int h);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial Texture* SDL_CreateTextureFromSurface(nint renderer, nint surface);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial Texture* SDL_CreateTextureWithProperties(nint renderer, uint props);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetTextureProperties(nint texture); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetRendererFromTexture(nint texture); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool
		SDL_GetTextureSize(nint texture, out float w, out float h); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool
		SDL_SetTextureColorMod(nint texture, byte r, byte g, byte b); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool
		SDL_SetTextureColorModFloat(nint texture, float r, float g, float b); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool
		SDL_GetTextureColorMod(nint texture, out byte r, out byte g, out byte b); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool
		SDL_GetTextureColorModFloat(nint texture, out float r, out float g,
			out float b); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetTextureAlphaMod(nint texture, byte alpha); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool
		SDL_SetTextureAlphaModFloat(nint texture, float alpha); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool
		SDL_GetTextureAlphaMod(nint texture, out byte alpha); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool
		SDL_GetTextureAlphaModFloat(nint texture, out float alpha); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool
		SDL_SetTextureBlendMode(nint texture, uint blendMode); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool
		SDL_GetTextureBlendMode(nint texture, nint blendMode); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool
		SDL_SetTextureScaleMode(nint texture, ScaleMode scaleMode); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool
		SDL_GetTextureScaleMode(nint texture, out ScaleMode scaleMode); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool
		SDL_UpdateTexture(nint texture, ref Rect rect, nint pixels, int pitch); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_UpdateYUVTexture(nint texture, ref Rect rect, nint yplane, int ypitch,
		nint uplane, int upitch, nint vplane, int vpitch); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_UpdateNVTexture(nint texture, ref Rect rect, nint yplane, int ypitch,
		nint uVplane, int uVpitch); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool
		SDL_LockTexture(nint texture, ref Rect rect, out nint pixels,
			out int pitch); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool
		SDL_LockTextureToSurface(nint texture, ref Rect rect,
			out nint surface); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_UnlockTexture(nint texture); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool
		SDL_SetRenderTarget(nint renderer, nint texture); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial Texture* SDL_GetRenderTarget(nint renderer);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetRenderLogicalPresentation(nint renderer, int w, int h,
		RendererLogicalPresentation mode);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetRenderLogicalPresentation(nint renderer, out int w, out int h,
		out RendererLogicalPresentation mode);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetRenderLogicalPresentationRect(nint renderer, out FRect rect);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenderCoordinatesFromWindow(nint renderer, float windowX, float windowY,
		out float x, out float y);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenderCoordinatesToWindow(nint renderer, float x, float y, out float windowX,
		out float windowY);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ConvertEventToRenderCoordinates(nint renderer, ref Event @event);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetRenderViewport(nint renderer, ref Rect rect);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetRenderViewport(nint renderer, out Rect rect);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenderViewportSet(nint renderer);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetRenderSafeArea(nint renderer, out Rect rect);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetRenderClipRect(nint renderer, ref Rect rect);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetRenderClipRect(nint renderer, out Rect rect);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenderClipEnabled(nint renderer);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetRenderScale(nint renderer, float scaleX, float scaleY);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetRenderScale(nint renderer, out float scaleX, out float scaleY);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetRenderDrawColor(nint renderer, byte r, byte g, byte b, byte a);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetRenderDrawColorFloat(nint renderer, float r, float g, float b, float a);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetRenderDrawColor(nint renderer, out byte r, out byte g, out byte b,
		out byte a);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetRenderDrawColorFloat(nint renderer, out float r, out float g, out float b,
		out float a);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetRenderColorScale(nint renderer, float scale);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetRenderColorScale(nint renderer, out float scale);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetRenderDrawBlendMode(nint renderer, uint blendMode);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetRenderDrawBlendMode(nint renderer, nint blendMode);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenderClear(nint renderer);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenderPoint(nint renderer, float x, float y);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenderPoints(nint renderer, Span<FPoint> points, int count);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenderLine(nint renderer, float x1, float y1, float x2, float y2);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenderLines(nint renderer, Span<FPoint> points, int count);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenderRect(nint renderer, ref FRect rect);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenderRects(nint renderer, Span<FRect> rects, int count);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenderFillRect(nint renderer, ref FRect rect);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenderFillRects(nint renderer, Span<FRect> rects, int count);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenderTexture(nint renderer, nint texture, ref FRect srcrect,
		ref FRect dstrect); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenderTextureRotated(nint renderer, nint texture, ref FRect srcrect,
		ref FRect dstrect, double angle, ref FPoint center, FlipMode flip); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenderTextureAffine(nint renderer, nint texture, in FRect srcrect,
		in FPoint origin, in FPoint right, in FPoint down);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenderTextureTiled(nint renderer, nint texture, ref FRect srcrect,
		float scale, ref FRect dstrect); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenderTexture9Grid(nint renderer, nint texture, ref FRect srcrect,
		float leftWidth, float rightWidth, float topHeight, float bottomHeight, float scale,
		ref FRect dstrect); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenderGeometry(nint renderer, nint texture, Span<Vertex> vertices,
		int numVertices, Span<int> indices, int numIndices); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenderGeometryRaw(nint renderer, nint texture, nint xy, int xyStride,
		nint color, int colorStride, nint uv, int uvStride, int numVertices, nint indices, int numIndices,
		int sizeIndices); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial Surface* SDL_RenderReadPixels(nint renderer, ref Rect rect);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenderPresent(nint renderer);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DestroyTexture(nint texture); // WARN_UNKNOWN_POINTER_PARAMETER

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DestroyRenderer(nint renderer);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_FlushRenderer(nint renderer);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetRenderMetalLayer(nint renderer);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetRenderMetalCommandEncoder(nint renderer);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_AddVulkanRenderSemaphores(nint renderer, uint waitStageMask, long waitSemaphore,
		long signalSemaphore);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_SetRenderVSync(nint renderer, int vsync);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetRenderVSync(nint renderer, out int vsync);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenderDebugText(nint renderer, float x, float y, string str);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenderDebugTextFormat(nint renderer, float x, float y, string fmt);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_OpenTitleStorage(string @override, uint props);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_OpenUserStorage(string org, string app, uint props);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_OpenFileStorage(string path);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_OpenStorage(ref StorageInterface iface, nint userdata);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_CloseStorage(nint storage);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_StorageReady(nint storage);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetStorageFileSize(nint storage, string path, out ulong length);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_ReadStorageFile(nint storage, string path, nint destination, ulong length);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_WriteStorageFile(nint storage, string path, nint source, ulong length);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_CreateStorageDirectory(nint storage, string path);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_EnumerateStorageDirectory(nint storage, string path,
		SdlEnumerateDirectoryCallback callback, nint userdata);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RemoveStoragePath(nint storage, string path);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RenameStoragePath(nint storage, string oldpath, string newpath);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_CopyStorageFile(nint storage, string oldpath, string newpath);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetStoragePathInfo(nint storage, string path, out PathInfo info);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ulong SDL_GetStorageSpaceRemaining(nint storage);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GlobStorageDirectory(nint storage, string path, string pattern,
		GlobFlags flags, out int count);

	// /usr/local/include/SDL3/SDL_system.h

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_IsTablet();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_IsTV();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial Sandbox SDL_GetSandbox();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_OnApplicationWillTerminate();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_OnApplicationDidReceiveMemoryWarning();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_OnApplicationWillEnterBackground();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_OnApplicationDidEnterBackground();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_OnApplicationWillEnterForeground();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_OnApplicationDidEnterForeground();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetDateTimeLocalePreferences(out DateFormat dateFormat,
		out TimeFormat timeFormat);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetCurrentTime(nint ticks);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_TimeToDateTime(long ticks, out SDL3.Structs.DateTime dt, SdlBool localTime);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_DateTimeToTime(ref SDL3.Structs.DateTime dt, nint ticks);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_TimeToWindows(long ticks, out uint dwLowDateTime, out uint dwHighDateTime);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial long SDL_TimeFromWindows(uint dwLowDateTime, uint dwHighDateTime);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetDaysInMonth(int year, int month);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetDayOfYear(int year, int month, int day);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetDayOfWeek(int year, int month, int day);

	// /usr/local/include/SDL3/SDL_timer.h

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ulong SDL_GetTicks();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ulong SDL_GetTicksNS();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ulong SDL_GetPerformanceCounter();

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ulong SDL_GetPerformanceFrequency();

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
	private static partial uint SDL_AddTimer(uint interval, SdlTimerCallback callback, nint userdata);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_AddTimerNS(ulong interval, SdlNsTimerCallback callback, nint userdata);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_RemoveTimer(uint id);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateTray(nint icon, string tooltip);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetTrayIcon(nint tray, nint icon);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetTrayTooltip(nint tray, string tooltip);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateTrayMenu(nint tray);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_CreateTraySubmenu(nint entry);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetTrayMenu(nint tray);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetTraySubmenu(nint entry);

	private static Span<nint> GetTrayEntries(nint menu)
	{
		nint result = SDL_GetTrayEntries(menu, out int size);
		return new Span<nint>((void*) result, size);
	}

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetTrayEntries(nint menu, out int size);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_RemoveTrayEntry(nint entry);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_InsertTrayEntryAt(nint menu, int pos, string label, TrayEntryFlags flags);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetTrayEntryLabel(nint entry, string label);

	[LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(OwnedStringMarshaller))]
	private static partial string SDL_GetTrayEntryLabel(nint entry);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetTrayEntryChecked(nint entry, SdlBool check);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetTrayEntryChecked(nint entry);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetTrayEntryEnabled(nint entry, SdlBool enabled);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial SdlBool SDL_GetTrayEntryEnabled(nint entry);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetTrayEntryCallback(nint entry, SdlTrayCallback callback, nint userdata);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_ClickTrayEntry(nint entry);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_DestroyTray(nint tray);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetTrayEntryParent(nint entry);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetTrayMenuParentEntry(nint menu);

	[LibraryImport(NativeLibName)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial nint SDL_GetTrayMenuParentTray(nint menu);

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