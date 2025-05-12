using System;

namespace SDL3.Enums;

[Flags]
public enum MessageBoxButtonFlags : uint
{
	ReturnKeyDefault = 0x1,
	EscapeKeyDefault = 0x2
}