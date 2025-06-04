using System;

namespace SharpSDL3.Enums;

[Flags]
public enum GpuTextureUsageFlags : uint
{
	Sampler = 0x1,
	ColorTarget = 0x2,
	DepthStencilTarget = 0x4,
	GraphicsStorageRead = 0x08,
	ComputeStorageRead = 0x10,
	ComputeStorageWrite = 0x20
}