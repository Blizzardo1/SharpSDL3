using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuBufferCreateInfo
{
	public GpuBufferUsageFlags Usage;
	public uint Size;
	public uint Props;
}