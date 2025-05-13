using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct CameraSpec
{
	public PixelFormat Format;
	public Colorspace ColorSpace;
	public int Width;
	public int Height;
	public int FramerateNumerator;
	public int FramerateDenominator;
}