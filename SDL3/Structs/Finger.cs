using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct Finger
{
	public ulong Id;
	public float X;
	public float Y;
	public float Pressure;
}