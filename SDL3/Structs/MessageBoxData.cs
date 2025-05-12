using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct MessageBoxData
{
	public MessageBoxFlags Flags;
	public nint Window;
	public byte* Title;
	public byte* Message;
	public int NumButtons;
	public MessageBoxButtonData* Buttons;
	public MessageBoxColorScheme* ColorScheme;
}