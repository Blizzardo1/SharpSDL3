using System.Runtime.InteropServices;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuTextureRegion
{
	public nint Texture;
	public uint MipLevel;
	public uint Layer;
	public uint X;
	public uint Y;
	public uint Z;
	public uint W;
	public uint H;
	public uint D;
}