using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuColorTargetDescription {
    public GpuTextureFormat Format;
    public GpuColorTargetBlendState BlendState;
}