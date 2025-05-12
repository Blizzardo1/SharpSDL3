using System.Runtime.InteropServices;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct HapticLeftRight
{
	public ushort Type;
	public uint Length;
	public ushort LargeMagnitude;
	public ushort SmallMagnitude;
}