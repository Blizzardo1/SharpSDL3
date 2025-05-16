using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct HapticDirection
{
	public byte Type;
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
	public int[] Dir;
}