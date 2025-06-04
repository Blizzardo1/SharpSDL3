<<<<<<< HEAD
using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct MouseWheelEvent {
    public EventType Type;
    public uint Reserved;
    public ulong Timestamp;
    public uint WindowId;
    public uint Which;
    public float X;
    public float Y;
    public MouseWheelDirection Direction;
    public float MouseX;
    public float MouseY;
=======
using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct MouseWheelEvent
{
	public EventType Type;
	public uint Reserved;
	public ulong Timestamp;
	public uint WindowId;
	public uint Which;
	public float X;
	public float Y;
	public MouseWheelDirection Direction;
	public float MouseX;
	public float MouseY;
>>>>>>> main
}