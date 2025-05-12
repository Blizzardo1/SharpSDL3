using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuTransferBufferCreateInfo
{
	public GpuTransferBufferUsage Usage;
	public uint Size;
	public uint Props;
}