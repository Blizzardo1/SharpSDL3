using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct PixelFormatDetails
{
	public PixelFormat Format;
	public byte BitsPerPixel;
	public byte BytesPerPixel;
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	public byte[] Padding;
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