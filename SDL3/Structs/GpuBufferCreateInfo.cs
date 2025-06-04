using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
<<<<<<< HEAD
public struct GpuBufferCreateInfo {
    public GpuBufferUsageFlags Usage;
    public uint Size;
    public uint Props;
=======
public struct GpuBufferCreateInfo
{
	public GpuBufferUsageFlags Usage;
	public uint Size;
	public uint Props;
>>>>>>> main
}