using System;

namespace SharpSDL3.Enums;

[Flags]
public enum WindowFlags : ulong {
    Fullscreen = 0x1,
    Opengl = 0x2,
    Occluded = 0x4,
    Hidden = 0x08,
    Borderless = 0x10,
    Resizable = 0x20,
    Minimized = 0x40,
    Maximized = 0x080,
    MouseGrabbed = 0x100,
    InputFocus = 0x200,
    MouseFocus = 0x400,
    External = 0x0800,
    Modal = 0x1000,
    HighPixelDensity = 0x2000,
    MouseCapture = 0x4000,
    MouseRelativeMode = 0x08000,
    AlwaysOnTop = 0x10000,
    Utility = 0x20000,
    Tooltip = 0x40000,
    PopupMenu = 0x080000,
    KeyboardGrabbed = 0x100000,
    Vulkan = 0x10000000,
    Metal = 0x20000000,
    Transparent = 0x40000000,
    NotFocusable = 0x080000000
}