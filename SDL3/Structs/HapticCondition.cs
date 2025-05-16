using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct HapticCondition {
	public ushort Type;
	public HapticDirection Direction;
	public uint Length;
	public ushort Delay;
	public ushort Button;
	public ushort Interval;
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
	public ushort[] RightSat;
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
	public ushort[] LeftSat;
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
	public short[] RightCoeff;
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
	public short[] LeftCoeff;
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
	public ushort[] Deadband;
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
	public short[] Center;
}