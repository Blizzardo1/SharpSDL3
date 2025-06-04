using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Explicit)]
public struct HapticEffect
{
	[FieldOffset(0)] public ushort Type;
	[FieldOffset(0)] public HapticConstant Constant;
	[FieldOffset(0)] public HapticPeriodic Periodic;
	[FieldOffset(0)] public HapticCondition Condition;
	[FieldOffset(0)] public HapticRamp Ramp;
	[FieldOffset(0)] public HapticLeftRight LeftRight;
	[FieldOffset(0)] public HapticCustom Custom;
}