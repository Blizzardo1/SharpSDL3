namespace SharpSDL3.Enums;

/// <summary>
/// Possible values to be set for the <see cref="GlAttr.ContextReleaseBehavior"/> attribute.
/// </summary>
/// <remarks>
/// <strong>Version</strong>: This datatype is available since SDL 3.2.0
/// </remarks>
public enum GlContextReleaseFlag {
    /// <summary>
    /// Context Release Behavior None
    /// </summary>
    None = 0x0000,
    /// <summary>
    /// Context Release Behavior Flush
    /// </summary>
    Flush = 0x0001
}
