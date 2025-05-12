using System.Runtime.InteropServices;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct MessageBoxColor
{
	public byte R;
	public byte D;
	public byte B;
}