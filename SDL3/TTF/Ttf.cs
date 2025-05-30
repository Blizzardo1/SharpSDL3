﻿using SharpSDL3.Enums;
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
    public const int Micro = 0;
    public const int Minor = 3;
    public const string TTF_PROP_FONT_CREATE_EXISTING_FONT = "SDL_ttf.font.create.existing_font";
    public const string TTF_PROP_FONT_CREATE_FACE_NUMBER = "SDL_ttf.font.create.face";
    public const string TTF_PROP_FONT_CREATE_FILENAME_STRING = "SDL_ttf.font.create.filename";
    public const string TTF_PROP_FONT_CREATE_HORIZONTAL_DPI_NUMBER = "SDL_ttf.font.create.hdpi";
    public const string TTF_PROP_FONT_CREATE_IOSTREAM_AUTOCLOSE_BOOLEAN = "SDL_ttf.font.create.iostream.autoclose";
    public const string TTF_PROP_FONT_CREATE_IOSTREAM_OFFSET_NUMBER = "SDL_ttf.font.create.iostream.offset";
    public const string TTF_PROP_FONT_CREATE_IOSTREAM_POINTER = "SDL_ttf.font.create.iostream";
    public const string TTF_PROP_FONT_CREATE_SIZE_FLOAT = "SDL_ttf.font.create.size";
    public const string TTF_PROP_FONT_CREATE_VERTICAL_DPI_NUMBER = "SDL_ttf.font.create.vdpi";
    public const string TTF_PROP_FONT_OUTLINE_LINE_CAP_NUMBER = "SDL_ttf.font.outline.line_cap";
    public const string TTF_PROP_FONT_OUTLINE_LINE_JOIN_NUMBER = "SDL_ttf.font.outline.line_join";
    public const string TTF_PROP_FONT_OUTLINE_MITER_LIMIT_NUMBER = "SDL_ttf.font.outline.miter_limit";
    public const string TTF_PROP_GPU_TEXT_ENGINE_ATLAS_TEXTURE_SIZE = "SDL_ttf.gpu_text_engine.create.atlas_texture_size";
    public const string TTF_PROP_GPU_TEXT_ENGINE_DEVICE = "SDL_ttf.gpu_text_engine.create.device";
    public const string TTF_PROP_RENDERER_TEXT_ENGINE_ATLAS_TEXTURE_SIZE = "SDL_ttf.renderer_text_engine.create.atlas_texture_size";
    public const string TTF_PROP_RENDERER_TEXT_ENGINE_RENDERER = "SDL_ttf.renderer_text_engine.create.renderer";
    private const int MaxUnicodeCodePoint = 0x10FFFF;
    private const string NativeLibName = "SDL3_ttf";
    /// <summary>
    /// Add a fallback font
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to modify.</param>
    /// <param name="fallback">The <see cref="Font"/> to add as a fallback</param>
    /// <remarks>
    /// <para>Add a font that will be used for glyphs that are not in the current font. The fallback font should have the same size and style as the current font.</para>
    /// <para>If there are multiple fallback fonts, they are used in the order added.</para>
    /// <para>This updates any <see cref="Font"/> objects using this <see cref="Font"/>.</para>
    /// <para>This function should be called on the thread that created both fonts.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <seealso cref="ClearFallbackFonts(Font)"/>
    /// <seealso cref="RemoveFallbackFont(Font, Font)"/>
    /// <returns>Returns <see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    public static bool AddFallbackFont(Font font, Font fallback) {
        bool result = TTF_AddFallbackFont(font.Handle, fallback.Handle);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to add fallback font. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>
    /// Append UTF-8 text to a text object.
    /// </summary>
    /// <param name="text">The <see cref="Text"/> to modify.</param>
    /// <param name="str">The UTF-8 text to insert.</param>
    /// <param name="length">The length of the text, in bytes, or 0 for null terminated text.</param>
    /// <remarks>
    /// <para>This function may cause the internal text representation to be rebuilt.</para>
    /// <para>This function should be called on the thread that created both fonts.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <seealso cref="DeleteTextString(Text, int, int)"/>
    /// <seealso cref="InsertTextString(Text, int, string)"/>
    /// <seealso cref="SetTextString(Text, string, ulong)"/>
    /// <exception cref="ArgumentNullException">Throws if the <see cref="Text"/> handle is null or 0x0.</exception>
    public static bool AppendTextString(Text text, string str, ulong length) {
        ArgumentException.ThrowIfNullOrEmpty(str);

        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }

        return TTF_AppendTextString(text.Handle, str, length);
    }

    /// <summary>
    /// Append UTF-8 text to a text object.
    /// </summary>
    /// <param name="text">The <see cref="Text"/> to modify.</param>
    /// <param name="str">The UTF-8 text to insert.</param>
    /// <remarks>
    /// <para>This function may cause the internal text representation to be rebuilt.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <seealso cref="DeleteTextString(Text, int, int)"/>
    /// <seealso cref="InsertTextString(Text, int, string)"/>
    /// <seealso cref="SetTextString(Text, string, ulong)"/>
    public static bool AppendTextString(Text text, string str) {
        return AppendTextString(text, str, (ulong)str.Length);
    }

    /// <summary>
    /// Remove all fallback fonts.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to modify.</param>
    /// <remarks>
    /// <para>This updates any <see cref="Font"/> objects using this <see cref="Font"/>.</para>
    /// <para>This function shold be called on the thread that created the <see cref="Font"/></para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <seealso cref="AddFallbackFont(Font, Font)"/>
    /// <seealso cref="RemoveFallbackFont(Font, Font)"/>
    public static void ClearFallbackFonts(Font font) {
        TTF_ClearFallbackFonts(font.Handle);
    }

    /// <summary>
    /// Dispose of a previously-created <see cref="Font"/>
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to dispose of.</param>
    /// <remarks>
    /// <para>Call this when done with a <see cref="Font"/>. This function will free any resources associated with it. It is safe to call this function on <see langword="null"/>, for example on the result of a failed call to <see cref="OpenFont(string, float)"/>.</para>
    /// <para>The <see cref="Font"/> is not valid after being passed to this function. String pointers from functions that return information on this font, such as <see cref="GetFontFamilyName(Font)"/> and <see cref="GetFontStyleName(Font)"/> are no longer valid after this call, as well.</para>
    /// <para>This function should not be called while any other thread is using the <see cref="Font"/>.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <seealso cref="OpenFont(string, float)"/>
    /// <seealso cref="OpenFontIO(IOStream, bool, float)"/>
    public static void CloseFont(Font font) {
        TTF_CloseFont(font.Handle);
    }

    /// <summary>
    /// Create a copy of an existing <see cref="Font"/>
    /// </summary>
    /// <param name="existingFont">The <see cref="Font"/> to copy.</param>
    /// <remarks>
    /// <para>The copy will be distinct from the original, but will share the font file and have the same size and style as the original.</para>
    /// <para>When done with the returned <see cref="Font"/>, use <see cref="CloseFont(Font)"/> to dispose of it.</para>
    /// <para>This function should be called on the thread that created the original <see cref="Font"/>.</para>
    /// <para>This function is available since SDL_ttf 3.0.0</para>
    /// </remarks>
    /// <returns>Returns a valid <see cref="Font"/>, or <see langword="null"/> on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <seealso cref="CloseFont(Font)"/>
    public static Font CopyFont(Font existingFont) {
        Font font = TTF_CopyFont(existingFont.Handle);
        return font;
    }

    /// <summary>
    /// Create a text engine for drawing text with the SDL GPU API.
    /// </summary>
    /// <param name="device">The SDL_GPUDevice to use for creating textures and drawing text.</param>
    /// <remarks>
    /// <para>This function should be called on the thread that created the device.</para>
    /// <para>This function is available since SDL_ttf 3.0.0</para>
    /// </remarks>
    /// <returns>Returns a <see cref="TextEngine"/> object or <see langword="null"/> on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <exception cref="ArgumentNullException">Throws on a <see langword="null"/> <paramref name="device"/></exception>
    /// <exception cref="InvalidOperationException">Throws when failed to create a <see cref="TextEngine"/>.</exception>
    /// <seealso cref="CreateGPUTextEngineWithProperties(int)"/>
    /// <seealso cref="DestroyGPUTextEngine(nint)"/>
    /// <seealso cref="GetGPUTextDrawData(nint)"/>
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

    /// <summary>
    /// Create a GPU text engine with custom properties.
    /// </summary>
    /// <param name="props">A pointer to a set of properties for the text engine.</param>
    /// <remarks>
    /// <para>This function creates a text engine for drawing text with the SDL GPU API, using the specified properties.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns a pointer to the created GPU text engine, or <see langword="null"/> on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    public static nint CreateGPUTextEngineWithProperties(int props) {
        if (props == 0) {
            throw new ArgumentNullException(nameof(props), "Properties cannot be null.");
        }
        return TTF_CreateGPUTextEngineWithProperties(props);
    }

    /// <summary>
    /// Create a text engine for drawing text with the SDL renderer API.
    /// </summary>
    /// <param name="renderer">The SDL_Renderer to use for creating textures and drawing text.</param>
    /// <remarks>
    /// <para>This function should be called on the thread that created the renderer.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns a <see cref="TextEngine"/> object or <see langword="null"/> on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <exception cref="ArgumentNullException">Throws on a <see langword="null"/> <paramref name="renderer"/></exception>
    /// <exception cref="InvalidOperationException">Throws when failed to create a <see cref="TextEngine"/>.</exception>
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

    /// <summary>
    /// Create a renderer text engine with custom properties.
    /// </summary>
    /// <param name="props">A pointer to a set of properties for the text engine.</param>
    /// <remarks>
    /// <para>This function creates a text engine for drawing text with the SDL renderer API, using the specified properties.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns a <see cref="TextEngine"/> object or <see langword="null"/> on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <exception cref="ArgumentNullException">Throws on a <see langword="null"/> <paramref name="props"/></exception>
    /// <exception cref="InvalidOperationException">Throws when failed to create a <see cref="TextEngine"/>.</exception>
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

    /// <summary>
    /// Create a text engine for drawing text to an SDL surface.
    /// </summary>
    /// <remarks>
    /// <para>This function creates a text engine for drawing text to an SDL surface.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns a <see cref="TextEngine"/> object or <see langword="null"/> on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <exception cref="InvalidOperationException">Throws when failed to create a <see cref="TextEngine"/>.</exception>
    public static unsafe TextEngine CreateSurfaceTextEngine() {
        nint tePtr = TTF_CreateSurfaceTextEngine();
        if (tePtr == nint.Zero) {
            throw new InvalidOperationException($"Failed to create surface text engine. SDL Error: {Sdl.GetError()}");
        }
        TextEngine engine = *(TextEngine*)tePtr;
        engine.Handle = tePtr;
        return engine;
    }

    /// <summary>
    /// Create a text object from UTF-8 text and a text engine.
    /// </summary>
    /// <param name="engine">The <see cref="TextEngine"/> to use when creating the text object, may be <see langword="null"/>.</param>
    /// <param name="font">The <see cref="Font"/> to render with.</param>
    /// <param name="text">The text to use, in UTF-8 encoding.</param>
    /// <param name="length">The length of the text, in bytes, or 0 for null terminated text.</param>
    /// <remarks>
    /// <para>This function should be called on the thread that created the font and text engine.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns a <see cref="Text"/> object or <see langword="null"/> on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <exception cref="InvalidOperationException">If the text failed to be created.</exception>
    /// <exception cref="ArgumentException">If the <paramref name="text"/> is <see langword="null"/> or empty.</exception>
    /// <seealso cref="DestroyText(Text)"/>
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

    /// <summary>
    /// Create a text object from UTF-8 text and a text engine.
    /// </summary>
    /// <param name="engine">The <see cref="TextEngine"/> to use when creating the text object, may be <see langword="null"/>.</param>
    /// <param name="font">The <see cref="Font"/> to render with.</param>
    /// <param name="text">The text to use, in UTF-8 encoding.</param>
    /// <remarks>
    /// <para>This function should be called on the thread that created the font and text engine.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns a <see cref="Text"/> object or <see langword="null"/> on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <seealso cref="DestroyText(Text)"/>
    public static Text CreateText(TextEngine engine, Font font, string text) {
        return CreateText(engine, font, text, text.Length);
    }

    /// <summary>
    /// Delete a range of UTF-8 text from a text object.
    /// </summary>
    /// <param name="text">The <see cref="Text"/> to modify.</param>
    /// <param name="offset">The offset, in bytes, where the deletion starts.</param>
    /// <param name="length">The length of the text to delete, in bytes.</param>
    /// <remarks>
    /// <para>This function may cause the internal text representation to be rebuilt.</para>
    /// <para>This function should be called on the thread that created the text object.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <exception cref="ArgumentNullException">Throws if the <see cref="Text"/> handle is null or 0x0.</exception>
    /// <seealso cref="AppendTextString(Text, string, ulong)"/>
    /// <seealso cref="InsertTextString(Text, int, string)"/>
    /// <seealso cref="SetTextString(Text, string, ulong)"/>
    public static bool DeleteTextString(Text text, int offset, int length) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_DeleteTextString(text.Handle, offset, length);
    }

    /// <summary>
    /// Destroy a GPU text engine.
    /// </summary>
    /// <param name="engine">A pointer to the GPU text engine to destroy.</param>
    /// <remarks>
    /// <para>This function should be called on the thread that created the text engine.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <exception cref="ArgumentNullException">Throws if the <paramref name="engine"/> is null or 0x0.</exception>
    /// <seealso cref="CreateGPUTextEngine(nint)"/>
    /// <seealso cref="CreateGPUTextEngineWithProperties(int)"/>
    public static void DestroyGPUTextEngine(nint engine) {
        if (engine == nint.Zero) {
            throw new ArgumentNullException(nameof(engine), "Engine cannot be null.");
        }
        TTF_DestroyGPUTextEngine(engine);
    }

    /// <summary>
    /// Destroy a renderer text engine.
    /// </summary>
    /// <param name="engine">The <see cref="TextEngine"/> to destroy.</param>
    /// <remarks>
    /// <para>This function should be called on the thread that created the text engine.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <exception cref="ArgumentNullException">Throws if the <see cref="TextEngine"/> handle is null or 0x0.</exception>
    /// <seealso cref="CreateRendererTextEngine(nint)"/>
    /// <seealso cref="CreateRendererTextEngineWithProperties(int)"/>
    public static void DestroyRendererTextEngine(TextEngine engine) {
        if (engine.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(engine), "Engine cannot be null.");
        }
        TTF_DestroyRendererTextEngine(engine.Handle);
    }

    /// <summary>
    /// Destroy a surface text engine.
    /// </summary>
    /// <param name="engine">The <see cref="TextEngine"/> to destroy.</param>
    /// <remarks>
    /// <para>This function should be called on the thread that created the text engine.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <exception cref="ArgumentNullException">Throws if the <see cref="TextEngine"/> handle is null or 0x0.</exception>
    /// <seealso cref="CreateSurfaceTextEngine()"/>
    public static void DestroySurfaceTextEngine(TextEngine engine) {
        if (engine.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(engine), "Engine cannot be null.");
        }
        TTF_DestroySurfaceTextEngine(engine.Handle);
    }

    /// <summary>
    /// Destroy a text object.
    /// </summary>
    /// <param name="text">The <see cref="Text"/> to destroy.</param>
    /// <remarks>
    /// <para>This function frees all resources associated with the text object.</para>
    /// <para>This function should be called on the thread that created the text object.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <exception cref="ArgumentNullException">Throws if the <see cref="Text"/> handle is null or 0x0.</exception>
    /// <seealso cref="CreateText(TextEngine, Font, string, int)"/>
    public static void DestroyText(Text text) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        TTF_DestroyText(text.Handle);
    }

    /// <summary>
    /// Draw a text object using a renderer text engine.
    /// </summary>
    /// <param name="text">The <see cref="Text"/> to draw.</param>
    /// <param name="x">The x-coordinate where to draw the text.</param>
    /// <param name="y">The y-coordinate where to draw the text.</param>
    /// <remarks>
    /// <para>The text is drawn at the specified (x, y) coordinates using the associated renderer text engine.</para>
    /// <para>This function should be called on the thread that created the text object and its renderer.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <exception cref="ArgumentNullException">Throws if the <see cref="Text"/> handle is null or 0x0.</exception>
    /// <seealso cref="CreateRendererTextEngine(nint)"/>
    /// <seealso cref="SetTextPosition(Text, int, int)"/>
    public static bool DrawRendererText(Text text, float x, float y) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_DrawRendererText(text.Handle, x, y);
    }

    /// <summary>
    /// Draw a text object to an SDL surface.
    /// </summary>
    /// <param name="text">The <see cref="Text"/> to draw.</param>
    /// <param name="x">The x-coordinate where to draw the text.</param>
    /// <param name="y">The y-coordinate where to draw the text.</param>
    /// <param name="surface">A pointer to the SDL surface to draw to.</param>
    /// <remarks>
    /// <para>The text is drawn at the specified (x, y) coordinates on the provided surface.</para>
    /// <para>This function should be called on the thread that created the text object and surface.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <exception cref="ArgumentNullException">Throws if the <see cref="Text"/> or <paramref name="surface"/> handle is null or 0x0.</exception>
    /// <seealso cref="CreateSurfaceTextEngine()"/>
    public static bool DrawSurfaceText(Text text, int x, int y, nint surface) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        if (surface == nint.Zero) {
            throw new ArgumentNullException(nameof(surface), "Surface cannot be null.");
        }
        return TTF_DrawSurfaceText(text.Handle, x, y, surface);
    }

    /// <summary>
    /// Check if a font contains a specific glyph for a Unicode character.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <param name="ch">The Unicode character code to check.</param>
    /// <remarks>
    /// <para>This function checks if the font contains a glyph for the specified Unicode character.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true"/> if the glyph exists, or <see langword="false"/> otherwise; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <seealso cref="GetGlyphImage(Font, int, ImageType)"/>
    public static bool FontHasGlyph(Font font, int ch) {
        bool result = TTF_FontHasGlyph(font.Handle, ch);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to check if font has glyph for character {ch}. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>
    /// Get the font ascent.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <remarks>
    /// <para>The ascent is the distance from the baseline to the top of most characters in the font.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the font ascent on success, or 0 on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <seealso cref="GetFontDescent(Font)"/>
    /// <seealso cref="GetFontHeight(Font)"/>
    public static int GetFontAscent(Font font) {
        int result = TTF_GetFontAscent(font.Handle);
        if (result < 0) {
            Sdl.LogError(LogCategory.Error, $"Failed to get font ascent. SDL Error: {Sdl.GetError()}");
            return 0;
        }
        return result;
    }

    /// <summary>
    /// Get the font descent.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <remarks>
    /// <para>The descent is the distance from the baseline to the bottom of most characters in the font.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the font descent on success, or 0 on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <seealso cref="GetFontAscent(Font)"/>
    /// <seealso cref="GetFontHeight(Font)"/>
    public static int GetFontDescent(Font font) {
        return TTF_GetFontDescent(font.Handle);
    }

    /// <summary>
    /// Get the text direction of a font.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <remarks>
    /// <para>This function returns the text direction set for the font, such as left-to-right or right-to-left.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the <see cref="Direction"/> of the font.</returns>
    /// <seealso cref="SetFontDirection(Font, Direction)"/>
    public static Direction GetFontDirection(Font font) {
        return TTF_GetFontDirection(font.Handle);
    }

    /// <summary>
    /// Get the DPI settings of a font.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <param name="hdpi">The output horizontal DPI value.</param>
    /// <param name="vdpi">The output vertical DPI value.</param>
    /// <remarks>
    /// <para>This function retrieves the horizontal and vertical DPI settings used by the font.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <seealso cref="SetFontSizeDPI(Font, float, int, int)"/>
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

    /// <summary>
    /// Get the DPI settings of a font.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <remarks>
    /// <para>This function retrieves the horizontal and vertical DPI settings used by the font.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns> Returns the output horizontal DPI value and vertical DPI value as a <see cref="Size"/>. This may be empty. If so, call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <seealso cref="SetFontSizeDPI(Font, float, int, int)"/>
    public static Size GetFontDPI(Font font) {
        if (GetFontDPI(font, out int hdpi, out int vdpi)) {
            return new Size(hdpi, vdpi);
        }

        return new();
    }

    /// <summary>
    /// Get the font family name.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <remarks>
    /// <para>This function retrieves the family name of the font, such as "Arial" or "Times New Roman".</para>
    /// <para>The returned string is invalid after calling <see cref="CloseFont(Font)"/>.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the font family name as a string, or an empty string on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <seealso cref="GetFontStyleName(Font)"/>
    public static string GetFontFamilyName(Font font) {
        string result = TTF_GetFontFamilyName(font.Handle);
        if (string.IsNullOrEmpty(result)) {
            Sdl.LogError(LogCategory.System, $"Failed to get font family name. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>
    /// Get the font generation number.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <remarks>
    /// <para>The generation number is incremented each time the font's internal data is modified, useful for detecting changes.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the font generation number, or 0 on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <exception cref="InvalidOperationException">Throws if the generation number cannot be retrieved.</exception>
    public static int GetFontGeneration(Font font) {
        int generation = TTF_GetFontGeneration(font.Handle);
        if (generation == 0) {
            throw new InvalidOperationException($"Failed to get font generation. SDL Error: {Sdl.GetError()}");
        }
        return generation;
    }

    /// <summary>
    /// Get the font height.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <remarks>
    /// <para>The height is the maximum height of the font, typically the sum of ascent and descent.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the font height on success, or 0 on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <seealso cref="GetFontAscent(Font)"/>
    /// <seealso cref="GetFontDescent(Font)"/>
    public static int GetFontHeight(Font font) {
        int result = TTF_GetFontHeight(font.Handle);
        if (result < 0) {
            Sdl.LogError(LogCategory.Error, $"Failed to get font height. SDL Error: {Sdl.GetError()}");
            return 0;
        }
        return result;
    }

    /// <summary>
    /// Get the font hinting setting.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <remarks>
    /// <para>This function retrieves the hinting setting for the font, which controls how glyphs are rendered.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the <see cref="Hinting"/> setting of the font.</returns>
    /// <seealso cref="SetFontHinting(Font, Hinting)"/>
    public static Hinting GetFontHinting(Font font) {
        return TTF_GetFontHinting(font.Handle);
    }

    /// <summary>
    /// Get the font kerning setting.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <remarks>
    /// <para>This function checks if kerning is enabled for the font, which adjusts spacing between certain character pairs.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true"/> if kerning is enabled, or <see langword="false"/> otherwise; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <seealso cref="SetFontKerning(Font, bool)"/>
    public static bool GetFontKerning(Font font) {
        bool result = TTF_GetFontKerning(font.Handle);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to get font kerning. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>
    /// Get the recommended line spacing for a font.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <remarks>
    /// <para>The line skip is the recommended spacing between lines of text for the font.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the line skip value, or 0 on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <seealso cref="SetFontLineSkip(Font, int)"/>
    public static int GetFontLineSkip(Font font) {
        return TTF_GetFontLineSkip(font.Handle);
    }

    /// <summary>
    /// Get the font outline width.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <remarks>
    /// <para>This function retrieves the outline width set for the font, used for rendering outlined text.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the outline width on success, or 0 on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <seealso cref="SetFontOutline(Font, int)"/>
    public static int GetFontOutline(Font font) {
        int result = TTF_GetFontOutline(font.Handle);
        if (result < 0) {
            Sdl.LogError(LogCategory.Error, $"Failed to get font outline. SDL Error: {Sdl.GetError()}");
            return 0;
        }
        return result;
    }

    /// <summary>
    /// Get the font properties.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <remarks>
    /// <para>This function retrieves the properties associated with the font, such as rendering settings.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the font properties, or 0 on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <exception cref="InvalidOperationException">Throws if the properties cannot be retrieved.</exception>
    /// <seealso cref="OpenFontWithProperties(int)"/>
    public static int GetFontProperties(Font font) {
        int props = TTF_GetFontProperties(font.Handle);
        if (props == 0) {
            throw new InvalidOperationException($"Failed to get font properties. SDL Error: {Sdl.GetError()}");
        }
        return props;
    }

    /// <summary>
    /// Get the script tag for a font.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <remarks>
    /// <para>This function retrieves the script tag associated with the font, such as 'latn' for Latin.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the script tag as an integer, or 0 on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <seealso cref="SetFontScript(Font, int)"/>
    public static int GetFontScript(Font font) {
        return TTF_GetFontScript(font.Handle);
    }

    /// <summary>
    /// Check if Signed Distance Field (SDF) rendering is enabled for a font.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <remarks>
    /// <para>SDF rendering can improve the quality of scaled text rendering.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true"/> if SDF rendering is enabled, or <see langword="false"/> otherwise; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <seealso cref="SetFontSDF(Font, bool)"/>
    public static bool GetFontSDF(Font font) {
        bool result = TTF_GetFontSDF(font.Handle);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to get font SDF. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>
    /// Get the point size of a font.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <remarks>
    /// <para>This function retrieves the point size of the font in points.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the point size as a float, or 0.0f on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <exception cref="InvalidOperationException">Throws if the point size cannot be retrieved.</exception>
    /// <seealso cref="SetFontSize(Font, float)"/>
    public static float GetFontSize(Font font) {
        float size = TTF_GetFontSize(font.Handle);
        if (size <= 0.01f) {
            throw new InvalidOperationException($"Failed to get font size. SDL Error: {Sdl.GetError()}");
        }
        return size;
    }

    /// <summary>
    /// Get the font style.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <remarks>
    /// <para>This function retrieves the style of the font, such as bold or italic.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the <see cref="FontStyle"/> of the font.</returns>
    /// <seealso cref="SetFontStyle(Font, FontStyle)"/>
    public static FontStyle GetFontStyle(Font font) {
        return TTF_GetFontStyle(font.Handle);
    }

    /// <summary>
    /// Get the font style name.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <remarks>
    /// <para>This function retrieves the style name of the font, such as "Bold" or "Italic".</para>
    /// <para>The returned string is invalid after calling <see cref="CloseFont(Font)"/>.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the font style name as a string, or an empty string on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <seealso cref="GetFontFamilyName(Font)"/>
    public static string GetFontStyleName(Font font) {
        string result = TTF_GetFontStyleName(font.Handle);
        if (string.IsNullOrEmpty(result)) {
            Sdl.LogError(LogCategory.System, $"Failed to get font style name. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>
    /// Get the font weight.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <remarks>
    /// <para>This function retrieves the weight of the font, such as normal or bold.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the <see cref="FontWeight"/> of the font.</returns>
    public static FontWeight GetFontWeight(Font font) {
        return TTF_GetFontWeight(font.Handle);
    }

    /// <summary>
    /// Get the text wrapping alignment for a font.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <remarks>
    /// <para>This function retrieves the horizontal alignment used for text wrapping, such as left, center, or right.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the <see cref="HorizontalAlignment"/> setting for text wrapping.</returns>
    /// <seealso cref="SetFontWrapAlignment(Font, HorizontalAlignment)"/>
    public static HorizontalAlignment GetFontWrapAlignment(Font font) {
        return TTF_GetFontWrapAlignment(font.Handle);
    }

    /// <summary>
    /// Get the version of the FreeType library used by SDL_ttf.
    /// </summary>
    /// <param name="major">The output major version number.</param>
    /// <param name="minor">The output minor version number.</param>
    /// <param name="patch">The output patch version number.</param>
    /// <remarks>
    /// <para>This function retrieves the version of the FreeType library linked with SDL_ttf.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <seealso cref="GetHarfBuzzVersion(out int, out int, out int)"/>
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

    /// <summary>
    /// Get the image for a specific glyph in a font.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <param name="ch">The Unicode character code of the glyph.</param>
    /// <param name="image_type">The type of image to generate (e.g., solid, shaded, blended).</param>
    /// <remarks>
    /// <para>This function returns a surface containing the rendered glyph for the specified character.</para>
    /// <para>The caller is responsible for freeing the returned surface.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns a pointer to the surface containing the glyph image, or <see langword="null"/> on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Throws if the <paramref name="image_type"/> is invalid.</exception>
    /// <seealso cref="GetGlyphImageForIndex(Font, int, ImageType)"/>
    public static nint GetGlyphImage(Font font, int ch, ImageType image_type) {
        if (!Enum.IsDefined(image_type)) {
            throw new ArgumentOutOfRangeException(nameof(image_type), "Invalid image type value.");
        }
        return TTF_GetGlyphImage(font.Handle, ch, image_type);
    }

    /// <summary>
    /// Get the image for a specific glyph by index in a font.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <param name="glyph_index">The index of the glyph in the font.</param>
    /// <param name="image_type">The type of image to generate (e.g., solid, shaded, blended).</param>
    /// <remarks>
    /// <para>This function returns a surface containing the rendered glyph for the specified glyph index.</para>
    /// <para>The caller is responsible for freeing the returned surface.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns a pointer to the surface containing the glyph image, or <see langword="null"/> on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Throws if the <paramref name="image_type"/> is invalid.</exception>
    /// <seealso cref="GetGlyphImage(Font, int, ImageType)"/>
    public static nint GetGlyphImageForIndex(Font font, int glyph_index, ImageType image_type) {
        if (!Enum.IsDefined(image_type)) {
            throw new ArgumentOutOfRangeException(nameof(image_type), "Invalid image type value.");
        }
        return TTF_GetGlyphImageForIndex(font.Handle, glyph_index, image_type);
    }

    /// <summary>
    /// Get the kerning value between two glyphs.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <param name="previous_ch">The Unicode character code of the previous glyph.</param>
    /// <param name="ch">The Unicode character code of the current glyph.</param>
    /// <param name="kerning">The kerning value to retrieve.</param>
    /// <remarks>
    /// <para>This function retrieves the kerning adjustment between two glyphs, if any.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <seealso cref="GetFontKerning(Font)"/>
    public static bool GetGlyphKerning(Font font, int previous_ch, int ch, int kerning) {
        bool result = TTF_GetGlyphKerning(font.Handle, previous_ch, ch, kerning);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to get glyph kerning for characters {previous_ch} and {ch}. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>
    /// Get the metrics for a specific glyph.
    /// </summary>
    /// <param name="font">The <see cref="Font"/> to query.</param>
    /// <param name="ch">The Unicode character code of the glyph.</param>
    /// <param name="minx">Pointer to store the minimum x-coordinate of the glyph.</param>
    /// <param name="maxx">Pointer to store the maximum x-coordinate of the glyph.</param>
    /// <param name="miny">Pointer to store the minimum y-coordinate of the glyph.</param>
    /// <param name="maxy">Pointer to store the maximum y-coordinate of the glyph.</param>
    /// <param name="advance">Pointer to store the advance width of the glyph.</param>
    /// <remarks>
    /// <para>This function retrieves the metrics (bounding box and advance) for the specified glyph.</para>
    /// <para>This function should be called on the thread that created the font.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <seealso cref="FontHasGlyph(Font, int)"/>
    public static bool GetGlyphMetrics(Font font, int ch, nint minx, nint maxx, nint miny, nint maxy, nint advance) {
        bool result = TTF_GetGlyphMetrics(font.Handle, ch, minx, maxx, miny, maxy, advance);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to get glyph metrics for character {ch}. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    /// <summary>
    /// Get the script tag for a Unicode character.
    /// </summary>
    /// <param name="ch">The Unicode character code to query.</param>
    /// <remarks>
    /// <para>This function retrieves the script tag (e.g., 'latn' for Latin) for the specified Unicode character.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the script tag as an integer, or 0 on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Throws if the character code is not a valid Unicode code point.</exception>
    /// <seealso cref="SetFontScript(Font, int)"/>
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

    /// <summary>
    /// Get the draw data for a GPU text object.
    /// </summary>
    /// <param name="text">A pointer to the text object.</param>
    /// <remarks>
    /// <para>This function retrieves the data needed to draw the text using a GPU text engine.</para>
    /// <para>This function should be called on the thread that created the text object.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns a pointer to the draw data, or <see langword="null"/> on failure; call <see cref="Sdl.GetError"/> for more information.</returns>
    /// <exception cref="ArgumentNullException">Throws if the <paramref name="text"/> is null or 0x0.</exception>
    /// <seealso cref="CreateGPUTextEngine(nint)"/>
    public static nint GetGPUTextDrawData(nint text) {
        if (text == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_GetGPUTextDrawData(text);
    }

    /// <summary>
    /// Get the winding order of a GPU text engine.
    /// </summary>
    /// <param name="engine">A pointer to the GPU text engine.</param>
    /// <remarks>
    /// <para>This function retrieves the winding order used by the GPU text engine for rendering.</para>
    /// <para>This function should be called on the thread that created the text engine.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <returns>Returns the <see cref="GPUTextEngineWinding"/> setting of the text engine.</returns>
    /// <exception cref="ArgumentNullException">Throws if the <paramref name="engine"/> is null or 0x0.</exception>
    /// <seealso cref="SetGPUTextEngineWinding(nint, GPUTextEngineWinding)"/>
    public static GPUTextEngineWinding GetGPUTextEngineWinding(nint engine) {
        if (engine == nint.Zero) {
            throw new ArgumentNullException(nameof(engine), "Engine cannot be null.");
        }
        return TTF_GetGPUTextEngineWinding(engine);
    }

    /// <summary>
    /// Get the version of the HarfBuzz library used by SDL_ttf.
    /// </summary>
    /// <param name="major">The output major version number.</param>
    /// <param name="minor">The output minor version number.</param>
    /// <param name="patch">The output patch version number.</param>
    /// <remarks>
    /// <para>This function retrieves the version of the HarfBuzz library linked with SDL_ttf.</para>
    /// <para>This function is available since SDL_ttf 3.0.0.</para>
    /// </remarks>
    /// <seealso cref="GetFreeTypeVersion(out int, out int, out int)"/>
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

    public static SubString GetNextTextSubString(Text text, SubString substring) {
        _ = GetNextTextSubString(text, substring, out SubString next);
        return next;
    }

    public static int GetNumFontFaces(Font font) {
        int result = TTF_GetNumFontFaces(font.Handle);
        if (result < 0) {
            Sdl.LogError(LogCategory.Error, $"Failed to get number of font faces. SDL Error: {Sdl.GetError()}");
            return 0;
        }
        return result;
    }

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

    public static SubString GetPreviousTextSubString(Text text, SubString substring) {
        _ = GetPreviousTextSubString(text, substring, out SubString previous);
        return previous;
    }

    public static bool GetStringSize(Font font, string text, ulong length, int w, int h) {
        bool result = TTF_GetStringSize(font.Handle, text, length, w, h);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to get string size for text '{text}'. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    public static bool GetStringSize(Font font, string text, ulong length, Size size) {
        return GetStringSize(font, text, length, size.Width, size.Height);
    }

    public static bool GetStringSizeWrapped(Font font, string text, ulong length, int wrap_width, int w, int h) {
        bool result = TTF_GetStringSizeWrapped(font.Handle, text, length, wrap_width, w, h);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to get wrapped string size for text '{text}'. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

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

    public static bool GetTextColorFloat(Text text, out FColor color) {
        GetTextColorFloat(text, out float r, out float g, out float b, out float a);
        color = new FColor() { R = r, G = g, B = b, A = a };
        return true;
    }

    public static Direction GetTextDirection(Text text) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_GetTextDirection(text.Handle);
    }

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

    public static Point GetTextPosition(Text text) {
        GetTextPosition(text, out int x, out int y);
        return new Point() { X = x, Y = y };
    }

    public static uint GetTextProperties(nint text) {
        if (text == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_GetTextProperties(text);
    }

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

    public static Size GetTextSize(Text text) {
        GetTextSize(text, out int w, out int h);
        return new Size() { Width = w, Height = h };
    }

    public static bool GetTextSubString(Text text, int offset, SubString substring) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_GetTextSubString(text.Handle, offset, substring.Handle);
    }

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

    public static SubString GetTextSubStringForLine(Text text, int line) {
        bool result = GetTextSubStringForLine(text, line, out SubString substring);
        if (!result) {
            Sdl.LogError(LogCategory.Error, "Failed to get text substring for line.");
            return new SubString();
        }
        return substring;
    }

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

    public static bool GetTextSubStringForPoint(Text text, Point point, out SubString substring) {
        return GetTextSubStringForPoint(text, point.X, point.Y, out substring);
    }

    public static SubString GetTextSubStringForPoint(Text text, int x, int y) {
        bool result = GetTextSubStringForPoint(text, x, y, out SubString substring);
        if (!result) {
            Sdl.LogError(LogCategory.Error, "Failed to get text substring for point.");
            return new SubString();
        }
        return substring;
    }

    public static SubString GetTextSubStringForPoint(Text text, Point point) {
        bool result = GetTextSubStringForPoint(text, point, out SubString substring);
        if (!result) {
            Sdl.LogError(LogCategory.Error, "Failed to get text substring for point.");
            return new SubString();
        }
        return substring;
    }

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

    public static bool InsertTextString(Text text, int offset, string str, ulong length) {
        ArgumentException.ThrowIfNullOrEmpty(str);

        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_InsertTextString(text.Handle, offset, str, length);
    }

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

    public static Size MeasureString(Font font, string text, int max_width) {
        MeasureString(font, text, max_width, out Size measuredSize);
        return measuredSize;
    }

    public static Size MeasureString(Font font, string text) {
        return MeasureString(font, text, 0);
    }

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

    public static Font OpenFontWithProperties(int props) {
        Font font = TTF_OpenFontWithProperties(props);
        return font;
    }

    public static void Quit() {
        TTF_Quit();
    }

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

    public static nint RenderTextSolid(Font font, string text, ulong length, Color fg) {
        ArgumentException.ThrowIfNullOrEmpty(text);
        if (font.Handle == nint.Zero) {
            return nint.Zero;
        }
        return TTF_RenderText_Solid(font.Handle, text, length, fg);
    }

    public static nint RenderTextSolid(Font font, string text, Color fg) {
        return RenderTextSolid(font, text, (ulong)text.Length, fg);
    }

    public static nint RenderTextSolidWrapped(Font font, string text, ulong length, Color fg, int wrapLength) {
        ArgumentException.ThrowIfNullOrEmpty(text);
        return TTF_RenderText_Solid_Wrapped(font.Handle, text, length, fg, wrapLength);
    }

    public static bool SetFontDirection(Font font, Direction direction) {
        bool result = TTF_SetFontDirection(font.Handle, direction);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to set font direction. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    public static void SetFontHinting(Font font, Hinting hinting) {
        if (!Enum.IsDefined(hinting)) {
            throw new ArgumentOutOfRangeException(nameof(hinting), "Invalid hinting value.");
        }
        TTF_SetFontHinting(font.Handle, hinting);
    }

    public static void SetFontKerning(Font font, bool enabled) {
        TTF_SetFontKerning(font.Handle, enabled);
    }

    public static bool SetFontLanguage(Font font, string language_bcp47) {
        bool result = TTF_SetFontLanguage(font.Handle, language_bcp47);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to set font language. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    public static void SetFontLineSkip(Font font, int lineskip) {
        TTF_SetFontLineSkip(font.Handle, lineskip);
    }

    public static bool SetFontOutline(Font font, int outline) {
        bool result = TTF_SetFontOutline(font.Handle, outline);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to set font outline. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    public static bool SetFontScript(Font font, int script) {
        bool result = TTF_SetFontScript(font.Handle, script);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to set font script. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    public static bool SetFontSDF(Font font, bool enabled) {
        bool result = TTF_SetFontSDF(font.Handle, enabled);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to set font SDF. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

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

    public static bool SetFontSizeDPI(Font font, float ptsize, int hdpi, int vdpi) {
        bool result = TTF_SetFontSizeDPI(font.Handle, ptsize, hdpi, vdpi);
        if (!result) {
            Sdl.LogError(LogCategory.Error, $"Failed to set font size DPI. SDL Error: {Sdl.GetError()}");
        }
        return result;
    }

    public static void SetFontStyle(Font font, FontStyle style) {
        if (!Enum.IsDefined(style)) {
            throw new ArgumentOutOfRangeException(nameof(style), "Invalid font style.");
        }
        TTF_SetFontStyle(font.Handle, style);
    }

    public static void SetFontWrapAlignment(Font font, HorizontalAlignment align) {
        if (!Enum.IsDefined(align)) {
            throw new ArgumentOutOfRangeException(nameof(align), "Invalid horizontal alignment value.");
        }
        TTF_SetFontWrapAlignment(font.Handle, align);
    }

    public static void SetGPUTextEngineWinding(nint engine, GPUTextEngineWinding winding) {
        if (engine == nint.Zero) {
            throw new ArgumentNullException(nameof(engine), "Engine cannot be null.");
        }
        TTF_SetGPUTextEngineWinding(engine, winding);
    }

    public static bool SetTextColor(Text text, byte r, byte g, byte b, byte a) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextColor(text.Handle, r, g, b, a);
    }

    public static bool SetTextColor(Text text, Color color) {
        return SetTextColor(text, color.R, color.G, color.B, color.A);
    }

    public static bool SetTextColorFloat(Text text, float r, float g, float b, float a) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextColorFloat(text.Handle, r, g, b, a);
    }

    public static bool SetTextColorFloat(Text text, FColor color) {
        return SetTextColorFloat(text, color.R, color.G, color.B, color.A);
    }

    public static bool SetTextDirection(Text text, Direction direction) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextDirection(text.Handle, direction);
    }

    public static bool SetTextEngine(Text text, TextEngine engine) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }

        if (engine.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(engine), "Engine cannot be null.");
        }

        return TTF_SetTextEngine(text.Handle, engine.Handle);
    }

    public static bool SetTextFont(Text text, Font font) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextFont(text.Handle, font.Handle);
    }

    public static bool SetTextPosition(Text text, int x, int y) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextPosition(text.Handle, x, y);
    }

    public static bool SetTextPosition(Text text, Point position) {
        return SetTextPosition(text, position.X, position.Y);
    }

    public static bool SetTextScript(Text text, int script) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextScript(text.Handle, script);
    }

    public static bool SetTextString(Text text, string str, ulong length) {
        ArgumentException.ThrowIfNullOrEmpty(str);

        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }

        return TTF_SetTextString(text.Handle, str, length);
    }

    public static bool SetTextWrapWhitespaceVisible(Text text, bool visible) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextWrapWhitespaceVisible(text.Handle, visible);
    }

    public static bool SetTextWrapWidth(Text text, int wrap_width) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_SetTextWrapWidth(text.Handle, wrap_width);
    }

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

    /// <summary>
    /// May not work properly
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static string TagToString(int tag, ulong size) {
        Span<char> buffer = stackalloc char[5];
        TTF_TagToString(tag, new string(buffer), size);
        return new string(buffer);
    }

    public static bool TextWrapWhitespaceVisible(Text text) {
        if (text.Handle == nint.Zero) {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }
        return TTF_TextWrapWhitespaceVisible(text.Handle);
    }

    public static int TtfVersion() => TTF_Version();

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
    [return:MarshalUsing(typeof(OwnedFontMarshaller))]
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