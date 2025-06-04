using System;

namespace SharpSDL3.Enums;

[Flags]
public enum MessageBoxDefaultButton : uint {
    ReturnKeyDefault = 0x1,
    EscapeKeyDefault = 0x2
}