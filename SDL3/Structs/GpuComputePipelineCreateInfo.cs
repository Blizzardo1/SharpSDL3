using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct GpuComputePipelineCreateInfo
{
	public nuint CodeSize;
	public byte* Code;
	public byte* EntryPoint;
	public GpuShaderFormat Format;
	public uint NumSamplers;
	public uint NumReadOnlyStorageTextures;
	public uint NumReadOnlyStorageBuffers;
	public uint NumReadWriteStorageTextures;
	public uint NumReadWriteStorageBuffers;
	public uint NumUniformBuffers;
	public uint ThreadCountX;
	public uint ThreadCountY;
	public uint ThreadCountZ;
	public uint Props;
}