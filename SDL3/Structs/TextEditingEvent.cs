using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct TextEditingEvent
{
	public EventType Type;
	public uint Reserved;
	public ulong Timestamp;
	public uint WindowId;
	public nint Text;
	public int Start;
	public int Length;
}