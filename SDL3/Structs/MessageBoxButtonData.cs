using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct MessageBoxButtonData
{
	public MessageBoxButtonFlags Flags;
	public int ButtonID;
	public byte* Text;
}