using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct CachedGlyphPositions {
    public int Direction; // TTF_Direction (use int or appropriate enum)
    public uint Script;
    public sbyte* Text;
    public nuint Length;
    public GlyphPositions Positions;
}
