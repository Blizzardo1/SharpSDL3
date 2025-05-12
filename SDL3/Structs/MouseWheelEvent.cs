using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct MouseWheelEvent
{
	public EventType Type;
	public uint Reserved;
	public ulong Timestamp;
	public uint WindowId;
	public uint Which;
	public float X;
	public float Y;
	public MouseWheelDirection Direction;
	public float MouseX;
	public float MouseY;
}