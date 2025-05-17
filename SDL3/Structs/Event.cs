using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Explicit, Size = 128)]
public unsafe struct Event {
    [FieldOffset(0)] public EventType Type;
	[FieldOffset(0)] public CommonEvent Common;
	[FieldOffset(0)] public DisplayEvent Display;
	[FieldOffset(0)] public WindowEvent Window;
	[FieldOffset(0)] public KeyboardDeviceEvent KDevice;
	[FieldOffset(0)] public KeyboardEvent Key;
	[FieldOffset(0)] public TextEditingEvent Edit;
	[FieldOffset(0)] public TextEditingCandidatesEvent EditCandidates;
	[FieldOffset(0)] public TextInputEvent Text;
	[FieldOffset(0)] public MouseDeviceEvent MDevice;
	[FieldOffset(0)] public MouseMotionEvent Motion;
	[FieldOffset(0)] public MouseButtonEvent Button;
	[FieldOffset(0)] public MouseWheelEvent Wheel;
	[FieldOffset(0)] public JoyDeviceEvent JDevice;
	[FieldOffset(0)] public JoyAxisEvent JAxis;
	[FieldOffset(0)] public JoyBallEvent JBall;
	[FieldOffset(0)] public JoyHatEvent JHat;
	[FieldOffset(0)] public JoyButtonEvent JButton;
	[FieldOffset(0)] public JoyBatteryEvent JBattery;
	[FieldOffset(0)] public GamepadDeviceEvent GDevice;
	[FieldOffset(0)] public GamepadAxisEvent GAxis;
	[FieldOffset(0)] public GamepadButtonEvent GButton;
	[FieldOffset(0)] public GamepadTouchpadEvent GTouchpad;
	[FieldOffset(0)] public GamepadSensorEvent GSensor;
	[FieldOffset(0)] public AudioDeviceEvent ADevice;
	[FieldOffset(0)] public CameraDeviceEvent CDevice;
	[FieldOffset(0)] public SensorEvent Sensor;
	[FieldOffset(0)] public QuitEvent Quit;
	[FieldOffset(0)] public UserEvent User;
	[FieldOffset(0)] public TouchFingerEvent TFinger;
	[FieldOffset(0)] public PenProximityEvent PProximity;
	[FieldOffset(0)] public PenTouchEvent PTouch;
	[FieldOffset(0)] public PenMotionEvent PMotion;
	[FieldOffset(0)] public PenButtonEvent PButton;
	[FieldOffset(0)] public PenAxisEvent PAxis;
	[FieldOffset(0)] public RenderEvent Render;
	[FieldOffset(0)] public DropEvent Drop;
	[FieldOffset(0)] public ClipboardEvent Clipboard;

    /// <summary>
	/// <para>
    /// This is necessary for ABI compatibility between Visual C++ and GCC.
	/// </para>
    /// Visual C++ will respect the push pack pragma and use 52 bytes (size of
    /// SDL_TextEditingEvent, the largest structure for 32-bit and 64-bit
    /// architectures) for this union, and GCC will use the alignment of the
    /// largest datatype within the union, which is 8 bytes on 64-bit
    /// architectures.
    /// <para>
    /// So... we'll add padding to force the size to be the same for both.
	/// </para>
    /// <para>
    /// On architectures where pointers are 16 bytes, this needs rounding up to
    /// the next multiple of 16, 64, and on architectures where pointers are
    /// even larger the size of SDL_UserEvent will dominate as being 3 pointers.
	/// </para>
    /// </summary>
    [FieldOffset(0)] private fixed byte Padding[128];
}