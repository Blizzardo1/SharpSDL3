using System;

namespace SharpSDL3.Enums;

[Flags]
public enum InitFlags : uint
{
	Timer = 0x1,
	Audio = 0x10,
	Video = 0x20,
	Joystick = 0x200,
	Haptic = 0x1000,
	Gamepad = 0x2000,
	Events = 0x4000,
	Sensor = 0x08000,
	Camera = 0x10000
}