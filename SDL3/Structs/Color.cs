using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct Color {
    public byte R;
    public byte G;
    public byte B;
    public byte A;
}