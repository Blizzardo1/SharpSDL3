using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct SensorEvent {
    public EventType Type;
    public uint Reserved;
    public ulong Timestamp;
    public uint Which;
    public fixed float Data[6];
    public ulong SensorTimestamp;
}