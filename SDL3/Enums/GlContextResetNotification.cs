
namespace SharpSDL3.Enums;

/// <summary>
/// Possible values to be set for the <see cref="GlAttr.ContextResetNotification"/> attribute.
/// </summary>
/// <remarks>
/// <strong>Version</strong>: This datatype is available since SDL 3.2.0
/// </remarks>
public enum GlContextResetNotification {
    /// <summary>
    /// No Notification
    /// </summary>
    NoNotification = 0x0000,
    /// <summary>
    /// Lose Context
    /// </summary>
    LoseContext = 0x0001
}