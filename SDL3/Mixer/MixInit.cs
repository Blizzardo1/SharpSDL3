using System;

namespace SharpSDL3.Mixer;

public static unsafe partial class Mixer {

    /// <summary>
    /// The internal format for a music chunk interpreted via codecs
    /// </summary>
    [Flags]
    public enum MixInit : uint {

        /// <summary>
        /// No initialization.
        /// </summary>
        None = 0x00000000,

        /// <summary>
        /// FLAC audio format.
        /// </summary>
        Flac = 0x00000001,

        /// <summary>
        /// MOD audio format.
        /// </summary>
        Mod = 0x00000002,

        /// <summary>
        /// MP3 audio format.
        /// </summary>
        Mp3 = 0x00000008,

        /// <summary>
        /// OGG audio format.
        /// </summary>
        Ogg = 0x00000010,

        /// <summary>
        /// MIDI audio format.
        /// </summary>
        Midi = 0x00000020,

        /// <summary>
        /// OPUS audio format.
        /// </summary>
        Opus = 0x00000040,

        /// <summary>
        /// WavPack audio format.
        /// </summary>
        WavPack = 0x00000080
    }
}