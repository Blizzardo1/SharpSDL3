using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
<<<<<<< HEAD
public struct GpuIndirectDrawCommand {
    public uint NumVertices;
    public uint NumInstances;
    public uint FirstVertex;
    public uint FirstInstance;
=======
public struct GpuIndirectDrawCommand
{
	public uint NumVertices;
	public uint NumInstances;
	public uint FirstVertex;
	public uint FirstInstance;
>>>>>>> main
}