using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SharpSDL3.Mixer;

public static unsafe partial class Mixer {
    public const string EffectMaxSpeed = "MIX_EFFECTSMAXSPEED";

    /// <summary>
    /// Printable format: "%d.%d.%d", MAJOR, MINOR, MICRO
    /// </summary>
    public const int Major = 3;

    public const int Micro = 0;
    public const int Minor = 0;
    public const string NativeLibName = "SDL3_mixer";

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void Mix_ChannelFinishedCallback(int channel);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool Mix_EachSoundFontCallback(string a, void* b);

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
    public delegate void Mix_EffectDone_t(int chan, void* udata);

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
    public delegate void Mix_EffectFunc_t(int chan, void* stream, int len, void* udata);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void Mix_MixCallback(void* udata, byte* stream, int len);

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
    public static void ChannelFinished(Mix_ChannelFinishedCallback channelFinishedCallback) {
        if (channelFinishedCallback == null) {
            throw new ArgumentNullException(nameof(channelFinishedCallback), "Callback cannot be null.");
        }

        Mix_ChannelFinished(channelFinishedCallback);
    }

    public static void EffectDone(int channel, void* udata) {
        if (udata == null)
            throw new ArgumentNullException(nameof(udata));
        Mix_EffectDone_t effectDone = Marshal.GetDelegateForFunctionPointer<Mix_EffectDone_t>((nint)udata);
        effectDone(channel, udata);
    }

    public static int ExpireChannel(int channel, int ticks) {
        if (ticks < 0)
            throw new ArgumentOutOfRangeException(nameof(ticks), "Ticks must be non-negative.");
        return Mix_ExpireChannel(channel, ticks);
    }

    public static int FadeInChannel(int channel, Chunk chunk, int loops, int ms) {
        if (chunk.AudioBuffer == null) // Check if the chunk is uninitialized by verifying its pointer.
            throw new ArgumentNullException(nameof(chunk));
        return Mix_FadeInChannel(channel, &chunk, loops, ms);
    }

    public static int FadeInChannelTimed(int channel, Chunk chunk, int loops, int ms, int ticks) {
        if (chunk.AudioBuffer == null) // Check if the chunk is uninitialized by verifying its pointer.
            throw new ArgumentNullException(nameof(chunk));
        return Mix_FadeInChannelTimed(channel, &chunk, loops, ms, ticks);
    }

    public static bool FadeInMusic(Music music, int loops, int ms) {
        if (music.@interface == null)
            throw new ArgumentNullException(nameof(music));
        return Mix_FadeInMusic((Music*)Unsafe.AsPointer(ref music), loops, ms);
    }

    public static bool FadeInMusicPos(Music music, int loops, int ms, double position) {
        if (music.@interface == null)
            throw new ArgumentNullException(nameof(music));
        return Mix_FadeInMusicPos((Music*)Unsafe.AsPointer(ref music), loops, ms, position);
    }

    public static int FadeOutChannel(int which, int ms) {
        if (ms < 0)
            throw new ArgumentOutOfRangeException(nameof(ms), "Milliseconds must be non-negative.");
        return Mix_FadeOutChannel(which, ms);
    }

    public static int FadeOutGroup(int tag, int ms) {
        if (ms < 0)
            throw new ArgumentOutOfRangeException(nameof(ms), "Milliseconds must be non-negative.");
        return Mix_FadeOutGroup(tag, ms);
    }

    public static bool FadeOutMusic(int ms) {
        if (ms < 0)
            throw new ArgumentOutOfRangeException(nameof(ms), "Milliseconds must be non-negative.");
        return Mix_FadeOutMusic(ms);
    }

    public static Fading FadingChannel(int which) {
        if (which < 0)
            throw new ArgumentOutOfRangeException(nameof(which), "Channel must be non-negative.");
        return Mix_FadingChannel(which);
    }

    public static Fading FadingMusic() => Mix_FadingMusic();

    public static Chunk* GetChunk(int channel) {
        if (channel < 0)
            throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be non-negative.");
        return Mix_GetChunk(channel);
    }

    public static void* GetMusicHookData() => Mix_GetMusicHookData();

    public static double GetMusicLoopEndTime(Music music) {
        if (music.@interface == null)
            throw new ArgumentNullException(nameof(music));
        return Mix_GetMusicLoopEndTime((Music*)Unsafe.AsPointer(ref music));
    }

    public static double GetMusicLoopEndTime(Music* music) {
        if (music == null)
            throw new ArgumentNullException(nameof(music));
        return Mix_GetMusicLoopEndTime(music);
    }

    public static double GetMusicLoopLengthTime(Music music) {
        if (music.@interface == null)
            throw new ArgumentNullException(nameof(music));
        return Mix_GetMusicLoopLengthTime((Music*)Unsafe.AsPointer(ref music));
    }

    public static double GetMusicLoopLengthTime(Music* music) {
        if (music == null)
            throw new ArgumentNullException(nameof(music));
        return Mix_GetMusicLoopLengthTime(music);
    }

    public static double GetMusicLoopStartTime(Music music) {
        if (music.@interface == null)
            throw new ArgumentNullException(nameof(music));
        return Mix_GetMusicLoopStartTime((Music*)Unsafe.AsPointer(ref music));
    }

    public static double GetMusicLoopStartTime(Music* music) {
        if (music == null)
            throw new ArgumentNullException(nameof(music));
        return Mix_GetMusicLoopStartTime(music);
    }

    public static double GetMusicPosition(Music music) {
        if (music.@interface == null)
            throw new ArgumentNullException(nameof(music));
        return Mix_GetMusicPosition((Music*)Unsafe.AsPointer(ref music));
    }

    public static double GetMusicPosition(Music* music) {
        if (music == null)
            throw new ArgumentNullException(nameof(music));
        return Mix_GetMusicPosition(music);
    }

    public static int GetMusicVolume(Music music) {
        if (music.@interface == null)
            throw new ArgumentNullException(nameof(music));
        return Mix_GetMusicVolume((Music*)Unsafe.AsPointer(ref music));
    }

    public static int GetMusicVolume(Music* music) {
        if (music == null)
            throw new ArgumentNullException(nameof(music));
        return Mix_GetMusicVolume(music);
    }

    public static int GetNumTracks(Music music) {
        if (music.@interface == null)
            throw new ArgumentNullException(nameof(music));
        return Mix_GetNumTracks((Music*)Unsafe.AsPointer(ref music));
    }

    public static int GetNumTracks(Music* music) {
        if (music == null)
            throw new ArgumentNullException(nameof(music));
        return Mix_GetNumTracks(music);
    }

    public static string GetSoundFonts() {
        string soundFonts = Mix_GetSoundFonts();
        if (string.IsNullOrEmpty(soundFonts))
            throw new SdlException($"Failed to get sound fonts: {Sdl.GetError()}");
        return soundFonts;
    }

    public static string GetTimidityCfg() {
        string timidityCfg = Mix_GetTimidityCfg();
        if (string.IsNullOrEmpty(timidityCfg))
            throw new SdlException($"Failed to get Timidity configuration: {Sdl.GetError()}");
        return timidityCfg;
    }

    public static int GroupAvailable(int tag) {
        if (tag < 0)
            throw new ArgumentOutOfRangeException(nameof(tag), "Tag must be non-negative.");
        return Mix_GroupAvailable(tag);
    }

    public static bool GroupChannel(int which, int tag) {
        if (which < 0)
            throw new ArgumentOutOfRangeException(nameof(which), "Channel must be non-negative.");
        if (tag < 0)
            throw new ArgumentOutOfRangeException(nameof(tag), "Tag must be non-negative.");
        return Mix_GroupChannel(which, tag);
    }

    public static bool GroupChannels(int from, int to, int tag) {
        if (from < 0)
            throw new ArgumentOutOfRangeException(nameof(from), "From channel must be non-negative.");
        if (to < 0)
            throw new ArgumentOutOfRangeException(nameof(to), "To channel must be non-negative.");
        if (tag < 0)
            throw new ArgumentOutOfRangeException(nameof(tag), "Tag must be non-negative.");
        return Mix_GroupChannels(from, to, tag);
    }

    public static int GroupCount(int tag) {
        if (tag < 0)
            throw new ArgumentOutOfRangeException(nameof(tag), "Tag must be non-negative.");
        return Mix_GroupCount(tag);
    }

    public static int GroupNewer(int tag) {
        if (tag < 0)
            throw new ArgumentOutOfRangeException(nameof(tag), "Tag must be non-negative.");
        return Mix_GroupNewer(tag);
    }

    public static int GroupOldest(int tag) {
        if (tag < 0)
            throw new ArgumentOutOfRangeException(nameof(tag), "Tag must be non-negative.");
        return Mix_GroupOldest(tag);
    }

    public static void HaltChannel(int channel) {
        if (channel < 0)
            throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be non-negative.");
        Mix_HaltChannel(channel);
    }

    public static void HaltGroup(int tag) {
        if (tag < 0)
            throw new ArgumentOutOfRangeException(nameof(tag), "Tag must be non-negative.");
        Mix_HaltGroup(tag);
    }

    public static void HaltMusic() => Mix_HaltMusic();

    public static void HookMusic(Mix_MixCallback mix_func, void* arg) {
        if (mix_func == null)
            throw new ArgumentNullException(nameof(mix_func), "Callback cannot be null.");
        Mix_HookMusic(mix_func, arg);
    }

    public static void HookMusicFinished(MusicFinishedCallback music_finished) {
        if (music_finished == null)
            throw new ArgumentNullException(nameof(music_finished), "Callback cannot be null.");
        Mix_HookMusicFinished(music_finished);
    }

    public static int MasterVolume(int volume) {
        if (volume < 0 || volume > MaxVolume)
            throw new ArgumentOutOfRangeException(nameof(volume), $"Volume must be between 0 and {MaxVolume}.");
        return Mix_MasterVolume(volume);
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
    /// <returns>True if the compiled version is at least the specified version; otherwise, false.</returns>
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
        if (result != flags)
            Logger.LogError(LogCategory.Audio, $"Failed to initialize SDL3_mixer with flags {flags}: {Sdl.GetError()}");
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
    public static void OpenAudio(AudioDeviceId deviceId = default, AudioSpec? spec = null) {
        AudioSpec nativeSpec = spec ?? default;
        bool result = Mix_OpenAudio(deviceId, spec.HasValue ? &nativeSpec : null);
        if (!result)
            Logger.LogError(LogCategory.Audio, $"Failed to open audio device: {Sdl.GetError()}");
    }

    /// <summary>
    /// Deinitializes SDL3_mixer, unloading all audio format support.
    /// </summary>
    /// <remarks>
    /// Call this after closing all audio devices and freeing resources.
    /// This method is safe to call multiple times.
    /// </remarks>
    public static void Quit() => Mix_Quit();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial MixInit Mix_Init(MixInit flags);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool Mix_OpenAudio(AudioDeviceId deviceId, AudioSpec* spec);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_Quit();

    #endregion Initialization and Cleanup

    #region Audio Loading

    public static void FreeChunk(Chunk chunk) {
        if (chunk.AudioBuffer == null) // Check if the chunk is uninitialized by verifying its pointer.
            throw new ArgumentNullException(nameof(chunk));
        Mix_FreeChunk(&chunk);
    }

    public static void FreeChunk(Chunk* chunk) {
        if (chunk == null)
            throw new ArgumentNullException(nameof(chunk));
        Mix_FreeChunk(chunk);
    }

    public static void FreeMusic(Music music) {
        if (music.@interface == null)
            throw new ArgumentNullException(nameof(music));
        Mix_FreeMusic((Music*)Unsafe.AsPointer(ref music));
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
        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentNullException(nameof(filePath));
        Music* musicPtr = Mix_LoadMUS(filePath);
        if (musicPtr == null) {
            Logger.LogError(LogCategory.Audio, $"Failed to load music file: {Sdl.GetError()}");
            return default; // Return default Music struct instead of comparing.
        }
        return *musicPtr;
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
    public static Chunk LoadWav(string filePath) {
        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentNullException(nameof(filePath));
        Chunk* chunkPtr = Mix_LoadWAV(filePath);
        if (chunkPtr == null) {
            Logger.LogError(LogCategory.Audio, $"Failed to load WAV file: {Sdl.GetError()}");
            return default;
        }
        return *chunkPtr;
    }

    /// <summary>
    /// Plays an audio chunk on a specific channel or the first available channel.
    /// </summary>
    /// <param name="chunk">The <see cref="Chunk"/> to play.</param>
    /// <param name="loops">The number of times to loop the sound (-1 for infinite, 0 for once).</param>
    /// <param name="channel">The channel to play on, or -1 for the first available channel.</param>
    /// <returns>The channel used for playback, or -1 if no channel was available.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="chunk"/> is null.</exception>
    /// <exception cref="SdlException">Thrown if playback fails.</exception>
    public static int PlayChannel(Chunk chunk, int loops, int channel = -1) {
        if (chunk.AudioBuffer == null) // Check if the chunk is uninitialized by verifying its pointer.
            throw new ArgumentNullException(nameof(chunk));
        int result = Mix_PlayChannel(channel, &chunk, loops);
        if (result == -1 && channel == -1)
            throw new SdlException($"No available channel for playback: {Sdl.GetError()}");
        return result;
    }

    public static int PlayChannel(int channel, Chunk chunk, int loops) {
        if (chunk.AudioBuffer == null) // Check if the chunk is uninitialized by verifying its pointer.
            throw new ArgumentNullException(nameof(chunk));
        return Mix_PlayChannel(channel, &chunk, loops);
    }

    public static int PlayChannelTimed(int channel, Chunk chunk, int loops, int ticks) {
        if (chunk.AudioBuffer == null) // Check if the chunk is uninitialized by verifying its pointer.
            throw new ArgumentNullException(nameof(chunk));
        return Mix_PlayChannelTimed(channel, &chunk, loops, ticks);
    }

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
    public static void PlayMusic(Music music, int loops) {
        if (!Mix_PlayMusic((Music*)Unsafe.AsPointer(ref music), loops))
            throw new SdlException($"Failed to play music: {Sdl.GetError()}");
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_FreeChunk(Chunk* chunk);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_FreeMusic(Music* music);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Music* Mix_LoadMUS(string file);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Chunk* Mix_LoadWAV(string file);

    #endregion Audio Loading

    #region Effects

    /// <summary>
    /// Registers a custom audio effect for a channel or the final mix.
    /// </summary>
    /// <param name="channel">The channel to apply the effect to, or <see cref="MIX_CHANNEL_POST"/> for the final mix.</param>
    /// <param name="effect">The effect callback that processes audio data.</param>
    /// <param name="done">The callback invoked when the effect is unregistered (optional).</param>
    /// <param name="userData">User-defined data passed to the callbacks.</param>
    /// <exception cref="SdlException">Thrown if the effect cannot be registered.</exception>
    /// <remarks>
    /// The effect callback must be kept alive for the duration of its use.
    /// </remarks>
    public static void RegisterEffect(int channel, Mix_EffectFunc_t effect, Mix_EffectDone_t done, IntPtr userData) {
        if (!Mix_RegisterEffect(channel, effect, done, userData.ToPointer()))
            throw new SdlException($"Failed to register effect: {Sdl.GetError()}");
    }

    /// <summary>
    /// Sets the stereo panning for a specific channel or the final mix.
    /// </summary>
    /// <param name="channel">The channel to apply panning to, or <see cref="MIX_CHANNEL_POST"/> for the final mix.</param>
    /// <param name="left">The volume of the left channel (0 to 255, where 0 is silent and 255 is full volume).</param>
    /// <param name="right">The volume of the right channel (0 to 255, where 0 is silent and 255 is full volume).</param>
    /// <exception cref="SdlException">Thrown if panning cannot be set.</exception>
    /// <remarks>
    /// This has no effect if the audio device is not in stereo mode.
    /// </remarks>
    public static void SetPanning(int channel, byte left, byte right) {
        if (!Mix_SetPanning(channel, left, right))
            throw new SdlException($"Failed to set panning: {Sdl.GetError()}");
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
    public const int MIX_CHANNEL_POST = -2;

    /// <summary>
    /// Alias for <see cref="MaxVolume"/>.
    /// </summary>
    public const int MIX_MAX_VOLUME = 128;

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
     * Passing a NULL callback disables the post-mix callback until such a time as
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
        if (order < 0)
            throw new ArgumentOutOfRangeException(nameof(order), "Order must be non-negative.");
        return Mix_ModMusicJumpToOrder(order);
    }

    public static double MusicDuration(Music music) {
        if (music.@interface == null)
            throw new ArgumentNullException(nameof(music));
        return Mix_MusicDuration((Music*)Unsafe.AsPointer(ref music));
    }

    public static double MusicDuration(Music* music) {
        if (music == null)
            throw new ArgumentNullException(nameof(music));
        return Mix_MusicDuration(music);
    }

    public static void Pause(int channel) {
        if (channel < 0)
            throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be non-negative.");
        Mix_Pause(channel);
    }

    public static int Paused(int channel) {
        if (channel < 0)
            throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be non-negative.");
        return Mix_Paused(channel);
    }

    public static bool PausedMusic() => Mix_PausedMusic();

    public static void PauseGroup(int tag) {
        if (tag < 0)
            throw new ArgumentOutOfRangeException(nameof(tag), "Tag must be non-negative.");
        Mix_PauseGroup(tag);
    }

    public static void PauseMusic() => Mix_PauseMusic();

    public static int PlayChannel(int channel, Chunk chunk, int loops) {
        if (chunk.AudioBuffer == null) // Check if the chunk is uninitialized by verifying its pointer.
            throw new ArgumentNullException(nameof(chunk));
        return Mix_PlayChannel(channel, &chunk, loops);
    }

    public static int PlayChannelTimed(int channel, Chunk chunk, int loops, int ticks) {
        if (chunk.AudioBuffer == null) // Check if the chunk is uninitialized by verifying its pointer.
            throw new ArgumentNullException(nameof(chunk));
        return Mix_PlayChannelTimed(channel, &chunk, loops, ticks);
    }

    public static int Playing(int channel) {
        if (channel < 0)
            throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be non-negative.");
        return Mix_Playing(channel);
    }

    public static bool PlayingMusic() => Mix_PlayingMusic();

    public static bool PlayMusic(Music* music, int loops) {
        if (music == null)
            throw new ArgumentNullException(nameof(music));
        return Mix_PlayMusic(music, loops);
    }

    public static int ReserveChannels(int num) {
        if (num < 0)
            throw new ArgumentOutOfRangeException(nameof(num), "Number of channels must be non-negative.");
        return Mix_ReserveChannels(num);
    }

    public static void Resume(int channel) {
        if (channel < 0)
            throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be non-negative.");
        Mix_Resume(channel);
    }

    public static void ResumeGroup(int tag) {
        if (tag < 0)
            throw new ArgumentOutOfRangeException(nameof(tag), "Tag must be non-negative.");
        Mix_ResumeGroup(tag);
    }

    public static void ResumeMusic() => Mix_ResumeMusic();

    public static void RewindMusic() => Mix_RewindMusic();

    public static bool SetDistance(int channel, byte distance) {
        if (channel < 0)
            throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be non-negative.");
        if (distance > 255)
            throw new ArgumentOutOfRangeException(nameof(distance), "Distance must be between 0 and 255.");
        return Mix_SetDistance(channel, distance);
    }

    public static bool SetMusicPosition(double position) {
        if (position < 0)
            throw new ArgumentOutOfRangeException(nameof(position), "Position must be non-negative.");
        return Mix_SetMusicPosition(position);
    }

    public static bool SetPosition(int channel, short angle, byte distance) {
        if (channel < 0)
            throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be non-negative.");
        if (angle < -360 || angle > 360)
            throw new ArgumentOutOfRangeException(nameof(angle), "Angle must be between -360 and 360.");
        if (distance > 255)
            throw new ArgumentOutOfRangeException(nameof(distance), "Distance must be between 0 and 255.");
        return Mix_SetPosition(channel, angle, distance);
    }

    public static void SetPostMix(Mix_MixCallback mix_func, void* arg) {
        if (mix_func == null)
            throw new ArgumentNullException(nameof(mix_func), "Callback cannot be null.");
        Mix_SetPostMix(mix_func, arg);
    }

    public static bool SetReverseStereo(int channel, int flip) {
        if (channel < 0)
            throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be non-negative.");
        if (flip < 0 || flip > 1)
            throw new ArgumentOutOfRangeException(nameof(flip), "Flip must be either 0 or 1.");
        return Mix_SetReverseStereo(channel, flip);
    }

    public static bool SetSoundFonts(string paths) {
        if (string.IsNullOrEmpty(paths))
            throw new ArgumentNullException(nameof(paths), "Paths cannot be null or empty.");
        return Mix_SetSoundFonts(paths);
    }

    public static bool SetTimidityCfg(string path) {
        if (string.IsNullOrEmpty(path))
            throw new ArgumentNullException(nameof(path), "Path cannot be null or empty.");
        return Mix_SetTimidityCfg(path);
    }

    public static bool StartTrack(Music music, int track) {
        if (music.@interface == null)
            throw new ArgumentNullException(nameof(music));
        if (track < 0)
            throw new ArgumentOutOfRangeException(nameof(track), "Track must be non-negative.");
        return Mix_StartTrack((Music*)Unsafe.AsPointer(ref music), track);
    }

    public static bool UnregisterAllEffects(int channel) {
        if (channel < 0)
            throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be non-negative.");
        return Mix_UnregisterAllEffects(channel);
    }

    public static bool UnregisterEffect(int channel, Mix_EffectFunc_t f) {
        if (channel < 0)
            throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be non-negative.");
        if (f == null)
            throw new ArgumentNullException(nameof(f), "Effect function cannot be null.");
        return Mix_UnregisterEffect(channel, f);
    }

    public static int Volume(int channel, int volume) {
        if (channel < 0)
            throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be non-negative.");
        if (volume < 0 || volume > MaxVolume)
            throw new ArgumentOutOfRangeException(nameof(volume), $"Volume must be between 0 and {MaxVolume}.");
        return Mix_Volume(channel, volume);
    }

    public static int VolumeChunk(Chunk chunk, int volume) {
        if (chunk.AudioBuffer == null) // Check if the chunk is uninitialized by verifying its pointer.
            throw new ArgumentNullException(nameof(chunk));
        return Mix_VolumeChunk(&chunk, volume);
    }

    public static int VolumeMusic(int volume) {
        if (volume < 0 || volume > MaxVolume)
            throw new ArgumentOutOfRangeException(nameof(volume), $"Volume must be between 0 and {MaxVolume}.");
        return Mix_VolumeMusic(volume);
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_ChannelFinished(Mix_ChannelFinishedCallback channel_finished);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_CloseAudio();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool Mix_EachSoundFont(Mix_EachSoundFontCallback function, void* data);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_ExpireChannel(int channel, int ticks);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_FadeInChannel(int channel, Chunk* chunk, int loops, int ms);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_FadeInChannelTimed(int channel, Chunk* chunk, int loops, int ms, int ticks);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool Mix_FadeInMusic(Music* music, int loops, int ms);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool Mix_FadeInMusicPos(Music* music, int loops, int ms, double position);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_FadeOutChannel(int which, int ms);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_FadeOutGroup(int tag, int ms);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool Mix_FadeOutMusic(int ms);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Fading Mix_FadingChannel(int which);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Fading Mix_FadingMusic();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Chunk* Mix_GetChunk(int channel);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void* Mix_GetMusicHookData();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double Mix_GetMusicLoopEndTime(Music* music);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double Mix_GetMusicLoopLengthTime(Music* music);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double Mix_GetMusicLoopStartTime(Music* music);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double Mix_GetMusicPosition(Music* music);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_GetMusicVolume(Music* music);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_GetNumTracks(Music* music);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial string Mix_GetSoundFonts();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string Mix_GetTimidityCfg();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_GroupAvailable(int tag);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool Mix_GroupChannel(int which, int tag);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool Mix_GroupChannels(int from, int to, int tag);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_GroupCount(int tag);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_GroupNewer(int tag);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_GroupOldest(int tag);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_HaltChannel(int channel);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_HaltGroup(int tag);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_HaltMusic();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_HookMusic(Mix_MixCallback mix_func, void* arg);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_HookMusicFinished(MusicFinishedCallback music_finished);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_MasterVolume(int volume);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool Mix_ModMusicJumpToOrder(int order);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double Mix_MusicDuration(Music* music);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_Pause(int channel);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_Paused(int channel);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool Mix_PausedMusic();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_PauseGroup(int tag);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_PauseMusic();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_PlayChannel(int channel, Chunk* chunk, int loops);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_PlayChannelTimed(int channel, Chunk* chunk, int loops, int ticks);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_Playing(int channel);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool Mix_PlayingMusic();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool Mix_PlayMusic(Music* music, int loops);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool Mix_RegisterEffect(int chan, Mix_EffectFunc_t f, Mix_EffectDone_t d, void* arg);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_ReserveChannels(int num);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_Resume(int channel);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_ResumeGroup(int tag);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_ResumeMusic();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_RewindMusic();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool Mix_SetDistance(int channel, byte distance);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool Mix_SetMusicPosition(double position);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool Mix_SetPanning(int channel, byte left, byte right);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool Mix_SetPosition(int channel, short angle, byte distance);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void Mix_SetPostMix(Mix_MixCallback mix_func, void* arg);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool Mix_SetReverseStereo(int channel, int flip);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool Mix_SetSoundFonts(string paths);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool Mix_SetTimidityCfg(string path);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool Mix_StartTrack(Music* music, int track);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool Mix_UnregisterAllEffects(int channel);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool Mix_UnregisterEffect(int channel, Mix_EffectFunc_t f);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
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
     *          the current volume. If `chunk` is NULL, this returns -1.
     *
     * \since This function is available since SDL_mixer 3.0.0.
     */

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_VolumeChunk(Chunk* chunk, int volume);

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

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int Mix_VolumeMusic(int volume);
}