using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct ClipboardEvent
{
	public EventType Type;
	public uint Reserved;
	public ulong Timestamp;
	public SdlBool Owner;
	public int NumMimeTypes;
	public nint* MimeTypes;
}