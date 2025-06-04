using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuBufferRegion {
    public nint Buffer;
    public uint Offset;
    public uint Size;
}