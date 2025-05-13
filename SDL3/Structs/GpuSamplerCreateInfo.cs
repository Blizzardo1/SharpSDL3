using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuSamplerCreateInfo
{
	public GpuFilter MinFilter;
	public GpuFilter MagFilter;
	public GpuSamplerMipmapMode MipmapMode;
	public GpuSamplerAddressMode AddressModeU;
	public GpuSamplerAddressMode AddressModeV;
	public GpuSamplerAddressMode AddressModeW;
	public float MipLodBias;
	public float MaxAnisotropy;
	public GpuCompareOp CompareOp;
	public float MinLod;
	public float MaxLod;
	public SdlBool EnableAnisotropy;
	public SdlBool EnableCompare;
	public byte Padding1;
	public byte Padding2;
	public uint Props;
}