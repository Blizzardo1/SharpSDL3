<<<<<<< HEAD
using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct RenderEvent {
    public EventType Type;
    public uint Reserved;
    public ulong Timestamp;
    public uint WindowId;
=======
using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct RenderEvent
{
	public EventType Type;
	public uint Reserved;
	public ulong Timestamp;
	public uint WindowId;
>>>>>>> main
}