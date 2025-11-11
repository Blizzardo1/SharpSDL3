using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct IoStream {
    public IoStreamInterface Interface;
    public nint Userdata;
    public IoStatus Status;
    public int Props;

    public nint Handle;
}