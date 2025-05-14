using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public struct BBox {
    public long XMin;
    public long YMin;
    public long XMax;
    public long YMax;
}