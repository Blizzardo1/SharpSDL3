using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct GpuShaderCreateInfo
{
	public nuint CodeSize;
	public nint Code;
	public nint EntryPoint;
	public GpuShaderFormat Format;
	public GpuShaderStage Stage;
	public uint NumSamplers;
	public uint NumStorageTextures;
	public uint NumStorageBuffers;
	public uint NumUniformBuffers;
	public uint Props;
}