using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct VirtualJoystickSensorDesc
{
	public SensorType Type;
	public float Rate;
}