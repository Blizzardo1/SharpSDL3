using SDL3.Enums;
using SDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;
using static SDL3.Delegates;
using static SDL3.Sdl;

namespace SDL3; 
public static unsafe partial class Audio {

    public static SdlBool AudioDevicePaused(uint dev) {
        SdlBool result = SDL_AudioDevicePaused(dev);
        if (!result) {
            Logger.LogError(LogCategory.System, "AudioDevicePaused: Failed to check if audio device is paused.");
            throw new InvalidOperationException("SDL_AudioDevicePaused failed.");
        }
        return result;
    }

    public static SdlBool AudioStreamDevicePaused(nint stream) {
        SdlBool result = SDL_AudioStreamDevicePaused(stream);
        if (!result) {
            Logger.LogError(LogCategory.System, "AudioStreamDevicePaused: Failed to check if audio stream device is paused.");
            throw new InvalidOperationException("SDL_AudioStreamDevicePaused failed.");
        }
        return result;
    }

    public static SdlBool BindAudioStream(uint devid, nint stream) {
        SdlBool result = SDL_BindAudioStream(devid, stream);
        if (!result) {
            Logger.LogError(LogCategory.System, "BindAudioStream: Failed to bind audio stream.");
            throw new InvalidOperationException("SDL_BindAudioStream failed.");
        }
        return result;
    }

    public static SdlBool BindAudioStreams(uint devid, Span<nint> streams) {
        SdlBool result = SDL_BindAudioStreams(devid, streams, streams.Length);
        if (!result) {
            Logger.LogError(LogCategory.System, "BindAudioStreams: Failed to bind audio streams.");
            throw new InvalidOperationException("SDL_BindAudioStreams failed.");
        }
        return result;
    }

    public static SdlBool ClearAudioStream(nint stream) {
        SdlBool result = SDL_ClearAudioStream(stream);
        if (!result) {
            Logger.LogError(LogCategory.System, "ClearAudioStream: Failed to clear audio stream.");
            throw new InvalidOperationException("SDL_ClearAudioStream failed.");
        }
        return result;
    }

    public static void CloseAudioDevice(uint devid) {
        try {
            // Log the action for debugging purposes
            Logger.LogInfo(LogCategory.System, $"Closing audio device with ID: {devid}");

            // Call the native method to close the audio device
            SDL_CloseAudioDevice(devid);

            // Log success
            Logger.LogInfo(LogCategory.System, $"Successfully closed audio device with ID: {devid}");
        } catch (Exception ex) {
            // Log any unexpected errors
            Logger.LogError(LogCategory.System, $"Error while closing audio device with ID: {devid}. Exception: {ex.Message}");
            throw;
        }
    }

    public static SdlBool ConvertAudioSamples(ref AudioSpec srcSpec, nint srcData, int srcLen,
        ref AudioSpec dstSpec, nint dstData, out int dstLen) {
        SdlBool result = SDL_ConvertAudioSamples(ref srcSpec, srcData, srcLen, ref dstSpec, dstData, out dstLen);
        if (!result) {
            Logger.LogError(LogCategory.System, "ConvertAudioSamples: Failed to convert audio samples.");
            throw new InvalidOperationException("SDL_ConvertAudioSamples failed.");
        }
        return result;
    }

    public static nint CreateAudioStream(ref AudioSpec srcSpec, ref AudioSpec dstSpec) {
        nint result = SDL_CreateAudioStream(ref srcSpec, ref dstSpec);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.System, "CreateAudioStream: Failed to create audio stream.");
            throw new InvalidOperationException("SDL_CreateAudioStream failed.");
        }
        return result;
    }

    public static void DestroyAudioStream(nint stream) {
        try {
            // Log the action for debugging purposes
            Logger.LogInfo(LogCategory.System, $"Destroying audio stream with handle: {stream}");
            // Call the native method to destroy the audio stream
            SDL_DestroyAudioStream(stream);
            // Log success
            Logger.LogInfo(LogCategory.System, $"Successfully destroyed audio stream with handle: {stream}");
        } catch (Exception ex) {
            // Log any unexpected errors
            Logger.LogError(LogCategory.System, $"Error while destroying audio stream with handle: {stream}. Exception: {ex.Message}");
            throw;
        }
    }

    public static SdlBool FlushAudioStream(nint stream) {
        SdlBool result = SDL_FlushAudioStream(stream);
        if (!result) {
            Logger.LogError(LogCategory.System, "FlushAudioStream: Failed to flush audio stream.");
            throw new InvalidOperationException("SDL_FlushAudioStream failed.");
        }
        return result;
    }

    public static Span<int> GetAudioDeviceChannelMap(uint devid) {
        nint result = SDL_GetAudioDeviceChannelMap(devid, out int count);
        return new Span<int>((void*)result, count);
    }

    public static SdlBool GetAudioDeviceFormat(uint devId, out AudioSpec spec, out int sampleFrames) {
        SdlBool result = SDL_GetAudioDeviceFormat(devId, out spec, out sampleFrames);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetAudioDeviceFormat: Failed to get audio device format.");
            throw new InvalidOperationException("SDL_GetAudioDeviceFormat failed.");
        }
        return result;
    }

    public static float GetAudioDeviceGain(uint devid) {
        float result = SDL_GetAudioDeviceGain(devid);
        if (result < 0) {
            Logger.LogError(LogCategory.System, "GetAudioDeviceGain: Failed to get audio device gain.");
            throw new InvalidOperationException("SDL_GetAudioDeviceGain failed.");
        }
        return result;
    }

    public static string GetAudioDeviceName(uint devId) {
        string name = SDL_GetAudioDeviceName(devId);
        if (string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.System, "GetAudioDeviceName: Failed to get audio device name.");
            throw new InvalidOperationException("SDL_GetAudioDeviceName failed.");
        }
        return name;
    }

    public static string GetAudioDriver(int index) {
        string driver = SDL_GetAudioDriver(index);
        if (string.IsNullOrEmpty(driver)) {
            Logger.LogError(LogCategory.System, "GetAudioDriver: Failed to get audio driver.");
            throw new InvalidOperationException("SDL_GetAudioDriver failed.");
        }
        return driver;
    }

    public static string GetAudioFormatName(AudioFormat format) {
        string name = SDL_GetAudioFormatName(format);
        if (string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.System, "GetAudioFormatName: Failed to get audio format name.");
            throw new InvalidOperationException("SDL_GetAudioFormatName failed.");
        }
        return name;
    }

    public static nint GetAudioPlaybackDevices(out int count) {
        nint result = SDL_GetAudioPlaybackDevices(out count);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetAudioPlaybackDevices: Failed to get audio playback devices.");
            throw new InvalidOperationException("SDL_GetAudioPlaybackDevices failed.");
        }
        return result;
    }

    public static nint GetAudioRecordingDevices(out int count) {
        nint result = SDL_GetAudioRecordingDevices(out count);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetAudioRecordingDevices: Failed to get audio recording devices.");
            throw new InvalidOperationException("SDL_GetAudioRecordingDevices failed.");
        }
        return result;
    }

    public static int GetAudioStreamAvailable(nint stream) {
        int result = SDL_GetAudioStreamAvailable(stream);
        if (result < 0) {
            Logger.LogError(LogCategory.System, "GetAudioStreamAvailable: Failed to get audio stream available.");
            throw new InvalidOperationException("SDL_GetAudioStreamAvailable failed.");
        }
        return result;
    }

    public static int GetAudioStreamData(nint stream, nint buf, int len) {
        int result = SDL_GetAudioStreamData(stream, buf, len);
        if (result < 0) {
            Logger.LogError(LogCategory.System, "GetAudioStreamData: Failed to get audio stream data.");
            throw new InvalidOperationException("SDL_GetAudioStreamData failed.");
        }
        return result;
    }

    public static uint GetAudioStreamDevice(nint stream) {
        uint result = SDL_GetAudioStreamDevice(stream);
        if (result == 0) {
            Logger.LogError(LogCategory.System, "GetAudioStreamDevice: Failed to get audio stream device.");
            throw new InvalidOperationException("SDL_GetAudioStreamDevice failed.");
        }
        return result;
    }

    public static SdlBool GetAudioStreamFormat(nint stream, out AudioSpec srcSpec, out AudioSpec dstSpec) {
        SdlBool result = SDL_GetAudioStreamFormat(stream, out srcSpec, out dstSpec);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetAudioStreamFormat: Failed to get audio stream format.");
            throw new InvalidOperationException("SDL_GetAudioStreamFormat failed.");
        }
        return result;
    }

    public static float GetAudioStreamFrequencyRatio(nint stream) {
        float result = SDL_GetAudioStreamFrequencyRatio(stream);
        if (result < 0) {
            Logger.LogError(LogCategory.System, "GetAudioStreamFrequencyRatio: Failed to get audio stream frequency ratio.");
            throw new InvalidOperationException("SDL_GetAudioStreamFrequencyRatio failed.");
        }
        return result;
    }

    public static float GetAudioStreamGain(nint stream) {
        float result = SDL_GetAudioStreamGain(stream);
        if (result < 0) {
            Logger.LogError(LogCategory.System, "GetAudioStreamGain: Failed to get audio stream gain.");
            throw new InvalidOperationException("SDL_GetAudioStreamGain failed.");
        }
        return result;
    }

    public static Span<int> GetAudioStreamInputChannelMap(nint stream, out int count) {
        nint result = SDL_GetAudioStreamInputChannelMap(stream, out count);
        return new Span<int>((void*)result, count);
    }

    public static Span<int> GetAudioStreamOutputChannelMap(nint stream, out int count) {
        nint result = SDL_GetAudioStreamOutputChannelMap(stream, out count);
        return new Span<int>((void*)result, count);
    }

    public static uint GetAudioStreamProperties(nint stream) {
        uint result = SDL_GetAudioStreamProperties(stream);
        if (result == 0) {
            Logger.LogError(LogCategory.System, "GetAudioStreamProperties: Failed to get audio stream properties.");
            throw new InvalidOperationException("SDL_GetAudioStreamProperties failed.");
        }
        return result;
    }

    public static int GetAudioStreamQueued(nint stream) {
        int result = SDL_GetAudioStreamQueued(stream);
        if (result < 0) {
            Logger.LogError(LogCategory.System, "GetAudioStreamQueued: Failed to get audio stream queued.");
            throw new InvalidOperationException("SDL_GetAudioStreamQueued failed.");
        }
        return result;
    }

    public static string GetCurrentAudioDriver() {
        string driver = SDL_GetCurrentAudioDriver();
        if (string.IsNullOrEmpty(driver)) {
            Logger.LogError(LogCategory.System, "GetCurrentAudioDriver: Failed to get current audio driver.");
            throw new InvalidOperationException("SDL_GetCurrentAudioDriver failed.");
        }
        return driver;
    }

    public static int GetNumAudioDrivers() {
        int numDrivers = SDL_GetNumAudioDrivers();
        if (numDrivers < 0) {
            Logger.LogError(LogCategory.System, "GetNumAudioDrivers: Failed to get number of audio drivers.");
            throw new InvalidOperationException("SDL_GetNumAudioDrivers failed.");
        }
        return numDrivers;
    }

    public static int GetSilenceValueForFormat(AudioFormat format) {
        int silenceValue = SDL_GetSilenceValueForFormat(format);
        if (silenceValue < 0) {
            Logger.LogError(LogCategory.System, "GetSilenceValueForFormat: Failed to get silence value for format.");
            throw new InvalidOperationException("SDL_GetSilenceValueForFormat failed.");
        }
        return silenceValue;
    }

    public static SdlBool IsAudioDevicePhysical(uint devid) {
        SdlBool result = SDL_IsAudioDevicePhysical(devid);
        if (!result) {
            Logger.LogError(LogCategory.System, "IsAudioDevicePhysical: Failed to check if audio device is physical.");
            throw new InvalidOperationException("SDL_IsAudioDevicePhysical failed.");
        }
        return result;
    }

    public static SdlBool IsAudioDevicePlayback(uint devid) {
        SdlBool result = SDL_IsAudioDevicePlayback(devid);
        if (!result) {
            Logger.LogError(LogCategory.System, "IsAudioDevicePlayback: Failed to check if audio device is playback.");
            throw new InvalidOperationException("SDL_IsAudioDevicePlayback failed.");
        }
        return result;
    }

    public static SdlBool LoadWav(string path, out AudioSpec spec,
        out nint audioBuf, out uint audioLen) {
        SdlBool result = SDL_LoadWAV(path, out spec, out audioBuf, out audioLen);
        if (!result) {
            Logger.LogError(LogCategory.System, "LoadWAV: Failed to load WAV.");
            throw new InvalidOperationException("SDL_LoadWAV failed.");
        }
        return result;
    }

    public static SdlBool LoadWavIo(nint src, SdlBool closeio, out AudioSpec spec,
        out nint audioBuf, out uint audioLen) {
        SdlBool result = SDL_LoadWAV_IO(src, closeio, out spec, out audioBuf, out audioLen);
        if (!result) {
            Logger.LogError(LogCategory.System, "LoadWAV_IO: Failed to load WAV IO.");
            throw new InvalidOperationException("SDL_LoadWAV_IO failed.");
        }
        return result;
    }

    public static SdlBool LockAudioStream(nint stream) {
        SdlBool result = SDL_LockAudioStream(stream);
        if (!result) {
            Logger.LogError(LogCategory.System, "LockAudioStream: Failed to lock audio stream.");
            throw new InvalidOperationException("SDL_LockAudioStream failed.");
        }
        return result;
    }

    public static SdlBool MixAudio(nint dst, nint src, AudioFormat format, uint len, float volume) {
        SdlBool result = SDL_MixAudio(dst, src, format, len, volume);
        if (!result) {
            Logger.LogError(LogCategory.System, "MixAudio: Failed to mix audio.");
            throw new InvalidOperationException("SDL_MixAudio failed.");
        }
        return result;
    }

    public static uint OpenAudioDevice(uint devid, ref AudioSpec spec) {
        uint result = SDL_OpenAudioDevice(devid, ref spec);
        if (result == 0) {
            Logger.LogError(LogCategory.System, "OpenAudioDevice: Failed to open audio device.");
            throw new InvalidOperationException("SDL_OpenAudioDevice failed.");
        }
        return result;
    }

    public static nint OpenAudioDeviceStream(uint devid, ref AudioSpec spec,
        SdlAudioStreamCallback callback, nint userdata) {
        nint result = SDL_OpenAudioDeviceStream(devid, ref spec, callback, userdata);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.System, "OpenAudioDeviceStream: Failed to open audio device stream.");
            throw new InvalidOperationException("SDL_OpenAudioDeviceStream failed.");
        }
        return result;
    }

    public static SdlBool PauseAudioDevice(uint dev) {
        SdlBool result = SDL_PauseAudioDevice(dev);
        if (!result) {
            Logger.LogError(LogCategory.System, "PauseAudioDevice: Failed to pause audio device.");
            throw new InvalidOperationException("SDL_PauseAudioDevice failed.");
        }
        return result;
    }

    public static SdlBool PauseAudioStreamDevice(nint stream) {
        SdlBool result = SDL_PauseAudioStreamDevice(stream);
        if (!result) {
            Logger.LogError(LogCategory.System, "PauseAudioStreamDevice: Failed to pause audio stream device.");
            throw new InvalidOperationException("SDL_PauseAudioStreamDevice failed.");
        }
        return result;
    }

    public static SdlBool PutAudioStreamData(nint stream, nint buf, int len) {
        SdlBool result = SDL_PutAudioStreamData(stream, buf, len);
        if (!result) {
            Logger.LogError(LogCategory.System, "PutAudioStreamData: Failed to put audio stream data.");
            throw new InvalidOperationException("SDL_PutAudioStreamData failed.");
        }
        return result;
    }

    public static SdlBool ResumeAudioDevice(uint dev) {
        SdlBool result = SDL_ResumeAudioDevice(dev);
        if (!result) {
            Logger.LogError(LogCategory.System, "ResumeAudioDevice: Failed to resume audio device.");
            throw new InvalidOperationException("SDL_ResumeAudioDevice failed.");
        }
        return result;
    }

    public static SdlBool ResumeAudioStreamDevice(nint stream) {
        SdlBool result = SDL_ResumeAudioStreamDevice(stream);
        if (!result) {
            Logger.LogError(LogCategory.System, "ResumeAudioStreamDevice: Failed to resume audio stream device.");
            throw new InvalidOperationException("SDL_ResumeAudioStreamDevice failed.");
        }
        return result;
    }

    public static SdlBool SetAudioDeviceGain(uint devid, float gain) {
        SdlBool result = SDL_SetAudioDeviceGain(devid, gain);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetAudioDeviceGain: Failed to set audio device gain.");
            throw new InvalidOperationException("SDL_SetAudioDeviceGain failed.");
        }
        return result;
    }

    public static SdlBool SetAudioPostmixCallback(uint devid, SdlAudioPostmixCallback callback, nint userdata) {
        SdlBool result = SDL_SetAudioPostmixCallback(devid, callback, userdata);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetAudioPostmixCallback: Failed to set audio postmix callback.");
            throw new InvalidOperationException("SDL_SetAudioPostmixCallback failed.");
        }
        return result;
    }

    public static SdlBool SetAudioStreamFormat(nint stream, ref AudioSpec srcSpec, ref AudioSpec dstSpec) {
        SdlBool result = SDL_SetAudioStreamFormat(stream, ref srcSpec, ref dstSpec);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetAudioStreamFormat: Failed to set audio stream format.");
            throw new InvalidOperationException("SDL_SetAudioStreamFormat failed.");
        }
        return result;
    }

    public static SdlBool SetAudioStreamFrequencyRatio(nint stream, float ratio) {
        SdlBool result = SDL_SetAudioStreamFrequencyRatio(stream, ratio);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetAudioStreamFrequencyRatio: Failed to set audio stream frequency ratio.");
            throw new InvalidOperationException("SDL_SetAudioStreamFrequencyRatio failed.");
        }
        return result;
    }

    public static SdlBool SetAudioStreamGetCallback(nint stream, SdlAudioStreamCallback callback, nint userdata) {
        SdlBool result = SDL_SetAudioStreamGetCallback(stream, callback, userdata);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetAudioStreamGetCallback: Failed to set audio stream get callback.");
            throw new InvalidOperationException("SDL_SetAudioStreamGetCallback failed.");
        }
        return result;
    }

    public static SdlBool SetAudioStreamInputChannelMap(nint stream, Span<int> chmap, int count) {
        SdlBool result = SDL_SetAudioStreamInputChannelMap(stream, chmap, count);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetAudioStreamInputChannelMap: Failed to set audio stream input channel map.");
            throw new InvalidOperationException("SDL_SetAudioStreamInputChannelMap failed.");
        }
        return result;
    }

    public static SdlBool SetAudioStreamOutputChannelMap(nint stream, Span<int> chmap, int count) {
        SdlBool result = SDL_SetAudioStreamOutputChannelMap(stream, chmap, count);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetAudioStreamOutputChannelMap: Failed to set audio stream output channel map.");
            throw new InvalidOperationException("SDL_SetAudioStreamOutputChannelMap failed.");
        }
        return result;
    }

    public static SdlBool SetAudioStreamPutCallback(nint stream, SdlAudioStreamCallback callback, nint userdata) {
        SdlBool result = SDL_SetAudioStreamPutCallback(stream, callback, userdata);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetAudioStreamPutCallback: Failed to set audio stream put callback.");
            throw new InvalidOperationException("SDL_SetAudioStreamPutCallback failed.");
        }
        return result;
    }

    public static void UnbindAudioStream(nint stream) {
        try {
            // Log the action for debugging purposes
            Logger.LogInfo(LogCategory.System, $"Unbinding audio stream with handle: {stream}");

            // Call the native method to unbind the audio stream
            SDL_UnbindAudioStream(stream);

            // Log success
            Logger.LogInfo(LogCategory.System, $"Successfully unbound audio stream with handle: {stream}");
        } catch (Exception ex) {
            // Log any unexpected errors
            Logger.LogError(LogCategory.System, $"Error while unbinding audio stream with handle: {stream}. Exception: {ex.Message}");
            throw;
        }
    }

    public static void UnbindAudioStreams(Span<nint> streams) {
        SDL_UnbindAudioStreams(streams, streams.Length);
    }

    public static SdlBool UnlockAudioStream(nint stream) {
        SdlBool result = SDL_UnlockAudioStream(stream);
        if (!result) {
            Logger.LogError(LogCategory.System, "UnlockAudioStream: Failed to unlock audio stream.");
            throw new InvalidOperationException("SDL_UnlockAudioStream failed.");
        }
        return result;
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_AudioDevicePaused(uint dev);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_AudioStreamDevicePaused(nint stream);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_BindAudioStream(uint devid, nint stream);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_BindAudioStreams(uint devid, Span<nint> streams, int numStreams);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ClearAudioStream(nint stream);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_CloseAudioDevice(uint devid);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ConvertAudioSamples(ref AudioSpec srcSpec, nint srcData, int srcLen,
        ref AudioSpec dstSpec, nint dstData, out int dstLen);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateAudioStream(ref AudioSpec srcSpec, ref AudioSpec dstSpec);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyAudioStream(nint stream);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_FlushAudioStream(nint stream);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetAudioDeviceChannelMap(uint devid, out int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetAudioDeviceFormat(uint devid, out AudioSpec spec, out int sampleFrames);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetAudioDeviceGain(uint devid);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetAudioDeviceName(uint devid);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetAudioDriver(int index);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetAudioFormatName(AudioFormat format);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetAudioPlaybackDevices(out int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetAudioRecordingDevices(out int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetAudioStreamAvailable(nint stream);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetAudioStreamData(nint stream, nint buf, int len);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetAudioStreamDevice(nint stream);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetAudioStreamFormat(nint stream, out AudioSpec srcSpec,
        out AudioSpec dstSpec);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetAudioStreamFrequencyRatio(nint stream);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetAudioStreamGain(nint stream);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetAudioStreamInputChannelMap(nint stream, out int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetAudioStreamOutputChannelMap(nint stream, out int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetAudioStreamProperties(nint stream);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetAudioStreamQueued(nint stream);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetCurrentAudioDriver();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumAudioDrivers();
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetSilenceValueForFormat(AudioFormat format);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_IsAudioDevicePhysical(uint devid);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_IsAudioDevicePlayback(uint devid);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_LoadWAV(string path, out AudioSpec spec, out nint audioBuf,
        out uint audioLen);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_LoadWAV_IO(nint src, SdlBool closeio, out AudioSpec spec,
        out nint audioBuf, out uint audioLen);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_LockAudioStream(nint stream);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_MixAudio(nint dst, nint src, AudioFormat format, uint len, float volume);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_OpenAudioDevice(uint devid, ref AudioSpec spec);
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenAudioDeviceStream(uint devid, ref AudioSpec spec,
        SdlAudioStreamCallback callback, nint userdata);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_PauseAudioDevice(uint dev);
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_PauseAudioStreamDevice(nint stream);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_PutAudioStreamData(nint stream, nint buf, int len);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ResumeAudioDevice(uint dev);
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ResumeAudioStreamDevice(nint stream);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAudioDeviceGain(uint devid, float gain);
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAudioPostmixCallback(uint devid, SdlAudioPostmixCallback callback,
        nint userdata);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAudioStreamFormat(nint stream, ref AudioSpec srcSpec,
        ref AudioSpec dstSpec);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAudioStreamFrequencyRatio(nint stream, float ratio);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAudioStreamGain(nint stream, float gain);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAudioStreamGetCallback(nint stream, SdlAudioStreamCallback callback,
        nint userdata);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAudioStreamInputChannelMap(nint stream, Span<int> chmap, int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAudioStreamOutputChannelMap(nint stream, Span<int> chmap, int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAudioStreamPutCallback(nint stream, SdlAudioStreamCallback callback,
        nint userdata);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnbindAudioStream(nint stream);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnbindAudioStreams(Span<nint> streams, int numStreams);
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_UnlockAudioStream(nint stream);
}
