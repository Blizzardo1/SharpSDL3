using System.Runtime.InteropServices;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct DialogFileFilter
{
	public byte* Name;
	public byte* Pattern;
}