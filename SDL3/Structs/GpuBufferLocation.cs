using System.Runtime.InteropServices;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuBufferLocation
{
	public nint Buffer;
	public uint Offset;
}