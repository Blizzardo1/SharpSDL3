<<<<<<< HEAD
using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct MessageBoxData {
    public MessageBoxFlags Flags;
    public nint Window;
    public nint Title;
    public nint Message;
    public int NumButtons;
    public nint Buttons;
    public nint ColorScheme;
=======
using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct MessageBoxData
{
	public MessageBoxFlags Flags;
	public nint Window;
	public nint Title;
	public nint Message;
	public int NumButtons;
	public nint Buttons;
	public nint ColorScheme;
>>>>>>> main
}