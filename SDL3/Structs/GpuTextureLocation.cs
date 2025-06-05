using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
<<<<<<< HEAD
public struct GpuTextureLocation {
    public nint Texture;
    public uint MipLevel;
    public uint Layer;
    public uint X;
    public uint Y;
    public uint Z;
=======
public struct GpuTextureLocation
{
	public nint Texture;
	public uint MipLevel;
	public uint Layer;
	public uint X;
	public uint Y;
	public uint Z;
>>>>>>> main
}