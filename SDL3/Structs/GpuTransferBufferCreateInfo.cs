using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuTransferBufferCreateInfo
{
	public GpuTransferBufferUsage Usage;
	public uint Size;
	public uint Props;
}