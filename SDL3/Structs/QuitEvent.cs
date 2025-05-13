using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct QuitEvent
{
	public EventType Type;
	public uint Reserved3;
	public ulong Timestamp;
}