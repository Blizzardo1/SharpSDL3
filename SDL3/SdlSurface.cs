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
    /// The functions <see cref="SetRenderDrawBlendMode" /> and <see cref="SetTextureBlendMode" /> accept the <see cref="BlendMode" /> returned by this function if the renderer supports it.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetRenderDrawBlendMode" />
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
    /// <seealso cref="SetSurfaceColorMod" />
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

        Span<nint> images = new(ref result);
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
}