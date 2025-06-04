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
	public fixed ushort RightSat[3];
	public fixed ushort LeftSat[3];
	public fixed short RightCoeff[3];
	public fixed short LeftCoeff[3];
	public fixed ushort Deadband[3];
	public fixed short Center[3];
}