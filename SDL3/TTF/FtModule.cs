using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public struct FtModule{
    public nint clazz;
    public nint library;
    public Memory memory;

}