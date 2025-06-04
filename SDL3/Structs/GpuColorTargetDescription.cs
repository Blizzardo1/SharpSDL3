using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuColorTargetDescription
{
	public GpuTextureFormat Format;
	public GpuColorTargetBlendState BlendState;
}