using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuRasterizerState {
    public GpuFillMode FillMode;
    public GpuCullMode CullMode;
    public GpuFrontFace FrontFace;
    public float DepthBiasConstantFactor;
    public float DepthBiasClamp;
    public float DepthBiasSlopeFactor;
    public SdlBool EnableDepthBias;
    public SdlBool EnableDepthClip;
    public byte Padding1;
    public byte Padding2;
}