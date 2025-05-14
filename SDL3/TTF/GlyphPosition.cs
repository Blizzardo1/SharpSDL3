using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct GlyphPosition {
    public Font* Font;
    public uint Index;
    public CachedGlyph* Glyph;
    public int XOffset;
    public int YOffset;
    public int XAdvance;
    public int YAdvance;
    public int X;
    public int Y;
    public int Offset;
}
