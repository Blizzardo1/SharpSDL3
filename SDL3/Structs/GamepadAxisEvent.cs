using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GamepadAxisEvent
{
	public EventType Type;
	public uint Reserved;
	public ulong Timestamp;
	public uint Which;
	public byte Axis;
	public byte Padding1;
	public byte Padding2;
	public byte Padding3;
	public short Value;
	public ushort Padding4;
}