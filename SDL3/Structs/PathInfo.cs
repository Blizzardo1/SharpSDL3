using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct PathInfo
{
	public PathType Type;
	public ulong Size;
	public long CreateTime;
	public long ModifyTime;
	public long AccessTime;
}