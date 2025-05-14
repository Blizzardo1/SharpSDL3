namespace SharpSDL3.TTF;

/**
 * Hinting flags for TTF (TrueType Fonts)
 *
 * This enum specifies the level of hinting to be applied to the font
 * rendering. The hinting level determines how much the font's outlines are
 * adjusted for better alignment on the pixel grid.
 *
 * \since This enum is available since SDL_ttf 3.0.0.
 *
 * \sa TTF_SetFontHinting
 * \sa TTF_GetFontHinting
 */
public enum Hinting {
    Invalid = -1,
    Normal,         /**< Normal hinting applies standard grid-fitting. */
    Light,          /**< Light hinting applies subtle adjustments to improve rendering. */
    Mono,           /**< Monochrome hinting adjusts the font for better rendering at lower resolutions. */
    None,           /**< No hinting, the font is rendered without any grid-fitting. */
    LightSubpixel   /**< Light hinting with subpixel rendering for more precise font edges. */
}