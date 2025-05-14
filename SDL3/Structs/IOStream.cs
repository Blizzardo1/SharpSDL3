using System;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct IOStream {
    public IoStreamInterface Interface;
    public nint Userdata;
    public IOStatus Status;
    public int Props;
}
