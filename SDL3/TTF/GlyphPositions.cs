using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct GlyphPositions {
    public nint Pos;
    public int Len;
    public int Width26Dot6;
    public int Height26Dot6;
    public int NumClusters;
    public int MaxLen;
}