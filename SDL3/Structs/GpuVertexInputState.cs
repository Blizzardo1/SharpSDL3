using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct GpuVertexInputState
{
	public GpuVertexBufferDescription* VertexBufferDescriptions;
	public uint NumVertexBuffers;
	public GpuVertexAttribute* VertexAttributes;
	public uint NumVertexAttributes;
}