using System;

namespace SharpSDL3.Enums;

[Flags]
public enum MouseButtonFlags : uint {
    LMask = 0x1,
    MMask = 0x2,
    RMask = 0x4,
    X1Mask = 0x08,
    X2Mask = 0x10
}