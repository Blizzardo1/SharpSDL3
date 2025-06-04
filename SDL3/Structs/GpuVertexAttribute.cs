using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuVertexAttribute {
    public uint Location;
    public uint BufferSlot;
    public GpuVertexElementFormat Format;
    public uint Offset;
}