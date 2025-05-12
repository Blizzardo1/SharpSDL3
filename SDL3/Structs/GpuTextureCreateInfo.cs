using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuTextureCreateInfo
{
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