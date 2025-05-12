using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

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