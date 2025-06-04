using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct ClipboardEvent {
    public EventType Type;
    public uint Reserved;
    public ulong Timestamp;
    public SdlBool Owner;
    public int NumMimeTypes;
    public nint* MimeTypes;
}