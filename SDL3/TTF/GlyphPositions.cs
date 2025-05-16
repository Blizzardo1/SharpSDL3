using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct GlyphPositions {
    nint Pos;
    int Len;
    int Width26Dot6;
    int Height26Dot6;
    int NumClusters;
    int MaxLen;
}
