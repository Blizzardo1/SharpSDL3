namespace SharpSDL3.Enums;

public enum BlendMode : uint {
    None = 0x00000000u,/**< no blending: dstRGBA = srcRGBA */
    Blend = 0x00000001u,/**< alpha blending: dstRGB = (srcRGB * srcA) + (dstRGB * (1-srcA)), dstA = srcA + (dstA * (1-srcA)) */
    BlendPremultiplied = 0x00000010u,/**< pre-multiplied alpha blending: dstRGBA = srcRGBA + (dstRGBA * (1-srcA)) */
    Add = 0x00000002u,/**< additive blending: dstRGB = (srcRGB * srcA) + dstRGB, dstA = dstA */
    AddPremultiplied = 0x00000020u,/**< pre-multiplied additive blending: dstRGB = srcRGB + dstRGB, dstA = dstA */
    Mod = 0x00000004u,/**< color modulate: dstRGB = srcRGB * dstRGB, dstA = dstA */
    Mul = 0x00000008u,/**< color multiply: dstRGB = (srcRGB * dstRGB) + (dstRGB * (1-srcA)), dstA = dstA */
    Invalid = 0x7FFFFFFFu
}