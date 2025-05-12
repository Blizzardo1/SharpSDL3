using System.Runtime.InteropServices;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuIndirectDrawCommand
{
	public uint NumVertices;
	public uint NumInstances;
	public uint FirstVertex;
	public uint FirstInstance;
}