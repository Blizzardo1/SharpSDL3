using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct PixelFormatDetails {
    public PixelFormat Format;
    public byte BitsPerPixel;
    public byte BytesPerPixel;
    public unsafe fixed byte Padding[2];
    public uint RMask;
    public uint GMask;
    public uint BMask;
    public uint AMask;
    public byte RBits;
    public byte GBits;
    public byte BBits;
    public byte ABits;
    public byte RShift;
    public byte GShift;
    public byte BShift;
    public byte AShift;
}