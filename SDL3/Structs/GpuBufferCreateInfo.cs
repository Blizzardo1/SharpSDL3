using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuBufferCreateInfo {
    public GpuBufferUsageFlags Usage;
    public uint Size;
    public uint Props;
}