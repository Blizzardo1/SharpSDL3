using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
<<<<<<< HEAD
public struct GpuTextureRegion {
    public nint Texture;
    public uint MipLevel;
    public uint Layer;
    public uint X;
    public uint Y;
    public uint Z;
    public uint W;
    public uint H;
    public uint D;
=======
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
>>>>>>> main
}