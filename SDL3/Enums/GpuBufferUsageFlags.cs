using System;

namespace SharpSDL3.Enums;

[Flags]
public enum GpuBufferUsageFlags : uint
{
	Vertex = 0x1,
	Index = 0x2,
	Indirect = 0x4,
	GraphicsStorageRead = 0x08,
	ComputeStorageRead = 0x10,
	ComputeStorageWrite = 0x20
}