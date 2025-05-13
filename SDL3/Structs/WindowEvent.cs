using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct WindowEvent
{
	public EventType Type;
	public uint Reserved;
	public ulong Timestamp;
	public uint WindowId;
	public int Data1;
	public int Data2;
}