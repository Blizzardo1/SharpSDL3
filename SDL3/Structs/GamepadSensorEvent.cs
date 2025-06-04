<<<<<<< HEAD
using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct GamepadSensorEvent {
    public EventType Type;
    public uint Reserved;
    public ulong Timestamp;
    public uint Which;
    public int Sensor;
    public fixed float Data[3];
    public ulong SensorTimestamp;
=======
using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct GamepadSensorEvent
{
	public EventType Type;
	public uint Reserved;
	public ulong Timestamp;
	public uint Which;
	public int Sensor;
	public fixed float Data[3];
	public ulong SensorTimestamp;
>>>>>>> main
}