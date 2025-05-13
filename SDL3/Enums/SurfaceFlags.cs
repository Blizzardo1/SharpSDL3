using System;

namespace SharpSDL3.Enums;

[Flags]
public enum SurfaceFlags : uint
{
	Preallocated = 0x1,
	LockNeeded = 0x2,
	Locked = 0x4,
	SimdAligned = 0x08
}