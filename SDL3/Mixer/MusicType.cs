namespace SharpSDL3.Mixer;

public static unsafe partial class Mixer {
    /// <summary>
    /// These are types of music files (not libraries used to load them)
    /// </summary>
    public enum MusicType {
        None,
        Wav,
        Mod,
        Mid,
        Ogg,
        Mp3,
        Flac,
        Opus,
        WavPack,
        Gme
    }
}