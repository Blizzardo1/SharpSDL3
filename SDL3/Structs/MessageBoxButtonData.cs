<<<<<<< HEAD
using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct MessageBoxButtonData {
    public MessageBoxDefaultButton Flags;
    public int ButtonID;
    public nint Text;
=======
using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct MessageBoxButtonData
{
	public MessageBoxDefaultButton Flags;
	public int ButtonID;
	public nint Text;
>>>>>>> main
}