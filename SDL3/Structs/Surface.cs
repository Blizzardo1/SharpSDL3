using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

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