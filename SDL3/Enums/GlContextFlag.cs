namespace SharpSDL3.Enums;
/// <summary>
/// Possible flags to be set for the <see cref="GlAttr.ContextFlags"/> attribute.
/// </summary>
/// <remarks>
/// <strong>Version</strong>: This datatype is available since SDL 3.2.0.
/// </remarks>
public enum GlContextFlag {
    /// <summary>
    /// Debug Flag
    /// </summary>
    DebugFlag = 0x0001,
    /// <summary>
    /// Forward Compatible Flag
    /// </summary>
    ForwardCompatibleFlag = 0x0002,
    /// <summary>
    /// Robust Access Flag
    /// </summary>
    RobustAccessFlag = 0x0004,
    /// <summary>
    /// Reset Isolation Flag
    /// </summary>
    ResetIsolationFlag = 0x0008
}