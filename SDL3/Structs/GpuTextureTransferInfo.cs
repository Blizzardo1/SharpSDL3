using System.Runtime.InteropServices;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuTextureTransferInfo
{
	public nint TransferBuffer;
	public uint Offset;
	public uint PixelsPerRow;
	public uint RowsPerLayer;
}