using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuDepthStencilState
{
	public GpuCompareOp CompareOp;
	public GpuStencilOpState BackStencilState;
	public GpuStencilOpState FrontStencilState;
	public byte CompareMask;
	public byte WriteMask;
	public SdlBool EnableDepthTest;
	public SdlBool EnableDepthWrite;
	public SdlBool EnableStencilTest;
	public byte Padding1;
	public byte Padding2;
	public byte Padding3;
}