using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct GpuShaderCreateInfo
{
	public nuint CodeSize;
	public byte* Code;
	public byte* EntryPoint;
	public GpuShaderFormat Format;
	public GpuShaderStage Stage;
	public uint NumSamplers;
	public uint NumStorageTextures;
	public uint NumStorageBuffers;
	public uint NumUniformBuffers;
	public uint Props;
}