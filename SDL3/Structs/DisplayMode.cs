using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct DisplayMode
{
	public uint DisplayId;
	public PixelFormat Format;
	public int W;
	public int H;
	public float PixelDensity;
	public float RefreshRate;
	public int RefreshRateNumerator;
	public int RefreshRateDenominator;
	public nint Internal;
}