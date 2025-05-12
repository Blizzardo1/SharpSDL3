using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuVertexBufferDescription
{
	public uint Slot;
	public uint Pitch;
	public GpuVertexInputRate InputRate;
	public uint InstanceStepRate;
}