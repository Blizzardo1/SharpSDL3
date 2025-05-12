using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct GpuGraphicsPipelineTargetInfo
{
	public GpuColorTargetDescription* ColorTargetDescription;
	public uint NumColorTargets;
	public GpuTextureFormat DepthStencilFormat;
	public SdlBool HasDepthStencilTarget;
	public byte Padding1;
	public byte Padding2;
	public byte Padding3;
}