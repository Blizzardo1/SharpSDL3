<<<<<<< HEAD
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

    /// <param name="devid">a device opened by SDL_OpenAudioDevice().</param>
    /// <remarks>
    /// Unlike in SDL2, audio devices start in an unpaused state, since an app
    /// has to bind a stream before any audio will flow.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PauseAudioDevice"/>
    /// <seealso cref="ResumeAudioDevice"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if device is valid and paused, <see langword="false" /> otherwise.</returns>
=======
﻿using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;
using static SharpSDL3.Delegates;

namespace SharpSDL3; 
public static unsafe partial class Sdl {
>>>>>>> main

    public static bool AudioDevicePaused(uint dev) {
        bool result = SDL_AudioDevicePaused(dev);
        if (!result) {
            LogError(LogCategory.Error, "AudioDevicePaused: Failed to check if audio device is paused.");
<<<<<<< HEAD
            throw new InvalidOperationException("AudioDevicePaused failed.");
=======
            throw new InvalidOperationException("SDL_AudioDevicePaused failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to query if an audio device associated with a stream is paused.</summary>

    /// <param name="stream">the audio stream associated with the audio device to query.</param>
    /// <remarks>
    /// Unlike in SDL2, audio devices start in an unpaused state, since an app
    /// has to bind a stream before any audio will flow.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PauseAudioStreamDevice"/>
    /// <seealso cref="ResumeAudioStreamDevice"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if device is valid and paused, <see langword="false" /> otherwise.</returns>

=======
>>>>>>> main
    public static bool AudioStreamDevicePaused(nint stream) {
        bool result = SDL_AudioStreamDevicePaused(stream);
        if (!result) {
            LogError(LogCategory.Error, "AudioStreamDevicePaused: Failed to check if audio stream device is paused.");
<<<<<<< HEAD
            throw new InvalidOperationException("AudioStreamDevicePaused failed.");
=======
            throw new InvalidOperationException("SDL_AudioStreamDevicePaused failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Bind a single audio stream to an audio device.</summary>

    /// <param name="devid">an audio device to bind a stream to.</param>
    /// <param name="stream">an audio stream to bind to a device.</param>
    /// <remarks>
    /// This is a convenience function, equivalent to calling
    /// SDL_BindAudioStreams(devid, &amp;stream, 1).
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BindAudioStreams"/>
    /// <seealso cref="UnbindAudioStream"/>
    /// <seealso cref="GetAudioStreamDevice"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static bool BindAudioStream(uint devid, nint stream) {
        bool result = SDL_BindAudioStream(devid, stream);
        if (!result) {
            LogError(LogCategory.Error, "BindAudioStream: Failed to bind audio stream.");
<<<<<<< HEAD
            throw new InvalidOperationException("BindAudioStream failed.");
=======
            throw new InvalidOperationException("SDL_BindAudioStream failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Bind a list of audio streams to an audio device.</summary>

    /// <param name="devid">an audio device to bind a stream to.</param>
    /// <param name="streams">an array of audio streams to bind.</param>
    /// <param name="num_streams">number streams listed in the streams array.</param>
    /// <remarks>
    /// Audio data will flow through any bound streams. For a playback device, data
    /// for all bound streams will be mixed together and fed to the device. For a
    /// recording device, a copy of recorded data will be provided to each bound
    /// stream.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BindAudioStreams"/>
    /// <seealso cref="UnbindAudioStream"/>
    /// <seealso cref="GetAudioStreamDevice"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static bool BindAudioStreams(uint devid, Span<nint> streams) {
        bool result = SDL_BindAudioStreams(devid, streams, streams.Length);
        if (!result) {
            LogError(LogCategory.Error, "BindAudioStreams: Failed to bind audio streams.");
<<<<<<< HEAD
            throw new InvalidOperationException("BindAudioStreams failed.");
=======
            throw new InvalidOperationException("SDL_BindAudioStreams failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Clear any pending data in the stream.</summary>

    /// <param name="stream">the audio stream to clear.</param>
    /// <remarks>
    /// This drops any queued data, so there will be nothing to read from the
    /// stream until more is added.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAudioStreamAvailable"/>
    /// <seealso cref="GetAudioStreamData"/>
    /// <seealso cref="GetAudioStreamQueued"/>
    /// <seealso cref="PutAudioStreamData"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static bool ClearAudioStream(nint stream) {
        bool result = SDL_ClearAudioStream(stream);
        if (!result) {
            LogError(LogCategory.Error, "ClearAudioStream: Failed to clear audio stream.");
<<<<<<< HEAD
            throw new InvalidOperationException("ClearAudioStream failed.");
=======
            throw new InvalidOperationException("SDL_ClearAudioStream failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Close a previously-opened audio device.</summary>

    /// <param name="devid">an audio device id previously returned by SDL_OpenAudioDevice().</param>
    /// <remarks>
    /// The application should close open audio devices once they are no longer
    /// needed.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="OpenAudioDevice"/>
    /// </remarks>

=======
>>>>>>> main
    public static void CloseAudioDevice(uint devid) {
        try {
            // Log the action for debugging purposes
            LogInfo(LogCategory.System, $"Closing audio device with ID: {devid}");

            // Call the native method to close the audio device
            SDL_CloseAudioDevice(devid);

            // Log success
            LogInfo(LogCategory.System, $"Successfully closed audio device with ID: {devid}");
        } catch (Exception ex) {
            // Log any unexpected errors
            LogError(LogCategory.Error, $"Error while closing audio device with ID: {devid}. Exception: {ex.Message}");
            throw;
        }
    }

<<<<<<< HEAD
    /// <summary>Convert some audio data of one format to another format.</summary>

    /// <param name="src_spec">the format details of the input audio.</param>
    /// <param name="src_data">the audio data to be converted.</param>
    /// <param name="src_len">the len of src_data.</param>
    /// <param name="dst_spec">the format details of the output audio.</param>
    /// <param name="dst_data">will be filled with a pointer to converted audio data, which should be freed with <see cref="Free"/>. On error, it will be <see langword="null" />.</param>
    /// <param name="dst_len">will be filled with the len of dst_data.</param>
    /// <remarks>
    /// Please note that this function is for convenience, but should not be used
    /// to resample audio in blocks, as it will introduce audio artifacts on the
    /// boundaries. You should only use this function if you are converting audio
    /// data in its entirety in one call. If you want to convert audio in smaller
    /// chunks, use an SDL_AudioStream, which is designed for
    /// this situation.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool ConvertAudioSamples(ref AudioSpec srcSpec, nint srcData, int srcLen,
            ref AudioSpec dstSpec, nint dstData, out int dstLen) {
        bool result = SDL_ConvertAudioSamples(ref srcSpec, srcData, srcLen, ref dstSpec, dstData, out dstLen);
        if (!result) {
            LogError(LogCategory.Error, "ConvertAudioSamples: Failed to convert audio samples.");
            throw new InvalidOperationException("ConvertAudioSamples failed.");
=======
    public static bool ConvertAudioSamples(ref AudioSpec srcSpec, nint srcData, int srcLen,
        ref AudioSpec dstSpec, nint dstData, out int dstLen) {
        bool result = SDL_ConvertAudioSamples(ref srcSpec, srcData, srcLen, ref dstSpec, dstData, out dstLen);
        if (!result) {
            LogError(LogCategory.Error, "ConvertAudioSamples: Failed to convert audio samples.");
            throw new InvalidOperationException("SDL_ConvertAudioSamples failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Create a new audio stream.</summary>

    /// <param name="src_spec">the format details of the input audio.</param>
    /// <param name="dst_spec">the format details of the output audio.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PutAudioStreamData"/>
    /// <seealso cref="GetAudioStreamData"/>
    /// <seealso cref="GetAudioStreamAvailable"/>
    /// <seealso cref="FlushAudioStream"/>
    /// <seealso cref="ClearAudioStream"/>
    /// <seealso cref="SetAudioStreamFormat"/>
    /// <seealso cref="DestroyAudioStream"/>
    /// </remarks>
    /// <returns>(SDL_AudioStream *) Returns a new audio stream on success or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

=======
>>>>>>> main
    public static nint CreateAudioStream(ref AudioSpec srcSpec, ref AudioSpec dstSpec) {
        nint result = SDL_CreateAudioStream(ref srcSpec, ref dstSpec);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "CreateAudioStream: Failed to create audio stream.");
<<<<<<< HEAD
            throw new InvalidOperationException("CreateAudioStream failed.");
=======
            throw new InvalidOperationException("SDL_CreateAudioStream failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Free an audio stream.</summary>

    /// <param name="stream">the audio stream to destroy.</param>
    /// <remarks>
    /// This will release all allocated data, including any audio that is still
    /// queued. You do not need to manually clear the stream first.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateAudioStream"/>
    /// </remarks>

=======
>>>>>>> main
    public static void DestroyAudioStream(nint stream) {
        try {
            // Log the action for debugging purposes
            LogInfo(LogCategory.System, $"Destroying audio stream with handle: {stream}");
            // Call the native method to destroy the audio stream
            SDL_DestroyAudioStream(stream);
            // Log success
            LogInfo(LogCategory.System, $"Successfully destroyed audio stream with handle: {stream}");
        } catch (Exception ex) {
            // Log any unexpected errors
            LogError(LogCategory.Error, $"Error while destroying audio stream with handle: {stream}. Exception: {ex.Message}");
            throw;
        }
    }

<<<<<<< HEAD
    /// <summary>Tell the stream that you're done sending data, and anything being buffered should be converted/resampled and made available immediately.</summary>

    /// <param name="stream">the audio stream to flush.</param>
    /// <remarks>
    /// It is legal to add more data to a stream after flushing, but there may be
    /// audio gaps in the output. Generally this is intended to signal the end of
    /// input, so the complete output becomes available.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PutAudioStreamData"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static bool FlushAudioStream(nint stream) {
        bool result = SDL_FlushAudioStream(stream);
        if (!result) {
            LogError(LogCategory.Error, "FlushAudioStream: Failed to flush audio stream.");
<<<<<<< HEAD
            throw new InvalidOperationException("FlushAudioStream failed.");
=======
            throw new InvalidOperationException("SDL_FlushAudioStream failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Get the current channel map of an audio device.</summary>

    /// <param name="devid">the instance ID of the device to query.</param>
    /// <param name="count">On output, set to number of channels in the map. Can be <see langword="null" />.</param>
    /// <remarks>
    /// Channel maps are optional; most things do not need them, instead passing
    /// data in the order that SDL expects.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAudioStreamInputChannelMap"/>
    /// </remarks>
    /// <returns>(int *) Returns an array of the current channel mapping, with as manyelements as the current output spec's channels, or <see langword="null" /> if default. Thisshould be freed with <see cref="Free"/> when it is no longer needed.</returns>

=======
>>>>>>> main
    public static int[] GetAudioDeviceChannelMap(uint devid) {
        nint result = SDL_GetAudioDeviceChannelMap(devid, out int count);

        if (result == nint.Zero) {
            LogError(LogCategory.Error, "GetAudioDeviceChannelMap: Failed to get audio device channel map.");
            return [];
        }

        if (count < 0) {
            LogError(LogCategory.Error, "GetAudioDeviceChannelMap: Invalid channel map count.");
        }

        if (count == 0) {
            LogError(LogCategory.Error, "GetAudioDeviceChannelMap: No channels available.");
            return [];
        }

        int[] map = new int[count];

<<<<<<< HEAD
        for (int i = 0; i < count; i++) {
=======
        for(int i = 0; i < count; i++) {
>>>>>>> main
            map[i] = Marshal.ReadInt32(result, i * sizeof(int));
        }

        return map;
    }

<<<<<<< HEAD
    /// <summary>Get the current audio format of a specific audio device.</summary>

    /// <param name="devid">the instance ID of the device to query.</param>
    /// <param name="spec">on return, will be filled with device details.</param>
    /// <param name="sample_frames">pointer to store device buffer size, in sample frames. Can be <see langword="null" />.</param>
    /// <remarks>
    /// For an opened device, this will report the format the device is currently
    /// using. If the device isn't yet opened, this will report the device's
    /// preferred format (or a reasonable default if this can't be determined).
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static bool GetAudioDeviceFormat(uint devId, out AudioSpec spec, out int sampleFrames) {
        bool result = SDL_GetAudioDeviceFormat(devId, out spec, out sampleFrames);
        if (!result) {
            LogError(LogCategory.Error, "GetAudioDeviceFormat: Failed to get audio device format.");
<<<<<<< HEAD
            throw new InvalidOperationException("GetAudioDeviceFormat failed.");
=======
            throw new InvalidOperationException("SDL_GetAudioDeviceFormat failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Get the gain of an audio device.</summary>

    /// <param name="devid">the audio device to query.</param>
    /// <remarks>
    /// The gain of a device is its volume; a larger gain means a louder output,
    /// with a gain of zero being silence.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAudioDeviceGain"/>
    /// </remarks>
    /// <returns>Returns the gain of the device or -1.0f on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static float GetAudioDeviceGain(uint devid) {
        float result = SDL_GetAudioDeviceGain(devid);
        if (result < 0) {
            LogError(LogCategory.Error, "GetAudioDeviceGain: Failed to get audio device gain.");
<<<<<<< HEAD
            throw new InvalidOperationException("GetAudioDeviceGain failed.");
=======
            throw new InvalidOperationException("SDL_GetAudioDeviceGain failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Get the human-readable name of a specific audio device.</summary>

    /// <param name="devid">the instance ID of the device to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAudioPlaybackDevices"/>
    /// <seealso cref="GetAudioRecordingDevices"/>
    /// </remarks>
    /// <returns>Returns the name of the audio device, or <see langword="null" /> on failure;call <see cref="GetError()" /> for more information.</returns>

=======
>>>>>>> main
    public static string GetAudioDeviceName(uint devId) {
        string name = SDL_GetAudioDeviceName(devId);
        if (string.IsNullOrEmpty(name)) {
            LogError(LogCategory.Error, "GetAudioDeviceName: Failed to get audio device name.");
<<<<<<< HEAD
            throw new InvalidOperationException("GetAudioDeviceName failed.");
=======
            throw new InvalidOperationException("SDL_GetAudioDeviceName failed.");
>>>>>>> main
        }
        return name;
    }

<<<<<<< HEAD
    /// <summary>Use this function to get the name of a built in audio driver.</summary>

    /// <param name="index">the index of the audio driver; the value ranges from 0 to SDL_GetNumAudioDrivers() - 1.</param>
    /// <remarks>
    /// The list of audio drivers is given in the order that they are normally
    /// initialized by default; the drivers that seem more reasonable to choose
    /// first (as far as the SDL developers believe) are earlier in the list.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetNumAudioDrivers"/>
    /// </remarks>
    /// <returns>Returns the name of the audio driver at the requested index,  <see langword="null" /> if an invalid index was specified.</returns>

=======
>>>>>>> main
    public static string GetAudioDriver(int index) {
        string driver = SDL_GetAudioDriver(index);
        if (string.IsNullOrEmpty(driver)) {
            LogError(LogCategory.Error, "GetAudioDriver: Failed to get audio driver.");
<<<<<<< HEAD
            throw new InvalidOperationException("GetAudioDriver failed.");
=======
            throw new InvalidOperationException("SDL_GetAudioDriver failed.");
>>>>>>> main
        }
        return driver;
    }

<<<<<<< HEAD
    /// <summary>Get the human readable name of an audio format.</summary>

    /// <param name="format">the audio format to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the human readable name of the specified audioformat or &quot;SDL_AUDIO_UNKNOWN&quot; if the format isn'trecognized.</returns>

=======
>>>>>>> main
    public static string GetAudioFormatName(AudioFormat format) {
        string name = SDL_GetAudioFormatName(format);
        if (string.IsNullOrEmpty(name)) {
            LogError(LogCategory.Error, "GetAudioFormatName: Failed to get audio format name.");
<<<<<<< HEAD
            throw new InvalidOperationException("GetAudioFormatName failed.");
=======
            throw new InvalidOperationException("SDL_GetAudioFormatName failed.");
>>>>>>> main
        }
        return name;
    }

<<<<<<< HEAD
    /// <summary>Get a list of currently-connected audio playback devices.</summary>

    /// <param name="count">a pointer filled in with the number of devices returned, may be discarded.</param>
    /// <remarks>
    /// This returns of list of available devices that play sound, perhaps to
    /// speakers or headphones (&quot;playback&quot; devices). If you want devices that
    /// record audio, like a microphone (&quot;recording&quot; devices), use
    /// SDL_GetAudioRecordingDevices() instead.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="OpenAudioDevice"/>
    /// <seealso cref="GetAudioRecordingDevices"/>
    /// </remarks>
    /// <returns>(SDL_AudioDeviceID *) Returns a 0 terminated array ofdevice instance IDs or <see langword="null" /> on error; call <see cref="GetError()" />for more information. This should be freed with <see cref="Free"/> whenit is no longer needed.</returns>

=======
>>>>>>> main
    public static uint[] GetAudioPlaybackDevices(out int count) {
        nint result = SDL_GetAudioPlaybackDevices(out count);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "GetAudioPlaybackDevices: Failed to get audio playback devices.");
<<<<<<< HEAD
            throw new InvalidOperationException("GetAudioPlaybackDevices failed.");
=======
            throw new InvalidOperationException("SDL_GetAudioPlaybackDevices failed.");
>>>>>>> main
        }

        if (count < 0) {
            LogError(LogCategory.Error, "GetAudioPlaybackDevices: Invalid device count.");
            return [];
        }

        int[] playpackDevicesI = new int[count];
        Marshal.Copy(result, playpackDevicesI, 0, count);
        uint[] playpackDevices = Array.ConvertAll(playpackDevicesI, x => (uint)x);
        return playpackDevices;
    }

<<<<<<< HEAD
    /// <summary>Get a list of currently-connected audio recording devices.</summary>

    /// <param name="count">a pointer filled in with the number of devices returned, may be discarded.</param>
    /// <remarks>
    /// This returns of list of available devices that record audio, like a
    /// microphone (&quot;recording&quot; devices). If you want devices that play sound,
    /// perhaps to speakers or headphones (&quot;playback&quot; devices), use
    /// SDL_GetAudioPlaybackDevices() instead.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="OpenAudioDevice"/>
    /// <seealso cref="GetAudioPlaybackDevices"/>
    /// </remarks>
    /// <returns>(SDL_AudioDeviceID *) Returns a 0 terminated array ofdevice instance IDs, or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information. This should be freedwith <see cref="Free"/> when it is no longer needed.</returns>

=======
>>>>>>> main
    public static uint[] GetAudioRecordingDevices(out int count) {
        nint result = SDL_GetAudioRecordingDevices(out count);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "GetAudioRecordingDevices: Failed to get audio recording devices.");
<<<<<<< HEAD
            throw new InvalidOperationException("GetAudioRecordingDevices failed.");
        }

        if (count < 0) {
=======
            throw new InvalidOperationException("SDL_GetAudioRecordingDevices failed.");
        }

        if(count < 0) {
>>>>>>> main
            LogError(LogCategory.Error, "GetAudioRecordingDevices: Invalid device count.");
            return [];
        }

        int[] recordingDevicesI = new int[count];
        Marshal.Copy(result, recordingDevicesI, 0, count);
        uint[] recordingDevices = Array.ConvertAll(recordingDevicesI, x => (uint)x);

        return recordingDevices;
    }

<<<<<<< HEAD
    /// <summary>Get the number of converted/resampled bytes available.</summary>

    /// <param name="stream">the audio stream to query.</param>
    /// <remarks>
    /// The stream may be buffering data behind the scenes until it has enough to
    /// resample correctly, so this number might be lower than what you expect, or
    /// even be zero. Add more data or flush the stream if you need the data now.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAudioStreamData"/>
    /// <seealso cref="PutAudioStreamData"/>
    /// </remarks>
    /// <returns>Returns the number of converted/resampled bytes available or -1 on failure; call <see cref="GetError()" /> for more information.</returns>

=======
>>>>>>> main
    public static int GetAudioStreamAvailable(nint stream) {
        int result = SDL_GetAudioStreamAvailable(stream);
        if (result < 0) {
            LogError(LogCategory.Error, "GetAudioStreamAvailable: Failed to get audio stream available.");
<<<<<<< HEAD
            throw new InvalidOperationException("GetAudioStreamAvailable failed.");
=======
            throw new InvalidOperationException("SDL_GetAudioStreamAvailable failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Get converted/resampled data from the stream.</summary>

    /// <param name="stream">the stream the audio is being requested from.</param>
    /// <param name="buf">a buffer to fill with audio data.</param>
    /// <param name="len">the maximum number of bytes to fill.</param>
    /// <remarks>
    /// The input/output data format/channels/samplerate is specified when creating
    /// the stream, and can be changed after creation by calling
    /// SDL_SetAudioStreamFormat.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, but if the stream has acallback set, the caller might need to manage extra locking.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ClearAudioStream"/>
    /// <seealso cref="GetAudioStreamAvailable"/>
    /// <seealso cref="PutAudioStreamData"/>
    /// </remarks>
    /// <returns>Returns the number of bytes read from the stream or -1 on failure;call <see cref="GetError()" /> for more information.</returns>

=======
>>>>>>> main
    public static int GetAudioStreamData(nint stream, nint buf, int len) {
        int result = SDL_GetAudioStreamData(stream, buf, len);
        if (result < 0) {
            LogError(LogCategory.Error, "GetAudioStreamData: Failed to get audio stream data.");
<<<<<<< HEAD
            throw new InvalidOperationException("GetAudioStreamData failed.");
=======
            throw new InvalidOperationException("SDL_GetAudioStreamData failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Query an audio stream for its currently-bound device.</summary>

    /// <param name="stream">the audio stream to query.</param>
    /// <remarks>
    /// This reports the audio device that an audio stream is currently bound to.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BindAudioStream"/>
    /// <seealso cref="BindAudioStreams"/>
    /// </remarks>
    /// <returns>Returns the bound audio device, or0 if not bound or invalid.</returns>

=======
>>>>>>> main
    public static uint GetAudioStreamDevice(nint stream) {
        uint result = SDL_GetAudioStreamDevice(stream);
        if (result == 0) {
            LogError(LogCategory.Error, "GetAudioStreamDevice: Failed to get audio stream device.");
<<<<<<< HEAD
            throw new InvalidOperationException("GetAudioStreamDevice failed.");
=======
            throw new InvalidOperationException("SDL_GetAudioStreamDevice failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Query the current format of an audio stream.</summary>

    /// <param name="stream">the SDL_AudioStream to query.</param>
    /// <param name="src_spec">where to store the input audio format; ignored if <see langword="null" />.</param>
    /// <param name="dst_spec">where to store the output audio format; ignored if <see langword="null" />.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, as it holds astream-specific mutex while running.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAudioStreamFormat"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static bool GetAudioStreamFormat(nint stream, out AudioSpec srcSpec, out AudioSpec dstSpec) {
        bool result = SDL_GetAudioStreamFormat(stream, out srcSpec, out dstSpec);
        if (!result) {
            LogError(LogCategory.Error, "GetAudioStreamFormat: Failed to get audio stream format.");
<<<<<<< HEAD
            throw new InvalidOperationException("GetAudioStreamFormat failed.");
=======
            throw new InvalidOperationException("SDL_GetAudioStreamFormat failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Get the frequency ratio of an audio stream.</summary>

    /// <param name="stream">the SDL_AudioStream to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, as it holds astream-specific mutex while running.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAudioStreamFrequencyRatio"/>
    /// </remarks>
    /// <returns>Returns the frequency ratio of the stream or 0.0 on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static float GetAudioStreamFrequencyRatio(nint stream) {
        float result = SDL_GetAudioStreamFrequencyRatio(stream);
        if (result < 0) {
            LogError(LogCategory.Error, "GetAudioStreamFrequencyRatio: Failed to get audio stream frequency ratio.");
<<<<<<< HEAD
            throw new InvalidOperationException("GetAudioStreamFrequencyRatio failed.");
=======
            throw new InvalidOperationException("SDL_GetAudioStreamFrequencyRatio failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Get the gain of an audio stream.</summary>

    /// <param name="stream">the SDL_AudioStream to query.</param>
    /// <remarks>
    /// The gain of a stream is its volume; a larger gain means a louder output,
    /// with a gain of zero being silence.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, as it holds astream-specific mutex while running.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAudioStreamGain"/>
    /// </remarks>
    /// <returns>Returns the gain of the stream or -1.0f on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static float GetAudioStreamGain(nint stream) {
        float result = SDL_GetAudioStreamGain(stream);
        if (result < 0) {
            LogError(LogCategory.Error, "GetAudioStreamGain: Failed to get audio stream gain.");
<<<<<<< HEAD
            throw new InvalidOperationException("GetAudioStreamGain failed.");
=======
            throw new InvalidOperationException("SDL_GetAudioStreamGain failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Get the current input channel map of an audio stream.</summary>

    /// <param name="stream">the SDL_AudioStream to query.</param>
    /// <param name="count">On output, set to number of channels in the map. Can be <see langword="null" />.</param>
    /// <remarks>
    /// Channel maps are optional; most things do not need them, instead passing
    /// data in the order that SDL expects.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, as it holds astream-specific mutex while running.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAudioStreamInputChannelMap"/>
    /// </remarks>
    /// <returns>(int *) Returns an array of the current channel mapping, with as manyelements as the current output spec's channels, or <see langword="null" /> if default. Thisshould be freed with <see cref="Free"/> when it is no longer needed.</returns>

=======
>>>>>>> main
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

<<<<<<< HEAD
    /// <summary>Get the current output channel map of an audio stream.</summary>

    /// <param name="stream">the SDL_AudioStream to query.</param>
    /// <param name="count">On output, set to number of channels in the map. Can be <see langword="null" />.</param>
    /// <remarks>
    /// Channel maps are optional; most things do not need them, instead passing
    /// data in the order that SDL expects.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, as it holds astream-specific mutex while running.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAudioStreamInputChannelMap"/>
    /// </remarks>
    /// <returns>(int *) Returns an array of the current channel mapping, with as manyelements as the current output spec's channels, or <see langword="null" /> if default. Thisshould be freed with <see cref="Free"/> when it is no longer needed.</returns>

=======
>>>>>>> main
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

<<<<<<< HEAD
    /// <summary>Get the properties associated with an audio stream.</summary>

    /// <param name="stream">the SDL_AudioStream to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns a valid property ID on success or 0 on failure; call <see cref="GetError()" /> for more information.</returns>

=======
>>>>>>> main
    public static uint GetAudioStreamProperties(nint stream) {
        uint result = SDL_GetAudioStreamProperties(stream);
        if (result == 0) {
            LogError(LogCategory.Error, "GetAudioStreamProperties: Failed to get audio stream properties.");
<<<<<<< HEAD
            throw new InvalidOperationException("GetAudioStreamProperties failed.");
=======
            throw new InvalidOperationException("SDL_GetAudioStreamProperties failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Get the number of bytes currently queued.</summary>

    /// <param name="stream">the audio stream to query.</param>
    /// <remarks>
    /// This is the number of bytes put into a stream as input, not the number that
    /// can be retrieved as output. Because of several details, it's not possible
    /// to calculate one number directly from the other. If you need to know how
    /// much usable data can be retrieved right now, you should use
    /// SDL_GetAudioStreamAvailable() and not this
    /// function.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PutAudioStreamData"/>
    /// <seealso cref="ClearAudioStream"/>
    /// </remarks>
    /// <returns>Returns the number of bytes queued or -1 on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static int GetAudioStreamQueued(nint stream) {
        int result = SDL_GetAudioStreamQueued(stream);
        if (result < 0) {
            LogError(LogCategory.Error, "GetAudioStreamQueued: Failed to get audio stream queued.");
<<<<<<< HEAD
            throw new InvalidOperationException("GetAudioStreamQueued failed.");
=======
            throw new InvalidOperationException("SDL_GetAudioStreamQueued failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Get the name of the current audio driver.</summary>
    /// <remarks>
    /// The names of drivers are all simple, low-ASCII identifiers, like &quot;alsa&quot;,
    /// &quot;coreaudio&quot; or &quot;wasapi&quot;. These never have Unicode characters, and are not
    /// meant to be proper names.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the name of the current audio driver or <see langword="null" /> if nodriver has been initialized.</returns>

=======
>>>>>>> main
    public static string GetCurrentAudioDriver() {
        string driver = SDL_GetCurrentAudioDriver();
        if (string.IsNullOrEmpty(driver)) {
            LogError(LogCategory.Error, "GetCurrentAudioDriver: Failed to get current audio driver.");
<<<<<<< HEAD
            throw new InvalidOperationException("GetCurrentAudioDriver failed.");
=======
            throw new InvalidOperationException("SDL_GetCurrentAudioDriver failed.");
>>>>>>> main
        }
        return driver;
    }

<<<<<<< HEAD
    /// <summary>Use this function to get the number of built-in audio drivers.</summary>
    /// <remarks>
    /// This function returns a hardcoded number. This never returns a negative
    /// value; if there are no drivers compiled into this build of SDL, this
    /// function returns zero. The presence of a driver in this list does not mean
    /// it will function, it just means SDL is capable of interacting with that
    /// interface. For example, a build of SDL might have esound support, but if
    /// there's no esound server available, SDL's esound driver would fail if used.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAudioDriver"/>
    /// </remarks>
    /// <returns>Returns the number of built-in audio drivers.</returns>

=======
>>>>>>> main
    public static int GetNumAudioDrivers() {
        int numDrivers = SDL_GetNumAudioDrivers();
        if (numDrivers < 0) {
            LogError(LogCategory.Error, "GetNumAudioDrivers: Failed to get number of audio drivers.");
<<<<<<< HEAD
            throw new InvalidOperationException("GetNumAudioDrivers failed.");
=======
            throw new InvalidOperationException("SDL_GetNumAudioDrivers failed.");
>>>>>>> main
        }
        return numDrivers;
    }

<<<<<<< HEAD
    /// <summary>Get the appropriate memset value for silencing an audio format.</summary>

    /// <param name="format">the audio data format to query.</param>
    /// <remarks>
    /// The value returned by this function can be used as the second argument to
    /// memset (or SDL_memset) to set an audio buffer in a specific
    /// format to silence.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns a byte value that can be passed to memset.</returns>

=======
>>>>>>> main
    public static int GetSilenceValueForFormat(AudioFormat format) {
        int silenceValue = SDL_GetSilenceValueForFormat(format);
        if (silenceValue < 0) {
            LogError(LogCategory.Error, "GetSilenceValueForFormat: Failed to get silence value for format.");
<<<<<<< HEAD
            throw new InvalidOperationException("GetSilenceValueForFormat failed.");
=======
            throw new InvalidOperationException("SDL_GetSilenceValueForFormat failed.");
>>>>>>> main
        }
        return silenceValue;
    }

<<<<<<< HEAD
    /// <summary>Determine if an audio device is physical (instead of logical).</summary>

    /// <param name="devid">the device ID to query.</param>
    /// <remarks>
    /// An SDL_AudioDeviceID that represents physical hardware
    /// is a physical device; there is one for each piece of hardware that SDL can
    /// see. Logical devices are created by calling
    /// SDL_OpenAudioDevice or
    /// SDL_OpenAudioDeviceStream, and while each is
    /// associated with a physical device, there can be any number of logical
    /// devices on one physical device.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if devid is a physical device, <see langword="false" /> if it is logical.</returns>

=======
>>>>>>> main
    public static SdlBool IsAudioDevicePhysical(uint devid) {
        SdlBool result = SDL_IsAudioDevicePhysical(devid);
        if (!result) {
            LogError(LogCategory.Error, "IsAudioDevicePhysical: Failed to check if audio device is physical.");
<<<<<<< HEAD
            throw new InvalidOperationException("IsAudioDevicePhysical failed.");
=======
            throw new InvalidOperationException("SDL_IsAudioDevicePhysical failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Determine if an audio device is a playback device (instead of recording).</summary>

    /// <param name="devid">the device ID to query.</param>
    /// <remarks>
    /// This function may return either <see langword="true" /> or <see langword="false" /> for invalid device IDs.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if devid is a playback device, <see langword="false" /> if it isrecording.</returns>

=======
>>>>>>> main
    public static SdlBool IsAudioDevicePlayback(uint devid) {
        SdlBool result = SDL_IsAudioDevicePlayback(devid);
        if (!result) {
            LogError(LogCategory.Error, "IsAudioDevicePlayback: Failed to check if audio device is playback.");
<<<<<<< HEAD
            throw new InvalidOperationException("IsAudioDevicePlayback failed.");
=======
            throw new InvalidOperationException("SDL_IsAudioDevicePlayback failed.");
>>>>>>> main
        }
        return result;
    }

    public static SdlBool LoadWav(string path, out AudioSpec spec,
        out nint audioBuf, out uint audioLen) {
        SdlBool result = SDL_LoadWAV(path, out spec, out audioBuf, out audioLen);
        if (!result) {
            LogError(LogCategory.Error, "LoadWAV: Failed to load WAV.");
<<<<<<< HEAD
            throw new InvalidOperationException("LoadWAV failed.");
=======
            throw new InvalidOperationException("SDL_LoadWAV failed.");
>>>>>>> main
        }
        return result;
    }

    public static SdlBool LoadWavIo(nint src, SdlBool closeio, out AudioSpec spec,
        out nint audioBuf, out uint audioLen) {
        SdlBool result = SDL_LoadWAV_IO(src, closeio, out spec, out audioBuf, out audioLen);
        if (!result) {
            LogError(LogCategory.Error, "LoadWAV_IO: Failed to load WAV IO.");
<<<<<<< HEAD
            throw new InvalidOperationException("LoadWAV_IO failed.");
=======
            throw new InvalidOperationException("SDL_LoadWAV_IO failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Lock an audio stream for serialized access.</summary>

    /// <param name="stream">the audio stream to lock.</param>
    /// <remarks>
    /// Each SDL_AudioStream has an internal mutex it uses to
    /// protect its data structures from threading conflicts. This function allows
    /// an app to lock that mutex, which could be useful if registering callbacks
    /// on this stream.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="UnlockAudioStream"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static SdlBool LockAudioStream(nint stream) {
        SdlBool result = SDL_LockAudioStream(stream);
        if (!result) {
            LogError(LogCategory.Error, "LockAudioStream: Failed to lock audio stream.");
<<<<<<< HEAD
            throw new InvalidOperationException("LockAudioStream failed.");
=======
            throw new InvalidOperationException("SDL_LockAudioStream failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Mix audio data in a specified format.</summary>

    /// <param name="dst">the destination for the mixed audio.</param>
    /// <param name="src">the source audio buffer to be mixed.</param>
    /// <param name="format">the SDL_AudioFormat structure representing the desired audio format.</param>
    /// <param name="len">the length of the audio buffer in bytes.</param>
    /// <param name="volume">ranges from 0.0 - 1.0, and should be set to 1.0 for full audio volume.</param>
    /// <remarks>
    /// This takes an audio buffer src of len bytes of format data and mixes
    /// it into dst, performing addition, volume adjustment, and overflow
    /// clipping. The buffer pointed to by dst must also be len bytes of
    /// format data.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static SdlBool MixAudio(nint dst, nint src, AudioFormat format, uint len, float volume) {
        SdlBool result = SDL_MixAudio(dst, src, format, len, volume);
        if (!result) {
            LogError(LogCategory.Error, "MixAudio: Failed to mix audio.");
<<<<<<< HEAD
            throw new InvalidOperationException("MixAudio failed.");
=======
            throw new InvalidOperationException("SDL_MixAudio failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Open a specific audio device.</summary>

    /// <param name="devid">the device instance id to open, or SDL_AUDIO_DEVICE_DEFAULT_PLAYBACK or SDL_AUDIO_DEVICE_DEFAULT_RECORDING for the most reasonable default device.</param>
    /// <param name="spec">the requested device configuration. Can be <see langword="null" /> to use reasonable defaults.</param>
    /// <remarks>
    /// You can open both playback and recording devices through this function.
    /// Playback devices will take data from bound audio streams, mix it, and send
    /// it to the hardware. Recording devices will feed any bound audio streams
    /// with a copy of any incoming data.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CloseAudioDevice"/>
    /// <seealso cref="GetAudioDeviceFormat"/>
    /// </remarks>
    /// <returns>Returns the device ID on successor 0 on failure; call <see cref="GetError()" /> for more information.</returns>

=======
>>>>>>> main
    public static uint OpenAudioDevice(uint devid, ref AudioSpec spec) {
        uint result = SDL_OpenAudioDevice(devid, ref spec);
        if (result == 0) {
            LogError(LogCategory.Error, "OpenAudioDevice: Failed to open audio device.");
<<<<<<< HEAD
            throw new InvalidOperationException("OpenAudioDevice failed.");
=======
            throw new InvalidOperationException("SDL_OpenAudioDevice failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Convenience function for straightforward audio init for the common case.</summary>

    /// <param name="devid">an audio device to open, or SDL_AUDIO_DEVICE_DEFAULT_PLAYBACK or SDL_AUDIO_DEVICE_DEFAULT_RECORDING.</param>
    /// <param name="spec">the audio stream's data format. Can be <see langword="null" />.</param>
    /// <param name="callback">a callback where the app will provide new data for playback, or receive new data for recording. Can be <see langword="null" />, in which case the app will need to call SDL_PutAudioStreamData or SDL_GetAudioStreamData as necessary.</param>
    /// <param name="userdata">app-controlled pointer passed to callback. Can be <see langword="null" />. Ignored if callback is <see langword="null" />.</param>
    /// <remarks>
    /// If all your app intends to do is provide a single source of PCM audio, this
    /// function allows you to do all your audio setup in a single call.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAudioStreamDevice"/>
    /// <seealso cref="ResumeAudioStreamDevice"/>
    /// </remarks>
    /// <returns>(SDL_AudioStream *) Returns an audio stream on success,ready to use, or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information. When done with this stream, callSDL_DestroyAudioStream to free resources andclose the device.</returns>

    public static nint OpenAudioDeviceStream(uint devid, ref AudioSpec spec,
            SdlAudioStreamCallback callback, nint userdata) {
        nint result = SDL_OpenAudioDeviceStream(devid, ref spec, callback, userdata);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "OpenAudioDeviceStream: Failed to open audio device stream.");
            throw new InvalidOperationException("OpenAudioDeviceStream failed.");
=======
    public static nint OpenAudioDeviceStream(uint devid, ref AudioSpec spec,
        SdlAudioStreamCallback callback, nint userdata) {
        nint result = SDL_OpenAudioDeviceStream(devid, ref spec, callback, userdata);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "OpenAudioDeviceStream: Failed to open audio device stream.");
            throw new InvalidOperationException("SDL_OpenAudioDeviceStream failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to pause audio playback on a specified device.</summary>

    /// <param name="devid">a device opened by SDL_OpenAudioDevice().</param>
    /// <remarks>
    /// This function pauses audio processing for a given device. Any bound audio
    /// streams will not progress, and no audio will be generated. Pausing one
    /// device does not prevent other unpaused devices from running.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ResumeAudioDevice"/>
    /// <seealso cref="AudioDevicePaused"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static SdlBool PauseAudioDevice(uint dev) {
        SdlBool result = SDL_PauseAudioDevice(dev);
        if (!result) {
            LogError(LogCategory.Error, "PauseAudioDevice: Failed to pause audio device.");
<<<<<<< HEAD
            throw new InvalidOperationException("PauseAudioDevice failed.");
=======
            throw new InvalidOperationException("SDL_PauseAudioDevice failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to pause audio playback on the audio device associated with an audio stream.</summary>

    /// <param name="stream">the audio stream associated with the audio device to pause.</param>
    /// <remarks>
    /// This function pauses audio processing for a given device. Any bound audio
    /// streams will not progress, and no audio will be generated. Pausing one
    /// device does not prevent other unpaused devices from running.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ResumeAudioStreamDevice"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static SdlBool PauseAudioStreamDevice(nint stream) {
        SdlBool result = SDL_PauseAudioStreamDevice(stream);
        if (!result) {
            LogError(LogCategory.Error, "PauseAudioStreamDevice: Failed to pause audio stream device.");
<<<<<<< HEAD
            throw new InvalidOperationException("PauseAudioStreamDevice failed.");
=======
            throw new InvalidOperationException("SDL_PauseAudioStreamDevice failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Add data to the stream.</summary>

    /// <param name="stream">the stream the audio data is being added to.</param>
    /// <param name="buf">a pointer to the audio data to add.</param>
    /// <param name="len">the number of bytes to write to the stream.</param>
    /// <remarks>
    /// This data must match the format/channels/samplerate specified in the latest
    /// call to SDL_SetAudioStreamFormat, or the format
    /// specified when creating the stream if it hasn't been changed.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, but if the stream has acallback set, the caller might need to manage extra locking.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ClearAudioStream"/>
    /// <seealso cref="FlushAudioStream"/>
    /// <seealso cref="GetAudioStreamData"/>
    /// <seealso cref="GetAudioStreamQueued"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static SdlBool PutAudioStreamData(nint stream, nint buf, int len) {
        SdlBool result = SDL_PutAudioStreamData(stream, buf, len);
        if (!result) {
            LogError(LogCategory.Error, "PutAudioStreamData: Failed to put audio stream data.");
<<<<<<< HEAD
            throw new InvalidOperationException("PutAudioStreamData failed.");
=======
            throw new InvalidOperationException("SDL_PutAudioStreamData failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to unpause audio playback on a specified device.</summary>

    /// <param name="devid">a device opened by SDL_OpenAudioDevice().</param>
    /// <remarks>
    /// This function unpauses audio processing for a given device that has
    /// previously been paused with SDL_PauseAudioDevice().
    /// Once unpaused, any bound audio streams will begin to progress again, and
    /// audio can be generated.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="AudioDevicePaused"/>
    /// <seealso cref="PauseAudioDevice"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static SdlBool ResumeAudioDevice(uint dev) {
        SdlBool result = SDL_ResumeAudioDevice(dev);
        if (!result) {
            LogError(LogCategory.Error, "ResumeAudioDevice: Failed to resume audio device.");
<<<<<<< HEAD
            throw new InvalidOperationException("ResumeAudioDevice failed.");
=======
            throw new InvalidOperationException("SDL_ResumeAudioDevice failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to unpause audio playback on the audio device associated with an audio stream.</summary>

    /// <param name="stream">the audio stream associated with the audio device to resume.</param>
    /// <remarks>
    /// This function unpauses audio processing for a given device that has
    /// previously been paused. Once unpaused, any bound audio streams will begin
    /// to progress again, and audio can be generated.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PauseAudioStreamDevice"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static SdlBool ResumeAudioStreamDevice(nint stream) {
        SdlBool result = SDL_ResumeAudioStreamDevice(stream);
        if (!result) {
            LogError(LogCategory.Error, "ResumeAudioStreamDevice: Failed to resume audio stream device.");
<<<<<<< HEAD
            throw new InvalidOperationException("ResumeAudioStreamDevice failed.");
=======
            throw new InvalidOperationException("SDL_ResumeAudioStreamDevice failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Change the gain of an audio device.</summary>

    /// <param name="devid">the audio device on which to change gain.</param>
    /// <param name="gain">the gain. 1.0f is no change, 0.0f is silence.</param>
    /// <remarks>
    /// The gain of a device is its volume; a larger gain means a louder output,
    /// with a gain of zero being silence.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, as it holds astream-specific mutex while running.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAudioDeviceGain"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static SdlBool SetAudioDeviceGain(uint devid, float gain) {
        SdlBool result = SDL_SetAudioDeviceGain(devid, gain);
        if (!result) {
            LogError(LogCategory.Error, "SetAudioDeviceGain: Failed to set audio device gain.");
<<<<<<< HEAD
            throw new InvalidOperationException("SetAudioDeviceGain failed.");
=======
            throw new InvalidOperationException("SDL_SetAudioDeviceGain failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Set a callback that fires when data is about to be fed to an audio device.</summary>

    /// <param name="devid">the ID of an opened audio device.</param>
    /// <param name="callback">a callback function to be called. Can be <see langword="null" />.</param>
    /// <param name="userdata">app-controlled pointer passed to callback. Can be <see langword="null" />.</param>
    /// <remarks>
    /// This is useful for accessing the final mix, perhaps for writing a
    /// visualizer or applying a final effect to the audio data before playback.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static SdlBool SetAudioPostmixCallback(uint devid, SdlAudioPostmixCallback callback, nint userdata) {
        SdlBool result = SDL_SetAudioPostmixCallback(devid, callback, userdata);
        if (!result) {
            LogError(LogCategory.Error, "SetAudioPostmixCallback: Failed to set audio postmix callback.");
<<<<<<< HEAD
            throw new InvalidOperationException("SetAudioPostmixCallback failed.");
=======
            throw new InvalidOperationException("SDL_SetAudioPostmixCallback failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Change the input and output formats of an audio stream.</summary>

    /// <param name="stream">the stream the format is being changed.</param>
    /// <param name="src_spec">the new format of the audio input; if <see langword="null" />, it is not changed.</param>
    /// <param name="dst_spec">the new format of the audio output; if <see langword="null" />, it is not changed.</param>
    /// <remarks>
    /// Future calls to and
    /// SDL_GetAudioStreamAvailable and
    /// SDL_GetAudioStreamData will reflect the new
    /// format, and future calls to
    /// SDL_PutAudioStreamData must provide data in the
    /// new input formats.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, as it holds astream-specific mutex while running.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAudioStreamFormat"/>
    /// <seealso cref="SetAudioStreamFrequencyRatio"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static SdlBool SetAudioStreamFormat(nint stream, ref AudioSpec srcSpec, ref AudioSpec dstSpec) {
        SdlBool result = SDL_SetAudioStreamFormat(stream, ref srcSpec, ref dstSpec);
        if (!result) {
            LogError(LogCategory.Error, "SetAudioStreamFormat: Failed to set audio stream format.");
<<<<<<< HEAD
            throw new InvalidOperationException("SetAudioStreamFormat failed.");
=======
            throw new InvalidOperationException("SDL_SetAudioStreamFormat failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Change the frequency ratio of an audio stream.</summary>

    /// <param name="stream">the stream the frequency ratio is being changed.</param>
    /// <param name="ratio">the frequency ratio. 1.0 is normal speed. Must be between 0.01 and 100.</param>
    /// <remarks>
    /// The frequency ratio is used to adjust the rate at which input data is
    /// consumed. Changing this effectively modifies the speed and pitch of the
    /// audio. A value greater than 1.0 will play the audio faster, and at a higher
    /// pitch. A value less than 1.0 will play the audio slower, and at a lower
    /// pitch.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, as it holds astream-specific mutex while running.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAudioStreamFrequencyRatio"/>
    /// <seealso cref="SetAudioStreamFormat"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static SdlBool SetAudioStreamFrequencyRatio(nint stream, float ratio) {
        SdlBool result = SDL_SetAudioStreamFrequencyRatio(stream, ratio);
        if (!result) {
            LogError(LogCategory.Error, "SetAudioStreamFrequencyRatio: Failed to set audio stream frequency ratio.");
<<<<<<< HEAD
            throw new InvalidOperationException("SetAudioStreamFrequencyRatio failed.");
=======
            throw new InvalidOperationException("SDL_SetAudioStreamFrequencyRatio failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Set a callback that runs when data is requested from an audio stream.</summary>

    /// <param name="stream">the audio stream to set the new callback on.</param>
    /// <param name="callback">the new callback function to call when data is requested from the stream.</param>
    /// <param name="userdata">an opaque pointer provided to the callback for its own personal use.</param>
    /// <remarks>
    /// This callback is called before data is obtained from the stream, giving
    /// the callback the chance to add more on-demand.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAudioStreamPutCallback"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information. This only fails ifstream is <see langword="null" />.</returns>

=======
>>>>>>> main
    public static SdlBool SetAudioStreamGetCallback(nint stream, SdlAudioStreamCallback callback, nint userdata) {
        SdlBool result = SDL_SetAudioStreamGetCallback(stream, callback, userdata);
        if (!result) {
            LogError(LogCategory.Error, "SetAudioStreamGetCallback: Failed to set audio stream get callback.");
<<<<<<< HEAD
            throw new InvalidOperationException("SetAudioStreamGetCallback failed.");
=======
            throw new InvalidOperationException("SDL_SetAudioStreamGetCallback failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Set the current input channel map of an audio stream.</summary>

    /// <param name="stream">the SDL_AudioStream to change.</param>
    /// <param name="chmap">the new channel map, <see langword="null" /> to reset to default.</param>
    /// <param name="count">The number of channels in the map.</param>
    /// <remarks>
    /// Channel maps are optional; most things do not need them, instead passing
    /// data in the order that SDL expects.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, as it holds astream-specific mutex while running. Don't change the stream's format tohave a different number of channels from a a different thread at the sametime, though!</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAudioStreamInputChannelMap"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static SdlBool SetAudioStreamInputChannelMap(nint stream, Span<int> chmap, int count) {
        SdlBool result = SDL_SetAudioStreamInputChannelMap(stream, chmap, count);
        if (!result) {
            LogError(LogCategory.Error, "SetAudioStreamInputChannelMap: Failed to set audio stream input channel map.");
<<<<<<< HEAD
            throw new InvalidOperationException("SetAudioStreamInputChannelMap failed.");
=======
            throw new InvalidOperationException("SDL_SetAudioStreamInputChannelMap failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Set the current output channel map of an audio stream.</summary>

    /// <param name="stream">the SDL_AudioStream to change.</param>
    /// <param name="chmap">the new channel map, <see langword="null" /> to reset to default.</param>
    /// <param name="count">The number of channels in the map.</param>
    /// <remarks>
    /// Channel maps are optional; most things do not need them, instead passing
    /// data in the order that SDL expects.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, as it holds astream-specific mutex while running. Don't change the stream's format tohave a different number of channels from a a different thread at the sametime, though!</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAudioStreamInputChannelMap"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static SdlBool SetAudioStreamOutputChannelMap(nint stream, Span<int> chmap, int count) {
        SdlBool result = SDL_SetAudioStreamOutputChannelMap(stream, chmap, count);
        if (!result) {
            LogError(LogCategory.Error, "SetAudioStreamOutputChannelMap: Failed to set audio stream output channel map.");
<<<<<<< HEAD
            throw new InvalidOperationException("SetAudioStreamOutputChannelMap failed.");
=======
            throw new InvalidOperationException("SDL_SetAudioStreamOutputChannelMap failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Set a callback that runs when data is added to an audio stream.</summary>

    /// <param name="stream">the audio stream to set the new callback on.</param>
    /// <param name="callback">the new callback function to call when data is added to the stream.</param>
    /// <param name="userdata">an opaque pointer provided to the callback for its own personal use.</param>
    /// <remarks>
    /// This callback is called after the data is added to the stream, giving the
    /// callback the chance to obtain it immediately.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAudioStreamGetCallback"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information. This only fails ifstream is <see langword="null" />.</returns>

=======
>>>>>>> main
    public static SdlBool SetAudioStreamPutCallback(nint stream, SdlAudioStreamCallback callback, nint userdata) {
        SdlBool result = SDL_SetAudioStreamPutCallback(stream, callback, userdata);
        if (!result) {
            LogError(LogCategory.Error, "SetAudioStreamPutCallback: Failed to set audio stream put callback.");
<<<<<<< HEAD
            throw new InvalidOperationException("SetAudioStreamPutCallback failed.");
=======
            throw new InvalidOperationException("SDL_SetAudioStreamPutCallback failed.");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Unbind a single audio stream from its audio device.</summary>

    /// <param name="stream">an audio stream to unbind from a device. Can be <see langword="null" />.</param>
    /// <remarks>
    /// This is a convenience function, equivalent to calling
    /// SDL_UnbindAudioStreams(&amp;stream, 1).
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BindAudioStream"/>
    /// </remarks>

=======
>>>>>>> main
    public static void UnbindAudioStream(nint stream) {
        try {
            // Log the action for debugging purposes
            LogInfo(LogCategory.System, $"Unbinding audio stream with handle: {stream}");

            // Call the native method to unbind the audio stream
            SDL_UnbindAudioStream(stream);

            // Log success
            LogInfo(LogCategory.System, $"Successfully unbound audio stream with handle: {stream}");
        } catch (Exception ex) {
            // Log any unexpected errors
            LogError(LogCategory.Error, $"Error while unbinding audio stream with handle: {stream}. Exception: {ex.Message}");
            throw;
        }
    }

<<<<<<< HEAD
    /// <summary>Unbind a list of audio streams from their audio devices.</summary>

    /// <param name="streams">an array of audio streams to unbind. Can be <see langword="null" /> or contain <see langword="null" />.</param>
    /// <param name="num_streams">number streams listed in the streams array.</param>
    /// <remarks>
    /// The streams being unbound do not all have to be on the same device. All
    /// streams on the same device will be unbound atomically (data will stop
    /// flowing through all unbound streams on the same device at the same time).
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BindAudioStreams"/>
    /// </remarks>

=======
>>>>>>> main
    public static void UnbindAudioStreams(Span<nint> streams) {
        SDL_UnbindAudioStreams(streams, streams.Length);
    }

<<<<<<< HEAD
    /// <summary>Unlock an audio stream for serialized access.</summary>

    /// <param name="stream">the audio stream to unlock.</param>
    /// <remarks>
    /// This unlocks an audio stream after a call to
    /// SDL_LockAudioStream.
    /// <para><strong>Thread Safety:</strong> You should only call this from the same thread that previously calledSDL_LockAudioStream.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LockAudioStream"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static SdlBool UnlockAudioStream(nint stream) {
        SdlBool result = SDL_UnlockAudioStream(stream);
        if (!result) {
            LogError(LogCategory.Error, "UnlockAudioStream: Failed to unlock audio stream.");
<<<<<<< HEAD
            throw new InvalidOperationException("UnlockAudioStream failed.");
=======
            throw new InvalidOperationException("SDL_UnlockAudioStream failed.");
>>>>>>> main
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

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetAudioDeviceName(uint devid);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetAudioDriver(int index);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
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

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetCurrentAudioDriver();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumAudioDrivers();
<<<<<<< HEAD

=======
>>>>>>> main
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetSilenceValueForFormat(AudioFormat format);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_IsAudioDevicePhysical(uint devid);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_IsAudioDevicePlayback(uint devid);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
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
<<<<<<< HEAD

=======
>>>>>>> main
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenAudioDeviceStream(uint devid, ref AudioSpec spec,
        SdlAudioStreamCallback callback, nint userdata);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_PauseAudioDevice(uint dev);
<<<<<<< HEAD

=======
>>>>>>> main
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_PauseAudioStreamDevice(nint stream);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_PutAudioStreamData(nint stream, nint buf, int len);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ResumeAudioDevice(uint dev);
<<<<<<< HEAD

=======
>>>>>>> main
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ResumeAudioStreamDevice(nint stream);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAudioDeviceGain(uint devid, float gain);
<<<<<<< HEAD

=======
>>>>>>> main
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
<<<<<<< HEAD

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_UnlockAudioStream(nint stream);
}
=======
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_UnlockAudioStream(nint stream);
}
>>>>>>> main
