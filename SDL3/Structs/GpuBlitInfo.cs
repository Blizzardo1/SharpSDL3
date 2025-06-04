using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuBlitInfo
{
	public GpuBlitRegion Source;
	public GpuBlitRegion Destination;
	public GpuLoadOp LoadOp;
	public FColor ClearColor;
	public FlipMode FlipMode;
	public GpuFilter Filter;
	public SdlBool Cycle;
	public byte Padding1;
	public byte Padding2;
	public byte Padding3;
}