using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuTransferBufferLocation
{
	public nint TransferBuffer;
	public uint Offset;
}