namespace SharpSDL3.Enums;

/// <summary>
/// Colorspace definitions.
/// </summary>
public enum Colorspace {
    /// <summary>
    /// Unknown Colorspace
    /// </summary>
    Unknown = 0x0,

    /// <summary>
    /// sRGB is a gamma corrected colorspace, and the default colorspace for SDL rendering and 8-bit RGB surfaces
    /// </summary>
    Srgb = 0x120005A0,

    /// <summary>
    /// his is a linear colorspace and the default colorspace for floating point surfaces. On Windows this is the scRGB colorspace, and on Apple platforms this is kCGColorSpaceExtendedLinearSRGB for EDR content
    /// </summary>
    SrgbLinear = 0x12000500,
    
    /// <summary>
    /// HDR10 is a non-linear HDR colorspace and the default colorspace for 10-bit surfaces
    /// </summary>
    Hdr10 = 0x12002600,

    /// <summary>
    /// JPEG/JFIF YUV colorspace. Full range BT.601 matrix, used by JPEG images and many web/consumer formats.
    /// Chroma is not subsampled (4:4:4).
    /// </summary>
    Jpeg = 0x220004C6,
    
    /// <summary>
    /// BT.601 limited range YUV colorspace. Standard definition TV colorspace (NTSC/PAL).
    /// Limited range means luma is clamped to [16–235] and chroma to [16–240].
    /// </summary>
    Bt601Limited = 0x211018C6,
    
    /// <summary>
    /// BT.601 full range YUV colorspace. Standard definition TV primaries with full [0–255] luma and chroma range.
    /// Commonly used in MJPEG and some webcam outputs.
    /// </summary>
    Bt601Full = 0x221018C6,
    
    /// <summary>
    /// BT.709 limited range YUV colorspace. High definition TV standard (1080p/720p broadcast).
    /// Limited range luma [16–235], the dominant format for HD video delivery and Blu-ray.
    /// </summary>
    Bt709Limited = 0x21100421,
    
    /// <summary>
    /// BT.709 full range YUV colorspace. HD primaries with full [0–255] range.
    /// Less common than limited range; sometimes seen in PC-captured HD content.
    /// </summary>
    Bt709Full = 0x22100421,
    
    /// <summary>
    /// BT.2020 limited range YUV colorspace. Ultra HD / 4K broadcast standard with a much wider color gamut
    /// than BT.709. Limited range; used in HDR10 and HLG HDR video delivery.
    /// </summary>
    Bt2020Limited = 0x21102609,
    
    /// <summary>
    /// BT.2020 full range YUV colorspace. Same wide-gamut UHD primaries as BT.2020 Limited
    /// but with full [0–255] range. Uncommon in broadcast; occasionally seen in professional/camera workflows.
    /// </summary>
    Bt2020Full = 0x22102609,
    
    /// <summary>
    /// The default colorspace for RGB surfaces if no colorspace is specified
    /// </summary>
    RgbDefault = Srgb,

    /// <summary>
    /// The default colorspace for YUV surfaces if no colorspace is specified
    /// </summary>
    YuvDefault = Jpeg
}