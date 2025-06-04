using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct Image {
    public byte* Buffer; // aligned
    public int Left;
    public int Top;
    public int Width;
    public int Rows;
    public int Pitch;
    public int IsColor;
}
