using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuVertexBufferDescription {
    public uint Slot;
    public uint Pitch;
    public GpuVertexInputRate InputRate;
    public uint InstanceStepRate;
}