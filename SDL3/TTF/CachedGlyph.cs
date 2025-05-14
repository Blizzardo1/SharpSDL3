using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Explicit)]
public struct CachedGlyph {
    [StructLayout(LayoutKind.Sequential)]
    public struct SubPixel {
        public int LsbMinusRsb;
        public int Translation;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KerningSmart {
        public int RsbDelta;
        public int LsbDelta;
    }

    [FieldOffset(0)] public int Stored;
    [FieldOffset(4)] public uint index;
    [FieldOffset(36)] public Image Bitmap;
    [FieldOffset(68)] public Image Pixmap;
    [FieldOffset(72)] public int Left;
    [FieldOffset(76)] public int Top;
    [FieldOffset(80)] public int Width;
    [FieldOffset(86)] public int Rows;
    [FieldOffset(90)] public int Advance;
    // Union
    // TTF_HINTING_LIGHT_SUBPIXEL (only pixmap)
    [FieldOffset(94)] public SubPixel subpixel;
    // Other hinting
    [FieldOffset(94)] public KerningSmart kerning_smart;
    // End Union
}