using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct GpuGraphicsPipelineTargetInfo {
    public nint ColorTargetDescription;
    public uint NumColorTargets;
    public GpuTextureFormat DepthStencilFormat;
    public SdlBool HasDepthStencilTarget;
    public byte Padding1;
    public byte Padding2;
    public byte Padding3;
}