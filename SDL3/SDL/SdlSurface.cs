using System;
using System.Runtime.InteropServices;
using SharpSDL3.Enums;
using SharpSDL3.Structs;

namespace SharpSDL3;

public static partial class Sdl
{
    
    /// <summary>Clear a surface with a specific color, with floating point precision.</summary>
    /// <param name="surface">the <see cref="Surface" /> to clear.</param>
    /// <param name="r">the red component of the pixel, normally in the range 0-1.</param>
    /// <param name="g">the green component of the pixel, normally in the range 0-1.</param>
    /// <param name="b">the blue component of the pixel, normally in the range 0-1.</param>
    /// <param name="a">the alpha component of the pixel, normally in the range 0-1.</param>
    /// <remarks>
    /// This function handles all surface formats, and ignores any clip rectangle.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool ClearSurface(nint surface, float r, float g, float b, float a) {
        if (surface != nint.Zero) return SDL_ClearSurface(surface, r, g, b, a);
        LogWarn(LogCategory.System, "ClearSurface: Surface pointer is null.");
        return false;
    }

    /// <summary>Compose a custom blend mode for renderers.</summary>
    /// <param name="srcColorFactor">the <see cref="BlendFactor" /> applied to the red, green, and blue components of the source pixels.</param>
    /// <param name="dstColorFactor">the <see cref="BlendFactor" /> applied to the red, green, and blue components of the destination pixels.</param>
    /// <param name="colorOperation">the <see cref="BlendOperation" /> used to combine the red, green, and blue components of the source and destination pixels.</param>
    /// <param name="srcAlphaFactor">the <see cref="BlendFactor" /> applied to the alpha component of the source pixels.</param>
    /// <param name="dstAlphaFactor">the <see cref="BlendFactor" /> applied to the alpha component of the destination pixels.</param>
    /// <param name="alphaOperation">the <see cref="BlendOperation" /> used to combine the alpha component of the source and destination pixels.</param>
    /// <remarks>
    /// The functions <see cref="SetRenderDrawBlendMode(nint, BlendMode)" /> and <see cref="SetTextureBlendMode" /> accept the <see cref="BlendMode" /> returned by this function if the renderer supports it.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetRenderDrawBlendMode(nint, BlendMode)" />
    /// <seealso cref="GetRenderDrawBlendMode" />
    /// <seealso cref="SetTextureBlendMode" />
    /// <seealso cref="GetTextureBlendMode" />
    /// </remarks>
    /// <returns>Returns an <see cref="BlendMode" />that represents the chosen factors and operations.</returns>
    public static BlendMode ComposeCustomBlendMode(BlendFactor srcColorFactor, BlendFactor dstColorFactor, BlendOperation colorOperation, BlendFactor srcAlphaFactor, BlendFactor dstAlphaFactor, BlendOperation alphaOperation) {
        if (!Enum.IsDefined(srcColorFactor) ||
            !Enum.IsDefined(dstColorFactor) ||
            !Enum.IsDefined(colorOperation) ||
            !Enum.IsDefined(srcAlphaFactor) ||
            !Enum.IsDefined(dstAlphaFactor) ||
            !Enum.IsDefined(alphaOperation)) {
            LogError(LogCategory.Error, "ComposeCustomBlendMode: Invalid blend factors or operations provided.");
            throw new ArgumentException("Invalid blend factors or operations.");
        }

        var blendMode = SDL_ComposeCustomBlendMode(srcColorFactor, dstColorFactor, colorOperation, srcAlphaFactor, dstAlphaFactor, alphaOperation);
        if (blendMode == 0) {
            LogError(LogCategory.Error, "ComposeCustomBlendMode: Failed to compose custom blend mode.");
        }

        return (BlendMode)blendMode;
    }

    /// <summary>Copy a block of pixels of one format to another format.</summary>
    /// <param name="width">the width of the block to copy, in pixels.</param>
    /// <param name="height">the height of the block to copy, in pixels.</param>
    /// <param name="srcFormat">an <see cref="PixelFormat" /> value of the src pixels format.</param>
    /// <param name="src">a pointer to the source pixels.</param>
    /// <param name="srcPitch">the pitch of the source pixels, in bytes.</param>
    /// <param name="dstFormat">an <see cref="PixelFormat" /> value of the dst pixels format.</param>
    /// <param name="dst">a pointer to be filled in with new pixel data.</param>
    /// <param name="dstPitch">the pitch of the destination pixels, in bytes.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: The same destination pixels should not be used from two threads at once. It is safe to use the same source pixels from multiple threads.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ConvertPixelsAndColorspace" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool ConvertPixels(int width, int height, PixelFormat srcFormat, nint src, int srcPitch, PixelFormat dstFormat, nint dst, int dstPitch) {
        if (src != nint.Zero && dst != nint.Zero)
            return SDL_ConvertPixels(width, height, srcFormat, src, srcPitch, dstFormat, dst, dstPitch);
        LogWarn(LogCategory.System, "ConvertPixels: Source or destination pointer is null.");
        return false;
    }

    /// <summary>Copy a block of pixels of one format and colorspace to another format and colorspace.</summary>
    /// <param name="width">the width of the block to copy, in pixels.</param>
    /// <param name="height">the height of the block to copy, in pixels.</param>
    /// <param name="srcFormat">an <see cref="PixelFormat" /> value of the src pixels format.</param>
    /// <param name="srcColorspace">an <see cref="Colorspace" /> value describing the colorspace of the src pixels.</param>
    /// <param name="srcProperties">an SDL_PropertiesID with additional source color properties, or 0.</param>
    /// <param name="src">a pointer to the source pixels.</param>
    /// <param name="srcPitch">the pitch of the source pixels, in bytes.</param>
    /// <param name="dstFormat">an <see cref="PixelFormat" /> value of the dst pixels format.</param>
    /// <param name="dstColorspace">an <see cref="Colorspace" /> value describing the colorspace of the dst pixels.</param>
    /// <param name="dstProperties">an SDL_PropertiesID with additional destination color properties, or 0.</param>
    /// <param name="dst">a pointer to be filled in with new pixel data.</param>
    /// <param name="dstPitch">the pitch of the destination pixels, in bytes.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: The same destination pixels should not be used from two threads at once. It is safe to use the same source pixels from multiple threads.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ConvertPixels" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool ConvertPixelsAndColorspace(int width, int height, PixelFormat srcFormat, Colorspace srcColorspace, uint srcProperties, nint src, int srcPitch, PixelFormat dstFormat, Colorspace dstColorspace, uint dstProperties, nint dst, int dstPitch) {
        if (src != nint.Zero && dst != nint.Zero)
            return SDL_ConvertPixelsAndColorspace(width, height, srcFormat, srcColorspace, srcProperties, src, srcPitch,
                dstFormat, dstColorspace, dstProperties, dst, dstPitch);
        LogWarn(LogCategory.System, "ConvertPixelsAndColorspace: Source or destination pointer is null.");
        return false;
    }

    /// <summary>Copy an existing surface to a new surface of the specified format.</summary>
    /// <param name="surface">the existing SDL_Surface structure to convert.</param>
    /// <param name="format">the new pixel format.</param>
    /// <remarks>
    /// This function is used to optimize images for faster repeat blitting. This
    /// is accomplished by converting the original and storing the result as a new
    /// surface. The new, optimized surface can then be used as the source for
    /// future blits, making them faster.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ConvertSurfaceAndColorspace" />
    /// <seealso cref="DestroySurface" />
    /// </remarks>
    /// <returns>(SDL_Surface *) Returns the new SDL_Surfacestructure that is created or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static nint ConvertSurface(nint surface, PixelFormat format) {
        if (surface != nint.Zero) return SDL_ConvertSurface(surface, format);
        LogWarn(LogCategory.System, "ConvertSurface: Surface pointer is null.");
        return nint.Zero;
    }

    /// <summary>Copy an existing surface to a new surface of the specified format and colorspace.</summary>
    /// <param name="surface">the existing SDL_Surface structure to convert.</param>
    /// <param name="format">the new pixel format.</param>
    /// <param name="palette">an optional palette to use for indexed formats, may be discarded.</param>
    /// <param name="colorspace">the new colorspace.</param>
    /// <param name="props">an SDL_PropertiesID with additional color properties, or 0.</param>
    /// <remarks>
    /// This function converts an existing surface to a new format and colorspace
    /// and returns the new surface. This will perform any pixel format and
    /// colorspace conversion needed.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ConvertSurface" />
    /// <seealso cref="DestroySurface" />
    /// </remarks>
    /// <returns>(SDL_Surface *) Returns the new SDL_Surfacestructure that is created or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint ConvertSurfaceAndColorspace(nint surface, PixelFormat format, nint palette, Colorspace colorspace, uint props) {
        if (surface != nint.Zero) return SDL_ConvertSurfaceAndColorspace(surface, format, palette, colorspace, props);
        LogWarn(LogCategory.System, "ConvertSurfaceAndColorspace: Surface pointer is null.");
        return nint.Zero;
    }
    
    /// <summary>Allocate a new surface with a specific pixel format.</summary>
    /// <param name="width">the width of the surface.</param>
    /// <param name="height">the height of the surface.</param>
    /// <param name="format">the <see cref="PixelFormat" /> for the new surface's pixel format.</param>
    /// <remarks>
    /// The pixels of the new surface are initialized to zero.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateSurfaceFrom" />
    /// <seealso cref="DestroySurface" />
    /// </remarks>
    /// <returns>(SDL_Surface *) Returns the new SDL_Surfacestructure that is created or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static nint CreateSurface(int width, int height, PixelFormat format) {
        if (width > 0 && height > 0) return SDL_CreateSurface(width, height, format);
        LogError(LogCategory.Error, "CreateSurface: Invalid width or height.");
        return nint.Zero;

    }

    /// <summary>Allocate a new surface with a specific pixel format and existing pixel data.</summary>
    /// <param name="width">the width of the surface.</param>
    /// <param name="height">the height of the surface.</param>
    /// <param name="format">the <see cref="PixelFormat" /> for the new surface's pixel format.</param>
    /// <param name="pixels">a pointer to existing pixel data.</param>
    /// <param name="pitch">the number of bytes between each row, including padding.</param>
    /// <remarks>
    /// No copy is made of the pixel data. Pixel data is not managed automatically;
    /// you must free the surface before you free the pixel data.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateSurface" />
    /// <seealso cref="DestroySurface" />
    /// </remarks>
    /// <returns>(SDL_Surface *) Returns the new SDL_Surface structure that is created or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static nint CreateSurfaceFrom(int width, int height, PixelFormat format, nint pixels, int pitch) {
        if (pixels == nint.Zero) {
            LogError(LogCategory.System, "CreateSurfaceFrom: Pixels pointer is null.");
            return nint.Zero;
        }

        if (Enum.IsDefined(format)) return SDL_CreateSurfaceFrom(width, height, format, pixels, pitch);
        LogError(LogCategory.Error, "CreateSurfaceFrom: Invalid pixel format.");
        return nint.Zero;

    }

    /// <summary>Create a palette and associate it with a surface.</summary>
    /// <param name="surface">the <see cref="Surface" /> structure to update.</param>
    /// <remarks>
    /// This function creates a palette compatible with the provided surface. The
    /// palette is then returned for you to modify, and the surface will
    /// automatically use the new palette in future operations. You do not need to
    /// destroy the returned palette, it will be freed when the reference count
    /// reaches 0, usually when the surface is destroyed.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetPaletteColors" />
    /// </remarks>
    /// <returns>(SDL_Palette *) Returns a new SDL_Palettestructure on success or <see langword="null" /> on failure (e.g. if the surface didn't have anindex format); call <see cref="GetError()" /> for more information.</returns>
    public static nint CreateSurfacePalette(nint surface) {
        if (surface != nint.Zero) return SDL_CreateSurfacePalette(surface);
        LogError(LogCategory.System, "CreateSurfacePalette: Surface pointer is null.");
        return nint.Zero;
    }

        /// <summary>Free a surface.</summary>
    /// <param name="surface">the <see cref="Surface" /> to free.</param>
    /// <remarks>
    /// It is safe to pass <see cref="nint.Zero" /> to this function.
    /// <para><strong>Thread Safety</strong>: No other thread should be using the surface when it is freed.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateSurface" />
    /// <seealso cref="CreateSurfaceFrom" />
    /// </remarks>
    public static void DestroySurface(nint surface) {
        if (surface == nint.Zero) {
            LogInfo(LogCategory.System, "Will destroy nothing.");
        }

        SDL_DestroySurface(surface);
    }
        
    /// <summary>Creates a new surface identical to the existing surface.</summary>
    /// <param name="surface">the surface to duplicate.</param>
    /// <remarks>
    /// If the original surface has alternate images, the new surface will have a reference to them as well.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="DestroySurface" />
    /// </remarks>
    /// <returns>(SDL_Surface *) Returns a copy of the surface or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static nint DuplicateSurface(nint surface) {
        if (surface != nint.Zero) return SDL_DuplicateSurface(surface);
        LogWarn(LogCategory.System, "DuplicateSurface: Surface pointer is null.");
        return nint.Zero;
    }
    
    /// <summary>Perform a fast fill of a rectangle with a specific color.</summary>
    /// <param name="dst">the <see cref="Surface" /> structure that is the drawing target.</param>
    /// <param name="rect">the <see cref="Rect" /> structure representing the rectangle to fill, or <see cref="nint.Zero" /> to fill the entire surface.</param>
    /// <param name="color">the color to fill with.</param>
    /// <remarks>
    /// color should be a pixel of the format used by the surface, and can be
    /// generated by <see cref="MapRgb" /> or <see cref="MapRgba" />. If
    /// the color value contains an alpha component then the destination is simply
    /// filled with that alpha information, no blending takes place.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="FillSurfaceRects" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static unsafe bool FillSurfaceRect(nint dst, Rect rect, uint color) {
        if (dst == nint.Zero) {
            LogWarn(LogCategory.System, "FillSurfaceRect: Destination pointer is null.");
            return false;
        }
        var rectPtr = Marshal.AllocHGlobal(sizeof(Rect));
        *(Rect*)rectPtr = rect;
        bool result = SDL_FillSurfaceRect(dst, rectPtr, color);
        if (!result) {
            LogError(LogCategory.Error, "FillSurfaceRect: Failed to fill surface rectangle.");
        }
        Marshal.FreeHGlobal(rectPtr);
        return result;
    }

    /// <summary>Perform a fast fill of a set of rectangles with a specific color.</summary>
    /// <param name="dst">the <see cref="Surface" /> structure that is the drawing target.</param>
    /// <param name="rects">an array of <see cref="Rect" />s representing the rectangles to fill.</param>
    /// <param name="color">the color to fill with.</param>
    /// <remarks>
    /// color should be a pixel of the format used by the surface, and can be
    /// generated by <see cref="MapRgb" /> or <see cref="MapRgba" />. If
    /// the color value contains an alpha component then the destination is simply
    /// filled with that alpha information, no blending takes place.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="FillSurfaceRect" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool FillSurfaceRects(nint dst, Span<Rect> rects, uint color) {
        if (dst == nint.Zero) {
            LogWarn(LogCategory.System, "FillSurfaceRects: Destination pointer is null.");
            return false;
        }
        if (rects.IsEmpty) {
            LogWarn(LogCategory.System, "FillSurfaceRects: Rectangles span is empty.");
            return false;
        }
        bool result = SDL_FillSurfaceRects(dst, rects, rects.Length, color);
        if (!result) {
            LogError(LogCategory.Error, "FillSurfaceRects: Failed to fill surface rectangles.");
        }
        return result;
    }

    /// <summary>Flip a surface vertically or horizontally.</summary>
    /// <param name="surface">the surface to flip.</param>
    /// <param name="flip">the direction to flip.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool FlipSurface(nint surface, FlipMode flip) {
        if (surface == nint.Zero) {
            LogWarn(LogCategory.System, "FlipSurface: Surface pointer is null.");
            return false;
        }
        return SDL_FlipSurface(surface, flip);
    }
    /// <summary>Get the additional alpha value used in blit operations.</summary>
    /// <param name="surface">the <see cref="Surface" /> structure to query.</param>
    /// <param name="alpha">a pointer filled in with the current alpha value.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetSurfaceColorMod" />
    /// <seealso cref="SetSurfaceAlphaMod" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool GetSurfaceAlphaMod(nint surface, out byte alpha) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "GetSurfaceAlphaMod: Surface pointer is null.");
            alpha = 0;
            return false;
        }
        bool result = SDL_GetSurfaceAlphaMod(surface, out alpha);
        if (!result) {
            LogError(LogCategory.Error, "GetSurfaceAlphaMod: Failed to retrieve surface alpha mod.");
        }
        return result;
    }

    /// <summary>
    /// Get the palette used by a surface.
    /// </summary>
    /// <param name="surface">the <see cref="Surface" /> to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetSurfacePalette" />
    /// </remarks>
    /// <returns>(SDL_Palette *) Returns a pointer to the palette used by the surface, or <see langword="null" /> if there is no palette used.</returns>
    public static nint GetSurfacePalette(nint surface) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "GetSurfacePalette: Surface pointer is null.");
            return nint.Zero;
        }
        var palette = SDL_GetSurfacePalette(surface);
        if (palette == nint.Zero) {
            LogError(LogCategory.Error, "GetSurfacePalette: Failed to retrieve surface palette.");
        }

        return palette;
    }

    /// <summary>Get the blend mode used for blit operations.</summary>
    /// <param name="surface">the <see cref="Surface" /> structure to query.</param>
    /// <param name="blendMode">a pointer filled in with the current <see cref="BlendMode" />.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetSurfaceBlendMode" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool GetSurfaceBlendMode(nint surface, nint blendMode) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "GetSurfaceBlendMode: Surface pointer is null.");
            return false;
        }
        bool result = SDL_GetSurfaceBlendMode(surface, blendMode);
        if (!result) {
            LogError(LogCategory.Error, "GetSurfaceBlendMode: Failed to retrieve surface blend mode.");
        }
        return result;
    }

    /// <summary>Get the clipping rectangle for a surface.</summary>
    /// <param name="surface">the <see cref="Surface" /> structure representing the surface to be clipped.</param>
    /// <param name="rect">a <see cref="Rect" /> structure filled in with the clipping rectangle for the surface.</param>
    /// <remarks>
    /// When surface is the destination of a blit, only the area within the clip rectangle is drawn into.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetSurfaceClipRect" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool GetSurfaceClipRect(nint surface, out Rect rect) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "GetSurfaceClipRect: Surface pointer is null.");
            rect = default;
            return false;
        }
        bool result = SDL_GetSurfaceClipRect(surface, out rect);
        if (!result) {
            LogError(LogCategory.Error, "GetSurfaceClipRect: Failed to retrieve surface clip rect.");
        }
        return result;
    }

    /// <summary>Get the color key (transparent pixel) for a surface.</summary>
    /// <param name="surface">the <see cref="Surface" /> structure to query.</param>
    /// <param name="key">a pointer filled in with the transparent pixel.</param>
    /// <remarks>
    /// The color key is a pixel of the format used by the surface, as generated by <see cref="MapRgb" />.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetSurfaceColorKey" />
    /// <seealso cref="SurfaceHasColorKey" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool GetSurfaceColorKey(nint surface, out uint key) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "GetSurfaceColorKey: Surface pointer is null.");
            key = 0;
            return false;
        }
        bool result = SDL_GetSurfaceColorKey(surface, out key);
        if (!result) {
            LogError(LogCategory.Error, "GetSurfaceColorKey: Failed to retrieve surface color key.");
        }
        return result;
    }

    /// <summary>Get the additional color value multiplied into blit operations.</summary>
    /// <param name="surface">the <see cref="Surface" /> structure to query.</param>
    /// <param name="r">a pointer filled in with the current red color value.</param>
    /// <param name="g">a pointer filled in with the current green color value.</param>
    /// <param name="b">a pointer filled in with the current blue color value.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetSurfaceAlphaMod" />
    /// <seealso cref="SetSurfaceColorMod(nint, byte, byte, byte)" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool GetSurfaceColorMod(nint surface, out byte r, out byte g, out byte b) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "GetSurfaceColorMod: Surface pointer is null.");
            r = g = b = 0;
            return false;
        }
        bool result = SDL_GetSurfaceColorMod(surface, out r, out g, out b);
        if (!result) {
            LogError(LogCategory.Error, "GetSurfaceColorMod: Failed to retrieve surface color mod.");
        }
        return result;
    }

    /// <summary>
    /// Get the colorspace used by a surface.
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <remarks>
    /// <para>The colorspace defaults to <see cref="Enums.Colorspace.SrgbLinear"/> for floating point formats,
    /// <see cref="Enums.Colorspace.Hdr10"/> for 10-bit formats, <see cref="Enums.Colorspace.Srgb"/> for other RGB surfaces and
    /// <see cref="Enums.Colorspace.Bt709Full"/> for YUV textures.
    /// </para>
    /// <para><strong>Thread Safety</strong>: This function can be called on different threads with different surfaces.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetSurfaceColorspace"/>
    /// </remarks>
    /// <returns>(SDL_Colorspace) Returns the colorspace used by the surface, or <see cref="Enums.Colorspace.Unknown"/> if the surface is <see langword="null"/>.</returns>
    public static Colorspace GetSurfaceColorspace(nint surface)
    {
        return SDL_GetSurfaceColorspace(surface);
    }

    /// <summary>Get an array including all versions of a surface.</summary>
    /// <param name="surface">the <see cref="Surface" /> structure to query.</param>
    /// <param name="count">a pointer filled in with the number of surface pointers returned, may be discarded.</param>
    /// <remarks>
    /// This returns all versions of a surface, with the surface being queried as
    /// the first element in the returned array.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="AddSurfaceAlternateImage" />
    /// <seealso cref="RemoveSurfaceAlternateImages" />
    /// <seealso cref="SurfaceHasAlternateImages" />
    /// </remarks>
    /// <returns>(SDL_Surface **) Returns a <see langword="null" /> terminated array ofSDL_Surface pointers or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information. This should be freedwith <see cref="Free" /> when it is no longer needed.</returns>
    public static Span<nint> GetSurfaceImages(nint surface, out int count) {
        var result = SDL_GetSurfaceImages(surface, out count);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "GetSurfaceImages: Failed to retrieve surface images.");
            return [];
        }

        if (count <= 0) {
            LogError(LogCategory.Error, "GetSurfaceImages: No images found.");
            return [];
        }

        Span<nint> images = new Span<IntPtr>(ref result);
        if (images == []) {
            LogError(LogCategory.Error, "GetSurfaceImages: Failed to create span for surface images.");
            return [];
        }

        if (images.Length != count) {
            LogError(LogCategory.Error, "GetSurfaceImages: Mismatch between count and span length.");
            return [];
        }

        for (var i = 0; i < count; i++)
        {
            if (images[i] != nint.Zero) continue;
            LogError(LogCategory.Error, $"GetSurfaceImages: Image at index {i} is null.");
            return [];
        }

        return images.ToArray();
    }

    /// <summary>Get the properties associated with a surface.</summary>
    /// <param name="surface">the <see cref="Surface" /> structure to query.</param>
    /// <remarks>
    /// The following properties are understood by SDL:
    /// <list type="bullet">
    /// <item>SDL_PROP_SURFACE_SDR_WHITE_POINT_FLOAT: for HDR10 and floating point surfaces, this defines the value of 100% diffuse white, with higher values being displayed in the High Dynamic Range headroom.This defaults to 203 for HDR10 surfaces and 1.0 for floating point surfaces.</item>
    /// <item>SDL_PROP_SURFACE_HDR_HEADROOM_FLOAT: for HDR10 and floating point surfaces, this defines the maximum dynamic range used by the content, in terms of the SDR white point.This defaults to 0.0, which disables tone mapping.</item>
    /// <item>SDL_PROP_SURFACE_TONEMAP_OPERATOR_STRING: the tone mapping operator used when compressing from a surface with high dynamic range to another with lower dynamic range. Currently this supports "chrome", which uses the same tone mapping that Chrome uses for HDR content, the form "*=N", where N is a floating point scale factor applied in linear space, and "none", which disables tone mapping. This defaults to "chrome".</item>
    /// <item>SDL_PROP_SURFACE_HOTSPOT_X_NUMBER: the hotspot pixel offset from the left edge of the image, if this surface is being used as a cursor.</item>
    /// <item>SDL_PROP_SURFACE_HOTSPOT_Y_NUMBER: the hotspot pixel offset from the top edge of the image, if this surface is being used as a cursor.</item>
    /// </list>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns a valid property ID on success or 0 on failure; call <see cref="GetError()" /> for more information.</returns>
    public static uint GetSurfaceProperties(nint surface) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "GetSurfaceProperties: Surface pointer is null.");
            return 0;
        }
        var properties = SDL_GetSurfaceProperties(surface);
        if (properties == 0) {
            LogError(LogCategory.Error, "GetSurfaceProperties: Failed to retrieve surface properties.");
        }
        return properties;
    }

    /// <summary>Set up a surface for directly accessing the pixels.</summary>
    /// <param name="surface">the <see cref="Surface" /> structure to be locked.</param>
    /// <remarks>
    /// Between calls to <see cref="LockSurface" /> /
    /// <see cref="UnlockSurface" />, you can write to and read from
    /// surface-&gt;pixels, using the pixel format stored in surface-&gt;format. Once
    /// you are done accessing the surface, you should use
    /// <see cref="UnlockSurface" /> to release it.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe. The locking referred to by this function is making the pixels available for direct access, not thread-safe locking.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="MUSTLOCK" />
    /// <seealso cref="UnlockSurface" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool LockSurface(nint surface) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "LockSurface: Surface pointer is null.");
            return false;
        }
        bool result = SDL_LockSurface(surface);
        if (!result) {
            LogError(LogCategory.Error, "LockSurface: Failed to lock surface.");
        }
        return result;
    }
    
    /// <summary>Map an RGB triple to an opaque pixel value for a given pixel format.</summary>
    /// <param name="format">a pointer to <see cref="PixelFormat" />Details describing the pixel format.</param>
    /// <param name="palette">an optional palette for indexed formats, may be discarded.</param>
    /// <param name="r">the red component of the pixel in the range 0-255.</param>
    /// <param name="g">the green component of the pixel in the range 0-255.</param>
    /// <param name="b">the blue component of the pixel in the range 0-255.</param>
    /// <remarks>
    /// This function maps the RGB color value to the specified pixel format and
    /// returns the pixel value best approximating the given RGB color value for
    /// the given pixel format.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread, as long as the palette is not modified.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetPixelFormatDetails" />
    /// <seealso cref="GetRgb" />
    /// <seealso cref="MapRgba" />
    /// <seealso cref="MapSurfaceRgb(nint, byte, byte, byte)" />
    /// </remarks>
    /// <returns>Returns a pixel value.</returns>
    public static uint MapRgb(nint format, nint palette, byte r, byte g, byte b) {
        if (format == nint.Zero || palette == nint.Zero) {
            LogError(LogCategory.Error, "MapRgb: Format or palette pointer is null.");
            return 0;
        }
        var color = SDL_MapRGB(format, palette, r, g, b);
        if (color == 0) {
            LogError(LogCategory.Error, "MapRgb: Failed to map RGB color.");
        }
        return color;
    }

    /// <summary>Map an RGBA quadruple to a pixel value for a given pixel format.</summary>
    /// <param name="format">a pointer to <see cref="PixelFormat" />Details describing the pixel format.</param>
    /// <param name="palette">an optional palette for indexed formats, may be discarded.</param>
    /// <param name="r">the red component of the pixel in the range 0-255.</param>
    /// <param name="g">the green component of the pixel in the range 0-255.</param>
    /// <param name="b">the blue component of the pixel in the range 0-255.</param>
    /// <param name="a">the alpha component of the pixel in the range 0-255.</param>
    /// <remarks>
    /// This function maps the RGBA color value to the specified pixel format and
    /// returns the pixel value best approximating the given RGBA color value for
    /// the given pixel format.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread, as long as the palette is not modified.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetPixelFormatDetails" />
    /// <seealso cref="GetRgba" />
    /// <seealso cref="MapRgb" />
    /// <seealso cref="MapSurfaceRgba(nint, byte, byte, byte, byte)" />
    /// </remarks>
    /// <returns>Returns a pixel value.</returns>
    public static uint MapRgba(nint format, nint palette, byte r, byte g, byte b, byte a) {
        if (format == nint.Zero || palette == nint.Zero) {
            LogError(LogCategory.Error, "MapRgba: Format or palette pointer is null.");
            return 0;
        }
        var color = SDL_MapRGBA(format, palette, r, g, b, a);
        if (color == 0) {
            LogError(LogCategory.Error, "MapRgba: Failed to map RGBA color.");
        }
        return color;
    }

    /// <summary>Map an RGB triple to an opaque pixel value for a surface.</summary>
    /// <param name="surface">the surface to use for the pixel format and palette.</param>
    /// <param name="r">the red component of the pixel in the range 0-255.</param>
    /// <param name="g">the green component of the pixel in the range 0-255.</param>
    /// <param name="b">the blue component of the pixel in the range 0-255.</param>
    /// <remarks>
    /// This function maps the RGB color value to the specified pixel format and
    /// returns the pixel value best approximating the given RGB color value for
    /// the given pixel format.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="MapSurfaceRgba(nint, byte, byte, byte, byte)" />
    /// </remarks>
    /// <returns>Returns a pixel value.</returns>
    public static uint MapSurfaceRgb(nint surface, byte r, byte g, byte b) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "MapSurfaceRgb: Surface pointer is null.");
            return 0;
        }
        var color = SDL_MapSurfaceRGB(surface, r, g, b);
        if (color == 0) {
            LogError(LogCategory.Error, "MapSurfaceRgb: Failed to map surface RGB color.");
        }
        return color;
    }

    /// <summary>Map an RGB triple to an opaque pixel value for a surface.</summary>
    /// <param name="surface">the surface to use for the pixel format and palette.</param>
    /// <param name="color">the <see cref="Color" /> representing RGB ranging from 0-255.</param>
    /// <remarks>
    /// This function maps the RGB color value to the specified pixel format and
    /// returns the pixel value best approximating the given RGB color value for
    /// the given pixel format.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="MapSurfaceRgba(nint, Color)" />
    /// </remarks>
    /// <returns>Returns a pixel value.</returns>
    public static uint MapSurfaceRgb(nint surface, Color color) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "MapSurfaceRgb: Surface pointer is null.");
            return 0;
        }

        var colorValue = SDL_MapSurfaceRGB(surface, color.R, color.G, color.B);
        if (colorValue == 0) {
            LogError(LogCategory.Error, "MapSurfaceRgb: Failed to map surface RGB color.");
        }
        return colorValue;
    }

    /// <summary>Map an RGBA quadruple to a pixel value for a surface.</summary>
    /// <param name="surface">the surface to use for the pixel format and palette.</param>
    /// <param name="r">the red component of the pixel in the range 0-255.</param>
    /// <param name="g">the green component of the pixel in the range 0-255.</param>
    /// <param name="b">the blue component of the pixel in the range 0-255.</param>
    /// <param name="a">the alpha component of the pixel in the range 0-255.</param>
    /// <remarks>
    /// This function maps the RGBA color value to the specified pixel format and
    /// returns the pixel value best approximating the given RGBA color value for
    /// the given pixel format.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="MapSurfaceRgb(nint, byte, byte, byte)" />
    /// </remarks>
    /// <returns>Returns a pixel value.</returns>
    public static uint MapSurfaceRgba(nint surface, byte r, byte g, byte b, byte a) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "MapSurfaceRgba: Surface pointer is null.");
            return 0;
        }
        var color = SDL_MapSurfaceRGBA(surface, r, g, b, a);
        if (color == 0) {
            LogError(LogCategory.Error, "MapSurfaceRgba: Failed to map surface RGBA color.");
        }
        return color;
    }

    /// <summary>Map an RGBA quadruple to a pixel value for a surface.</summary>
    /// <param name="surface">the surface to use for the pixel format and palette.</param>
    /// <param name="color">the <see cref="Color" /> representing RGB ranging from 0-255.</param>
    /// <remarks>
    /// This function maps the RGBA color value to the specified pixel format and
    /// returns the pixel value best approximating the given RGBA color value for
    /// the given pixel format.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="MapSurfaceRgb(nint, Color)" />
    /// </remarks>
    /// <returns>Returns a pixel value.</returns>
    public static uint MapSurfaceRgba(nint surface, Color color) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "MapSurfaceRgba: Surface pointer is null.");
            return 0;
        }
        var colorValue = SDL_MapSurfaceRGBA(surface, color.R, color.G, color.B, color.A);
        if (colorValue == 0) {
            LogError(LogCategory.Error, "MapSurfaceRgba: Failed to map surface RGBA color.");
        }
        return colorValue;
    }
    /// <summary>Retrieves a single pixel from a surface.</summary>
    /// <param name="surface">the surface to read.</param>
    /// <param name="x">the horizontal coordinate, 0 &lt;= x &lt; width.</param>
    /// <param name="y">the vertical coordinate, 0 &lt;= y &lt; height.</param>
    /// <param name="r">a pointer filled in with the red channel, 0-255, or discard to ignore this channel.</param>
    /// <param name="g">a pointer filled in with the green channel, 0-255, or discard to ignore this channel.</param>
    /// <param name="b">a pointer filled in with the blue channel, 0-255, or discard to ignore this channel.</param>
    /// <param name="a">a pointer filled in with the alpha channel, 0-255, or discard to ignore this channel.</param>
    /// <remarks>
    /// This function prioritizes correctness over speed: it is suitable for unit
    /// tests, but is not intended for use in a game engine.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool ReadSurfacePixel(nint surface, int x, int y, out byte r, out byte g, out byte b, out byte a) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "ReadSurfacePixel: Surface pointer is null.");
            r = g = b = a = 0;
            return false;
        }
        bool result = SDL_ReadSurfacePixel(surface, x, y, out r, out g, out b, out a);
        if (!result) {
            LogError(LogCategory.Error, "ReadSurfacePixel: Failed to read surface pixel.");
        }
        return result;
    }

    /// <summary>Retrieves a single pixel from a surface.</summary>
    /// <param name="surface">the surface to read.</param>
    /// <param name="x">the horizontal coordinate, 0 &lt;= x &lt; width.</param>
    /// <param name="y">the vertical coordinate, 0 &lt;= y &lt; height.</param>
    /// <param name="color">the color that is read from the <paramref name="surface"/> at <paramref name="x"/> and <paramref name="y"/> coordinates.</param>
    /// <remarks>
    /// This function prioritizes correctness over speed: it is suitable for unit
    /// tests, but is not intended for use in a game engine.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool ReadSurfacePixel(nint surface, int x, int y, out Color color) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "ReadSurfacePixel: Surface pointer is null.");
            color = default;
            return false;
        }
        bool result = SDL_ReadSurfacePixel(surface, x, y, out var r, out var g, out var b, out var a);
        if (!result) {
            LogError(LogCategory.Error, "ReadSurfacePixel: Failed to read surface pixel.");
            color = default;
            return false;
        }
        color = new Color() { R = r, G = g, B = b, A = a };
        return true;
    }

    /// <summary>Retrieves a single pixel from a surface.</summary>
    /// <param name="surface">the surface to read.</param>
    /// <param name="x">the horizontal coordinate, 0 &lt;= x &lt; width.</param>
    /// <param name="y">the vertical coordinate, 0 &lt;= y &lt; height.</param>
    /// <remarks>
    /// This function prioritizes correctness over speed: it is suitable for unit
    /// tests, but is not intended for use in a game engine.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static Color ReadSurfacePixel(nint surface, int x, int y) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "ReadSurfacePixel: Surface pointer is null.");
            return default;
        }
        bool result = SDL_ReadSurfacePixel(surface, x, y, out var r, out var g, out var b, out var a);
        if (!result) {
            LogError(LogCategory.Error, "ReadSurfacePixel: Failed to read surface pixel.");
            return default;
        }
        return new Color() { R = r, G = g, B = b, A = a };
    }

    /// <summary>Retrieves a single pixel from a surface.</summary>
    /// <param name="surface">the surface to read.</param>
    /// <param name="x">the horizontal coordinate, 0 &lt;= x &lt; width.</param>
    /// <param name="y">the vertical coordinate, 0 &lt;= y &lt; height.</param>
    /// <param name="r">a pointer filled in with the red channel, normally in the range 0-1, or discard to ignore this channel.</param>
    /// <param name="g">a pointer filled in with the green channel, normally in the range 0-1, or discard to ignore this channel.</param>
    /// <param name="b">a pointer filled in with the blue channel, normally in the range 0-1, or discard to ignore this channel.</param>
    /// <param name="a">a pointer filled in with the alpha channel, normally in the range 0-1, or discard to ignore this channel.</param>
    /// <remarks>
    /// This function prioritizes correctness over speed: it is suitable for unit
    /// tests, but is not intended for use in a game engine.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool ReadSurfacePixelFloat(nint surface, int x, int y, out float r, out float g, out float b, out float a) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "ReadSurfacePixelFloat: Surface pointer is null.");
            r = g = b = a = 0;
            return false;
        }
        bool result = SDL_ReadSurfacePixelFloat(surface, x, y, out r, out g, out b, out a);
        if (!result) {
            LogError(LogCategory.Error, "ReadSurfacePixelFloat: Failed to read surface pixel.");
        }
        return result;
    }

    /// <summary>Retrieves a single pixel from a surface.</summary>
    /// <param name="surface">the surface to read.</param>
    /// <param name="x">the horizontal coordinate, 0 &lt;= x &lt; width.</param>
    /// <param name="y">the vertical coordinate, 0 &lt;= y &lt; height.</param>
    /// <param name="color">the <see cref="FColor" /> structure filled with color data, or discard to ignore.</param>
    /// <remarks>
    /// This function prioritizes correctness over speed: it is suitable for unit
    /// tests, but is not intended for use in a game engine.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool ReadSurfacePixelFloat(nint surface, int x, int y, out FColor color) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "ReadSurfacePixelFloat: Surface pointer is null.");
            color = default;
            return false;
        }
        bool result = SDL_ReadSurfacePixelFloat(surface, x, y, out var r, out var g, out var b, out var a);
        if (!result) {
            LogError(LogCategory.Error, "ReadSurfacePixelFloat: Failed to read surface pixel.");
            color = default;
            return false;
        }
        color = new FColor() { R = r, G = g, B = b, A = a };
        return true;
    }

    /// <summary>Retrieves a single pixel from a surface.</summary>
    /// <param name="surface">the surface to read.</param>
    /// <param name="x">the horizontal coordinate, 0 &lt;= x &lt; width.</param>
    /// <param name="y">the vertical coordinate, 0 &lt;= y &lt; height.</param>
    /// <remarks>
    /// This function prioritizes correctness over speed: it is suitable for unit
    /// tests, but is not intended for use in a game engine.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static FColor ReadSurfacePixelFloat(nint surface, int x, int y) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "ReadSurfacePixelFloat: Surface pointer is null.");
            return default;
        }
        bool result = SDL_ReadSurfacePixelFloat(surface, x, y, out var r, out var g, out var b, out var a);
        if (!result) {
            LogError(LogCategory.Error, "ReadSurfacePixelFloat: Failed to read surface pixel.");
            return default;
        }
        return new FColor() { R = r, G = g, B = b, A = a };
    }
    
    /// <summary>Creates a new surface identical to the existing surface, scaled to the desired size.</summary>
    /// <param name="surface">the surface to duplicate and scale.</param>
    /// <param name="width">the width of the new surface.</param>
    /// <param name="height">the height of the new surface.</param>
    /// <param name="scaleMode">the <see cref="ScaleMode" /> to be used.</param>
    /// <remarks>
    /// The returned surface should be freed with
    /// <see cref="DestroySurface" />.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="DestroySurface" />
    /// </remarks>
    /// <returns>(SDL_Surface *) Returns a copy of the surface or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static nint ScaleSurface(nint surface, int width, int height, ScaleMode scaleMode) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "ScaleSurface: Surface pointer is null.");
            return nint.Zero;
        }

        if (!Enum.IsDefined(scaleMode)) {
            LogError(LogCategory.Error, "ScaleSurface: Invalid scale mode.");
            return nint.Zero;
        }

        if (width <= 0 || height <= 0) {
            LogError(LogCategory.Error, "ScaleSurface: Invalid width or height.");
            return nint.Zero;
        }

        // System.EngineExecutionException thrown here, why?
        var scaledSurface = SDL_ScaleSurface(surface, width, height, (int)scaleMode);
        if (scaledSurface == nint.Zero) {
            LogError(LogCategory.Error, $"ScaleSurface: Failed to scale surface. {Sdl.GetError()}");
        }
        return scaledSurface;
    }

    /// <summary>Creates a new surface identical to the existing surface, scaled to the desired size.</summary>
    /// <param name="surface">the surface to duplicate and scale.</param>
    /// <param name="width">the width of the new surface.</param>
    /// <param name="height">the height of the new surface.</param>
    /// <param name="scaleMode">the <see cref="ScaleMode" /> to be used.</param>
    /// <remarks>
    /// <para>The returned surface should be freed with <see cref="DestroySurface" />.</para>
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="DestroySurface" />
    /// </remarks>
    /// <returns>(SDL_Surface *) Returns a copy of the surface or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static Surface ScaleSurface(ref Surface surface, int width, int height, ScaleMode scaleMode) {
        var oSurface = StructureToPointer(ref surface);
        var newSurface = ScaleSurface(oSurface, width, height, scaleMode);
        var rSurface = PointerToStructure<Surface>(newSurface);
        return rSurface;
    }

    
    /// <summary>Set an additional alpha value used in blit operations.</summary>
    /// <param name="surface">the <see cref="Surface" /> structure to update.</param>
    /// <param name="alpha">the alpha value multiplied into blit operations.</param>
    /// <remarks>
    /// When this surface is blitted, during the blit operation the source alpha
    /// value is modulated by this alpha value according to the following formula:
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetSurfaceAlphaMod" />
    /// <seealso cref="SetSurfaceColorMod(nint, byte, byte, byte)" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool SetSurfaceAlphaMod(nint surface, byte alpha) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "SetSurfaceAlphaMod: Surface pointer is null.");
            return false;
        }
        bool result = SDL_SetSurfaceAlphaMod(surface, alpha);
        if (!result) {
            LogError(LogCategory.Error, "SetSurfaceAlphaMod: Failed to set surface alpha mod.");
        }
        return result;
    }

    /// <summary>Set the blend mode used for blit operations.</summary>
    /// <param name="surface">the <see cref="Surface" /> structure to update.</param>
    /// <param name="blendMode">the <see cref="BlendMode" /> to use for blit blending.</param>
    /// <remarks>
    /// To copy a surface to another surface (or texture) without blending with the
    /// existing data, the blendmode of the SOURCE surface should be set to
    /// SDL_BLENDMODE_NONE.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetSurfaceBlendMode" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool SetSurfaceBlendMode(nint surface, uint blendMode) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "SetSurfaceBlendMode: Surface pointer is null.");
            return false;
        }
        bool result = SDL_SetSurfaceBlendMode(surface, blendMode);
        if (!result) {
            LogError(LogCategory.Error, "SetSurfaceBlendMode: Failed to set surface blend mode.");
        }
        return result;
    }

    /// <summary>Set the clipping rectangle for a surface.</summary>
    /// <param name="surface">the <see cref="Surface" /> structure to be clipped.</param>
    /// <param name="rect">the <see cref="Rect" /> structure representing the clipping rectangle, or <see langword="null" /> to disable clipping.</param>
    /// <remarks>
    /// When surface is the destination of a blit, only the area within the clip
    /// rectangle is drawn into.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetSurfaceClipRect" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the rectangle intersects the surface, otherwise<see langword="false" /> and blits will be completely clipped.</returns>
    public static bool SetSurfaceClipRect(nint surface, ref Rect rect) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "SetSurfaceClipRect: Surface pointer is null.");
            return false;
        }
        bool result = SDL_SetSurfaceClipRect(surface, ref rect);
        if (!result) {
            LogError(LogCategory.Error, "SetSurfaceClipRect: Failed to set surface clip rect.");
        }
        return result;
    }

    /// <summary>Set the color key (transparent pixel) in a surface.</summary>
    /// <param name="surface">the <see cref="Surface" /> structure to update.</param>
    /// <param name="enabled"><see langword="true" /> to enable color key, <see langword="false" /> to disable color key.</param>
    /// <param name="key">the transparent pixel.</param>
    /// <remarks>
    /// The color key defines a pixel value that will be treated as transparent in
    /// a blit. For example, one can use this to specify that cyan pixels should be
    /// considered transparent, and therefore not rendered.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetSurfaceColorKey" />
    /// <seealso cref="SetSurfaceRle" />
    /// <seealso cref="SurfaceHasColorKey" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool SetSurfaceColorKey(nint surface, bool enabled, uint key) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "SetSurfaceColorKey: Surface pointer is null.");
            return false;
        }
        bool result = SDL_SetSurfaceColorKey(surface, enabled, key);
        if (!result) {
            LogError(LogCategory.Error, "SetSurfaceColorKey: Failed to set surface color key.");
        }
        return result;
    }

    /// <summary>Set an additional color value multiplied into blit operations.</summary>
    /// <param name="surface">the <see cref="Surface" /> structure to update.</param>
    /// <param name="r">the red color value multiplied into blit operations.</param>
    /// <param name="g">the green color value multiplied into blit operations.</param>
    /// <param name="b">the blue color value multiplied into blit operations.</param>
    /// <remarks>
    /// When this surface is blitted, during the blit operation each source color
    /// channel is modulated by the appropriate color value according to the
    /// following formula:
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetSurfaceColorMod" />
    /// <seealso cref="SetSurfaceAlphaMod" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool SetSurfaceColorMod(nint surface, byte r, byte g, byte b) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "SetSurfaceColorMod: Surface pointer is null.");
            return false;
        }
        bool result = SDL_SetSurfaceColorMod(surface, r, g, b);
        if (!result) {
            LogError(LogCategory.Error, "SetSurfaceColorMod: Failed to set surface color mod.");
        }
        return result;
    }

    /// <summary>Set an additional color value multiplied into blit operations.</summary>
    /// <param name="surface">the <see cref="Surface" /> structure to update.</param>
    /// <param name="color">the color value.</param>
    /// <remarks>
    /// When this surface is blitted, during the blit operation each source color
    /// channel is modulated by the appropriate color value according to the
    /// following formula:
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetSurfaceColorMod" />
    /// <seealso cref="SetSurfaceAlphaMod" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool SetSurfaceColorMod(nint surface, Color color) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "SetSurfaceColorMod: Surface pointer is null.");
            return false;
        }
        bool result = SDL_SetSurfaceColorMod(surface, color.R, color.G, color.B);
        if (!result) {
            LogError(LogCategory.Error, "SetSurfaceColorMod: Failed to set surface color mod.");
        }
        return result;
    }

    /// <summary>Set the colorspace used by a surface.</summary>
    /// <param name="surface">the <see cref="Surface" /> structure to update.</param>
    /// <param name="colorspace">an <see cref="Colorspace" /> value describing the surface colorspace.</param>
    /// <remarks>
    /// Setting the colorspace doesn't change the pixels, only how they are
    /// interpreted in color operations.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetSurfaceColorspace" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool SetSurfaceColorspace(nint surface, Colorspace colorspace) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "SetSurfaceColorspace: Surface pointer is null.");
            return false;
        }
        bool result = SDL_SetSurfaceColorspace(surface, colorspace);
        if (!result) {
            LogError(LogCategory.Error, "SetSurfaceColorspace: Failed to set surface colorspace.");
        }
        return result;
    }

    /// <summary>Set the palette used by a surface.</summary>
    /// <param name="surface">the <see cref="Surface" /> structure to update.</param>
    /// <param name="palette">the SDL_Palette structure to use.</param>
    /// <remarks>
    /// A single palette can be shared with many surfaces.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreatePalette" />
    /// <seealso cref="GetSurfacePalette" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool SetSurfacePalette(nint surface, nint palette) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "SetSurfacePalette: Surface pointer is null.");
            return false;
        }
        if (palette == nint.Zero) {
            LogError(LogCategory.Error, "SetSurfacePalette: Palette pointer is null.");
            return false;
        }
        bool result = SDL_SetSurfacePalette(surface, palette);
        if (!result) {
            LogError(LogCategory.Error, "SetSurfacePalette: Failed to set surface palette.");
        }
        return result;
    }

    /// <summary>Set the RLE acceleration hint for a surface.</summary>
    /// <param name="surface">the <see cref="Surface" /> structure to optimize.</param>
    /// <param name="enabled"><see langword="true" /> to enable RLE acceleration, <see langword="false" /> to disable it.</param>
    /// <remarks>
    /// If RLE is enabled, color key and alpha blending blits are much faster, but
    /// the surface must be locked before directly accessing the pixels.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BlitSurface" />
    /// <seealso cref="LockSurface" />
    /// <seealso cref="UnlockSurface" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool SetSurfaceRle(nint surface, bool enabled) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "SetSurfaceRLE: Surface pointer is null.");
            return false;
        }
        bool result = SDL_SetSurfaceRLE(surface, enabled);
        if (!result) {
            LogError(LogCategory.Error, "SetSurfaceRLE: Failed to set surface RLE.");
        }
        return result;
    }
    
    /// <summary>Return whether a surface has alternate versions available.</summary>
    /// <param name="surface">the <see cref="Surface" /> structure to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="AddSurfaceAlternateImage" />
    /// <seealso cref="RemoveSurfaceAlternateImages" />
    /// <seealso cref="GetSurfaceImages" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if alternate versions are available or <see langword="false" /> otherwise.</returns>
    public static bool SurfaceHasAlternateImages(nint surface) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "SurfaceHasAlternateImages: Surface pointer is null.");
            return false;
        }
        bool result = SDL_SurfaceHasAlternateImages(surface);
        if (!result) {
            LogError(LogCategory.Error, "SurfaceHasAlternateImages: Failed to check surface alternate images.");
        }
        return result;
    }

    /// <summary>Returns whether the surface has a color key.</summary>
    /// <param name="surface">the <see cref="Surface" /> structure to query.</param>
    /// <remarks>
    /// It is safe to pass a <see langword="null" /> surface here; it will return false.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetSurfaceColorKey" />
    /// <seealso cref="GetSurfaceColorKey" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the surface has a color key, <see langword="false" /> otherwise.</returns>
    public static bool SurfaceHasColorKey(nint surface) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "SurfaceHasColorKey: Surface pointer is null.");
            return false;
        }
        bool result = SDL_SurfaceHasColorKey(surface);
        if (!result) {
            LogError(LogCategory.Error, "SurfaceHasColorKey: Failed to check surface color key.");
        }
        return result;
    }

    /// <summary>Returns whether the surface is RLE enabled.</summary>
    /// <param name="surface">the <see cref="Surface" /> structure to query.</param>
    /// <remarks>
    /// It is safe to pass a <see langword="null" /> surface here; it will return false.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetSurfaceRle" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the surface is RLE enabled, <see langword="false" /> otherwise.</returns>
    public static bool SurfaceHasRle(nint surface) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "SurfaceHasRLE: Surface pointer is null.");
            return false;
        }
        bool result = SDL_SurfaceHasRLE(surface);
        if (!result) {
            LogError(LogCategory.Error, "SurfaceHasRLE: Failed to check surface RLE.");
        }
        return result;
    }
    
    /// <summary>Release a surface after directly accessing the pixels.</summary>
    /// <param name="surface">the <see cref="Surface" /> structure to be unlocked.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function is not thread safe. The locking referred to by this functionis making the pixels available for direct access, not thread-safe locking.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LockSurface" />
    /// </remarks>
    public static void UnlockSurface(nint surface) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "UnlockSurface: Surface pointer is null.");
            return;
        }
        SDL_UnlockSurface(surface);
    }

    
    /// <summary>Writes a single pixel to a surface.</summary>
    /// <param name="surface">the surface to write.</param>
    /// <param name="x">the horizontal coordinate, 0 &lt;= x &lt; width.</param>
    /// <param name="y">the vertical coordinate, 0 &lt;= y &lt; height.</param>
    /// <param name="r">the red channel value, 0-255.</param>
    /// <param name="g">the green channel value, 0-255.</param>
    /// <param name="b">the blue channel value, 0-255.</param>
    /// <param name="a">the alpha channel value, 0-255.</param>
    /// <remarks>
    /// This function prioritizes correctness over speed: it is suitable for unit
    /// tests, but is not intended for use in a game engine.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool WriteSurfacePixel(nint surface, int x, int y, byte r, byte g, byte b, byte a) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "WriteSurfacePixel: Surface pointer is null.");
            return false;
        }
        bool result = SDL_WriteSurfacePixel(surface, x, y, r, g, b, a);
        if (!result) {
            LogError(LogCategory.Error, "WriteSurfacePixel: Failed to write surface pixel.");
        }
        return result;
    }

    /// <summary>Writes a single pixel to a surface.</summary>
    /// <param name="surface">the surface to write.</param>
    /// <param name="x">the horizontal coordinate, 0 &lt;= x &lt; width.</param>
    /// <param name="y">the vertical coordinate, 0 &lt;= y &lt; height.</param>
    /// <param name="color">the <see cref="Color" /> struct filled with data</param>
    /// <remarks>
    /// This function prioritizes correctness over speed: it is suitable for unit
    /// tests, but is not intended for use in a game engine.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool WriteSurfacePixel(nint surface, int x, int y, Color color) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "WriteSurfacePixel: Surface pointer is null.");
            return false;
        }
        bool result = SDL_WriteSurfacePixel(surface, x, y, color.R, color.G, color.B, color.A);
        if (!result) {
            LogError(LogCategory.Error, "WriteSurfacePixel: Failed to write surface pixel.");
        }
        return result;
    }

    /// <summary>Writes a single pixel to a surface.</summary>
    /// <param name="surface">the surface to write.</param>
    /// <param name="location">the <see cref="Point" /> struct that provides xy coordinates</param>
    /// <param name="color">the <see cref="Color" /> struct filled with data</param>
    /// <remarks>
    /// This function prioritizes correctness over speed: it is suitable for unit
    /// tests, but is not intended for use in a game engine.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool WriteSurfacePixel(nint surface, Point location, Color color) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "WriteSurfacePixel: Surface pointer is null.");
            return false;
        }
        return WriteSurfacePixel(surface, location.X, location.Y, color.R, color.G, color.B, color.A);
    }

    /// <summary>Writes a single pixel to a surface.</summary>
    /// <param name="surface">the surface to write.</param>
    /// <param name="location">the <see cref="Point" /> struct that provides xy coordinates</param>
    /// <param name="r">the red channel value, 0-255.</param>
    /// <param name="g">the green channel value, 0-255.</param>
    /// <param name="b">the blue channel value, 0-255.</param>
    /// <param name="a">the alpha channel value, 0-255.</param>
    /// <remarks>
    /// This function prioritizes correctness over speed: it is suitable for unit
    /// tests, but is not intended for use in a game engine.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool WriteSurfacePixel(nint surface, Point location, byte r, byte g, byte b, byte a) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "WriteSurfacePixel: Surface pointer is null.");
            return false;
        }
        return WriteSurfacePixel(surface, location.X, location.Y, r, g, b, a);
    }

    /// <summary>Writes a single pixel to a surface.</summary>
    /// <param name="surface">the surface to write.</param>
    /// <param name="x">the horizontal coordinate, 0 &lt;= x &lt; width.</param>
    /// <param name="y">the vertical coordinate, 0 &lt;= y &lt; height.</param>
    /// <param name="r">the red channel value, normally in the range 0-1.</param>
    /// <param name="g">the green channel value, normally in the range 0-1.</param>
    /// <param name="b">the blue channel value, normally in the range 0-1.</param>
    /// <param name="a">the alpha channel value, normally in the range 0-1.</param>
    /// <remarks>
    /// This function prioritizes correctness over speed: it is suitable for unit
    /// tests, but is not intended for use in a game engine.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool WriteSurfacePixelFloat(nint surface, int x, int y, float r, float g, float b, float a) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "WriteSurfacePixelFloat: Surface pointer is null.");
            return false;
        }
        bool result = SDL_WriteSurfacePixelFloat(surface, x, y, r, g, b, a);
        if (!result) {
            LogError(LogCategory.Error, "WriteSurfacePixelFloat: Failed to write surface pixel float.");
        }
        return result;
    }

    /// <summary>Writes a single pixel to a surface.</summary>
    /// <param name="surface">the surface to write.</param>
    /// <param name="location">the <see cref="Point" /> struct that provides xy coordinates</param>
    /// <param name="color">the <see cref="Color" /> struct filled with data</param>
    /// <remarks>
    /// This function prioritizes correctness over speed: it is suitable for unit
    /// tests, but is not intended for use in a game engine.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool WriteSurfacePixelFloat(nint surface, Point location, FColor color) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "WriteSurfacePixelFloat: Window pointer is null.");
            return false;
        }
        return WriteSurfacePixelFloat(surface, location.X, location.Y, color.R, color.G, color.B,
            color.A);
    }

    /// <summary>Writes a single pixel to a surface.</summary>
    /// <param name="surface">the surface to write.</param>
    /// <param name="location">the <see cref="Point" /> struct that provides xy coordinates</param>
    /// <param name="r">the red channel value, normally in the range 0-1.</param>
    /// <param name="g">the green channel value, normally in the range 0-1.</param>
    /// <param name="b">the blue channel value, normally in the range 0-1.</param>
    /// <param name="a">the alpha channel value, normally in the range 0-1.</param>
    /// <remarks>
    /// This function prioritizes correctness over speed: it is suitable for unit
    /// tests, but is not intended for use in a game engine.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool WriteSurfacePixelFloat(nint surface, Point location, float r, float g, float b, float a) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "WriteSurfacePixelFloat: Window pointer is null.");
            return false;
        }
        return WriteSurfacePixelFloat(surface, location.X, location.Y, r, g, b, a);
    }

}