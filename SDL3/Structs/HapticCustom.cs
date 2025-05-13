using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct HapticCustom
{
	public ushort Type;
	public HapticDirection Direction;
	public uint Length;
	public ushort Delay;
	public ushort Button;
	public ushort Interval;
	public byte Channels;
	public ushort Period;
	public ushort Samples;
	public ushort* Data;
	public ushort AttackLength;
	public ushort AttackLevel;
	public ushort FadeLength;
	public ushort FadeLevel;
}