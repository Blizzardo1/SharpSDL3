using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

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