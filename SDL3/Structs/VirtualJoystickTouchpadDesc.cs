using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct VirtualJoystickTouchpadDesc {
    public ushort NFingers;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public ushort[] Padding;
}