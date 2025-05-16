using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public struct GlyphKerningSmart {
    public int RsbDelta;
    public int LsbDelta;
}
