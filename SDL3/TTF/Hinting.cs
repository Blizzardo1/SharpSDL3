using System;

namespace SharpSDL3.TTF;

[Flags]
public enum Hinting {
    Invalid = -1,
    Normal = 0,         /** Normal hinting applies standard grid-fitting. */
    Light = 1,          /** Light hinting applies subtle adjustments to improve rendering. */
    Mono = 2,           /** Monochrome hinting adjusts the font for better rendering at lower resolutions. */
    None = 4,           /** No hinting, the font is rendered without any grid-fitting. */
    LightSubpixel = 8  /** Light hinting with subpixel rendering for more precise font edges. */
}