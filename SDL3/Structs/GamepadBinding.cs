<<<<<<< HEAD
using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Explicit)]
public struct GamepadBinding {
    [FieldOffset(0)] public GamepadBindingType InputType;
    [FieldOffset(4)] public int InputButton;
    [FieldOffset(4)] public InternalGamepadBindingInputAxis InputAxis;
    [FieldOffset(4)] public InternalGamepadBindingInputHat InputHat;
    [FieldOffset(16)] public GamepadBindingType OutputType;
    [FieldOffset(20)] public GamepadButton OutputButton;
    [FieldOffset(20)] public InternalGamepadBindingOutputAxis OutputAxis;
=======
using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

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
>>>>>>> main
}