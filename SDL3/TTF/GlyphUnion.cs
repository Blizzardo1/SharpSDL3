using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Explicit)]
public struct GlyphUnion {
<<<<<<< HEAD

    [FieldOffset(0)]
    public GlyphSubpixel Subpixel;

    [FieldOffset(0)]
    public GlyphKerningSmart KerningSmart;
}
=======
    [FieldOffset(0)]
    public GlyphSubpixel Subpixel;
    [FieldOffset(0)]
    public GlyphKerningSmart KerningSmart;
}
>>>>>>> main
