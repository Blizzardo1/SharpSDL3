using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuStorageBufferReadWriteBinding
{
	public nint Buffer;
	public SdlBool Cycle;
	public byte Padding1;
	public byte Padding2;
	public byte Padding3;
}