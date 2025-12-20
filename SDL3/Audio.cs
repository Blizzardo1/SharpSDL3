using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using static SharpSDL3.Delegates;

namespace SharpSDL3;

public static unsafe partial class Sdl {
    /// <summary>Use this function to query if an audio device is paused.</summary>

    /// <param name="deviceId">a device opened by <see cref="OpenAudioDevice" />.</param>
    /// <remarks>
    /// Unlike in SDL2, audio devices start in an unpaused state, since an app
    /// has to bind a stream before any audio will flow.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PauseAudioDevice" />
    /// <seealso cref="ResumeAudioDevice" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if device is valid and paused, <see langword="false" /> otherwise.</returns>
    public static bool AudioDevicePaused(uint deviceId) {
        bool result = SDL_AudioDevicePaused(deviceId);
        if (!result) {
            LogError(LogCategory.Error, "AudioDevicePaused: Failed to check if audio device is paused.");
        }
        return result;
    }

    /// <summary>Use this function to query if an audio device associated with a stream is paused.</summary>
    /// <param name="stream">the audio stream associated with the audio device to query.</param>
    /// <remarks>
    /// Unlike in SDL2, audio devices start in an unpaused state, since an app
    /// has to bind a stream before any audio will flow.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PauseAudioStreamDevice" />
    /// <seealso cref="ResumeAudioStreamDevice" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if device is valid and paused, <see langword="false" /> otherwise.</returns>
    public static bool AudioStreamDevicePaused(nint stream) {
        bool result = SDL_AudioStreamDevicePaused(stream);
        if (!result) {
            LogError(LogCategory.Error, "AudioStreamDevicePaused: Failed to check if audio stream device is paused.");
        }
        return result;
    }

    /// <summary>Bind a single audio stream to an audio device.</summary>
    /// <param name="deviceId">an audio device to bind a stream to.</param>
    /// <param name="stream">an audio stream to bind to a device.</param>
    /// <remarks>
    /// This is a convenience function, equivalent to calling BindAudioStreams(deviceId, &amp;stream, 1).
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BindAudioStreams" />
    /// <seealso cref="UnbindAudioStream" />
    /// <seealso cref="GetAudioStreamDevice" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool BindAudioStream(uint deviceId, nint stream) {
        bool result = SDL_BindAudioStream(deviceId, stream);
        if (!result) {
            LogError(LogCategory.Error, "BindAudioStream: Failed to bind audio stream.");
        }
        return result;
    }

    /// <summary>Bind a list of audio streams to an audio device.</summary>
    /// <param name="deviceId">an audio device to bind a stream to.</param>
    /// <param name="streams">an array of audio streams to bind.</param>
    /// <remarks>
    /// <para>Audio data will flow through any bound streams.</para>
    /// <para>For a playback device, data for all bound streams will be mixed together and fed to the device.</para>
    /// <para>For a recording device, a copy of recorded data will be provided to each bound stream.</para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BindAudioStreams" />
    /// <seealso cref="UnbindAudioStream" />
    /// <seealso cref="GetAudioStreamDevice" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool BindAudioStreams(uint deviceId, Span<nint> streams) {
        bool result = SDL_BindAudioStreams(deviceId, streams, streams.Length);
        if (!result) {
            LogError(LogCategory.Error, "BindAudioStreams: Failed to bind audio streams.");
        }
        return result;
    }

    /// <summary>Clear any pending data in the stream.</summary>
    /// <param name="stream">the audio stream to clear.</param>
    /// <remarks>
    /// This drops any queued data, so there will be nothing to read from the stream until more is added.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAudioStreamAvailable" />
    /// <seealso cref="GetAudioStreamData" />
    /// <seealso cref="GetAudioStreamQueued" />
    /// <seealso cref="PutAudioStreamData" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool ClearAudioStream(nint stream) {
        bool result = SDL_ClearAudioStream(stream);
        if (!result) {
            LogError(LogCategory.Error, "ClearAudioStream: Failed to clear audio stream.");
        }
        return result;
    }

    /// <summary>Close a previously-opened audio device.</summary>
    /// <param name="deviceId">an audio device id previously returned by <see cref="OpenAudioDevice" />.</param>
    /// <remarks>
    /// The application should close open audio devices once they are no longer needed.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="OpenAudioDevice" />
    /// </remarks>
    public static void CloseAudioDevice(uint deviceId) {
        // Log the action for debugging purposes
        LogInfo(LogCategory.System, $"Closing audio device with ID: {deviceId}");

        SDL_CloseAudioDevice(deviceId);

        LogInfo(LogCategory.System, $"Successfully closed audio device with ID: {deviceId}");
    }

    /// <summary>Convert some audio data of one format to another format.</summary>
    /// <param name="srcSpec">the format details of the input audio.</param>
    /// <param name="srcData">the audio data to be converted.</param>
    /// <param name="srcLen">the length of <paramref name="srcData" />.</param>
    /// <param name="dstSpec">the format details of the output audio.</param>
    /// <param name="dstData">will be filled with a pointer to converted audio data, which should be freed with <see cref="Free" />. On error, it will be <see langword="null" />.</param>
    /// <param name="dstLen">will be filled with the len of dst_data.</param>
    /// <remarks>
    /// <para>Please note that this function is for convenience, but should not be used to resample audio in blocks, as it will introduce audio artifacts on the boundaries.</para>
    /// <para>You should only use this function if you are converting audio data in its entirety in one call. If you want to convert audio in smaller chunks, use an SDL_AudioStream, which is designed for this situation.</para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool ConvertAudioSamples(ref AudioSpec srcSpec, nint srcData, int srcLen,
            ref AudioSpec dstSpec, nint dstData, out int dstLen) {
        bool result = SDL_ConvertAudioSamples(ref srcSpec, srcData, srcLen, ref dstSpec, dstData, out dstLen);
        if (!result) {
            LogError(LogCategory.Error, "ConvertAudioSamples: Failed to convert audio samples.");
        }
        return result;
    }

    /// <summary>Create a new audio stream.</summary>
    /// <param name="srcSpec">the format details of the input audio.</param>
    /// <param name="dstSpec">the format details of the output audio.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PutAudioStreamData" />
    /// <seealso cref="GetAudioStreamData" />
    /// <seealso cref="GetAudioStreamAvailable" />
    /// <seealso cref="FlushAudioStream" />
    /// <seealso cref="ClearAudioStream" />
    /// <seealso cref="SetAudioStreamFormat" />
    /// <seealso cref="DestroyAudioStream" />
    /// </remarks>
    /// <returns>(SDL_AudioStream *) Returns a new audio stream on success or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static nint CreateAudioStream(ref AudioSpec srcSpec, ref AudioSpec dstSpec) {
        nint result = SDL_CreateAudioStream(ref srcSpec, ref dstSpec);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "CreateAudioStream: Failed to create audio stream.");
        }
        return result;
    }

    /// <summary>Free an audio stream.</summary>
    /// <param name="stream">the audio stream to destroy.</param>
    /// <remarks>
    /// <para>This will release all allocated data, including any audio that is still queued.</para>
    /// <para>You do not need to manually clear the stream first.</para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateAudioStream" />
    /// </remarks>
    public static void DestroyAudioStream(nint stream) {
        // Log the action for debugging purposes
        LogInfo(LogCategory.System, $"Destroying audio stream with handle: {stream}");
        SDL_DestroyAudioStream(stream);
        LogInfo(LogCategory.System, $"Successfully destroyed audio stream with handle: {stream}");
    }

    /// <summary>Tell the stream that you're done sending data, and anything being buffered should be converted/resampled and made available immediately.</summary>
    /// <param name="stream">the audio stream to flush.</param>
    /// <remarks>
    /// It is legal to add more data to a stream after flushing, but there may be
    /// audio gaps in the output. Generally this is intended to signal the end of
    /// input, so the complete output becomes available.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PutAudioStreamData" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool FlushAudioStream(nint stream) {
        bool result = SDL_FlushAudioStream(stream);
        if (!result) {
            LogError(LogCategory.Error, "FlushAudioStream: Failed to flush audio stream.");
        }
        return result;
    }

    /// <summary>Get the current channel map of an audio device.</summary>
    /// <param name="deviceId">the instance ID of the device to query.</param>
    /// <remarks>
    /// Channel maps are optional; most things do not need them, instead passing data in the order that SDL expects.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAudioStreamInputChannelMap" />
    /// </remarks>
    /// <returns>(int *) Returns an array of the current channel mapping, with as many elements as the current output spec's channels, or <see langword="null" /> if default. This should be freed with <see cref="Free" /> when it is no longer needed.</returns>
    public static int[] GetAudioDeviceChannelMap(uint deviceId) {
        nint result = SDL_GetAudioDeviceChannelMap(deviceId, out int count);

        if (result == nint.Zero) {
            LogError(LogCategory.Error, "GetAudioDeviceChannelMap: Failed to get audio device channel map.");
            return [];
        }

        switch (count) {
            case < 0:
                LogError(LogCategory.Error, "GetAudioDeviceChannelMap: Invalid channel map count.");
                break;
            case 0:
                LogError(LogCategory.Error, "GetAudioDeviceChannelMap: No channels available.");
                return [];
        }

        int[] map = new int[count];

        for (int i = 0; i < count; i++) {
            map[i] = Marshal.ReadInt32(result, i * sizeof(int));
        }

        return map;
    }

    /// <summary>Get the current audio format of a specific audio device.</summary>
    /// <param name="deviceId">the instance ID of the device to query.</param>
    /// <param name="spec">on return, will be filled with device details.</param>
    /// <param name="sampleFrames">pointer to store device buffer size, in sample frames. Can be <see langword="null" />.</param>
    /// <remarks>
    /// <para>For an opened device, this will report the format the device is currently using.</para>
    /// <para>If the device isn't yet opened, this will report the device's preferred format (or a reasonable default if this can't be determined).</para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool GetAudioDeviceFormat(uint deviceId, out AudioSpec spec, out int sampleFrames) {
        bool result = SDL_GetAudioDeviceFormat(deviceId, out spec, out sampleFrames);
        if (!result) {
            LogError(LogCategory.Error, "GetAudioDeviceFormat: Failed to get audio device format.");
        }
        return result;
    }

    /// <summary>Get the gain of an audio device.</summary>
    /// <param name="deviceId">the audio device to query.</param>
    /// <remarks>
    /// The gain of a device is its volume; a larger gain means a louder output, with a gain of zero being silence.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAudioDeviceGain" />
    /// </remarks>
    /// <returns>Returns the gain of the device or -1.0f on failure; call <see cref="GetError()" /> for more information.</returns>
    public static float GetAudioDeviceGain(uint deviceId) {
        float result = SDL_GetAudioDeviceGain(deviceId);
        if (result < 0) {
            LogError(LogCategory.Error, "GetAudioDeviceGain: Failed to get audio device gain.");
        }
        return result;
    }

    /// <summary>Get the human-readable name of a specific audio device.</summary>
    /// <param name="deviceId">the instance ID of the device to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAudioPlaybackDevices" />
    /// <seealso cref="GetAudioRecordingDevices" />
    /// </remarks>
    /// <returns>Returns the name of the audio device, or <see langword="null" /> on failure;call <see cref="GetError()" /> for more information.</returns>
    public static string GetAudioDeviceName(uint deviceId) {
        string name = SDL_GetAudioDeviceName(deviceId);
        if (string.IsNullOrEmpty(name)) {
            LogError(LogCategory.Error, "GetAudioDeviceName: Failed to get audio device name.");
        }
        return name;
    }

    /// <summary>Use this function to get the name of a built-in audio driver.</summary>
    /// <param name="index">the index of the audio driver; the value ranges from 0 to SDL_GetNumAudioDrivers() - 1.</param>
    /// <remarks>
    /// The list of audio drivers is given in the order that they are normally
    /// initialized by default; the drivers that seem more reasonable to choose
    /// first (as far as the SDL developers believe) are earlier in the list.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetNumAudioDrivers" />
    /// </remarks>
    /// <returns>Returns the name of the audio driver at the requested index,  <see langword="null" /> if an invalid index was specified.</returns>
    public static string GetAudioDriver(int index) {
        string driver = SDL_GetAudioDriver(index);
        if (string.IsNullOrEmpty(driver)) {
            LogError(LogCategory.Error, "GetAudioDriver: Failed to get audio driver.");
        }
        return driver;
    }

    /// <summary>Get the human-readable name of an audio format.</summary>
    /// <param name="format">the audio format to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the human-readable name of the specified audio format or &quot;SDL_AUDIO_UNKNOWN&quot; if the format isn't recognized.</returns>
    public static string GetAudioFormatName(AudioFormat format) {
        string name = SDL_GetAudioFormatName(format);
        if (string.IsNullOrEmpty(name)) {
            LogError(LogCategory.Error, "GetAudioFormatName: Failed to get audio format name.");
        }
        return name;
    }

    /// <summary>Get a list of currently-connected audio playback devices.</summary>
    /// <param name="count">a pointer filled in with the number of devices returned, may be discarded.</param>
    /// <remarks>
    /// <para>This returns of list of available devices that play sound, perhaps to speakers or headphones (&quot;playback&quot; devices).</para>
    /// <para>If you want devices that record audio, like a microphone (&quot;recording&quot; devices), use <see cref="GetAudioRecordingDevices" /> instead.</para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="OpenAudioDevice" />
    /// <seealso cref="GetAudioRecordingDevices" />
    /// </remarks>
    /// <returns>(SDL_AudioDeviceID *) Returns a 0 terminated array of device instance IDs or <see langword="null" /> on error; call <see cref="GetError()" />for more information.
    /// This should be freed with <see cref="Free" /> whenit is no longer needed.
    /// </returns>
    public static uint[] GetAudioPlaybackDevices(out int count) {
        nint result = SDL_GetAudioPlaybackDevices(out count);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "GetAudioPlaybackDevices: Failed to get audio playback devices.");
            throw new InvalidOperationException("GetAudioPlaybackDevices failed.");
        }

        if (count < 0) {
            LogError(LogCategory.Error, "GetAudioPlaybackDevices: Invalid device count.");
            return [];
        }

        int[] playbackDevicesI = new int[count];
        Marshal.Copy(result, playbackDevicesI, 0, count);
        uint[] playbackDevicesU = Array.ConvertAll(playbackDevicesI, x => (uint)x);
        return playbackDevicesU;
    }

    /// <summary>Get a list of currently-connected audio recording devices.</summary>
    /// <param name="count">a pointer filled in with the number of devices returned, may be discarded.</param>
    /// <remarks>
    /// <para>This returns of list of available devices that record audio, like a microphone (&quot;recording&quot; devices).</para>
    /// <para>If you want devices that play sound, perhaps to speakers or headphones (&quot;playback&quot; devices), use <see cref="GetAudioPlaybackDevices" /> instead.</para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="OpenAudioDevice" />
    /// <seealso cref="GetAudioPlaybackDevices" />
    /// </remarks>
    /// <returns>
    /// (SDL_AudioDeviceID *) Returns a 0 terminated array of device instance IDs, or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.
    /// This should be freed with <see cref="Free" /> when it is no longer needed.
    /// </returns>
    public static uint[] GetAudioRecordingDevices(out int count) {
        nint result = SDL_GetAudioRecordingDevices(out count);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "GetAudioRecordingDevices: Failed to get audio recording devices.");
        }

        if (count < 0) {
            LogError(LogCategory.Error, "GetAudioRecordingDevices: Invalid device count.");
            return [];
        }

        int[] recordingDevicesI = new int[count];
        Marshal.Copy(result, recordingDevicesI, 0, count);
        uint[] recordingDevices = Array.ConvertAll(recordingDevicesI, x => (uint)x);

        return recordingDevices;
    }

    /// <summary>Get the number of converted/resampled bytes available.</summary>
    /// <param name="stream">the audio stream to query.</param>
    /// <remarks>
    /// <para>The stream may be buffering data behind the scenes until it has enough to resample correctly,
    /// so this number might be lower than what you expect, or even be zero.</para>
    /// <para>Add more data or flush the stream if you need the data now.</para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAudioStreamData" />
    /// <seealso cref="PutAudioStreamData" />
    /// </remarks>
    /// <returns>Returns the number of converted/resampled bytes available or -1 on failure; call <see cref="GetError()" /> for more information.</returns>
    public static int GetAudioStreamAvailable(nint stream) {
        int result = SDL_GetAudioStreamAvailable(stream);
        if (result < 0) {
            LogError(LogCategory.Error, "GetAudioStreamAvailable: Failed to get audio stream available.");
        }
        return result;
    }

    /// <summary>Get converted/resampled data from the stream.</summary>
    /// <param name="stream">the stream the audio is being requested from.</param>
    /// <param name="buf">a buffer to fill with audio data.</param>
    /// <param name="len">the maximum number of bytes to fill.</param>
    /// <remarks>
    /// <para>The input/output data format/channels/sample-rate is specified when creating the stream, and can be changed after creation by calling <see cref="SetAudioStreamFormat" />.</para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread, but if the stream has a callback set, the caller might need to manage extra locking.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ClearAudioStream" />
    /// <seealso cref="GetAudioStreamAvailable" />
    /// <seealso cref="PutAudioStreamData" />
    /// </remarks>
    /// <returns>Returns the number of bytes read from the stream or -1 on failure;call <see cref="GetError()" /> for more information.</returns>
    public static int GetAudioStreamData(nint stream, nint buf, int len) {
        int result = SDL_GetAudioStreamData(stream, buf, len);
        if (result < 0) {
            LogError(LogCategory.Error, "GetAudioStreamData: Failed to get audio stream data.");
        }
        return result;
    }

    /// <summary>Query an audio stream for its currently-bound device.</summary>
    /// <param name="stream">the audio stream to query.</param>
    /// <remarks>
    /// This reports the audio device that an audio stream is currently bound to.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BindAudioStream" />
    /// <seealso cref="BindAudioStreams" />
    /// </remarks>
    /// <returns>Returns the bound audio device, or 0 if not bound or invalid.</returns>
    public static uint GetAudioStreamDevice(nint stream) {
        uint result = SDL_GetAudioStreamDevice(stream);
        if (result == 0) {
            LogError(LogCategory.Error, "GetAudioStreamDevice: Failed to get audio stream device.");
        }
        return result;
    }

    /// <summary>Query the current format of an audio stream.</summary>
    /// <param name="stream">the SDL_AudioStream to query.</param>
    /// <param name="srcSpec">where to store the input audio format; ignored if <see langword="null" />.</param>
    /// <param name="dstSpec">where to store the output audio format; ignored if <see langword="null" />.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread, as it holds astream-specific mutex while running.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAudioStreamFormat" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool GetAudioStreamFormat(nint stream, out AudioSpec srcSpec, out AudioSpec dstSpec) {
        bool result = SDL_GetAudioStreamFormat(stream, out srcSpec, out dstSpec);
        if (!result) {
            LogError(LogCategory.Error, "GetAudioStreamFormat: Failed to get audio stream format.");
        }
        return result;
    }

    /// <summary>Get the frequency ratio of an audio stream.</summary>
    /// <param name="stream">the SDL_AudioStream to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread, as it holds a stream-specific mutex while running.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAudioStreamFrequencyRatio" />
    /// </remarks>
    /// <returns>Returns the frequency ratio of the stream or 0.0 on failure; call <see cref="GetError()" /> for more information.</returns>
    public static float GetAudioStreamFrequencyRatio(nint stream) {
        float result = SDL_GetAudioStreamFrequencyRatio(stream);
        if (result < 0) {
            LogError(LogCategory.Error, "GetAudioStreamFrequencyRatio: Failed to get audio stream frequency ratio.");
        }
        return result;
    }

    /// <summary>Get the gain of an audio stream.</summary>
    /// <param name="stream">the SDL_AudioStream to query.</param>
    /// <remarks>
    /// The gain of a stream is its volume; a larger gain means a louder output, with a gain of zero being silence.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread, as it holds astream-specific mutex while running.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAudioStreamGain" />
    /// </remarks>
    /// <returns>Returns the gain of the stream or -1.0f on failure; call <see cref="GetError()" /> for more information.</returns>
    public static float GetAudioStreamGain(nint stream) {
        float result = SDL_GetAudioStreamGain(stream);
        if (result < 0) {
            LogError(LogCategory.Error, "GetAudioStreamGain: Failed to get audio stream gain.");
        }
        return result;
    }

    /// <summary>Get the current input channel map of an audio stream.</summary>
    /// <param name="stream">the SDL_AudioStream to query.</param>
    /// <param name="count">On output, set to number of channels in the map. Can be <see langword="null" />.</param>
    /// <remarks>
    /// Channel maps are optional; most things do not need them, instead passing data in the order that SDL expects.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread, as it holds astream-specific mutex while running.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAudioStreamInputChannelMap" />
    /// </remarks>
    /// <returns>
    /// <para>Returns an array of the current channel mapping, with as many elements as the current output spec's channels, or <see langword="null" /> if default.</para>
    /// <para>This should be freed with <see cref="Free" /> when it is no longer needed.</para>
    /// </returns>
    public static int[] GetAudioStreamInputChannelMap(nint stream, out int count) {
        nint result = SDL_GetAudioStreamInputChannelMap(stream, out count);

        if (result == nint.Zero) {
            LogError(LogCategory.Error, "GetAudioStreamInputChannelMap: Failed to get audio stream input channel map.");
            return [];
        }

        if (count < 0) {
            LogError(LogCategory.Error, "GetAudioStreamInputChannelMap: Invalid channel map count.");
        }

        int[] map = new int[count];

        for (int i = 0; i < count; i++) {
            map[i] = Marshal.ReadInt32(result, i * sizeof(int));
        }

        return map;
    }

    /// <summary>Get the current output channel map of an audio stream.</summary>
    /// <param name="stream">the SDL_AudioStream to query.</param>
    /// <param name="count">On output, set to number of channels in the map. Can be <see langword="null" />.</param>
    /// <remarks>
    /// Channel maps are optional; most things do not need them, instead passing data in the order that SDL expects.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread, as it holds a stream-specific mutex while running.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAudioStreamInputChannelMap" />
    /// </remarks>
    /// <returns>
    /// <para>Returns an array of the current channel mapping, with as many elements as the current output spec's channels, or <see langword="null" /> if default.</para>
    /// <para>This should be freed with <see cref="Free" /> when it is no longer needed.</para>
    /// </returns>
    public static int[] GetAudioStreamOutputChannelMap(nint stream, out int count) {
        nint result = SDL_GetAudioStreamOutputChannelMap(stream, out count);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "GetAudioStreamOutputChannelMap: Failed to get audio stream input channel map.");
            return [];
        }

        if (count < 0) {
            LogError(LogCategory.Error, "GetAudioStreamOutputChannelMap: Invalid channel map count.");
        }

        int[] map = new int[count];

        for (int i = 0; i < count; i++) {
            map[i] = Marshal.ReadInt32(result, i * sizeof(int));
        }

        return map;
    }

    /// <summary>Get the properties associated with an audio stream.</summary>
    /// <param name="stream">the SDL_AudioStream to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns a valid property ID on success or 0 on failure; call <see cref="GetError()" /> for more information.</returns>
    public static uint GetAudioStreamProperties(nint stream) {
        uint result = SDL_GetAudioStreamProperties(stream);
        if (result == 0) {
            LogError(LogCategory.Error, "GetAudioStreamProperties: Failed to get audio stream properties.");
        }
        return result;
    }

    /// <summary>Get the number of bytes currently queued.</summary>
    /// <param name="stream">the audio stream to query.</param>
    /// <remarks>
    /// <para>This is the number of bytes put into a stream as input, not the number that can be retrieved as output.</para>
    /// <para>Because of several details, it's not possible to calculate one number directly from the other.</para>
    /// <para>If you need to know how much usable data can be retrieved right now, you should use <see cref="GetAudioStreamAvailable" /> and not this function.</para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PutAudioStreamData" />
    /// <seealso cref="ClearAudioStream" />
    /// </remarks>
    /// <returns>Returns the number of bytes queued or -1 on failure; call <see cref="GetError()" /> for more information.</returns>
    public static int GetAudioStreamQueued(nint stream) {
        int result = SDL_GetAudioStreamQueued(stream);
        if (result < 0) {
            LogError(LogCategory.Error, "GetAudioStreamQueued: Failed to get audio stream queued.");
        }
        return result;
    }

    /// <summary>Get the name of the current audio driver.</summary>
    /// <remarks>
    /// <para>The names of drivers are all simple, low-ASCII identifiers, like &quot;alsa&quot;, &quot;coreaudio&quot; or &quot;wasapi&quot;.</para>
    /// <para>These never have Unicode characters, and are not meant to be proper names.</para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the name of the current audio driver or <see langword="null" /> if no driver has been initialized.</returns>
    public static string GetCurrentAudioDriver() {
        string driver = SDL_GetCurrentAudioDriver();
        if (string.IsNullOrEmpty(driver)) {
            LogError(LogCategory.Error, "GetCurrentAudioDriver: Failed to get current audio driver.");
        }
        return driver;
    }

    /// <summary>Use this function to get the number of built-in audio drivers.</summary>
    /// <remarks>
    /// <para>This function returns a hardcoded number.</para>
    /// <para>This never returns a negative value; if there are no drivers compiled into this build of SDL, this function returns zero.</para>
    /// <para>The presence of a driver in this list does not mean it will function, it just means SDL is capable of interacting with that interface.</para>
    /// <para>For example, a build of SDL might have esound support, but if there's no esound server available, SDL's esound driver would fail if used.</para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAudioDriver" />
    /// </remarks>
    /// <returns>Returns the number of built-in audio drivers.</returns>
    public static int GetNumAudioDrivers() {
        int numDrivers = SDL_GetNumAudioDrivers();
        if (numDrivers < 0) {
            LogError(LogCategory.Error, "GetNumAudioDrivers: Failed to get number of audio drivers.");
        }
        return numDrivers;
    }

    /// <summary>Get the appropriate memset value for silencing an audio format.</summary>
    /// <param name="format">the audio data format to query.</param>
    /// <remarks>
    /// <para>The value returned by this function can be used as the second argument to memset (or SDL_memset) to set an audio buffer in a specific format to silence.</para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns a byte value that can be passed to memset.</returns>
    public static int GetSilenceValueForFormat(AudioFormat format) {
        int silenceValue = SDL_GetSilenceValueForFormat(format);
        if (silenceValue < 0) {
            LogError(LogCategory.Error, "GetSilenceValueForFormat: Failed to get silence value for format.");
        }
        return silenceValue;
    }

    /// <summary>Determine if an audio device is physical (instead of logical).</summary>
    /// <param name="deviceId">the device ID to query.</param>
    /// <remarks>
    /// <para>An SDL_AudioDeviceID that represents physical hardware is a physical device; there is one for each piece of hardware that SDL can see.</para>
    /// <para>Logical devices are created by calling <see cref="OpenAudioDevice" /> or <see cref="OpenAudioDeviceStream" />, and while each is associated with a physical device, there can be any number of logical devices on one physical device.</para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if <paramref name="deviceId" /> is a physical device, <see langword="false" /> if it is logical.</returns>
    public static SdlBool IsAudioDevicePhysical(uint deviceId) {
        SdlBool result = SDL_IsAudioDevicePhysical(deviceId);
        if (!result) {
            LogError(LogCategory.Error, "IsAudioDevicePhysical: Failed to check if audio device is physical.");
        }
        return result;
    }

    /// <summary>Determine if an audio device is a playback device (instead of recording).</summary>
    /// <param name="deviceId">the device ID to query.</param>
    /// <remarks>
    /// This function may return either <see langword="true" /> or <see langword="false" /> for invalid device IDs.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if <paramref name="deviceId" /> is a playback device, <see langword="false" /> if it is recording.</returns>
    public static SdlBool IsAudioDevicePlayback(uint deviceId) {
        SdlBool result = SDL_IsAudioDevicePlayback(deviceId);
        if (!result) {
            LogError(LogCategory.Error, "IsAudioDevicePlayback: Failed to check if audio device is playback.");
        }
        return result;
    }

    /// <summary>
    /// Loads a WAV from a file path.
    /// </summary>
    /// <param name="path">the file path of the WAV file to open.</param>
    /// <param name="spec">a pointer to an AudioSpec that will be set to the WAVE data's format details on successful return.</param>
    /// <param name="audioBuf">a pointer filled with the audio data, allocated by the function.</param>
    /// <param name="audioLen">a value filled with the length of the audio data buffer in bytes.</param>
    /// <remarks>
    /// <para>
    /// This is a convenience function that is effectively the same as:
    /// <code>LoadWavIo(IoFromFile(<paramref name="path" />, &quot;rb&quot;, <see langword="true" />, <paramref name="spec" />, <see langword="out" /> <see langword="nuint" /> <paramref name="audioBuf" />, <see langword="out" /> <see langword="uint" /> <paramref name="audioLen" />);</code>
    /// </para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="Free" />
    /// <seealso cref="LoadWavIo" />
    /// </remarks>
    /// <returns>
    /// <para>Returns <see langword="true" /> on success. <paramref name="audioBuf" /> will be filled with a pointer to an allocated buffer containing the audio data, and <paramref name="audioLen" /> is filled with the length of that audio buffer in bytes.</para>
    /// <para>This function returns <see langword="false"/> if the .WAV file cannot be opened, uses an unknown data format, or is corrupt; call <see cref="GetError"/> for more information.</para>
    /// <para>When the application is done with the data returned in <paramref name="audioBuf"/>, it should call <see cref="Free"/> to dispose of it.</para>
    /// </returns>
    public static SdlBool LoadWav(string path, out AudioSpec spec,
        out nuint audioBuf, out uint audioLen) {
        SdlBool result = SDL_LoadWAV(path, out spec, out audioBuf, out audioLen);
        if (!result) {
            LogError(LogCategory.Error, "LoadWAV: Failed to load WAV.");
        }
        return result;
    }

    /// <summary>
    /// Load the audio data of a WAVE file into memory.
    /// </summary>
    /// <param name="src">the data source for the WAVE data.</param>
    /// <param name="closeIo">if <see langword="true"/>, calls <see cref="CloseIo"/> on <paramref name="src"/> before returning, even in the case of an error.</param>
    /// <param name="spec">a pointer to an <see cref="AudioSpec"/> that will be set to the WAVE data's format details on successful return.</param>
    /// <param name="audioBuf">(Uint8 **) a pointer filled with the audio data, allocated by the function/</param>
    /// <param name="audioLen">a value filled with the length of the audio data buffer in bytes.</param>
    /// <remarks>
    /// <list type="bullet">
    /// <item>
    /// <para>Loading a WAVE file requires src, spec, audio_buf and audio_len to be valid pointers.</para>
    /// <para>The entire data portion of the file is then loaded into memory and decoded if necessary.</para>
    /// </item>
    /// <item>
    /// <para>Supported formats are RIFF WAVE files with the formats PCM (8, 16, 24, and 32 bits), IEEE Float (32 bits), Microsoft ADPCM and IMA ADPCM (4 bits), and A-law and mu-law (8 bits).</para>
    /// <para>Other formats are currently unsupported and cause an error.</para>
    /// </item>
    /// <item>
    /// <para>If this function succeeds, the return value is zero and the pointer to the audio data allocated by the function is written to <paramref name="audioBuf"/> and its length in bytes to <paramref name="audioLen"/>.</para>
    /// <para>The <see cref="AudioSpec"/> members freq, channels, and format are set to the values of the audio data in the buffer.</para>
    /// </item>
    /// <item>
    /// It's necessary to use <see cref="Free"/> to free the audio data returned in <paramref name="audioBuf"/> when it is no longer used.
    /// </item>
    /// <item>
    /// <para>Because of the underspecification of the .WAV format, there are many problematic files in the wild that cause issues with strict decoders.</para>
    /// <para>To provide compatibility with these files, this decoder is lenient in regard to the truncation of the file, the fact chunk, and the size of the RIFF chunk.</para>
    /// <para>The hints SDL_HINT_WAVE_RIFF_CHUNK_SIZE, SDL_HINT_WAVE_TRUNCATION, and SDL_HINT_WAVE_FACT_CHUNK can be used to tune the behavior of the loading process.</para>
    /// </item>
    /// <item>
    /// <para>Any file that is invalid (due to truncation, corruption, or wrong values in the headers), too big, or unsupported causes an error.</para>
    /// <para>Additionally, any critical I/O error from the data source will terminate the loading process with an error.</para>
    /// <para>The function returns <see langword="null"/> on error and in all cases (with the exception of <paramref name="src"/> being <see langword="null"/>), an appropriate error message will be set.</para>
    /// </item>
    /// <item>It is required that the data source supports seeking.</item>
    /// <item>
    /// <para>Example:</para>
    /// <para><code>LoadWavIo(IoFromFile(&quot;sample.wav&quot;, &quot;rb&quot;), <see langword="true" />, <see langword="out" /> <see cref="AudioSpec" /> <paramref name="spec" />, <see langword="out" /> <see langword="nuint" /> <paramref name="audioBuf" />, <see langword="out" /> <see langword="uint" /> <paramref name="audioLen" />);</code></para>
    /// <para>Note that <see cref="LoadWav" /> funcion does this same thing for you, but in a less messy way.</para>
    /// <para><code>LoadWav(&quot;sample.wav&quot;, <see langword="out" /> <see cref="AudioSpec" /> <paramref name="spec" />, <see langword="out" /> <see langword="nuint" /> <paramref name="audioBuf" />, <see langword="out" /> <see langword="uint" /> <paramref name="audioLen" />);</code></para>
    /// </item>
    /// </list>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="Free"/>
    /// <seealso cref="LoadWav"/>
    /// </remarks>
    /// <returns>
    /// <para>Returns <see langword="true"/> on success. <paramref name="audioBuf"/> will be filled with a pointer to an allocated buffer containing the audio data, and <paramref name="audioLen"/> is filled with the length of that audio buffer in bytes.</para>
    /// <para>This function returns <see langword="false"/> if the .WAV file cannot be opened, uses an unknown data format, or is corrupt; call <see cref="GetError"/> for more information.</para>
    /// <para>When the application is done with the data returned in <paramref name="audioBuf"/>, it should call <see cref="Free"/> to dispose of it.</para>
    /// </returns>
    public static SdlBool LoadWavIo(nint src, SdlBool closeIo, out AudioSpec spec,
        out nuint audioBuf, out uint audioLen) {
        SdlBool result = SDL_LoadWAV_IO(src, closeIo, out spec, out audioBuf, out audioLen);
        if (!result) {
            LogError(LogCategory.Error, "LoadWAV_IO: Failed to load WAV IO.");
        }
        return result;
    }

    /// <summary>Lock an audio stream for serialized access.</summary>
    /// <param name="stream">the audio stream to lock.</param>
    /// <remarks>
    /// <para>Each SDL_AudioStream has an internal mutex it uses to protect its data structures from threading conflicts.</para>
    /// <para>This function allows an app to lock that mutex, which could be useful if registering callbacks on this stream.</para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="UnlockAudioStream" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static SdlBool LockAudioStream(nint stream) {
        SdlBool result = SDL_LockAudioStream(stream);
        if (!result) {
            LogError(LogCategory.Error, "LockAudioStream: Failed to lock audio stream.");
        }
        return result;
    }

    /// <summary>Mix audio data in a specified format.</summary>
    /// <param name="dst">the destination for the mixed audio.</param>
    /// <param name="src">the source audio buffer to be mixed.</param>
    /// <param name="format">the SDL_AudioFormat structure representing the desired audio format.</param>
    /// <param name="len">the length of the audio buffer in bytes.</param>
    /// <param name="volume">ranges from 0.0 - 1.0, and should be set to 1.0 for full audio volume.</param>
    /// <remarks>
    /// <para>This takes an audio buffer <paramref name="src" /> of <paramref name="len" /> bytes of format data and mixes
    /// it into <paramref name="dst" />, performing addition, volume adjustment, and overflow clipping.</para>
    /// <para>The buffer pointed to by <paramref name="dst" /> must also be <paramref name="len" /> bytes of format data.</para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static SdlBool MixAudio(nint dst, nint src, AudioFormat format, uint len, float volume) {
        SdlBool result = SDL_MixAudio(dst, src, format, len, volume);
        if (!result) {
            LogError(LogCategory.Error, "MixAudio: Failed to mix audio.");
        }
        return result;
    }

    /// <summary>Open a specific audio device.</summary>
    /// <param name="deviceId">the device instance id to open, or SDL_AUDIO_DEVICE_DEFAULT_PLAYBACK or SDL_AUDIO_DEVICE_DEFAULT_RECORDING for the most reasonable default device.</param>
    /// <param name="spec">the requested device configuration. Can be <see langword="null" /> to use reasonable defaults.</param>
    /// <remarks>
    /// <para>You can open both playback and recording devices through this function.</para>
    /// <para>Playback devices will take data from bound audio streams, mix it, and send it to the hardware.</para>
    /// <para>Recording devices will feed any bound audio streams with a copy of any incoming data.</para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CloseAudioDevice" />
    /// <seealso cref="GetAudioDeviceFormat" />
    /// </remarks>
    /// <returns>Returns the device ID on successor 0 on failure; call <see cref="GetError()" /> for more information.</returns>
    public static uint OpenAudioDevice(uint deviceId, ref AudioSpec spec) {
        uint result = SDL_OpenAudioDevice(deviceId, ref spec);
        if (result == 0) {
            LogError(LogCategory.Error, "OpenAudioDevice: Failed to open audio device.");
        }
        return result;
    }

    /// <summary>Convenience function for straightforward audio init for the common case.</summary>
    /// <param name="deviceId">an audio device to open, or SDL_AUDIO_DEVICE_DEFAULT_PLAYBACK or SDL_AUDIO_DEVICE_DEFAULT_RECORDING.</param>
    /// <param name="spec">the audio stream's data format. Can be <see langword="null" />.</param>
    /// <param name="callback">a callback where the app will provide new data for playback, or receive new data for recording. Can be <see langword="null" />, in which case the app will need to call SDL_PutAudioStreamData or SDL_GetAudioStreamData as necessary.</param>
    /// <param name="userdata">app-controlled pointer passed to callback. Can be <see langword="null" />. Ignored if callback is <see langword="null" />.</param>
    /// <remarks>
    /// <para>If all your app intends to do is provide a single source of PCM audio, this function allows you to do all your audio setup in a single call.</para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAudioStreamDevice" />
    /// <seealso cref="ResumeAudioStreamDevice" />
    /// </remarks>
    /// <returns>
    /// <para>(SDL_AudioStream *) Returns an audio stream on success,ready to use, or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</para>
    /// <para>When done with this stream, call <see cref="DestroyAudioStream" /> to free resources and close the device.</para>
    /// </returns>
    public static nint OpenAudioDeviceStream(uint deviceId, ref AudioSpec spec,
            SdlAudioStreamCallback callback, nint userdata) {
        nint result = SDL_OpenAudioDeviceStream(deviceId, ref spec, callback, userdata);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "OpenAudioDeviceStream: Failed to open audio device stream.");
        }
        return result;
    }

    /// <summary>Use this function to pause audio playback on a specified device.</summary>
    /// <param name="deviceId">a device opened by <see cref="OpenAudioDevice" />.</param>
    /// <remarks>
    /// <para>This function pauses audio processing for a given device.</para>
    /// <para>Any bound audio streams will not progress, and no audio will be generated.</para>
    /// <para>Pausing one device does not prevent other unpaused devices from running.</para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ResumeAudioDevice" />
    /// <seealso cref="AudioDevicePaused" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static SdlBool PauseAudioDevice(uint deviceId) {
        SdlBool result = SDL_PauseAudioDevice(deviceId);
        if (!result) {
            LogError(LogCategory.Error, "PauseAudioDevice: Failed to pause audio device.");
        }
        return result;
    }

    /// <summary>Use this function to pause audio playback on the audio device associated with an audio stream.</summary>
    /// <param name="stream">the audio stream associated with the audio device to pause.</param>
    /// <remarks>
    /// <para>This function pauses audio processing for a given device.</para>
    /// <para>Any bound audio streams will not progress, and no audio will be generated.</para>
    /// <para>Pausing one device does not prevent other unpaused devices from running.</para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ResumeAudioStreamDevice" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static SdlBool PauseAudioStreamDevice(nint stream) {
        SdlBool result = SDL_PauseAudioStreamDevice(stream);
        if (!result) {
            LogError(LogCategory.Error, "PauseAudioStreamDevice: Failed to pause audio stream device.");
        }
        return result;
    }

    /// <summary>Add data to the stream.</summary>
    /// <param name="stream">the stream the audio data is being added to.</param>
    /// <param name="buf">a pointer to the audio data to add.</param>
    /// <param name="len">the number of bytes to write to the stream.</param>
    /// <remarks>
    /// This data must match the format/channels/sample rate specified in the latest call to <see cref="SetAudioStreamFormat" />, or the format specified when creating the stream if it hasn't been changed.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread, but if the stream has a callback set, the caller might need to manage extra locking.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ClearAudioStream" />
    /// <seealso cref="FlushAudioStream" />
    /// <seealso cref="GetAudioStreamData" />
    /// <seealso cref="GetAudioStreamQueued" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static SdlBool PutAudioStreamData(nint stream, nint buf, int len) {
        SdlBool result = SDL_PutAudioStreamData(stream, buf, len);
        if (!result) {
            LogError(LogCategory.Error, "PutAudioStreamData: Failed to put audio stream data.");
        }
        return result;
    }

    /// <summary>Use this function to unpause audio playback on a specified device.</summary>
    /// <param name="deviceId">a device opened by <see cref="OpenAudioDevice" />.</param>
    /// <remarks>
    /// <para>This function unpauses audio processing for a given device that has previously been paused with <see cref="PauseAudioDevice" />.</para>
    /// <para>Once unpaused, any bound audio streams will begin to progress again, and audio can be generated.</para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="AudioDevicePaused" />
    /// <seealso cref="PauseAudioDevice" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static SdlBool ResumeAudioDevice(uint deviceId) {
        SdlBool result = SDL_ResumeAudioDevice(deviceId);
        if (!result) {
            LogError(LogCategory.Error, "ResumeAudioDevice: Failed to resume audio device.");
        }
        return result;
    }

    /// <summary>Use this function to unpause audio playback on the audio device associated with an audio stream.</summary>
    /// <param name="stream">the audio stream associated with the audio device to resume.</param>
    /// <remarks>
    /// <para>This function unpauses audio processing for a given device that has previously been paused.</para>
    /// <para>Once unpaused, any bound audio streams will begin to progress again, and audio can be generated.</para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PauseAudioStreamDevice" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static SdlBool ResumeAudioStreamDevice(nint stream) {
        SdlBool result = SDL_ResumeAudioStreamDevice(stream);
        if (!result) {
            LogError(LogCategory.Error, "ResumeAudioStreamDevice: Failed to resume audio stream device.");
        }
        return result;
    }

    /// <summary>Change the gain of an audio device.</summary>
    /// <param name="deviceId">the audio device on which to change gain.</param>
    /// <param name="gain">the gain. 1.0f is no change, 0.0f is silence.</param>
    /// <remarks>
    /// The gain of a device is its volume; a larger gain means a louder output, with a gain of zero being silence.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread, as it holds astream-specific mutex while running.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAudioDeviceGain" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static SdlBool SetAudioDeviceGain(uint deviceId, float gain) {
        SdlBool result = SDL_SetAudioDeviceGain(deviceId, gain);
        if (!result) {
            LogError(LogCategory.Error, "SetAudioDeviceGain: Failed to set audio device gain.");
        }
        return result;
    }

    /// <summary>Set a callback that fires when data is about to be fed to an audio device.</summary>
    /// <param name="deviceId">the ID of an opened audio device.</param>
    /// <param name="callback">a callback function to be called. Can be <see langword="null" />.</param>
    /// <param name="userdata">app-controlled pointer passed to callback. Can be <see langword="null" />.</param>
    /// <remarks>
    /// This is useful for accessing the final mix, perhaps for writing a
    /// visualizer or applying a final effect to the audio data before playback.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static SdlBool SetAudioPostmixCallback(uint deviceId, SdlAudioPostmixCallback callback, nint userdata) {
        SdlBool result = SDL_SetAudioPostmixCallback(deviceId, callback, userdata);
        if (!result) {
            LogError(LogCategory.Error, "SetAudioPostmixCallback: Failed to set audio postmix callback.");
        }
        return result;
    }

    /// <summary>Change the input and output formats of an audio stream.</summary>
    /// <param name="stream">the stream the format is being changed.</param>
    /// <param name="srcSpec">the new format of the audio input; if <see langword="null" />, it is not changed.</param>
    /// <param name="dstSpec">the new format of the audio output; if <see langword="null" />, it is not changed.</param>
    /// <remarks>
    /// Future calls to and <see cref="GetAudioStreamAvailable" />
    /// and <see cref="GetAudioStreamData" /> will reflect the new format,
    /// and future calls to <see cref="PutAudioStreamData" /> must provide data in the new input formats.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread, as it holds a stream-specific mutex while running.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAudioStreamFormat" />
    /// <seealso cref="SetAudioStreamFrequencyRatio" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static SdlBool SetAudioStreamFormat(nint stream, ref AudioSpec srcSpec, ref AudioSpec dstSpec) {
        SdlBool result = SDL_SetAudioStreamFormat(stream, ref srcSpec, ref dstSpec);
        if (!result) {
            LogError(LogCategory.Error, "SetAudioStreamFormat: Failed to set audio stream format.");
        }
        return result;
    }

    /// <summary>Change the frequency ratio of an audio stream.</summary>
    /// <param name="stream">the stream the frequency ratio is being changed.</param>
    /// <param name="ratio">the frequency ratio. 1.0 is normal speed. Must be between 0.01 and 100.</param>
    /// <remarks>
    /// <para>The frequency ratio is used to adjust the rate at which input data is consumed.</para>
    /// <para>Changing this effectively modifies the speed and pitch of the audio.</para>
    /// <para>A value greater than 1.0 will play the audio faster, and at a higher pitch.</para>
    /// <para>A value less than 1.0 will play the audio slower, and at a lower pitch.</para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread, as it holds astream-specific mutex while running.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAudioStreamFrequencyRatio" />
    /// <seealso cref="SetAudioStreamFormat" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static SdlBool SetAudioStreamFrequencyRatio(nint stream, float ratio) {
        SdlBool result = SDL_SetAudioStreamFrequencyRatio(stream, ratio);
        if (!result) {
            LogError(LogCategory.Error, "SetAudioStreamFrequencyRatio: Failed to set audio stream frequency ratio.");
        }
        return result;
    }

    /// <summary>
    /// Change the gain of an audio stream.
    /// </summary>
    /// <param name="stream">the stream on which the gain is being changed.</param>
    /// <param name="gain">the gain. 1.0f is no change, 0.0f is silence;</param>
    /// <remarks>
    /// <para>The gain of a stream is its volume; a larger gain means a louder output, with a gain of zero being silence.</para>
    /// <para>Audio streams default to a gain of 1.0f (no change in output).</para>
    /// <para>This is applied during <see cref="GetAudioStreamData" />, and can be continuously changed to create various effects.</para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread, as it holds a stream-specific mutex while running.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAudioStreamGain" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError" /> for more information.</returns>
    public static SdlBool SetAudioStreamGain(nint stream, float gain) {
        SdlBool result = SDL_SetAudioStreamGain(stream,  gain);
        if (!result) {
            LogError(LogCategory.Error, "SetAudioStreamGain: Failed to set the audio stream gain.");
        }

        return result;
    }

    /// <summary>Set a callback that runs when data is requested from an audio stream.</summary>
    /// <param name="stream">the audio stream to set the new callback on.</param>
    /// <param name="callback">the new callback function to call when data is requested from the stream.</param>
    /// <param name="userdata">an opaque pointer provided to the callback for its own personal use.</param>
    /// <remarks>
    /// This <paramref name="callback" /> is called before data is obtained from the stream, giving the <paramref name="callback" /> the chance to add more on-demand.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAudioStreamPutCallback" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information. This only fails if stream is <see langword="null" />.</returns>
    public static SdlBool SetAudioStreamGetCallback(nint stream, SdlAudioStreamCallback callback, nint userdata) {
        SdlBool result = SDL_SetAudioStreamGetCallback(stream, callback, userdata);
        if (!result) {
            LogError(LogCategory.Error, "SetAudioStreamGetCallback: Failed to set audio stream get callback.");
        }
        return result;
    }

    /// <summary>Set the current input channel map of an audio stream.</summary>
    /// <param name="stream">the SDL_AudioStream to change.</param>
    /// <param name="channelMap">the new channel map, <see langword="null" /> to reset to default.</param>
    /// <param name="count">The number of channels in the map.</param>
    /// <remarks>
    /// Channel maps are optional; most things do not need them, instead passing data in the order that SDL expects.
    /// <para><strong>Thread Safety</strong>:</para>
    /// <para>It is safe to call this function from any thread, as it holds a stream-specific mutex while running.</para>
    /// <para>Don't change the stream's format to have a different number of channels from a different thread at the same time, though!</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAudioStreamInputChannelMap" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static SdlBool SetAudioStreamInputChannelMap(nint stream, Span<int> channelMap, int count) {
        SdlBool result = SDL_SetAudioStreamInputChannelMap(stream, channelMap, count);
        if (!result) {
            LogError(LogCategory.Error, "SetAudioStreamInputChannelMap: Failed to set audio stream input channel map.");
        }
        return result;
    }

    /// <summary>Set the current output channel map of an audio stream.</summary>
    /// <param name="stream">the SDL_AudioStream to change.</param>
    /// <param name="channelMap">the new channel map, <see langword="null" /> to reset to default.</param>
    /// <param name="count">The number of channels in the map.</param>
    /// <remarks>
    /// Channel maps are optional; most things do not need them, instead passing data in the order that SDL expects.
    /// <para><strong>Thread Safety</strong>:</para>
    /// <para>It is safe to call this function from any thread, as it holds a stream-specific mutex while running.</para>
    /// <para>Don't change the stream's format to have a different number of channels from a different thread at the sametime, though!</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAudioStreamInputChannelMap" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static SdlBool SetAudioStreamOutputChannelMap(nint stream, Span<int> channelMap, int count) {
        SdlBool result = SDL_SetAudioStreamOutputChannelMap(stream, channelMap, count);
        if (!result) {
            LogError(LogCategory.Error, "SetAudioStreamOutputChannelMap: Failed to set audio stream output channel map.");
        }
        return result;
    }

    /// <summary>Set a callback that runs when data is added to an audio stream.</summary>
    /// <param name="stream">the audio stream to set the new callback on.</param>
    /// <param name="callback">the new callback function to call when data is added to the stream.</param>
    /// <param name="userdata">an opaque pointer provided to the callback for its own personal use.</param>
    /// <remarks>
    /// This callback is called after the data is added to the stream, giving the callback the chance to obtain it immediately.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAudioStreamGetCallback" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information. This only fails if stream is <see langword="null" />.</returns>
    public static SdlBool SetAudioStreamPutCallback(nint stream, SdlAudioStreamCallback callback, nint userdata) {
        SdlBool result = SDL_SetAudioStreamPutCallback(stream, callback, userdata);
        if (!result) {
            LogError(LogCategory.Error, "SetAudioStreamPutCallback: Failed to set audio stream put callback.");
        }
        return result;
    }

    /// <summary>Unbind a single audio stream from its audio device.</summary>
    /// <param name="stream">an audio stream to unbind from a device. Can be <see langword="null" />.</param>
    /// <remarks>
    /// This is a convenience function, equivalent to calling <code>UnbindAudioStreams(<paramref name="stream" />, 1);</code>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BindAudioStream" />
    /// </remarks>
    public static void UnbindAudioStream(nint stream) {
            // Log the action for debugging purposes
            LogInfo(LogCategory.System, $"Unbinding audio stream with handle: {stream}");
            SDL_UnbindAudioStream(stream);
    }

    /// <summary>Unbind a list of audio streams from their audio devices.</summary>
    /// <param name="streams">an array of audio streams to unbind. Can be <see langword="null" /> or contain <see langword="null" />.</param>
    /// <remarks>
    /// <para>The streams being unbound do not all have to be on the same device.</para>
    /// <para>All streams on the same device will be unbound atomically (data will stop flowing through all unbound streams on the same device at the same time).</para>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BindAudioStreams" />
    /// </remarks>
    public static void UnbindAudioStreams(Span<nint> streams) {
        SDL_UnbindAudioStreams(streams, streams.Length);
    }

    /// <summary>Unlock an audio stream for serialized access.</summary>
    /// <param name="stream">the audio stream to unlock.</param>
    /// <remarks>
    /// This unlocks an audio stream after a call to <see cref="LockAudioStream" />.
    /// <para><strong>Thread Safety</strong>: You should only call this from the same thread that previously called <see cref="LockAudioStream" />.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LockAudioStream" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static SdlBool UnlockAudioStream(nint stream) {
        SdlBool result = SDL_UnlockAudioStream(stream);
        if (!result) {
            LogError(LogCategory.Error, "UnlockAudioStream: Failed to unlock audio stream.");
        }
        return result;
    }

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_AudioDevicePaused(uint dev);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_AudioStreamDevicePaused(nint stream);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_BindAudioStream(uint deviceId, nint stream);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_BindAudioStreams(uint deviceId, Span<nint> streams, int numStreams);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ClearAudioStream(nint stream);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_CloseAudioDevice(uint deviceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ConvertAudioSamples(ref AudioSpec srcSpec, nint srcData, int srcLen,
        ref AudioSpec dstSpec, nint dstData, out int dstLen);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateAudioStream(ref AudioSpec srcSpec, ref AudioSpec dstSpec);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyAudioStream(nint stream);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_FlushAudioStream(nint stream);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetAudioDeviceChannelMap(uint deviceId, out int count);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetAudioDeviceFormat(uint deviceId, out AudioSpec spec, out int sampleFrames);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetAudioDeviceGain(uint deviceId);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetAudioDeviceName(uint deviceId);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetAudioDriver(int index);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetAudioFormatName(AudioFormat format);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetAudioPlaybackDevices(out int count);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetAudioRecordingDevices(out int count);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetAudioStreamAvailable(nint stream);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetAudioStreamData(nint stream, nint buf, int len);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetAudioStreamDevice(nint stream);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetAudioStreamFormat(nint stream, out AudioSpec srcSpec,
        out AudioSpec dstSpec);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetAudioStreamFrequencyRatio(nint stream);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetAudioStreamGain(nint stream);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetAudioStreamInputChannelMap(nint stream, out int count);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetAudioStreamOutputChannelMap(nint stream, out int count);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetAudioStreamProperties(nint stream);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetAudioStreamQueued(nint stream);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetCurrentAudioDriver();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumAudioDrivers();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetSilenceValueForFormat(AudioFormat format);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_IsAudioDevicePhysical(uint deviceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_IsAudioDevicePlayback(uint deviceId);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_LoadWAV(string path, out AudioSpec spec, out nuint audioBuf,
        out uint audioLen);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_LoadWAV_IO(nint src, SdlBool closeIo, out AudioSpec spec,
        out nuint audioBuf, out uint audioLen);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_LockAudioStream(nint stream);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_MixAudio(nint dst, nint src, AudioFormat format, uint len, float volume);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_OpenAudioDevice(uint deviceId, ref AudioSpec spec);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenAudioDeviceStream(uint deviceId, ref AudioSpec spec,
        SdlAudioStreamCallback callback, nint userdata);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_PauseAudioDevice(uint dev);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_PauseAudioStreamDevice(nint stream);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_PutAudioStreamData(nint stream, nint buf, int len);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ResumeAudioDevice(uint dev);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ResumeAudioStreamDevice(nint stream);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAudioDeviceGain(uint deviceId, float gain);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAudioPostmixCallback(uint deviceId, SdlAudioPostmixCallback callback,
        nint userdata);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAudioStreamFormat(nint stream, ref AudioSpec srcSpec,
        ref AudioSpec dstSpec);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAudioStreamFrequencyRatio(nint stream, float ratio);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAudioStreamGain(nint stream, float gain);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAudioStreamGetCallback(nint stream, SdlAudioStreamCallback callback,
        nint userdata);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAudioStreamInputChannelMap(nint stream, Span<int> channelMap, int count);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAudioStreamOutputChannelMap(nint stream, Span<int> channelMap, int count);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAudioStreamPutCallback(nint stream, SdlAudioStreamCallback callback,
        nint userdata);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnbindAudioStream(nint stream);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnbindAudioStreams(Span<nint> streams, int numStreams);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_UnlockAudioStream(nint stream);
}