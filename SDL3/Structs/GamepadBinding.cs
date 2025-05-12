using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Explicit)]
public struct GamepadBinding
{
	[FieldOffset(0)] public GamepadBindingType InputType;
	[FieldOffset(4)] public int InputButton;
	[FieldOffset(4)] public InternalGamepadBindingInputAxis InputAxis;
	[FieldOffset(4)] public InternalGamepadBindingInputHat InputHat;
	[FieldOffset(16)] public GamepadBindingType OutputType;
	[FieldOffset(20)] public GamepadButton OutputButton;
	[FieldOffset(20)] public InternalGamepadBindingOutputAxis OutputAxis;
}