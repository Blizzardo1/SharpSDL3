using System.Runtime.InteropServices;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct UserEvent
{
	public uint Type;
	public uint Reserved;
	public ulong Timestamp;
	public uint WindowId;
	public int Code;
	public nint Data1;
	public nint Data2;
}