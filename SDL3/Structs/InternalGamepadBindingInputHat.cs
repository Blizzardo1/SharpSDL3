using System.Runtime.InteropServices;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct InternalGamepadBindingInputHat
{
	public int Hat;
	public int HatMask;
}