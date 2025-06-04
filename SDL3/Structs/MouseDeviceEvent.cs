using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct MouseDeviceEvent {
    public EventType Type;
    public uint Reserved;
    public ulong Timestamp;
    public uint Which;
}