using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct DisplayMode {
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