using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct Rect
{
	public int X;
	public int Y;
	public int W;
	public int H;
}