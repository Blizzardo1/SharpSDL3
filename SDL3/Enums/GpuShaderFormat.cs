using System;

namespace SharpSDL3.Enums;

[Flags]
public enum GpuShaderFormat : uint {
    Private = 0x1,
    Spirv = 0x2,
    Dxbc = 0x4,
    Dxil = 0x08,
    Msl = 0x10,
    MetalLib = 0x20
}