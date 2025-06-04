using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct DisplayEvent {
    public EventType Type;
    public uint Reserved;
    public ulong Timestamp;
    public uint DisplayId;
    public int Data1;
    public int Data2;
}