using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct PenAxisEvent
{
	public EventType Type;
	public uint Reserved;
	public ulong Timestamp;
	public uint WindowId;
	public uint Which;
	public PenInputFlags PenState;
	public float X;
	public float Y;
	public PenAxis Axis;
	public float Value;
}