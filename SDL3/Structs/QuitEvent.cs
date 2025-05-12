using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct QuitEvent
{
	public EventType Type;
	public uint Reserved3;
	public ulong Timestamp;
}