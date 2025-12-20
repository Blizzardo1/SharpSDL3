namespace SharpSDL3.Enums;

/// <summary>
/// An enumeration of OpenGL Configuration attributes.
/// </summary>
/// <remarks>
/// <para>
/// While you can set most OpenGL attributes normally, the attributes listed above must be known before
/// SDL creates the window that will be used with the OpenGL context. These attributes are set and read
/// with <see cref="Sdl.GlSetAttribute"/> and <see cref="Sdl.GlGetAttribute"/>.</para>
/// <para>
/// In some cases, these attributes are minimum requests; the GL does not promise to give you exactly
/// what you asked for. It's possible to ask for a 16-bit depth buffer and get a 24-bit one instead, for
/// example, or to ask for no stencil buffer and still have one available. Context creation should fail if the GL
/// can't provide your requested attributes at a minimum, but you should check to see exactly what you got.
/// </para>
/// <para><strong>Version</strong>: This enum is available since SDL 3.2.0.</para>
/// </remarks>
public enum GlAttr {
    /// <summary>
    /// The minimum number of bits for the red channel of the color buffer; defaults to 8. 
    /// </summary>
    RedSize = 0,
    /// <summary>
    /// The minimum number of bits for the green channel of the color buffer; defaults to 8.
    /// </summary>
    GreenSize = 1,
    /// <summary>
    /// The minimum number of bits for the blue channel of the color buffer; defaults to 8.
    /// </summary>
    BlueSize = 2,
    /// <summary>
    /// The minimum number of bits for the alpha channel of the color buffer; defaults to 8.
    /// </summary>
    AlphaSize = 3,
    /// <summary>
    ///The minimum number of bits for frame buffer size; defaults to 0.
    /// </summary>
    BufferSize = 4,
    /// <summary>
    /// Whether the output is single or double buffered; defaults to double buffering on.
    /// </summary>
    DoubleBuffer = 5,
    /// <summary>
    /// The minimum number of bits in the depth buffer; defaults to 16.
    /// </summary>
    DepthSize = 6,
    /// <summary>
    /// The minimum number of bits in the stencil buffer; defaults to 0.
    /// </summary>
    StencilSize = 7,
    /// <summary>
    /// The minimum number of bits for the red channel of the accumulation buffer; defaults to 0.
    /// </summary>
    AccumRedSize = 8,
    /// <summary>
    /// The minimum number of bits for the green channel of the accumulation buffer; defaults to 0.
    /// </summary>
    AccumGreenSize = 9,
    /// <summary>
    /// The minimum number of bits for the blue channel of the accumulation buffer; defaults to 0.
    /// </summary>
    AccumBlueSize = 10,
    /// <summary>
    /// The minimum number of bits for the alpha channel of the accumulation buffer; defaults to 0.
    /// </summary>
    AccumAlphaSize = 11,
    /// <summary>
    /// Whether the output is stereo 3D; defaults to off.
    /// </summary>
    Stereo = 12,
    /// <summary>
    /// The number of buffers used for multisample anti-aliasing; defaults to 0.
    /// </summary>
    MultiSampleBuffers = 13,
    /// <summary>
    /// The number of samples used around the current pixel used for multisample anti-aliasing.
    /// </summary>
    MultiSampleSamples = 14,
    /// <summary>
    /// Not used (deprecated).
    /// </summary>
    AcceleratedVisual = 15,
    /// <summary>
    /// Set to 1 to require hardware acceleration, set to 0 to force software rendering; defaults to allow either.
    /// </summary>
    RetainedBacking = 16,
    /// <summary>
    /// OpenGL context major version.
    /// </summary>
    ContextMajorVersion = 17,
    /// <summary>
    /// OpenGL context minor version.
    /// </summary>
    ContextMinorVersion = 18,
    /// <summary>
    /// Some combination of 0 or more of elements of the <see cref="GlContextFlag"/> enumeration; defaults to 0.
    /// </summary>
    ContextFlags = 19,
    /// <summary>
    /// Type of GL context (Core, Compatibility, ES). See <see cref="GlProfile"/>; default value depends on platform.
    /// </summary>
    ContextProfileMask = 20,
    /// <summary>
    /// OpenGL context sharing; defaults to 0.
    /// </summary>
    ShareWithCurrentContext = 21,
    /// <summary>
    /// Requests sRGB-capable visual if 1. Defaults to -1 ("don't care"). This is a request; GL drivers might not comply!
    /// </summary>
    FrameBufferSrgbCapable = 22,
    /// <summary>
    /// Sets context the release behavior. See <see cref="GlContextReleaseFlag"/> defaults to <see cref="GlContextReleaseFlag.Flush"/>.
    /// </summary>
    ContextReleaseBehavior = 23,
    /// <summary>
    /// Set context reset notification. See <see cref="GlContextResetNotification"/>; defaults to <see cref="GlContextResetNotification.NoNotification"/>.
    /// </summary>
    ContextResetNotification = 24,
    /// <summary>
    /// No Error
    /// </summary>
    ContextNoError = 25,
    /// <summary>
    /// Float Buffers
    /// </summary>
    FloatBuffers = 26,
    /// <summary>
    /// Platform
    /// </summary>
    EglPlatform = 27
}