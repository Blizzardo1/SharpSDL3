using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuStencilOpState
{
	public GpuStencilOp FailOp;
	public GpuStencilOp PassOp;
	public GpuStencilOp DepthFailOp;
	public GpuCompareOp CompareOp;
}