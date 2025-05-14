using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Explicit)]
public struct FtStreamDesc {
    [FieldOffset(0)]public long Value;
    [FieldOffset(0)]public nint Pointer;
}