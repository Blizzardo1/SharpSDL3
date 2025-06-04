<<<<<<< HEAD
using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct KeyboardEvent {
    public EventType Type;
    public uint Reserved;
    public ulong Timestamp;
    public uint WindowId;
    public uint Which;
    public Scancode ScanCode;
    public uint Key;
    public KeyMod Mod;
    public ushort Raw;
    public SdlBool Down;
    public SdlBool Repeat;
=======
using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct KeyboardEvent
{
	public EventType Type;
	public uint Reserved;
	public ulong Timestamp;
	public uint WindowId;
	public uint Which;
	public ScanCode ScanCode;
	public uint Key;
	public KeyMod Mod;
	public ushort Raw;
	public SdlBool Down;
	public SdlBool Repeat;
>>>>>>> main
}