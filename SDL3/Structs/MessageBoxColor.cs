using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct MessageBoxColor {
    public byte R;
    public byte G;
    public byte B;
}