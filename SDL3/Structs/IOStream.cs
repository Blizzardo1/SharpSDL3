<<<<<<< HEAD
using System.Runtime.InteropServices;
=======
﻿using System.Runtime.InteropServices;
>>>>>>> main

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct IOStream {
    public IOStreamInterface Interface;
    public nint Userdata;
    public IOStatus Status;
    public int Props;

    public nint Handle;
<<<<<<< HEAD
}
=======
}
>>>>>>> main
