using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct VirtualJoystickSensorDesc {
    public SensorType Type;
    public float Rate;
}