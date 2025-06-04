using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
<<<<<<< HEAD
public unsafe struct GpuVertexInputState {
    public nint VertexBufferDescriptions;
    public uint NumVertexBuffers;
    public nint VertexAttributes;
    public uint NumVertexAttributes;
=======
public unsafe struct GpuVertexInputState
{
	public nint VertexBufferDescriptions;
	public uint NumVertexBuffers;
	public nint VertexAttributes;
	public uint NumVertexAttributes;
>>>>>>> main
}