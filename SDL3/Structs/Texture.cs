using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct Texture {
    public PixelFormat Format;
    public int W;
    public int H;
    public int RefCount;
}