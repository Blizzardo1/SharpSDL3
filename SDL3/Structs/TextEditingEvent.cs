using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct TextEditingEvent
{
	public EventType Type;
	public uint Reserved;
	public ulong Timestamp;
	public uint WindowId;
	public byte* Text;
	public int Start;
	public int Length;
}