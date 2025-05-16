using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct MouseButtonEvent
{
	public EventType Type;
	public uint Reserved;
	public ulong Timestamp;
	public uint WindowId;
	public uint Which;
	public byte Button;
	public bool Down;
	public byte Clicks;
	public byte Padding;
	public float X;
	public float Y;
}