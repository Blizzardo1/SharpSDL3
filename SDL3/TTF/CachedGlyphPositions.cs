using SharpSDL3.Structs;
using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct CachedGlyphPositions {
    public Direction direction;
    public int script;
    public string text;
    public Size length;
    public GlyphPositions positions;
}