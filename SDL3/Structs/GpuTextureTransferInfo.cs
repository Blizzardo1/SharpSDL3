using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
<<<<<<< HEAD
public struct GpuTextureTransferInfo {
    public nint TransferBuffer;
    public uint Offset;
    public uint PixelsPerRow;
    public uint RowsPerLayer;
=======
public struct GpuTextureTransferInfo
{
	public nint TransferBuffer;
	public uint Offset;
	public uint PixelsPerRow;
	public uint RowsPerLayer;
>>>>>>> main
}