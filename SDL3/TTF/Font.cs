using SharpSDL3.Structs;
using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential, Size = 384)]
public unsafe struct Font {

    [MarshalAs(Sdl.StringType)]
    public string Name;

    public nint Face;
    public long FaceIndex;
    public nint Props;
    public readonly uint Generation => (uint)Ttf.GetFontGeneration(this);
    public nint Text;

    public readonly float Size {
        get => Ttf.GetFontSize(this);
        set => Ttf.SetFontSize(this, value);
    }

    public int Hdpi;
    public int Vdpi;
    public readonly int Height => Ttf.GetFontHeight(this);
    public readonly int Ascent => Ttf.GetFontAscent(this);
    public readonly int Descent => Ttf.GetFontDescent(this);
    public readonly int Lineskip => Ttf.GetFontLineSkip(this);

    public readonly FontStyle Style {
        get => Ttf.GetFontStyle(this);
        set => Ttf.SetFontStyle(this, value);
    }

    public readonly FontWeight Weight => Ttf.GetFontWeight(this);
    public int Outline;
    public nint Stroker;

    public readonly SdlBool EnableKerning {
        get => Ttf.GetFontKerning(this);
        set => Ttf.SetFontKerning(this, value);
    }

#if !TTF_USE_HARFBUZZ
    public SdlBool UseKerning;
#endif
    public int GlyphOverhang;
    public int LineThickness;
    public int UnderlineTopRow;
    public int StrikethroughTopRow;
    public nint Glyphs;
    public nint GlyphIndices;
    public nint Src;
    public long SrcOffset;
    public SdlBool CloseIo;
    public nint Args;
    public int NextCachedPositions;
    public fixed byte CachedPositions[256]; // Replace sizeof(CachedGlyphPositions) with its known size (32 bytes in this case)
    public nint Positions;
    public int FtLoadTarget;
    public int RenderSubpixel;
#if TTF_USE_HARFBUZZ
       public nint HbFont;
       public nint HbLanguage;
#endif
    public uint Script;
    public int Direction;
    public SdlBool RenderSdf;
    public int HorizontalAlign;
    public nint Fallbacks;
    public nint FallbackFor;

    public nint Handle {
        get;
        internal set;
    }

    public readonly Size GetTextSize(string text) => Ttf.MeasureString(this, text);

    public readonly nint RenderTextSolid(string text, Color foregroundColor) => Ttf.RenderTextSolid(this, text, (ulong)text.Length, foregroundColor);

    public readonly nint RenderGlyphSolid(char c, Color foregroundColor) => Ttf.RenderGlyphSolid(this, c, foregroundColor);

    public readonly nint RenderTextShaded(string text, Color foregroundColor, Color bg) => Ttf.RenderTextShaded(this, text, (ulong)text.Length, foregroundColor, bg);

    public readonly nint RenderGlyphShaded(char c, Color foregroundColor, Color bg) => Ttf.RenderGlyphShaded(this, c, foregroundColor, bg);

    public readonly nint RenderTextBlended(string text, Color foregroundColor) => Ttf.RenderTextBlended(this, text, foregroundColor);

    public readonly nint RenderGlyphBlended(char c, Color foregroundColor) => Ttf.RenderGlyphBlended(this, c, foregroundColor);

    public readonly nint RenderTextBlendedWrapped(string text, Color foregroundColor, int wrapped) => Ttf.RenderTextBlendedWrapped(this, text, foregroundColor, wrapped);

    public readonly SdlBool GetFontKerningSize() => Ttf.GetFontKerning(this);

    public readonly void Close() => Ttf.CloseFont(this);
}