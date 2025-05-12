using System;

namespace SDL3.Enums;

[Flags]
public enum GpuColorComponentFlags : byte
{
	R = 0x1,
	G = 0x2,
	B = 0x4,
	A = 0x08
}