using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct FColor
{
	public float R;
	public float G;
	public float B;
	public float A;
}