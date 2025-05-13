using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

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