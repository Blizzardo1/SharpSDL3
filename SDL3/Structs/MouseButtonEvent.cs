using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct MouseButtonEvent
{
	public EventType Type;
	public uint Reserved;
	public ulong Timestamp;
	public uint WindowId;
	public uint Which;
	public byte Button;
	public SdlBool Down;
	public byte Clicks;
	public byte Padding;
	public float X;
	public float Y;
}