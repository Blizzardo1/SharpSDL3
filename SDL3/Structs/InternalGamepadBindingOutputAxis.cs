using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct InternalGamepadBindingOutputAxis
{
	public GamepadAxis Axis;
	public int AxisMin;
	public int AxisMax;
}