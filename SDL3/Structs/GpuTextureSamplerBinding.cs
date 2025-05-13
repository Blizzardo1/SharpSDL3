using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuTextureSamplerBinding
{
	public nint Texture;
	public nint Sampler;
}