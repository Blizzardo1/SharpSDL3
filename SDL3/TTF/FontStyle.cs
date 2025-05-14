namespace SharpSDL3.TTF;

/**
 * Font style flags for TTF_Font
 *
 * These are the flags which can be used to set the style of a font in
 * SDL_ttf. A combination of these flags can be used with functions that set
 * or query font style, such as TTF_SetFontStyle or TTF_GetFontStyle.
 *
 * \since This datatype is available since SDL_ttf 3.0.0.
 *
 * \sa TTF_SetFontStyle
 * \sa TTF_GetFontStyle
 */
public enum FontStyle {

    Normal = 0x00, /**< No special style */
    Bold = 0x01, /**< Bold style */
    Italic = 0x02, /**< Italic style */
    Underline = 0x04, /**< Underlined text */
    Strikethrough = 0x08 /**< Strikethrough text */
}