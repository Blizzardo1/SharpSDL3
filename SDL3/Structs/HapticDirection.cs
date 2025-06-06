using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct HapticDirection {
    public byte Type;
    public fixed int Dir[3];
}