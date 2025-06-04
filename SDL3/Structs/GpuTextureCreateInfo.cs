using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuTextureCreateInfo {
    public GpuTextureType Type;
    public GpuTextureFormat Format;
    public GpuTextureUsageFlags Usage;
    public uint Width;
    public uint Height;
    public uint LayerCountOrDepth;
    public uint NumLevels;
    public GpuSampleCount SampleCount;
    public uint Props;
}