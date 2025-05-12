using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct Surface
{
	public SurfaceFlags Flags;
	public PixelFormat Format;
	public int W;
	public int H;
	public int Pitch;
	public nint Pixels;
	public int Refcount;
	public nint Reserved;
}