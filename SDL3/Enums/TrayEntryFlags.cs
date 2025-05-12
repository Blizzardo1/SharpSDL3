using System;

namespace SDL3.Enums;

[Flags]
public enum TrayEntryFlags : uint
{
	Button = 0x00000001u,
	Checkbox = 0x00000002u,
	Submenu = 0x00000004u,
	Disabled = 0x80000000u,
	Checked = 0x40000000u
}