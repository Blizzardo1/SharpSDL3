using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct CommonEvent
{
	public uint Type;
	public uint Reserved;
	public ulong Timestamp;
}