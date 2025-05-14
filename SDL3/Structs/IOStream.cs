using System;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct IOStream {
    IOStreamInterface Interface;
    nint Userdata;
    IOStatus Status;
    int Props;
}
