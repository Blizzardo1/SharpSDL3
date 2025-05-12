using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuVertexAttribute
{
	public uint Location;
	public uint BufferSlot;
	public GpuVertexElementFormat Format;
	public uint Offset;
}