using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct MessageBoxButtonData {
    public MessageBoxDefaultButton Flags;
    public int ButtonID;
    public nint Text;
}