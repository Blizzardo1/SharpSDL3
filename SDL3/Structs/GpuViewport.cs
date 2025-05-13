using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuViewport
{
	public float X;
	public float Y;
	public float W;
	public float H;
	public float MinDepth;
	public float MaxDepth;
}