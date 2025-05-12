using System;

namespace SDL3.Enums;

[Flags]
public enum PenInputFlags : uint
{
	Down = 0x1,
	Button1 = 0x2,
	Button2 = 0x4,
	Button3 = 0x08,
	Button4 = 0x10,
	Button5 = 0x20,
	EraserTip = 0x40000000
}