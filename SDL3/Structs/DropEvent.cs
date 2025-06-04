<<<<<<< HEAD
using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct DropEvent {
    public EventType Type;
    public uint Reserved;
    public ulong Timestamp;
    public uint WindowId;
    public float X;
    public float Y;
    public nint Source;
    public nint Data;
=======
using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct DropEvent
{
	public EventType Type;
	public uint Reserved;
	public ulong Timestamp;
	public uint WindowId;
	public float X;
	public float Y;
	public nint Source;
	public nint Data;
>>>>>>> main
}