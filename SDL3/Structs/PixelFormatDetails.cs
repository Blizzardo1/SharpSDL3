using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct PixelFormatDetails
{
	public PixelFormat Format;
	public byte BitsPerPixel;
	public byte BytesPerPixel;
	public fixed byte Padding[2];
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