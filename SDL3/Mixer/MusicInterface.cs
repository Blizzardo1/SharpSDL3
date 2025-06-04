using SharpSDL3.Structs;
using System.Runtime.InteropServices;

namespace SharpSDL3.Mixer;

/// <summary>
/// Defines an interface for a music backend in SDL3_mixer, specifying how to load, play, and manage music data.
/// </summary>
/// <remarks>
/// This struct is used internally by SDL3_mixer to interact with different music backends (e.g., MP3, OGG, MIDI).
/// Each field is either a property or a delegate corresponding to a function in the music backend.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
public struct MusicInterface {

    /// <summary>
    /// A tag identifying the music backend (e.g., "mp3", "ogg").
    /// </summary>
    [MarshalAs(UnmanagedType.LPStr)]
    public string Tag;

    /// <summary>
    /// The API type of the music backend.
    /// </summary>
    public MusicApi Api;

    /// <summary>
    /// The type of music supported by this backend.
    /// </summary>
    public MusicType Type;

    /// <summary>
    /// Indicates whether the backend library is loaded.
    /// </summary>
    [MarshalAs(Sdl.BoolType)]
    public bool Loaded;

    /// <summary>
    /// Indicates whether the backend is initialized for audio output.
    /// </summary>
    [MarshalAs(Sdl.BoolType)]
    public bool Opened;

    /// <summary>
    /// Loads the backend library.
    /// </summary>
    /// <returns>0 on success, or a negative error code on failure.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int LoadDelegate();

    /// <summary>
    /// Initializes the backend for audio output with the specified audio specification.
    /// </summary>
    /// <param name="spec">The desired audio specification.</param>
    /// <returns>0 on success, or a negative error code on failure.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int OpenDelegate([In] AudioSpec spec);

    /// <summary>
    /// Creates a music object from an SDL_IOStream.
    /// </summary>
    /// <param name="src">The input stream containing music data.</param>
    /// <param name="closeio">True to close the stream after loading, <see langword="false" /> otherwise.</param>
    /// <returns>An opaque pointer to the music object, or <see cref="nint.Zero"/> on failure.</returns>
    /// <remarks>
    /// If the function returns <see cref="nint.Zero"/>, the caller is responsible for freeing <paramref name="src"/> if needed.
    /// </remarks>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate nint CreateFromIODelegate(nint src, [MarshalAs(Sdl.BoolType)] bool closeio);

    /// <summary>
    /// Creates a music object from a file, used when SDL_IOStream is not supported.
    /// </summary>
    /// <param name="file">The path to the music file.</param>
    /// <returns>An opaque pointer to the music object, or <see cref="nint.Zero"/> on failure.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate nint CreateFromFileDelegate([MarshalAs(Sdl.StringType)] string file);

    /// <summary>
    /// Sets the volume for a music object.
    /// </summary>
    /// <param name="music">An opaque pointer to the music object.</param>
    /// <param name="volume">The volume level (0 to <see cref="Mixer.MIX_MAX_VOLUME"/>).</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SetVolumeDelegate(nint music, int volume);

    /// <summary>
    /// Gets the volume for a music object.
    /// </summary>
    /// <param name="music">An opaque pointer to the music object.</param>
    /// <returns>The current volume level (0 to <see cref="Mixer.MIX_MAX_VOLUME"/>).</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int GetVolumeDelegate(nint music);

    /// <summary>
    /// Starts playing a music object from the beginning.
    /// </summary>
    /// <param name="music">An opaque pointer to the music object.</param>
    /// <param name="playCount">The number of loops (-1 for infinite, 0 for once).</param>
    /// <returns>0 on success, or a negative error code on failure.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int PlayDelegate(nint music, int playCount);

    /// <summary>
    /// Checks if a music object is currently playing.
    /// </summary>
    /// <param name="music">An opaque pointer to the music object.</param>
    /// <returns>True if the music is playing, <see langword="false" /> otherwise.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(Sdl.BoolType)]
    public delegate bool IsPlayingDelegate(nint music);

    /// <summary>
    /// Retrieves audio data from a music object.
    /// </summary>
    /// <param name="music">An opaque pointer to the music object.</param>
    /// <param name="data">A pointer to the buffer to store audio data.</param>
    /// <param name="bytes">The number of bytes requested.</param>
    /// <returns>The number of bytes written to <paramref name="data"/>, or -1 on error.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int GetAudioDelegate(nint music, nint data, int bytes);

    /// <summary>
    /// Jumps to a specific order in MOD music.
    /// </summary>
    /// <param name="music">An opaque pointer to the music object.</param>
    /// <param name="order">The order to jump to.</param>
    /// <returns>0 on success, or a negative error code on failure.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int JumpDelegate(nint music, int order);

    /// <summary>
    /// Seeks to a specific position in the music.
    /// </summary>
    /// <param name="music">An opaque pointer to the music object.</param>
    /// <param name="position">The position to seek to, in seconds.</param>
    /// <returns>0 on success, or a negative error code on failure.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SeekDelegate(nint music, double position);

    /// <summary>
    /// Gets the current playback position of the music.
    /// </summary>
    /// <param name="music">An opaque pointer to the music object.</param>
    /// <returns>The current position in seconds, or -1.0 if not supported.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate double TellDelegate(nint music);

    /// <summary>
    /// Gets the total duration of the music.
    /// </summary>
    /// <param name="music">An opaque pointer to the music object.</param>
    /// <returns>The duration in seconds, or -1.0 if not supported.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate double DurationDelegate(nint music);

    /// <summary>
    /// Gets the loop start position of the music.
    /// </summary>
    /// <param name="music">An opaque pointer to the music object.</param>
    /// <returns>The loop start position in seconds, or -1.0 if not supported.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate double LoopStartDelegate(nint music);

    /// <summary>
    /// Gets the loop end position of the music.
    /// </summary>
    /// <param name="music">An opaque pointer to the music object.</param>
    /// <returns>The loop end position in seconds, or -1.0 if not supported.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate double LoopEndDelegate(nint music);

    /// <summary>
    /// Gets the loop length of the music.
    /// </summary>
    /// <param name="music">An opaque pointer to the music object.</param>
    /// <returns>The loop length in seconds, or -1.0 if not supported.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate double LoopLengthDelegate(nint music);

    /// <summary>
    /// Retrieves a metadata tag from the music.
    /// </summary>
    /// <param name="music">An opaque pointer to the music object.</param>
    /// <param name="tagType">The type of metadata tag to retrieve.</param>
    /// <returns>The metadata string, or an empty string if not available.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(Sdl.StringType)]
    public delegate string GetMetaTagDelegate(nint music, MusicMetaTag tagType);

    /// <summary>
    /// Gets the number of tracks in the music.
    /// </summary>
    /// <param name="music">An opaque pointer to the music object.</param>
    /// <returns>The number of tracks, or -1 if not applicable.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int GetNumTracksDelegate(nint music);

    /// <summary>
    /// Starts playback from a specific track.
    /// </summary>
    /// <param name="music">An opaque pointer to the music object.</param>
    /// <param name="track">The track number to start.</param>
    /// <returns>0 on success, or a negative error code on failure.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int StartTrackDelegate(nint music, int track);

    /// <summary>
    /// Pauses playback of the music.
    /// </summary>
    /// <param name="music">An opaque pointer to the music object.</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void PauseDelegate(nint music);

    /// <summary>
    /// Resumes playback of the music.
    /// </summary>
    /// <param name="music">An opaque pointer to the music object.</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ResumeDelegate(nint music);

    /// <summary>
    /// Stops playback of the music.
    /// </summary>
    /// <param name="music">An opaque pointer to the music object.</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void StopDelegate(nint music);

    /// <summary>
    /// Deletes a music object, freeing its resources.
    /// </summary>
    /// <param name="music">An opaque pointer to the music object.</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void DeleteDelegate(nint music);

    /// <summary>
    /// Closes the backend and cleans up resources.
    /// </summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void CloseDelegate();

    /// <summary>
    /// Unloads the backend library.
    /// </summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void UnloadDelegate();

    // Delegate fields
    public LoadDelegate Load;

    public OpenDelegate Open;
    public CreateFromIODelegate CreateFromIO;
    public CreateFromFileDelegate CreateFromFile;
    public SetVolumeDelegate SetVolume;
    public GetVolumeDelegate GetVolume;
    public PlayDelegate Play;
    public IsPlayingDelegate IsPlaying;
    public GetAudioDelegate GetAudio;
    public JumpDelegate Jump;
    public SeekDelegate Seek;
    public TellDelegate Tell;
    public DurationDelegate Duration;
    public LoopStartDelegate LoopStart;
    public LoopEndDelegate LoopEnd;
    public LoopLengthDelegate LoopLength;
    public GetMetaTagDelegate GetMetaTag;
    public GetNumTracksDelegate GetNumTracks;
    public StartTrackDelegate StartTrack;
    public PauseDelegate Pause;
    public ResumeDelegate Resume;
    public StopDelegate Stop;
    public DeleteDelegate Delete;
    public CloseDelegate Close;
    public UnloadDelegate Unload;
}