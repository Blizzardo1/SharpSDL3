namespace SharpSDL3.Enums;

/// <summary>
/// The scaling mode
/// </summary>
/// <remarks>
/// <para><strong>Version</strong>: This enum if available since SDL 3.2.0</para>
/// </remarks>
public enum ScaleMode {
    /// <summary>
    /// Invalid Scale Mode
    /// </summary>
    Invalid = -1,
    /// <summary>
    /// Nearest pixel sampling
    /// </summary>
    Nearest,
    /// <summary>
    /// Linear filtering
    /// </summary>
    Linear,
    /// <summary>
    /// Nearest pixel sampling with improved scaling for pixel art
    /// </summary>
    /// <remarks>
    /// <para><strong>Version</strong>: Available since SDL 3.4.0</para>
    /// </remarks>
    PixelArt
}