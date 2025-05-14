using SharpSDL3.Structs;
using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public struct FtFace {
    public long NumFaces;
    public long FaceIndex;
    public long FaceFlags;
    public long StyleFlags;
    public long NumGlyphs;

    public string FamilyName;
    public string StyleName;

    public int NumFixedSizes;
    public nint AvailableSizes;

    public int NumCharmaps;
    public nint CharMaps;

    public nint Generic;

    /* The following member variables (down to `underline_thickness`) */
    /* are only relevant to scalable outlines; cf. @FT_Bitmap_Size    */
    /* for bitmap fonts.                                              */
    public BBox Bbox;

    public ushort UnitsPerEM;
    public short Ascender;
    public short Descender;
    public short Height;

    public short MaxAdvanceWidth;
    public short MaxAdvanceHeight;

    public short UnderlinePosition;
    public short UnderlineThickness;

    public nint Glyph;
    public Size Size;
    public nint CharMap;

    /* private fields, internal to FreeType */

    public nint Driver;
    public Memory Memory;
    public FtStream Stream;

    public nint SizesList;

    public nint AutoHint;   /* face-specific auto-hinter data */
    public nint Extensions; /* unused                         */

    public nint FaceInternal;

  }
