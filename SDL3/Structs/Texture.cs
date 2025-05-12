using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct Texture
{
	public PixelFormat Format;
	public int W;
	public int H;
	public int RefCount;
}