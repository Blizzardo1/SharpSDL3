using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct JoyHatEvent
{
	public EventType Type;
	public uint Reserved;
	public ulong Timestamp;
	public uint Which;
	public byte Hat;
	public byte Value;
	public byte Padding1;
	public byte Padding2;
}