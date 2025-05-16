using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct GpuVertexInputState
{
	public nint VertexBufferDescriptions;
	public uint NumVertexBuffers;
	public nint VertexAttributes;
	public uint NumVertexAttributes;
}