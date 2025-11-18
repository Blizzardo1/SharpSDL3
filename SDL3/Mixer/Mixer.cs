using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SharpSDL3.Mixer;

public static unsafe partial class Mixer {
    public const string EffectMaxSpeed = "EFFECTSMAXSPEED";

    /// <summary>
    /// Printable format: "%d.%d.%d", MAJOR, MINOR, MICRO
    /// </summary>
    public const int Major = 3;
    public const int Micro = 2;
    public const int Minor = 4;

    private const string NativeLibName = "SDL3_mixer";

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MixChannelFinishedCallback(int channel);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool MixEachSoundFontCallback(string a, nint b);

    /// <summary>
    /// This is a callback that signifies that a channel has finished all its loops
    /// and has completed playback.
    /// </summary>
    /// <param name="chan"> The channel number that your effect is affecting.</param>
    /// <param name="udata">User Data</param>
    /// <remarks>
    /// This gets called if the buffer plays out normally, or if you call
    /// Mix_HaltChannel(), implicitly stop a channel via Mix_AllocateChannels(), or
    /// unregister a callback while it's still playing.
    /// </remarks>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MixEffectDoneT(int chan, nint udata);

    /// <summary>
    /// This is the format of a special effect callback:
    ///
    /// myeffect(int chan, void *stream, int len, void *udata);
    /// </summary>
    /// <param name="chan"> The channel number that your effect is affecting.</param>
    /// <param name="stream"> The buffer of data to work upon.</param>
    /// <param name="len"> The size of (stream).</param>
    /// <param name="udata">User Data</param>
    /// <remarks>
    /// (chan) is the channel number that your effect is affecting. (stream) is the
    /// buffer of data to work upon. (len) is the size of (stream), and (udata) is
    /// a user-defined bit of data, which you pass as the last arg of
    /// Mix_RegisterEffect(), and is passed back unmolested to your callback. Your
    /// effect changes the contents of (stream) based on whatever parameters are
    /// significant, or just leaves it be, if you prefer. You can do whatever you
    /// like to the buffer, though, and it will continue in its changed state down
    /// the mixing pipeline, through any other effect functions, then finally to be
    /// mixed with the rest of the channels and music for the final output stream.
    /// </remarks>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MixEffectFuncT(int chan, nint stream, int len, nint udata);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MixMixCallback(nint udata, nint stream, int len);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MusicFinishedCallback();

    /// <summary>
    /// Sets a callback that runs when a channel has finished playing.
    /// </summary>
    /// <param name="channelFinishedCallback">The callback function to be invoked when a channel finishes playing.</param>
    /// <remarks>
    /// This method ensures that the callback is properly registered and provides a layer of abstraction
    /// over the native method to make it less trivial.
    /// </remarks>
    public static void ChannelFinished(MixChannelFinishedCallback channelFinishedCallback) {
        if (channelFinishedCallback == null) {
            throw new ArgumentNullException(nameof(channelFinishedCallback), "Callback cannot be null.");
        }

        Mix_ChannelFinished(channelFinishedCallback);
    }

    public static void EffectDone(int channel, nint udata) {
        if (udata == nint.Zero) {
            throw new ArgumentNullException(nameof(udata));
        }

        var effectDone = Marshal.GetDelegateForFunctionPointer<MixEffectDoneT>(udata);

        effectDone(channel, udata);
    }

    public static int ExpireChannel(int channel, int ticks) {
        return ticks < 0
            ? throw new ArgumentOutOfRangeException(nameof(ticks), "Ticks must be non-negative.")
            : Mix_ExpireChannel(channel, ticks);
    }

    public static int FadeInChannel(int channel, Chunk chunk, int loops, int ms) {
        if (chunk.AudioBuffer == nint.Zero) {
            Sdl.LogError(LogCategory.Error, "Null Chunk");
            return -1;
        }

        nint pChunk = Sdl.Malloc(Sdl.SizeOf<Chunk>());
        *(Chunk*)pChunk = chunk;
        int result = Mix_FadeInChannel(channel, pChunk, loops, ms);
        Sdl.Free(pChunk);

        if (result == -1) {
            Sdl.LogError(LogCategory.Error, $"Failed to fade in channel: {Sdl.GetError()}");
        }

        return result;
    }

    public static int FadeInChannelTimed(int channel, Chunk chunk, int loops, int ms, int ticks) {
        if (chunk.AudioBuffer == nint.Zero) {
            Sdl.LogError(LogCategory.Error, "Null Chunk");
            return -1;
        }

        int result = Mix_FadeInChannelTimed(channel, (nint)Unsafe.AsPointer(ref chunk), loops, ms, ticks);

        if (result == -1) {
            Sdl.LogError(LogCategory.Error, $"Failed to fade in channel: {Sdl.GetError()}");
        }

        return result;
    }

    public static bool FadeInMusic(Music music, int loops, int ms) {
        if (music.Interface != nint.Zero) return Mix_FadeInMusic((nint)Unsafe.AsPointer(ref music), loops, ms);
        Sdl.LogError(LogCategory.Error, "Null Music");
        return false;

    }

    public static bool FadeInMusicPos(Music music, int loops, int ms, double position) {
        if (music.Interface == nint.Zero) {
            Sdl.LogError(LogCategory.Error, "Null Music");
        }

        return Mix_FadeInMusicPos((nint)Unsafe.AsPointer(ref music), loops, ms, position);
    }

    public static int FadeOutChannel(int which, int ms) {
        return ms < 0
            ? throw new ArgumentOutOfRangeException(nameof(ms), "Milliseconds must be non-negative.") 
            : Mix_FadeOutChannel(which, ms);
    }

    public static int FadeOutGroup(int tag, int ms) {
        return ms < 0
            ? throw new ArgumentOutOfRangeException(nameof(ms), "Milliseconds must be non-negative.")
            : Mix_FadeOutGroup(tag, ms);
    }

    public static bool FadeOutMusic(int ms) {
        return ms < 0 
            ? throw new ArgumentOutOfRangeException(nameof(ms), "Milliseconds must be non-negative.") 
            : Mix_FadeOutMusic(ms);
    }

    public static Fading FadingChannel(int which) {
        return which < 0 
            ? throw new ArgumentOutOfRangeException(nameof(which), "Channel must be non-negative.") 
            : Mix_FadingChannel(which);
    }

    public static Fading FadingMusic() => Mix_FadingMusic();

    public static nint GetChunk(int channel) {
        return channel < 0 
            ? throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be non-negative.") 
            : Mix_GetChunk(channel);
    }

    public static nint GetMusicHookData() => Mix_GetMusicHookData();

    public static double GetMusicLoopEndTime(Music music) {
        return music.Interface == nint.Zero 
            ? throw new ArgumentNullException(nameof(music)) 
            : Mix_GetMusicLoopEndTime((nint)Unsafe.AsPointer(ref music));
    }

    public static double GetMusicLoopEndTime(nint music) {
        return music == nint.Zero 
            ? throw new ArgumentNullException(nameof(music))
            : Mix_GetMusicLoopEndTime(music);
    }

    public static double GetMusicLoopLengthTime(Music music) {
        return music.Interface == nint.Zero
            ? throw new ArgumentNullException(nameof(music))
            : Mix_GetMusicLoopLengthTime((nint)Unsafe.AsPointer(ref music));
    }

    public static double GetMusicLoopLengthTime(nint music) {
        return music == nint.Zero 
            ? throw new ArgumentNullException(nameof(music)) 
            : Mix_GetMusicLoopLengthTime(music);
    }

    public static double GetMusicLoopStartTime(Music music) {
        return music.Interface == nint.Zero 
            ? throw new ArgumentNullException(nameof(music)) 
            : Mix_GetMusicLoopStartTime((nint)Unsafe.AsPointer(ref music));
    }

    public static double GetMusicLoopStartTime(nint music) {
        return music == nint.Zero
            ? throw new ArgumentNullException(nameof(music)) 
            : Mix_GetMusicLoopStartTime(music);
    }

    public static double GetMusicPosition(Music music) {
        return music.Interface == nint.Zero 
            ? throw new ArgumentNullException(nameof(music)) 
            : Mix_GetMusicPosition((nint)Unsafe.AsPointer(ref music));
    }

    public static double GetMusicPosition(nint music) {
        return music == nint.Zero 
            ? throw new ArgumentNullException(nameof(music)) 
            : Mix_GetMusicPosition(music);
    }

    public static int GetMusicVolume(Music music) {
        return music.Interface == nint.Zero 
            ? throw new ArgumentNullException(nameof(music)) 
            : Mix_GetMusicVolume((nint)Unsafe.AsPointer(ref music));
    }

    public static int GetMusicVolume(nint music) {
        return music == nint.Zero 
            ? throw new ArgumentNullException(nameof(music)) 
            : Mix_GetMusicVolume(music);
    }

    public static int GetNumTracks(Music music) {
        return music.Interface == nint.Zero 
            ? throw new ArgumentNullException(nameof(music))
            : Mix_GetNumTracks((nint)Unsafe.AsPointer(ref music));
    }

    public static int GetNumTracks(nint music) {
        return music == nint.Zero 
            ? throw new ArgumentNullException(nameof(music)) 
            : Mix_GetNumTracks(music);
    }

    public static string GetSoundFonts() {
        string soundFonts = Mix_GetSoundFonts();
        return string.IsNullOrEmpty(soundFonts) 
            ? throw new SdlException($"Failed to get sound fonts: {Sdl.GetError()}") 
            : soundFonts;
    }

    public static string GetTimidityCfg() {
        string timidityCfg = Mix_GetTimidityCfg();
        return string.IsNullOrEmpty(timidityCfg) 
            ? throw new SdlException($"Failed to get Timidity configuration: {Sdl.GetError()}")
            : timidityCfg;
    }

    public static int GroupAvailable(int tag) {
        return tag < 0 
            ? throw new ArgumentOutOfRangeException(nameof(tag), "Tag must be non-negative.") 
            : Mix_GroupAvailable(tag);
    }

    public static bool GroupChannel(int which, int tag) {
        if (which < 0) {
            throw new ArgumentOutOfRangeException(nameof(which), "Channel must be non-negative.");
        }

        return tag < 0 ? throw new ArgumentOutOfRangeException(nameof(tag), "Tag must be non-negative.") : Mix_GroupChannel(which, tag);
    }

    public static bool GroupChannels(int from, int to, int tag) {
        if (from < 0) {
            throw new ArgumentOutOfRangeException(nameof(from), "From channel must be non-negative.");
        }

        if (to < 0) {
            throw new ArgumentOutOfRangeException(nameof(to), "To channel must be non-negative.");
        }

        return tag < 0 
            ? throw new ArgumentOutOfRangeException(nameof(tag), "Tag must be non-negative.")
            : Mix_GroupChannels(from, to, tag);
    }

    public static int GroupCount(int tag) {
        return tag < 0 
            ? throw new ArgumentOutOfRangeException(nameof(tag), "Tag must be non-negative.") 
            : Mix_GroupCount(tag);
    }

    public static int GroupNewer(int tag) {
        return tag < 0 
            ? throw new ArgumentOutOfRangeException(nameof(tag), "Tag must be non-negative.") 
            : Mix_GroupNewer(tag);
    }

    public static int GroupOldest(int tag) {
        return tag < 0 
            ? throw new ArgumentOutOfRangeException(nameof(tag), "Tag must be non-negative.") 
            : Mix_GroupOldest(tag);
    }

    public static void HaltChannel(int channel) {
        if (channel < 0) {
            throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be non-negative.");
        }

        Mix_HaltChannel(channel);
    }

    public static void HaltGroup(int tag) {
        if (tag < 0) {
            throw new ArgumentOutOfRangeException(nameof(tag), "Tag must be non-negative.");
        }

        Mix_HaltGroup(tag);
    }

    public static void HaltMusic() => Mix_HaltMusic();

    public static void HookMusic(MixMixCallback mixFunc, nint arg) {
        if (mixFunc == null) {
            throw new ArgumentNullException(nameof(mixFunc), "Callback cannot be null.");
        }

        Mix_HookMusic(mixFunc, arg);
    }

    public static void HookMusicFinished(MusicFinishedCallback musicFinished) {
        if (musicFinished == null) {
            throw new ArgumentNullException(nameof(musicFinished), "Callback cannot be null.");
        }

        Mix_HookMusicFinished(musicFinished);
    }

    public static int MasterVolume(int volume) {
        return volume is < 0 or > MaxVolume 
            ? throw new ArgumentOutOfRangeException(nameof(volume), $"Volume must be between 0 and {MaxVolume}.") 
            : Mix_MasterVolume(volume);
    }

    /// <summary>
    /// This is the version number macro for the current SDL_mixer version.
    /// </summary>
    public static string MixerVersion() => $"{Major}.{Minor}.{Micro}";

    /// <summary>
    /// Checks if the compiled SDL3_mixer version is at least the specified version.
    /// </summary>
    /// <param name="major">The major version to check.</param>
    /// <param name="minor">The minor version to check.</param>
    /// <param name="micro">The micro version to check.</param>
    /// <returns>True if the compiled version is at least the specified version; otherwise, <see langword="false" />.</returns>
    public static bool MixerVersionAtLeast(int major, int minor, int micro) =>
        (Major >= major) &&
        (Major > major || Minor >= minor) &&
        (Major > major || Minor > minor || Micro >= micro);

    #region Initialization and Cleanup

    /// <summary>
    /// Closes the currently open audio device, halting all playback.
    /// </summary>
    /// <remarks>
    /// This triggers any registered channel and music finished callbacks.
    /// Ensure all <see cref="Chunk"/> and <see cref="Music"/> objects are disposed before calling.
    /// </remarks>
    public static void CloseAudio() => Mix_CloseAudio();

    /// <summary>
    /// Initializes SDL3_mixer with the specified audio format support.
    /// </summary>
    /// <param name="flags">Flags indicating which audio formats to initialize (e.g., <see cref="MixInit.MIX_INIT_WAV"/>).</param>
    /// <returns>The flags that were successfully initialized.</returns>
    /// <exception cref="SdlException">Thrown if initialization fails for any requested flags.</exception>
    /// <remarks>
    /// Call this before opening an audio device to ensure support for specific audio formats.
    /// You can call this multiple times to initialize additional formats.
    /// Use <see cref="Quit"/> to deinitialize when done.
    /// </remarks>
    public static MixInit Initialize(MixInit flags) {
        MixInit result = Mix_Init(flags);
        if (result != flags) {
            Sdl.LogError(LogCategory.Audio, $"Failed to initialize SDL3_mixer with flags {flags}: {Sdl.GetError()}");
        }

        return result;
    }

    /// <summary>
    /// Opens an audio device for playback with the specified settings.
    /// </summary>
    /// <param name="deviceId">The ID of the audio device to open, or 0 for the default device.</param>
    /// <param name="spec">The desired audio specification, or null for default settings.</param>
    /// <exception cref="SdlException">Thrown if the audio device cannot be opened.</exception>
    /// <remarks>
    /// Call <see cref="CloseAudio"/> when done to close the device.
    /// If <paramref name="spec"/> is null, SDL3_mixer uses reasonable defaults.
    /// </remarks>
    public static void OpenAudio(AudioDeviceId deviceId, AudioSpec? spec = null) {
        AudioSpec nativeSpec = spec ?? default;
        bool result = Mix_OpenAudio(deviceId, spec.HasValue ? (nint)Unsafe.AsPointer(ref nativeSpec) : nint.Zero);
        if (!result) {
            Sdl.LogError(LogCategory.Audio, $"Failed to open audio device: {Sdl.GetError()}");
        }
    }

    /// <summary>
    /// Deinitializes SDL3_mixer, unloading all audio format support.
    /// </summary>
    /// <remarks>
    /// Call this after closing all audio devices and freeing resources.
    /// This method is safe to call multiple times.
    /// </remarks>
    public static void Quit() => Mix_Quit();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial MixInit Mix_Init(MixInit flags);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(Sdl.BoolType)]
    private static partial bool Mix_OpenAudio(AudioDeviceId deviceId, nint spec);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_Quit();

    #endregion Initialization and Cleanup

    #region Audio Loading

    public static void FreeChunk(Chunk chunk) {
        if (chunk.AudioBuffer == nint.Zero) {
            Sdl.LogError(LogCategory.Error, "Chunk already freed");
            return;
        }
        FreeChunk((nint)Unsafe.AsPointer(ref chunk));
    }

    public static void FreeChunk(nint chunk) {
        if (chunk == nint.Zero) {
            Sdl.LogError(LogCategory.Error, "Chunk already freed");
            return;
        }

        Mix_FreeChunk(chunk);
    }

    public static void FreeMusic(Music music) {
        if (music.Interface == nint.Zero) {
            Sdl.LogError(LogCategory.Error, "Music already freed");
            return;
        }

        FreeMusic((nint)Unsafe.AsPointer(ref music));
    }

    public static void FreeMusic(nint music) {
        if (music == nint.Zero) {
            Sdl.LogError(LogCategory.Error, "Music already freed");
            return;
        }

        Mix_FreeMusic(music);
    }

    /// <summary>
    /// Loads a music object from a file.
    /// </summary>
    /// <param name="filePath">The path to the music file (e.g., MP3, OGG).</param>
    /// <returns>A <see cref="Music"/> object representing the loaded music.</returns>
    /// <exception cref="SdlException">Thrown if the file cannot be loaded.</exception>
    /// <remarks>
    /// Dispose of the returned <see cref="Music"/> when no longer needed.
    /// </remarks>
    public static Music LoadMusic(string filePath) {
        if (string.IsNullOrEmpty(filePath)) {
            throw new ArgumentNullException(nameof(filePath));
        }

        nint musicPtr = Mix_LoadMUS(filePath);
        if (musicPtr == nint.Zero) {
            Sdl.LogError(LogCategory.Audio, $"Failed to load music file: {Sdl.GetError()}");
            return default; // Return default Music struct instead of comparing.
        }

        // Use unsafe code to avoid runtime marshalling
        Music music = *(Music*)musicPtr;

        if (music.Interface != nint.Zero) return music;
        
        Sdl.LogError(LogCategory.Audio, $"Failed to load music file: {Sdl.GetError()}");
        return default; // Return default Music struct instead of comparing.

    }

    /// <summary>
    /// Loads an audio chunk from a file.
    /// </summary>
    /// <param name="filePath">The path to the audio file (e.g., WAV).</param>
    /// <returns>A <see cref="Chunk"/> object representing the loaded audio.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the file path is null or empty.</exception>
    /// <exception cref="SdlException">Thrown if the file cannot be loaded.</exception>
    /// <remarks>
    /// Dispose of the returned <see cref="Chunk"/> when no longer needed.
    /// </remarks>
    /// <summary>Loads a WAV from a file path.</summary>

    /// <param name="path">the file path of the WAV file to open.</param>
    /// <param name="spec">a pointer to an SDL_AudioSpec that will be set to the WAVE data's format details on successful return.</param>
    /// <param name="audio_buf">a pointer filled with the audio data, allocated by the function.</param>
    /// <param name="audio_len">a pointer filled with the length of the audio data buffer in bytes.</param>
    /// <remarks>
    /// This is a convenience function that is effectively the same as:
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="free"/>
    /// <seealso cref="LoadWAV_IO"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success. audio_buf will be filled with a pointerto an allocated buffer containing the audio data, and audio_len is filledwith the length of that audio buffer in bytes.</returns>

    public static unsafe Chunk LoadWav(string filePath) {
        if (string.IsNullOrEmpty(filePath)) {
            throw new ArgumentNullException(nameof(filePath));
        }

        nint chunkPtr = Mix_LoadWAV(filePath);
        if (chunkPtr == nint.Zero) {
            Sdl.LogError(LogCategory.Audio, $"Failed to load WAV file: {Sdl.GetError()}");
            return default;
        }

        Chunk chunk = *(Chunk*)chunkPtr;
        if (chunk.AudioBuffer != nint.Zero) return chunk;
        
        Sdl.LogError(LogCategory.Audio, $"Failed to load WAV file: {Sdl.GetError()}");
        return default;

    }

    /// <summary>
    /// Plays an audio chunk on a specific channel or the first available channel.
    /// </summary>
    /// <param name="chunk">The <see cref="Chunk"/> to play.</param>
    /// <param name="loops">The number of times to loop the sound (-1 for infinite, 0 for once).</param>
    /// <param name="channel">The channel to play on, or -1 for the first available channel.</param>
    /// <returns>The channel used for playback, or -1 if no channel was available.</returns>
    /// <exception cref="SdlException">Thrown if playback fails.</exception>
    public static int PlayChannel(Chunk chunk, int loops, int channel = -1) {
        if (chunk.AudioBuffer == nint.Zero) {
            Sdl.LogError(LogCategory.Error, "Null Chunk");
            return -1;
        }

        nint pChunk = (nint)Unsafe.AsPointer(ref chunk);

        int result = Mix_PlayChannel(channel, pChunk, loops);
        if (result == -1 && channel == -1) {
            throw new SdlException($"No available channel for playback: {Sdl.GetError()}");
        }

        return result;
    }

    public static int PlayChannel(int channel, Chunk chunk, int loops) {
        return PlayChannel(chunk, loops, channel);
    }

    public static int PlayChannelTimed(int channel, Chunk chunk, int loops, int ticks) {
        if (chunk.AudioBuffer == nint.Zero) {
            Sdl.LogError(LogCategory.Error, "Null Chunk");
            return -1;
        }

        nint pChunk = (nint)Unsafe.AsPointer(ref chunk);

        return Mix_PlayChannelTimed(channel, pChunk, loops, ticks);
    }

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_FreeChunk(nint chunk);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_FreeMusic(nint music);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint Mix_LoadMUS(string file);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint Mix_LoadWAV(string file);

    #endregion Audio Loading

    #region Effects

    /// <summary>
    /// Registers a custom audio effect for a channel or the final mix.
    /// </summary>
    /// <param name="channel">The channel to apply the effect to, or <see cref="CHANNEL_POST"/> for the final mix.</param>
    /// <param name="effect">The effect callback that processes audio data.</param>
    /// <param name="done">The callback invoked when the effect is unregistered (optional).</param>
    /// <param name="userData">User-defined data passed to the callbacks.</param>
    /// <exception cref="SdlException">Thrown if the effect cannot be registered.</exception>
    /// <remarks>
    /// The effect callback must be kept alive for the duration of its use.
    /// </remarks>
    public static void RegisterEffect(int channel, MixEffectFuncT effect, MixEffectDoneT done, nint userData) {
        if (!Mix_RegisterEffect(channel, effect, done, userData)) {
            throw new SdlException($"Failed to register effect: {Sdl.GetError()}");
        }
    }

    /// <summary>
    /// Sets the stereo panning for a specific channel or the final mix.
    /// </summary>
    /// <param name="channel">The channel to apply panning to, or <see cref="CHANNEL_POST"/> for the final mix.</param>
    /// <param name="left">The volume of the left channel (0 to 255, where 0 is silent and 255 is full volume).</param>
    /// <param name="right">The volume of the right channel (0 to 255, where 0 is silent and 255 is full volume).</param>
    /// <exception cref="SdlException">Thrown if panning cannot be set.</exception>
    /// <remarks>
    /// This has no effect if the audio device is not in stereo mode.
    /// </remarks>
    public static void SetPanning(int channel, byte left, byte right) {
        if (!Mix_SetPanning(channel, left, right)) {
            throw new SdlException($"Failed to set panning: {Sdl.GetError()}");
        }
    }

    #endregion Effects

    #region Constants

    /// <summary>
    /// The default number of mixing channels.
    /// </summary>
    public const int Channels = 8;

    /// <summary>
    /// The default number of audio channels (stereo).
    /// </summary>
    public const int DefaultChannels = 2;

    /// <summary>
    /// The default audio format (16-bit signed).
    /// </summary>
    public const AudioFormat DefaultFormat = AudioFormat.S16;

    /// <summary>
    /// The default audio frequency (44100 Hz).
    /// </summary>
    public const int DefaultFrequency = 44100;

    /// <summary>
    /// The maximum volume for a chunk or channel (128).
    /// </summary>
    public const int MaxVolume = 128;

    /// <summary>
    /// Special channel value for applying effects to the final mixed stream.
    /// </summary>
    public const int MixChannelPost = -2;

    /// <summary>
    /// Alias for <see cref="MaxVolume"/>.
    /// </summary>
    public const int MixMaxVolume = 128;

    #endregion Constants

    /**
     * Set a function that is called after all mixing is performed.
     *
     * This can be used to provide real-time visual display of the audio stream or
     * add a custom mixer filter for the stream data.
     *
     * The callback will fire every time SDL_mixer is ready to supply more data to
     * the audio device, after it has finished all its mixing work. This runs
     * inside an SDL audio callback, so it's important that the callback return
     * quickly, or there could be problems in the audio playback.
     *
     * The data provided to the callback is in the format that the audio device
     * was opened in, and it represents the exact waveform SDL_mixer has mixed
     * from all playing chunks and music for playback. You are allowed to modify
     * the data, but it cannot be resized (so you can't add a reverb effect that
     * goes past the end of the buffer without saving some state between runs to
     * add it into the next callback, or resample the buffer to a smaller size to
     * speed it up, etc).
     *
     * The `arg` pointer supplied here is passed to the callback as-is, for
     * whatever the callback might want to do with it (keep track of some ongoing
     * state, settings, etc).
     *
     * Passing a <see langword="null" /> callback disables the post-mix callback until such a time as
     * a new one callback is set.
     *
     * There is only one callback available. If you need to mix multiple inputs,
     * be prepared to handle them from a single function.
     *
     * \param mix_func the callback function to become the new post-mix callback.
     * \param arg a pointer that is passed, untouched, to the callback.
     *
     * \since This function is available since SDL_mixer 3.0.0.
     *
     * \sa Mix_HookMusic
     */

    public static bool ModMusicJumpToOrder(int order) {
        return order < 0
            ? throw new ArgumentOutOfRangeException(nameof(order), "Order must be non-negative.")
            : Mix_ModMusicJumpToOrder(order);
    }

    public static double MusicDuration(Music music) {
        return music.Interface == nint.Zero
            ? throw new ArgumentNullException(nameof(music))
            : Mix_MusicDuration((nint)Unsafe.AsPointer(ref music));
    }

    public static double MusicDuration(nint music) {
        return music == nint.Zero
            ? throw new ArgumentNullException(nameof(music))
            : Mix_MusicDuration(music);
    }

    public static void Pause(int channel) {
        if (channel < 0) {
            throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be non-negative.");
        }

        Mix_Pause(channel);
    }

    public static int Paused(int channel) {
        return channel < 0
            ? throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be non-negative.")
            : Mix_Paused(channel);
    }

    public static bool PausedMusic() => Mix_PausedMusic();

    public static void PauseGroup(int tag) {
        if (tag < 0) {
            throw new ArgumentOutOfRangeException(nameof(tag), "Tag must be non-negative.");
        }

        Mix_PauseGroup(tag);
    }

    public static void PauseMusic() => Mix_PauseMusic();

    public static int Playing(int channel) {
        return channel < 0 
            ? throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be non-negative.") 
            : Mix_Playing(channel);
    }

    public static bool PlayingMusic() => Mix_PlayingMusic();

    /// <summary>
    /// Plays a music object.
    /// </summary>
    /// <param name="music">The <see cref="Music"/> to play.</param>
    /// <param name="loops">The number of times to loop the music (0 for once, -1 for infinite).</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="music"/> is null.</exception>
    /// <exception cref="SdlException">Thrown if playback fails.</exception>
    /// <remarks>
    /// Only one music object can play at a time. This halts any currently playing music.
    /// </remarks>
    public static bool PlayMusic(Music music, int loops) {
        return PlayMusic((nint)Unsafe.AsPointer(ref music), loops);
    }

    public static bool PlayMusic(nint music, int loops) {
        if (music == nint.Zero) {
            Sdl.LogError(LogCategory.Error, "Null Music");
            return false;
        }

        bool result = Mix_PlayMusic(music, loops);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to play music: {Sdl.GetError()}");
        }

        return result;
    }

    public static int ReserveChannels(int num) {
        return num < 0
            ? throw new ArgumentOutOfRangeException(nameof(num), "Number of channels must be non-negative.")
            : Mix_ReserveChannels(num);
    }

    public static void Resume(int channel) {
        if (channel < 0) {
            throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be non-negative.");
        }

        Mix_Resume(channel);
    }

    public static void ResumeGroup(int tag) {
        if (tag < 0) {
            throw new ArgumentOutOfRangeException(nameof(tag), "Tag must be non-negative.");
        }

        Mix_ResumeGroup(tag);
    }

    public static void ResumeMusic() => Mix_ResumeMusic();

    public static void RewindMusic() => Mix_RewindMusic();

    public static bool SetDistance(int channel, byte distance) {
        return channel < 0
            ? throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be non-negative.")
            : Mix_SetDistance(channel, distance);
    }

    public static bool SetMusicPosition(double position) {
        return position < 0 ? throw new ArgumentOutOfRangeException(nameof(position), "Position must be non-negative.") : Mix_SetMusicPosition(position);
    }

    public static bool SetPosition(int channel, short angle, byte distance) {
        if (channel < 0) {
            throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be non-negative.");
        }

        return angle is < -360 or > 360
            ? throw new ArgumentOutOfRangeException(nameof(angle), "Angle must be between -360 and 360.")
            : Mix_SetPosition(channel, angle, distance);
    }

    public static void SetPostMix(MixMixCallback mixFunc, nint arg) {
        if (mixFunc == null) {
            throw new ArgumentNullException(nameof(mixFunc), "Callback cannot be null.");
        }

        Mix_SetPostMix(mixFunc, arg);
    }

    public static bool SetReverseStereo(int channel, int flip) {
        if (channel < 0) {
            throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be non-negative.");
        }

        return flip is < 0 or > 1
            ? throw new ArgumentOutOfRangeException(nameof(flip), "Flip must be either 0 or 1.")
            : Mix_SetReverseStereo(channel, flip);
    }

    public static bool SetSoundFonts(string paths) {
        return string.IsNullOrEmpty(paths)
            ? throw new ArgumentNullException(nameof(paths), "Paths cannot be null or empty.")
            : Mix_SetSoundFonts(paths);
    }

    public static bool SetTimidityCfg(string path) {
        return string.IsNullOrEmpty(path) 
            ? throw new ArgumentNullException(nameof(path), "Path cannot be null or empty.") 
            : Mix_SetTimidityCfg(path);
    }

    public static bool StartTrack(Music music, int track) {
        if (music.Interface == nint.Zero) {
            throw new ArgumentNullException(nameof(music));
        }

        return track < 0 
            ? throw new ArgumentOutOfRangeException(nameof(track), "Track must be non-negative.") 
            : Mix_StartTrack((nint)Unsafe.AsPointer(ref music), track);
    }

    public static bool UnregisterAllEffects(int channel) {
        return channel < 0 
            ? throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be non-negative.") 
            : Mix_UnregisterAllEffects(channel);
    }

    public static bool UnregisterEffect(int channel, MixEffectFuncT f) {
        if (channel < 0) {
            throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be non-negative.");
        }

        return f == null 
            ? throw new ArgumentNullException(nameof(f), "Effect function cannot be null.") 
            : Mix_UnregisterEffect(channel, f);
    }

    public static int Volume(int channel, int volume) {
        if (channel < 0) {
            throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be non-negative.");
        }

        return volume is < 0 or > MaxVolume 
            ? throw new ArgumentOutOfRangeException(nameof(volume), $"Volume must be between 0 and {MaxVolume}.") 
            : Mix_Volume(channel, volume);
    }

    public static int VolumeChunk(Chunk chunk, int volume) {
        return VolumeChunk((nint)Unsafe.AsPointer(ref chunk), volume);
    }

    public static int VolumeChunk(nint chunk, int volume) {
        if (chunk != nint.Zero) return Mix_VolumeChunk(chunk, volume);

        Sdl.LogError(LogCategory.Error, "Null Chunk");
        return -1;
    }

    public static int VolumeMusic(int volume) {
        return volume is < 0 or > MaxVolume 
            ? throw new ArgumentOutOfRangeException(nameof(volume), $"Volume must be between 0 and {MaxVolume}.") 
            : Mix_VolumeMusic(volume);
    }

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_ChannelFinished(MixChannelFinishedCallback channelFinished);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_CloseAudio();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(Sdl.BoolType)]
    private static partial bool Mix_EachSoundFont(MixEachSoundFontCallback function, nint data);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_ExpireChannel(int channel, int ticks);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_FadeInChannel(int channel, nint chunk, int loops, int ms);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_FadeInChannelTimed(int channel, nint chunk, int loops, int ms, int ticks);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(Sdl.BoolType)]
    private static partial bool Mix_FadeInMusic(nint music, int loops, int ms);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(Sdl.BoolType)]
    private static partial bool Mix_FadeInMusicPos(nint music, int loops, int ms, double position);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_FadeOutChannel(int which, int ms);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_FadeOutGroup(int tag, int ms);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(Sdl.BoolType)]
    private static partial bool Mix_FadeOutMusic(int ms);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Fading Mix_FadingChannel(int which);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Fading Mix_FadingMusic();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint Mix_GetChunk(int channel);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint Mix_GetMusicHookData();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double Mix_GetMusicLoopEndTime(nint music);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double Mix_GetMusicLoopLengthTime(nint music);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double Mix_GetMusicLoopStartTime(nint music);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double Mix_GetMusicPosition(nint music);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_GetMusicVolume(nint music);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_GetNumTracks(nint music);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial string Mix_GetSoundFonts();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string Mix_GetTimidityCfg();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_GroupAvailable(int tag);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(Sdl.BoolType)]
    private static partial bool Mix_GroupChannel(int which, int tag);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(Sdl.BoolType)]
    private static partial bool Mix_GroupChannels(int from, int to, int tag);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_GroupCount(int tag);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_GroupNewer(int tag);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_GroupOldest(int tag);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_HaltChannel(int channel);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_HaltGroup(int tag);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_HaltMusic();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_HookMusic(MixMixCallback mixFunc, nint arg);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_HookMusicFinished(MusicFinishedCallback musicFinished);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_MasterVolume(int volume);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(Sdl.BoolType)]
    private static partial bool Mix_ModMusicJumpToOrder(int order);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double Mix_MusicDuration(nint music);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_Pause(int channel);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_Paused(int channel);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(Sdl.BoolType)]
    private static partial bool Mix_PausedMusic();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_PauseGroup(int tag);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_PauseMusic();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_PlayChannel(int channel, nint chunk, int loops);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_PlayChannelTimed(int channel, nint chunk, int loops, int ticks);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_Playing(int channel);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(Sdl.BoolType)]
    private static partial bool Mix_PlayingMusic();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(Sdl.BoolType)]
    private static partial bool Mix_PlayMusic(nint music, int loops);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(Sdl.BoolType)]
    private static partial bool Mix_RegisterEffect(int chan, MixEffectFuncT f, MixEffectDoneT d, nint arg);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_ReserveChannels(int num);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_Resume(int channel);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_ResumeGroup(int tag);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_ResumeMusic();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_RewindMusic();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(Sdl.BoolType)]
    private static partial bool Mix_SetDistance(int channel, byte distance);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(Sdl.BoolType)]
    private static partial bool Mix_SetMusicPosition(double position);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(Sdl.BoolType)]
    private static partial bool Mix_SetPanning(int channel, byte left, byte right);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(Sdl.BoolType)]
    private static partial bool Mix_SetPosition(int channel, short angle, byte distance);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_SetPostMix(MixMixCallback mixFunc, nint arg);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(Sdl.BoolType)]
    private static partial bool Mix_SetReverseStereo(int channel, int flip);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(Sdl.BoolType)]
    private static partial bool Mix_SetSoundFonts(string paths);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(Sdl.BoolType)]
    private static partial bool Mix_SetTimidityCfg(string path);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(Sdl.BoolType)]
    private static partial bool Mix_StartTrack(nint music, int track);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(Sdl.BoolType)]
    private static partial bool Mix_UnregisterAllEffects(int channel);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(Sdl.BoolType)]
    private static partial bool Mix_UnregisterEffect(int channel, MixEffectFuncT f);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_Volume(int channel, int volume);

    /**
     * Set the volume for a specific chunk.
     *
     * In addition to channels having a volume setting, individual chunks also
     * maintain a separate volume. Both values are considered when mixing, so both
     * affect the final attenuation of the sound. This lets an app adjust the
     * volume for all instances of a sound in addition to specific instances of
     * that sound.
     *
     * The volume must be between 0 (silence) and MIX_MAX_VOLUME (full volume).
     * Note that MIX_MAX_VOLUME is 128. Values greater than MIX_MAX_VOLUME are
     * clamped to MIX_MAX_VOLUME.
     *
     * Specifying a negative volume will not change the current volume; as such,
     * this can be used to query the current volume without making changes, as
     * this function returns the previous (in this case, still-current) value.
     *
     * The default volume for a chunk is MIX_MAX_VOLUME (no attenuation).
     *
     * \param chunk the chunk whose volume to adjust.
     * \param volume the new volume, between 0 and MIX_MAX_VOLUME, or -1 to query.
     * \returns the previous volume. If the specified volume is -1, this returns
     *          the current volume. If `chunk` is <see langword="null" />, this returns -1.
     *
     * \since This function is available since SDL_mixer 3.0.0.
     */
    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_VolumeChunk(nint chunk, int volume);

    /**
     * Set the volume for the music channel.
     *
     * The volume must be between 0 (silence) and MIX_MAX_VOLUME (full volume).
     * Note that MIX_MAX_VOLUME is 128. Values greater than MIX_MAX_VOLUME are
     * clamped to MIX_MAX_VOLUME.
     *
     * Specifying a negative volume will not change the current volume; as such,
     * this can be used to query the current volume without making changes, as
     * this function returns the previous (in this case, still-current) value.
     *
     * The default volume for music is MIX_MAX_VOLUME (no attenuation).
     *
     * \param volume the new volume, between 0 and MIX_MAX_VOLUME, or -1 to query.
     * \returns the previous volume. If the specified volume is -1, this returns
     *          the current volume.
     *
     * \since This function is available since SDL_mixer 3.0.0.
     */
    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_VolumeMusic(int volume);
}