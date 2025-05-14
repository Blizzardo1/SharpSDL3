using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public struct Image {
    public string Buffer; // aligned
    public int Left;
    public int Top;
    public int Width;
    public int Rows;
    public int Pitch;
    public int IsColor;
}