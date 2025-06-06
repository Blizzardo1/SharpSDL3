using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct GpuComputePipelineCreateInfo {
    public nuint CodeSize;
    public nint Code;
    public nint EntryPoint;
    public GpuShaderFormat Format;
    public uint NumSamplers;
    public uint NumReadOnlyStorageTextures;
    public uint NumReadOnlyStorageBuffers;
    public uint NumReadWriteStorageTextures;
    public uint NumReadWriteStorageBuffers;
    public uint NumUniformBuffers;
    public uint ThreadCountX;
    public uint ThreadCountY;
    public uint ThreadCountZ;
    public uint Props;
}