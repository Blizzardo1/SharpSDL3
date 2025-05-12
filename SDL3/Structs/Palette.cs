using System.Runtime.InteropServices;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct Palette
{
	public int NColors;
	public Color* Colors;
	public uint Version;
	public int RefCount;
}