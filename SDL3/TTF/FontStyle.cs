using System;

namespace SharpSDL3.TTF;

[Flags]
public enum FontStyle {
    Normal = 0x00,          // No special style
    Bold = 0x01,            // Bold style
    Italic = 0x02,          // Italic style
    Underline = 0x04,       // Underlined text
    Strikethrough = 0x08    // Strikethrough text
}