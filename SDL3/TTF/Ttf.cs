using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SharpSDL3.TTF;

public static unsafe partial class Ttf {
    /**
     * Printable format: "%d.%d.%d", MAJOR, MINOR, MICRO
     */
    public const int Major = 3;
    public const int Micro = 2;
    public const int Minor = 2;
    public const string TTF_PROP_FONT_CREATE_EXISTING_FONT = "ttf.font.create.existing_font";
    public const string TTF_PROP_FONT_CREATE_FACE_NUMBER = "ttf.font.create.face";
    public const string TTF_PROP_FONT_CREATE_FILENAME_STRING = "ttf.font.create.filename";
    public const string TTF_PROP_FONT_CREATE_HORIZONTAL_DPI_NUMBER = "ttf.font.create.hdpi";
    public const string TTF_PROP_FONT_CREATE_IOSTREAM_AUTOCLOSE_BOOLEAN = "ttf.font.create.iostream.autoclose";
    public const string TTF_PROP_FONT_CREATE_IOSTREAM_OFFSET_NUMBER = "ttf.font.create.iostream.offset";
    public const string TTF_PROP_FONT_CREATE_IOSTREAM_POINTER = "ttf.font.create.iostream";
    public const string TTF_PROP_FONT_CREATE_SIZE_FLOAT = "ttf.font.create.size";
    public const string TTF_PROP_FONT_CREATE_VERTICAL_DPI_NUMBER = "ttf.font.create.vdpi";
    public const string TTF_PROP_FONT_OUTLINE_LINE_CAP_NUMBER = "ttf.font.outline.line_cap";
    public const string TTF_PROP_FONT_OUTLINE_LINE_JOIN_NUMBER = "ttf.font.outline.line_join";
    public const string TTF_PROP_FONT_OUTLINE_MITER_LIMIT_NUMBER = "ttf.font.outline.miter_limit";
    public const string TTF_PROP_GPU_TEXT_ENGINE_ATLAS_TEXTURE_SIZE = "ttf.gpu_text_engine.create.atlas_texture_size";
    public const string TTF_PROP_GPU_TEXT_ENGINE_DEVICE = "ttf.gpu_text_engine.create.device";
    public const string TTF_PROP_RENDERER_TEXT_ENGINE_ATLAS_TEXTURE_SIZE = "ttf.renderer_text_engine.create.atlas_texture_size";
    public const string TTF_PROP_RENDERER_TEXT_ENGINE_RENDERER = "ttf.renderer_text_engine.create.renderer";
    private const int MaxUnicodeCodePoint = 0x10FFFF;
    private const string NativeLibName = "SDL3_ttf";

    /// <summary>Add a fallback font.</summary>

    /// <param name="font">the font to modify.</param>
    /// <param name="fallback">the font to add as a fallback.</param>
    /// <remarks>
    /// Add a font that will be used for glyphs that are not in the current font.
    /// The fallback font should have the same size and style as the current font.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created both fonts.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="ClearFallbackFonts"/>
    /// <seealso cref="RemoveFallbackFont"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static bool AddFallbackFont(Font font, Font fallback) {
        bool result = TTF_AddFallbackFont(font.Handle, fallback.Handle);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to add fallback font. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>Append UTF-8 text to a text object.</summary>

    /// <param name="text">the <see cref="Text"/> to modify.</param>
    /// <param name="string">the UTF-8 text to insert.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for null terminated text.</param>
    /// <remarks>
    /// This function may cause the internal text representation to be rebuilt.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="DeleteTextString"/>
    /// <seealso cref="InsertTextString"/>
    /// <seealso cref="SetTextString"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static bool AppendTextString(Text text, string str, ulong length) {
        ArgumentException.ThrowIfNullOrEmpty(str);

        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }

        return TTF_AppendTextString(text.Handle, str, length);
    }

    /// <summary>Append UTF-8 text to a text object.</summary>

    /// <param name="text">the <see cref="Text"/> to modify.</param>
    /// <param name="string">the UTF-8 text to insert.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for null terminated text.</param>
    /// <remarks>
    /// This function may cause the internal text representation to be rebuilt.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="DeleteTextString"/>
    /// <seealso cref="InsertTextString"/>
    /// <seealso cref="SetTextString"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static bool AppendTextString(Text text, string str) {
        return AppendTextString(text, str, (ulong)str.Length);
    }

    /// <summary>Remove all fallback fonts.</summary>

    /// <param name="font">the font to modify.</param>
    /// <remarks>
    /// This updates any TTF_Text objects using this font.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="AddFallbackFont"/>
    /// <seealso cref="RemoveFallbackFont"/>
    /// </remarks>

    public static void ClearFallbackFonts(Font font) {
        TTF_ClearFallbackFonts(font.Handle);
    }

    /// <summary>Dispose of a previously-created font.</summary>

    /// <param name="font">the font to dispose of.</param>
    /// <remarks>
    /// Call this when done with a font. This function will free any resources
    /// associated with it. It is safe to call this function on <see langword="null" />, for example
    /// on the result of a failed call to TTF_OpenFont().
    /// <para><strong>Thread Safety:</strong> This function should not be called while any other thread is using thefont.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="OpenFont"/>
    /// <seealso cref="OpenFontIO"/>
    /// </remarks>

    public static void CloseFont(Font font) {
        TTF_CloseFont(font.Handle);
    }

    /// <summary>Create a copy of an existing font.</summary>

    /// <param name="existing_font">the font to copy.</param>
    /// <remarks>
    /// The copy will be distinct from the original, but will share the font file
    /// and have the same size and style as the original.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the originalfont.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="CloseFont"/>
    /// </remarks>
    /// <returns>(TTF_Font *) Returns a valid TTF_Font, or <see langword="null" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static Font CopyFont(Font existingFont) {
        Font font = TTF_CopyFont(existingFont.Handle);
        return font;
    }

    /// <summary>Create a text engine for drawing text with the SDL GPU API.</summary>

    /// <param name="device">the SDL_GPUDevice to use for creating textures and drawing text.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the device.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="CreateGPUTextEngineWithProperties"/>
    /// <seealso cref="DestroyGPUTextEngine"/>
    /// <seealso cref="GetGPUTextDrawData"/>
    /// </remarks>
    /// <returns>(TTF_TextEngine *) Returns aTTF_TextEngine object or <see langword="null" /> on failure; call<see cref="Sdl.GetError()" /> for more information.</returns>

    public static TextEngine CreateGPUTextEngine(nint device) {
        if (device == nint.Zero) {
            throw new ArgumentNullException(nameof(device), "Device cannot be null.");
        }
        nint tePtr = TTF_CreateGPUTextEngine(device);
        if (tePtr == nint.Zero) {
            throw new InvalidOperationException($"Failed to create renderer text engine. SDL Error: {Sdl.GetError()}");
        }
        TextEngine engine = *(TextEngine*)tePtr;
        engine.Handle = tePtr;
        return engine;
    }

    /// <summary>Create a text engine for drawing text with the SDL GPU API, with the specified properties.</summary>

    /// <param name="props">the properties to use.</param>
    /// <remarks>
    /// These are the supported properties:
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the device.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="CreateGPUTextEngine"/>
    /// <seealso cref="DestroyGPUTextEngine"/>
    /// <seealso cref="GetGPUTextDrawData"/>
    /// </remarks>
    /// <returns>(TTF_TextEngine *) Returns aTTF_TextEngine object or <see langword="null" /> on failure; call<see cref="Sdl.GetError()" /> for more information.</returns>

    public static nint CreateGPUTextEngineWithProperties(int props) {
        if (props == 0) {
            throw new ArgumentNullException(nameof(props), "Properties cannot be null.");
        }
        return TTF_CreateGPUTextEngineWithProperties(props);
    }

    /// <summary>Create a text engine for drawing text on an SDL renderer.</summary>

    /// <param name="renderer">the renderer to use for creating textures and drawing text.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the renderer.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="DestroyRendererTextEngine"/>
    /// <seealso cref="DrawRendererText"/>
    /// <seealso cref="CreateRendererTextEngineWithProperties"/>
    /// </remarks>
    /// <returns>(TTF_TextEngine *) Returns aTTF_TextEngine object or <see langword="null" /> on failure; call<see cref="Sdl.GetError()" /> for more information.</returns>

    public static unsafe TextEngine CreateRendererTextEngine(nint renderer) {
        if (renderer == IntPtr.Zero) {
            throw new ArgumentNullException(nameof(renderer), "Renderer cannot be null.");
        }
        nint tePtr = TTF_CreateRendererTextEngine(renderer);
        if (tePtr == nint.Zero) {
            throw new InvalidOperationException($"Failed to create renderer text engine. SDL Error: {Sdl.GetError()}");
        }
        TextEngine engine = *(TextEngine*)tePtr;
        engine.Handle = tePtr;
        return engine;
    }

    /// <summary>Create a text engine for drawing text on an SDL renderer, with the specified properties.</summary>

    /// <param name="props">the properties to use.</param>
    /// <remarks>
    /// These are the supported properties:
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the renderer.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="CreateRendererTextEngine"/>
    /// <seealso cref="DestroyRendererTextEngine"/>
    /// <seealso cref="DrawRendererText"/>
    /// </remarks>
    /// <returns>(TTF_TextEngine *) Returns aTTF_TextEngine object or <see langword="null" /> on failure; call<see cref="Sdl.GetError()" /> for more information.</returns>

    public static unsafe TextEngine CreateRendererTextEngineWithProperties(int props) {
        if (props == 0) {
            throw new ArgumentNullException(nameof(props), "Properties cannot be null.");
        }
        nint tePtr = TTF_CreateRendererTextEngineWithProperties(props);
        if (tePtr == nint.Zero) {
            throw new InvalidOperationException($"Failed to create renderer text engine. SDL Error: {Sdl.GetError()}");
        }

        TextEngine engine = *(TextEngine*)tePtr;
        engine.Handle = tePtr;

        return engine;
    }

    /// <summary>Create a text engine for drawing text on SDL surfaces.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="DestroySurfaceTextEngine"/>
    /// <seealso cref="DrawSurfaceText"/>
    /// </remarks>
    /// <returns>(TTF_TextEngine *) Returns aTTF_TextEngine object or <see langword="null" /> on failure; call<see cref="Sdl.GetError()" /> for more information.</returns>

    public static unsafe TextEngine CreateSurfaceTextEngine() {
        nint tePtr = TTF_CreateSurfaceTextEngine();
        if (tePtr == nint.Zero) {
            throw new InvalidOperationException($"Failed to create surface text engine. SDL Error: {Sdl.GetError()}");
        }
        TextEngine engine = *(TextEngine*)tePtr;
        engine.Handle = tePtr;
        return engine;
    }

    /// <seealso cref="DestroyText(Text)"/>
    /// <summary>Create a text object from UTF-8 text and a text engine.</summary>

    /// <param name="engine">the text engine to use when creating the text object, may be discarded.</param>
    /// <param name="font">the font to render with.</param>
    /// <param name="text">the text to use, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for null terminated text.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font and textengine.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="DestroyText"/>
    /// </remarks>
    /// <returns>(TTF_Text *) Returns a TTF_Text object or <see langword="null" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static Text CreateText(TextEngine engine, Font font, string text, int length) {
        ArgumentException.ThrowIfNullOrEmpty(text);
        nint tPtr = TTF_CreateText(engine.Handle, font.Handle, text, (nuint)length);
        if (tPtr == nint.Zero) {
            throw new InvalidOperationException($"Failed to create text. SDL Error: {Sdl.GetError()}");
        }
        Text textObj = *(Text*)tPtr;
        textObj.Handle = tPtr;
        return textObj;
    }

    /// <summary>Create a text object from UTF-8 text and a text engine.</summary>

    /// <param name="engine">the text engine to use when creating the text object, may be discarded.</param>
    /// <param name="font">the font to render with.</param>
    /// <param name="text">the text to use, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for null terminated text.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font and textengine.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="DestroyText"/>
    /// </remarks>
    /// <returns>(TTF_Text *) Returns a TTF_Text object or <see langword="null" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static Text CreateText(TextEngine engine, Font font, string text) {
        return CreateText(engine, font, text, text.Length);
    }

    /// <summary>Delete UTF-8 text from a text object.</summary>

    /// <param name="text">the <see cref="Text"/> to modify.</param>
    /// <param name="offset">the offset, in bytes, from the beginning of the string if &gt;= 0, the offset from the end of the string if &lt; 0. Note that this does not do UTF-8 validation, so you should only delete at UTF-8 sequence boundaries.</param>
    /// <param name="length">the length of text to delete, in bytes, or -1 for the remainder of the string.</param>
    /// <remarks>
    /// This function may cause the internal text representation to be rebuilt.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="AppendTextString"/>
    /// <seealso cref="InsertTextString"/>
    /// <seealso cref="SetTextString"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static bool DeleteTextString(Text text, int offset, int length) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_DeleteTextString(text.Handle, offset, length);
    }

    /// <summary>Destroy a text engine created for drawing text with the SDL GPU API.</summary>

    /// <param name="engine">a TTF_TextEngine object created with TTF_CreateGPUTextEngine().</param>
    /// <remarks>
    /// All text created by this engine should be destroyed before calling this
    /// function.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the engine.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="CreateGPUTextEngine"/>
    /// </remarks>

    public static void DestroyGPUTextEngine(nint engine) {
        if (engine == nint.Zero) {
            throw new ArgumentNullException(nameof(engine), "Engine cannot be null.");
        }
        TTF_DestroyGPUTextEngine(engine);
    }

    /// <summary>Destroy a text engine created for drawing text on an SDL renderer.</summary>

    /// <param name="engine">a TTF_TextEngine object created with TTF_CreateRendererTextEngine().</param>
    /// <remarks>
    /// All text created by this engine should be destroyed before calling this
    /// function.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the engine.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="CreateRendererTextEngine"/>
    /// </remarks>

    public static void DestroyRendererTextEngine(TextEngine engine) {
        if (engine.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(engine), "Engine cannot be null.");
        }
        TTF_DestroyRendererTextEngine(engine.Handle);
    }

    /// <summary>Destroy a text engine created for drawing text on SDL surfaces.</summary>

    /// <param name="engine">a TTF_TextEngine object created with TTF_CreateSurfaceTextEngine().</param>
    /// <remarks>
    /// All text created by this engine should be destroyed before calling this
    /// function.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the engine.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="CreateSurfaceTextEngine"/>
    /// </remarks>

    public static void DestroySurfaceTextEngine(TextEngine engine) {
        if (engine.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(engine), "Engine cannot be null.");
        }
        TTF_DestroySurfaceTextEngine(engine.Handle);
    }

    /// <summary>Destroy a text object created by a text engine.</summary>

    /// <param name="text">the text to destroy.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="CreateText"/>
    /// </remarks>

    public static void DestroyText(Text text) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        TTF_DestroyText(text.Handle);
    }

    /// <summary>Draw text to an SDL renderer.</summary>

    /// <param name="text">the text to draw.</param>
    /// <param name="x">the x coordinate in pixels, positive from the left edge towards the right.</param>
    /// <param name="y">the y coordinate in pixels, positive from the top edge towards the bottom.</param>
    /// <remarks>
    /// text must have been created using a TTF_TextEngine from
    /// TTF_CreateRendererTextEngine(), and will
    /// draw using the renderer passed to that function.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="CreateRendererTextEngine"/>
    /// <seealso cref="CreateText"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static bool DrawRendererText(Text text, float x, float y) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_DrawRendererText(text.Handle, x, y);
    }

    /// <seealso cref="CreateSurfaceTextEngine()"/>
    /// <summary>Draw text to an SDL surface.</summary>

    /// <param name="text">the text to draw.</param>
    /// <param name="x">the x coordinate in pixels, positive from the left edge towards the right.</param>
    /// <param name="y">the y coordinate in pixels, positive from the top edge towards the bottom.</param>
    /// <param name="surface">the surface to draw on.</param>
    /// <remarks>
    /// text must have been created using a TTF_TextEngine from
    /// TTF_CreateSurfaceTextEngine().
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="CreateSurfaceTextEngine"/>
    /// <seealso cref="CreateText"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static bool DrawSurfaceText(Text text, int x, int y, nint surface) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        if (surface == nint.Zero) {
            throw new ArgumentNullException(nameof(surface), "Surface cannot be null.");
        }
        return TTF_DrawSurfaceText(text.Handle, x, y, surface);
    }

    /// <summary>Check whether a glyph is provided by the font for a UNICODE codepoint.</summary>

    /// <param name="font">the font to query.</param>
    /// <param name="ch">the codepoint to check.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if font provides a glyph for this character, <see langword="false" /> ifnot.</returns>

    public static bool FontHasGlyph(Font font, int ch) {
        bool result = TTF_FontHasGlyph(font.Handle, ch);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to check if font has glyph for character {ch}. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>Query the offset from the baseline to the top of a font.</summary>

    /// <param name="font">the font to query.</param>
    /// <remarks>
    /// This is a positive value, relative to the baseline.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the font's ascent.</returns>

    public static int GetFontAscent(Font font) {
        int result = TTF_GetFontAscent(font.Handle);
        if (result < 0) {
            Sdl.LogError(LogCategory.Error, $"Failed to get font ascent. SDL Error: {Sdl.GetError()}");
            return 0;
        }
        return result;
    }

    /// <summary>Query the offset from the baseline to the bottom of a font.</summary>

    /// <param name="font">the font to query.</param>
    /// <remarks>
    /// This is a negative value, relative to the baseline.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the font's descent.</returns>

    public static int GetFontDescent(Font font) {
        return TTF_GetFontDescent(font.Handle);
    }

    /// <summary>Get the direction to be used for text shaping by a font.</summary>

    /// <param name="font">the font to query.</param>
    /// <remarks>
    /// This defaults to TTF_DIRECTION_INVALID if it
    /// hasn't been set.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the direction to be used for textshaping.</returns>

    public static Direction GetFontDirection(Font font) {
        return TTF_GetFontDirection(font.Handle);
    }

    public static bool GetFontDPI(Font font, out int hdpi, out int vdpi) {
        nint pHdpi = Sdl.Malloc(4);
        nint pVdpi = Sdl.Malloc(4);

        bool result = TTF_GetFontDPI(font.Handle, pHdpi, pVdpi);

        try {
            if (!result) {
                Sdl.LogError(LogCategory.Error, $"Failed to get font DPI. SDL Error: {Sdl.GetError()}");
                hdpi = 0;
                vdpi = 0;
                return false;
            }

            hdpi = Marshal.ReadInt32(pHdpi);
            vdpi = Marshal.ReadInt32(pVdpi);
            return result;
        } finally {
            Sdl.Free(pHdpi);
            Sdl.Free(pVdpi);
        }
    }

    /// <summary>Get font target resolutions, in dots per inch.</summary>

    /// <param name="font">the font to query.</param>
    /// <param name="hdpi">a pointer filled in with the target horizontal DPI.</param>
    /// <param name="vdpi">a pointer filled in with the target vertical DPI.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="SetFontSizeDPI"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static Size GetFontDPI(Font font) {
        if (GetFontDPI(font, out int hdpi, out int vdpi)) {
            return new Size(hdpi, vdpi);
        }

        return new();
    }

    /// <summary>Query a font's family name.</summary>

    /// <param name="font">the font to query.</param>
    /// <remarks>
    /// This string is dictated by the contents of the font file.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the font's family name.</returns>

    public static string GetFontFamilyName(Font font) {
        string result = TTF_GetFontFamilyName(font.Handle);
        if (string.IsNullOrEmpty(result)) {
            Sdl.LogError(LogCategory.System, $"Failed to get font family name. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>Get the font generation.</summary>

    /// <param name="font">the font to query.</param>
    /// <remarks>
    /// The generation is incremented each time font properties change that require
    /// rebuilding glyphs, such as style, size, etc.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the font generation or 0 on failure; call <see cref="Sdl.GetError()" />for more information.</returns>

    public static int GetFontGeneration(Font font) {
        int generation = TTF_GetFontGeneration(font.Handle);
        if (generation == 0) {
            throw new InvalidOperationException($"Failed to get font generation. SDL Error: {Sdl.GetError()}");
        }
        return generation;
    }

    /// <summary>Query the total height of a font.</summary>

    /// <param name="font">the font to query.</param>
    /// <remarks>
    /// This is usually equal to point size.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the font's height.</returns>

    public static int GetFontHeight(Font font) {
        int result = TTF_GetFontHeight(font.Handle);
        if (result < 0) {
            Sdl.LogError(LogCategory.Error, $"Failed to get font height. SDL Error: {Sdl.GetError()}");
            return 0;
        }
        return result;
    }

    /// <summary>Query a font's current FreeType hinter setting.</summary>

    /// <param name="font">the font to query.</param>
    /// <remarks>
    /// The hinter setting is a single value:
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="SetFontHinting"/>
    /// </remarks>
    /// <returns>Returns the font's current hintervalue, or TTF_HINTING_INVALID if the font isinvalid.</returns>

    public static Hinting GetFontHinting(Font font) {
        return TTF_GetFontHinting(font.Handle);
    }

    /// <summary>Query whether or not kerning is enabled for a font.</summary>

    /// <param name="font">the font to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="SetFontKerning"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if kerning is enabled, <see langword="false" /> otherwise.</returns>

    public static bool GetFontKerning(Font font) {
        bool result = TTF_GetFontKerning(font.Handle);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to get font kerning. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>Query the spacing between lines of text for a font.</summary>

    /// <param name="font">the font to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="SetFontLineSkip"/>
    /// </remarks>
    /// <returns>Returns the font's recommended spacing.</returns>

    public static int GetFontLineSkip(Font font) {
        return TTF_GetFontLineSkip(font.Handle);
    }

    /// <summary>Query a font's current outline.</summary>

    /// <param name="font">the font to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="SetFontOutline"/>
    /// </remarks>
    /// <returns>Returns the font's current outline value.</returns>

    public static int GetFontOutline(Font font) {
        int result = TTF_GetFontOutline(font.Handle);
        if (result < 0) {
            Sdl.LogError(LogCategory.Error, $"Failed to get font outline. SDL Error: {Sdl.GetError()}");
            return 0;
        }
        return result;
    }

    /// <summary>Get the properties associated with a font.</summary>

    /// <param name="font">the font to query.</param>
    /// <remarks>
    /// The following read-write properties are provided by SDL:
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns a valid property ID on success or 0 on failure;call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static int GetFontProperties(Font font) {
        int props = TTF_GetFontProperties(font.Handle);
        if (props == 0) {
            throw new InvalidOperationException($"Failed to get font properties. SDL Error: {Sdl.GetError()}");
        }
        return props;
    }

    /// <summary>Get the script used for text shaping a font.</summary>

    /// <param name="font">the font to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="TagToString"/>
    /// </remarks>
    /// <returns>Returns anISO 15924 codeor 0 if a script hasn't been set.</returns>

    public static int GetFontScript(Font font) {
        return TTF_GetFontScript(font.Handle);
    }

    /// <summary>Query whether Signed Distance Field rendering is enabled for a font.</summary>

    /// <param name="font">the font to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="SetFontSDF"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if enabled, <see langword="false" /> otherwise.</returns>

    public static bool GetFontSDF(Font font) {
        bool result = TTF_GetFontSDF(font.Handle);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to get font SDF. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>Get the size of a font.</summary>

    /// <param name="font">the font to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="SetFontSize"/>
    /// <seealso cref="SetFontSizeDPI"/>
    /// </remarks>
    /// <returns>Returns the size of the font, or 0.0f on failure; call<see cref="Sdl.GetError()" /> for more information.</returns>

    public static float GetFontSize(Font font) {
        float size = TTF_GetFontSize(font.Handle);
        if (size <= 0.01f) {
            throw new InvalidOperationException($"Failed to get font size. SDL Error: {Sdl.GetError()}");
        }
        return size;
    }

    /// <summary>Query a font's current style.</summary>

    /// <param name="font">the font to query.</param>
    /// <remarks>
    /// The font styles are a set of bit flags, OR'd together:
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="SetFontStyle"/>
    /// </remarks>
    /// <returns>Returns the current font style,as a set of bit flags.</returns>

    public static FontStyle GetFontStyle(Font font) {
        return TTF_GetFontStyle(font.Handle);
    }

    /// <summary>Query a font's style name.</summary>

    /// <param name="font">the font to query.</param>
    /// <remarks>
    /// This string is dictated by the contents of the font file.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the font's style name.</returns>

    public static string GetFontStyleName(Font font) {
        string result = TTF_GetFontStyleName(font.Handle);
        if (string.IsNullOrEmpty(result)) {
            Sdl.LogError(LogCategory.System, $"Failed to get font style name. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>Query a font's weight, in terms of the lightness/heaviness of the strokes.</summary>

    /// <param name="font">the font to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.2.2.</para>
    /// </remarks>
    /// <returns>Returns the font's current weight.</returns>

    public static FontWeight GetFontWeight(Font font) {
        return TTF_GetFontWeight(font.Handle);
    }

    /// <summary>Query a font's current wrap alignment option.</summary>

    /// <param name="font">the font to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="SetFontWrapAlignment"/>
    /// </remarks>
    /// <returns>Returns the font'scurrent wrap alignment option.</returns>

    public static HorizontalAlignment GetFontWrapAlignment(Font font) {
        return TTF_GetFontWrapAlignment(font.Handle);
    }

    /// <summary>Query the version of the FreeType library in use.</summary>

    /// <param name="major">to be filled in with the major version number. Can be <see langword="null" />.</param>
    /// <param name="minor">to be filled in with the minor version number. Can be <see langword="null" />.</param>
    /// <param name="patch">to be filled in with the param version number. Can be <see langword="null" />.</param>
    /// <remarks>
    /// TTF_Init() should be called before calling this function.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="Init"/>
    /// </remarks>

    public static void GetFreeTypeVersion(out int major, out int minor, out int patch) {
        nint ma = Sdl.Malloc(4);
        nint mi = Sdl.Malloc(4);
        nint pa = Sdl.Malloc(4);

        TTF_GetFreeTypeVersion(ma, mi, pa);
        major = Marshal.ReadInt32(ma);
        minor = Marshal.ReadInt32(mi);
        patch = Marshal.ReadInt32(pa);
        if (major == 0 && minor == 0 && patch == 0) {
            Sdl.LogError(LogCategory.Error, $"Failed to get FreeType version. SDL Error: {Sdl.GetError()}");
        }
        Sdl.Free(ma);
        Sdl.Free(mi);
        Sdl.Free(pa);
    }

    /// <summary>Get the pixel image for a UNICODE codepoint.</summary>

    /// <param name="font">the font to query.</param>
    /// <param name="ch">the codepoint to check.</param>
    /// <param name="image_type">a pointer filled in with the glyph image type, may be discarded.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>(SDL_Surface *) Returns an SDL_Surface containing the glyph, or <see langword="null" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static nint GetGlyphImage(Font font, int ch, ImageType image_type) {
        if (!Enum.IsDefined(image_type)) {
            throw new ArgumentOutOfRangeException(nameof(image_type), "Invalid image type value.");
        }
        return TTF_GetGlyphImage(font.Handle, ch, image_type);
    }

    /// <summary>Get the pixel image for a character index.</summary>

    /// <param name="font">the font to query.</param>
    /// <param name="glyph_index">the index of the glyph to return.</param>
    /// <param name="image_type">a pointer filled in with the glyph image type, may be discarded.</param>
    /// <remarks>
    /// This is useful for text engine implementations, which can call this with
    /// the glyph_index in a TTF_CopyOperation
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>(SDL_Surface *) Returns an SDL_Surface containing the glyph, or <see langword="null" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static nint GetGlyphImageForIndex(Font font, int glyph_index, ImageType image_type) {
        if (!Enum.IsDefined(image_type)) {
            throw new ArgumentOutOfRangeException(nameof(image_type), "Invalid image type value.");
        }
        return TTF_GetGlyphImageForIndex(font.Handle, glyph_index, image_type);
    }

    /// <summary>Query the kerning size between the glyphs of two UNICODE codepoints.</summary>

    /// <param name="font">the font to query.</param>
    /// <param name="previous_ch">the previous codepoint.</param>
    /// <param name="ch">the current codepoint.</param>
    /// <param name="kerning">a pointer filled in with the kerning size between the two glyphs, in pixels, may be discarded.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static bool GetGlyphKerning(Font font, int previous_ch, int ch, int kerning) {
        bool result = TTF_GetGlyphKerning(font.Handle, previous_ch, ch, kerning);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to get glyph kerning for characters {previous_ch} and {ch}. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>Query the metrics (dimensions) of a font's glyph for a UNICODE codepoint.</summary>

    /// <param name="font">the font to query.</param>
    /// <param name="ch">the codepoint to check.</param>
    /// <param name="minx">a pointer filled in with the minimum x coordinate of the glyph from the left edge of its bounding box. This value may be negative.</param>
    /// <param name="maxx">a pointer filled in with the maximum x coordinate of the glyph from the left edge of its bounding box.</param>
    /// <param name="miny">a pointer filled in with the minimum y coordinate of the glyph from the bottom edge of its bounding box. This value may be negative.</param>
    /// <param name="maxy">a pointer filled in with the maximum y coordinate of the glyph from the bottom edge of its bounding box.</param>
    /// <param name="advance">a pointer filled in with the distance to the next glyph from the left edge of this glyph's bounding box.</param>
    /// <remarks>
    /// To understand what these metrics mean, here is a useful link:
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static bool GetGlyphMetrics(Font font, int ch, nint minx, nint maxx, nint miny, nint maxy, nint advance) {
        bool result = TTF_GetGlyphMetrics(font.Handle, ch, minx, maxx, miny, maxy, advance);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to get glyph metrics for character {ch}. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>Get the script used by a 32-bit codepoint.</summary>

    /// <param name="ch">the character code to check.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function is thread-safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="TagToString"/>
    /// </remarks>
    /// <returns>Returns anISO 15924 codeon success, or 0 on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static int GetGlyphScript(int ch) {
        if (ch < 0 || ch > MaxUnicodeCodePoint) {
            throw new ArgumentOutOfRangeException(nameof(ch), "The character code must be a valid Unicode code point.");
        }

        int script = TTF_GetGlyphScript(ch);

        if (script == 0) {
            Sdl.LogError(LogCategory.Error, $"Failed to get glyph script for character {ch}. SDL Error: {Sdl.GetError()}");
        }

        return script;
    }

    /// <summary>Get the geometry data needed for drawing the text.</summary>

    /// <param name="text">the text to draw.</param>
    /// <remarks>
    /// text must have been created using a TTF_TextEngine from
    /// TTF_CreateGPUTextEngine().
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="CreateGPUTextEngine"/>
    /// <seealso cref="CreateText"/>
    /// </remarks>
    /// <returns>(TTF_GPUAtlasDrawSequence *) Returns a <see langword="null" />terminated linked list ofTTF_GPUAtlasDrawSequence objects or <see langword="null" /> if thepassed text is empty or in case of failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static nint GetGPUTextDrawData(nint text) {
        if (text == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_GetGPUTextDrawData(text);
    }

    /// <summary>Get the winding order of the vertices returned by TTF_GetGPUTextDrawData for a particular GPU text engine</summary>

    /// <param name="engine">a TTF_TextEngine object created with TTF_CreateGPUTextEngine().</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the engine.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="SetGPUTextEngineWinding"/>
    /// </remarks>
    /// <returns>Returns the windingorder used by the GPU text engine orTTF_GPU_TEXTENGINE_WINDING_INVALID incase of error.</returns>

    public static GPUTextEngineWinding GetGPUTextEngineWinding(nint engine) {
        if (engine == nint.Zero) {
            throw new ArgumentNullException(nameof(engine), "Engine cannot be null.");
        }
        return TTF_GetGPUTextEngineWinding(engine);
    }

    /// <summary>Query the version of the HarfBuzz library in use.</summary>

    /// <param name="major">to be filled in with the major version number. Can be <see langword="null" />.</param>
    /// <param name="minor">to be filled in with the minor version number. Can be <see langword="null" />.</param>
    /// <param name="patch">to be filled in with the param version number. Can be <see langword="null" />.</param>
    /// <remarks>
    /// If HarfBuzz is not available, the version reported is 0.0.0.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>

    public static void GetHarfBuzzVersion(out int major, out int minor, out int patch) {
        nint ma = Sdl.Malloc(4);
        nint mi = Sdl.Malloc(4);
        nint pa = Sdl.Malloc(4);
        major = Marshal.ReadInt32(ma);
        minor = Marshal.ReadInt32(mi);
        patch = Marshal.ReadInt32(pa);
        TTF_GetHarfBuzzVersion(ma, mi, pa);
        if (major == 0 && minor == 0 && patch == 0) {
            Sdl.LogError(LogCategory.Error, $"Failed to get FreeType HarfBuzz version. SDL Error: {Sdl.GetError()}");
        }
        Sdl.Free(ma);
        Sdl.Free(mi);
        Sdl.Free(pa);
    }

    /// <summary>Get the next substring in a text object</summary>

    /// <param name="text">the <see cref="Text"/> to query.</param>
    /// <param name="substring">the TTF_SubString to query.</param>
    /// <param name="next">a pointer filled in with the next substring.</param>
    /// <remarks>
    /// If called at the end of the text, this will return a zero length substring
    /// with the TTF_SUBSTRING_TEXT_END flag set.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    public static unsafe bool GetNextTextSubString(Text text, SubString substring, out SubString next) {
        if (text.Handle == nint.Zero) {
            Sdl.LogError(LogCategory.Error, "Text cannot be null.");
            next = new();
            return false;
        }

        if (substring.Handle == nint.Zero) {
            Sdl.LogError(LogCategory.Error, "Substring cannot be null.");
            next = new();
            return false;
        }
        nint pNext = Sdl.Malloc(Sdl.SizeOf<SubString>());
        try {
            bool result = TTF_GetNextTextSubString(text.Handle, substring.Handle, pNext);

            if (!result) {
                Sdl.LogError(LogCategory.Error, "Failed to get next text substring.");
            }

            next = *(SubString*)pNext;

            return result;
        } finally {
            Sdl.Free(pNext);
        }
    }

    /// <summary>Get the next substring in a text object</summary>

    /// <param name="text">the <see cref="Text"/> to query.</param>
    /// <param name="substring">the TTF_SubString to query.</param>
    /// <remarks>
    /// If called at the end of the text, this will return a zero length substring
    /// with the TTF_SUBSTRING_TEXT_END flag set.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static SubString GetNextTextSubString(Text text, SubString substring) {
        _ = GetNextTextSubString(text, substring, out SubString next);
        return next;
    }

    /// <summary>Query the number of faces of a font.</summary>

    /// <param name="font">the font to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the number of FreeType font faces.</returns>

    public static int GetNumFontFaces(Font font) {
        int result = TTF_GetNumFontFaces(font.Handle);
        if (result < 0) {
            Sdl.LogError(LogCategory.Error, $"Failed to get number of font faces. SDL Error: {Sdl.GetError()}");
            return 0;
        }
        return result;
    }

    /// <summary>Get the previous substring in a text object</summary>

    /// <param name="text">the <see cref="Text"/> to query.</param>
    /// <param name="substring">the TTF_SubString to query.</param>
    /// <param name="previous">a pointer filled in with the previous substring in the text object.</param>
    /// <remarks>
    /// If called at the start of the text, this will return a zero length
    /// substring with the TTF_SUBSTRING_TEXT_START
    /// flag set.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    public static unsafe bool GetPreviousTextSubString(Text text, SubString substring, out SubString previous) {
        if (text.Handle == nint.Zero) {
            Sdl.LogError(LogCategory.Error, "Text cannot be null.");
            previous = new();
            return false;
        }

        if (substring.Handle == nint.Zero) {
            Sdl.LogError(LogCategory.Error, "Substring cannot be null.");
            previous = new();
            return false;
        }

        nint pPrevious = Sdl.Malloc(Sdl.SizeOf<SubString>());
        try {
            bool result = TTF_GetPreviousTextSubString(text.Handle, substring.Handle, pPrevious);
            if (!result) {
                Sdl.LogError(LogCategory.Error, "Failed to get previous text substring.");
            }
            previous = *(SubString*)pPrevious;
            return result;
        } finally {
            Sdl.Free(pPrevious);
        }
    }

    /// <summary>Get the previous substring in a text object</summary>

    /// <param name="text">the <see cref="Text"/> to query.</param>
    /// <param name="substring">the TTF_SubString to query.</param>
    /// <remarks>
    /// If called at the start of the text, this will return a zero length
    /// substring with the TTF_SUBSTRING_TEXT_START
    /// flag set.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static SubString GetPreviousTextSubString(Text text, SubString substring) {
        _ = GetPreviousTextSubString(text, substring, out SubString previous);
        return previous;
    }

    /// <summary>Calculate the dimensions of a rendered string of UTF-8 text.</summary>
    /// <param name="font">the font to query.</param>
    /// <param name="text">text to calculate, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for null terminated text.</param>
    /// <param name="w">will be filled with width, in pixels, on return.</param>
    /// <param name="h">will be filled with height, in pixels, on return.</param>
    /// <remarks>
    /// This will report the width and height, in pixels, of the space that the
    /// specified string will take to fully render.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    public static bool GetStringSize(Font font, string text, ulong length, int w, int h) {
        bool result = TTF_GetStringSize(font.Handle, text, length, w, h);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to get string size for text '{text}'. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>Calculate the dimensions of a rendered string of UTF-8 text.</summary>
    /// <param name="font">the font to query.</param>
    /// <param name="text">text to calculate, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for null terminated text.</param>
    /// <remarks>
    /// This will report the width and height, in pixels, of the space that the
    /// specified string will take to fully render.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns
    public static bool GetStringSize(Font font, string text, ulong length, Size size) {
        return GetStringSize(font, text, length, size.Width, size.Height);
    }

    /// <summary>Calculate the dimensions of a rendered string of UTF-8 text.</summary>
    /// <param name="font">the font to query.</param>
    /// <param name="text">text to calculate, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for null terminated text.</param>
    /// <param name="wrap_width">the maximum width or 0 to wrap on newline characters.</param>
    /// <param name="w">will be filled with width, in pixels, on return.</param>
    /// <param name="h">will be filled with height, in pixels, on return.</param>
    /// <remarks>
    /// This will report the width and height, in pixels, of the space that the
    /// specified string will take to fully render.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static bool GetStringSizeWrapped(Font font, string text, ulong length, int wrap_width, int w, int h) {
        bool result = TTF_GetStringSizeWrapped(font.Handle, text, length, wrap_width, w, h);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to get wrapped string size for text '{text}'. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>Calculate the dimensions of a rendered string of UTF-8 text.</summary>

    /// <param name="font">the font to query.</param>
    /// <param name="text">text to calculate, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for null terminated text.</param>
    /// <param name="wrap_width">the maximum width or 0 to wrap on newline characters.</param>
    /// <param name="w">will be filled with width, in pixels, on return.</param>
    /// <param name="h">will be filled with height, in pixels, on return.</param>
    /// <remarks>
    /// This will report the width and height, in pixels, of the space that the
    /// specified string will take to fully render.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static bool GetStringSizeWrapped(Font font, string text, ulong length, int wrap_width, Size size) {
        return GetStringSizeWrapped(font, text, length, wrap_width, size.Width, size.Height);
    }

    public static bool GetTextColor(Text text, out byte r, out byte g, out byte b, out byte a) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        nint pr = Sdl.Malloc(1);
        nint pg = Sdl.Malloc(1);
        nint pb = Sdl.Malloc(1);
        nint pa = Sdl.Malloc(1);
        try {
            bool result = TTF_GetTextColor(text.Handle, pr, pg, pb, pa);
            if (!result) {
                Sdl.LogError(LogCategory.System, $"Failed to get text color. SDL Error: {Sdl.GetError()}");
                r = g = b = a = 0;
                return false;
            }
            r = Marshal.ReadByte(pr);
            g = Marshal.ReadByte(pg);
            b = Marshal.ReadByte(pb);
            a = Marshal.ReadByte(pa);
            return result;
        } finally {
            Sdl.Free(pr);
            Sdl.Free(pg);
            Sdl.Free(pb);
            Sdl.Free(pa);
        }
    }

    /// <summary>Get the color of a text object.</summary>

    /// <param name="text">the <see cref="Text"/> to query.</param>
    /// <param name="r">a pointer filled in with the red color value in the range of 0-255, may be discarded.</param>
    /// <param name="g">a pointer filled in with the green color value in the range of 0-255, may be discarded.</param>
    /// <param name="b">a pointer filled in with the blue color value in the range of 0-255, may be discarded.</param>
    /// <param name="a">a pointer filled in with the alpha value in the range of 0-255, may be discarded.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="GetTextColorFloat"/>
    /// <seealso cref="SetTextColor"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static Color GetTextColor(Text text) {
        GetTextColor(text, out byte r, out byte g, out byte b, out byte a);
        return new Color() { R = r, G = g, B = b, A = a };
    }

    public static unsafe bool GetTextColorFloat(Text text, out float r, out float g, out float b, out float a) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        nint pr = Sdl.Malloc(sizeof(float));
        nint pg = Sdl.Malloc(sizeof(float));
        nint pb = Sdl.Malloc(sizeof(float));
        nint pa = Sdl.Malloc(sizeof(float));
        try {
            bool result = TTF_GetTextColorFloat(text.Handle, pr, pg, pb, pa);

            if (!result) {
                Sdl.LogError(LogCategory.System, $"Failed to get text color. SDL Error: {Sdl.GetError()}");
                r = g = b = a = 0;
                return false;
            }

            r = *(float*)pr;
            g = *(float*)pg;
            b = *(float*)pb;
            a = *(float*)pa;
            return result;
        } finally {
            Sdl.Free(pr);
            Sdl.Free(pg);
            Sdl.Free(pb);
            Sdl.Free(pa);
        }
    }

    /// <summary>Get the color of a text object.</summary>

    /// <param name="text">the <see cref="Text"/> to query.</param>
    /// <param name="r">a pointer filled in with the red color value, normally in the range of 0-1, may be discarded.</param>
    /// <param name="g">a pointer filled in with the green color value, normally in the range of 0-1, may be discarded.</param>
    /// <param name="b">a pointer filled in with the blue color value, normally in the range of 0-1, may be discarded.</param>
    /// <param name="a">a pointer filled in with the alpha value in the range of 0-1, may be discarded.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="GetTextColor"/>
    /// <seealso cref="SetTextColorFloat"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static bool GetTextColorFloat(Text text, out FColor color) {
        bool result = GetTextColorFloat(text, out float r, out float g, out float b, out float a);
        color = new FColor() { R = r, G = g, B = b, A = a };
        return result;
    }

    /// <summary>Get the direction to be used for text shaping a text object.</summary>

    /// <param name="text">the text to query.</param>
    /// <remarks>
    /// This defaults to the direction of the font used by the text object.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the direction to be used for textshaping.</returns>

    public static Direction GetTextDirection(Text text) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_GetTextDirection(text.Handle);
    }

    /// <summary>Get the text engine used by a text object.</summary>

    /// <param name="text">the <see cref="Text"/> to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="SetTextEngine"/>
    /// </remarks>
    /// <returns>(TTF_TextEngine *) Returns theTTF_TextEngine used by the text on success or <see langword="null" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static unsafe TextEngine GetTextEngine(Text text) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        nint tePtr = TTF_GetTextEngine(text.Handle);
        if (tePtr == nint.Zero) {
            throw new InvalidOperationException($"Failed to get text engine. SDL Error: {Sdl.GetError()}");
        }
        TextEngine engine = *(TextEngine*)tePtr;
        engine.Handle = tePtr;
        return engine;
    }

    /// <summary>Get the font used by a text object.</summary>

    /// <param name="text">the <see cref="Text"/> to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="SetTextFont"/>
    /// </remarks>
    /// <returns>(TTF_Font *) Returns the <see cref="Font"/> used by the texton success or <see langword="null" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static Font GetTextFont(Text text) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        nint fPtr = TTF_GetTextFont(text.Handle);
        if (fPtr == nint.Zero) {
            throw new InvalidOperationException($"Failed to get text font. SDL Error: {Sdl.GetError()}");
        }
        Font font = *(Font*)fPtr;
        font.Handle = fPtr;
        return font;
    }

    public static bool GetTextPosition(Text text, out int x, out int y) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        nint px = Sdl.Malloc(sizeof(int));
        nint py = Sdl.Malloc(sizeof(int));

        bool result = TTF_GetTextPosition(text.Handle, px, py);

        x = Marshal.ReadInt32(px);
        y = Marshal.ReadInt32(py);

        Sdl.Free(px);
        Sdl.Free(py);

        return result;
    }

    /// <summary>Get the position of a text object.</summary>

    /// <param name="text">the <see cref="Text"/> to query.</param>
    /// <param name="x">a pointer filled in with the x offset of the upper left corner of this text in pixels, may be discarded.</param>
    /// <param name="y">a pointer filled in with the y offset of the upper left corner of this text in pixels, may be discarded.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="SetTextPosition"/>
    /// </remarks>

    public static Point GetTextPosition(Text text) {
        GetTextPosition(text, out int x, out int y);
        return new Point() { X = x, Y = y };
    }

    /// <summary>Get the properties associated with a text object.</summary>

    /// <param name="text">the <see cref="Text"/> to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns a valid property ID on success or 0 on failure;call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static uint GetTextProperties(nint text) {
        if (text == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_GetTextProperties(text);
    }

    /// <summary>Get the script used for text shaping a text object.</summary>

    /// <param name="text">the text to query.</param>
    /// <remarks>
    /// This defaults to the script of the font used by the text object.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="TagToString"/>
    /// </remarks>
    /// <returns>Returns anISO 15924 codeor 0 if a script hasn't been set on either the text object or the font.</returns>

    public static int GetTextScript(Text text) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_GetTextScript(text.Handle);
    }

    public static bool GetTextSize(Text text, out int w, out int h) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }

        nint pw = Sdl.Malloc(sizeof(int));
        nint ph = Sdl.Malloc(sizeof(int));

        bool result = TTF_GetTextSize(text.Handle, pw, ph);

        w = Marshal.ReadInt32(pw);
        h = Marshal.ReadInt32(ph);

        Sdl.Free(pw);
        Sdl.Free(ph);

        return result;
    }

    /// <summary>Get the size of a text object.</summary>

    /// <param name="text">the <see cref="Text"/> to query.</param>
    /// <param name="w">a pointer filled in with the width of the text, in pixels, may be discarded.</param>
    /// <param name="h">a pointer filled in with the height of the text, in pixels, may be discarded.</param>
    /// <remarks>
    /// The size of the text may change when the font or font style and size
    /// change.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static Size GetTextSize(Text text) {
        GetTextSize(text, out int w, out int h);
        return new Size() { Width = w, Height = h };
    }

    /// <summary>Get the substring of a text object that surrounds a text offset.</summary>

    /// <param name="text">the <see cref="Text"/> to query.</param>
    /// <param name="offset">a byte offset into the text string.</param>
    /// <param name="substring">a pointer filled in with the substring containing the offset.</param>
    /// <remarks>
    /// If offset is less than 0, this will return a zero length substring at the
    /// beginning of the text with the
    /// TTF_SUBSTRING_TEXT_START flag set. If offset
    /// is greater than or equal to the length of the text string, this will return
    /// a zero length substring at the end of the text with the
    /// TTF_SUBSTRING_TEXT_END flag set.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="text"/> is <see langword="null"/></exception>
    public static bool GetTextSubString(Text text, int offset, SubString substring) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_GetTextSubString(text.Handle, offset, substring.Handle);
    }

    /// <summary>Get the substring of a text object that contains the given line.</summary>
    /// <param name="text">the <see cref="Text"/> to query.</param>
    /// <param name="line">a zero-based line index, in the range [0 .. text-&gt;num_lines-1].</param>
    /// <param name="substring">a pointer filled in with the substring containing the offset.</param>
    /// <remarks>
    /// If line is less than 0, this will return a zero length substring at the
    /// beginning of the text with the
    /// TTF_SUBSTRING_TEXT_START flag set. If line is
    /// greater than or equal to text-&gt;num_lines this will return a zero length
    /// substring at the end of the text with the
    /// TTF_SUBSTRING_TEXT_END flag set.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static unsafe bool GetTextSubStringForLine(Text text, int line, out SubString substring) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        if (line < 0) {
            throw new ArgumentOutOfRangeException(nameof(line), "Line index cannot be negative.");
        }

        nint pSubstring = Sdl.Malloc(Sdl.SizeOf<SubString>());
        try {
            bool result = TTF_GetTextSubStringForLine(text.Handle, line, pSubstring);

            if (!result) {
                Sdl.LogError(LogCategory.Error, "Failed to get text substring for line.");
                substring = default;
                return false;
            }

            substring = *(SubString*)pSubstring;
            substring.Handle = pSubstring;

            return result;
        } finally {
            Sdl.Free(pSubstring);
        }
    }

    /// <summary>Get the substring of a text object that contains the given line.</summary>
    /// <param name="text">the <see cref="Text"/> to query.</param>
    /// <param name="line">a zero-based line index, in the range [0 .. text-&gt;num_lines-1].</param>
    /// <remarks>
    /// If line is less than 0, this will return a zero length substring at the
    /// beginning of the text with the
    /// TTF_SUBSTRING_TEXT_START flag set. If line is
    /// greater than or equal to text-&gt;num_lines this will return a zero length
    /// substring at the end of the text with the
    /// TTF_SUBSTRING_TEXT_END flag set.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static SubString GetTextSubStringForLine(Text text, int line) {
        bool result = GetTextSubStringForLine(text, line, out SubString substring);
        if (!result) {
            Sdl.LogError(LogCategory.Error, "Failed to get text substring for line.");
            return new SubString();
        }
        return substring;
    }

    /// <summary>Get the portion of a text string that is closest to a point.</summary>
    /// <param name="text">the <see cref="Text"/> to query.</param>
    /// <param name="x">the x coordinate relative to the left side of the text, may be outside the bounds of the text area.</param>
    /// <param name="y">the y coordinate relative to the top side of the text, may be outside the bounds of the text area.</param>
    /// <param name="substring">a pointer filled in with the closest substring of text to the given point.</param>
    /// <remarks>
    /// This will return the closest substring of text to the given point.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    public static unsafe bool GetTextSubStringForPoint(Text text, int x, int y, out SubString substring) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        nint pSubstring = Sdl.Malloc(Sdl.SizeOf<SubString>());

        bool result = TTF_GetTextSubStringForPoint(text.Handle, x, y, pSubstring);
        if (!result) {
            Sdl.LogError(LogCategory.Error, "Failed to get text substring for point.");
            substring = default;
            return false;
        }
        substring = *(SubString*)pSubstring;
        Sdl.Free(pSubstring);
        return result;
    }

    /// <summary>Get the portion of a text string that is closest to a point.</summary>
    /// <param name="text">the <see cref="Text"/> to query.</param>
    /// <param name="substring">a pointer filled in with the closest substring of text to the given point.</param>
    /// <remarks>
    /// This will return the closest substring of text to the given point.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    public static bool GetTextSubStringForPoint(Text text, Point point, out SubString substring) {
        return GetTextSubStringForPoint(text, point.X, point.Y, out substring);
    }

    /// <summary>Get the portion of a text string that is closest to a point.</summary>
    /// <param name="text">the <see cref="Text"/> to query.</param>
    /// <param name="x">the x coordinate relative to the left side of the text, may be outside the bounds of the text area.</param>
    /// <param name="y">the y coordinate relative to the top side of the text, may be outside the bounds of the text area.</param>
    /// <remarks>
    /// This will return the closest substring of text to the given point.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static SubString GetTextSubStringForPoint(Text text, int x, int y) {
        bool result = GetTextSubStringForPoint(text, x, y, out SubString substring);
        if (!result) {
            Sdl.LogError(LogCategory.Error, "Failed to get text substring for point.");
            return new SubString();
        }
        return substring;
    }

    /// <summary>Get the portion of a text string that is closest to a point.</summary>

    /// <param name="text">the <see cref="Text"/> to query.</param>
    /// <remarks>
    /// This will return the closest substring of text to the given point.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    public static SubString GetTextSubStringForPoint(Text text, Point point) {
        bool result = GetTextSubStringForPoint(text, point, out SubString substring);
        if (!result) {
            Sdl.LogError(LogCategory.Error, "Failed to get text substring for point.");
            return new SubString();
        }
        return substring;
    }

    /// <summary>Get the substrings of a text object that contain a range of text.</summary>
    /// <param name="text">the <see cref="Text"/> to query.</param>
    /// <param name="offset">a byte offset into the text string.</param>
    /// <param name="length">the length of the range being queried, in bytes, or -1 for the remainder of the string.</param>
    /// <param name="count">a pointer filled in with the number of substrings returned, may be discarded.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>(TTF_SubString **) Returns a <see langword="null" /> terminated array ofsubstring pointers or <see langword="null" /> on failure; call <see cref="Sdl.GetError()" /> for more information. This is a single allocation that should be freed withSDL_free() when it is no longer needed.</returns>
    public static unsafe SubString[] GetTextSubStringsForRange(Text text, int offset, int length, out int count) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }

        if (offset < 0) {
            throw new ArgumentOutOfRangeException(nameof(offset), "Offset cannot be negative.");
        }

        nint pCount = Sdl.Malloc(sizeof(int));

        nint pSubStrings = TTF_GetTextSubStringsForRange(text.Handle, offset, length, pCount);
        count = Marshal.ReadInt32(pCount);

        if (pSubStrings == nint.Zero) {
            Sdl.LogError(LogCategory.Error, "Failed to get text substrings for range.");
        }

        SubString[] substrings = new SubString[count];
        for (int i = 0; i < count; i++) {
            // Issue: might be a problem with PtrToStructure<T>
            substrings[i] = *(SubString*)(pSubStrings + i * (int)Sdl.SizeOf<SubString>());
        }

        Sdl.Free(pCount);
        Sdl.Free(pSubStrings);

        return substrings;
    }

    /// <summary>Get whether wrapping is enabled on a text object.</summary>

    /// <param name="text">the <see cref="Text"/> to query.</param>
    /// <param name="wrap_width">a pointer filled in with the maximum width in pixels or 0 if the text is being wrapped on newline characters.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="SetTextWrapWidth"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static bool GetTextWrapWidth(Text text, out int wrap_width) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }

        nint pWrapWidth = Sdl.Malloc(sizeof(int));

        bool result = TTF_GetTextWrapWidth(text.Handle, pWrapWidth);

        wrap_width = Marshal.ReadInt32(pWrapWidth);

        Sdl.Free(pWrapWidth);

        return result;
    }

    public static bool Init() => TTF_Init();

    /// <summary>Insert UTF-8 text into a text object.</summary>

    /// <param name="text">the <see cref="Text"/> to modify.</param>
    /// <param name="offset">the offset, in bytes, from the beginning of the string if &gt;= 0, the offset from the end of the string if &lt; 0. Note that this does not do UTF-8 validation, so you should only insert at UTF-8 sequence boundaries.</param>
    /// <param name="string">the UTF-8 text to insert.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for null terminated text.</param>
    /// <remarks>
    /// This function may cause the internal text representation to be rebuilt.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="AppendTextString"/>
    /// <seealso cref="DeleteTextString"/>
    /// <seealso cref="SetTextString"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static bool InsertTextString(Text text, int offset, string str, ulong length) {
        ArgumentException.ThrowIfNullOrEmpty(str);

        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_InsertTextString(text.Handle, offset, str, length);
    }

    /// <summary>Insert UTF-8 text into a text object.</summary>

    /// <param name="text">the <see cref="Text"/> to modify.</param>
    /// <param name="offset">the offset, in bytes, from the beginning of the string if &gt;= 0, the offset from the end of the string if &lt; 0. Note that this does not do UTF-8 validation, so you should only insert at UTF-8 sequence boundaries.</param>
    /// <param name="string">the UTF-8 text to insert.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for null terminated text.</param>
    /// <remarks>
    /// This function may cause the internal text representation to be rebuilt.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="AppendTextString"/>
    /// <seealso cref="DeleteTextString"/>
    /// <seealso cref="SetTextString"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static bool InsertTextString(Text text, int offset, string str) {
        return InsertTextString(text, offset, str, (ulong)str.Length);
    }

    public static bool IsFixedWidth(Font font) {
        bool result = TTF_FontIsFixedWidth(font.Handle);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to check if font is fixed width. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    public static bool IsScalable(Font font) {
        bool result = TTF_FontIsScalable(font.Handle);
        if (!result) {
            Sdl.LogInfo(LogCategory.Error, $"Font is not scalable. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    public static bool MeasureString(Font font, string text, int max_width, out int measured_width, out int measured_length) {
        ArgumentException.ThrowIfNullOrEmpty(text);
        bool result = TTF_MeasureString(font.Handle, text, (nuint)text.Length, max_width, out int mW, out nuint mL);

        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to measure string '{text}'. SDL Error: {Sdl.GetError()}");
        }
        measured_width = mW;
        measured_length = (int)mL;
        return result;
    }

    public static bool MeasureString(Font font, string text, int max_width, out Size measuredSize) {
        bool result = MeasureString(font, text, max_width, out int mW, out int mL);

        measuredSize = new(mW, mL);
        return result;
    }

    /// <summary>Calculate how much of a UTF-8 string will fit in a given width.</summary>

    /// <param name="font">the font to query.</param>
    /// <param name="text">text to calculate, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for null terminated text.</param>
    /// <param name="max_width">maximum width, in pixels, available for the string, or 0 for unbounded width.</param>
    /// <param name="measured_width">a pointer filled in with the width, in pixels, of the string that will fit, may be discarded.</param>
    /// <param name="measured_length">a pointer filled in with the length, in bytes, of the string that will fit, may be discarded.</param>
    /// <remarks>
    /// This reports the number of characters that can be rendered before reaching
    /// max_width.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static Size MeasureString(Font font, string text, int max_width) {
        MeasureString(font, text, max_width, out Size measuredSize);
        return measuredSize;
    }

    /// <summary>Calculate how much of a UTF-8 string will fit in a given width.</summary>

    /// <param name="font">the font to query.</param>
    /// <param name="text">text to calculate, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for null terminated text.</param>
    /// <param name="max_width">maximum width, in pixels, available for the string, or 0 for unbounded width.</param>
    /// <param name="measured_width">a pointer filled in with the width, in pixels, of the string that will fit, may be discarded.</param>
    /// <param name="measured_length">a pointer filled in with the length, in bytes, of the string that will fit, may be discarded.</param>
    /// <remarks>
    /// This reports the number of characters that can be rendered before reaching
    /// max_width.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static Size MeasureString(Font font, string text) {
        return MeasureString(font, text, 0);
    }

    /// <summary>
    /// Checks if the compiled SDL3_ttf version is at least the specified version.
    /// </summary>
    /// <param name="major">The major version to check.</param>
    /// <param name="minor">The minor version to check.</param>
    /// <param name="micro">The micro version to check.</param>
    /// <returns>True if the compiled version is at least the specified version; otherwise, <see langword="false" />.</returns>
    public static bool TtfVersionAtLeast(int major, int minor, int micro) =>
        (Major >= major) &&
        (Major > major || Minor >= minor) &&
        (Major > major || Minor > minor || Micro >= micro);

    /// <summary>Create a font from a file, using a specified point size.</summary>

    /// <param name="file">path to font file.</param>
    /// <param name="ptsize">point size to use for the newly-opened font.</param>
    /// <remarks>
    /// Some .fon fonts will have several sizes embedded in the file, so the point
    /// size becomes the index of choosing which size. If the value is too high,
    /// the last indexed size will be the default.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="CloseFont"/>
    /// </remarks>
    /// <returns>(TTF_Font *) Returns a valid TTF_Font, or <see langword="null" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static Font OpenFont(string file, float ptsize) {
        if (string.IsNullOrEmpty(file)) {
            throw new ArgumentException("Font file path cannot be null or empty.", nameof(file));
        }

        if (!File.Exists(file)) {
            throw new FileNotFoundException($"Font file not found: {file}", file);
        }

        Font fontPtr = TTF_OpenFont(file, ptsize);

        return fontPtr;
    }

    public static Font OpenFontIO(IOStream src, bool closeio, float ptsize) {
        if (src.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        Font font = TTF_OpenFontIO(src.Handle, closeio, ptsize);
        return font;
    }

    /// <summary>Create a font with the specified properties.</summary>

    /// <param name="props">the properties to use.</param>
    /// <remarks>
    /// These are the supported properties:
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="CloseFont"/>
    /// </remarks>
    /// <returns>(TTF_Font *) Returns a valid TTF_Font, or <see langword="null" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static Font OpenFontWithProperties(int props) {
        Font font = TTF_OpenFontWithProperties(props);
        return font;
    }

    public static void Quit() {
        TTF_Quit();
    }

    /// <summary>Remove a fallback font.</summary>

    /// <param name="font">the font to modify.</param>
    /// <param name="fallback">the font to remove as a fallback.</param>
    /// <remarks>
    /// This updates any TTF_Text objects using this font.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created both fonts.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="AddFallbackFont"/>
    /// <seealso cref="ClearFallbackFonts"/>
    /// </remarks>

    public static void RemoveFallbackFont(Font font, Font fallback) {
        TTF_RemoveFallbackFont(font.Handle, fallback.Handle);
    }

    public static nint RenderGlyphBlended(Font font, int ch, Color fg) {
        if (ch < 0) {
            throw new ArgumentOutOfRangeException(nameof(ch), "Character codepoint must be non-negative.");
        }
        return TTF_RenderGlyph_Blended(font.Handle, ch, fg);
    }

    public static Surface RenderGlyphLCD(Font font, int ch, Color fg, Color bg) {
        if (ch < 0 || ch > MaxUnicodeCodePoint) {
            throw new ArgumentOutOfRangeException(nameof(ch), "The character code must be a valid Unicode code point.");
        }
        if (fg.A == 0 && bg.A == 0) {
            throw new ArgumentException("Both foreground and background colors cannot be fully transparent.");
        }

        nint result = TTF_RenderGlyph_LCD(font.Handle, ch, fg, bg);
        if (result == nint.Zero) {
            Sdl.LogError(LogCategory.Error, $"Failed to render glyph {ch} with LCD quality. SDL Error: {Sdl.GetError()}");
        }
        Surface surface = *(Surface*)result;
        return surface;
    }

    public static nint RenderGlyphShaded(Font font, int ch, Color fg, Color bg) {
        if (ch < 0 || ch > MaxUnicodeCodePoint) {
            throw new ArgumentOutOfRangeException(nameof(ch), "The character code must be a valid Unicode code point.");
        }
        return TTF_RenderGlyph_Shaded(font.Handle, ch, fg, bg);
    }

    public static nint RenderGlyphSolid(Font font, int ch, Color fg) {
        if (ch < 0 || ch > MaxUnicodeCodePoint) {
            throw new ArgumentOutOfRangeException(nameof(ch), "The character code must be a valid Unicode code point.");
        }
        return TTF_RenderGlyph_Solid(font.Handle, ch, fg);
    }

    public static nint RenderTextBlended(Font font, string text, ulong length, Color fg) {
        ArgumentException.ThrowIfNullOrEmpty(text);
        return TTF_RenderText_Blended(font.Handle, text, length, fg);
    }

    public static nint RenderTextBlended(Font font, string text, Color fg) {
        ArgumentException.ThrowIfNullOrEmpty(text);
        return RenderTextBlended(font, text, (ulong)text.Length, fg);
    }

    public static nint RenderTextBlendedWrapped(Font font, string text, ulong length, Color fg, int wrap_width) {
        ArgumentException.ThrowIfNullOrEmpty(text);
        return TTF_RenderText_Blended_Wrapped(font.Handle, text, length, fg, wrap_width);
    }

    public static nint RenderTextBlendedWrapped(Font font, string text, Color fg, int wrap_width) {
        ArgumentException.ThrowIfNullOrEmpty(text);

        return RenderTextBlendedWrapped(font, text, (ulong)text.Length, fg, wrap_width);
    }

    public static nint RenderTextLCD(Font font, string text, ulong length, Color fg, Color bg) {
        ArgumentException.ThrowIfNullOrEmpty(text);
        return TTF_RenderText_LCD(font.Handle, text, length, fg, bg);
    }

    public static nint RenderTextLCDWrapped(Font font, string text, ulong length, Color fg, Color bg, int wrap_width) {
        ArgumentException.ThrowIfNullOrEmpty(text);
        return TTF_RenderText_LCD_Wrapped(font.Handle, text, length, fg, bg, wrap_width);
    }

    public static nint RenderTextShaded(Font font, string text, ulong length, Color fg, Color bg) {
        ArgumentException.ThrowIfNullOrEmpty(text);
        return TTF_RenderText_Shaded(font.Handle, text, length, fg, bg);
    }

    public static nint RenderTextShadedWrapped(Font font, string text, ulong length, Color fg, Color bg, int wrap_width) {
        ArgumentException.ThrowIfNullOrEmpty(text);
        return TTF_RenderText_Shaded_Wrapped(font.Handle, text, length, fg, bg, wrap_width);
    }

    /// <summary>
    /// Render UTF-8 text at fast quality to a new 8-bit surface.
    /// </summary>
    /// <param name="font">the font to render with.</param>
    /// <param name="text">text to render, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for null terminated text.</param>
    /// <param name="fg">the foreground color for the text.</param>
    /// <remarks>
    /// This function will allocate a new 8-bit, palettized surface. The surface's 0 pixel will be the color key, giving a transparent background. The 1 pixel will be set to the text color.
    /// <para>This will not word-wrap the string; you'll get a surface with a single line of text, as long as the string requires. You can use <see cref="RenderTextSolidWrapped"/> instead if you need to wrap the output to multiple lines.</para>
    /// <para>This will not wrap on newline characters.</para>
    /// <para>You can render at other quality levels with <see cref="RenderTextShaded"/>, <see cref="RenderTextBlended"/>, and <see cref="RenderTextLCD"/>.</para>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0</para>
    /// <seealso cref="RenderTextBlended"/>
    /// <seealso cref="RenderTextLCD"/>
    /// <seealso cref="RenderTextShaded"/>
    /// <seealso cref="RenderTextSolid"/>
    /// <seealso cref="RenderTextSolidWrapped"/>
    /// </remarks>
    /// <returns>(SDL_Surface *) Returns a new 8-bit, palettized surface, or <see cref="nint.Zero"/> if there was an error.</returns>
    public static nint RenderTextSolid(Font font, string text, ulong length, Color fg) {
        ArgumentException.ThrowIfNullOrEmpty(text);
        if (font.Handle == nint.Zero) {
            return nint.Zero;
        }
        return TTF_RenderText_Solid(font.Handle, text, length, fg);
    }

    /// <summary>
    /// Render UTF-8 text at fast quality to a new 8-bit surface.
    /// </summary>
    /// <param name="font">the font to render with.</param>
    /// <param name="text">text to render, in UTF-8 encoding.</param>
    /// <param name="fg">the foreground color for the text.</param>
    /// <remarks>
    /// This function will allocate a new 8-bit, palettized surface. The surface's 0 pixel will be the color key, giving a transparent background. The 1 pixel will be set to the text color.
    /// <para>This will not word-wrap the string; you'll get a surface with a single line of text, as long as the string requires. You can use <see cref="RenderTextSolidWrapped"/> instead if you need to wrap the output to multiple lines.</para>
    /// <para>This will not wrap on newline characters.</para>
    /// <para>You can render at other quality levels with <see cref="RenderTextShaded"/>, <see cref="RenderTextBlended"/>, and <see cref="RenderTextLCD"/>.</para>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0</para>
    /// <seealso cref="RenderTextBlended"/>
    /// <seealso cref="RenderTextLCD"/>
    /// <seealso cref="RenderTextShaded"/>
    /// <seealso cref="RenderTextSolid"/>
    /// <seealso cref="RenderTextSolidWrapped"/>
    /// </remarks>
    /// <returns>(SDL_Surface *) Returns a new 8-bit, palettized surface, or <see cref="nint.Zero"/> if there was an error.</returns>
    public static nint RenderTextSolid(Font font, string text, Color fg) {
        return RenderTextSolid(font, text, (ulong)text.Length, fg);
    }

    public static nint RenderTextSolidWrapped(Font font, string text, ulong length, Color fg, int wrapLength) {
        ArgumentException.ThrowIfNullOrEmpty(text);
        return TTF_RenderText_Solid_Wrapped(font.Handle, text, length, fg, wrapLength);
    }

    /// <summary>Set the direction to be used for text shaping by a font.</summary>
    /// <param name="font">the font to modify.</param>
    /// <param name="direction">the new direction for text to flow.</param>
    /// <remarks>
    /// This function only supports left-to-right text shaping if SDL_ttf was not
    /// built with HarfBuzz support.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    public static bool SetFontDirection(Font font, Direction direction) {
        bool result = TTF_SetFontDirection(font.Handle, direction);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to set font direction. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>Set a font's current hinter setting.</summary>
    /// <param name="font">the font to set a new hinter setting on.</param>
    /// <param name="hinting">the new hinter setting.</param>
    /// <remarks>
    /// This updates any <see cref="Text"/> objects using this font, and clears
    /// already-generated glyphs, if any, from the cache.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="GetFontHinting"/>
    /// </remarks>
    public static void SetFontHinting(Font font, Hinting hinting) {
        if (!Enum.IsDefined(hinting)) {
            throw new ArgumentOutOfRangeException(nameof(hinting), "Invalid hinting value.");
        }
        TTF_SetFontHinting(font.Handle, hinting);
    }

    /// <summary>Set if kerning is enabled for a font.</summary>
    /// <param name="font">the font to set kerning on.</param>
    /// <param name="enabled"><see langword="true" /> to enable kerning, <see langword="false" /> to disable.</param>
    /// <remarks>
    /// Newly-opened fonts default to allowing kerning. This is generally a good
    /// policy unless you have a strong reason to disable it, as it tends to
    /// produce better rendering (with kerning disabled, some fonts might render
    /// the word kerning as something that looks like keming for example).
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="GetFontKerning"/>
    /// </remarks>
    public static void SetFontKerning(Font font, bool enabled) {
        TTF_SetFontKerning(font.Handle, enabled);
    }

    /// <summary>Set language to be used for text shaping by a font.</summary>
    /// <param name="font">the font to specify a language for.</param>
    /// <param name="language_bcp47">a null-terminated string containing the desired language's BCP47 code. Or null to reset the value.</param>
    /// <remarks>
    /// If SDL_ttf was not built with HarfBuzz support, this function returns <see langword="false" />.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    public static bool SetFontLanguage(Font font, string language_bcp47) {
        bool result = TTF_SetFontLanguage(font.Handle, language_bcp47);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to set font language. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>Set the spacing between lines of text for a font.</summary>
    /// <param name="font">the font to modify.</param>
    /// <param name="lineskip">the new line spacing for the font.</param>
    /// <remarks>
    /// This updates any <see cref="Text"/> objects using this font.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="GetFontLineSkip"/>
    /// </remarks>
    public static void SetFontLineSkip(Font font, int lineskip) {
        TTF_SetFontLineSkip(font.Handle, lineskip);
    }

    /// <summary>Set a font's current outline.</summary>
    /// <param name="font">the font to set a new outline on.</param>
    /// <param name="outline">positive outline value, 0 to default.</param>
    /// <remarks>
    /// This uses the font properties:
    /// <list type="bullet">
    /// <item>TTF_PROP_FONT_OUTLINE_LINE_CAP_NUMBER</item>
    /// <item>TTF_PROP_FONT_OUTLINE_LINE_JOIN_NUMBER</item>
    /// <item>TTF_PROP_FONT_OUTLINE_MITER_LIMIT_NUMBER</item>
    /// </list>
    /// when setting the font outline.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="GetFontOutline"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    public static bool SetFontOutline(Font font, int outline) {
        bool result = TTF_SetFontOutline(font.Handle, outline);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to set font outline. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>Set the script to be used for text shaping by a font.</summary>
    /// <param name="font">the font to modify.</param>
    /// <param name="script">an ISO 15924 code .</param>
    /// <remarks>
    /// This returns <see langword="false" /> if SDL_ttf isn't built with HarfBuzz support.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="StringToTag"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    public static bool SetFontScript(Font font, int script) {
        bool result = TTF_SetFontScript(font.Handle, script);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to set font script. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>Enable Signed Distance Field rendering for a font.</summary>
    /// <param name="font">the font to set SDF support on.</param>
    /// <param name="enabled"><see langword="true" /> to enable SDF, <see langword="false" /> to disable.</param>
    /// <remarks>
    /// SDF is a technique that helps fonts look sharp even when scaling and
    /// rotating, and requires special shader support for display.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="GetFontSDF"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    public static bool SetFontSDF(Font font, bool enabled) {
        bool result = TTF_SetFontSDF(font.Handle, enabled);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to set font SDF. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>Set a font's size dynamically.</summary>
    /// <param name="font">the font to resize.</param>
    /// <param name="ptsize">the new point size.</param>
    /// <remarks>
    /// This updates any <see cref="Text"/> objects using this font, and clears
    /// already-generated glyphs, if any, from the cache.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="GetFontSize"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    public static bool SetFontSize(Font font, float ptsize) {
        if (ptsize <= 0) {
            throw new ArgumentOutOfRangeException(nameof(ptsize), "Point size must be greater than zero.");
        }

        bool result = TTF_SetFontSize(font.Handle, ptsize);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to set font size. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>Set font size dynamically with target resolutions, in dots per inch.</summary>
    /// <param name="font">the font to resize.</param>
    /// <param name="ptsize">the new point size.</param>
    /// <param name="hdpi">the target horizontal DPI.</param>
    /// <param name="vdpi">the target vertical DPI.</param>
    /// <remarks>
    /// This updates any <see cref="Text"/> objects using this font, and clears
    /// already-generated glyphs, if any, from the cache.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="GetFontSize"/>
    /// <seealso cref="GetFontSizeDPI"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    public static bool SetFontSizeDPI(Font font, float ptsize, int hdpi, int vdpi) {
        bool result = TTF_SetFontSizeDPI(font.Handle, ptsize, hdpi, vdpi);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to set font size DPI. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>Set a font's current style.</summary>
    /// <param name="font">the font to set a new style on.</param>
    /// <param name="style">the new style values to set, OR'd together.</param>
    /// <remarks>
    /// This updates any <see cref="Text"/> objects using this font, and clears
    /// already-generated glyphs, if any, from the cache.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="GetFontStyle"/>
    /// </remarks>
    public static void SetFontStyle(Font font, FontStyle style) {
        if (!Enum.IsDefined(style)) {
            throw new ArgumentOutOfRangeException(nameof(style), "Invalid font style.");
        }
        TTF_SetFontStyle(font.Handle, style);
    }

    /// <summary>Set a font's current wrap alignment option.</summary>
    /// <param name="font">the font to set a new wrap alignment option on.</param>
    /// <param name="align">the new wrap alignment option.</param>
    /// <remarks>
    /// This updates any <see cref="Text"/> objects using this font.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the font.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="GetFontWrapAlignment"/>
    /// </remarks>
    public static void SetFontWrapAlignment(Font font, HorizontalAlignment align) {
        if (!Enum.IsDefined(align)) {
            throw new ArgumentOutOfRangeException(nameof(align), "Invalid horizontal alignment value.");
        }
        TTF_SetFontWrapAlignment(font.Handle, align);
    }

    /// <summary>Sets the winding order of the vertices returned by <see cref="GetGPUTextDrawData"/> for a particular GPU text engine.</summary>
    /// <param name="engine">a TTF_TextEngine object created with <see cref="CreateGPUTextEngine"/>.</param>
    /// <param name="winding">the new winding order option.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the engine.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="GetGPUTextEngineWinding"/>
    /// </remarks>
    public static void SetGPUTextEngineWinding(nint engine, GPUTextEngineWinding winding) {
        if (engine == nint.Zero) {
            throw new ArgumentNullException(nameof(engine), "Engine cannot be null.");
        }
        TTF_SetGPUTextEngineWinding(engine, winding);
    }

    /// <summary>Set the color of a text object.</summary>
    /// <param name="text">the <see cref="Text"/> to modify.</param>
    /// <param name="r">the red color value in the range of 0-255.</param>
    /// <param name="g">the green color value in the range of 0-255.</param>
    /// <param name="b">the blue color value in the range of 0-255.</param>
    /// <param name="a">the alpha value in the range of 0-255.</param>
    /// <remarks>
    /// The default text color is white (255, 255, 255, 255).
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="GetTextColor"/>
    /// <seealso cref="SetTextColorFloat"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    public static bool SetTextColor(Text text, byte r, byte g, byte b, byte a) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextColor(text.Handle, r, g, b, a);
    }

    /// <summary>Set the color of a text object.</summary>
    /// <param name="text">the <see cref="Text"/> to modify.</param>
    /// <param name="color">the color value.</param>
    /// <remarks>
    /// The default text color is white (255, 255, 255, 255).
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="GetTextColor"/>
    /// <seealso cref="SetTextColorFloat"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    public static bool SetTextColor(Text text, Color color) {
        return SetTextColor(text, color.R, color.G, color.B, color.A);
    }

    /// <summary>Set the color of a text object.</summary>

    /// <param name="text">the <see cref="Text"/> to modify.</param>
    /// <param name="r">the red color value, normally in the range of 0-1.</param>
    /// <param name="g">the green color value, normally in the range of 0-1.</param>
    /// <param name="b">the blue color value, normally in the range of 0-1.</param>
    /// <param name="a">the alpha value in the range of 0-1.</param>
    /// <remarks>
    /// The default text color is white (1.0f, 1.0f, 1.0f, 1.0f).
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="GetTextColorFloat"/>
    /// <seealso cref="SetTextColor"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    public static bool SetTextColorFloat(Text text, float r, float g, float b, float a) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextColorFloat(text.Handle, r, g, b, a);
    }

    /// <summary>Set the color of a text object.</summary>
    /// <param name="text">the <see cref="Text"/> to modify.</param>
    /// <param name="color">the color value</param>
    /// <remarks>
    /// The default text color is white (1.0f, 1.0f, 1.0f, 1.0f).
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="GetTextColorFloat"/>
    /// <seealso cref="SetTextColor"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>

    public static bool SetTextColorFloat(Text text, FColor color) {
        return SetTextColorFloat(text, color.R, color.G, color.B, color.A);
    }

    /// <summary>Set the direction to be used for text shaping a text object.</summary>
    /// <param name="text">the text to modify.</param>
    /// <param name="direction">the new direction for text to flow.</param>
    /// <remarks>
    /// This function only supports left-to-right text shaping if SDL_ttf was not
    /// built with HarfBuzz support.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    public static bool SetTextDirection(Text text, Direction direction) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextDirection(text.Handle, direction);
    }

    /// <summary>Set the text engine used by a text object.</summary>
    /// <param name="text">the <see cref="Text"/> to modify.</param>
    /// <param name="engine">the text engine to use for drawing.</param>
    /// <remarks>
    /// This function may cause the internal text representation to be rebuilt.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="GetTextEngine"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    public static bool SetTextEngine(Text text, TextEngine engine) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }

        if (engine.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(engine), "Engine cannot be null.");
        }

        return TTF_SetTextEngine(text.Handle, engine.Handle);
    }

    /// <summary>Set the font used by a text object.</summary>
    /// <param name="text">the <see cref="Text"/> to modify.</param>
    /// <param name="font">the font to use, may be discarded.</param>
    /// <remarks>
    /// When a text object has a font, any changes to the font will automatically
    /// regenerate the text. If you set the font to <see langword="null" />, the text will continue to
    /// render but changes to the font will no longer affect the text.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="GetTextFont"/>
    /// </remarks>
    /// <returns>Returns <see langword="false" /> if the text pointer is null; otherwise, <see langword="true" />. call<see cref="Sdl.GetError()" /> for more information.</returns>
    public static bool SetTextFont(Text text, Font font) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextFont(text.Handle, font.Handle);
    }

    /// <summary>Set the position of a text object.</summary>
    /// <param name="text">the <see cref="Text"/> to modify.</param>
    /// <param name="x">the x offset of the upper left corner of this text in pixels.</param>
    /// <param name="y">the y offset of the upper left corner of this text in pixels.</param>
    /// <remarks>
    /// This can be used to position multiple text objects within a single wrapping
    /// text area.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="GetTextPosition"/>
    /// </remarks>
    public static bool SetTextPosition(Text text, int x, int y) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextPosition(text.Handle, x, y);
    }

    /// <summary>Set the position of a text object.</summary>
    /// <param name="text">the <see cref="Text"/> to modify.</param>
    /// <param name="x">the x offset of the upper left corner of this text in pixels.</param>
    /// <param name="y">the y offset of the upper left corner of this text in pixels.</param>
    /// <remarks>
    /// This can be used to position multiple text objects within a single wrapping
    /// text area.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="GetTextPosition"/>
    /// </remarks>
    public static bool SetTextPosition(Text text, Point position) {
        return SetTextPosition(text, position.X, position.Y);
    }

    /// <summary>Set the script to be used for text shaping a text object.</summary>
    /// <param name="text">the text to modify.</param>
    /// <param name="script">an ISO 15924 code .</param>
    /// <remarks>
    /// This returns <see langword="false" /> if SDL_ttf isn't built with HarfBuzz support.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="StringToTag"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    public static bool SetTextScript(Text text, int script) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextScript(text.Handle, script);
    }

    /// <summary>Set the UTF-8 text used by a text object.</summary>
    /// <param name="text">the <see cref="Text"/> to modify.</param>
    /// <param name="string">the UTF-8 text to use, may be discarded.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for null terminated text.</param>
    /// <remarks>
    /// This function may cause the internal text representation to be rebuilt.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="AppendTextString"/>
    /// <seealso cref="DeleteTextString"/>
    /// <seealso cref="InsertTextString"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    public static bool SetTextString(Text text, string str, ulong length) {
        ArgumentException.ThrowIfNullOrEmpty(str);

        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }

        return TTF_SetTextString(text.Handle, str, length);
    }

    /// <summary>Set whether whitespace should be visible when wrapping a text object.</summary>
    /// <param name="text">the <see cref="Text"/> to modify.</param>
    /// <param name="visible"><see langword="true" /> to show whitespace when wrapping text, <see langword="false" /> to hide it.</param>
    /// <remarks>
    /// If the whitespace is visible, it will take up space for purposes of
    /// alignment and wrapping. This is good for editing, but looks better when
    /// centered or aligned if whitespace around line wrapping is hidden. This
    /// defaults <see langword="false" />.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="TextWrapWhitespaceVisible"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    public static bool SetTextWrapWhitespaceVisible(Text text, bool visible) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextWrapWhitespaceVisible(text.Handle, visible);
    }

    /// <summary>Set whether wrapping is enabled on a text object.</summary>
    /// <param name="text">the <see cref="Text"/> to modify.</param>
    /// <param name="wrap_width">the maximum width in pixels, 0 to wrap on newline characters.</param>
    /// <remarks>
    /// This function may cause the internal text representation to be rebuilt.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="GetTextWrapWidth"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    public static bool SetTextWrapWidth(Text text, int wrap_width) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextWrapWidth(text.Handle, wrap_width);
    }

    /// <summary>Convert from a 4 character string to a 32-bit tag.</summary>
    /// <param name="string">the 4 character string to convert.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="TagToString"/>
    /// </remarks>
    /// <returns>Returns the 32-bit representation of the string.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="str"/> is not exactly 4 characters long, or if <paramref name="str"/> contains <see langword="null"/> characters.</exception>
    public static int StringToTag(string str) {
        ArgumentException.ThrowIfNullOrEmpty(str);

        if (str.Length != 4) {
            throw new ArgumentException("String must be exactly 4 characters long.", nameof(str));
        }
        if (str[0] == '\0' || str[1] == '\0' || str[2] == '\0' || str[3] == '\0') {
            throw new ArgumentException("String must not contain null characters.", nameof(str));
        }
        return TTF_StringToTag(str);
    }

    /// <summary>Convert from a 32-bit tag to a 4 character string.</summary>
    /// <param name="tag">the 32-bit tag to convert.</param>
    /// <param name="size">the size of the buffer pointed at by string, should be at least 4.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// <seealso cref="TagToString"/>
    /// </remarks>
    /// <returns>a pointer filled in with the 4 character representation of the tag.</returns>
    public static string TagToString(int tag, ulong size) {
        Span<char> buffer = stackalloc char[5];
        TTF_TagToString(tag, ref MemoryMarshal.GetReference(buffer), size);
        return new string(buffer.Slice(0, 4));
    }

    /// <summary>
    /// Return whether whitespace is shown when wrapping a text object.
    /// </summary>
    /// <param name="text">the <see cref="Text"/> to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0</para>
    /// </remarks>
    /// <returns><see langword="true"/> if whitespace is shown when wrapping text, or <see langword="false"/> otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown is <paramref name="text"/> is <see langword="null"/>.</exception>
    public static bool TextWrapWhitespaceVisible(Text text) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_TextWrapWhitespaceVisible(text.Handle);
    }

    public static int TtfVersion() => TTF_Version();

    /// <summary>Update the layout of a text object.</summary>
    /// <param name="text">the <see cref="Text"/> to update.</param>
    /// <remarks>
    /// This is automatically done when the layout is requested or the text is
    /// rendered, but you can call this if you need more control over the timing of
    /// when the layout and text engine representation are updated.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the text.</para>
    /// <para><strong>Version:</strong> This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="Sdl.GetError()" /> for more information.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="text"/> is <see langword="null"/>.</exception>
    public static bool UpdateText(Text text) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_UpdateText(text.Handle);
    }

    /// <summary>
    /// This is the version number macro for the current SDL_ttf version.
    /// </summary>
    /// <returns></returns>
    public static int Version() => Sdl.VersionNum(Major, Minor, Micro);

    /// <summary>Get a mask of the specified subsystems which are currently initialized.</summary>
    /// <param name="flags">any of the flags used by <see cref="Init"/>; see <see cref="Init"/> for details.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="Init"/>
    /// <seealso cref="InitSubSystem"/>
    /// </remarks>
    /// <returns>Returns a mask of all initialized subsystems if flags is 0, otherwise it returns the initialization status of the specified subsystems.</returns>
    public static int WasInit() {
        return TTF_WasInit();
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_AddFallbackFont(nint font, nint fallback);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_AppendTextString(nint text, [MarshalAs(Sdl.StringType)] string str, ulong length);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_ClearFallbackFonts(nint font);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_CloseFont(nint font);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedFontMarshaller))]
    private static partial Font TTF_CopyFont(nint existing_font);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint TTF_CreateGPUTextEngine(nint device);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint TTF_CreateGPUTextEngineWithProperties(int props);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint TTF_CreateRendererTextEngine(nint renderer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint TTF_CreateRendererTextEngineWithProperties(int props);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint TTF_CreateSurfaceTextEngine();

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint TTF_CreateText(nint engine, nint font, [MarshalAs(Sdl.StringType)] string text, nuint length);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_DeleteTextString(nint text, int offset, int length);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_DestroyGPUTextEngine(nint engine);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_DestroyRendererTextEngine(nint engine);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_DestroySurfaceTextEngine(nint engine);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_DestroyText(nint text);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_DrawRendererText(nint text, float x, float y);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_DrawSurfaceText(nint text, int x, int y, nint surface);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_FontHasGlyph(nint font, int ch);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_FontIsFixedWidth(nint font);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_FontIsScalable(nint font);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetFontAscent(nint font);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetFontDescent(nint font);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Direction TTF_GetFontDirection(nint font);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_GetFontDPI(nint font, nint hdpi, nint vdpi);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial string TTF_GetFontFamilyName(nint font);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetFontGeneration(nint font);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetFontHeight(nint font);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Hinting TTF_GetFontHinting(nint font);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_GetFontKerning(nint font);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetFontLineSkip(nint font);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetFontOutline(nint font);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetFontProperties(nint font);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetFontScript(nint font);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_GetFontSDF(nint font);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float TTF_GetFontSize(nint font);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial FontStyle TTF_GetFontStyle(nint font);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial string TTF_GetFontStyleName(nint font);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial FontWeight TTF_GetFontWeight(nint font);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial HorizontalAlignment TTF_GetFontWrapAlignment(nint font);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_GetFreeTypeVersion(nint major, nint minor, nint patch);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint TTF_GetGlyphImage(nint font, int ch, ImageType image_type);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint TTF_GetGlyphImageForIndex(nint font, int glyph_index, ImageType image_type);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_GetGlyphKerning(nint font, int previous_ch, int ch, int kerning);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_GetGlyphMetrics(nint font, int ch, nint minx, nint maxx, nint miny, nint maxy, nint advance);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetGlyphScript(int ch);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint TTF_GetGPUTextDrawData(nint text);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GPUTextEngineWinding TTF_GetGPUTextEngineWinding(nint engine);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_GetHarfBuzzVersion(nint major, nint minor, nint patch);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_GetNextTextSubString(nint text, nint substring, nint next);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetNumFontFaces(nint font);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_GetPreviousTextSubString(nint text, nint substring, nint previous);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_GetStringSize(nint font, [MarshalAs(Sdl.StringType)] string text, ulong length, int w, int h);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_GetStringSizeWrapped(nint font, [MarshalAs(Sdl.StringType)] string text, ulong length, int wrap_width, int w, int h);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_GetTextColor(nint text, nint r, nint g, nint b, nint a);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_GetTextColorFloat(nint text, nint r, nint g, nint b, nint a);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Direction TTF_GetTextDirection(nint text);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint TTF_GetTextEngine(nint text);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint TTF_GetTextFont(nint text);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_GetTextPosition(nint text, nint x, nint y);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint TTF_GetTextProperties(nint text);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetTextScript(nint text);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_GetTextSize(nint text, nint w, nint h);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_GetTextSubString(nint text, int offset, nint substring);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_GetTextSubStringForLine(nint text, int line, nint substring);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_GetTextSubStringForPoint(nint text, int x, int y, nint substring);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint TTF_GetTextSubStringsForRange(nint text, int offset, int length, nint count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_GetTextWrapWidth(nint text, nint wrap_width);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_Init();

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_InsertTextString(nint text, int offset, string str, ulong length);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_MeasureString(nint font, string text, nuint length, int max_width, out int measured_width, out nuint measured_length);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedFontMarshaller))]
    private static partial Font TTF_OpenFont(string file, float ptsize);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedFontMarshaller))]
    private static partial Font TTF_OpenFontIO(nint src, [MarshalAs(Sdl.BoolType)] bool closeio, float ptsize);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedFontMarshaller))]
    private static partial Font TTF_OpenFontWithProperties(int props);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_Quit();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_RemoveFallbackFont(nint font, nint fallback);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint TTF_RenderGlyph_Blended(nint font, int ch, Color fg);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint TTF_RenderGlyph_LCD(nint font, int ch, Color fg, Color bg);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint TTF_RenderGlyph_Shaded(nint font, int ch, Color fg, Color bg);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint TTF_RenderGlyph_Solid(nint font, int ch, Color fg);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint TTF_RenderText_Blended(nint font, string text, ulong length, Color fg);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint TTF_RenderText_Blended_Wrapped(nint font, string text, ulong length, Color fg, int wrap_width);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint TTF_RenderText_LCD(nint font, string text, ulong length, Color fg, Color bg);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint TTF_RenderText_LCD_Wrapped(nint font, string text, ulong length, Color fg, Color bg, int wrap_width);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint TTF_RenderText_Shaded(nint font, string text, ulong length, Color fg, Color bg);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint TTF_RenderText_Shaded_Wrapped(nint font, string text, ulong length, Color fg, Color bg, int wrap_width);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint TTF_RenderText_Solid(nint font, string text, ulong length, Color fg);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint TTF_RenderText_Solid_Wrapped(nint font, string text, ulong length, Color fg, int wrapLength);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_SetFontDirection(nint font, Direction direction);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_SetFontHinting(nint font, Hinting hinting);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_SetFontKerning(nint font, [MarshalAs(Sdl.BoolType)] bool enabled);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_SetFontLanguage(nint font, string language_bcp47);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_SetFontLineSkip(nint font, int lineskip);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_SetFontOutline(nint font, int outline);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_SetFontScript(nint font, int script);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_SetFontSDF(nint font, [MarshalAs(Sdl.BoolType)] bool enabled);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_SetFontSize(nint font, float ptsize);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_SetFontSizeDPI(nint font, float ptsize, int hdpi, int vdpi);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_SetFontStyle(nint font, FontStyle style);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_SetFontWrapAlignment(nint font, HorizontalAlignment align);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_SetGPUTextEngineWinding(nint engine, GPUTextEngineWinding winding);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_SetTextColor(nint text, byte r, byte g, byte b, byte a);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_SetTextColorFloat(nint text, float r, float g, float b, float a);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_SetTextDirection(nint text, Direction direction);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_SetTextEngine(nint text, nint engine);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_SetTextFont(nint text, nint font);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_SetTextPosition(nint text, int x, int y);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_SetTextScript(nint text, int script);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_SetTextString(nint text, [MarshalAs(Sdl.StringType)] string str, ulong length);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_SetTextWrapWhitespaceVisible(nint text, [MarshalAs(Sdl.BoolType)] bool visible);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_SetTextWrapWidth(nint text, int wrap_width);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_StringToTag([MarshalAs(Sdl.StringType)] string str);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_TagToString(int tag, [MarshalAs(Sdl.StringType)] string str, ulong size);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_TextWrapWhitespaceVisible(nint text);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool TTF_UpdateText(nint text);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_Version();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_WasInit();
}