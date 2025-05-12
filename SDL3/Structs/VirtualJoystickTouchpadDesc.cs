using System.Runtime.InteropServices;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct VirtualJoystickTouchpadDesc
{
	public ushort NFingers;
	public fixed ushort Padding[3];
}