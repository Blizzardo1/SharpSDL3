namespace SharpSDL3.Mixer;

public enum AudioDeviceId : uint {

    /// <summary>
    /// A value used to request a default playback audio device.
    /// Several functions that require an <see cref="AudioDeviceId"/> will accept this value
    /// to signify the app just wants the system to choose a default device instead
    /// of the app providing a specific one.
    /// <para>Since SDL 3.2.0.</para>
    /// </summary>
    DefaultPlayback = 0xFFFFFFFFu,

    /// <summary>
    /// A value used to request a default recording audio device.
    /// Several functions that require an <see cref="AudioDeviceId"/> will accept this value
    /// to signify the app just wants the system to choose a default device instead
    /// of the app providing a specific one.
    /// <para>Since SDL 3.2.0.</para>
    /// </summary>
    DefaultRecording = 0xFFFFFFFEu
}