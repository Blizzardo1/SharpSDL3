using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuColorTargetDescription
{
	public GpuTextureFormat Format;
	public GpuColorTargetBlendState BlendState;
}