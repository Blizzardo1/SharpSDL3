using System.Runtime.InteropServices;
using static SharpSDL3.Delegates;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public struct Memory {
    public nint user;
    public FtAllocFunc alloc;
    public FtFreeFunc free;
    public FtReallocFunc realloc;

}