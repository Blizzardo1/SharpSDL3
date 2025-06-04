<<<<<<< HEAD
using System;
=======
﻿using System;
>>>>>>> main

namespace SharpSDL3.Mixer;

public static unsafe partial class Mixer {
<<<<<<< HEAD

=======
>>>>>>> main
    /// <summary>
    /// The internal format for a music chunk interpreted via codecs
    /// </summary>
    [Flags]
    public enum MixInit : uint {
<<<<<<< HEAD

=======
>>>>>>> main
        /// <summary>
        /// No initialization.
        /// </summary>
        None = 0x00000000,
<<<<<<< HEAD

=======
>>>>>>> main
        /// <summary>
        /// FLAC audio format.
        /// </summary>
        Flac = 0x00000001,
<<<<<<< HEAD

=======
>>>>>>> main
        /// <summary>
        /// MOD audio format.
        /// </summary>
        Mod = 0x00000002,
<<<<<<< HEAD

=======
>>>>>>> main
        /// <summary>
        /// MP3 audio format.
        /// </summary>
        Mp3 = 0x00000008,
<<<<<<< HEAD

=======
>>>>>>> main
        /// <summary>
        /// OGG audio format.
        /// </summary>
        Ogg = 0x00000010,
<<<<<<< HEAD

=======
>>>>>>> main
        /// <summary>
        /// MIDI audio format.
        /// </summary>
        Midi = 0x00000020,
<<<<<<< HEAD

=======
>>>>>>> main
        /// <summary>
        /// OPUS audio format.
        /// </summary>
        Opus = 0x00000040,
<<<<<<< HEAD

=======
>>>>>>> main
        /// <summary>
        /// WavPack audio format.
        /// </summary>
        WavPack = 0x00000080
    }
}