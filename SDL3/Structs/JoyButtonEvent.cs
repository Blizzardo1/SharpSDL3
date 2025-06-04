<<<<<<< HEAD
using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct JoyButtonEvent {
    public EventType Type;
    public uint Reserved;
    public ulong Timestamp;
    public uint Which;
    public byte Button;
    public SdlBool Down;
    public byte Padding1;
    public byte Padding2;
=======
using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct JoyButtonEvent
{
	public EventType Type;
	public uint Reserved;
	public ulong Timestamp;
	public uint Which;
	public byte Button;
	public SdlBool Down;
	public byte Padding1;
	public byte Padding2;
>>>>>>> main
}