using System;

namespace SharpSDL3.TTF;

[Flags]
public enum SubStringFlags : int {
    DirectionMask = 0x000000FF,    // The mask for the flow direction for this substring
    TextStart = 0x00000100,    // This substring contains the beginning of the text
    LineStart = 0x00000200,    // This substring contains the beginning of line `line_index`
    LineEnd = 0x00000400,    // This substring contains the end of line `line_index`
    TextEnd = 0x00000800     // This substring contains the end of the text
}