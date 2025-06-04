using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct VirtualJoystickDesc {
    public uint Version;
    public ushort Type;
    public ushort Padding;
    public ushort VendorId;
    public ushort ProductId;
    public ushort NAxes;
    public ushort NButtons;
    public ushort NBalls;
    public ushort NHats;
    public ushort NTouchpads;
    public ushort NSensors;
    public unsafe fixed ushort Padding2[2];
    public uint ButtonMask;
    public uint AxisMask;
    public nint Name;
    public nint Touchpads;
    public nint Sensors;
    public nint Userdata;
    public nint Update; // WARN_ANONYMOUS_FUNCTION_POINTER
    public nint SetPlayerIndex; // WARN_ANONYMOUS_FUNCTION_POINTER
    public nint Rumble; // WARN_ANONYMOUS_FUNCTION_POINTER
    public nint RumbleTriggers; // WARN_ANONYMOUS_FUNCTION_POINTER
    public nint SetLED; // WARN_ANONYMOUS_FUNCTION_POINTER
    public nint SendEffect; // WARN_ANONYMOUS_FUNCTION_POINTER
    public nint SetSensorsEnabled; // WARN_ANONYMOUS_FUNCTION_POINTER
    public nint Cleanup; // WARN_ANONYMOUS_FUNCTION_POINTER
}