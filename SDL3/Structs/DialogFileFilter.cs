using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct DialogFileFilter {
    public nint Name;
    public nint Pattern;
}