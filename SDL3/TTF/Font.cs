using SharpSDL3.Structs;
using System;
using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct Font {
    [MarshalAs(UnmanagedType.LPStr)]
    public string Name;
    public nint Face;
    public long FaceIndex;
    public nint Props;
    public uint Generation;
    public nint Text;
    public float Ptsize;
    public int Hdpi;
    public int Vdpi;
    public readonly int Height => Ttf.GetFontHeight(this);
    public readonly int Ascent => Ttf.GetFontAscent(this);
    public readonly int Descent => Ttf.GetFontDescent(this);
    public readonly int Lineskip => Ttf.GetFontLineSkip(this);
    public readonly FontStyle Style => Ttf.GetFontStyle(this);
    public readonly FontWeight Weight => Ttf.GetFontWeight(this);
    public int Outline;
    public nint Stroker;
    public readonly bool EnableKerning {
        get => Ttf.GetFontKerning(this);
        set => Ttf.SetFontKerning(this, value);
    }
#if !TTF_USE_HARFBUZZ
    public byte UseKerning;
#endif
    public int GlyphOverhang;
    public int LineThickness;
    public int UnderlineTopRow;
    public int StrikethroughTopRow;
    public nint Glyphs;
    public nint GlyphIndices;
    public nint Src;
    public long SrcOffset;
    public byte Closeio;
    public nint Args;
    public int NextCachedPositions;
    public fixed byte CachedPositions[8 * 32]; // Replace sizeof(CachedGlyphPositions) with its known size (32 bytes in this case)
    public nint Positions;
    public int FtLoadTarget;
    public int RenderSubpixel;
#if TTF_USE_HARFBUZZ
       public nint HbFont;
       public nint HbLanguage;
#endif
    public uint Script;
    public int Direction;
    public byte RenderSdf;
    public int HorizontalAlign;
    public FontList* Fallbacks;
    public FontList* FallbackFor;

    public static Size GetTextSize(string text) => Ttf.GetTextSize(text);
    public readonly nint RenderTextSolid(string text, Color foregroundColor) => Ttf.RenderTextSolid(this, text, GetTextSize(text), foregroundColor);
    public readonly nint RenderGlyphSolid(char c, Color foregroundColor) => Ttf.RenderGlyphSolid(this, c, foregroundColor);
    public readonly nint RenderTextShaded(string text, Color foregroundColor, Color bg) => Ttf.RenderTextShaded(this, text, GetTextSize(text), foregroundColor, bg);
    public readonly nint RenderGlyphShaded(char c, Color foregroundColor, Color bg) => Ttf.RenderGlyphShaded(this, c, foregroundColor, bg);
    public readonly nint RenderTextBlended(string text, Color foregroundColor) => Ttf.RenderTextBlended(this, text, foregroundColor);
    public readonly nint RenderGlyphBlended(char c, Color foregroundColor) => Ttf.RenderGlyphBlended(this, c, foregroundColor);
    public readonly nint RenderTextBlendedWrapped(string text, Color foregroundColor, int wrapped) => Ttf.RenderTextBlendedWrapped(this, text, foregroundColor, wrapped);

    public readonly bool GetFontKerningSize() => Ttf.GetFontKerning(this);

    public readonly void Close() => Ttf.CloseFont(this);
}