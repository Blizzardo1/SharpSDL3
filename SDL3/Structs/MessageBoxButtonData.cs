using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct MessageBoxButtonData
{
	public MessageBoxDefaultButton Flags;
	public int ButtonID;
	public byte* Text;
}