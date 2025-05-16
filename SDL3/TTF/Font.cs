#define TTF_USE_HARFBUZZ

using SharpSDL3.Structs;
using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;
[StructLayout(LayoutKind.Sequential)]
public struct Font {

    // The name of the font
    
    public string? Name;

    // Freetype2 maintains all sorts of useful info itself
    public FtFace Face;
    public long FaceIndex;

    // Properties exposed to the application
    public readonly int Props => Ttf.GetFontProperties(this);

    // The current font generation, changes when glyphs need to be rebuilt
    public readonly int Generation => Ttf.GetFontGeneration(this);

    // Text objects using this font
    public nint Text;

    // We'll cache these ourselves
    public readonly float Ptsize => Ttf.GetFontSize(this);
    public int Hdpi;
    public int Vdpi;
    public readonly int Height => Ttf.GetFontHeight(this);
    public readonly int Ascent => Ttf.GetFontAscent(this);
    public readonly int Descent => Ttf.GetFontDescent(this);
    public readonly int Lineskip => Ttf.GetFontLineSkip(this);

    // The font style
    public readonly FontStyle Style => Ttf.GetFontStyle(this);
    public readonly FontWeight Weight => Ttf.GetFontWeight(this);
    public readonly int Outline => Ttf.GetFontOutline(this);
    public Stroker Stroker;

    // Whether kerning is desired
    public readonly bool EnableKerning {
        get => Ttf.GetFontKerning(this);
        set => Ttf.SetFontKerning(this, value);
    }
#if !TTF_USE_HARFBUZZ
    public readonly bool UseKerning;
#endif

    // Extra width in glyph bounds for text styles
    public int GlyphOverhang;

    // Information in the font for underlining
    public int LineThickness;
    public int UnderlineTopRow;
    public int StrikethroughTopRow;

    // Cache for style-transformed glyphs
    public nint Glyphs;
    public nint GlyphIndices;

    // We are responsible for closing the font stream
    public nint Src;
    public long SrcOffset;
    public bool CloseIo;
    public FtOpenArgs Args;

    /* Internal buffer to store positions computed by TTF_Size_Internal()
     * for rendered string by Render_Line() */
    public int NextCachedPositions;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public CachedGlyphPositions[] CachedPositions;
    public nint Positions;

    // Hinting modes
    public int FtLoadTarget;
    public int RenderSubpixel;
#if TTF_USE_HARFBUZZ
    public nint HbFont;
    public char HbLanguage;
#endif
    public int Script; // ISO 15924 script tag
    public readonly Direction Direction {
        get => Ttf.GetFontDirection(this);
        set => Ttf.SetFontDirection(this, value);
    }
    public bool RenderSdf;

    // Extra layout setting for wrapped text
    public HorizontalAlignment HorizontalAlign;

    // Fallback fonts
    public nint Fallbacks;
    public nint FallbackFor;

    public static bool GetTextSize(string text, out int width, out int height) => Ttf.GetTextSize(Marshal.StringToHGlobalUni(text), out width, out height);
    public readonly nint RenderTextSolid(string text, Color foreground) {
        if(!GetTextSize(text, out int width, out int height)) {
            Logger.LogError(Enums.LogCategory.System, "Failed to get text size.");
            return nint.Zero;
        }
        if (width <= 0 || height <= 0) {
            Logger.LogError(Enums.LogCategory.System, "Invalid text size.");
            return nint.Zero;
        }

        return Ttf.RenderTextSolid(this, text, new(width, height), foreground);
    }

    public readonly nint RenderTextShaded(string text, Color foreground, Color background) {
        if (!GetTextSize(text, out int width, out int height)) {
            Logger.LogError(Enums.LogCategory.System, "Failed to get text size.");
            return nint.Zero;
        }
        if (width <= 0 || height <= 0) {
            Logger.LogError(Enums.LogCategory.System, "Invalid text size.");
            return nint.Zero;
        }
        return Ttf.RenderTextShaded(this, text, new(width, height), foreground, background);
    }

    public readonly nint RenderGlyphSolid(char c, Color foregroundColor) => Ttf.RenderGlyphSolid(this, c, foregroundColor);
    public readonly nint RenderGlyphShaded(char c, Color foregroundColor, Color bg) => Ttf.RenderGlyphShaded(this, c, foregroundColor, bg);
    public readonly nint RenderTextBlended(string text, Color foregroundColor) => Ttf.RenderTextBlended(this, text, foregroundColor);
    public readonly nint RenderGlyphBlended(char c, Color foregroundColor) => Ttf.RenderGlyphBlended(this, c, foregroundColor);
    public readonly nint RenderTextBlendedWrapped(string text, Color foregroundColor, int wrapped_width) => Ttf.RenderTextBlendedWrapped(this, text, Ttf.GetTextSize(text), foregroundColor, wrapped_width);
    public readonly void Close() {
        Ttf.CloseFont(this);
    }
};
