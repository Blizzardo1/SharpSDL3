using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct Glyph {
    public int Stored;
    public uint Index;
    public Image Bitmap;
    public Image Pixmap;
    public int SzLeft;
    public int SzTop;
    public int SzWidth;
    public int SzRows;
    public int Advance;
    public GlyphUnion Hinting;
}
