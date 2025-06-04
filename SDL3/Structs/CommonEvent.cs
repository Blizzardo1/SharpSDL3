using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
<<<<<<< HEAD
public struct CommonEvent {
    public EventType Type;
    public uint Reserved;
    public ulong Timestamp;
=======
public struct CommonEvent
{
	public EventType Type;
	public uint Reserved;
	public ulong Timestamp;
>>>>>>> main
}