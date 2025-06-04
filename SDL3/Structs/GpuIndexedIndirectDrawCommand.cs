using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuIndexedIndirectDrawCommand {
    public uint NumIndices;
    public uint NumInstances;
    public uint FirstIndex;
    public int VertexOffset;
    public uint FirstInstance;
}