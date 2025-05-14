using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SharpSDL3.TTF;

public static unsafe partial class Ttf {

    /**
     * Printable format: "%d.%d.%d", MAJOR, MINOR, MICRO
     */
    public const int Major = 3;
    public const int Minor = 3;
    public const int Micro = 0;
    public const string NativeLibName = "SDL3_ttf";

    /// <summary>
    /// This is the version number macro for the current SDL_ttf version.
    /// </summary>
    /// <returns></returns>
    public static int Version() => Sdl.VersionNum(Major, Minor, Micro);

    /// <summary>
    /// Checks if the compiled SDL3_ttf version is at least the specified version.
    /// </summary>
    /// <param name="major">The major version to check.</param>
    /// <param name="minor">The minor version to check.</param>
    /// <param name="micro">The micro version to check.</param>
    /// <returns>True if the compiled version is at least the specified version; otherwise, false.</returns>
    public static bool MixerVersionAtLeast(int major, int minor, int micro) =>
        (Major >= major) &&
        (Major > major || Minor >= minor) &&
        (Major > major || Minor > minor || Micro >= micro);

    /**
     * This function gets the version of the dynamically linked SDL_ttf library.
     *
     * \returns SDL_ttf version.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_Version();

    public static int TtfVersion() => TTF_Version();

    /**
     * Query the version of the FreeType library in use.
     *
     * TTF_Init() should be called before calling this function.
     *
     * \param major to be filled in with the major version number. Can be NULL.
     * \param minor to be filled in with the minor version number. Can be NULL.
     * \param patch to be filled in with the param version number. Can be NULL.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_Init
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_GetFreeTypeVersion(int* major, int* minor, int* patch);

    public static void GetFreeTypeVersion(out int major, out int minor, out int patch) {
        fixed (int* pMajor = &major)
        fixed (int* pMinor = &minor)
        fixed (int* pPatch = &patch) {
            TTF_GetFreeTypeVersion(pMajor, pMinor, pPatch);
        }
    }

    /**
     * Query the version of the HarfBuzz library in use.
     *
     * If HarfBuzz is not available, the version reported is 0.0.0.
     *
     * \param major to be filled in with the major version number. Can be NULL.
     * \param minor to be filled in with the minor version number. Can be NULL.
     * \param patch to be filled in with the param version number. Can be NULL.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_GetHarfBuzzVersion(int* major, int* minor, int* patch);

    public static void GetHarfBuzzVersion(out int major, out int minor, out int patch) {
        fixed (int* pMajor = &major)
        fixed (int* pMinor = &minor)
        fixed (int* pPatch = &patch) {
            TTF_GetHarfBuzzVersion(pMajor, pMinor, pPatch);
        }
    }

    /**
     * Initialize SDL_ttf.
     *
     * You must successfully call this function before it is safe to call any
     * other function in this library.
     *
     * It is safe to call this more than once, and each successful TTF_Init() call
     * should be paired with a matching TTF_Quit() call.
     *
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_Quit
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_Init();

    public static bool Init() => TTF_Init();

    /**
     * Create a font from a file, using a specified point size.
     *
     * Some .fon fonts will have several sizes embedded in the file, so the point
     * size becomes the index of choosing which size. If the value is too high,
     * the last indexed size will be the default.
     *
     * When done with the returned TTF_Font, use TTF_CloseFont() to dispose of it.
     *
     * \param file path to font file.
     * \param ptsize point size to use for the newly-opened font.
     * \returns a valid TTF_Font, or NULL on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_CloseFont
     */
    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Font* TTF_OpenFont(string file, float ptsize);

    public static Font* OpenFont(string file, float ptsize) {
        if (string.IsNullOrEmpty(file)) {
            throw new ArgumentException("Font file path cannot be null or empty.", nameof(file));
        }

        Font* fontPtr = TTF_OpenFont(file, ptsize);
        if (fontPtr == null) {
            throw new InvalidOperationException($"Failed to open font. SDL Error: {Sdl.GetError()}");
        }

        return fontPtr;
    }

    /**
     * Create a font from an SDL_IOStream, using a specified point size.
     *
     * Some .fon fonts will have several sizes embedded in the file, so the point
     * size becomes the index of choosing which size. If the value is too high,
     * the last indexed size will be the default.
     *
     * If `closeio` is true, `src` will be automatically closed once the font is
     * closed. Otherwise you should keep `src` open until the font is closed.
     *
     * When done with the returned TTF_Font, use TTF_CloseFont() to dispose of it.
     *
     * \param src an SDL_IOStream to provide a font file's data.
     * \param closeio true to close `src` when the font is closed, false to leave
     *                it open.
     * \param ptsize point size to use for the newly-opened font.
     * \returns a valid TTF_Font, or NULL on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_CloseFont
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Font* TTF_OpenFontIO(IOStream* src, [MarshalAs(UnmanagedType.Bool)] bool closeio, float ptsize);

    public static Font* OpenFontIO(IOStream* src, bool closeio, float ptsize) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "SDL_IOStream cannot be null.");
        }
        Font* fontPtr = TTF_OpenFontIO(src, closeio, ptsize);
        if (fontPtr == null) {
            throw new InvalidOperationException($"Failed to open font. SDL Error: {Sdl.GetError()}");
        }
        return fontPtr;
    }

    /**
     * Create a font with the specified properties.
     *
     * These are the supported properties:
     *
     * - `TTF_PROP_FONT_CREATE_FILENAME_STRING`: the font file to open, if an
     *   SDL_IOStream isn't being used. This is required if
     *   `TTF_PROP_FONT_CREATE_IOSTREAM_POINTER` and
     *   `TTF_PROP_FONT_CREATE_EXISTING_FONT` aren't set.
     * - `TTF_PROP_FONT_CREATE_IOSTREAM_POINTER`: an SDL_IOStream containing the
     *   font to be opened. This should not be closed until the font is closed.
     *   This is required if `TTF_PROP_FONT_CREATE_FILENAME_STRING` and
     *   `TTF_PROP_FONT_CREATE_EXISTING_FONT` aren't set.
     * - `TTF_PROP_FONT_CREATE_IOSTREAM_OFFSET_NUMBER`: the offset in the iostream
     *   for the beginning of the font, defaults to 0.
     * - `TTF_PROP_FONT_CREATE_IOSTREAM_AUTOCLOSE_BOOLEAN`: true if closing the
     *   font should also close the associated SDL_IOStream.
     * - `TTF_PROP_FONT_CREATE_SIZE_FLOAT`: the point size of the font. Some .fon
     *   fonts will have several sizes embedded in the file, so the point size
     *   becomes the index of choosing which size. If the value is too high, the
     *   last indexed size will be the default.
     * - `TTF_PROP_FONT_CREATE_FACE_NUMBER`: the face index of the font, if the
     *   font contains multiple font faces.
     * - `TTF_PROP_FONT_CREATE_HORIZONTAL_DPI_NUMBER`: the horizontal DPI to use
     *   for font rendering, defaults to
     *   `TTF_PROP_FONT_CREATE_VERTICAL_DPI_NUMBER` if set, or 72 otherwise.
     * - `TTF_PROP_FONT_CREATE_VERTICAL_DPI_NUMBER`: the vertical DPI to use for
     *   font rendering, defaults to `TTF_PROP_FONT_CREATE_HORIZONTAL_DPI_NUMBER`
     *   if set, or 72 otherwise.
     * - `TTF_PROP_FONT_CREATE_EXISTING_FONT`: an optional TTF_Font that, if set,
     *   will be used as the font data source and the initial size and style of
     *   the new font.
     *
     * \param props the properties to use.
     * \returns a valid TTF_Font, or NULL on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_CloseFont
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Font* TTF_OpenFontWithProperties(int props);
    public static Font* OpenFontWithProperties(int props) {
        Font* fontPtr = TTF_OpenFontWithProperties(props);
        if (fontPtr == null) {
            throw new InvalidOperationException($"Failed to open font. SDL Error: {Sdl.GetError()}");
        }
        return fontPtr;
    }

    public const string TTF_PROP_FONT_CREATE_FILENAME_STRING = "SDL_ttf.font.create.filename";
    public const string TTF_PROP_FONT_CREATE_IOSTREAM_POINTER = "SDL_ttf.font.create.iostream";
    public const string TTF_PROP_FONT_CREATE_IOSTREAM_OFFSET_NUMBER = "SDL_ttf.font.create.iostream.offset";
    public const string TTF_PROP_FONT_CREATE_IOSTREAM_AUTOCLOSE_BOOLEAN = "SDL_ttf.font.create.iostream.autoclose";
    public const string TTF_PROP_FONT_CREATE_SIZE_FLOAT = "SDL_ttf.font.create.size";
    public const string TTF_PROP_FONT_CREATE_FACE_NUMBER = "SDL_ttf.font.create.face";
    public const string TTF_PROP_FONT_CREATE_HORIZONTAL_DPI_NUMBER = "SDL_ttf.font.create.hdpi";
    public const string TTF_PROP_FONT_CREATE_VERTICAL_DPI_NUMBER = "SDL_ttf.font.create.vdpi";
    public const string TTF_PROP_FONT_CREATE_EXISTING_FONT = "SDL_ttf.font.create.existing_font";

    /**
     * Create a copy of an existing font.
     *
     * The copy will be distinct from the original, but will share the font file
     * and have the same size and style as the original.
     *
     * When done with the returned TTF_Font, use TTF_CloseFont() to dispose of it.
     *
     * \param existing_font the font to copy.
     * \returns a valid TTF_Font, or NULL on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               original font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_CloseFont
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Font* TTF_CopyFont(Font* existing_font);

    public static Font* CopyFont(Font* existingFont) {
        if (existingFont == null) {
            throw new ArgumentNullException(nameof(existingFont), "Existing font cannot be null.");
        }
        Font* fontPtr = TTF_CopyFont(existingFont);
        if (fontPtr == null) {
            throw new InvalidOperationException($"Failed to copy font. SDL Error: {Sdl.GetError()}");
        }
        return fontPtr;
    }

    /**
     * Get the properties associated with a font.
     *
     * The following read-write properties are provided by SDL:
     *
     * - `TTF_PROP_FONT_OUTLINE_LINE_CAP_NUMBER`: The FT_Stroker_LineCap value
     *   used when setting the font outline, defaults to
     *   `FT_STROKER_LINECAP_ROUND`.
     * - `TTF_PROP_FONT_OUTLINE_LINE_JOIN_NUMBER`: The FT_Stroker_LineJoin value
     *   used when setting the font outline, defaults to
     *   `FT_STROKER_LINEJOIN_ROUND`.
     * - `TTF_PROP_FONT_OUTLINE_MITER_LIMIT_NUMBER`: The FT_Fixed miter limit used
     *   when setting the font outline, defaults to 0.
     *
     * \param font the font to query.
     * \returns a valid property ID on success or 0 on failure; call
     *          SDL_GetError() for more information.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * 
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetFontProperties(Font* font);

    public static int GetFontProperties(Font* font) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        int props = TTF_GetFontProperties(font);
        if (props == 0) {
            throw new InvalidOperationException($"Failed to get font properties. SDL Error: {Sdl.GetError()}");
        }
        return props;
    }

    public const string TTF_PROP_FONT_OUTLINE_LINE_CAP_NUMBER = "SDL_ttf.font.outline.line_cap";
    public const string TTF_PROP_FONT_OUTLINE_LINE_JOIN_NUMBER = "SDL_ttf.font.outline.line_join";
    public const string TTF_PROP_FONT_OUTLINE_MITER_LIMIT_NUMBER = "SDL_ttf.font.outline.miter_limit";

    /**
     * Get the font generation.
     *
     * The generation is incremented each time font properties change that require
     * rebuilding glyphs, such as style, size, etc.
     *
     * \param font the font to query.
     * \returns the font generation or 0 on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetFontGeneration(Font* font);
    public static int GetFontGeneration(Font* font) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        int generation = TTF_GetFontGeneration(font);
        if (generation == 0) {
            throw new InvalidOperationException($"Failed to get font generation. SDL Error: {Sdl.GetError()}");
        }
        return generation;
    }

    /**
     * Add a fallback font.
     *
     * Add a font that will be used for glyphs that are not in the current font.
     * The fallback font should have the same size and style as the current font.
     *
     * If there are multiple fallback fonts, they are used in the order added.
     *
     * This updates any TTF_Text objects using this font.
     *
     * \param font the font to modify.
     * \param fallback the font to add as a fallback.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created
     *               both fonts.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_ClearFallbackFonts
     * \sa TTF_RemoveFallbackFont
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_AddFallbackFont(Font* font, Font* fallback);

    public static bool AddFallbackFont(Font* font, Font* fallback) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        if (fallback == null) {
            throw new ArgumentNullException(nameof(fallback), "Fallback font cannot be null.");
        }
        return TTF_AddFallbackFont(font, fallback);
    }

    /**
     * Remove a fallback font.
     *
     * This updates any TTF_Text objects using this font.
     *
     * \param font the font to modify.
     * \param fallback the font to remove as a fallback.
     *
     * \threadsafety This function should be called on the thread that created
     *               both fonts.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_AddFallbackFont
     * \sa TTF_ClearFallbackFonts
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_RemoveFallbackFont(Font* font, Font* fallback);

    public static void RemoveFallbackFont(Font* font, Font* fallback) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        if (fallback == null) {
            throw new ArgumentNullException(nameof(fallback), "Fallback font cannot be null.");
        }
        TTF_RemoveFallbackFont(font, fallback);
    }

    /**
     * Remove all fallback fonts.
     *
     * This updates any TTF_Text objects using this font.
     *
     * \param font the font to modify.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_AddFallbackFont
     * \sa TTF_RemoveFallbackFont
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_ClearFallbackFonts(Font* font);

    public static void ClearFallbackFonts(Font* font) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        TTF_ClearFallbackFonts(font);
    }

    /**
     * Set a font's size dynamically.
     *
     * This updates any TTF_Text objects using this font, and clears
     * already-generated glyphs, if any, from the cache.
     *
     * \param font the font to resize.
     * \param ptsize the new point size.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_GetFontSize
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_SetFontSize(Font* font, float ptsize);

    public static bool SetFontSize(Font* font, float ptsize) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_SetFontSize(font, ptsize);
    }

    /**
     * Set font size dynamically with target resolutions, in dots per inch.
     *
     * This updates any TTF_Text objects using this font, and clears
     * already-generated glyphs, if any, from the cache.
     *
     * \param font the font to resize.
     * \param ptsize the new point size.
     * \param hdpi the target horizontal DPI.
     * \param vdpi the target vertical DPI.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_GetFontSize
     * \sa TTF_GetFontSizeDPI
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_SetFontSizeDPI(Font* font, float ptsize, int hdpi, int vdpi);

    public static bool SetFontSizeDPI(Font* font, float ptsize, int hdpi, int vdpi) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_SetFontSizeDPI(font, ptsize, hdpi, vdpi);
    }

    /**
     * Get the size of a font.
     *
     * \param font the font to query.
     * \returns the size of the font, or 0.0f on failure; call SDL_GetError() for
     *          more information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_SetFontSize
     * \sa TTF_SetFontSizeDPI
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float TTF_GetFontSize(Font* font);

    public static float GetFontSize(Font* font) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        float size = TTF_GetFontSize(font);
        if (size == 0.0f) {
            throw new InvalidOperationException($"Failed to get font size. SDL Error: {Sdl.GetError()}");
        }
        return size;
    }

    /**
     * Get font target resolutions, in dots per inch.
     *
     * \param font the font to query.
     * \param hdpi a pointer filled in with the target horizontal DPI.
     * \param vdpi a pointer filled in with the target vertical DPI.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_SetFontSizeDPI
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_GetFontDPI(Font* font, int* hdpi, int* vdpi);
    public static bool GetFontDPI(Font* font, out int hdpi, out int vdpi) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        fixed (int* pHdpi = &hdpi)
        fixed (int* pVdpi = &vdpi) {
            return TTF_GetFontDPI(font, pHdpi, pVdpi);
        }
    }

    /**
     * Set a font's current style.
     *
     * This updates any TTF_Text objects using this font, and clears
     * already-generated glyphs, if any, from the cache.
     *
     * The font styles are a set of bit flags, OR'd together:
     *
     * - `TTF_STYLE_NORMAL` (is zero)
     * - `TTF_STYLE_BOLD`
     * - `TTF_STYLE_ITALIC`
     * - `TTF_STYLE_UNDERLINE`
     * - `TTF_STYLE_STRIKETHROUGH`
     *
     * \param font the font to set a new style on.
     * \param style the new style values to set, OR'd together.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_GetFontStyle
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_SetFontStyle(Font* font, FontStyle style);

    public static void SetFontStyle(Font* font, FontStyle style) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        TTF_SetFontStyle(font, style);
    }

    /**
     * Query a font's current style.
     *
     * The font styles are a set of bit flags, OR'd together:
     *
     * - `TTF_STYLE_NORMAL` (is zero)
     * - `TTF_STYLE_BOLD`
     * - `TTF_STYLE_ITALIC`
     * - `TTF_STYLE_UNDERLINE`
     * - `TTF_STYLE_STRIKETHROUGH`
     *
     * \param font the font to query.
     * \returns the current font style, as a set of bit flags.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_SetFontStyle
     */

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial FontStyle TTF_GetFontStyle(Font* font);

    public static FontStyle GetFontStyle(Font* font) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_GetFontStyle(font);
    }

    /**
     * Set a font's current outline.
     *
     * This uses the font properties `TTF_PROP_FONT_OUTLINE_LINE_CAP_NUMBER`,
     * `TTF_PROP_FONT_OUTLINE_LINE_JOIN_NUMBER`, and
     * `TTF_PROP_FONT_OUTLINE_MITER_LIMIT_NUMBER` when setting the font outline.
     *
     * This updates any TTF_Text objects using this font, and clears
     * already-generated glyphs, if any, from the cache.
     *
     * \param font the font to set a new outline on.
     * \param outline positive outline value, 0 to default.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_GetFontOutline
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_SetFontOutline(Font* font, int outline);
    public static bool SetFontOutline(Font* font, int outline) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_SetFontOutline(font, outline);
    }

    /**
     * Query a font's current outline.
     *
     * \param font the font to query.
     * \returns the font's current outline value.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_SetFontOutline
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetFontOutline(Font* font);

    public static int GetFontOutline(Font* font) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_GetFontOutline(font);
    }

    /**
     * Set a font's current hinter setting.
     *
     * This updates any TTF_Text objects using this font, and clears
     * already-generated glyphs, if any, from the cache.
     *
     * The hinter setting is a single value:
     *
     * - `TTF_HINTING_NORMAL`
     * - `TTF_HINTING_LIGHT`
     * - `TTF_HINTING_MONO`
     * - `TTF_HINTING_NONE`
     * - `TTF_HINTING_LIGHT_SUBPIXEL` (available in SDL_ttf 3.0.0 and later)
     *
     * \param font the font to set a new hinter setting on.
     * \param hinting the new hinter setting.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_GetFontHinting
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_SetFontHinting(Font* font, Hinting hinting);

    public static void SetFontHinting(Font* font, Hinting hinting) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        TTF_SetFontHinting(font, hinting);
    }

    /**
     * Query the number of faces of a font.
     *
     * \param font the font to query.
     * \returns the number of FreeType font faces.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetNumFontFaces(Font* font);

    public static int GetNumFontFaces(Font* font) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_GetNumFontFaces(font);
    }

    /**
     * Query a font's current FreeType hinter setting.
     *
     * The hinter setting is a single value:
     *
     * - `TTF_HINTING_NORMAL`
     * - `TTF_HINTING_LIGHT`
     * - `TTF_HINTING_MONO`
     * - `TTF_HINTING_NONE`
     * - `TTF_HINTING_LIGHT_SUBPIXEL` (available in SDL_ttf 3.0.0 and later)
     *
     * \param font the font to query.
     * \returns the font's current hinter value, or TTF_HINTING_INVALID if the
     *          font is invalid.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_SetFontHinting
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Hinting TTF_GetFontHinting(Font* font);

    public static Hinting GetFontHinting(Font* font) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_GetFontHinting(font);
    }

    /**
     * Enable Signed Distance Field rendering for a font.
     *
     * SDF is a technique that helps fonts look sharp even when scaling and
     * rotating, and requires special shader support for display.
     *
     * This works with Blended APIs, and generates the raw signed distance values
     * in the alpha channel of the resulting texture.
     *
     * This updates any TTF_Text objects using this font, and clears
     * already-generated glyphs, if any, from the cache.
     *
     * \param font the font to set SDF support on.
     * \param enabled true to enable SDF, false to disable.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_GetFontSDF
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_SetFontSDF(Font* font, [MarshalAs(UnmanagedType.Bool)] bool enabled);

    public static bool SetFontSDF(Font* font, bool enabled) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_SetFontSDF(font, enabled);
    }

    /**
     * Query whether Signed Distance Field rendering is enabled for a font.
     *
     * \param font the font to query.
     * \returns true if enabled, false otherwise.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_SetFontSDF
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_GetFontSDF(Font* font);

    public static bool GetFontSDF(Font* font) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_GetFontSDF(font);
    }

    /**
     * Query a font's weight, in terms of the lightness/heaviness of the strokes.
     *
     * \param font the font to query.
     * \returns the font's current weight.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.2.2.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial FontWeight TTF_GetFontWeight(Font* font);

    public static FontWeight GetFontWeight(Font* font) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_GetFontWeight(font);
    }

    /**
     * Set a font's current wrap alignment option.
     *
     * This updates any TTF_Text objects using this font.
     *
     * \param font the font to set a new wrap alignment option on.
     * \param align the new wrap alignment option.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_GetFontWrapAlignment
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_SetFontWrapAlignment(Font* font, HorizontalAlignment align);

    public static void SetFontWrapAlignment(Font* font, HorizontalAlignment align) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        TTF_SetFontWrapAlignment(font, align);
    }

    /**
     * Query a font's current wrap alignment option.
     *
     * \param font the font to query.
     * \returns the font's current wrap alignment option.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_SetFontWrapAlignment
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial HorizontalAlignment TTF_GetFontWrapAlignment(Font* font);

    public static HorizontalAlignment GetFontWrapAlignment(Font* font) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_GetFontWrapAlignment(font);
    }

    /**
     * Query the total height of a font.
     *
     * This is usually equal to point size.
     *
     * \param font the font to query.
     * \returns the font's height.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetFontHeight(Font* font);

    public static int GetFontHeight(Font* font) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_GetFontHeight(font);
    }

    /**
     * Query the offset from the baseline to the top of a font.
     *
     * This is a positive value, relative to the baseline.
     *
     * \param font the font to query.
     * \returns the font's ascent.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetFontAscent(Font* font);

    public static int GetFontAscent(Font* font) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_GetFontAscent(font);
    }

    /**
     * Query the offset from the baseline to the bottom of a font.
     *
     * This is a negative value, relative to the baseline.
     *
     * \param font the font to query.
     * \returns the font's descent.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetFontDescent(Font* font);

    public static int GetFontDescent(Font* font) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_GetFontDescent(font);
    }

    /**
     * Set the spacing between lines of text for a font.
     *
     * This updates any TTF_Text objects using this font.
     *
     * \param font the font to modify.
     * \param lineskip the new line spacing for the font.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_GetFontLineSkip
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_SetFontLineSkip(Font* font, int lineskip);

    public static void SetFontLineSkip(Font* font, int lineskip) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        TTF_SetFontLineSkip(font, lineskip);
    }

    /**
     * Query the spacing between lines of text for a font.
     *
     * \param font the font to query.
     * \returns the font's recommended spacing.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_SetFontLineSkip
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetFontLineSkip(Font* font);

    public static int GetFontLineSkip(Font* font) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_GetFontLineSkip(font);
    }

    /**
     * Set if kerning is enabled for a font.
     *
     * Newly-opened fonts default to allowing kerning. This is generally a good
     * policy unless you have a strong reason to disable it, as it tends to
     * produce better rendering (with kerning disabled, some fonts might render
     * the word `kerning` as something that looks like `keming` for example).
     *
     * This updates any TTF_Text objects using this font.
     *
     * \param font the font to set kerning on.
     * \param enabled true to enable kerning, false to disable.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_GetFontKerning
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_SetFontKerning(Font* font, [MarshalAs(UnmanagedType.Bool)] bool enabled);

    public static void SetFontKerning(Font* font, bool enabled) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        TTF_SetFontKerning(font, enabled);
    }

    /**
     * Query whether or not kerning is enabled for a font.
     *
     * \param font the font to query.
     * \returns true if kerning is enabled, false otherwise.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_SetFontKerning
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_GetFontKerning(Font* font);

    public static bool GetFontKerning(Font* font) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_GetFontKerning(font);
    }

    /**
     * Query whether a font is fixed-width.
     *
     * A "fixed-width" font means all glyphs are the same width across; a
     * lowercase 'i' will be the same size across as a capital 'W', for example.
     * This is common for terminals and text editors, and other apps that treat
     * text as a grid. Most other things (WYSIWYG word processors, web pages, etc)
     * are more likely to not be fixed-width in most cases.
     *
     * \param font the font to query.
     * \returns true if the font is fixed-width, false otherwise.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_FontIsFixedWidth(Font* font);

    public static bool IsFixedWidth(Font* font) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_FontIsFixedWidth(font);
    }

    /**
     * Query whether a font is scalable or not.
     *
     * Scalability lets us distinguish between outline and bitmap fonts.
     *
     * \param font the font to query.
     * \returns true if the font is scalable, false otherwise.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_SetFontSDF
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_FontIsScalable(Font* font);

    public static bool IsScalable(Font* font) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_FontIsScalable(font);
    }

    /**
     * Query a font's family name.
     *
     * This string is dictated by the contents of the font file.
     *
     * Note that the returned string is to internal storage, and should not be
     * modified or free'd by the caller. The string becomes invalid, with the rest
     * of the font, when `font` is handed to TTF_CloseFont().
     *
     * \param font the font to query.
     * \returns the font's family name.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string TTF_GetFontFamilyName(Font* font);

    public static string GetFontFamilyName(Font* font) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_GetFontFamilyName(font);
    }

    /**
     * Query a font's style name.
     *
     * This string is dictated by the contents of the font file.
     *
     * Note that the returned string is to internal storage, and should not be
     * modified or free'd by the caller. The string becomes invalid, with the rest
     * of the font, when `font` is handed to TTF_CloseFont().
     *
     * \param font the font to query.
     * \returns the font's style name.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string TTF_GetFontStyleName(Font* font);

    public static string GetFontStyleName(Font* font) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_GetFontStyleName(font);
    }

    /**
     * Set the direction to be used for text shaping by a font.
     *
     * This function only supports left-to-right text shaping if SDL_ttf was not
     * built with HarfBuzz support.
     *
     * This updates any TTF_Text objects using this font.
     *
     * \param font the font to modify.
     * \param direction the new direction for text to flow.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_SetFontDirection(Font* font, Direction direction);

    public static bool SetFontDirection(Font* font, Direction direction) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_SetFontDirection(font, direction);
    }

    /**
     * Get the direction to be used for text shaping by a font.
     *
     * This defaults to TTF_DIRECTION_INVALID if it hasn't been set.
     *
     * \param font the font to query.
     * \returns the direction to be used for text shaping.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Direction TTF_GetFontDirection(Font* font);

    public static Direction GetFontDirection(Font* font) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_GetFontDirection(font);
    }

    /**
     * Convert from a 4 character string to a 32-bit tag.
     *
     * \param string the 4 character string to convert.
     * \returns the 32-bit representation of the string.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_TagToString
     */
    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_StringToTag(string str);

    public static int StringToTag(string str) {
        if (str == null) {
            throw new ArgumentNullException(nameof(str), "String cannot be null.");
        }
        if (str.Length != 4) {
            throw new ArgumentException("String must be exactly 4 characters long.", nameof(str));
        }
        return TTF_StringToTag(str);
    }

    /**
     * Convert from a 32-bit tag to a 4 character string.
     *
     * \param tag the 32-bit tag to convert.
     * \param string a pointer filled in with the 4 character representation of
     *               the tag.
     * \param size the size of the buffer pointed at by string, should be at least
     *             4.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_TagToString
     */
    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_TagToString(int tag, string str, Size size);

    /// <summary>
    /// May not work properly
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static string TagToString(int tag, Size size) {
        Span<char> buffer = stackalloc char[5];
        TTF_TagToString(tag, new string(buffer), size);
        return new string(buffer);
    }

    /**
     * Set the script to be used for text shaping by a font.
     *
     * This returns false if SDL_ttf isn't built with HarfBuzz support.
     *
     * This updates any TTF_Text objects using this font.
     *
     * \param font the font to modify.
     * \param script an
     *               [ISO 15924 code](https://unicode.org/iso15924/iso15924-codes.html)
     *               .
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_StringToTag
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_SetFontScript(Font* font, int script);

    public static bool SetFontScript(Font* font, int script) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_SetFontScript(font, script);
    }

    /**
     * Get the script used for text shaping a font.
     *
     * \param font the font to query.
     * \returns an
     *          [ISO 15924 code](https://unicode.org/iso15924/iso15924-codes.html)
     *          or 0 if a script hasn't been set.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_TagToString
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetFontScript(Font* font);

    public static int GetFontScript(Font* font) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_GetFontScript(font);
    }

    /**
     * Get the script used by a 32-bit codepoint.
     *
     * \param ch the character code to check.
     * \returns an
     *          [ISO 15924 code](https://unicode.org/iso15924/iso15924-codes.html)
     *          on success, or 0 on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function is thread-safe.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_TagToString
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetGlyphScript(int ch);

    public static int GetGlyphScript(int ch) {
        return TTF_GetGlyphScript(ch);
    }

    /**
     * Set language to be used for text shaping by a font.
     *
     * If SDL_ttf was not built with HarfBuzz support, this function returns
     * false.
     *
     * This updates any TTF_Text objects using this font.
     *
     * \param font the font to specify a language for.
     * \param language_bcp47 a null-terminated string containing the desired
     *                       language's BCP47 code. Or null to reset the value.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_SetFontLanguage(Font* font, string language_bcp47);

    public static bool SetFontLanguage(Font* font, string language_bcp47) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_SetFontLanguage(font, language_bcp47);
    }

    /**
     * Check whether a glyph is provided by the font for a UNICODE codepoint.
     *
     * \param font the font to query.
     * \param ch the codepoint to check.
     * \returns true if font provides a glyph for this character, false if not.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_FontHasGlyph(Font* font, int ch);

    public static bool FontHasGlyph(Font* font, int ch) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_FontHasGlyph(font, ch);
    }

    /**
     * Get the pixel image for a UNICODE codepoint.
     *
     * \param font the font to query.
     * \param ch the codepoint to check.
     * \param image_type a pointer filled in with the glyph image type, may be
     *                   NULL.
     * \returns an Surface containing the glyph, or NULL on failure; call
     *          SDL_GetError() for more information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* TTF_GetGlyphImage(Font* font, int ch, ImageType* image_type);

    public static Surface* GetGlyphImage(Font* font, int ch, ImageType* image_type) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_GetGlyphImage(font, ch, image_type);
    }

    /**
     * Get the pixel image for a character index.
     *
     * This is useful for text engine implementations, which can call this with
     * the `glyph_index` in a TTF_CopyOperation
     *
     * \param font the font to query.
     * \param glyph_index the index of the glyph to return.
     * \param image_type a pointer filled in with the glyph image type, may be
     *                   NULL.
     * \returns an Surface containing the glyph, or NULL on failure; call
     *          SDL_GetError() for more information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* TTF_GetGlyphImageForIndex(Font* font, int glyph_index, ImageType* image_type);

    public static Surface* GetGlyphImageForIndex(Font* font, int glyph_index, ImageType* image_type) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_GetGlyphImageForIndex(font, glyph_index, image_type);
    }

    /**
     * Query the metrics (dimensions) of a font's glyph for a UNICODE codepoint.
     *
     * To understand what these metrics mean, here is a useful link:
     *
     * https://freetype.sourceforge.net/freetype2/docs/tutorial/step2.html
     *
     * \param font the font to query.
     * \param ch the codepoint to check.
     * \param minx a pointer filled in with the minimum x coordinate of the glyph
     *             from the left edge of its bounding box. This value may be
     *             negative.
     * \param maxx a pointer filled in with the maximum x coordinate of the glyph
     *             from the left edge of its bounding box.
     * \param miny a pointer filled in with the minimum y coordinate of the glyph
     *             from the bottom edge of its bounding box. This value may be
     *             negative.
     * \param maxy a pointer filled in with the maximum y coordinate of the glyph
     *             from the bottom edge of its bounding box.
     * \param advance a pointer filled in with the distance to the next glyph from
     *                the left edge of this glyph's bounding box.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_GetGlyphMetrics(Font* font, int ch, int* minx, int* maxx, int* miny, int* maxy, int* advance);

    public static bool GetGlyphMetrics(Font* font, int ch, int* minx, int* maxx, int* miny, int* maxy, int* advance) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_GetGlyphMetrics(font, ch, minx, maxx, miny, maxy, advance);
    }

    /**
     * Query the kerning size between the glyphs of two UNICODE codepoints.
     *
     * \param font the font to query.
     * \param previous_ch the previous codepoint.
     * \param ch the current codepoint.
     * \param kerning a pointer filled in with the kerning size between the two
     *                glyphs, in pixels, may be NULL.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_GetGlyphKerning(Font* font, int previous_ch, int ch, int* kerning);

    public static bool GetGlyphKerning(Font* font, int previous_ch, int ch, int* kerning) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_GetGlyphKerning(font, previous_ch, ch, kerning);
    }

    /**
     * Calculate the dimensions of a rendered string of UTF-8 text.
     *
     * This will report the width and height, in pixels, of the space that the
     * specified string will take to fully render.
     *
     * \param font the font to query.
     * \param text text to calculate, in UTF-8 encoding.
     * \param length the length of the text, in bytes, or 0 for null terminated
     *               text.
     * \param w will be filled with width, in pixels, on return.
     * \param h will be filled with height, in pixels, on return.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_GetStringSize(Font* font, string text, Size length, int* w, int* h);

    public static bool GetStringSize(Font* font, string text, Size length, int* w, int* h) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_GetStringSize(font, text, length, w, h);
    }

    /**
     * Calculate the dimensions of a rendered string of UTF-8 text.
     *
     * This will report the width and height, in pixels, of the space that the
     * specified string will take to fully render.
     *
     * Text is wrapped to multiple lines on line endings and on word boundaries if
     * it extends beyond `wrap_width` in pixels.
     *
     * If wrap_width is 0, this function will only wrap on newline characters.
     *
     * \param font the font to query.
     * \param text text to calculate, in UTF-8 encoding.
     * \param length the length of the text, in bytes, or 0 for null terminated
     *               text.
     * \param wrap_width the maximum width or 0 to wrap on newline characters.
     * \param w will be filled with width, in pixels, on return.
     * \param h will be filled with height, in pixels, on return.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_GetStringSizeWrapped(Font* font, string text, Size length, int wrap_width, int* w, int* h);

    public static bool GetStringSizeWrapped(Font* font, string text, Size length, int wrap_width, int* w, int* h) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_GetStringSizeWrapped(font, text, length, wrap_width, w, h);
    }

    /**
     * Calculate how much of a UTF-8 string will fit in a given width.
     *
     * This reports the number of characters that can be rendered before reaching
     * `max_width`.
     *
     * This does not need to render the string to do this calculation.
     *
     * \param font the font to query.
     * \param text text to calculate, in UTF-8 encoding.
     * \param length the length of the text, in bytes, or 0 for null terminated
     *               text.
     * \param max_width maximum width, in pixels, available for the string, or 0
     *                  for unbounded width.
     * \param measured_width a pointer filled in with the width, in pixels, of the
     *                       string that will fit, may be NULL.
     * \param measured_length a pointer filled in with the length, in bytes, of
     *                        the string that will fit, may be NULL.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_MeasureString(Font* font, string text, Size length, int max_width, int* measured_width, Size* measured_length);

    public static bool MeasureString(Font* font, string text, Size length, int max_width, int* measured_width, Size* measured_length) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_MeasureString(font, text, length, max_width, measured_width, measured_length);
    }

    /**
     * Render UTF-8 text at fast quality to a new 8-bit surface.
     *
     * This function will allocate a new 8-bit, palettized surface. The surface's
     * 0 pixel will be the colorkey, giving a transparent background. The 1 pixel
     * will be set to the text color.
     *
     * This will not word-wrap the string; you'll get a surface with a single line
     * of text, as long as the string requires. You can use
     * TTF_RenderText_Solid_Wrapped() instead if you need to wrap the output to
     * multiple lines.
     *
     * This will not wrap on newline characters.
     *
     * You can render at other quality levels with TTF_RenderText_Shaded,
     * TTF_RenderText_Blended, and TTF_RenderText_LCD.
     *
     * \param font the font to render with.
     * \param text text to render, in UTF-8 encoding.
     * \param length the length of the text, in bytes, or 0 for null terminated
     *               text.
     * \param fg the foreground color for the text.
     * \returns a new 8-bit, palettized surface, or NULL if there was an error.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_RenderText_Blended
     * \sa TTF_RenderText_LCD
     * \sa TTF_RenderText_Shaded
     * \sa TTF_RenderText_Solid
     * \sa TTF_RenderText_Solid_Wrapped
     */
    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* TTF_RenderText_Solid(Font* font, string text, Size length, Color fg);

    public static Surface* RenderTextSolid(Font* font, string text, Size length, Color fg) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_RenderText_Solid(font, text, length, fg);
    }

    /**
     * Render word-wrapped UTF-8 text at fast quality to a new 8-bit surface.
     *
     * This function will allocate a new 8-bit, palettized surface. The surface's
     * 0 pixel will be the colorkey, giving a transparent background. The 1 pixel
     * will be set to the text color.
     *
     * Text is wrapped to multiple lines on line endings and on word boundaries if
     * it extends beyond `wrapLength` in pixels.
     *
     * If wrapLength is 0, this function will only wrap on newline characters.
     *
     * You can render at other quality levels with TTF_RenderText_Shaded_Wrapped,
     * TTF_RenderText_Blended_Wrapped, and TTF_RenderText_LCD_Wrapped.
     *
     * \param font the font to render with.
     * \param text text to render, in UTF-8 encoding.
     * \param length the length of the text, in bytes, or 0 for null terminated
     *               text.
     * \param fg the foreground color for the text.
     * \param wrapLength the maximum width of the text surface or 0 to wrap on
     *                   newline characters.
     * \returns a new 8-bit, palettized surface, or NULL if there was an error.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_RenderText_Blended_Wrapped
     * \sa TTF_RenderText_LCD_Wrapped
     * \sa TTF_RenderText_Shaded_Wrapped
     * \sa TTF_RenderText_Solid
     */
    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* TTF_RenderText_Solid_Wrapped(Font* font, string text, Size length, Color fg, int wrapLength);

    public static Surface* RenderTextSolidWrapped(Font* font, string text, Size length, Color fg, int wrapLength) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_RenderText_Solid_Wrapped(font, text, length, fg, wrapLength);
    }

    /**
     * Render a single 32-bit glyph at fast quality to a new 8-bit surface.
     *
     * This function will allocate a new 8-bit, palettized surface. The surface's
     * 0 pixel will be the colorkey, giving a transparent background. The 1 pixel
     * will be set to the text color.
     *
     * The glyph is rendered without any padding or centering in the X direction,
     * and aligned normally in the Y direction.
     *
     * You can render at other quality levels with TTF_RenderGlyph_Shaded,
     * TTF_RenderGlyph_Blended, and TTF_RenderGlyph_LCD.
     *
     * \param font the font to render with.
     * \param ch the character to render.
     * \param fg the foreground color for the text.
     * \returns a new 8-bit, palettized surface, or NULL if there was an error.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_RenderGlyph_Blended
     * \sa TTF_RenderGlyph_LCD
     * \sa TTF_RenderGlyph_Shaded
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* TTF_RenderGlyph_Solid(Font* font, int ch, Color fg);

    public static Surface* RenderGlyphSolid(Font* font, int ch, Color fg) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_RenderGlyph_Solid(font, ch, fg);
    }

    /**
     * Render UTF-8 text at high quality to a new 8-bit surface.
     *
     * This function will allocate a new 8-bit, palettized surface. The surface's
     * 0 pixel will be the specified background color, while other pixels have
     * varying degrees of the foreground color. This function returns the new
     * surface, or NULL if there was an error.
     *
     * This will not word-wrap the string; you'll get a surface with a single line
     * of text, as long as the string requires. You can use
     * TTF_RenderText_Shaded_Wrapped() instead if you need to wrap the output to
     * multiple lines.
     *
     * This will not wrap on newline characters.
     *
     * You can render at other quality levels with TTF_RenderText_Solid,
     * TTF_RenderText_Blended, and TTF_RenderText_LCD.
     *
     * \param font the font to render with.
     * \param text text to render, in UTF-8 encoding.
     * \param length the length of the text, in bytes, or 0 for null terminated
     *               text.
     * \param fg the foreground color for the text.
     * \param bg the background color for the text.
     * \returns a new 8-bit, palettized surface, or NULL if there was an error.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_RenderText_Blended
     * \sa TTF_RenderText_LCD
     * \sa TTF_RenderText_Shaded_Wrapped
     * \sa TTF_RenderText_Solid
     */
    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* TTF_RenderText_Shaded(Font* font, string text, Size length, Color fg, Color bg);

    public static Surface* RenderTextShaded(Font* font, string text, Size length, Color fg, Color bg) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_RenderText_Shaded(font, text, length, fg, bg);
    }

    /**
     * Render word-wrapped UTF-8 text at high quality to a new 8-bit surface.
     *
     * This function will allocate a new 8-bit, palettized surface. The surface's
     * 0 pixel will be the specified background color, while other pixels have
     * varying degrees of the foreground color. This function returns the new
     * surface, or NULL if there was an error.
     *
     * Text is wrapped to multiple lines on line endings and on word boundaries if
     * it extends beyond `wrap_width` in pixels.
     *
     * If wrap_width is 0, this function will only wrap on newline characters.
     *
     * You can render at other quality levels with TTF_RenderText_Solid_Wrapped,
     * TTF_RenderText_Blended_Wrapped, and TTF_RenderText_LCD_Wrapped.
     *
     * \param font the font to render with.
     * \param text text to render, in UTF-8 encoding.
     * \param length the length of the text, in bytes, or 0 for null terminated
     *               text.
     * \param fg the foreground color for the text.
     * \param bg the background color for the text.
     * \param wrap_width the maximum width of the text surface or 0 to wrap on
     *                   newline characters.
     * \returns a new 8-bit, palettized surface, or NULL if there was an error.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_RenderText_Blended_Wrapped
     * \sa TTF_RenderText_LCD_Wrapped
     * \sa TTF_RenderText_Shaded
     * \sa TTF_RenderText_Solid_Wrapped
     */
    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* TTF_RenderText_Shaded_Wrapped(Font* font, string text, Size length, Color fg, Color bg, int wrap_width);

    public static Surface* RenderTextShadedWrapped(Font* font, string text, Size length, Color fg, Color bg, int wrap_width) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_RenderText_Shaded_Wrapped(font, text, length, fg, bg, wrap_width);
    }

    /**
     * Render a single UNICODE codepoint at high quality to a new 8-bit surface.
     *
     * This function will allocate a new 8-bit, palettized surface. The surface's
     * 0 pixel will be the specified background color, while other pixels have
     * varying degrees of the foreground color. This function returns the new
     * surface, or NULL if there was an error.
     *
     * The glyph is rendered without any padding or centering in the X direction,
     * and aligned normally in the Y direction.
     *
     * You can render at other quality levels with TTF_RenderGlyph_Solid,
     * TTF_RenderGlyph_Blended, and TTF_RenderGlyph_LCD.
     *
     * \param font the font to render with.
     * \param ch the codepoint to render.
     * \param fg the foreground color for the text.
     * \param bg the background color for the text.
     * \returns a new 8-bit, palettized surface, or NULL if there was an error.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_RenderGlyph_Blended
     * \sa TTF_RenderGlyph_LCD
     * \sa TTF_RenderGlyph_Solid
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* TTF_RenderGlyph_Shaded(Font* font, int ch, Color fg, Color bg);

    public static Surface* RenderGlyphShaded(Font* font, int ch, Color fg, Color bg) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_RenderGlyph_Shaded(font, ch, fg, bg);
    }

    /**
     * Render UTF-8 text at high quality to a new ARGB surface.
     *
     * This function will allocate a new 32-bit, ARGB surface, using alpha
     * blending to dither the font with the given color. This function returns the
     * new surface, or NULL if there was an error.
     *
     * This will not word-wrap the string; you'll get a surface with a single line
     * of text, as long as the string requires. You can use
     * TTF_RenderText_Blended_Wrapped() instead if you need to wrap the output to
     * multiple lines.
     *
     * This will not wrap on newline characters.
     *
     * You can render at other quality levels with TTF_RenderText_Solid,
     * TTF_RenderText_Shaded, and TTF_RenderText_LCD.
     *
     * \param font the font to render with.
     * \param text text to render, in UTF-8 encoding.
     * \param length the length of the text, in bytes, or 0 for null terminated
     *               text.
     * \param fg the foreground color for the text.
     * \returns a new 32-bit, ARGB surface, or NULL if there was an error.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_RenderText_Blended_Wrapped
     * \sa TTF_RenderText_LCD
     * \sa TTF_RenderText_Shaded
     * \sa TTF_RenderText_Solid
     */
    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* TTF_RenderText_Blended(Font* font, string text, Size length, Color fg);

    public static Surface* RenderTextBlended(Font* font, string text, Size length, Color fg) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_RenderText_Blended(font, text, length, fg);
    }

    /**
     * Render word-wrapped UTF-8 text at high quality to a new ARGB surface.
     *
     * This function will allocate a new 32-bit, ARGB surface, using alpha
     * blending to dither the font with the given color. This function returns the
     * new surface, or NULL if there was an error.
     *
     * Text is wrapped to multiple lines on line endings and on word boundaries if
     * it extends beyond `wrap_width` in pixels.
     *
     * If wrap_width is 0, this function will only wrap on newline characters.
     *
     * You can render at other quality levels with TTF_RenderText_Solid_Wrapped,
     * TTF_RenderText_Shaded_Wrapped, and TTF_RenderText_LCD_Wrapped.
     *
     * \param font the font to render with.
     * \param text text to render, in UTF-8 encoding.
     * \param length the length of the text, in bytes, or 0 for null terminated
     *               text.
     * \param fg the foreground color for the text.
     * \param wrap_width the maximum width of the text surface or 0 to wrap on
     *                   newline characters.
     * \returns a new 32-bit, ARGB surface, or NULL if there was an error.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_RenderText_Blended
     * \sa TTF_RenderText_LCD_Wrapped
     * \sa TTF_RenderText_Shaded_Wrapped
     * \sa TTF_RenderText_Solid_Wrapped
     */

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* TTF_RenderText_Blended_Wrapped(Font* font, string text, Size length, Color fg, int wrap_width);

    public static Surface* RenderTextBlendedWrapped(Font* font, string text, Size length, Color fg, int wrap_width) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_RenderText_Blended_Wrapped(font, text, length, fg, wrap_width);
    }

    /**
     * Render a single UNICODE codepoint at high quality to a new ARGB surface.
     *
     * This function will allocate a new 32-bit, ARGB surface, using alpha
     * blending to dither the font with the given color. This function returns the
     * new surface, or NULL if there was an error.
     *
     * The glyph is rendered without any padding or centering in the X direction,
     * and aligned normally in the Y direction.
     *
     * You can render at other quality levels with TTF_RenderGlyph_Solid,
     * TTF_RenderGlyph_Shaded, and TTF_RenderGlyph_LCD.
     *
     * \param font the font to render with.
     * \param ch the codepoint to render.
     * \param fg the foreground color for the text.
     * \returns a new 32-bit, ARGB surface, or NULL if there was an error.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_RenderGlyph_LCD
     * \sa TTF_RenderGlyph_Shaded
     * \sa TTF_RenderGlyph_Solid
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* TTF_RenderGlyph_Blended(Font* font, int ch, Color fg);

    public static Surface* RenderGlyphBlended(Font* font, int ch, Color fg) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_RenderGlyph_Blended(font, ch, fg);
    }

    /**
     * Render UTF-8 text at LCD subpixel quality to a new ARGB surface.
     *
     * This function will allocate a new 32-bit, ARGB surface, and render
     * alpha-blended text using FreeType's LCD subpixel rendering. This function
     * returns the new surface, or NULL if there was an error.
     *
     * This will not word-wrap the string; you'll get a surface with a single line
     * of text, as long as the string requires. You can use
     * TTF_RenderText_LCD_Wrapped() instead if you need to wrap the output to
     * multiple lines.
     *
     * This will not wrap on newline characters.
     *
     * You can render at other quality levels with TTF_RenderText_Solid,
     * TTF_RenderText_Shaded, and TTF_RenderText_Blended.
     *
     * \param font the font to render with.
     * \param text text to render, in UTF-8 encoding.
     * \param length the length of the text, in bytes, or 0 for null terminated
     *               text.
     * \param fg the foreground color for the text.
     * \param bg the background color for the text.
     * \returns a new 32-bit, ARGB surface, or NULL if there was an error.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_RenderText_Blended
     * \sa TTF_RenderText_LCD_Wrapped
     * \sa TTF_RenderText_Shaded
     * \sa TTF_RenderText_Solid
     */
    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* TTF_RenderText_LCD(Font* font, string text, Size length, Color fg, Color bg);

    public static Surface* RenderTextLCD(Font* font, string text, Size length, Color fg, Color bg) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_RenderText_LCD(font, text, length, fg, bg);
    }

    /**
     * Render word-wrapped UTF-8 text at LCD subpixel quality to a new ARGB
     * surface.
     *
     * This function will allocate a new 32-bit, ARGB surface, and render
     * alpha-blended text using FreeType's LCD subpixel rendering. This function
     * returns the new surface, or NULL if there was an error.
     *
     * Text is wrapped to multiple lines on line endings and on word boundaries if
     * it extends beyond `wrap_width` in pixels.
     *
     * If wrap_width is 0, this function will only wrap on newline characters.
     *
     * You can render at other quality levels with TTF_RenderText_Solid_Wrapped,
     * TTF_RenderText_Shaded_Wrapped, and TTF_RenderText_Blended_Wrapped.
     *
     * \param font the font to render with.
     * \param text text to render, in UTF-8 encoding.
     * \param length the length of the text, in bytes, or 0 for null terminated
     *               text.
     * \param fg the foreground color for the text.
     * \param bg the background color for the text.
     * \param wrap_width the maximum width of the text surface or 0 to wrap on
     *                   newline characters.
     * \returns a new 32-bit, ARGB surface, or NULL if there was an error.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_RenderText_Blended_Wrapped
     * \sa TTF_RenderText_LCD
     * \sa TTF_RenderText_Shaded_Wrapped
     * \sa TTF_RenderText_Solid_Wrapped
     */
    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* TTF_RenderText_LCD_Wrapped(Font* font, string text, Size length, Color fg, Color bg, int wrap_width);

    public static Surface* RenderTextLCDWrapped(Font* font, string text, Size length, Color fg, Color bg, int wrap_width) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_RenderText_LCD_Wrapped(font, text, length, fg, bg, wrap_width);
    }

    /**
     * Render a single UNICODE codepoint at LCD subpixel quality to a new ARGB
     * surface.
     *
     * This function will allocate a new 32-bit, ARGB surface, and render
     * alpha-blended text using FreeType's LCD subpixel rendering. This function
     * returns the new surface, or NULL if there was an error.
     *
     * The glyph is rendered without any padding or centering in the X direction,
     * and aligned normally in the Y direction.
     *
     * You can render at other quality levels with TTF_RenderGlyph_Solid,
     * TTF_RenderGlyph_Shaded, and TTF_RenderGlyph_Blended.
     *
     * \param font the font to render with.
     * \param ch the codepoint to render.
     * \param fg the foreground color for the text.
     * \param bg the background color for the text.
     * \returns a new 32-bit, ARGB surface, or NULL if there was an error.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_RenderGlyph_Blended
     * \sa TTF_RenderGlyph_Shaded
     * \sa TTF_RenderGlyph_Solid
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* TTF_RenderGlyph_LCD(Font* font, int ch, Color fg, Color bg);

    public static Surface* RenderGlyphLCD(Font* font, int ch, Color fg, Color bg) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        return TTF_RenderGlyph_LCD(font, ch, fg, bg);
    }

    /**
     * Create a text engine for drawing text on SDL surfaces.
     *
     * \returns a TTF_TextEngine object or NULL on failure; call SDL_GetError()
     *          for more information.
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_DestroySurfaceTextEngine
     * \sa TTF_DrawSurfaceText
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial TextEngine* TTF_CreateSurfaceTextEngine();

    public static TextEngine* CreateSurfaceTextEngine() {
        return TTF_CreateSurfaceTextEngine();
    }

    /**
     * Draw text to an SDL surface.
     *
     * `text` must have been created using a TTF_TextEngine from
     * TTF_CreateSurfaceTextEngine().
     *
     * \param text the text to draw.
     * \param x the x coordinate in pixels, positive from the left edge towards
     *          the right.
     * \param y the y coordinate in pixels, positive from the top edge towards the
     *          bottom.
     * \param surface the surface to draw on.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_CreateSurfaceTextEngine
     * \sa TTF_CreateText
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_DrawSurfaceText(TtfText* text, int x, int y, Surface* surface);

    public static bool DrawSurfaceText(TtfText* text, int x, int y, Surface* surface) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        if (surface == null) {
            throw new ArgumentNullException(nameof(surface), "Surface cannot be null.");
        }
        return TTF_DrawSurfaceText(text, x, y, surface);
    }

    /**
     * Destroy a text engine created for drawing text on SDL surfaces.
     *
     * All text created by this engine should be destroyed before calling this
     * function.
     *
     * \param engine a TTF_TextEngine object created with
     *               TTF_CreateSurfaceTextEngine().
     *
     * \threadsafety This function should be called on the thread that created the
     *               engine.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_CreateSurfaceTextEngine
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_DestroySurfaceTextEngine(TextEngine* engine);

    public static void DestroySurfaceTextEngine(TextEngine* engine) {
        if (engine == null) {
            throw new ArgumentNullException(nameof(engine), "Engine cannot be null.");
        }
        TTF_DestroySurfaceTextEngine(engine);
    }

    /**
     * Create a text engine for drawing text on an SDL renderer.
     *
     * \param renderer the renderer to use for creating textures and drawing text.
     * \returns a TTF_TextEngine object or NULL on failure; call SDL_GetError()
     *          for more information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               renderer.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_DestroyRendererTextEngine
     * \sa TTF_DrawRendererText
     * \sa TTF_CreateRendererTextEngineWithProperties
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial TextEngine* TTF_CreateRendererTextEngine(nint renderer);

    public static TextEngine* CreateRendererTextEngine(nint renderer) {
        if (renderer == IntPtr.Zero) {
            throw new ArgumentNullException(nameof(renderer), "Renderer cannot be null.");
        }
        return TTF_CreateRendererTextEngine(renderer);
    }

    /**
     * Create a text engine for drawing text on an SDL renderer, with the
     * specified properties.
     *
     * These are the supported properties:
     *
     * - `TTF_PROP_RENDERER_TEXT_ENGINE_RENDERER`: the renderer to use for
     *   creating textures and drawing text
     * - `TTF_PROP_RENDERER_TEXT_ENGINE_ATLAS_TEXTURE_SIZE`: the size of the
     *   texture atlas
     *
     * \param props the properties to use.
     * \returns a TTF_TextEngine object or NULL on failure; call SDL_GetError()
     *          for more information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               renderer.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_CreateRendererTextEngine
     * \sa TTF_DestroyRendererTextEngine
     * \sa TTF_DrawRendererText
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial TextEngine* TTF_CreateRendererTextEngineWithProperties(int props);
    public static TextEngine* CreateRendererTextEngineWithProperties(int props) {
        if (props == 0) {
            throw new ArgumentNullException(nameof(props), "Properties cannot be null.");
        }
        return TTF_CreateRendererTextEngineWithProperties(props);
    }


    public const string TTF_PROP_RENDERER_TEXT_ENGINE_RENDERER = "SDL_ttf.renderer_text_engine.create.renderer";
    public const string TTF_PROP_RENDERER_TEXT_ENGINE_ATLAS_TEXTURE_SIZE = "SDL_ttf.renderer_text_engine.create.atlas_texture_size";

    /**
     * Draw text to an SDL renderer.
     *
     * `text` must have been created using a TTF_TextEngine from
     * TTF_CreateRendererTextEngine(), and will draw using the renderer passed to
     * that function.
     *
     * \param text the text to draw.
     * \param x the x coordinate in pixels, positive from the left edge towards
     *          the right.
     * \param y the y coordinate in pixels, positive from the top edge towards the
     *          bottom.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_CreateRendererTextEngine
     * \sa TTF_CreateText
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_DrawRendererText(TtfText* text, float x, float y);

    public static bool DrawRendererText(TtfText* text, float x, float y) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_DrawRendererText(text, x, y);
    }

    /**
     * Destroy a text engine created for drawing text on an SDL renderer.
     *
     * All text created by this engine should be destroyed before calling this
     * function.
     *
     * \param engine a TTF_TextEngine object created with
     *               TTF_CreateRendererTextEngine().
     *
     * \threadsafety This function should be called on the thread that created the
     *               engine.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_CreateRendererTextEngine
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_DestroyRendererTextEngine(TextEngine* engine);

    public static void DestroyRendererTextEngine(TextEngine* engine) {
        if (engine == null) {
            throw new ArgumentNullException(nameof(engine), "Engine cannot be null.");
        }
        TTF_DestroyRendererTextEngine(engine);
    }

    /**
     * Create a text engine for drawing text with the SDL GPU API.
     *
     * \param device the SDL_GPUDevice to use for creating textures and drawing
     *               text.
     * \returns a TTF_TextEngine object or NULL on failure; call SDL_GetError()
     *          for more information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               device.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_CreateGPUTextEngineWithProperties
     * \sa TTF_DestroyGPUTextEngine
     * \sa TTF_GetGPUTextDrawData
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial TextEngine* TTF_CreateGPUTextEngine(nint device);

    public static TextEngine* CreateGPUTextEngine(nint device) {
        if (device == IntPtr.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        return TTF_CreateGPUTextEngine(device);
    }

    /**
     * Create a text engine for drawing text with the SDL GPU API, with the
     * specified properties.
     *
     * These are the supported properties:
     *
     * - `TTF_PROP_GPU_TEXT_ENGINE_DEVICE`: the SDL_GPUDevice to use for creating
     *   textures and drawing text.
     * - `TTF_PROP_GPU_TEXT_ENGINE_ATLAS_TEXTURE_SIZE`: the size of the texture
     *   atlas
     *
     * \param props the properties to use.
     * \returns a TTF_TextEngine object or NULL on failure; call SDL_GetError()
     *          for more information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               device.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_CreateGPUTextEngine
     * \sa TTF_DestroyGPUTextEngine
     * \sa TTF_GetGPUTextDrawData
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial TextEngine* TTF_CreateGPUTextEngineWithProperties(int props);

    public static TextEngine* CreateGPUTextEngineWithProperties(int props) {
        if (props == 0) {
            throw new ArgumentNullException(nameof(props), "Properties cannot be null.");
        }
        return TTF_CreateGPUTextEngineWithProperties(props);
    }


    public const string TTF_PROP_GPU_TEXT_ENGINE_DEVICE = "SDL_ttf.gpu_text_engine.create.device";
    public const string TTF_PROP_GPU_TEXT_ENGINE_ATLAS_TEXTURE_SIZE = "SDL_ttf.gpu_text_engine.create.atlas_texture_size";

    /**
     * Get the geometry data needed for drawing the text.
     *
     * `text` must have been created using a TTF_TextEngine from
     * TTF_CreateGPUTextEngine().
     *
     * The positive X-axis is taken towards the right and the positive Y-axis is
     * taken upwards for both the vertex and the texture coordinates, i.e, it
     * follows the same convention used by the SDL_GPU API. If you want to use a
     * different coordinate system you will need to transform the vertices
     * yourself.
     *
     * If the text looks blocky use linear filtering.
     *
     * \param text the text to draw.
     * \returns a NULL terminated linked list of TTF_GPUAtlasDrawSequence objects
     *          or NULL if the passed text is empty or in case of failure; call
     *          SDL_GetError() for more information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_CreateGPUTextEngine
     * \sa TTF_CreateText
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GPUAtlasDrawSequence* TTF_GetGPUTextDrawData(TtfText* text);

    public static GPUAtlasDrawSequence* GetGPUTextDrawData(TtfText* text) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_GetGPUTextDrawData(text);
    }

    /**
     * Destroy a text engine created for drawing text with the SDL GPU API.
     *
     * All text created by this engine should be destroyed before calling this
     * function.
     *
     * \param engine a TTF_TextEngine object created with
     *               TTF_CreateGPUTextEngine().
     *
     * \threadsafety This function should be called on the thread that created the
     *               engine.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_CreateGPUTextEngine
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_DestroyGPUTextEngine(TextEngine* engine);

    public static void DestroyGPUTextEngine(TextEngine* engine) {
        if (engine == null) {
            throw new ArgumentNullException(nameof(engine), "Engine cannot be null.");
        }
        TTF_DestroyGPUTextEngine(engine);
    }

    /**
     * The winding order of the vertices returned by TTF_GetGPUTextDrawData
     *
     * \since This enum is available since SDL_ttf 3.0.0.
     */
    public enum TTF_GPUTextEngineWinding {
        TTF_GPU_TEXTENGINE_WINDING_INVALID = -1,
        TTF_GPU_TEXTENGINE_WINDING_CLOCKWISE,
        TTF_GPU_TEXTENGINE_WINDING_COUNTER_CLOCKWISE
    }

    /**
     * Sets the winding order of the vertices returned by TTF_GetGPUTextDrawData
     * for a particular GPU text engine.
     *
     * \param engine a TTF_TextEngine object created with
     *               TTF_CreateGPUTextEngine().
     * \param winding the new winding order option.
     *
     * \threadsafety This function should be called on the thread that created the
     *               engine.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_GetGPUTextEngineWinding
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_SetGPUTextEngineWinding(TextEngine* engine, TTF_GPUTextEngineWinding winding);

    public static void SetGPUTextEngineWinding(TextEngine* engine, TTF_GPUTextEngineWinding winding) {
        if (engine == null) {
            throw new ArgumentNullException(nameof(engine), "Engine cannot be null.");
        }
        TTF_SetGPUTextEngineWinding(engine, winding);
    }

    /**
     * Get the winding order of the vertices returned by TTF_GetGPUTextDrawData
     * for a particular GPU text engine
     *
     * \param engine a TTF_TextEngine object created with
     *               TTF_CreateGPUTextEngine().
     * \returns the winding order used by the GPU text engine or
     *          TTF_GPU_TEXTENGINE_WINDING_INVALID in case of error.
     *
     * \threadsafety This function should be called on the thread that created the
     *               engine.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_SetGPUTextEngineWinding
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial TTF_GPUTextEngineWinding TTF_GetGPUTextEngineWinding(TextEngine* engine);

    public static TTF_GPUTextEngineWinding GetGPUTextEngineWinding(TextEngine* engine) {
        if (engine == null) {
            throw new ArgumentNullException(nameof(engine), "Engine cannot be null.");
        }
        return TTF_GetGPUTextEngineWinding(engine);
    }

    /**
     * Create a text object from UTF-8 text and a text engine.
     *
     * \param engine the text engine to use when creating the text object, may be
     *               NULL.
     * \param font the font to render with.
     * \param text the text to use, in UTF-8 encoding.
     * \param length the length of the text, in bytes, or 0 for null terminated
     *               text.
     * \returns a TTF_Text object or NULL on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               font and text engine.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_DestroyText
     */
    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial TtfText* TTF_CreateText(TextEngine* engine, Font* font, string text, Size length);

    public static TtfText* CreateText(TextEngine* engine, Font* font, string text, Size length) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_CreateText(engine, font, text, length);
    }

    /**
     * Get the properties associated with a text object.
     *
     * \param text the TTF_Text to query.
     * \returns a valid property ID on success or 0 on failure; call
     *          SDL_GetError() for more information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetTextProperties(TtfText* text);
    
    public static int GetTextProperties(TtfText* text) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_GetTextProperties(text);
    }

    /**
     * Set the text engine used by a text object.
     *
     * This function may cause the internal text representation to be rebuilt.
     *
     * \param text the TTF_Text to modify.
     * \param engine the text engine to use for drawing.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_GetTextEngine
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_SetTextEngine(TtfText* text, TextEngine* engine);

    public static bool SetTextEngine(TtfText* text, TextEngine* engine) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextEngine(text, engine);
    }

    /**
     * Get the text engine used by a text object.
     *
     * \param text the TTF_Text to query.
     * \returns the TTF_TextEngine used by the text on success or NULL on failure;
     *          call SDL_GetError() for more information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_SetTextEngine
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial TextEngine* TTF_GetTextEngine(TtfText* text);

    public static TextEngine* GetTextEngine(TtfText* text) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_GetTextEngine(text);
    }

    /**
     * Set the font used by a text object.
     *
     * When a text object has a font, any changes to the font will automatically
     * regenerate the text. If you set the font to NULL, the text will continue to
     * render but changes to the font will no longer affect the text.
     *
     * This function may cause the internal text representation to be rebuilt.
     *
     * \param text the TTF_Text to modify.
     * \param font the font to use, may be NULL.
     * \returns false if the text pointer is null; otherwise, true. call
     *          SDL_GetError() for more information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_GetTextFont
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_SetTextFont(TtfText* text, Font* font);

    public static bool SetTextFont(TtfText* text, Font* font) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextFont(text, font);
    }

    /**
     * Get the font used by a text object.
     *
     * \param text the TTF_Text to query.
     * \returns the TTF_Font used by the text on success or NULL on failure; call
     *          SDL_GetError() for more information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_SetTextFont
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Font* TTF_GetTextFont(TtfText* text);

    public static Font* GetTextFont(TtfText* text) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_GetTextFont(text);
    }

    /**
     * Set the direction to be used for text shaping a text object.
     *
     * This function only supports left-to-right text shaping if SDL_ttf was not
     * built with HarfBuzz support.
     *
     * \param text the text to modify.
     * \param direction the new direction for text to flow.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_SetTextDirection(TtfText* text, Direction direction);

    public static bool SetTextDirection(TtfText* text, Direction direction) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextDirection(text, direction);
    }

    /**
     * Get the direction to be used for text shaping a text object.
     *
     * This defaults to the direction of the font used by the text object.
     *
     * \param text the text to query.
     * \returns the direction to be used for text shaping.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Direction TTF_GetTextDirection(TtfText* text);

    public static Direction GetTextDirection(TtfText* text) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_GetTextDirection(text);
    }

    /**
     * Set the script to be used for text shaping a text object.
     *
     * This returns false if SDL_ttf isn't built with HarfBuzz support.
     *
     * \param text the text to modify.
     * \param script an
     *               [ISO 15924 code](https://unicode.org/iso15924/iso15924-codes.html)
     *               .
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_StringToTag
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_SetTextScript(TtfText* text, int script);

    public static bool SetTextScript(TtfText* text, int script) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextScript(text, script);
    }

    /**
     * Get the script used for text shaping a text object.
     *
     * This defaults to the script of the font used by the text object.
     *
     * \param text the text to query.
     * \returns an
     *          [ISO 15924 code](https://unicode.org/iso15924/iso15924-codes.html)
     *          or 0 if a script hasn't been set on either the text object or the
     *          font.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_TagToString
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetTextScript(TtfText* text);

    public static int GetTextScript(TtfText* text) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_GetTextScript(text);
    }

    /**
     * Set the color of a text object.
     *
     * The default text color is white (255, 255, 255, 255).
     *
     * \param text the TTF_Text to modify.
     * \param r the red color value in the range of 0-255.
     * \param g the green color value in the range of 0-255.
     * \param b the blue color value in the range of 0-255.
     * \param a the alpha value in the range of 0-255.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_GetTextColor
     * \sa TTF_SetTextColorFloat
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_SetTextColor(TtfText* text, byte r, byte g, byte b, byte a);

    public static bool SetTextColor(TtfText* text, byte r, byte g, byte b, byte a) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextColor(text, r, g, b, a);
    }

    public static bool SetTextColor(TtfText* text, Color color) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextColor(text, color.R, color.G, color.B, color.A);
    }

    /**
     * Set the color of a text object.
     *
     * The default text color is white (1.0f, 1.0f, 1.0f, 1.0f).
     *
     * \param text the TTF_Text to modify.
     * \param r the red color value, normally in the range of 0-1.
     * \param g the green color value, normally in the range of 0-1.
     * \param b the blue color value, normally in the range of 0-1.
     * \param a the alpha value in the range of 0-1.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_GetTextColorFloat
     * \sa TTF_SetTextColor
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_SetTextColorFloat(TtfText* text, float r, float g, float b, float a);

    public static bool SetTextColorFloat(TtfText* text, float r, float g, float b, float a) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextColorFloat(text, r, g, b, a);
    }

    public static bool SetTextColorFloat(TtfText* text, FColor color) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextColorFloat(text, color.R, color.G, color.B, color.A);
    }

    /**
     * Get the color of a text object.
     *
     * \param text the TTF_Text to query.
     * \param r a pointer filled in with the red color value in the range of
     *          0-255, may be NULL.
     * \param g a pointer filled in with the green color value in the range of
     *          0-255, may be NULL.
     * \param b a pointer filled in with the blue color value in the range of
     *          0-255, may be NULL.
     * \param a a pointer filled in with the alpha value in the range of 0-255,
     *          may be NULL.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_GetTextColorFloat
     * \sa TTF_SetTextColor
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_GetTextColor(TtfText* text, byte* r, byte* g, byte* b, byte* a);

    public static bool GetTextColor(TtfText* text, out byte r, out byte g, out byte b, out byte a) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        fixed (byte* pr = &r, pg = &g, pb = &b, pa = &a) {
            return TTF_GetTextColor(text, pr, pg, pb, pa);
        }
    }

    public static Color GetTextColor(TtfText* text) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        GetTextColor(text, out byte r, out byte g, out byte b, out byte a);
        return new Color() { R = r, G = g, B = b, A = a };
    }

    /**
     * Get the color of a text object.
     *
     * \param text the TTF_Text to query.
     * \param r a pointer filled in with the red color value, normally in the
     *          range of 0-1, may be NULL.
     * \param g a pointer filled in with the green color value, normally in the
     *          range of 0-1, may be NULL.
     * \param b a pointer filled in with the blue color value, normally in the
     *          range of 0-1, may be NULL.
     * \param a a pointer filled in with the alpha value in the range of 0-1, may
     *          be NULL.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_GetTextColor
     * \sa TTF_SetTextColorFloat
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_GetTextColorFloat(TtfText* text, float* r, float* g, float* b, float* a);

    public static bool GetTextColorFloat(TtfText* text, out float r, out float g, out float b, out float a) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        fixed (float* pr = &r, pg = &g, pb = &b, pa = &a) {
            return TTF_GetTextColorFloat(text, pr, pg, pb, pa);
        }
    }

    public static bool GetTextColorFloat(TtfText* text, out FColor color) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        GetTextColorFloat(text, out float r, out float g, out float b, out float a);
        color = new FColor() { R = r, G = g, B = b, A = a };
        return true;
    }

    /**
     * Set the position of a text object.
     *
     * This can be used to position multiple text objects within a single wrapping
     * text area.
     *
     * This function may cause the internal text representation to be rebuilt.
     *
     * \param text the TTF_Text to modify.
     * \param x the x offset of the upper left corner of this text in pixels.
     * \param y the y offset of the upper left corner of this text in pixels.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_GetTextPosition
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_SetTextPosition(TtfText* text, int x, int y);

    public static bool SetTextPosition(TtfText* text, int x, int y) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextPosition(text, x, y);
    }

    public static bool SetTextPosition(TtfText* text, Point position) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextPosition(text, position.X, position.Y);
    }

    /**
     * Get the position of a text object.
     *
     * \param text the TTF_Text to query.
     * \param x a pointer filled in with the x offset of the upper left corner of
     *          this text in pixels, may be NULL.
     * \param y a pointer filled in with the y offset of the upper left corner of
     *          this text in pixels, may be NULL.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_SetTextPosition
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_GetTextPosition(TtfText* text, int* x, int* y);

    public static bool GetTextPosition(TtfText* text, out int x, out int y) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        fixed (int* px = &x, py = &y) {
            return TTF_GetTextPosition(text, px, py);
        }
    }

    public static Point GetTextPosition(TtfText* text) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        GetTextPosition(text, out int x, out int y);
        return new Point() { X = x, Y = y };
    }

    /**
     * Set whether wrapping is enabled on a text object.
     *
     * This function may cause the internal text representation to be rebuilt.
     *
     * \param text the TTF_Text to modify.
     * \param wrap_width the maximum width in pixels, 0 to wrap on newline
     *                   characters.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_GetTextWrapWidth
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_SetTextWrapWidth(TtfText* text, int wrap_width);

    public static bool SetTextWrapWidth(TtfText* text, int wrap_width) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextWrapWidth(text, wrap_width);
    }

    /**
     * Get whether wrapping is enabled on a text object.
     *
     * \param text the TTF_Text to query.
     * \param wrap_width a pointer filled in with the maximum width in pixels or 0
     *                   if the text is being wrapped on newline characters.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_SetTextWrapWidth
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_GetTextWrapWidth(TtfText* text, int* wrap_width);

    public static bool GetTextWrapWidth(TtfText* text, out int wrap_width) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        fixed (int* pWrapWidth = &wrap_width) {
            return TTF_GetTextWrapWidth(text, pWrapWidth);
        }
    }

    /**
     * Set whether whitespace should be visible when wrapping a text object.
     *
     * If the whitespace is visible, it will take up space for purposes of
     * alignment and wrapping. This is good for editing, but looks better when
     * centered or aligned if whitespace around line wrapping is hidden. This
     * defaults false.
     *
     * This function may cause the internal text representation to be rebuilt.
     *
     * \param text the TTF_Text to modify.
     * \param visible true to show whitespace when wrapping text, false to hide
     *                it.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_TextWrapWhitespaceVisible
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_SetTextWrapWhitespaceVisible(TtfText* text, [MarshalAs(UnmanagedType.Bool)] bool visible);

    public static bool SetTextWrapWhitespaceVisible(TtfText* text, bool visible) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextWrapWhitespaceVisible(text, visible);
    }

    /**
     * Return whether whitespace is shown when wrapping a text object.
     *
     * \param text the TTF_Text to query.
     * \returns true if whitespace is shown when wrapping text, or false
     *          otherwise.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_SetTextWrapWhitespaceVisible
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_TextWrapWhitespaceVisible(TtfText* text);

    public static bool TextWrapWhitespaceVisible(TtfText* text) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_TextWrapWhitespaceVisible(text);
    }

    /**
     * Set the UTF-8 text used by a text object.
     *
     * This function may cause the internal text representation to be rebuilt.
     *
     * \param text the TTF_Text to modify.
     * \param string the UTF-8 text to use, may be NULL.
     * \param length the length of the text, in bytes, or 0 for null terminated
     *               text.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_AppendTextString
     * \sa TTF_DeleteTextString
     * \sa TTF_InsertTextString
     */
    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_SetTextString(TtfText* text, string str, Size length);

    public static bool SetTextString(TtfText* text, string str, Size length) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        if (str == null) {
            throw new ArgumentNullException(nameof(str), "String cannot be null.");
        }
        return TTF_SetTextString(text, str, length);
    }

    /**
     * Insert UTF-8 text into a text object.
     *
     * This function may cause the internal text representation to be rebuilt.
     *
     * \param text the TTF_Text to modify.
     * \param offset the offset, in bytes, from the beginning of the string if >=
     *               0, the offset from the end of the string if < 0. Note that
     *               this does not do UTF-8 validation, so you should only insert
     *               at UTF-8 sequence boundaries.
     * \param string the UTF-8 text to insert.
     * \param length the length of the text, in bytes, or 0 for null terminated
     *               text.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_AppendTextString
     * \sa TTF_DeleteTextString
     * \sa TTF_SetTextString
     */
    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_InsertTextString(TtfText* text, int offset, string str, Size length);

    public static bool InsertTextString(TtfText* text, int offset, string str, Size length) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        if (str == null) {
            throw new ArgumentNullException(nameof(str), "String cannot be null.");
        }
        return TTF_InsertTextString(text, offset, str, length);
    }

    /**
     * Append UTF-8 text to a text object.
     *
     * This function may cause the internal text representation to be rebuilt.
     *
     * \param text the TTF_Text to modify.
     * \param string the UTF-8 text to insert.
     * \param length the length of the text, in bytes, or 0 for null terminated
     *               text.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_DeleteTextString
     * \sa TTF_InsertTextString
     * \sa TTF_SetTextString
     */
    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_AppendTextString(TtfText* text, string str, Size length);

    public static bool AppendTextString(TtfText* text, string str, Size length) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        if (str == null) {
            throw new ArgumentNullException(nameof(str), "String cannot be null.");
        }
        return TTF_AppendTextString(text, str, length);
    }

    /**
     * Delete UTF-8 text from a text object.
     *
     * This function may cause the internal text representation to be rebuilt.
     *
     * \param text the TTF_Text to modify.
     * \param offset the offset, in bytes, from the beginning of the string if >=
     *               0, the offset from the end of the string if < 0. Note that
     *               this does not do UTF-8 validation, so you should only delete
     *               at UTF-8 sequence boundaries.
     * \param length the length of text to delete, in bytes, or -1 for the
     *               remainder of the string.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_AppendTextString
     * \sa TTF_InsertTextString
     * \sa TTF_SetTextString
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_DeleteTextString(TtfText* text, int offset, int length);

    public static bool DeleteTextString(TtfText* text, int offset, int length) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_DeleteTextString(text, offset, length);
    }

    /**
     * Get the size of a text object.
     *
     * The size of the text may change when the font or font style and size
     * change.
     *
     * \param text the TTF_Text to query.
     * \param w a pointer filled in with the width of the text, in pixels, may be
     *          NULL.
     * \param h a pointer filled in with the height of the text, in pixels, may be
     *          NULL.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_GetTextSize(TtfText* text, int* w, int* h);

    public static bool GetTextSize(TtfText* text, out int w, out int h) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        fixed (int* pw = &w, ph = &h) {
            return TTF_GetTextSize(text, pw, ph);
        }
    }

    public static Size GetTextSize(TtfText* text) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        GetTextSize(text, out int w, out int h);
        return new Size() { Width = w, Height = h };
    }

    /**
     * Get the substring of a text object that surrounds a text offset.
     *
     * If `offset` is less than 0, this will return a zero length substring at the
     * beginning of the text with the TTF_SUBSTRING_TEXT_START flag set. If
     * `offset` is greater than or equal to the length of the text string, this
     * will return a zero length substring at the end of the text with the
     * TTF_SUBSTRING_TEXT_END flag set.
     *
     * \param text the TTF_Text to query.
     * \param offset a byte offset into the text string.
     * \param substring a pointer filled in with the substring containing the
     *                  offset.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_GetTextSubString(TtfText* text, int offset, SubString* substring);

    public static bool GetTextSubString(TtfText* text, int offset, SubString* substring) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_GetTextSubString(text, offset, substring);
    }

    /**
     * Get the substring of a text object that contains the given line.
     *
     * If `line` is less than 0, this will return a zero length substring at the
     * beginning of the text with the TTF_SUBSTRING_TEXT_START flag set. If `line`
     * is greater than or equal to `text->num_lines` this will return a zero
     * length substring at the end of the text with the TTF_SUBSTRING_TEXT_END
     * flag set.
     *
     * \param text the TTF_Text to query.
     * \param line a zero-based line index, in the range [0 .. text->num_lines-1].
     * \param substring a pointer filled in with the substring containing the
     *                  offset.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_GetTextSubStringForLine(TtfText* text, int line, SubString* substring);

    public static bool GetTextSubStringForLine(TtfText* text, int line, out SubString substring) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        if (line < 0) {
            throw new ArgumentOutOfRangeException(nameof(line), "Line index cannot be negative.");
        }
        fixed (SubString* pSubstring = &substring) {
            return TTF_GetTextSubStringForLine(text, line, pSubstring);
        }
    }

    /**
     * Get the substrings of a text object that contain a range of text.
     *
     * \param text the TTF_Text to query.
     * \param offset a byte offset into the text string.
     * \param length the length of the range being queried, in bytes, or -1 for
     *               the remainder of the string.
     * \param count a pointer filled in with the number of substrings returned,
     *              may be NULL.
     * \returns a NULL terminated array of substring pointers or NULL on failure;
     *          call SDL_GetError() for more information. This is a single
     *          allocation that should be freed with SDL_free() when it is no
     *          longer needed.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SubString** TTF_GetTextSubStringsForRange(TtfText* text, int offset, int length, int* count);

    public static SubString** GetTextSubStringsForRange(TtfText* text, int offset, int length, out int count) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        if (offset < 0) {
            throw new ArgumentOutOfRangeException(nameof(offset), "Offset cannot be negative.");
        }
        fixed (int* pCount = &count) {
            return TTF_GetTextSubStringsForRange(text, offset, length, pCount);
        }
    }

    /**
     * Get the portion of a text string that is closest to a point.
     *
     * This will return the closest substring of text to the given point.
     *
     * \param text the TTF_Text to query.
     * \param x the x coordinate relative to the left side of the text, may be
     *          outside the bounds of the text area.
     * \param y the y coordinate relative to the top side of the text, may be
     *          outside the bounds of the text area.
     * \param substring a pointer filled in with the closest substring of text to
     *                  the given point.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_GetTextSubStringForPoint(TtfText* text, int x, int y, SubString* substring);

    public static bool GetTextSubStringForPoint(TtfText* text, int x, int y, out SubString substring) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        fixed (SubString* pSubstring = &substring) {
            return TTF_GetTextSubStringForPoint(text, x, y, pSubstring);
        }
    }

    public static bool GetTextSubStringForPoint(TtfText* text, Point point, out SubString substring) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return GetTextSubStringForPoint(text, point.X, point.Y, out substring);
    }

    /**
     * Get the previous substring in a text object
     *
     * If called at the start of the text, this will return a zero length
     * substring with the TTF_SUBSTRING_TEXT_START flag set.
     *
     * \param text the TTF_Text to query.
     * \param substring the TTF_SubString to query.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_GetPreviousTextSubString(TtfText* text, SubString* substring, SubString* previous);

    public static bool GetPreviousTextSubString(TtfText* text, SubString* substring, out SubString previous) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        fixed (SubString* pPrevious = &previous) {
            return TTF_GetPreviousTextSubString(text, substring, pPrevious);
        }
    }

    /**
     * Get the next substring in a text object
     *
     * If called at the end of the text, this will return a zero length substring
     * with the TTF_SUBSTRING_TEXT_END flag set.
     *
     * \param text the TTF_Text to query.
     * \param substring the TTF_SubString to query.
     * \param next a pointer filled in with the next substring.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_GetNextTextSubString(TtfText* text, SubString* substring, SubString* next);

    public static bool GetNextTextSubString(TtfText* text, SubString* substring, out SubString next) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        fixed (SubString* pNext = &next) {
            return TTF_GetNextTextSubString(text, substring, pNext);
        }
    }

    /**
     * Update the layout of a text object.
     *
     * This is automatically done when the layout is requested or the text is
     * rendered, but you can call this if you need more control over the timing of
     * when the layout and text engine representation are updated.
     *
     * \param text the TTF_Text to update.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool TTF_UpdateText(TtfText* text);

    public static bool UpdateText(TtfText* text) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_UpdateText(text);
    }

    /**
     * Destroy a text object created by a text engine.
     *
     * \param text the text to destroy.
     *
     * \threadsafety This function should be called on the thread that created the
     *               text.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_CreateText
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_DestroyText(TtfText* text);

    public static void DestroyText(TtfText* text) {
        if (text == null) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        TTF_DestroyText(text);
    }

    /**
     * Dispose of a previously-created font.
     *
     * Call this when done with a font. This function will free any resources
     * associated with it. It is safe to call this function on NULL, for example
     * on the result of a failed call to TTF_OpenFont().
     *
     * The font is not valid after being passed to this function. String pointers
     * from functions that return information on this font, such as
     * TTF_GetFontFamilyName() and TTF_GetFontStyleName(), are no longer valid
     * after this call, as well.
     *
     * \param font the font to dispose of.
     *
     * \threadsafety This function should not be called while any other thread is
     *               using the font.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_OpenFont
     * \sa TTF_OpenFontIO
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_CloseFont(Font* font);

    public static void CloseFont(Font* font) {
        if (font == null) {
            throw new ArgumentNullException(nameof(font), "Font cannot be null.");
        }
        TTF_CloseFont(font);
    }

    /**
     * Deinitialize SDL_ttf.
     *
     * You must call this when done with the library, to free internal resources.
     * It is safe to call this when the library isn't initialized, as it will just
     * return immediately.
     *
     * Once you have as many quit calls as you have had successful calls to
     * TTF_Init, the library will actually deinitialize.
     *
     * Please note that this does not automatically close any fonts that are still
     * open at the time of deinitialization, and it is possibly not safe to close
     * them afterwards, as parts of the library will no longer be initialized to
     * deal with it. A well-written program should call TTF_CloseFont() on any
     * open fonts before calling this function!
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_Quit();

    public static void Quit() {
        TTF_Quit();
    }

    /**
     * Check if SDL_ttf is initialized.
     *
     * This reports the number of times the library has been initialized by a call
     * to TTF_Init(), without a paired deinitialization request from TTF_Quit().
     *
     * In short: if it's greater than zero, the library is currently initialized
     * and ready to work. If zero, it is not initialized.
     *
     * Despite the return value being a signed integer, this function should not
     * return a negative number.
     *
     * \returns the current number of initialization calls, that need to
     *          eventually be paired with this many calls to TTF_Quit().
     *
     * \threadsafety It is safe to call this function from any thread.
     *
     * \since This function is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_Init
     * \sa TTF_Quit
     */
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_WasInit();

    public static int WasInit() {
        return TTF_WasInit();
    }
}