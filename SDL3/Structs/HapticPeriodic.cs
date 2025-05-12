using System.Runtime.InteropServices;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct HapticPeriodic
{
	public ushort Type;
	public HapticDirection Direction;
	public uint Length;
	public ushort Delay;
	public ushort Button;
	public ushort Interval;
	public ushort Period;
	public short Magnitude;
	public short Offset;
	public ushort Phase;
	public ushort AttackLength;
	public ushort AttackLevel;
	public ushort FadeLength;
	public ushort FadeLevel;
}