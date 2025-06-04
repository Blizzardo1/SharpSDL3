using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct GlyphPosition {
    public nint Font;
    public uint Index;
    public nint Glyph;
    public int XOffset;
    public int YOffset;
    public int XAdvance;
    public int YAdvance;
    public int X;
    public int Y;
    public int Offset;
}