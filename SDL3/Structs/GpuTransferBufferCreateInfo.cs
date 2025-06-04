<<<<<<< HEAD
using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuTransferBufferCreateInfo {
    public GpuTransferBufferUsage Usage;
    public uint Size;
    public uint Props;
=======
using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuTransferBufferCreateInfo
{
	public GpuTransferBufferUsage Usage;
	public uint Size;
	public uint Props;
>>>>>>> main
}