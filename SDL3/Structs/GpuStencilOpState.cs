using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuStencilOpState
{
	public GpuStencilOp FailOp;
	public GpuStencilOp PassOp;
	public GpuStencilOp DepthFailOp;
	public GpuCompareOp CompareOp;
}