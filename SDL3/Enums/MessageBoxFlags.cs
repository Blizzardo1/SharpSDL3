using System;

namespace SharpSDL3.Enums;

[Flags]
public enum MessageBoxFlags : uint {
    Error = 0x10,
    Warning = 0x20,
    Information = 0x40,
    ButtonsLeftToRight = 0x080,
    ButtonsRightToLeft = 0x100
}