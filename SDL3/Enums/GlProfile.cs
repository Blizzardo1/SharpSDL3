namespace SharpSDL3.Enums;

/// <summary>
/// Possible values to be set for the <see cref="GlAttr.ContextProfileMask"/> attribute
/// </summary>
/// <remarks>
/// <strong>Version</strong>: This datatype is available since SDL 3.2.0
/// </remarks>
public enum GlProfile {
    /// <summary>
    /// OpenGL Core Profile context
    /// </summary>
    ContextProfileCore = 0x0001,
    /// <summary>
    /// OpenGL Compatibility Profile context
    /// </summary>
    ContextProfileCompatibility = 0x0002,
    /// <summary>
    /// GLX_CONTEXT_ES2_PROFILE_BIT_EXT
    /// </summary>
    ContextProfileEs = 0x0004
}