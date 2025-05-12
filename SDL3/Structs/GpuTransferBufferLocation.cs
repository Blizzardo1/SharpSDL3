using System.Runtime.InteropServices;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuTransferBufferLocation
{
	public nint TransferBuffer;
	public uint Offset;
}