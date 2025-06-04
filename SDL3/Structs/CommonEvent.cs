using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct CommonEvent {
    public EventType Type;
    public uint Reserved;
    public ulong Timestamp;
}