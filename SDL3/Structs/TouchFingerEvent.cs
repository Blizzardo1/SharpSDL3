using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct TouchFingerEvent
{
	public EventType Type;
	public uint Reserved;
	public ulong Timestamp;
	public ulong TouchId;
	public ulong FingerId;
	public float X;
	public float Y;
	public float Dx;
	public float Dy;
	public float Pressure;
	public uint WindowId;
}