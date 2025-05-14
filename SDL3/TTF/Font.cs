using SharpSDL3.Structs;
using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;
[StructLayout(LayoutKind.Sequential)]
public unsafe struct Font {
    // The name of the font
    public string Name;

    // Freetype2 maintains all sorts of useful info itself
    public FtFace Face;
    public long FaceIndex;

    // Properties exposed to the application
    public int Props;

    // The current font generation, changes when glyphs need to be rebuilt
    public int Generation;

    // Text objects using this font
    public HashTable* Text;

    // We'll cache these ourselves
    public float Ptsize;
    public int Hdpi;
    public int Vdpi;
    public int Height;
    public int Ascent;
    public int Descent;
    public int Lineskip;

    // The font style
    public FontStyle Style;
    public int Weight;
    public int Outline;
    public Stroker Stroker;

    // Whether kerning is desired
    public bool EnableKerning;
    public bool UseKerning;

    // Extra width in glyph bounds for text styles
    public int GlyphOverhang;

    // Information in the font for underlining
    public int LineThickness;
    public int UnderlineTopRow;
    public int StrikethroughTopRow;

    // Cache for style-transformed glyphs
    public HashTable* Glyphs;
    public HashTable* GlyphIndices;

    // We are responsible for closing the font stream
    public IOStream* Src;
    public long SrcOffset;
    public bool Closeio;
    public FtOpenArgs Args;

    /* Internal buffer to store positions computed by TTF_Size_Internal()
     * for rendered string by Render_Line() */
    public int NextCachedPositions;
    public CachedGlyphPositions[] CachedPositions; // [8]
    public GlyphPositions* Positions;

    // Hinting modes
    public int FtLoadTarget;
    public int RenderSubpixel;
    public nint HbFont;
    public char HbLanguage;
    public int Script; // ISO 15924 script tag
    public Direction Direction;
    public bool RenderSdf;

    // Extra layout setting for wrapped text
    public HorizontalAlignment HorizontalAlign;

    // Fallback fonts
    public FontList* Fallbacks;
    public FontList* FallbackFor;
};
