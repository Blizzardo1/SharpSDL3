using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct JoyBallEvent
{
	public EventType Type;
	public uint Reserved;
	public ulong Timestamp;
	public uint Which;
	public byte Ball;
	public byte Padding1;
	public byte Padding2;
	public byte Padding3;
	public short Xrel;
	public short Yrel;
}