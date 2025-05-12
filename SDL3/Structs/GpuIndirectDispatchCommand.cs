using System.Runtime.InteropServices;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuIndirectDispatchCommand
{
	public uint GroupCountX;
	public uint GroupCountY;
	public uint GroupCountZ;
}