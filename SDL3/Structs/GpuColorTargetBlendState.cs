using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuColorTargetBlendState
{
	public GpuBlendFactor SrcColorBlendFactor;
	public GpuBlendFactor DstColorBlendFactor;
	public GpuBlendOp ColorBlendOp;
	public GpuBlendFactor SrcAlphaBlendFactor;
	public GpuBlendFactor DstAlphaBlendFactor;
	public GpuBlendOp AlphaBlendOp;
	public GpuColorComponentFlags ColorWriteMask;
	public SdlBool EnableBlend;
	public SdlBool EnableColorWriteMask;
	public byte Padding1;
	public byte Padding2;
}