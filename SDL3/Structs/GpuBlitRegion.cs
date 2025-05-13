using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuBlitRegion
{
	public nint Texture;
	public uint MipLevel;
	public uint LayerOrDepthPlane;
	public uint X;
	public uint Y;
	public uint W;
	public uint H;
}