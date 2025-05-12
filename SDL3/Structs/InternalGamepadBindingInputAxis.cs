using System.Runtime.InteropServices;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct InternalGamepadBindingInputAxis
{
	public int Axis;
	public int AxisMin;
	public int AxisMax;
}