using System.Runtime.InteropServices;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuBufferBinding
{
	public nint Buffer;
	public uint Offset;
}