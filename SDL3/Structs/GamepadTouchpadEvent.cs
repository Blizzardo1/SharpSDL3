<<<<<<< HEAD
using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GamepadTouchpadEvent {
    public EventType Type;
    public uint Reserved;
    public ulong Timestamp;
    public uint Which;
    public int Touchpad;
    public int Finger;
    public float X;
    public float Y;
    public float Pressure;
=======
using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GamepadTouchpadEvent
{
	public EventType Type;
	public uint Reserved;
	public ulong Timestamp;
	public uint Which;
	public int Touchpad;
	public int Finger;
	public float X;
	public float Y;
	public float Pressure;
>>>>>>> main
}