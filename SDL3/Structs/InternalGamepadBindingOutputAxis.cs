using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct InternalGamepadBindingOutputAxis
{
	public GamepadAxis Axis;
	public int AxisMin;
	public int AxisMax;
}