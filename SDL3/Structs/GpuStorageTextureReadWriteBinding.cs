using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuStorageTextureReadWriteBinding
{
	public nint Texture;
	public uint MipLevel;
	public uint Layer;
	public SdlBool Cycle;
	public byte Padding1;
	public byte Padding2;
	public byte Padding3;
}