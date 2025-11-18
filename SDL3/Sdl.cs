using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.Intrinsics.X86;
using static SharpSDL3.Delegates;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SharpSDL3;

public static unsafe partial class Sdl {
    internal const string NativeLibName = "SDL3";

    internal const UnmanagedType StringType = UnmanagedType.LPUTF8Str;
    internal const UnmanagedType BoolType = UnmanagedType.I1;
    internal const StringMarshalling Marshalling = StringMarshalling.Utf8;

    /// <summary>
    /// Converts a <typeparamref name="T"/> to a pointer.
    /// </summary>
    /// <typeparam name="T">The unmanaged generic type.</typeparam>
    /// <param name="str">An unmanaged type to convert to a <see cref="nint"/>.</param>
    /// <returns>A pointer in memory to an object, else <see cref="nint.Zero"/>.</returns>
    public static unsafe nint StructureToPointer<T>(ref T str) where T : unmanaged {
        int size = sizeof(T);
        nint ptr = Marshal.AllocHGlobal(size);
        Unsafe.CopyBlockUnaligned((void*)ptr, Unsafe.AsPointer(ref str), (uint)size);
        return ptr;
    }

    /// <summary>
    /// Convert a pointer to a structure of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The unmanaged generic type.</typeparam>
    /// <param name="ptr">The pointer of which contains a <typeparamref name="T"/>.</param>
    /// <returns>a <typeparamref name="T"/> filled with data.</returns>
    public static unsafe T PointerToStructure<T>(nint ptr) where T : unmanaged {
        T str = default;
        Unsafe.Copy(ref str, (void*)ptr);
        return str;
    }

    /// <summary>
    /// This macro turns the version numbers into a numeric value.
    /// </summary>
    /// <param name="major">The Major versiom number</param>
    /// <param name="minor">The Minor version number</param>
    /// <param name="patch">The Patch version number</param>
    /// <remarks>
    /// (1,2,3) becomes 1002003.
    /// <para><strong>Version:</strong> This macro is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>The version as a number</returns>
    public static int VersionNum(int major, int minor, int patch) => major * 1000000 + minor * 1000 + patch;

    /// <summary>Add a function to watch a particular hint.</summary>
    /// <param name="name">the hint to watch.</param>
    /// <param name="callback">An <see cref="SdlHintCallback"/> function that will be called when the hint value changes.</param>
    /// <param name="userdata">a pointer to pass to the callback function.</param>
    /// <remarks>
    /// The callback function is called during this function, to provide it an initial value, and again each time the hint's value changes.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RemoveHintCallback"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool AddHintCallback(string name, SdlHintCallback callback, nint userdata) {
        if (string.IsNullOrEmpty(name)) {
            LogWarn(LogCategory.System, "AddHintCallback: Name is null or empty.");
            return false;
        }
        if (callback == null) {
            LogWarn(LogCategory.System, "AddHintCallback: Callback is null.");
            return false;
        }
        return SDL_AddHintCallback(name, callback, userdata);
    }

    /// <summary>Add an alternate version of a surface.</summary>
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <param name="image">a pointer to an alternate SDL_Surface to associate with this surface.</param>
    /// <remarks>
    /// This function adds an alternate version of this surface, usually used for
    /// content with high DPI representations like cursors or icons. The size,
    /// format, and content do not need to match the original surface, and these
    /// alternate versions will not be updated when the original surface changes.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RemoveSurfaceAlternateImages"/>
    /// <seealso cref="GetSurfaceImages"/>
    /// <seealso cref="SurfaceHasAlternateImages"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool AddSurfaceAlternateImage(nint surface, nint image) {
        if (surface == nint.Zero || image == nint.Zero) {
            LogWarn(LogCategory.System, "AddSurfaceAlternateImage: Surface or image pointer is null.");
            return false;
        }
        return SDL_AddSurfaceAlternateImage(surface, image);
    }

    /// <summary>Performs a fast blit from the source surface to the destination surface with clipping.</summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be copied, or <see langword="null" /> to copy the entire surface.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the x and y position in the destination surface, or <see langword="null" /> for (0,0). The width and height are ignored, and are copied from srcrect. If you want a specific width and height, you should use <see cref="BlitSurfaceScaled"/>.</param>
    /// <remarks>
    /// If either srcrect or dstrect are <see langword="null" />, the entire surface (src or dst) is copied while ensuring clipping to dst-&gt;clip_rect.
    /// <para><strong>Thread Safety:</strong> Only one thread should be using the src and dst surfaces at any given time.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BlitSurfaceScaled"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool BlitSurface(nint src, nint srcrect, nint dst, nint dstrect) {
        if (src == nint.Zero || dst == nint.Zero) {
            LogWarn(LogCategory.System, "BlitSurface: Source or destination pointer is null.");
            return false;
        }
        return SDL_BlitSurface(src, srcrect, dst, dstrect);
    }

    /// <summary>Perform a scaled blit using the 9-grid algorithm to a destination surface, which may be of a different format.</summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be used for the 9-grid, or <see langword="null" /> to use the entire surface.</param>
    /// <param name="leftWidth">the width, in pixels, of the left corners in srcrect.</param>
    /// <param name="rightWidth">the width, in pixels, of the right corners in srcrect.</param>
    /// <param name="topHeight">the height, in pixels, of the top corners in srcrect.</param>
    /// <param name="bottomHeight">the height, in pixels, of the bottom corners in srcrect.</param>
    /// <param name="scale">the scale used to transform the corner of srcrect into the corner of dstrect, or 0.0f for an unscaled blit.</param>
    /// <param name="scaleMode">scale algorithm to be used.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in the destination surface, or <see langword="null" /> to fill the entire surface.</param>
    /// <remarks>
    /// The pixels in the source surface are split into a 3x3 grid, using the
    /// different corner sizes for each corner, and the sides and center making up
    /// the remaining pixels. The corners are then scaled using scale and fit
    /// into the corners of the destination rectangle. The sides and center are
    /// then stretched into place to cover the remaining destination rectangle.
    /// <para><strong>Thread Safety:</strong> Only one thread should be using the src and dst surfaces at any given time.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BlitSurface"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool BlitSurface9Grid(nint src, nint srcrect, int leftWidth, int rightWidth, int topHeight, int bottomHeight, float scale, ScaleMode scaleMode, nint dst, nint dstrect) {
        if (src == nint.Zero || dst == nint.Zero) {
            LogWarn(LogCategory.System, "BlitSurface9Grid: Source or destination pointer is null.");
            return false;
        }
        return SDL_BlitSurface9Grid(src, srcrect, leftWidth, rightWidth, topHeight, bottomHeight, scale, scaleMode, dst, dstrect);
    }

    /// <summary>Perform a scaled blit to a destination surface, which may be of a different format.</summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be copied, or <see langword="null" /> to copy the entire surface.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in the destination surface, or <see langword="null" /> to fill the entire destination surface.</param>
    /// <param name="scaleMode">the <see cref="ScaleMode"/> to be used.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> Only one thread should be using the src and dst surfaces at any given time.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BlitSurface"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool BlitSurfaceScaled(nint src, nint srcrect, nint dst, nint dstrect, ScaleMode scaleMode) {
        if (src == nint.Zero || dst == nint.Zero) {
            LogWarn(LogCategory.System, "BlitSurfaceScaled: Source or destination pointer is null.");
            return false;
        }
        return SDL_BlitSurfaceScaled(src, srcrect, dst, dstrect, scaleMode);
    }

    /// <summary>Perform a tiled blit to a destination surface, which may be of a different format.</summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be copied, or <see langword="null" /> to copy the entire surface.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in the destination surface, or <see langword="null" /> to fill the entire surface.</param>
    /// <remarks>
    /// The pixels in srcrect will be repeated as many times as needed to completely fill dstrect.
    /// <para><strong>Thread Safety:</strong> Only one thread should be using the src and dst surfaces at any given time.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BlitSurface"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool BlitSurfaceTiled(nint src, nint srcrect, nint dst, nint dstrect) {
        if (src == nint.Zero || dst == nint.Zero) {
            LogWarn(LogCategory.System, "BlitSurfaceTiled: Source or destination pointer is null.");
            return false;
        }
        return SDL_BlitSurfaceTiled(src, srcrect, dst, dstrect);
    }

    /// <summary>Perform a scaled and tiled blit to a destination surface, which may be of a different format.</summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be copied, or <see langword="null" /> to copy the entire surface.</param>
    /// <param name="scale">the scale used to transform srcrect into the destination rectangle, e.g. a 32x32 texture with a scale of 2 would fill 64x64 tiles.</param>
    /// <param name="scaleMode">scale algorithm to be used.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in the destination surface, or <see langword="null" /> to fill the entire surface.</param>
    /// <remarks>
    /// The pixels in srcrect will be scaled and repeated as many times as needed
    /// to completely fill dstrect.
    /// <para><strong>Thread Safety:</strong> Only one thread should be using the src and dst surfaces at any given time.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BlitSurface"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool BlitSurfaceTiledWithScale(nint src, nint srcrect, float scale, ScaleMode scaleMode, nint dst, nint dstrect) {
        if (src == nint.Zero || dst == nint.Zero) {
            LogWarn(LogCategory.System, "BlitSurfaceTiledWithScale: Source or destination pointer is null.");
            return false;
        }
        return SDL_BlitSurfaceTiledWithScale(src, srcrect, scale, scaleMode, dst, dstrect);
    }

    /// <summary>Perform low-level surface blitting only.</summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be copied, may not be <see langword="null" />.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in the destination surface, may not be <see langword="null" />.</param>
    /// <remarks>
    /// This is a semi-private blit function and it performs low-level surface
    /// blitting, assuming the input rectangles have already been clipped.
    /// <para><strong>Thread Safety:</strong> Only one thread should be using the src and dst surfaces at any given time.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BlitSurface"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool BlitSurfaceUnchecked(nint src, nint srcrect, nint dst, nint dstrect) {
        if (src == nint.Zero || dst == nint.Zero) {
            LogWarn(LogCategory.System, "BlitSurfaceUnchecked: Source or destination pointer is null.");
            return false;
        }
        return SDL_BlitSurfaceUnchecked(src, srcrect, dst, dstrect);
    }

    /// <summary>Perform low-level surface scaled blitting only.</summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be copied, may not be <see langword="null" />.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in the destination surface, may not be <see langword="null" />.</param>
    /// <param name="scaleMode">the <see cref="ScaleMode"/> to be used.</param>
    /// <remarks>
    /// This is a semi-private function and it performs low-level surface blitting,
    /// assuming the input rectangles have already been clipped.
    /// <para><strong>Thread Safety:</strong> Only one thread should be using the src and dst surfaces at any given time.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BlitSurfaceScaled"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool BlitSurfaceUncheckedScaled(nint src, nint srcrect, nint dst, nint dstrect, ScaleMode scaleMode) {
        if (src == nint.Zero || dst == nint.Zero) {
            LogWarn(LogCategory.System, "BlitSurfaceUncheckedScaled: Source or destination pointer is null.");
            return false;
        }
        return SDL_BlitSurfaceUncheckedScaled(src, srcrect, dst, dstrect, scaleMode);
    }

    /// <summary>Cleanup all TLS data for this thread.</summary>
    /// <remarks>
    /// If you are creating your threads outside of SDL and then calling SDL
    /// functions, you should call this function before your thread exits, to
    /// properly clean up SDL memory.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    public static void CleanupTls() {
        SDL_CleanupTLS();
    }

    /// <summary>Clear the clipboard data.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetClipboardData"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool ClearClipboardData() {
        return SDL_ClearClipboardData();
    }

    /// <summary>Dismiss the composition window/IME without disabling the subsystem.</summary>
    /// <param name="window">the window to affect.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="StartTextInput"/>
    /// <seealso cref="StopTextInput"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool ClearComposition(nint window) {
        if (window == nint.Zero) {
            LogWarn(LogCategory.System, "ClearComposition: Window handle is null.");
            return false;
        }
        return SDL_ClearComposition(window);
    }

    /// <summary>Clear any previous error message for this thread.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetError"/>
    /// <seealso cref="SetError"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" />.</returns>
    public static bool ClearError() {
        return SDL_ClearError();
    }

    /// <summary>Clear a property from a group of properties.</summary>
    /// <param name="props">the properties to modify.</param>
    /// <param name="name">the name of the property to clear.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool ClearProperty(uint props, string name) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            LogWarn(LogCategory.System, "ClearProperty: Properties handle is zero or name is null/empty.");
            return false;
        }
        return SDL_ClearProperty(props, name);
    }

    /// <summary>Clear a surface with a specific color, with floating point precision.</summary>
    /// <param name="surface">the <see cref="Surface"/> to clear.</param>
    /// <param name="r">the red component of the pixel, normally in the range 0-1.</param>
    /// <param name="g">the green component of the pixel, normally in the range 0-1.</param>
    /// <param name="b">the blue component of the pixel, normally in the range 0-1.</param>
    /// <param name="a">the alpha component of the pixel, normally in the range 0-1.</param>
    /// <remarks>
    /// This function handles all surface formats, and ignores any clip rectangle.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool ClearSurface(nint surface, float r, float g, float b, float a) {
        if (surface == nint.Zero) {
            LogWarn(LogCategory.System, "ClearSurface: Surface pointer is null.");
            return false;
        }
        return SDL_ClearSurface(surface, r, g, b, a);
    }

    /// <summary>Compose a custom blend mode for renderers.</summary>
    /// <param name="srcColorFactor">the <see cref="BlendFactor"/> applied to the red, green, and blue components of the source pixels.</param>
    /// <param name="dstColorFactor">the <see cref="BlendFactor"/> applied to the red, green, and blue components of the destination pixels.</param>
    /// <param name="colorOperation">the <see cref="BlendOperation"/> used to combine the red, green, and blue components of the source and destination pixels.</param>
    /// <param name="srcAlphaFactor">the <see cref="BlendFactor"/> applied to the alpha component of the source pixels.</param>
    /// <param name="dstAlphaFactor">the <see cref="BlendFactor"/> applied to the alpha component of the destination pixels.</param>
    /// <param name="alphaOperation">the <see cref="BlendOperation"/> used to combine the alpha component of the source and destination pixels.</param>
    /// <remarks>
    /// The functions <see cref="SetRenderDrawBlendMode"/> and <see cref="SetTextureBlendMode"/> accept the <see cref="BlendMode"/> returned by this function if the renderer supports it.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetRenderDrawBlendMode"/>
    /// <seealso cref="GetRenderDrawBlendMode"/>
    /// <seealso cref="SetTextureBlendMode"/>
    /// <seealso cref="GetTextureBlendMode"/>
    /// </remarks>
    /// <returns>Returns an <see cref="BlendMode"/>that represents the chosen factors and operations.</returns>
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

        uint blendMode = SDL_ComposeCustomBlendMode(srcColorFactor, dstColorFactor, colorOperation, srcAlphaFactor, dstAlphaFactor, alphaOperation);
        if (blendMode == 0) {
            LogError(LogCategory.Error, "ComposeCustomBlendMode: Failed to compose custom blend mode.");
        }

        return (BlendMode)blendMode;
    }

    /// <summary>Copy a block of pixels of one format to another format.</summary>
    /// <param name="width">the width of the block to copy, in pixels.</param>
    /// <param name="height">the height of the block to copy, in pixels.</param>
    /// <param name="srcFormat">an <see cref="PixelFormat"/> value of the src pixels format.</param>
    /// <param name="src">a pointer to the source pixels.</param>
    /// <param name="srcPitch">the pitch of the source pixels, in bytes.</param>
    /// <param name="dstFormat">an <see cref="PixelFormat"/> value of the dst pixels format.</param>
    /// <param name="dst">a pointer to be filled in with new pixel data.</param>
    /// <param name="dstPitch">the pitch of the destination pixels, in bytes.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> The same destination pixels should not be used from two threads at once. It is safe to use the same source pixels from multiple threads.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ConvertPixelsAndColorspace"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool ConvertPixels(int width, int height, PixelFormat srcFormat, nint src, int srcPitch, PixelFormat dstFormat, nint dst, int dstPitch) {
        if (src == nint.Zero || dst == nint.Zero) {
            LogWarn(LogCategory.System, "ConvertPixels: Source or destination pointer is null.");
            return false;
        }
        return SDL_ConvertPixels(width, height, srcFormat, src, srcPitch, dstFormat, dst, dstPitch);
    }

    /// <summary>Copy a block of pixels of one format and colorspace to another format and colorspace.</summary>
    /// <param name="width">the width of the block to copy, in pixels.</param>
    /// <param name="height">the height of the block to copy, in pixels.</param>
    /// <param name="srcFormat">an <see cref="PixelFormat"/> value of the src pixels format.</param>
    /// <param name="srcColorspace">an <see cref="Colorspace"/> value describing the colorspace of the src pixels.</param>
    /// <param name="srcProperties">an SDL_PropertiesID with additional source color properties, or 0.</param>
    /// <param name="src">a pointer to the source pixels.</param>
    /// <param name="srcPitch">the pitch of the source pixels, in bytes.</param>
    /// <param name="dstFormat">an <see cref="PixelFormat"/> value of the dst pixels format.</param>
    /// <param name="dstColorspace">an <see cref="Colorspace"/> value describing the colorspace of the dst pixels.</param>
    /// <param name="dstProperties">an SDL_PropertiesID with additional destination color properties, or 0.</param>
    /// <param name="dst">a pointer to be filled in with new pixel data.</param>
    /// <param name="dstPitch">the pitch of the destination pixels, in bytes.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> The same destination pixels should not be used from two threads at once. It is safe to use the same source pixels from multiple threads.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ConvertPixels"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool ConvertPixelsAndColorspace(int width, int height, PixelFormat srcFormat, Colorspace srcColorspace, uint srcProperties, nint src, int srcPitch, PixelFormat dstFormat, Colorspace dstColorspace, uint dstProperties, nint dst, int dstPitch) {
        if (src == nint.Zero || dst == nint.Zero) {
            LogWarn(LogCategory.System, "ConvertPixelsAndColorspace: Source or destination pointer is null.");
            return false;
        }
        return SDL_ConvertPixelsAndColorspace(width, height, srcFormat, srcColorspace, srcProperties, src, srcPitch, dstFormat, dstColorspace, dstProperties, dst, dstPitch);
    }

    /// <summary>Copy an existing surface to a new surface of the specified format.</summary>
    /// <param name="surface">the existing SDL_Surface structure to convert.</param>
    /// <param name="format">the new pixel format.</param>
    /// <remarks>
    /// This function is used to optimize images for faster repeat blitting. This
    /// is accomplished by converting the original and storing the result as a new
    /// surface. The new, optimized surface can then be used as the source for
    /// future blits, making them faster.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ConvertSurfaceAndColorspace"/>
    /// <seealso cref="DestroySurface"/>
    /// </remarks>
    /// <returns>(SDL_Surface *) Returns the new SDL_Surfacestructure that is created or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static nint ConvertSurface(nint surface, PixelFormat format) {
        if (surface == nint.Zero) {
            LogWarn(LogCategory.System, "ConvertSurface: Surface pointer is null.");
            return nint.Zero;
        }
        return SDL_ConvertSurface(surface, format);
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
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ConvertSurface"/>
    /// <seealso cref="DestroySurface"/>
    /// </remarks>
    /// <returns>(SDL_Surface *) Returns the new SDL_Surfacestructure that is created or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static nint ConvertSurfaceAndColorspace(nint surface, PixelFormat format, nint palette, Colorspace colorspace, uint props) {
        if (surface == nint.Zero) {
            LogWarn(LogCategory.System, "ConvertSurfaceAndColorspace: Surface pointer is null.");
            return nint.Zero;
        }
        return SDL_ConvertSurfaceAndColorspace(surface, format, palette, colorspace, props);
    }

    /// <summary>Copy a group of properties.</summary>
    /// <param name="src">the properties to copy.</param>
    /// <param name="dst">the destination properties.</param>
    /// <remarks>
    /// Copy all the properties from one group of properties to another, with the
    /// exception of properties requiring cleanup (set using <see cref="SetPointerPropertyWithCleanup"/>),
    /// which will not be copied. Any property that already exists on dst will be overwritten.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool CopyProperties(uint src, uint dst) {
        if (src == 0 || dst == 0) {
            LogWarn(LogCategory.System, "CopyProperties: Source or destination properties handle is zero.");
            return false;
        }
        return SDL_CopyProperties(src, dst);
    }

    /// <summary>Create a palette structure with the specified number of color entries.</summary>
    /// <param name="ncolors">represents the number of color entries in the color palette.</param>
    /// <remarks>
    /// The palette entries are initialized to white.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="DestroyPalette"/>
    /// <seealso cref="SetPaletteColors"/>
    /// <seealso cref="SetSurfacePalette"/>
    /// </remarks>
    /// <returns>(SDL_Palette *) Returns a new SDL_Palette structure on success or <see langword="null" /> on failure (e.g. if there wasn't enough memory); call <see cref="GetError()" /> for more information.</returns>
    public static nint CreatePalette(int ncolors) {
        if (ncolors <= 0) {
            LogError(LogCategory.Error, "CreatePalette: Number of colors must be greater than zero.");
        }

        nint palette = SDL_CreatePalette(ncolors);
        if (palette == nint.Zero) {
            LogError(LogCategory.Error, "CreatePalette: Failed to create palette.");
        }

        return palette;
    }

    /// <summary>Create a child popup window of the specified parent window.</summary>
    /// <param name="parent">the parent of the window, must not be <see langword="null" />.</param>
    /// <param name="offsetX">the x position of the popup window relative to the origin of the parent.</param>
    /// <param name="offsetY">the y position of the popup window relative to the origin of the parent window.</param>
    /// <param name="w">the width of the window.</param>
    /// <param name="h">the height of the window.</param>
    /// <param name="flags"><see cref="WindowFlags.Tooltip"/> or <see cref="WindowFlags.PopupMenu"/>, and zero or more additional <see cref="WindowFlags"/> OR'd together.</param>
    /// <remarks>
    /// The window size is a request and may be different than expected based on
    /// the desktop layout and window manager policies. Your application should be
    /// prepared to handle a window of any size.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateWindow"/>
    /// <seealso cref="CreateWindowWithProperties"/>
    /// <seealso cref="DestroyWindow"/>
    /// <seealso cref="GetWindowParent"/>
    /// </remarks>
    /// <returns>(SDL_Window *) Returns the window that was created or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static nint CreatePopupWindow(nint parent, int offsetX, int offsetY, int w, int h, WindowFlags flags) {
        if (parent == nint.Zero) {
            LogError(LogCategory.Error, "CreatePopupWindow: Parent window handle is null.");
            return nint.Zero;
        }
        if (w <= 0 || h <= 0) {
            LogError(LogCategory.Error, "CreatePopupWindow: Invalid width or height.");
            return nint.Zero;
        }

        if (!Enum.IsDefined(flags)) {
            LogError(LogCategory.Error, "CreatePopupWindow: Invalid window flags.");
            return nint.Zero;
        }

        nint popupWindow = SDL_CreatePopupWindow(parent, offsetX, offsetY, w, h, flags);
        if (popupWindow == nint.Zero) {
            LogError(LogCategory.Error, "CreatePopupWindow: Failed to create popup window.");
        }
        return popupWindow;
    }

    /// <summary>Create a group of properties.</summary>
    /// <remarks>
    /// All properties are automatically destroyed when <see cref="Quit"/> is called.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="DestroyProperties"/>
    /// </remarks>
    /// <returns>Returns an ID for a new group of properties, or 0 on failure; call <see cref="GetError()" /> for more information.</returns>
    public static uint CreateProperties() {
        uint props = SDL_CreateProperties();
        if (props == 0) {
            LogError(LogCategory.Error, "CreateProperties: Failed to create properties.");
        }

        return props;
    }

    /// <summary>Allocate a new surface with a specific pixel format.</summary>
    /// <param name="width">the width of the surface.</param>
    /// <param name="height">the height of the surface.</param>
    /// <param name="format">the <see cref="PixelFormat"/> for the new surface's pixel format.</param>
    /// <remarks>
    /// The pixels of the new surface are initialized to zero.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateSurfaceFrom"/>
    /// <seealso cref="DestroySurface"/>
    /// </remarks>
    /// <returns>(SDL_Surface *) Returns the new SDL_Surfacestructure that is created or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static nint CreateSurface(int width, int height, PixelFormat format) {
        if (width <= 0 || height <= 0) {
            LogError(LogCategory.Error, "CreateSurface: Invalid width or height.");
            return nint.Zero;
        }

        return SDL_CreateSurface(width, height, format);
    }

    /// <summary>Allocate a new surface with a specific pixel format and existing pixel data.</summary>
    /// <param name="width">the width of the surface.</param>
    /// <param name="height">the height of the surface.</param>
    /// <param name="format">the <see cref="PixelFormat"/> for the new surface's pixel format.</param>
    /// <param name="pixels">a pointer to existing pixel data.</param>
    /// <param name="pitch">the number of bytes between each row, including padding.</param>
    /// <remarks>
    /// No copy is made of the pixel data. Pixel data is not managed automatically;
    /// you must free the surface before you free the pixel data.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateSurface"/>
    /// <seealso cref="DestroySurface"/>
    /// </remarks>
    /// <returns>(SDL_Surface *) Returns the new SDL_Surface structure that is created or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static nint CreateSurfaceFrom(int width, int height, PixelFormat format, nint pixels, int pitch) {
        if (pixels == nint.Zero) {
            LogError(LogCategory.System, "CreateSurfaceFrom: Pixels pointer is null.");
            return nint.Zero;
        }

        if (!Enum.IsDefined(format)) {
            LogError(LogCategory.Error, "CreateSurfaceFrom: Invalid pixel format.");
            return nint.Zero;
        }

        return SDL_CreateSurfaceFrom(width, height, format, pixels, pitch);
    }

    /// <summary>Create a palette and associate it with a surface.</summary>
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <remarks>
    /// This function creates a palette compatible with the provided surface. The
    /// palette is then returned for you to modify, and the surface will
    /// automatically use the new palette in future operations. You do not need to
    /// destroy the returned palette, it will be freed when the reference count
    /// reaches 0, usually when the surface is destroyed.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetPaletteColors"/>
    /// </remarks>
    /// <returns>(SDL_Palette *) Returns a new SDL_Palettestructure on success or <see langword="null" /> on failure (e.g. if the surface didn't have anindex format); call <see cref="GetError()" /> for more information.</returns>
    public static nint CreateSurfacePalette(nint surface) {
        if (surface == nint.Zero) {
            LogError(LogCategory.System, "CreateSurfacePalette: Surface pointer is null.");
            return nint.Zero;
        }
        return SDL_CreateSurfacePalette(surface);
    }

    public static nint CreateThreadRuntime(SdlThreadFunction fn, string name, nint data, nint pfnBeginThread, nint pfnEndThread) {
        if (fn == null) {
            LogWarn(LogCategory.System, "CreateThreadRuntime: Thread function is null.");
            return nint.Zero;
        }
        return SDL_CreateThreadRuntime(fn, name, data, pfnBeginThread, pfnEndThread);
    }

    public static nint CreateThreadWithPropertiesRuntime(uint props, nint pfnBeginThread, nint pfnEndThread) {
        if (props == 0) {
            LogWarn(LogCategory.System, "CreateThreadWithPropertiesRuntime: Properties handle is zero.");
            return nint.Zero;
        }
        if (pfnBeginThread == nint.Zero || pfnEndThread == nint.Zero) {
            LogWarn(LogCategory.System, "CreateThreadWithPropertiesRuntime: Begin or End thread function pointer is null.");
            return nint.Zero;
        }

        nint threadHandle = SDL_CreateThreadWithPropertiesRuntime(props, pfnBeginThread, pfnEndThread);
        if (threadHandle == nint.Zero) {
            LogError(LogCategory.Error, "CreateThreadWithPropertiesRuntime: Failed to create thread with properties.");
        }

        return threadHandle;
    }

    /// <summary>Create a window with the specified dimensions and flags.</summary>
    /// <param name="title">the title of the window, in UTF-8 encoding.</param>
    /// <param name="w">the width of the window.</param>
    /// <param name="h">the height of the window.</param>
    /// <param name="flags">0, or one or more <see cref="WindowFlags"/> OR'd together.</param>
    /// <remarks>
    /// The window size is a request and may be different than expected based on
    /// the desktop layout and window manager policies. Your application should be
    /// prepared to handle a window of any size.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateWindowAndRenderer"/>
    /// <seealso cref="CreatePopupWindow"/>
    /// <seealso cref="CreateWindowWithProperties"/>
    /// <seealso cref="DestroyWindow"/>
    /// </remarks>
    /// <returns>(SDL_Window *) Returns the window that was created or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static nint CreateWindow(string title, int w, int h, WindowFlags flags) {
        if (string.IsNullOrEmpty(title)) {
            LogWarn(LogCategory.System, "CreateWindow: Title is null or empty.");
            return nint.Zero;
        }
        return SDL_CreateWindow(title, w, h, flags);
    }

    /// <summary>Create a window with the specified properties.</summary>
    /// <param name="props">the properties to use.</param>
    /// <remarks>
    /// The window size is a request and may be different than expected based on
    /// the desktop layout and window manager policies. Your application should be
    /// prepared to handle a window of any size.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateProperties"/>
    /// <seealso cref="CreateWindow"/>
    /// <seealso cref="DestroyWindow"/>
    /// </remarks>
    /// <returns>(SDL_Window *) Returns the window that was created or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static nint CreateWindowWithProperties(uint props) {
        if (props == 0) {
            LogWarn(LogCategory.System, "CreateWindowWithProperties: Properties handle is zero.");
            return nint.Zero;
        }

        nint windowHandle = SDL_CreateWindowWithProperties(props);
        if (windowHandle == nint.Zero) {
            LogError(LogCategory.Error, "CreateWindowWithProperties: Failed to create window with properties.");
            throw new InvalidOperationException("CreateWindowWithProperties failed.");
        }

        return windowHandle;
    }

    /// <summary>Free a palette created with <see cref="CreatePalette"/>.</summary>
    /// <param name="palette">the SDL_Palette structure to be freed.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, as long as the palette is not modified or destroyed in another thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreatePalette"/>
    /// </remarks>
    public static void DestroyPalette(nint palette) {
        if (palette == nint.Zero) {
            LogWarn(LogCategory.System, "DestroyPalette: Palette pointer is null.");
            return;
        }
        SDL_DestroyPalette(palette);
    }

    /// <summary>Destroy a group of properties.</summary>
    /// <param name="props">the properties to destroy.</param>
    /// <remarks>
    /// All properties are deleted and their cleanup functions will be called, if any.
    /// <para><strong>Thread Safety:</strong> This function should not be called while these properties are locked orother threads might be setting or getting values from these properties.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateProperties"/>
    /// </remarks>
    public static void DestroyProperties(uint props) {
        if (props == 0) {
            LogWarn(LogCategory.System, "DestroyProperties: Properties handle is zero.");
            return;
        }
        SDL_DestroyProperties(props);
    }

    /// <summary>Free a surface.</summary>
    /// <param name="surface">the <see cref="Surface"/> to free.</param>
    /// <remarks>
    /// It is safe to pass <see cref="nint.Zero" /> to this function.
    /// <para><strong>Thread Safety:</strong> No other thread should be using the surface when it is freed.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateSurface"/>
    /// <seealso cref="CreateSurfaceFrom"/>
    /// </remarks>
    public static void DestroySurface(nint surface) {
        if (surface == nint.Zero) {
            LogInfo(LogCategory.System, "Will destroy nothing.");
        }

        SDL_DestroySurface(surface);
    }

    /// <summary>Destroy a window.</summary>
    /// <param name="window">the window to destroy.</param>
    /// <remarks>
    /// Any child windows owned by the window will be recursively destroyed as well.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreatePopupWindow"/>
    /// <seealso cref="CreateWindow"/>
    /// <seealso cref="CreateWindowWithProperties"/>
    /// </remarks>
    public static void DestroyWindow(nint window) {
        if (window == nint.Zero) {
            LogWarn(LogCategory.System, "DestroyWindow: Window handle is null.");
            return;
        }
        SDL_DestroyWindow(window);
    }

    /// <summary>Destroy the surface associated with the window.</summary>
    /// <param name="window">the window to update.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowSurface"/>
    /// <seealso cref="WindowHasSurface"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool DestroyWindowSurface(nint window) {
        if (window == nint.Zero) {
            LogWarn(LogCategory.System, "DestroyWindowSurface: Window handle is null.");
            return false;
        }
        return SDL_DestroyWindowSurface(window);
    }

    /// <summary>Let a thread clean up on exit without intervention.</summary>
    /// <param name="thread">the SDL_Thread pointer that was returned from the <see cref="CreateThread"/> call that started this thread.</param>
    /// <remarks>
    /// A thread may be &quot;detached&quot; to signify that it should not remain until
    /// another thread has called <see cref="WaitThread"/> on it.
    /// Detaching a thread is useful for long-running threads that nothing needs to
    /// synchronize with or further manage. When a detached thread is done, it simply goes away.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateThread"/>
    /// <seealso cref="WaitThread"/>
    /// </remarks>
    public static void DetachThread(nint thread) {
        if (thread == nint.Zero) {
            LogWarn(LogCategory.System, "DetachThread: Thread handle is null.");
            return;
        }
        SDL_DetachThread(thread);
    }

    /// <summary>Prevent the screen from being blanked by a screen saver.</summary>
    /// <remarks>
    /// If you disable the screensaver, it is automatically re-enabled when SDL quits.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="EnableScreenSaver"/>
    /// <seealso cref="ScreenSaverEnabled"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool DisableScreenSaver() {
        return SDL_DisableScreenSaver();
    }

    /// <summary>Creates a new surface identical to the existing surface.</summary>
    /// <param name="surface">the surface to duplicate.</param>
    /// <remarks>
    /// If the original surface has alternate images, the new surface will have a reference to them as well.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="DestroySurface"/>
    /// </remarks>
    /// <returns>(SDL_Surface *) Returns a copy of the surface or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static nint DuplicateSurface(nint surface) {
        if (surface == nint.Zero) {
            LogWarn(LogCategory.System, "DuplicateSurface: Surface pointer is null.");
            return nint.Zero;
        }
        return SDL_DuplicateSurface(surface);
    }

    /// <summary>Allow the screen to be blanked by a screen saver.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="DisableScreenSaver"/>
    /// <seealso cref="ScreenSaverEnabled"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool EnableScreenSaver() {
        return SDL_EnableScreenSaver();
    }

    /// <summary>An entry point for SDL's use in SDL_MAIN_USE_CALLBACKS.</summary>
    /// <param name="argc">standard Unix main argc.</param>
    /// <param name="argv">standard Unix main argv.</param>
    /// <param name="appInit">the application's SDL_AppInit function.</param>
    /// <param name="appIter">the application's SDL_AppIterate function.</param>
    /// <param name="appEvent">the application's SDL_AppEvent function.</param>
    /// <param name="appQuit">the application's SDL_AppQuit function.</param>
    /// <remarks>
    /// Generally, you should not call this function directly. This only exists to
    /// hand off work into SDL as soon as possible, where it has a lot more control
    /// and functionality available, and make the inline code in SDL_main.h as small as possible.
    /// <para><strong>Thread Safety:</strong> It is not safe to call this anywhere except as the only function call inSDL_main.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns standard Unix main return value.</returns>
    public static int EnterAppMainCallbacks(int argc, nint argv, SdlAppInitFunc appInit,
                                                                       SdlAppIterateFunc appIter, SdlAppEventFunc appEvent, SdlAppQuitFunc appQuit) {
        ArgumentNullException.ThrowIfNull(appInit);
        ArgumentNullException.ThrowIfNull(appIter);
        ArgumentNullException.ThrowIfNull(appEvent);
        ArgumentNullException.ThrowIfNull(appQuit);

        LogDebug(LogCategory.System, "Entering App Main Callbacks with provided delegates.");

        return SDL_EnterAppMainCallbacks(argc, argv, appInit, appIter, appEvent, appQuit);
    }

    /// <summary>Enumerate the properties contained in a group of properties.</summary>
    /// <param name="props">the properties to query.</param>
    /// <param name="callback">the function to call for each property.</param>
    /// <param name="userdata">a pointer that is passed to callback.</param>
    /// <remarks>
    /// The callback function is called for each property in the group of
    /// properties. The properties are locked during enumeration.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool EnumerateProperties(uint props, SdlEnumeratePropertiesCallback callback, nint userdata) {
        if (props == 0 || callback == null) {
            LogWarn(LogCategory.System, "EnumerateProperties: Properties handle is zero or callback is null.");
            return false;
        }
        return SDL_EnumerateProperties(props, callback, userdata);
    }

    /// <summary>Perform a fast fill of a rectangle with a specific color.</summary>
    /// <param name="dst">the <see cref="Surface"/> structure that is the drawing target.</param>
    /// <param name="rect">the <see cref="Rect"/> structure representing the rectangle to fill, or <see cref="nint.Zero" /> to fill the entire surface.</param>
    /// <param name="color">the color to fill with.</param>
    /// <remarks>
    /// color should be a pixel of the format used by the surface, and can be
    /// generated by <see cref="MapRgb"/> or <see cref="MapRgba"/>. If
    /// the color value contains an alpha component then the destination is simply
    /// filled with that alpha information, no blending takes place.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="FillSurfaceRects"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static unsafe bool FillSurfaceRect(nint dst, Rect rect, uint color) {
        if (dst == nint.Zero) {
            LogWarn(LogCategory.System, "FillSurfaceRect: Destination pointer is null.");
            return false;
        }
        nint rectPtr = Marshal.AllocHGlobal(sizeof(Rect));
        *(Rect*)rectPtr = rect;
        bool result = SDL_FillSurfaceRect(dst, rectPtr, color);
        if (!result) {
            LogError(LogCategory.Error, "FillSurfaceRect: Failed to fill surface rectangle.");
        }
        Marshal.FreeHGlobal(rectPtr);
        return result;
    }

    /// <summary>Perform a fast fill of a set of rectangles with a specific color.</summary>
    /// <param name="dst">the <see cref="Surface"/> structure that is the drawing target.</param>
    /// <param name="rects">an array of <see cref="Rect"/>s representing the rectangles to fill.</param>
    /// <param name="color">the color to fill with.</param>
    /// <remarks>
    /// color should be a pixel of the format used by the surface, and can be
    /// generated by <see cref="MapRgb"/> or <see cref="MapRgba"/>. If
    /// the color value contains an alpha component then the destination is simply
    /// filled with that alpha information, no blending takes place.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="FillSurfaceRect"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
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

    /// <summary>Request a window to demand attention from the user.</summary>
    /// <param name="window">the window to be flashed.</param>
    /// <param name="operation">the operation to perform.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool FlashWindow(nint window, FlashOperation operation) {
        if (window == nint.Zero) {
            LogWarn(LogCategory.System, "FlashWindow: Window handle is null.");
            return false;
        }

        if (!Enum.IsDefined(operation)) {
            LogError(LogCategory.Error, "FlashWindow: Invalid flash operation.");
            return false;
        }

        bool result = SDL_FlashWindow(window, operation);
        if (!result) {
            LogError(LogCategory.Error, "FlashWindow: Failed to flash window.");
        }
        return result;
    }

    /// <summary>Flip a surface vertically or horizontally.</summary>
    /// <param name="surface">the surface to flip.</param>
    /// <param name="flip">the direction to flip.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool FlipSurface(nint surface, FlipMode flip) {
        if (surface == nint.Zero) {
            LogWarn(LogCategory.System, "FlipSurface: Surface pointer is null.");
            return false;
        }
        return SDL_FlipSurface(surface, flip);
    }

    public static void Free(nint mem) {
        if (mem == nint.Zero) {
            LogWarn(LogCategory.System, "Free: Memory pointer is null.");
            return;
        }

        SDL_free(mem);
    }

    /// <summary>Callback from the application to let the suspend continue.</summary>
    /// <remarks>
    /// This function is only needed for Xbox GDK support; all other platforms will
    /// do nothing and set an &quot;unsupported&quot; error message.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    public static void GdkSuspendComplete() {
        SDL_GDKSuspendComplete();
    }

    /// <summary>Get metadata about your app.</summary>
    /// <param name="name">the name of the metadata property to get.</param>
    /// <remarks>
    /// This returns metadata previously set using <see cref="SetAppMetadata"/> or <see cref="SetAppMetadataProperty"/>.
    /// <para>See <see cref="SetAppMetadataProperty"/> for the list of available properties and their meanings.</para>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, although the stringreturned is not protected and could potentially be freed if you callSDL_SetAppMetadataProperty() to set that property from another thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAppMetadata"/>
    /// <seealso cref="SetAppMetadataProperty"/>
    /// </remarks>
    /// <returns>Returns the current value of the metadata property, or the default if it is not set, <see langword="null" /> for properties with no default.</returns>
    public static string GetAppMetadataProperty(string name) {
        if (string.IsNullOrEmpty(name)) {
            LogWarn(LogCategory.System, "GetAppMetadataProperty: Name is null or empty.");
            return string.Empty;
        }
        string result = SDL_GetAppMetadataProperty(name);
        if (string.IsNullOrEmpty(result)) {
            LogError(LogCategory.Error, "GetAppMetadataProperty: Failed to retrieve property.");
        }
        return result;
    }

    /// <summary>Get a boolean property from a group of properties.</summary>
    /// <param name="props">the properties to query.</param>
    /// <param name="name">the name of the property to query.</param>
    /// <param name="defaultValue">the default value of the property.</param>
    /// <remarks>
    /// You can use  <see cref="GetPropertyType"/> to query whether the property exists and is a boolean property.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetPropertyType"/>
    /// <seealso cref="HasProperty"/>
    /// <seealso cref="SetBooleanProperty"/>
    /// </remarks>
    /// <returns>Returns the value of the property, or <paramref name="defaultValue"/> if it is not set or not a boolean property.</returns>
    public static bool GetBooleanProperty(uint props, string name, bool defaultValue) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            LogWarn(LogCategory.System, "GetBooleanProperty: Properties handle is zero or name is null/empty.");
            return defaultValue;
        }
        bool result = SDL_GetBooleanProperty(props, name, defaultValue);
        if (!result) {
            LogError(LogCategory.Error, "GetBooleanProperty: Failed to retrieve boolean property.");
        }
        return result;
    }

    /// <summary>Get the data from clipboard for a given mime type.</summary>
    /// <param name="mimeType">the mime type to read from the clipboard.</param>
    /// <remarks>
    /// The size of text data does not include the terminator, but the text is guaranteed to be null terminated.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="HasClipboardData"/>
    /// <seealso cref="SetClipboardData"/>
    /// </remarks>
    /// <returns>(void *) Returns the retrieved data buffer or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information. This should be freedwith <see cref="Free"/> when it is no longer needed.</returns>
    public static Span<nint> GetClipboardData(string mimeType) {
        if (string.IsNullOrEmpty(mimeType)) {
            LogWarn(LogCategory.System, "GetClipboardData: MimeType is null or empty.");
            return [];
        }
        nint result = SDL_GetClipboardData(mimeType, out nuint size);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "GetClipboardData: Failed to retrieve clipboard data.");
            return [];
        }

        if (size == 0) {
            LogWarn(LogCategory.System, "GetClipboardData: Retrieved data size is zero.");
            return [];
        }

        nint[] data = new nint[size];
        Marshal.Copy(result, data, 0, (int)size);

        return new Span<nint>(data);
    }

    /// <summary>Retrieve the list of mime types available in the clipboard.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetClipboardData"/>
    /// </remarks>
    /// <returns>(char **) Returns a null terminated array of strings with mime types, or<see langword="null" /> on failure; call <see cref="GetError()" /> for more information.This should be freed with <see cref="Free"/> when it is no longer needed.</returns>
    public static Span<nint> GetClipboardMimeTypes() {
        nint result = SDL_GetClipboardMimeTypes(out nuint numMimeTypes);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "GetClipboardMimeTypes: Failed to retrieve clipboard mime types.");
            return [];
        }

        nint[] data = new nint[numMimeTypes];
        Marshal.Copy(result, data, 0, (int)numMimeTypes);

        return new Span<nint>(data);
    }

    /// <summary>Retrieve the list of mime types available in the clipboard.</summary>
    /// <param name="numMimeTypes">a pointer filled with the number of mime types, may be discarded.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetClipboardData"/>
    /// </remarks>
    /// <returns>(char **) Returns a null terminated array of strings with mime types, or<see langword="null" /> on failure; call <see cref="GetError()" /> for more information.This should be freed with <see cref="Free"/> when it is no longer needed.</returns>
    public static nint GetClipboardMimeTypes(out nuint numMimeTypes) {
        nint result = SDL_GetClipboardMimeTypes(out numMimeTypes);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "GetClipboardMimeTypes: Failed to retrieve clipboard mime types.");
            return nint.Zero;
        }
        return result;
    }

    /// <summary>Get UTF-8 text from the clipboard.</summary>
    /// <remarks>
    /// This functions returns an empty string if there was not enough memory left for a copy of the clipboard's content.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="HasClipboardText"/>
    /// <seealso cref="SetClipboardText"/>
    /// </remarks>
    /// <returns>(char *) Returns the clipboard text on success or an empty string on failure; call <see cref="GetError()" /> for more information. Thisshould be freed with <see cref="Free"/> when it is no longer needed.</returns>
    public static string GetClipboardText() {
        string result = SDL_GetClipboardText();
        if (string.IsNullOrEmpty(result)) {
            LogError(LogCategory.Error, "GetClipboardText: Failed to retrieve clipboard text.");
        }
        return result;
    }

    /// <summary>Get the closest match to the requested display mode.</summary>
    /// <param name="displayId">the instance ID of the display to query.</param>
    /// <param name="w">the width in pixels of the desired display mode.</param>
    /// <param name="h">the height in pixels of the desired display mode.</param>
    /// <param name="refreshRate">the refresh rate of the desired display mode, or 0.0f for the desktop refresh rate.</param>
    /// <param name="includeHighDensityModes">boolean to include high density modes in the search.</param>
    /// <param name="closest">a pointer filled in with the closest display mode equal to or larger than the desired mode.</param>
    /// <remarks>
    /// The available display modes are scanned and closest is filled in with the
    /// closest mode matching the requested mode and returned. The mode format and
    /// refresh rate default to the desktop mode if they are set to 0. The modes
    /// are scanned with size being first priority, format being second priority,
    /// and finally checking the refresh rate. If all the available modes are too
    /// small, then <see langword="false" /> is returned.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetDisplays"/>
    /// <seealso cref="GetFullscreenDisplayModes"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool GetClosestFullscreenDisplayMode(uint displayId, int w, int h, float refreshRate,
                bool includeHighDensityModes, out DisplayMode closest) {
        if (displayId == 0) {
            LogWarn(LogCategory.System, "GetClosestFullscreenDisplayMode: Display ID is zero.");
            closest = default;
            return false;
        }
        bool result = SDL_GetClosestFullscreenDisplayMode(displayId, w, h, refreshRate, includeHighDensityModes,
            out closest);
        if (!result) {
            LogError(LogCategory.Error, "GetClosestFullscreenDisplayMode: Failed to retrieve closest mode.");
        }
        return result;
    }

    /// <summary>Get information about the current display mode.</summary>
    /// <param name="displayId">the instance ID of the display to query.</param>
    /// <remarks>
    /// There's a difference between this function and
    /// <see cref="GetDesktopDisplayMode"/> when SDL runs
    /// fullscreen and has changed the resolution. In that case this function will
    /// return the current display mode, and not the previous native display mode.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetDesktopDisplayMode"/>
    /// <seealso cref="GetDisplays"/>
    /// </remarks>
    /// <returns>(const SDL_DisplayMode *) Returns a pointer to the desktop display mode or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static nint GetCurrentDisplayMode(uint displayId) {
        if (displayId == 0) {
            LogWarn(LogCategory.System, "GetCurrentDisplayMode: Display ID is zero.");
            return nint.Zero;
        }
        nint mode = SDL_GetCurrentDisplayMode(displayId);
        if (mode == nint.Zero) {
            LogError(LogCategory.Error, "GetCurrentDisplayMode: Failed to retrieve current mode.");
        }
        return mode;
    }

    /// <summary>Get information about the current display mode.</summary>
    /// <param name="displayId">the instance ID of the display to query.</param>
    /// <param name="mode">the <see cref="DisplayMode"/></param>
    /// <remarks>
    /// There's a difference between this function and
    /// <see cref="GetDesktopDisplayMode"/> when SDL runs
    /// fullscreen and has changed the resolution. In that case this function will
    /// return the current display mode, and not the previous native display mode.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetDesktopDisplayMode"/>
    /// <seealso cref="GetDisplays"/>
    /// </remarks>
    public static unsafe void GetCurrentDisplayMode(uint displayId, out DisplayMode mode) {
        if (displayId == 0) {
            LogWarn(LogCategory.System, "GetCurrentDisplayMode: Display ID is zero.");
            mode = default;
            return;
        }
        nint modePtr = SDL_GetCurrentDisplayMode(displayId);
        if (modePtr == nint.Zero) {
            LogError(LogCategory.Error, "GetCurrentDisplayMode: Failed to retrieve current mode.");
            mode = default;
            return;
        }

        mode = *(DisplayMode*)modePtr;
    }

    /// <summary>Get the orientation of a display.</summary>
    /// <param name="displayId">the instance ID of the display to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetDisplays"/>
    /// </remarks>
    /// <returns>Returns theSDL_DisplayOrientation enum value of the display,  SDL_ORIENTATION_UNKNOWN if it isn'tavailable.</returns>
    public static DisplayOrientation GetCurrentDisplayOrientation(uint displayId) {
        if (displayId == 0) {
            LogWarn(LogCategory.System, "GetCurrentDisplayOrientation: Display ID is zero.");
            return DisplayOrientation.Unknown;
        }
        DisplayOrientation orientation = SDL_GetCurrentDisplayOrientation(displayId);
        if (orientation == DisplayOrientation.Unknown) {
            LogError(LogCategory.Error, "GetCurrentDisplayOrientation: Failed to retrieve orientation.");
        }
        return orientation;
    }

    /// <summary>Get the thread identifier for the current thread.</summary>
    /// <remarks>
    /// This thread identifier is as reported by the underlying operating system.
    /// If SDL is running on a platform that does not support threads the return
    /// value will always be zero.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetThreadId"/>
    /// </remarks>
    /// <returns>Returns the ID of the current thread.</returns>
    public static ulong GetCurrentThreadId() {
        ulong threadId = SDL_GetCurrentThreadID();
        if (threadId == 0) {
            LogError(LogCategory.Error, "GetCurrentThreadID: Failed to retrieve thread ID.");
        }
        return threadId;
    }

    /// <summary>Get the name of the currently initialized video driver.</summary>
    /// <remarks>
    /// The names of drivers are all simple, low-ASCII identifiers, like &quot;cocoa&quot;,
    /// &quot;x11&quot; or &quot;windows&quot;. These never have Unicode characters, and are not meant
    /// to be proper names.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetNumVideoDrivers"/>
    /// <seealso cref="GetVideoDriver"/>
    /// </remarks>
    /// <returns>Returns the name of the current video driver or <see langword="null" /> if nodriver has been initialized.</returns>
    public static string GetCurrentVideoDriver() {
        return SDL_GetCurrentVideoDriver();
    }

    /// <summary>Get information about the desktop's display mode.</summary>
    /// <param name="displayId">the instance ID of the display to query.</param>
    /// <remarks>
    /// There's a difference between this function and
    /// <see cref="GetCurrentDisplayMode"/> when SDL runs
    /// fullscreen and has changed the resolution. In that case this function will
    /// return the previous native display mode, and not the current display mode.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetCurrentDisplayMode"/>
    /// <seealso cref="GetDisplays"/>
    /// </remarks>
    /// <returns>Returns a <see cref="DisplayMode"/> structure to the desktop display mode or <see langword="default" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static DisplayMode GetDesktopDisplayMode(uint displayId) {
        GetDesktopDisplayMode(displayId, out DisplayMode mode);
        return mode;
    }

    /// <summary>Get information about the desktop's display mode.</summary>
    /// <param name="displayId">the instance ID of the display to query.</param>
    /// <param name="mode">the <see cref="DisplayMode"/>.</param>
    /// <remarks>
    /// There's a difference between this function and
    /// <see cref="GetCurrentDisplayMode"/> when SDL runs
    /// fullscreen and has changed the resolution. In that case this function will
    /// return the previous native display mode, and not the current display mode.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetCurrentDisplayMode"/>
    /// <seealso cref="GetDisplays"/>
    /// </remarks>
    public static unsafe void GetDesktopDisplayMode(uint displayId, out DisplayMode mode) {
        if (displayId == 0) {
            LogWarn(LogCategory.System, "GetDesktopDisplayMode: Display ID is zero.");
            mode = default;
            return;
        }
        nint modePtr = SDL_GetDesktopDisplayMode(displayId);
        if (modePtr == nint.Zero) {
            LogError(LogCategory.Error, "GetDesktopDisplayMode: Failed to retrieve desktop mode.");
            mode = default;
            return;
        }
        mode = *(DisplayMode*)modePtr;
    }

    /// <summary>Get the desktop area represented by a display.</summary>
    /// <param name="displayId">the instance ID of the display to query.</param>
    /// <param name="rect">the <see cref="Rect"/> structure filled in with the display bounds.</param>
    /// <remarks>
    /// The primary display is often located at (0,0), but may be placed at a
    /// different location depending on monitor layout.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetDisplayUsableBounds"/>
    /// <seealso cref="GetDisplays"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool GetDisplayBounds(uint displayId, out Rect rect) {
        if (displayId == 0) {
            LogWarn(LogCategory.System, "GetDisplayBounds: Display ID is zero.");
            rect = default;
            return false;
        }
        bool result = SDL_GetDisplayBounds(displayId, out rect);
        if (!result) {
            LogError(LogCategory.Error, "GetDisplayBounds: Failed to retrieve display bounds.");
        }
        return result;
    }

    /// <summary>Get the content scale of a display.</summary>
    /// <param name="displayId">the instance ID of the display to query.</param>
    /// <remarks>
    /// The content scale is the expected scale for content based on the DPI
    /// settings of the display. For example, a 4K display might have a 2.0 (200%)
    /// display scale, which means that the user expects UI elements to be twice as
    /// big on this display, to aid in readability.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowDisplayScale"/>
    /// <seealso cref="GetDisplays"/>
    /// </remarks>
    /// <returns>Returns the content scale of the display, or 0.0f on failure; call <see cref="GetError()"/> for more information.</returns>
    public static float GetDisplayContentScale(uint displayId) {
        if (displayId == 0) {
            LogWarn(LogCategory.System, "GetDisplayContentScale: Display ID is zero.");
            return 0f;
        }
        float scale = SDL_GetDisplayContentScale(displayId);
        if (scale <= 0.01f) {
            LogError(LogCategory.Error, "GetDisplayContentScale: Failed to retrieve content scale.");
        }
        return scale;
    }

    /// <summary>Get the display containing a point.</summary>
    /// <param name="point">the point to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetDisplayBounds"/>
    /// <seealso cref="GetDisplays"/>
    /// </remarks>
    /// <returns>Returns the instance ID of the displaycontaining the point or 0 on failure; call <see cref="GetError()" /> for more information.</returns>
    public static uint GetDisplayForPoint(ref Point point) {
        uint displayId = SDL_GetDisplayForPoint(ref point);
        if (displayId == 0) {
            LogError(LogCategory.Error, "GetDisplayForPoint: Failed to retrieve display ID.");
        }
        return displayId;
    }

    /// <summary>Get the display primarily containing a rect.</summary>
    /// <param name="rect">the rect to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetDisplayBounds"/>
    /// <seealso cref="GetDisplays"/>
    /// </remarks>
    /// <returns>Returns the instance ID of the display entirely containing the rect or closest to the center of the rect on success or 0 on failure; call <see cref="GetError()" /> for more information.</returns>
    public static uint GetDisplayForRect(ref Rect rect) {
        uint displayId = SDL_GetDisplayForRect(ref rect);
        if (displayId == 0) {
            LogError(LogCategory.Error, "GetDisplayForRect: Failed to retrieve display ID.");
        }
        return displayId;
    }

    /// <summary>Get the display associated with a window.</summary>
    /// <param name="window">the window to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetDisplayBounds"/>
    /// <seealso cref="GetDisplays"/>
    /// </remarks>
    /// <returns>Returns the instance ID of the display containing the center of the window on success or 0 on failure; call <see cref="GetError()"/> for more information.</returns>
    public static uint GetDisplayForWindow(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetDisplayForWindow: Window handle is null.");
            return 0;
        }
        uint displayId = SDL_GetDisplayForWindow(window);
        if (displayId == 0) {
            LogError(LogCategory.Error, "GetDisplayForWindow: Failed to retrieve display ID.");
        }
        return displayId;
    }

    /// <summary>Get the name of a display in UTF-8 encoding.</summary>
    /// <param name="displayId">the instance ID of the display to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetDisplays"/>
    /// </remarks>
    /// <returns>Returns the name of a display or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static string GetDisplayName(uint displayId) {
        if (displayId == 0) {
            LogWarn(LogCategory.System, "GetDisplayName: Display ID is zero.");
            return string.Empty;
        }
        string name = SDL_GetDisplayName(displayId);
        if (string.IsNullOrEmpty(name)) {
            LogError(LogCategory.Error, "GetDisplayName: Failed to retrieve display name.");
        }
        return name;
    }

    /// <summary>Get the properties associated with a display.</summary>
    /// <param name="displayId">the instance ID of the display to query.</param>
    /// <remarks>
    /// The following read-only properties are provided by SDL:
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns a valid property ID on success or 0 on failure; call <see cref="GetError()" /> for more information.</returns>
    public static uint GetDisplayProperties(uint displayId) {
        if (displayId == 0) {
            LogWarn(LogCategory.System, "GetDisplayProperties: Display ID is zero.");
            return 0;
        }
        uint props = SDL_GetDisplayProperties(displayId);
        if (props == 0) {
            LogError(LogCategory.Error, "GetDisplayProperties: Failed to retrieve display properties.");
        }
        return props;
    }

    /// <summary>Get a list of currently connected displays.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(SDL_DisplayID *) Returns a 0 terminated array of display instance IDs or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information. This should be freed with <see cref="Free"/> when itis no longer needed.</returns>
    public static Span<nint> GetDisplays() {
        Span<nint> result = GetDisplays(out int _);
        if (result == []) {
            return [];
        }
        return result;
    }

    /// <summary>Get a list of currently connected displays.</summary>
    /// <param name="count">a pointer filled in with the number of displays returned, may bediscarded.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(SDL_DisplayID *) Returns a 0 terminated array of display instance IDs or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information. This should be freed with <see cref="Free"/> when itis no longer needed.</returns>
    public static Span<nint> GetDisplays(out int count) {
        nint result = SDL_GetDisplays(out count);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "GetDisplays: Failed to retrieve display handles.");
            return [];
        }

        nint[] data = new nint[count];
        Marshal.Copy(result, data, 0, count);

        return data;
    }

    /// <summary>Get the usable desktop area represented by a display, in screen coordinates.</summary>
    /// <param name="displayId">the instance ID of the display to query.</param>
    /// <param name="rect">the <see cref="Rect"/> structure filled in with the display bounds.</param>
    /// <remarks>
    /// This is the same area as <see cref="GetDisplayBounds"/>
    /// reports, but with portions reserved by the system removed. For example, on
    /// Apple's macOS, this subtracts the area occupied by the menu bar and dock.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetDisplayBounds"/>
    /// <seealso cref="GetDisplays"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool GetDisplayUsableBounds(uint displayId, out Rect rect) {
        if (displayId == 0) {
            LogWarn(LogCategory.System, "GetDisplayUsableBounds: Display ID is zero.");
            rect = default;
            return false;
        }
        bool result = SDL_GetDisplayUsableBounds(displayId, out rect);
        if (!result) {
            LogError(LogCategory.Error, "GetDisplayUsableBounds: Failed to retrieve usable bounds.");
        }
        return result;
    }

    /// <summary>Retrieve a message about the last error that occurred on the current thread.</summary>
    /// <remarks>
    /// It is possible for multiple errors to occur before calling/ <see cref="GetError()" />. Only the last error is returned.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ClearError"/>
    /// <seealso cref="SetError"/>
    /// </remarks>
    /// <returns>Returns a message with information about the specific error that occurred, or an empty string if there hasn't been an error message set since the last call to <see cref="ClearError"/>.</returns>
    public static string GetError() {
        string error = SDL_GetError();
        return string.IsNullOrEmpty(error) ? "No error." : error;
    }

    /// <summary>Get a floating point property from a group of properties.</summary>
    /// <param name="props">the properties to query.</param>
    /// <param name="name">the name of the property to query.</param>
    /// <param name="defaultValue">the default value of the property.</param>
    /// <remarks>
    /// You can use <see cref="GetPropertyType"/> to query whether
    /// the property exists and is a floating point property.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetPropertyType"/>
    /// <seealso cref="HasProperty"/>
    /// <seealso cref="SetFloatProperty"/>
    /// </remarks>
    /// <returns>Returns the value of the property, or default_value if it is not set or not a float property.</returns>
    public static float GetFloatProperty(uint props, string name, float defaultValue) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            LogWarn(LogCategory.System, "GetFloatProperty: Properties handle is zero or name is null/empty.");
            return defaultValue;
        }
        float result = SDL_GetFloatProperty(props, name, defaultValue);
        if (result <= 0.1f) {
            LogError(LogCategory.Error, "GetFloatProperty: Failed to retrieve float property.");
        }
        return result;
    }


    /// <summary>Get a list of fullscreen display modes available on a display.</summary>
    /// <param name="displayId">the instance ID of the display to query.</param>
    /// <remarks>
    /// The display modes are sorted in this priority:
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetDisplays"/>
    /// </remarks>
    /// <returns>(SDL_DisplayMode **) Returns a <see langword="null" /> terminated array of display mode pointers or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information. This is a singleallocation that should be freed with <see cref="Free"/> when it is nolonger needed.</returns>
    public static Span<int> GetFullscreenDisplayModes(uint displayId) {
        nint result = SDL_GetFullscreenDisplayModes(displayId, out int count);

        if (result == nint.Zero) {
            LogError(LogCategory.Error, "GetFullscreenDisplayModes: Failed to retrieve fullscreen display modes.");
            return [];
        }

        if (count <= 0) {
            LogWarn(LogCategory.System, "GetFullscreenDisplayModes: Retrieved count is zero or negative.");
            return [];
        }

        int[] data = new int[count];
        Marshal.Copy(result, data, 0, count);

        return data;
    }

    /// <summary>Get a list of fullscreen display modes available on a display.</summary>
    /// <param name="displayId">the instance ID of the display to query.</param>
    /// <param name="count">a pointer filled in with the number of display modes returned, may be discarded.</param>
    /// <remarks>
    /// The display modes are sorted in this priority:
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetDisplays"/>
    /// </remarks>
    /// <returns>(SDL_DisplayMode **) Returns a <see langword="null" /> terminated array of display mode pointers or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information. This is a single allocation that should be freed with <see cref="Free"/> when it is no longer needed.</returns>
    public static Span<nint> GetFullscreenDisplayModes(uint displayId, out int count) {
        nint result = SDL_GetFullscreenDisplayModes(displayId, out count);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "GetFullscreenDisplayModes: Failed to retrieve fullscreen display modes.");
            return [];
        }

        if (count <= 0) {
            LogWarn(LogCategory.System, "GetFullscreenDisplayModes: Retrieved count is zero or negative.");
            return [];
        }

        nint[] data = new nint[count];
        Marshal.Copy(result, data, 0, count);

        return data;
    }

    /// <summary>Get the global SDL properties.</summary>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns a valid property ID on success or 0 on failure; call <see cref="GetError()" /> for more information.</returns>
    public static uint GetGlobalProperties() {
        return SDL_GetGlobalProperties();
    }

    /// <summary>Get the window that currently has an input grab enabled.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetWindowMouseGrab"/>
    /// <seealso cref="SetWindowKeyboardGrab"/>
    /// </remarks>
    /// <returns>(SDL_Window *) Returns the window if input is grabbed or <see langword="null" />otherwise.</returns>
    public static nint GetGrabbedWindow() {
        nint window = SDL_GetGrabbedWindow();
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetGrabbedWindow: Failed to retrieve grabbed window.");
        }
        return window;
    }

    /// <summary>Get the value of a hint.</summary>
    /// <param name="name">the hint to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, however the return value only remains valid until the hint is changed; if another thread might doso, the app should supply locks and/or make a copy of the string. Note that using a hint callback instead is always thread-safe, as SDL holds a lock onthe thread subsystem during the callback.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetHint"/>
    /// <seealso cref="SetHintWithPriority"/>
    /// </remarks>
    /// <returns>Returns the string value of a hint or <see langword="null" /> if the hint isn't set.</returns>
    public static string GetHint(string name) {
        if (string.IsNullOrEmpty(name)) {
            LogWarn(LogCategory.System, "GetHint: Name is null or empty.");
            return string.Empty;
        }
        string result = SDL_GetHint(name);
        if (string.IsNullOrEmpty(result)) {
            LogError(LogCategory.Error, "GetHint: Failed to retrieve hint.");
        }
        return result;
    }

    /// <summary>Get the boolean value of a hint variable.</summary>

    /// <param name="name">the name of the hint to get the boolean value from.</param>
    /// <param name="defaultValue">the value to return if the hint does not exist.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetHint"/>
    /// <seealso cref="SetHint"/>
    /// </remarks>
    /// <returns>Returns the boolean value of a hint or the provided default value if the hint does not exist.</returns>
    public static bool GetHintBoolean(string name, bool defaultValue) {
        if (string.IsNullOrEmpty(name)) {
            LogWarn(LogCategory.System, "GetHintBoolean: Name is null or empty.");
            return defaultValue;
        }
        bool result = SDL_GetHintBoolean(name, defaultValue);
        if (!result) {
            LogError(LogCategory.Error, "GetHintBoolean: Failed to retrieve hint boolean.");
        }
        return result;
    }

    /// <summary>
    /// Get a list of currently connected keyboards.
    /// </summary>
    /// <param name="count">the number of keyboards returned, may be discarded.</param>
    /// <remarks>
    /// Note that this will include any device or virtual driver that includes keyboard functionality, including some mice, KVM switches, motherboard power buttons, etc.
    /// <para>You should wait for input from a device before you consider it actively in use.</para>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0</para>
    /// </remarks>
    /// <returns>(SDL_KeyboardID *) Returns a 0 terminated array of keyboards instance IDs or <see cref="nint.Zero"/> on failure; call <see cref="GetError"/> for more information. This should be freed with <see cref="Free"/> when it is no longer needed.</returns>
    public static nint GetKeyboards(out int count) {
        nint result = SDL_GetKeyboards(out count);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "GetKeyboard: Failed to retrieve keyboard handles.");
            return nint.Zero;
        }
        return result;
    }

    /// <summary>
    /// Get a list of currently connected keyboards.
    /// </summary>
    /// <remarks>
    /// Note that this will include any device or virtual driver that includes keyboard functionality, including some mice, KVM switches, motherboard power buttons, etc.
    /// <para>You should wait for input from a device before you consider it actively in use.</para>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0</para>
    /// </remarks>
    /// <returns>(SDL_KeyboardID *) Returns a 0 terminated array of keyboards instance IDs or <see cref="nint.Zero"/> on failure; call <see cref="GetError"/> for more information. This should be freed with <see cref="Free"/> when it is no longer needed.</returns>
    public static Span<nint> GetKeyboards() {
        nint result = GetKeyboards(out int count);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "GetKeyboard: Failed to retrieve keyboard handles.");
            return [];
        }

        if (count <= 0) {
            LogWarn(LogCategory.System, "GetKeyboard: Retrieved count is zero or negative.");
            return [];
        }

        nint[] data = new nint[count];
        Marshal.Copy(result, data, 0, count);

        return data;
    }

    /// <summary>Query the window which currently has keyboard focus.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(SDL_Window *) Returns the window with keyboard focus.</returns>
    public static nint GetKeyboardFocus() {
        nint window = SDL_GetKeyboardFocus();
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetKeyboardFocus: Failed to retrieve keyboard focus.");
        }
        return window;
    }

    /// <summary>Get the name of a keyboard.</summary>
    /// <param name="instanceId">the keyboard instance ID.</param>
    /// <remarks>
    /// This function returns &quot;&quot; if the keyboard doesn't have a name.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetKeyboards"/>
    /// </remarks>
    /// <returns>Returns the name of the selected keyboard or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static string GetKeyboardNameForId(uint instanceId) {
        if (instanceId == 0) {
            LogWarn(LogCategory.System, "GetKeyboardNameForID: Instance ID is zero.");
            return string.Empty;
        }
        string name = SDL_GetKeyboardNameForID(instanceId);
        if (string.IsNullOrEmpty(name)) {
            LogError(LogCategory.Error, "GetKeyboardNameForID: Failed to retrieve keyboard name.");
        }
        return name;
    }

    /// <summary>Get a snapshot of the current state of the keyboard.</summary>
    /// <param name="numKeys">if non-<see langword="null" />, receives the length of the returned array.</param>
    /// <remarks>
    /// The pointer returned is a pointer to an internal SDL array. It will be
    /// valid for the whole lifetime of the application and should not be freed by the caller.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PumpEvents"/>
    /// <seealso cref="ResetKeyboard"/>
    /// </remarks>
    /// <returns>(const bool *) Returns a pointer to an array of key states.</returns>
    public static Span<bool> GetKeyboardState(out int numKeys) {
        nint result = SDL_GetKeyboardState(out numKeys);

        if (result == nint.Zero) {
            LogError(LogCategory.Error, "GetKeyboardState: Failed to retrieve keyboard state.");
            return [];
        }

        if (numKeys <= 0) {
            LogWarn(LogCategory.System, "GetKeyboardState: Retrieved count is zero or negative.");
            return [];
        }

        bool[] state = new bool[numKeys];
        for (int i = 0; i < numKeys; i++) {
            state[i] = Marshal.ReadByte(result, i) != 0;
        }

        return new(state);
    }

    /// <summary>Get a key code from a human-readable name.</summary>
    /// <param name="name">the human-readable key name.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetKeyFromScancode"/>
    /// <seealso cref="GetKeyName"/>
    /// <seealso cref="GetScancodeFromName"/>
    /// </remarks>
    /// <returns>Returns <see cref="Keycode"/>, or <see cref="Keycode.Unknown"/> if the name wasn't recognized; call <see cref="GetError()"/> for more information.</returns>
    public static Keycode GetKeyFromName(string name) {
        if (string.IsNullOrEmpty(name)) {
            LogWarn(LogCategory.System, "GetKeyFromName: Name is null or empty.");
            return 0;
        }
        uint key = SDL_GetKeyFromName(name);
        if (key == 0) {
            LogError(LogCategory.Error, "GetKeyFromName: Failed to retrieve key from name.");
        }
        return (Keycode)key;
    }

    /// <summary>Get the key code corresponding to the given scancode according to the current keyboard layout.</summary>
    /// <param name="scanCode">the desired SDL_Scancode to query.</param>
    /// <param name="modstate">the modifier state to use when translating the scancode to a keycode.</param>
    /// <param name="keyEvent"><see langword="true" /> if the keycode will be used in key events.</param>
    /// <remarks>
    /// If you want to get the keycode as it would be delivered in key events,
    /// including options specified in
    /// SDL_HINT_KEYCODE_OPTIONS, then you should pass <paramref name="keyEvent"/> as <see langword="true" />. Otherwise this function simply translates the scancode based on the given modifier state.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetKeyName"/>
    /// <seealso cref="GetScancodeFromKey"/>
    /// </remarks>
    /// <returns>Returns the <see cref="Keycode"/> that corresponds to the given <see cref="Scancode"/>.</returns>
    public static uint GetKeyFromScancode(Scancode scanCode, KeyMod modstate, bool keyEvent) {
        if (scanCode == Scancode.Unknown) {
            LogWarn(LogCategory.System, "GetKeyFromScancode: Scan code is unknown.");
            return 0;
        }
        uint key = SDL_GetKeyFromScancode(scanCode, modstate, keyEvent);
        if (key == 0) {
            LogError(LogCategory.Error, "GetKeyFromScancode: Failed to retrieve key from scan code.");
        }
        return key;
    }

    /// <summary>Get a human-readable name for a key.</summary>
    /// <param name="key">the desired <see cref="Keycode"/> to query.</param>
    /// <remarks>
    /// If the key doesn't have a name, this function returns an empty string (&quot;&quot;).
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetKeyFromName"/>
    /// <seealso cref="GetKeyFromScancode"/>
    /// <seealso cref="GetScancodeFromKey"/>
    /// </remarks>
    /// <returns>Returns a UTF-8 encoded string of the key name.</returns>
    public static string GetKeyName(Keycode key) {
        if (key == 0) {
            LogWarn(LogCategory.System, "GetKeyName: Key is zero.");
            return string.Empty;
        }
        string name = SDL_GetKeyName((uint)key);
        if (string.IsNullOrEmpty(name)) {
            LogError(LogCategory.Error, "GetKeyName: Failed to retrieve key name.");
        }
        return name;
    }

    /// <summary>Convert one of the enumerated pixel formats to a bpp value and RGBA masks.</summary>
    /// <param name="format">one of the <see cref="PixelFormat"/> values.</param>
    /// <param name="bpp">a bits per pixel value; usually 15, 16, or 32.</param>
    /// <param name="rmask">a pointer filled in with the red mask for the format.</param>
    /// <param name="gmask">a pointer filled in with the green mask for the format.</param>
    /// <param name="bmask">a pointer filled in with the blue mask for the format.</param>
    /// <param name="amask">a pointer filled in with the alpha mask for the format.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetPixelFormatForMasks"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool GetMasksForPixelFormat(PixelFormat format, out int bpp, out uint rmask, out uint gmask,
            out uint bmask, out uint amask) {
        if (format == PixelFormat.Unknown) {
            LogWarn(LogCategory.System, "GetMasksForPixelFormat: Format is unknown.");
            bpp = 0;
            rmask = 0;
            gmask = 0;
            bmask = 0;
            amask = 0;
            return false;
        }
        bool result = SDL_GetMasksForPixelFormat(format, out bpp, out rmask, out gmask, out bmask, out amask);
        if (!result) {
            LogError(LogCategory.Error, "GetMasksForPixelFormat: Failed to retrieve masks for pixel format.");
        }
        return result;
    }

    /// <summary>Get the current key modifier state for the keyboard.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetKeyboardState"/>
    /// <seealso cref="SetModState"/>
    /// </remarks>
    /// <returns>Returns an OR'd combination of the modifier keys for the keyboard. See <see cref="KeyMod"/> for details.</returns>
    public static KeyMod GetModState() {
        return SDL_GetModState();
    }

    /// <summary>Get the orientation of a display when it is unrotated.</summary>
    /// <param name="displayId">the instance ID of the display to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetDisplays"/>
    /// </remarks>
    /// <returns>Returns the <see cref="DisplayOrientation"/> enum value of the display, or <see cref="DisplayOrientation.Unknown"/> if it isn't available.</returns>
    public static DisplayOrientation GetNaturalDisplayOrientation(uint displayId) {
        if (displayId == 0) {
            LogWarn(LogCategory.System, "GetNaturalDisplayOrientation: Display ID is zero.");
            return DisplayOrientation.Unknown;
        }
        DisplayOrientation orientation = SDL_GetNaturalDisplayOrientation(displayId);
        if (orientation == DisplayOrientation.Unknown) {
            LogError(LogCategory.Error, "GetNaturalDisplayOrientation: Failed to retrieve orientation.");
        }
        return orientation;
    }

    /// <summary>Get a number property from a group of properties.</summary>
    /// <param name="props">the properties to query.</param>
    /// <param name="name">the name of the property to query.</param>
    /// <param name="defaultValue">the default value of the property.</param>
    /// <remarks>
    /// You can use <see cref="GetPropertyType"/> to query whether the property exists and is a number property.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetPropertyType"/>
    /// <seealso cref="HasProperty"/>
    /// <seealso cref="SetNumberProperty"/>
    /// </remarks>
    /// <returns>Returns the value of the property, or <paramref name="defaultValue"/> if it is not set or not a number property.</returns>
    public static long GetNumberProperty(uint props, string name, long defaultValue) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            LogWarn(LogCategory.System, "GetNumberProperty: Properties handle is zero or name is null/empty.");
            return defaultValue;
        }
        long result = SDL_GetNumberProperty(props, name, defaultValue);
        if (result <= 0) {
            LogError(LogCategory.Error, "GetNumberProperty: Failed to retrieve number property.");
        }
        return result;
    }

    /// <summary>Get the number of video drivers compiled into SDL.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetVideoDriver"/>
    /// </remarks>
    /// <returns>Returns the number of built in video drivers.</returns>
    public static int GetNumVideoDrivers() {
        int numDrivers = SDL_GetNumVideoDrivers();
        if (numDrivers <= 0) {
            LogError(LogCategory.Error, "GetNumVideoDrivers: Failed to retrieve number of video drivers.");
        }
        return numDrivers;
    }

    /// <summary>Create an <see cref="PixelFormat"/>Details structure corresponding to a pixel format.</summary>
    /// <param name="format">one of the <see cref="PixelFormat"/> values.</param>
    /// <remarks>
    /// Returned structure may come from a shared global cache (i.e. not newly allocated),
    /// and hence should not be modified, especially the palette.
    /// Weird errors such as Blit combination not supported may occur.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(const <see cref="PixelFormat"/>Details *) Returns apointer to a <see cref="PixelFormat"/>Details structure or<see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint GetPixelFormatDetails(PixelFormat format) {
        if (format == PixelFormat.Unknown) {
            LogWarn(LogCategory.System, "GetPixelFormatDetails: Format is unknown.");
            return nint.Zero;
        }
        nint details = SDL_GetPixelFormatDetails(format);
        if (details == nint.Zero) {
            LogError(LogCategory.Error, "GetPixelFormatDetails: Failed to retrieve pixel format details.");
        }
        return details;
    }

    /// <summary>Convert a bpp value and RGBA masks to an enumerated pixel format.</summary>
    /// <param name="bpp">a bits per pixel value; usually 15, 16, or 32.</param>
    /// <param name="rmask">the red mask for the format.</param>
    /// <param name="gmask">the green mask for the format.</param>
    /// <param name="bmask">the blue mask for the format.</param>
    /// <param name="amask">the alpha mask for the format.</param>
    /// <remarks>
    /// This will return <see cref="PixelFormat.Unknown"/> if the conversion wasn't possible.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetMasksForPixelFormat"/>
    /// </remarks>
    /// <returns>Returns the<see cref="PixelFormat"/> value corresponding to the format masks, or <see cref="PixelFormat.Unknown"/> if there isn't a match.</returns>
    public static PixelFormat GetPixelFormatForMasks(int bpp, uint rmask, uint gmask, uint bmask, uint amask) {
        if (bpp <= 0 || rmask == 0 || gmask == 0 || bmask == 0) {
            LogWarn(LogCategory.System, "GetPixelFormatForMasks: Invalid parameters.");
            return PixelFormat.Unknown;
        }
        PixelFormat format = SDL_GetPixelFormatForMasks(bpp, rmask, gmask, bmask, amask);
        if (format == PixelFormat.Unknown) {
            LogError(LogCategory.Error, "GetPixelFormatForMasks: Failed to retrieve pixel format.");
        }
        return format;
    }

    /// <summary>Get the human readable name of a pixel format.</summary>
    /// <param name="format">the pixel format to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the human readable name of the specified pixel format or &quot;<see cref="PixelFormat.Unknown"/>&quot; if the format isn't recognized.</returns>
    public static string GetPixelFormatName(PixelFormat format) {
        if (format == PixelFormat.Unknown) {
            LogWarn(LogCategory.System, "GetPixelFormatName: Format is unknown.");
            return string.Empty;
        }
        string name = SDL_GetPixelFormatName(format);
        if (string.IsNullOrEmpty(name)) {
            LogError(LogCategory.Error, "GetPixelFormatName: Failed to retrieve pixel format name.");
        }
        return name;
    }

    /// <summary>Get a pointer property from a group of properties.</summary>
    /// <param name="props">the properties to query.</param>
    /// <param name="name">the name of the property to query.</param>
    /// <param name="defaultValue">the default value of the property.</param>
    /// <remarks>
    /// By convention, the names of properties that SDL exposes on objects will
    /// start with &quot;SDL.&quot;, and properties that SDL uses internally will start with
    /// &quot;SDL.internal.&quot;. These should be considered read-only and should not be
    /// modified by applications.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, although the data returned is not protected and could potentially be freed if you call <see cref="SetPointerProperty"/> or <see cref="ClearProperty"/> on these properties from another thread.</para>
    /// <para>If you need to avoid this, use <see cref="LockProperties"/> an <see cref="UnlockProperties"/>.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetBooleanProperty"/>
    /// <seealso cref="GetFloatProperty"/>
    /// <seealso cref="GetNumberProperty"/>
    /// <seealso cref="GetPropertyType"/>
    /// <seealso cref="GetStringProperty"/>
    /// <seealso cref="HasProperty"/>
    /// <seealso cref="SetPointerProperty"/>
    /// </remarks>
    /// <returns>(void *) Returns the value of the property, or <paramref name="defaultValue"/> if it is not set or not a pointer property.</returns>
    public static nint GetPointerProperty(uint props, string name, nint defaultValue) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            LogWarn(LogCategory.System, "GetPointerProperty: Properties handle is zero or name is null/empty.");
            return defaultValue;
        }
        nint result = SDL_GetPointerProperty(props, name, defaultValue);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "GetPointerProperty: Failed to retrieve pointer property.");
        }
        return result;
    }

    /// <summary>Get the current power supply details.</summary>
    /// <param name="seconds">a pointer filled in with the seconds of battery life left, or discarded to ignore. This will be filled in with -1 if we can't determine a value or there is no battery.</param>
    /// <param name="percent">a pointer filled in with the percentage of battery life left, between 0 and 100, or discarded to ignore. This will be filled in with -1 we can't determine a value or there is no battery.</param>
    /// <remarks>
    /// You should never take a battery status as absolute truth. Batteries
    /// (especially failing batteries) are delicate hardware, and the values
    /// reported here are best estimates based on what that hardware reports. It's
    /// not uncommon for older batteries to lose stored power much faster than it
    /// reports, or completely drain when reporting it has 20 percent left, etc.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the current battery state or <see cref="PowerState.Error"/> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static PowerState GetPowerInfo(out int seconds, out int percent) {
        PowerState state = SDL_GetPowerInfo(out seconds, out percent);
        if (state == PowerState.Unknown) {
            LogError(LogCategory.Error, "GetPowerInfo: Failed to retrieve power info.");
        }
        return state;
    }

    /// <summary>Report the user's preferred locale.</summary>
    /// <remarks>
    /// Returned language strings are in the format xx, where 'xx' is an ISO-639
    /// language specifier (such as &quot;en&quot; for English, &quot;de&quot; for German, etc).
    /// Country strings are in the format YY, where &quot;YY&quot; is an ISO-3166 country
    /// code (such as &quot;US&quot; for the United States, &quot;CA&quot; for Canada, etc). Country
    /// might be <see langword="null" /> if there's no specific guidance on them (so you might get {
    /// &quot;en&quot;, &quot;US&quot; } for American English, but { &quot;en&quot;, <see langword="null" /> } means &quot;English
    /// language, generically&quot;). Language strings are never <see langword="null" />, except to
    /// terminate the array.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(SDL_Locale **) Returns a <see langword="null" /> terminated array of locale pointers, or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information. This is a single allocation that should be freed withSDL_free() when it is no longer needed.</returns>
    public static Span<nint> GetPreferredLocales() {
        nint result = SDL_GetPreferredLocales(out int count);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "GetPreferredLocales: Failed to retrieve preferred locales.");
            return [];
        }

        if (count <= 0) {
            LogWarn(LogCategory.System, "GetPreferredLocales: Retrieved count is zero or negative.");
            return [];
        }

        nint[] data = new nint[count];
        Marshal.Copy(result, data, 0, count);

        return data;
    }

    /// <summary>Return the primary display.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetDisplays"/>
    /// </remarks>
    /// <returns>Returns the instance ID of the primary display on success or 0 on failure; call <see cref="GetError()" /> for more information.</returns>
    public static uint GetPrimaryDisplay() {
        uint displayId = SDL_GetPrimaryDisplay();
        if (displayId == 0) {
            LogError(LogCategory.Error, "GetPrimaryDisplay: Failed to retrieve primary display ID.");
        }
        return displayId;
    }

    /// <summary>Get UTF-8 text from the primary selection.</summary>
    /// <remarks>
    /// This functions returns an empty string if there was not enough memory left
    /// for a copy of the primary selection's content.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="HasPrimarySelectionText"/>
    /// <seealso cref="SetPrimarySelectionText"/>
    /// </remarks>
    /// <returns>(char *) Returns the primary selection text on success or an empty string on failure; call <see cref="GetError()" /> for more information. This should be freed with <see cref="Free"/> when it is no longer needed.</returns>
    public static string GetPrimarySelectionText() {
        string text = SDL_GetPrimarySelectionText();
        if (string.IsNullOrEmpty(text)) {
            LogError(LogCategory.Error, "GetPrimarySelectionText: Failed to retrieve primary selection text.");
        }
        return text;
    }

    /// <summary>Get the type of a property in a group of properties.</summary>
    /// <param name="props">the properties to query.</param>
    /// <param name="name">the name of the property to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="HasProperty"/>
    /// </remarks>
    /// <returns>Returns the type of the property, or <see cref="PropertyType.Invalid"/> if it is not set.</returns>
    public static PropertyType GetPropertyType(uint props, string name) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            LogWarn(LogCategory.System, "GetPropertyType: Properties handle is zero or name is null/empty.");
        }
        return SDL_GetPropertyType(props, name);
    }

    /// <summary>Calculate the intersection of a rectangle and line segment.</summary>
    /// <param name="rect">an <see cref="Rect"/> structure representing the rectangle to intersect.</param>
    /// <param name="x1">a pointer to the starting X-coordinate of the line.</param>
    /// <param name="y1">a pointer to the starting Y-coordinate of the line.</param>
    /// <param name="x2">a pointer to the ending X-coordinate of the line.</param>
    /// <param name="y2">a pointer to the ending Y-coordinate of the line.</param>
    /// <remarks>
    /// This function is used to clip a line segment to a rectangle. A line segment
    /// contained entirely within the rectangle or that does not intersect will
    /// remain unchanged. A line segment that crosses the rectangle at either or
    /// both ends will be clipped to the boundary of the rectangle and the new
    /// coordinates saved in <paramref name="x1"/>, <paramref name="y1"/>, <paramref name="x2"/>, and/or <paramref name="y2"/> as necessary.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if there is an intersection, <see langword="false" /> otherwise.</returns>

    public static bool GetRectAndLineIntersection(ref Rect rect, ref int x1, ref int y1, ref int x2, ref int y2) {
        bool result = SDL_GetRectAndLineIntersection(ref rect, ref x1, ref y1, ref x2, ref y2);
        if (!result) {
            LogError(LogCategory.Error, "GetRectAndLineIntersection: Failed to retrieve intersection.");
        }
        return result;
    }

    /// <summary>Calculate the intersection of a rectangle and line segment with float precision.</summary>
    /// <param name="rect">an <see cref="FRect"/> structure representing the rectangle to intersect.</param>
    /// <param name="x1">a pointer to the starting X-coordinate of the line.</param>
    /// <param name="y1">a pointer to the starting Y-coordinate of the line.</param>
    /// <param name="x2">a pointer to the ending X-coordinate of the line.</param>
    /// <param name="y2">a pointer to the ending Y-coordinate of the line.</param>
    /// <remarks>
    /// This function is used to clip a line segment to a rectangle. A line segment
    /// contained entirely within the rectangle or that does not intersect will
    /// remain unchanged. A line segment that crosses the rectangle at either or
    /// both ends will be clipped to the boundary of the rectangle and the new
    /// coordinates saved in <paramref name="x1"/>, <paramref name="y1"/>, <paramref name="x2"/>, and/or <paramref name="y2"/> as necessary.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if there is an intersection, <see langword="false" /> otherwise.</returns>
    public static bool GetRectAndLineIntersectionFloat(ref FRect rect, ref float x1, ref float y1, ref float x2,
            ref float y2) {
        bool result = SDL_GetRectAndLineIntersectionFloat(ref rect, ref x1, ref y1, ref x2, ref y2);
        if (!result) {
            LogError(LogCategory.Error, "GetRectAndLineIntersectionFloat: Failed to retrieve intersection.");
        }
        return result;
    }

    /// <summary>Calculate a minimal rectangle enclosing a set of points.</summary>
    /// <param name="points">an array of SDL_Point structures representing points to be enclosed.</param>
    /// <param name="count">the number of structures in the points array.</param>
    /// <param name="clip">a <see cref="Rect"/> used for clipping or <see langword="null" /> to enclose all points.</param>
    /// <param name="result">a <see cref="Rect"/> structure filled in with the minimal enclosing rectangle.</param>
    /// <remarks>
    /// If clip is not <see langword="null" /> then only points inside of the clipping rectangle are
    /// considered.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if any points were enclosed or <see langword="false" /> if all the points were outside of the clipping rectangle.</returns>
    public static bool GetRectEnclosingPoints(Span<Point> points, int count, ref Rect clip, out Rect result) {
        bool resultBool = SDL_GetRectEnclosingPoints(points, count, ref clip, out result);
        if (!resultBool) {
            LogError(LogCategory.Error, "GetRectEnclosingPoints: Failed to retrieve enclosing points.");
        }
        return resultBool;
    }

    /// <summary>Calculate a minimal rectangle enclosing a set of points with float precision.</summary>
    /// <param name="points">an array of <see cref="FPoint"/> structures representing points to be enclosed.</param>
    /// <param name="count">the number of structures in the points array.</param>
    /// <param name="clip">an <see cref="FRect"/> used for clipping or <see langword="null" /> to enclose all points.</param>
    /// <param name="result">an <see cref="FRect"/> structure filled in with the minimal enclosing rectangle.</param>
    /// <remarks>
    /// If clip is not <see langword="null" /> then only points inside of the clipping rectangle are
    /// considered.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if any points were enclosed or <see langword="false" /> if all the points were outside of the clipping rectangle.</returns>
    public static bool GetRectEnclosingPointsFloat(Span<FPoint> points, int count, ref FRect clip, out FRect result) {
        bool resultBool = SDL_GetRectEnclosingPointsFloat(points, count, ref clip, out result);
        if (!resultBool) {
            LogError(LogCategory.Error, "GetRectEnclosingPointsFloat: Failed to retrieve enclosing points.");
        }
        return resultBool;
    }

    /// <summary>Calculate the intersection of two rectangles.</summary>
    /// <param name="a">a <see cref="Rect"/> structure representing the first rectangle.</param>
    /// <param name="b">a <see cref="Rect"/> structure representing the second rectangle.</param>
    /// <param name="result">a <see cref="Rect"/> structure filled in with the intersection of rectangles <paramref name="a"/> and <paramref name="b"/>.</param>
    /// <remarks>
    /// If result is <see langword="null" /> then this function will return <see langword="false"/>.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="HasRectIntersection"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if there is an intersection, <see langword="false" /> otherwise.</returns>
    public static bool GetRectIntersection(ref Rect a, ref Rect b, out Rect result) {
        bool resultBool = SDL_GetRectIntersection(ref a, ref b, out result);
        if (!resultBool) {
            LogError(LogCategory.Error, "GetRectIntersection: Failed to retrieve intersection.");
        }
        return resultBool;
    }

    /// <summary>Calculate the intersection of two rectangles with float precision.</summary>
    /// <param name="a">an <see cref="FRect"/> structure representing the first rectangle.</param>
    /// <param name="b">an <see cref="FRect"/> structure representing the second rectangle.</param>
    /// <param name="result">an <see cref="FRect"/> structure filled in with the intersection of rectangles <paramref name="a"/> and <paramref name="b".</param>
    /// <remarks>
    /// If result is <see langword="null" /> then this function will return <see langword="false"/>.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="HasRectIntersectionFloat"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if there is an intersection, <see langword="false" /> otherwise.</returns>
    public static bool GetRectIntersectionFloat(ref FRect a, ref FRect b, out FRect result) {
        bool resultBool = SDL_GetRectIntersectionFloat(ref a, ref b, out result);
        if (!resultBool) {
            LogError(LogCategory.Error, "GetRectIntersectionFloat: Failed to retrieve intersection.");
        }
        return resultBool;
    }

    /// <summary>Calculate the union of two rectangles.</summary>
    /// <param name="a">an <see cref="Rect"/> structure representing the first rectangle.</param>
    /// <param name="b">an <see cref="Rect"/> structure representing the second rectangle.</param>
    /// <param name="result">a <see cref="Rect"/> structure filled in with the union of rectangles <paramref name="a"/> and <paramref name="b"/>.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool GetRectUnion(ref Rect a, ref Rect b, out Rect result) {
        bool resultBool = SDL_GetRectUnion(ref a, ref b, out result);
        if (!resultBool) {
            LogError(LogCategory.Error, "GetRectUnion: Failed to retrieve union.");
        }
        return resultBool;
    }

    /// <summary>Calculate the union of two rectangles with float precision.</summary>
    /// <param name="a">an <see cref="FRect"/> structure representing the first rectangle.</param>
    /// <param name="b">an <see cref="FRect"/> structure representing the second rectangle.</param>
    /// <param name="result">an <see cref="FRect"/> structure filled in with the union of rectangles A and B.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool GetRectUnionFloat(ref FRect a, ref FRect b, out FRect result) {
        bool resultBool = SDL_GetRectUnionFloat(ref a, ref b, out result);
        if (!resultBool) {
            LogError(LogCategory.Error, "GetRectUnionFloat: Failed to retrieve union.");
        }
        return resultBool;
    }

    /// <summary>Get RGB values from a pixel in the specified format.</summary>
    /// <param name="pixel">a pixel value.</param>
    /// <param name="format">a pointer to <see cref="PixelFormat"/>Details describing the pixel format.</param>
    /// <param name="palette">an optional palette for indexed formats, may be discarded.</param>
    /// <remarks>
    /// This function uses the entire 8-bit [0..255] range when converting color
    /// components from pixel formats with less than 8-bits per RGB component
    /// (e.g., a completely white pixel in 16-bit RGB565 format would return [0xff,
    /// 0xff, 0xff] not [0xf8, 0xfc, 0xf8]).
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, as long as the palette is not modified.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetPixelFormatDetails"/>
    /// <seealso cref="GetRgba"/>
    /// <seealso cref="MapRgb"/>
    /// <seealso cref="MapRgba"/>
    /// </remarks>
    public static Color GetRgb(uint pixel, nint format, nint palette) {
        if (format == nint.Zero) {
            LogError(LogCategory.Error, "GetRGB: Format pointer is null.");
            throw new ArgumentNullException(nameof(format), "Format pointer cannot be null.");
        }

        if (palette == nint.Zero) {
            LogWarn(LogCategory.System, "GetRGB: Palette pointer is null. Defaulting to no palette.");
        }

        SDL_GetRGB(pixel, format, palette, out byte r, out byte g, out byte b);
        return new Color() { R = r, G = g, B = b, A = 255 };
    }

    /// <summary>Get RGBA values from a pixel in the specified format.</summary>
    /// <param name="pixel">a pixel value.</param>
    /// <param name="format">a pointer to <see cref="PixelFormat"/>Details describing the pixel format.</param>
    /// <param name="palette">an optional palette for indexed formats, may be discarded.</param>
    /// <remarks>
    /// This function uses the entire 8-bit [0..255] range when converting color
    /// components from pixel formats with less than 8-bits per RGB component
    /// (e.g., a completely white pixel in 16-bit RGB565 format would return [0xff,
    /// 0xff, 0xff] not [0xf8, 0xfc, 0xf8]).
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, as long as the palette is not modified.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetPixelFormatDetails"/>
    /// <seealso cref="GetRgb"/>
    /// <seealso cref="MapRgb"/>
    /// <seealso cref="MapRgba"/>
    /// </remarks>
    public static Color GetRgba(uint pixel, nint format, nint palette) {
        if (format == nint.Zero) {
            LogError(LogCategory.Error, "GetRGBA: Format pointer is null.");
            throw new ArgumentNullException(nameof(format), "Format pointer cannot be null.");
        }
        if (palette == nint.Zero) {
            LogWarn(LogCategory.System, "GetRGBA: Palette pointer is null. Defaulting to no palette.");
        }
        SDL_GetRGBA(pixel, format, palette, out byte r, out byte g, out byte b, out byte a);
        return new Color() { R = r, G = g, B = b, A = a };
    }

    /// <summary>Get the scancode corresponding to the given key code according to the current keyboard layout.</summary>
    /// <param name="key">the desired SDL_Keycode to query.</param>
    /// <param name="modstate">a pointer to the modifier state that would be used when the scancode generates this key, can be <see cref="nint.Zero"/>.</param>
    /// <remarks>
    /// Note that there may be multiple scancode+modifier states that can generate
    /// this keycode, this will just return the first one found.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetKeyFromScancode"/>
    /// <seealso cref="GetScancodeName"/>
    /// </remarks>
    /// <returns>Returns the <see cref="Scancode"/> that corresponds to the given <see cref="Keycode"/>.</returns>
    public static Scancode GetScancodeFromKey(uint key, nint modstate) {
        if (key == 0) {
            LogWarn(LogCategory.System, "GetScancodeFromKey: Key is zero.");
            return Scancode.Unknown;
        }
        Scancode scanCode = SDL_GetScancodeFromKey(key, modstate);
        if (scanCode == Scancode.Unknown) {
            LogError(LogCategory.Error, "GetScancodeFromKey: Failed to retrieve scan code from key.");
        }
        return scanCode;
    }

    /// <summary>Get a scancode from a human-readable name.</summary>
    /// <param name="name">the human-readable scancode name.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetKeyFromName"/>
    /// <seealso cref="GetScancodeFromKey"/>
    /// <seealso cref="GetScancodeName"/>
    /// </remarks>
    /// <returns>Returns the <see cref="Scancode"/>, or <see cref="Scancode.Unknown"/> if the name wasn't recognized; call <see cref="GetError()" /> for more information.</returns>
    public static Scancode GetScancodeFromName(string name) {
        if (string.IsNullOrEmpty(name)) {
            LogWarn(LogCategory.System, "GetScancodeFromName: Name is null or empty.");
            return Scancode.Unknown;
        }
        Scancode scanCode = SDL_GetScancodeFromName(name);
        if (scanCode == Scancode.Unknown) {
            LogError(LogCategory.Error, "GetScancodeFromName: Failed to retrieve scan code from name.");
        }
        return scanCode;
    }

    /// <summary>Get a human-readable name for a scancode.</summary>
    /// <param name="scanCode">the desired SDL_Scancode to query.</param>
    /// <remarks>
    /// Warning: The returned name is by design not stable across platforms,
    /// e.g. the name for <see cref="Scancode.LGui"/> is &quot;Left GUI&quot;
    /// under Linux but &quot;Left Windows&quot; under Microsoft Windows, and some scancodes
    /// like <see cref="Scancode.NonUsBackslash"/> don't
    /// have any name at all. There are even scancodes that share names, e.g.
    /// <see cref="Scancode.Return"/> and
    /// <see cref="Scancode.Return2"/> (both called &quot;Return&quot;). This
    /// function is therefore unsuitable for creating a stable cross-platform
    /// two-way mapping between strings and scancodes.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetScancodeFromKey"/>
    /// <seealso cref="GetScancodeFromName"/>
    /// <seealso cref="SetScancodeName"/>
    /// </remarks>
    /// <returns>Returns a pointer to the name for the scancode. If the scancode doesn't have a name this function returns an empty string (&quot;&quot;).</returns>
    public static string GetScancodeName(Scancode scanCode) {
        if (scanCode == Scancode.Unknown) {
            LogWarn(LogCategory.System, "GetScancodeName: Scan code is unknown.");
            return string.Empty;
        }
        string name = SDL_GetScancodeName(scanCode);
        if (string.IsNullOrEmpty(name)) {
            LogError(LogCategory.Error, "GetScancodeName: Failed to retrieve scan code name.");
        }
        return name;
    }

    /// <summary>Get a string property from a group of properties.</summary>
    /// <param name="props">the properties to query.</param>
    /// <param name="name">the name of the property to query.</param>
    /// <param name="defaultValue">the default value of the property.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, although the data returned is not protected and could potentially be freed if you call <see cref="SetStringProperty"/> or <see cref="ClearProperty"/> on these properties from another thread.</para>
    /// <para>If you need to avoid this, use <see cref="LockProperties"/> and <see cref="UnlockProperties"/>.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetPropertyType"/>
    /// <seealso cref="HasProperty"/>
    /// <seealso cref="SetStringProperty"/>
    /// </remarks>
    /// <returns>Returns the value of the property, or <paramref name="defaultValue"/> if itis not set or not a string property.</returns>
    public static string GetStringProperty(uint props, string name, string defaultValue) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            LogWarn(LogCategory.System, "GetStringProperty: Properties is zero or name is null/empty.");
            return defaultValue;
        }
        string result = SDL_GetStringProperty(props, name, defaultValue);
        if (string.IsNullOrEmpty(result)) {
            LogError(LogCategory.Error, "GetStringProperty: Failed to retrieve string property.");
        }
        return result;
    }

    /// <summary>Get the additional alpha value used in blit operations.</summary>
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <param name="alpha">a pointer filled in with the current alpha value.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetSurfaceColorMod"/>
    /// <seealso cref="SetSurfaceAlphaMod"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
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
    /// <param name="surface">the <see cref="Surface"/> to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetSurfacePalette"/>
    /// </remarks>
    /// <returns>(SDL_Palette *) Returns a pointer to the palette used by the surface, or <see langword="null"/> if there is no palette used.</returns>
    public static nint GetSurfacePalette(nint surface) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "GetSurfacePalette: Surface pointer is null.");
            return nint.Zero;
        }
        nint palette = SDL_GetSurfacePalette(surface);
        if (palette == nint.Zero) {
            LogError(LogCategory.Error, "GetSurfacePalette: Failed to retrieve surface palette.");
        }

        return palette;
    }

    /// <summary>Get the blend mode used for blit operations.</summary>
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <param name="blendMode">a pointer filled in with the current <see cref="BlendMode"/>.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetSurfaceBlendMode"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
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
    /// <param name="surface">the <see cref="Surface"/> structure representing the surface to be clipped.</param>
    /// <param name="rect">a <see cref="Rect"/> structure filled in with the clipping rectangle for the surface.</param>
    /// <remarks>
    /// When surface is the destination of a blit, only the area within the clip rectangle is drawn into.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetSurfaceClipRect"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
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
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <param name="key">a pointer filled in with the transparent pixel.</param>
    /// <remarks>
    /// The color key is a pixel of the format used by the surface, as generated by <see cref="MapRgb"/>.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetSurfaceColorKey"/>
    /// <seealso cref="SurfaceHasColorKey"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
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
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <param name="r">a pointer filled in with the current red color value.</param>
    /// <param name="g">a pointer filled in with the current green color value.</param>
    /// <param name="b">a pointer filled in with the current blue color value.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetSurfaceAlphaMod"/>
    /// <seealso cref="SetSurfaceColorMod"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
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
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <param name="count">a pointer filled in with the number of surface pointers returned, may be discarded.</param>
    /// <remarks>
    /// This returns all versions of a surface, with the surface being queried as
    /// the first element in the returned array.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="AddSurfaceAlternateImage"/>
    /// <seealso cref="RemoveSurfaceAlternateImages"/>
    /// <seealso cref="SurfaceHasAlternateImages"/>
    /// </remarks>
    /// <returns>(SDL_Surface **) Returns a <see langword="null" /> terminated array ofSDL_Surface pointers or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information. This should be freedwith <see cref="Free"/> when it is no longer needed.</returns>
    public static Span<nint> GetSurfaceImages(nint surface, out int count) {
        nint result = SDL_GetSurfaceImages(surface, out count);
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

        for (int i = 0; i < count; i++) {
            if (images[i] == nint.Zero) {
                LogError(LogCategory.Error, $"GetSurfaceImages: Image at index {i} is null.");
                return [];
            }
        }

        return images.ToArray();
    }

    /// <summary>Get the properties associated with a surface.</summary>
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <remarks>
    /// The following properties are understood by SDL:
    /// <list type="bullet">
    /// <item>SDL_PROP_SURFACE_SDR_WHITE_POINT_FLOAT: for HDR10 and floating point surfaces, this defines the value of 100% diffuse white, with higher values being displayed in the High Dynamic Range headroom.This defaults to 203 for HDR10 surfaces and 1.0 for floating point surfaces.</item>
    /// <item>SDL_PROP_SURFACE_HDR_HEADROOM_FLOAT: for HDR10 and floating point surfaces, this defines the maximum dynamic range used by the content, in terms of the SDR white point.This defaults to 0.0, which disables tone mapping.</item>
    /// <item>SDL_PROP_SURFACE_TONEMAP_OPERATOR_STRING: the tone mapping operator used when compressing from a surface with high dynamic range to another with lower dynamic range. Currently this supports "chrome", which uses the same tone mapping that Chrome uses for HDR content, the form "*=N", where N is a floating point scale factor applied in linear space, and "none", which disables tone mapping. This defaults to "chrome".</item>
    /// <item>SDL_PROP_SURFACE_HOTSPOT_X_NUMBER: the hotspot pixel offset from the left edge of the image, if this surface is being used as a cursor.</item>
    /// <item>SDL_PROP_SURFACE_HOTSPOT_Y_NUMBER: the hotspot pixel offset from the top edge of the image, if this surface is being used as a cursor.</item>
    /// </list>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns a valid property ID on success or 0 on failure; call <see cref="GetError()" /> for more information.</returns>
    public static uint GetSurfaceProperties(nint surface) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "GetSurfaceProperties: Surface pointer is null.");
            return 0;
        }
        uint properties = SDL_GetSurfaceProperties(surface);
        if (properties == 0) {
            LogError(LogCategory.Error, "GetSurfaceProperties: Failed to retrieve surface properties.");
        }
        return properties;
    }

    /// <summary>Get the current system theme.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the current system theme,light, dark, or unknown.</returns>
    public static SystemTheme GetSystemTheme() {
        SystemTheme theme = SDL_GetSystemTheme();
        if (theme == SystemTheme.Unknown) {
            LogError(LogCategory.Error, "GetSystemTheme: Failed to retrieve system theme.");
        }
        return theme;
    }

    /// <summary>Get the area used to type Unicode text input.</summary>
    /// <param name="window">the window for which to query the text input area.</param>
    /// <param name="rect">a pointer to an <see cref="Rect"/> filled in with the text input area, can bediscarded.</param>
    /// <param name="cursor">a pointer to the offset of the current cursor location relative to rect-&gt;x, may be discarded.</param>
    /// <remarks>
    /// This returns the values previously set by <see cref="SetTextInputArea"/>.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetTextInputArea"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool GetTextInputArea(nint window, out Rect rect, out int cursor) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetTextInputArea: Window pointer is null.");
            rect = default;
            cursor = 0;
            return false;
        }
        bool result = SDL_GetTextInputArea(window, out rect, out cursor);
        if (!result) {
            LogError(LogCategory.Error, "GetTextInputArea: Failed to retrieve text input area.");
        }
        return result;
    }

    /// <summary>Get the thread identifier for the specified thread.</summary>
    /// <param name="thread">the thread to query.</param>
    /// <remarks>
    /// This thread identifier is as reported by the underlying operating system.
    /// If SDL is running on a platform that does not support threads the return
    /// value will always be zero.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetCurrentThreadId"/>
    /// </remarks>
    /// <returns>Returns the ID of the specified thread, orthe ID of the current thread if thread is <see langword="null" />.</returns>
    public static ulong GetThreadId(nint thread) {
        if (thread == nint.Zero) {
            LogError(LogCategory.Error, "GetThreadId: Thread pointer is null.");
            return 0;
        }
        ulong threadId = SDL_GetThreadID(thread);
        if (threadId == 0) {
            LogError(LogCategory.Error, "GetThreadId: Failed to retrieve thread ID.");
        }
        return threadId;
    }

    /// <summary>Get the thread name as it was specified in <see cref="CreateThread"/>.</summary>
    /// <param name="thread">the thread to query.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns a pointer to a UTF-8 string that names the specifiedthread, or <see langword="null" /> if it doesn't have a name.</returns>
    public static string GetThreadName(nint thread) {
        if (thread == nint.Zero) {
            LogError(LogCategory.Error, "GetThreadName: Thread pointer is null.");
            return string.Empty;
        }
        string name = SDL_GetThreadName(thread);
        if (string.IsNullOrEmpty(name)) {
            LogError(LogCategory.Error, "GetThreadName: Failed to retrieve thread name.");
        }
        return name;
    }

    /// <summary>Get the current state of a thread.</summary>
    /// <param name="thread">the thread to query.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ThreadState"/>
    /// </remarks>
    /// <returns>Returns the current state of a thread,  SDL_THREAD_UNKNOWN if the thread isn't valid.</returns>
    public static SharpSDL3.Enums.ThreadState GetThreadState(nint thread) {
        if (thread == nint.Zero) {
            LogError(LogCategory.Error, "GetThreadState: Thread pointer is null.");
            return ThreadState.Unknown;
        }
        ThreadState state = SDL_GetThreadState(thread);
        if (state == ThreadState.Unknown) {
            LogError(LogCategory.Error, "GetThreadState: Failed to retrieve thread state.");
        }
        return state;
    }

    /// <summary>Get the current thread's value associated with a thread local storage ID.</summary>
    /// <param name="id">a pointer to the thread local storage ID, may not be <see langword="null" />.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetTls"/>
    /// </remarks>
    /// <returns>(void *) Returns the value associated with the ID for the current thread or<see langword="null" /> if no value has been set; call <see cref="GetError()" /> for more information.</returns>
    public static nint GetTls(nint id) {
        if (id == nint.Zero) {
            LogError(LogCategory.Error, "GetTLS: ID is zero.");
            return nint.Zero;
        }
        nint tls = SDL_GetTLS(id);
        if (tls == nint.Zero) {
            LogError(LogCategory.Error, "GetTLS: Failed to retrieve TLS value.");
        }
        return tls;
    }

    /// <summary>Get the name of a built in video driver.</summary>
    /// <param name="index">the index of a video driver.</param>
    /// <remarks>
    /// The video drivers are presented in the order in which they are normally checked during initialization.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetNumVideoDrivers"/>
    /// </remarks>
    /// <returns>Returns the name of the video driver with the givenindex.</returns>
    public static string GetVideoDriver(int index) {
        if (index < 0) {
            LogError(LogCategory.Error, "GetVideoDriver: Index is negative.");
            return string.Empty;
        }
        string driver = SDL_GetVideoDriver(index);
        if (string.IsNullOrEmpty(driver)) {
            LogError(LogCategory.Error, "GetVideoDriver: Failed to retrieve video driver.");
        }
        return driver;
    }

    /// <summary>Get the size of a window's client area.</summary>
    /// <param name="window">the window to query the width and height from.</param>
    /// <param name="minAspect">a pointer filled in with the minimum aspect ratio of the window, may be discarded.</param>
    /// <param name="maxAspect">a pointer filled in with the maximum aspect ratio of the window, may be discarded.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetWindowAspectRatio"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool GetWindowAspectRatio(nint window, out float minAspect, out float maxAspect) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowAspectRatio: Window pointer is null.");
            minAspect = maxAspect = 0;
            return false;
        }
        bool result = SDL_GetWindowAspectRatio(window, out minAspect, out maxAspect);
        if (!result) {
            LogError(LogCategory.Error, "GetWindowAspectRatio: Failed to retrieve window aspect ratio.");
        }
        return result;
    }

    /// <summary>
    /// Get the size of a window's borders (decorations) around the client area.
    /// </summary>
    /// <param name="window">the window to query the size values of the border (decorations) from.</param>
    /// <param name="top">out to variable for storing the size of the top border; discard is permitted.</param>
    /// <param name="left">out to variable for storing the size of the left border; discard is permitted.</param>
    /// <param name="bottom">out to variable for storing the size of the bottom border; discard is permitted.</param>
    /// <param name="right">out to variable for storing the size of the right border; discard is permitted.</param>
    /// <remarks>
    /// <para>>Note: If this function fails (returns <see langword="false"/>), the size values will be initialized to 0, 0, 0, 0 (if a non-NULL pointer is provided), as if the window in question was borderless.</para>
    /// <para>Note: This function may fail on systems where the window has not yet been decorated by the display server(for example, immediately after calling <see cref="CreateWindow"/>).</para>
    /// <para>It is recommended that you wait at least until the window has been presented and composited, so that the window system has a chance to decorate the window and provide the border dimensions to SDL.</para>
    /// <para>This function also returns <see langword="false"/> if getting the information is not supported.</para>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowSize"/>
    /// </remarks>
    /// <returns>Returns <see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="GetError"/> for more information.</returns>
    public static bool GetWindowBordersSize(nint window, out int top, out int left, out int bottom, out int right) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowBordersSize: Window pointer is null.");
            top = left = bottom = right = 0;
            return false;
        }
        bool result = SDL_GetWindowBordersSize(window, out top, out left, out bottom, out right);
        if (!result) {
            LogError(LogCategory.Error, "GetWindowBordersSize: Failed to retrieve window border size.");
        }
        return result;
    }

    /// <summary>
    /// Get the size of a window's borders (decorations) around the client area.
    /// </summary>
    /// <param name="window">the window to query the size values of the border (decorations) from.</param>
    /// <remarks>
    /// <para>>Note: If this function fails (returns <see langword="false"/>), the size values will be initialized to 0, 0, 0, 0 (if a non-NULL pointer is provided), as if the window in question was borderless.</para>
    /// <para>Note: This function may fail on systems where the window has not yet been decorated by the display server(for example, immediately after calling <see cref="CreateWindow"/>).</para>
    /// <para>It is recommended that you wait at least until the window has been presented and composited, so that the window system has a chance to decorate the window and provide the border dimensions to SDL.</para>
    /// <para>This function also returns a blank <see cref="Rect"/> if getting the information is not supported.</para>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowSize"/>
    /// </remarks>
    /// <returns>Returns a <see cref="Rect"/> on success, or a blank <see cref="Rect"/> on failure; call <see cref="GetError"/> for more information.</returns>
    public static Rect GetWindowBordersSize(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowBorderSize: Window pointer is null.");
            return default;
        }
        bool result = SDL_GetWindowBordersSize(window, out int top, out int left, out int bottom, out int right);
        if (!result) {
            LogError(LogCategory.Error, "GetWindowBorderSize: Failed to retrieve window border size.");
        }
        return new Rect() { X = left, Y = top, W = right - left, H = bottom - top };
    }

    /// <summary>Get the content display scale relative to a window's pixel size.</summary>
    /// <param name="window">the window to query.</param>
    /// <remarks>
    /// This is a combination of the window pixel density and the display content
    /// scale, and is the expected scale for displaying content in this window. For
    /// example, if a 3840x2160 window had a display scale of 2.0, the user expects
    /// the content to take twice as many pixels and be the same physical size as
    /// if it were being displayed in a 1920x1080 window with a display scale of
    /// 1.0.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the display scale, or 0.0f on failure; call <see cref="GetError()"/> for more information.</returns>
    public static float GetWindowDisplayScale(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowDisplayScale: Window pointer is null.");
            return 0;
        }
        float scale = SDL_GetWindowDisplayScale(window);
        if (scale <= 0) {
            LogError(LogCategory.Error, "GetWindowDisplayScale: Failed to retrieve window display scale.");
        }
        return scale;
    }

    /// <summary>Get the window flags.</summary>
    /// <param name="window">the window to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateWindow"/>
    /// <seealso cref="HideWindow"/>
    /// <seealso cref="MaximizeWindow"/>
    /// <seealso cref="MinimizeWindow"/>
    /// <seealso cref="SetWindowFullscreen"/>
    /// <seealso cref="SetWindowMouseGrab"/>
    /// <seealso cref="ShowWindow"/>
    /// </remarks>
    /// <returns>Returns a mask of the<see cref="WindowFlags"/> associated with window.</returns>
    public static WindowFlags GetWindowFlags(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowFlags: Window handle is null.");
            return 0;
        }
        WindowFlags flags = SDL_GetWindowFlags(window);
        if (flags == 0) {
            LogWarn(LogCategory.System, "GetWindowFlags: Failed to retrieve window flags.");
        }
        return flags;
    }

    /// <summary>Get a window from a stored ID.</summary>
    /// <param name="id">the ID of the window.</param>
    /// <remarks>
    /// The numeric ID is what <see cref="WindowEvent"/> references, and
    /// is necessary to map these events to specific SDL_Window
    /// objects.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowId"/>
    /// </remarks>
    /// <returns>(SDL_Window *) Returns the window associated with id or<see langword="null" /> if it doesn't exist; call <see cref="GetError()" /> for more information.</returns>
    public static nint GetWindowFromId(uint id) {
        if (id == 0) {
            LogError(LogCategory.Error, "GetWindowFromId: Window ID is zero.");
            return nint.Zero;
        }
        nint windowHandle = SDL_GetWindowFromID(id);
        if (windowHandle == nint.Zero) {
            LogWarn(LogCategory.System, "GetWindowFromId: Failed to retrieve window handle.");
        }
        return windowHandle;
    }

    /// <summary>Query the display mode to use when a window is visible at fullscreen.</summary>
    /// <param name="window">the window to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetWindowFullscreenMode"/>
    /// <seealso cref="SetWindowFullscreen"/>
    /// </remarks>
    /// <returns>(const SDL_DisplayMode *) Returns a pointer to the exclusive fullscreen mode to use or <see langword="null" /> for borderless fullscreen desktopmode.</returns>
    public static nint GetWindowFullscreenMode(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowFullscreenMode: Window pointer is null.");
            return nint.Zero;
        }
        nint mode = SDL_GetWindowFullscreenMode(window);
        if (mode == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowFullscreenMode: Failed to retrieve window fullscreen mode.");
        }
        return mode;
    }

    /// <summary>Query the display mode to use when a window is visible at fullscreen.</summary>
    /// <param name="window">the window to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetWindowFullscreenMode"/>
    /// <seealso cref="SetWindowFullscreen"/>
    /// </remarks>
    /// <returns>(const SDL_DisplayMode *) Returns a pointer to the exclusive fullscreen mode to use or <see langword="null" /> for borderless fullscreen desktop mode.</returns>
    public static unsafe DisplayMode GetWindowFullScreenMode(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowFullScreenMode: Window pointer is null.");
            return default;
        }
        DisplayMode mode = *(DisplayMode*)SDL_GetWindowFullscreenMode(window);
        if (mode.DisplayId == 0) {
            LogError(LogCategory.Error, "GetWindowFullScreenMode: Failed to retrieve window fullscreen mode.");
        }
        return mode;
    }

    /// <summary>Get the raw ICC profile data for the screen the window is currently on.</summary>
    /// <param name="window">the window to query.</param>
    /// <param name="size">the size of the ICC profile.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(void *) Returns the raw ICC profile data on success or <see langword="null" /> on failure;call <see cref="GetError()" /> for more information. This should befreed with <see cref="Free"/> when it is no longer needed.</returns>
    public static nint GetWindowIccProfile(nint window, out nuint size) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowICCProfile: Window pointer is null.");
            size = 0;
            return nint.Zero;
        }
        nint profile = SDL_GetWindowICCProfile(window, out size);
        if (profile == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowICCProfile: Failed to retrieve window ICC profile.");
        }
        return profile;
    }

    /// <summary>Get the numeric ID of a window.</summary>
    /// <param name="window">the window to query.</param>
    /// <remarks>
    /// The numeric ID is what <see cref="WindowEvent"/> references, and
    /// is necessary to map these events to specific SDL_Window
    /// objects.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowFromId"/>
    /// </remarks>
    /// <returns>Returns the ID of the window on success or 0 on failure; call <see cref="GetError()" /> for more information.</returns>
    public static uint GetWindowId(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowId: Window handle is null.");
            return 0;
        }

        uint windowId = SDL_GetWindowID(window);
        if (windowId == 0) {
            LogWarn(LogCategory.System, "GetWindowId: Failed to retrieve window ID.");
        }

        return windowId;
    }

    /// <summary>Get a window's keyboard grab mode.</summary>
    /// <param name="window">the window to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetWindowKeyboardGrab"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if keyboard is grabbed, and <see langword="false" /> otherwise.</returns>
    public static bool GetWindowKeyboardGrab(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowKeyboardGrab: Window pointer is null.");
            return false;
        }
        bool result = SDL_GetWindowKeyboardGrab(window);
        if (!result) {
            LogError(LogCategory.Error, "GetWindowKeyboardGrab: Failed to retrieve window keyboard grab.");
        }
        return result;
    }

    /// <summary>Get the maximum size of a window's client area.</summary>
    /// <param name="window">the window to query.</param>
    /// <param name="w">a pointer filled in with the maximum width of the window, may be discarded.</param>
    /// <param name="h">a pointer filled in with the maximum height of the window, may be discarded.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowMinimumSize"/>
    /// <seealso cref="SetWindowMaximumSize"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool GetWindowMaximumSize(nint window, out int w, out int h) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowMaximumSize: Window pointer is null.");
            w = h = 0;
            return false;
        }
        bool result = SDL_GetWindowMaximumSize(window, out w, out h);
        if (!result) {
            LogError(LogCategory.Error, "GetWindowMaximumSize: Failed to retrieve window maximum size.");
        }
        return result;
    }

    /// <summary>Get the maximum size of a window's client area.</summary>
    /// <param name="window">the window to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowMinimumSize"/>
    /// <seealso cref="SetWindowMaximumSize"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static Rect GetWindowMaximumSize(nint window) {
        if (window == nint.Zero) {
            return default;
        }

        bool result = SDL_GetWindowMaximumSize(window, out int w, out int h);
        if (!result) {
            LogError(LogCategory.Error, "GetWindowMaximumSize: Failed to retrieve window maximum size.");
            return default;
        }
        return new Rect() { W = w, H = h };
    }

    /// <summary>Get the minimum size of a window's client area.</summary>
    /// <param name="window">the window to query.</param>
    /// <param name="w">a pointer filled in with the minimum width of the window, may be discarded.</param>
    /// <param name="h">a pointer filled in with the minimum height of the window, may be discarded.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowMaximumSize"/>
    /// <seealso cref="SetWindowMinimumSize"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool GetWindowMinimumSize(nint window, out int w, out int h) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowMinimumSize: Window pointer is null.");
            w = h = 0;
            return false;
        }
        bool result = SDL_GetWindowMinimumSize(window, out w, out h);
        if (!result) {
            LogError(LogCategory.Error, "GetWindowMinimumSize: Failed to retrieve window minimum size.");
        }
        return result;
    }

    /// <summary>Get the minimum size of a window's client area.</summary>
    /// <param name="window">the window to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowMaximumSize"/>
    /// <seealso cref="SetWindowMinimumSize"/>
    /// </remarks>
    /// <returns>Returns a <see cref="Rect"/> on success or a blank <see cref="Rect"/> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static Rect GetWindowMinimumSize(nint window) {
        if (window == nint.Zero) {
            return default;
        }
        bool result = SDL_GetWindowMinimumSize(window, out int w, out int h);
        if (!result) {
            LogError(LogCategory.Error, "GetWindowMinimumSize: Failed to retrieve window minimum size.");
            return default;
        }
        return new Rect() { W = w, H = h };
    }

    /// <summary>Get a window's mouse grab mode.</summary>
    /// <param name="window">the window to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowMouseRect"/>
    /// <seealso cref="SetWindowMouseRect"/>
    /// <seealso cref="SetWindowMouseGrab"/>
    /// <seealso cref="SetWindowKeyboardGrab"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if mouse is grabbed, and <see langword="false" /> otherwise.</returns>
    public static bool GetWindowMouseGrab(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowMouseGrab: Window pointer is null.");
            return false;
        }
        bool result = SDL_GetWindowMouseGrab(window);
        if (!result) {
            LogError(LogCategory.Error, "GetWindowMouseGrab: Failed to retrieve window mouse grab.");
        }
        return result;
    }

    /// <summary>
    /// Get the mouse confinement rectangle of a window.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetWindowMouseRect"/>
    /// <seealso cref="GetWindowMouseGrab"/>
    /// <seealso cref="SetWindowMouseGrab"/>
    /// </remarks>
    /// <returns></returns>
    public static nint GetWindowMouseRectPtr(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowMouseRect: Window pointer is null.");
            return nint.Zero;
        }
        nint rect = SDL_GetWindowMouseRect(window);
        if (rect == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowMouseRect: Failed to retrieve window mouse rect.");
        }
        return rect;
    }

    /// <summary>Get the mouse confinement rectangle of a window.</summary>
    /// <param name="window">the window to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetWindowMouseRect"/>
    /// <seealso cref="GetWindowMouseGrab"/>
    /// <seealso cref="SetWindowMouseGrab"/>
    /// </remarks>
    /// <returns>(const <see cref="Rect"/> *) Returns a pointer to the mouse confinement rectangle of a window, or <see langword="null" /> if there isn't one.</returns>
    public static unsafe Rect GetWindowMouseRect(nint window) {
        nint result = GetWindowMouseRectPtr(window);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowMouseRect: Failed to retrieve window mouse rect.");
            return new();
        }

        Rect rect = *(Rect*)result;

        return rect;
    }

    /// <summary>Get the opacity of a window.</summary>
    /// <param name="window">the window to get the current opacity value from.</param>
    /// <remarks>
    /// If transparency isn't supported on this platform, opacity will be returned as 1.0f without error.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetWindowOpacity"/>
    /// </remarks>
    /// <returns>Returns the opacity, (0.0f - transparent, 1.0f - opaque), or -1.0f on failure; call <see cref="GetError()" /> for more information.</returns>
    public static float GetWindowOpacity(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowOpacity: Window pointer is null.");
            return 0;
        }
        float opacity = SDL_GetWindowOpacity(window);
        if (opacity < 0) {
            LogError(LogCategory.Error, "GetWindowOpacity: Failed to retrieve window opacity.");
        }
        return opacity;
    }

    /// <summary>Get parent of a window.</summary>
    /// <param name="window">the window to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreatePopupWindow"/>
    /// </remarks>
    /// <returns>(SDL_Window *) Returns the parent of the window on success or <see langword="null" /> if the window has no parent.</returns>
    public static nint GetWindowParent(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowParent: Window handle is null.");
            return nint.Zero;
        }
        nint parentHandle = SDL_GetWindowParent(window);
        if (parentHandle == nint.Zero) {
            LogWarn(LogCategory.System, "GetWindowParent: Failed to retrieve parent window handle.");
        }
        return parentHandle;
    }

    /// <summary>Get the pixel density of a window.</summary>
    /// <param name="window">the window to query.</param>
    /// <remarks>
    /// This is a ratio of pixel size to window size. For example, if the window is
    /// 1920x1080 and it has a high density back buffer of 3840x2160 pixels, it
    /// would have a pixel density of 2.0.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowDisplayScale"/>
    /// </remarks>
    /// <returns>Returns the pixel density or 0.0f on failure; call <see cref="GetError()"/> for more information.</returns>
    public static float GetWindowPixelDensity(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowPixelDensity: Window pointer is null.");
            return 0;
        }
        float pixelDensity = SDL_GetWindowPixelDensity(window);
        if (pixelDensity < 0) {
            LogError(LogCategory.Error, "GetWindowPixelDensity: Failed to retrieve window pixel density.");
        }
        return pixelDensity;
    }

    /// <summary>Get the pixel format associated with the window.</summary>
    /// <param name="window">the window to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the pixel format of the window on success or <see cref="PixelFormat.Unknown"/> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static PixelFormat GetWindowPixelFormat(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowPixelFormat: Window pointer is null.");
            return PixelFormat.Unknown;
        }
        PixelFormat pixelFormat = SDL_GetWindowPixelFormat(window);
        if (pixelFormat == PixelFormat.Unknown) {
            LogError(LogCategory.Error, "GetWindowPixelFormat: Failed to retrieve window pixel format.");
        }
        return pixelFormat;
    }

    /// <summary>Get the position of a window.</summary>
    /// <param name="window">the window to query.</param>
    /// <param name="x">a pointer filled in with the x position of the window, may be discarded.</param>
    /// <param name="y">a pointer filled in with the y position of the window, may be discarded.</param>
    /// <remarks>
    /// This is the current position of the window as last reported by the windowing system.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetWindowPosition"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool GetWindowPosition(nint window, out int x, out int y) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowPosition: Window pointer is null.");
            x = y = 0;
            return false;
        }
        bool result = SDL_GetWindowPosition(window, out x, out y);
        if (!result) {
            LogError(LogCategory.Error, "GetWindowPosition: Failed to retrieve window position.");
        }
        return result;
    }

    /// <summary>Get the position of a window.</summary>
    /// <param name="window">the window to query.</param>
    /// <remarks>
    /// This is the current position of the window as last reported by the
    /// windowing system.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetWindowPosition"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static Point GetWindowPosition(nint window) {
        if (window == nint.Zero) {
            return default;
        }
        bool result = SDL_GetWindowPosition(window, out int x, out int y);
        if (!result) {
            LogError(LogCategory.Error, "GetWindowPosition: Failed to retrieve window position.");
            return default;
        }
        return new Point() { X = x, Y = y };
    }

    /// <summary>Get the properties associated with a window.</summary>
    /// <param name="window">the window to query.</param>
    /// <remarks>
    /// The following read-only properties are provided by SDL:
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns a valid property ID on success or 0 on failure; call <see cref="GetError()" /> for more information.</returns>
    public static uint GetWindowProperties(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowProperties: Window handle is null.");
            return 0;
        }
        uint properties = SDL_GetWindowProperties(window);
        if (properties == 0) {
            LogWarn(LogCategory.System, "GetWindowProperties: Failed to retrieve window properties.");
        }
        return properties;
    }

    /// <summary>Get a list of valid windows.</summary>
    /// <param name="count">a pointer filled in with the number of windows returned, may be discarded.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(SDL_Window **) Returns a <see langword="null" /> terminated array of SDL_Window pointers or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information. This is a single allocation that should be freed with <see cref="Free"/> when it is nol onger needed.</returns>
    public static Span<nint> GetWindows(out int count) {
        nint result = SDL_GetWindows(out count);

        if (result == nint.Zero) {
            LogError(LogCategory.Error, "GetWindows: Failed to retrieve windows.");
            count = 0;
            return [];
        }

        nint[] windows = new nint[count];
        if (windows == null) {
            LogError(LogCategory.Error, "GetWindows: Failed to create array for windows.");
            count = 0;
            return [];
        }

        Span<nint> windowSpan = new(windows);

        return windowSpan.ToArray();
    }

    /// <summary>Get a list of valid windows.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(SDL_Window **) Returns a <see langword="null" /> terminated array ofSDL_Window pointers or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information. This is a single allocation that should be freed with <see cref="Free"/> when it is no longer needed.</returns>
    public static Span<nint> GetWindows() {
        return GetWindows(out _);
    }

    /// <summary>Get the safe area for this window.</summary>
    /// <param name="window">the window to query.</param>
    /// <param name="rect">a pointer filled in with the client area that is safe for interactive content.</param>
    /// <remarks>
    /// Some devices have portions of the screen which are partially obscured or
    /// not interactive, possibly due to on-screen controls, curved edges, camera
    /// notches, TV overscan, etc. This function provides the area of the window
    /// which is safe to have interactable content. You should continue rendering
    /// into the rest of the window, but it should not contain visually important
    /// or interactible content.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool GetWindowSafeArea(nint window, out Rect rect) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowSafeArea: Window pointer is null.");
            rect = default;
            return false;
        }
        bool result = SDL_GetWindowSafeArea(window, out rect);
        if (!result) {
            LogError(LogCategory.Error, "GetWindowSafeArea: Failed to retrieve window safe area.");
        }
        return result;
    }

    /// <summary>Get the safe area for this window.</summary>
    /// <param name="window">the window to query.</param>
    /// <remarks>
    /// Some devices have portions of the screen which are partially obscured or
    /// not interactive, possibly due to on-screen controls, curved edges, camera
    /// notches, TV overscan, etc. This function provides the area of the window
    /// which is safe to have interactable content. You should continue rendering
    /// into the rest of the window, but it should not contain visually important
    /// or interactible content.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns a <see cref="Rect"/> on success or an empty <see cref="Rect"/> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static Rect GetWindowSafeArea(nint window) {
        if (window == nint.Zero) {
            return default;
        }
        bool result = SDL_GetWindowSafeArea(window, out Rect rect);
        if (!result) {
            LogError(LogCategory.Error, "GetWindowSafeArea: Failed to retrieve window safe area.");
            return default;
        }
        return rect;
    }

    /// <summary>Get the size of a window's client area.</summary>
    /// <param name="window">the window to query the width and height from.</param>
    /// <param name="w">a pointer filled in with the width of the window, may be discarded.</param>
    /// <param name="h">a pointer filled in with the height of the window, may be discarded.</param>
    /// <remarks>
    /// The window pixel size may differ from its window coordinate size if the
    /// window is on a high pixel density display. Use <see cref="GetWindowSizeInPixels"/> or <see cref="GetRenderOutputSize"/> to get the real client area size in pixels.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRenderOutputSize"/>
    /// <seealso cref="GetWindowSizeInPixels"/>
    /// <seealso cref="SetWindowSize"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool GetWindowSize(nint window, out int w, out int h) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowSize: Window pointer is null.");
            w = h = 0;
            return false;
        }
        bool result = SDL_GetWindowSize(window, out w, out h);
        if (!result) {
            LogError(LogCategory.Error, "GetWindowSize: Failed to retrieve window size.");
        }
        return result;
    }

    /// <summary>Get the size of a window's client area.</summary>
    /// <param name="window">the window to query the width and height from.</param>
    /// <remarks>
    /// The window pixel size may differ from its window coordinate size if the
    /// window is on a high pixel density display. Use <see cref="GetWindowSizeInPixels"/> or <see cref="GetRenderOutputSize"/> to get the real client area size in pixels.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRenderOutputSize"/>
    /// <seealso cref="GetWindowSizeInPixels"/>
    /// <seealso cref="SetWindowSize"/>
    /// </remarks>
    /// <returns>Returns a <see cref="Rect"/> on success or an empty <see cref="Rect"/> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static Rect GetWindowSize(nint window) {
        if (window == nint.Zero) {
            return default;
        }
        bool result = SDL_GetWindowSize(window, out int w, out int h);
        if (!result) {
            LogError(LogCategory.Error, "GetWindowSize: Failed to retrieve window size.");
            return default;
        }
        return new Rect() { W = w, H = h };
    }

    /// <summary>Get the size of a window's client area, in pixels.</summary>
    /// <param name="window">the window from which the drawable size should be queried.</param>
    /// <param name="w">a pointer to variable for storing the width in pixels, may be discarded.</param>
    /// <param name="h">a pointer to variable for storing the height in pixels, may be discarded.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateWindow"/>
    /// <seealso cref="GetWindowSize"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool GetWindowSizeInPixels(nint window, out int w, out int h) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowSizeInPixels: Window pointer is null.");
            w = h = 0;
            return false;
        }
        bool result = SDL_GetWindowSizeInPixels(window, out w, out h);
        if (!result) {
            LogError(LogCategory.Error, "GetWindowSizeInPixels: Failed to retrieve window size in pixels.");
        }
        return result;
    }

    /// <summary>Get the size of a window's client area, in pixels.</summary>
    /// <param name="window">the window from which the drawable size should be queried.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateWindow"/>
    /// <seealso cref="GetWindowSize"/>
    /// </remarks>
    /// <returns>Returns a <see cref="Rect"/> on success or an empty <see cref="Rect"/> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static Rect GetWindowSizeInPixels(nint window) {
        if (window == nint.Zero) {
            return default;
        }
        bool result = SDL_GetWindowSizeInPixels(window, out int w, out int h);
        if (!result) {
            LogError(LogCategory.Error, "GetWindowSizeInPixels: Failed to retrieve window size in pixels.");
            return default;
        }
        return new Rect() { W = w, H = h };
    }

    /// <summary>
    /// Get the SDL surface associated with the window.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <remarks>
    /// A new surface will be created with the optimal format for the window, if necessary. This surface will be freed when the window is destroyed. Do not free this surface.
    /// <para>This surface will be invalidated if the window is resized.After resizing a window this function must be called again to return a valid surface.</para>
    /// <para>You may not combine this with 3D or the rendering API on this window.</para>
    /// <para>This function is affected by SDL_HINT_FRAMEBUFFER_ACCELERATION.</para>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="DestroyWindowSurface"/>
    /// <seealso cref="WindowHasSurface"/>
    /// <seealso cref="UpdateWindowSurface"/>
    /// <seealso cref="UpdateWindowSurfaceRects"/>
    /// </remarks>
    /// <returns>(SDL_Surface *) Returns the surface associated with the window, or <see cref="nint.Zero"/> on failure; call <see cref="GetError"/> for more information.</returns>
    public static nint GetWindowSurface(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowSurface: Window pointer is null.");
            return nint.Zero;
        }
        nint surface = SDL_GetWindowSurface(window);
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowSurface: Failed to retrieve window surface.");
        }
        return surface;
    }

    /// <summary>Get VSync for the window surface.</summary>
    /// <param name="window">the window to query.</param>
    /// <param name="vsync">an int filled with the current vertical refresh sync interval. See <see cref="SetWindowSurfaceVSync"/> for the meaning of the value.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetWindowSurfaceVSync"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool GetWindowSurfaceVSync(nint window, out int vsync) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowSurfaceVSync: Window pointer is null.");
            vsync = 0;
            return false;
        }
        bool result = SDL_GetWindowSurfaceVSync(window, out vsync);
        if (!result) {
            LogError(LogCategory.Error, "GetWindowSurfaceVSync: Failed to retrieve window surface VSync.");
        }
        return result;
    }

    /// <summary>Get VSync for the window surface.</summary>
    /// <param name="window">the window to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetWindowSurfaceVSync"/>
    /// </remarks>
    /// <returns>Returns the vsync on success or 0 on failure; call <see cref="GetError()"/> for more information.</returns>
    public static int GetWindowSurfaceVSync(nint window) {
        if (window == nint.Zero) {
            return 0;
        }
        bool result = SDL_GetWindowSurfaceVSync(window, out int vsync);
        if (!result) {
            LogError(LogCategory.Error, "GetWindowSurfaceVSync: Failed to retrieve window surface VSync.");
            return 0;
        }
        return vsync;
    }

    /// <summary>Get the title of a window.</summary>
    /// <param name="window">the window to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetWindowTitle"/>
    /// </remarks>
    /// <returns>Returns the title of the window in UTF-8 format or &quot;&quot; if there is no title.</returns>
    public static string GetWindowTitle(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "GetWindowTitle: Window handle is null.");
            return string.Empty;
        }
        string title = SDL_GetWindowTitle(window);
        if (string.IsNullOrEmpty(title)) {
            LogWarn(LogCategory.System, "GetWindowTitle: Failed to retrieve window title.");
        }
        return title;
    }

    /// <summary>Get an ASCII string representation for a given <see cref="SdlGuid"/>.</summary>
    /// <param name="guid">the <see cref="SdlGuid"/> you wish to convert to string.</param>
    /// <param name="pszGuid">buffer in which to write the ASCII string.</param>
    /// <param name="cbGuid">the size of pszGUID, should be at least 33 bytes.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="StringToGuid"/>
    /// </remarks>
    public static void GuidToString(SdlGuid guid, string pszGuid, int cbGuid) {
        if (guid.Data is null) {
            LogError(LogCategory.Error, "GuidToString: GUID is null.");
            return;
        }
        SDL_GUIDToString(guid, pszGuid, cbGuid);
    }

    /// <summary>Query whether there is data in the clipboard for the provided mime type.</summary>
    /// <param name="mimeType">the mime type to check for data for.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetClipboardData"/>
    /// <seealso cref="GetClipboardData"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if there exists data in clipboard for the provided mimetype, <see langword="false" /> if it does not.</returns>
    public static bool HasClipboardData(string mimeType) {
        if (string.IsNullOrEmpty(mimeType)) {
            LogError(LogCategory.Error, "HasClipboardData: MIME type is null or empty.");
            return false;
        }
        bool result = SDL_HasClipboardData(mimeType);
        if (!result) {
            LogError(LogCategory.Error, "HasClipboardData: Failed to check clipboard data.");
        }
        return result;
    }

    /// <summary>Query whether the clipboard exists and contains a non-empty text string.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetClipboardText"/>
    /// <seealso cref="SetClipboardText"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the clipboard has text, or <see langword="false" /> if it does not.</returns>
    public static bool HasClipboardText() {
        bool result = SDL_HasClipboardText();
        if (!result) {
            LogError(LogCategory.Error, "HasClipboardText: Failed to check clipboard text.");
        }
        return result;
    }

    /// <summary>Return whether a keyboard is currently connected.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetKeyboards"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if a keyboard is connected, <see langword="false" /> otherwise.</returns>
    public static bool HasKeyboard() {
        bool result = SDL_HasKeyboard();
        if (!result) {
            LogError(LogCategory.Error, "HasKeyboard: Failed to check keyboard.");
        }
        return result;
    }

    /// <summary>Query whether the primary selection exists and contains a non-empty text string.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetPrimarySelectionText"/>
    /// <seealso cref="SetPrimarySelectionText"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the primary selection has text, or <see langword="false" /> if it does not.</returns>
    public static bool HasPrimarySelectionText() {
        bool result = SDL_HasPrimarySelectionText();
        if (!result) {
            LogError(LogCategory.Error, "HasPrimarySelectionText: Failed to check primary selection text.");
        }
        return result;
    }

    /// <summary>Return whether a property exists in a group of properties.</summary>
    /// <param name="props">the properties to query.</param>
    /// <param name="name">the name of the property to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetPropertyType"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the property exists, or <see langword="false" /> if it doesn't.</returns>
    public static bool HasProperty(uint props, string name) {
        if (string.IsNullOrEmpty(name)) {
            LogError(LogCategory.Error, "HasProperty: Property name is null or empty.");
            return false;
        }
        bool result = SDL_HasProperty(props, name);
        if (!result) {
            LogError(LogCategory.Error, "HasProperty: Failed to check property.");
        }
        return result;
    }

    /// <summary>Determine whether two rectangles intersect.</summary>
    /// <param name="a">an <see cref="Rect"/> structure representing the first rectangle.</param>
    /// <param name="b">an <see cref="Rect"/> structure representing the second rectangle.</param>
    /// <remarks>
    /// If either pointer is <see langword="null" /> the function will return false.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRectIntersection"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if there is an intersection, <see langword="false" /> otherwise.</returns>
    public static bool HasRectIntersection(ref Rect a, ref Rect b) {
        bool result = SDL_HasRectIntersection(ref a, ref b);
        if (!result) {
            LogError(LogCategory.Error, "HasRectIntersection: Failed to check rectangle intersection.");
        }
        return result;
    }

    /// <summary>Determine whether two rectangles intersect with float precision.</summary>
    /// <param name="a">an <see cref="FRect"/> structure representing the first rectangle.</param>
    /// <param name="b">an <see cref="FRect"/> structure representing the second rectangle.</param>
    /// <remarks>
    /// If either pointer is <see langword="null" /> the function will return false.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRectIntersection"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if there is an intersection, <see langword="false" /> otherwise.</returns>
    public static bool HasRectIntersectionFloat(ref FRect a, ref FRect b) {
        bool result = SDL_HasRectIntersectionFloat(ref a, ref b);
        if (!result) {
            LogError(LogCategory.Error, "HasRectIntersectionFloat: Failed to check rectangle intersection.");
        }
        return result;
    }

    /// <summary>Check whether the platform has screen keyboard support.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="StartTextInput"/>
    /// <seealso cref="ScreenKeyboardShown"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the platform has some screen keyboard support or<see langword="false" /> if not.</returns>
    public static bool HasScreenKeyboardSupport() {
        bool result = SDL_HasScreenKeyboardSupport();
        if (!result) {
            LogError(LogCategory.Error, "HasScreenKeyboardSupport: Failed to check screen keyboard support.");
        }
        return result;
    }

    /// <summary>Hide a window.</summary>
    /// <param name="window">the window to hide.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ShowWindow"/>
    /// <seealso cref="WindowFlags.Hidden"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool HideWindow(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "HideWindow: Window pointer is null.");
            return false;
        }
        bool result = SDL_HideWindow(window);
        if (!result) {
            LogError(LogCategory.Error, "HideWindow: Failed to hide window.");
        }
        return result;
    }

    /// <summary>
    /// Initialize the SDL library.
    /// </summary>
    /// <param name="flags">subsystem initialization flags.</param>
    /// <remarks>
    /// <see cref="Init"/> simply forwards to calling <see cref="InitSubSystem"/>. Therefore, the two may be used interchangeably. Though for readability of your code <see cref="InitSubSystem"/> might be preferred.
    /// <para>The file I/O(for example: <see cref="IoFromFile"/>) and threading (<see cref="CreateThread"/>) subsystems are initialized by default.</para>
    /// <para>Message boxes(<see cref="ShowSimpleMessageBox"/>) also attempt to work without initializing the video subsystem, in hopes of being useful in showing an error dialog when <see cref="Init"/> fails. You must specifically initialize other subsystems if you use them in your application.</para>
    /// <para>Logging(such as <see cref="Log"/>) works without initialization, too.
    /// flags may be any of the following OR'd together:
    /// <list type="bullet">
    /// <item><see cref="InitFlags.Audio"/>: audio subsystem; automatically initializes the events subsystem</item>
    /// <item><see cref="InitFlags.Video"/>: video subsystem; automatically initializes the events subsystem, should be initialized on the main thread.</item>
    /// <item><see cref="InitFlags.Joystick"/>: joystick subsystem; automatically initializes the events subsystem</item>
    /// <item><see cref="InitFlags.Haptic"/>: haptic(force feedback) subsystem</item>
    /// <item><see cref="InitFlags.Gamepad"/>: gamepad subsystem; automatically initializes the joystick subsystem</item>
    /// <item><see cref="InitFlags.Events"/>: events subsystem</item>
    /// <item><see cref="InitFlags.Sensor"/>: sensor subsystem; automatically initializes the events subsystem</item>
    /// <item><see cref="InitFlags.Camera"/>: camera subsystem; automatically initializes the events subsystem</item>
    /// <item><see cref="InitFlags.Everything"/>: all of the above subsystems; automatically initializes the events subsystem</item>item>
    /// </list>
    /// </para>
    /// <para>Subsystem initialization is ref-counted, you must call <see cref="QuitSubSystem"/> for each <see cref="InitSubSystem"/> to correctly shutdown a subsystem manually(or call <see cref="Quit"/> to force shutdown). If a subsystem is already loaded then this call will increase the ref-count and return.</para>
    /// <para>Consider reporting some basic metadata about your application before calling <see cref="Init"/>, using either <see cref="SetAppMetadata"/> or <see cref="SetAppMetadataProperty"/>.</para>
    /// </remarks>
    /// <returns></returns>
    public static bool Init(InitFlags flags) {
        if (!Enum.IsDefined(flags)) {
            LogError(LogCategory.Error, "Init: Invalid initialization flags.");
            return false;
        }

        bool result = SDL_Init(flags);
        if (!result) {
            LogError(LogCategory.Error, "Init: Failed to initialize SDL.");
        }
        return result;
    }

    /// <summary>Compatibility function to initialize the SDL library.</summary>
    /// <param name="flags">any of the flags used by <see cref="Init"/>(); see <see cref="Init"/> for details.</param>
    /// <remarks>
    /// This function and <see cref="Init"/>() are interchangeable.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="Init"/>
    /// <seealso cref="Quit"/>
    /// <seealso cref="QuitSubSystem"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool InitSubSystem(InitFlags flags) {
        if (!Enum.IsDefined(flags)) {
            LogError(LogCategory.Error, "InitSubSystem: Invalid initialization flags.");
            return false;
        }
        bool result = SDL_InitSubSystem(flags);
        if (!result) {
            LogError(LogCategory.Error, "InitSubSystem: Failed to initialize SDL subsystem.");
        }
        return result;
    }

    /// <summary>Return whether this is the main thread.</summary>
    /// <remarks>
    /// On Apple platforms, the main thread is the thread that runs your program's
    /// main() entry point. On other platforms, the main thread is the one that
    /// calls SDL_Init(SDL_INIT_VIDEO), which should
    /// usually be the one that runs your program's main() entry point. If you are
    /// using the main callbacks, <see cref="SdlAppInitFunc"/>,
    /// <see cref="SdlAppIterateFunc"/>, and <see cref="SdlAppQuitFunc"/> are
    /// all called on the main thread.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RunOnMainThread"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if this thread is the main thread, or <see langword="false" /> otherwise.</returns>
    public static bool IsMainThread() {
        bool result = SDL_IsMainThread();
        if (!result) {
            LogError(LogCategory.Error, "IsMainThread: Failed to check if current thread is main thread.");
        }
        return result;
    }

    /// <summary>Load a BMP image from a file.</summary>
    /// <param name="file">the BMP file to load.</param>
    /// <remarks>
    /// The new surface should be freed with <see cref="DestroySurface"/>. Not doing so will result in a memory leak.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="DestroySurface"/>
    /// <seealso cref="LoadBmp_IO"/>
    /// <seealso cref="SaveBMP"/>
    /// </remarks>
    /// <returns>(SDL_Surface *) Returns a pointer to a new SDL_Surface structure or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static nint LoadBmp(string file) {
        if (string.IsNullOrEmpty(file)) {
            LogError(LogCategory.Error, "LoadBmp: File path is null or empty.");
            return nint.Zero;
        }
        nint surface = SDL_LoadBMP(file);
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "LoadBmp: Failed to load BMP file.");
        }
        return surface;
    }

    /// <summary>
    /// Load a BMP image from a seekable SDL data stream.
    /// </summary>
    /// <param name="src">the data stream for the surface.</param>
    /// <param name="closeIo">if <see langword="true"/>, calls <see cref="CloseIo"/> on src before returning, even in the case of an error.</param>
    /// <remarks>
    /// The new surface should be freed with <see cref="DestroySurface"/>. Not doing so will result in a memory leak.
    /// <para><strong>Thread Safety:</strong> it is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL3 3.2.0.</para>
    /// <seealso cref="DestroySurface"/>
    /// <seealso cref="LoadBmp"/>
    /// <seealso cref="SaveBmpIo"/>
    /// </remarks>
    /// <returns>(SDL_Surface *) Returns a pointer to a new SDL_Surface structure or <see langword="null"/> on failure; call <see cref="GetError"/> for more information.</returns>
    public static nint LoadBmpIo(nint src, bool closeIo) {
        if (src == nint.Zero) {
            LogError(LogCategory.Error, "LoadBmpIo: Source pointer is null.");
            return nint.Zero;
        }
        nint surface = SDL_LoadBMP_IO(src, closeIo);
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "LoadBmpIo: Failed to load BMP from IO source.");
        }
        return surface;
    }

    /// <summary>Look up the address of the named function in a shared object.</summary>
    /// <param name="handle">a valid shared object handle returned by SDL_LoadObject().</param>
    /// <param name="name">the name of the function to look up.</param>
    /// <remarks>
    /// This function pointer is no longer valid after calling <see cref="UnloadObject"/>.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LoadObject"/>
    /// </remarks>
    /// <returns>Returns a pointer to the function or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static nint LoadFunction(nint handle, string name) {
        if (handle == nint.Zero) {
            LogError(LogCategory.Error, "LoadFunction: Handle pointer is null.");
            return nint.Zero;
        }
        if (string.IsNullOrEmpty(name)) {
            LogError(LogCategory.Error, "LoadFunction: Function name is null or empty.");
            return nint.Zero;
        }
        nint function = SDL_LoadFunction(handle, name);
        if (function == nint.Zero) {
            LogError(LogCategory.Error, "LoadFunction: Failed to load function.");
        }
        return function;
    }

    /// <summary>Dynamically load a shared object.</summary>
    /// <param name="sofile">a system-dependent name of the object file.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LoadFunction"/>
    /// <seealso cref="UnloadObject"/>
    /// </remarks>
    /// <returns>(SDL_SharedObject *) Returns an opaque pointer to the object handle or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static nint LoadObject(string sofile) {
        if (string.IsNullOrEmpty(sofile)) {
            LogError(LogCategory.Error, "LoadObject: Shared object file path is null or empty.");
            return nint.Zero;
        }
        nint handle = SDL_LoadObject(sofile);
        if (handle == nint.Zero) {
            LogError(LogCategory.Error, "LoadObject: Failed to load shared object.");
        }
        return handle;
    }

    /// <summary>Lock a group of properties.</summary>
    /// <param name="props">the properties to lock.</param>
    /// <remarks>
    /// Obtain a multi-threaded lock for these properties. Other threads will wait
    /// while trying to lock these properties until they are unlocked. Properties
    /// must be unlocked before they are destroyed.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="UnlockProperties"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool LockProperties(uint props) {
        if (props == 0) {
            LogError(LogCategory.Error, "LockProperties: Properties are zero.");
            return false;
        }
        bool result = SDL_LockProperties(props);
        if (!result) {
            LogError(LogCategory.Error, "LockProperties: Failed to lock properties.");
        }
        return result;
    }

    /// <summary>Set up a surface for directly accessing the pixels.</summary>
    /// <param name="surface">the <see cref="Surface"/> structure to be locked.</param>
    /// <remarks>
    /// Between calls to <see cref="LockSurface"/> /
    /// <see cref="UnlockSurface"/>, you can write to and read from
    /// surface-&gt;pixels, using the pixel format stored in surface-&gt;format. Once
    /// you are done accessing the surface, you should use
    /// <see cref="UnlockSurface"/> to release it.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe. The locking referred to by this function is making the pixels available for direct access, not thread-safe locking.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="MUSTLOCK"/>
    /// <seealso cref="UnlockSurface"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
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

    /// <summary>Allocate uninitialized memory.</summary>
    /// <param name="size">the size to allocate.</param>
    /// <remarks>
    /// The allocated memory returned by this function must be freed with <see cref="Free"/>.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="Free"/>
    /// <seealso cref="calloc"/>
    /// <seealso cref="realloc"/>
    /// <seealso cref="aligned_alloc"/>
    /// </remarks>
    /// <returns>(void *) Returns a pointer to the allocated memory, or <see langword="null" /> if allocation failed.</returns>
    public static nint Malloc(nuint size) {
        if (size == nuint.Zero) {
            LogWarn(LogCategory.System, "Malloc: Size is zero.");
            return nint.Zero;
        }

        nint res = SDL_malloc(size);
        if (res == nint.Zero) {
            LogError(LogCategory.Error, "Malloc: Memory allocation failed.");
            SDL_OutOfMemory();
            return nint.Zero;
        }

        return res;
    }

    /// <summary>Map an RGB triple to an opaque pixel value for a given pixel format.</summary>
    /// <param name="format">a pointer to <see cref="PixelFormat"/>Details describing the pixel format.</param>
    /// <param name="palette">an optional palette for indexed formats, may be discarded.</param>
    /// <param name="r">the red component of the pixel in the range 0-255.</param>
    /// <param name="g">the green component of the pixel in the range 0-255.</param>
    /// <param name="b">the blue component of the pixel in the range 0-255.</param>
    /// <remarks>
    /// This function maps the RGB color value to the specified pixel format and
    /// returns the pixel value best approximating the given RGB color value for
    /// the given pixel format.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, as long as the palette is not modified.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetPixelFormatDetails"/>
    /// <seealso cref="GetRgb"/>
    /// <seealso cref="MapRgba"/>
    /// <seealso cref="MapSurfaceRgb"/>
    /// </remarks>
    /// <returns>Returns a pixel value.</returns>
    public static uint MapRgb(nint format, nint palette, byte r, byte g, byte b) {
        if (format == nint.Zero || palette == nint.Zero) {
            LogError(LogCategory.Error, "MapRgb: Format or palette pointer is null.");
            return 0;
        }
        uint color = SDL_MapRGB(format, palette, r, g, b);
        if (color == 0) {
            LogError(LogCategory.Error, "MapRgb: Failed to map RGB color.");
        }
        return color;
    }

    /// <summary>Map an RGBA quadruple to a pixel value for a given pixel format.</summary>
    /// <param name="format">a pointer to <see cref="PixelFormat"/>Details describing the pixel format.</param>
    /// <param name="palette">an optional palette for indexed formats, may be discarded.</param>
    /// <param name="r">the red component of the pixel in the range 0-255.</param>
    /// <param name="g">the green component of the pixel in the range 0-255.</param>
    /// <param name="b">the blue component of the pixel in the range 0-255.</param>
    /// <param name="a">the alpha component of the pixel in the range 0-255.</param>
    /// <remarks>
    /// This function maps the RGBA color value to the specified pixel format and
    /// returns the pixel value best approximating the given RGBA color value for
    /// the given pixel format.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, as long as the palette is not modified.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetPixelFormatDetails"/>
    /// <seealso cref="GetRgba"/>
    /// <seealso cref="MapRgb"/>
    /// <seealso cref="MapSurfaceRgba"/>
    /// </remarks>
    /// <returns>Returns a pixel value.</returns>
    public static uint MapRgba(nint format, nint palette, byte r, byte g, byte b, byte a) {
        if (format == nint.Zero || palette == nint.Zero) {
            LogError(LogCategory.Error, "MapRgba: Format or palette pointer is null.");
            return 0;
        }
        uint color = SDL_MapRGBA(format, palette, r, g, b, a);
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
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="MapSurfaceRgba"/>
    /// </remarks>
    /// <returns>Returns a pixel value.</returns>
    public static uint MapSurfaceRgb(nint surface, byte r, byte g, byte b) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "MapSurfaceRgb: Surface pointer is null.");
            return 0;
        }
        uint color = SDL_MapSurfaceRGB(surface, r, g, b);
        if (color == 0) {
            LogError(LogCategory.Error, "MapSurfaceRgb: Failed to map surface RGB color.");
        }
        return color;
    }

    /// <summary>Map an RGB triple to an opaque pixel value for a surface.</summary>
    /// <param name="surface">the surface to use for the pixel format and palette.</param>
    /// <param name="color">the <see cref="Color"/> representing RGB ranging from 0-255.</param>
    /// <remarks>
    /// This function maps the RGB color value to the specified pixel format and
    /// returns the pixel value best approximating the given RGB color value for
    /// the given pixel format.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="MapSurfaceRgba"/>
    /// </remarks>
    /// <returns>Returns a pixel value.</returns>
    public static uint MapSurfaceRgb(nint surface, Color color) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "MapSurfaceRgb: Surface pointer is null.");
            return 0;
        }

        uint colorValue = SDL_MapSurfaceRGB(surface, color.R, color.G, color.B);
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
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="MapSurfaceRgb"/>
    /// </remarks>
    /// <returns>Returns a pixel value.</returns>
    public static uint MapSurfaceRgba(nint surface, byte r, byte g, byte b, byte a) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "MapSurfaceRgba: Surface pointer is null.");
            return 0;
        }
        uint color = SDL_MapSurfaceRGBA(surface, r, g, b, a);
        if (color == 0) {
            LogError(LogCategory.Error, "MapSurfaceRgba: Failed to map surface RGBA color.");
        }
        return color;
    }

    /// <summary>Map an RGBA quadruple to a pixel value for a surface.</summary>
    /// <param name="surface">the surface to use for the pixel format and palette.</param>
    /// <param name="color">the <see cref="Color"/> representing RGB ranging from 0-255.</param>
    /// <remarks>
    /// This function maps the RGBA color value to the specified pixel format and
    /// returns the pixel value best approximating the given RGBA color value for
    /// the given pixel format.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="MapSurfaceRgb"/>
    /// </remarks>
    /// <returns>Returns a pixel value.</returns>
    public static uint MapSurfaceRgba(nint surface, Color color) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "MapSurfaceRgba: Surface pointer is null.");
            return 0;
        }
        uint colorValue = SDL_MapSurfaceRGBA(surface, color.R, color.G, color.B, color.A);
        if (colorValue == 0) {
            LogError(LogCategory.Error, "MapSurfaceRgba: Failed to map surface RGBA color.");
        }
        return colorValue;
    }

    /// <summary>Request that the window be made as large as possible.</summary>
    /// <param name="window">the window to maximize.</param>
    /// <remarks>
    /// Non-resizable windows can't be maximized. The window must have the <see cref="WindowFlags.Resizable"/> flag set, or this will have no effect.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="MinimizeWindow"/>
    /// <seealso cref="RestoreWindow"/>
    /// <seealso cref="SyncWindow"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool MaximizeWindow(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "MaximizeWindow: Window pointer is null.");
            return false;
        }
        bool result = SDL_MaximizeWindow(window);
        if (!result) {
            LogError(LogCategory.Error, "MaximizeWindow: Failed to maximize window.");
        }
        return result;
    }

    /// <summary>Request that the window be minimized to an iconic representation.</summary>
    /// <param name="window">the window to minimize.</param>
    /// <remarks>
    /// If the window is in a fullscreen state, this request has no direct effect.
    /// It may alter the state the window is returned to when leaving fullscreen.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="MaximizeWindow"/>
    /// <seealso cref="RestoreWindow"/>
    /// <seealso cref="SyncWindow"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool MinimizeWindow(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "MinimizeWindow: Window pointer is null.");
            return false;
        }
        bool result = SDL_MinimizeWindow(window);
        if (!result) {
            LogError(LogCategory.Error, "MinimizeWindow: Failed to minimize window.");
        }
        return result;
    }

    /// <summary>Set an error indicating that memory allocation failed.</summary>
    /// <remarks>
    /// This function does not do any memory allocation.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="false" />.</returns>
    public static bool OutOfMemory() {
        return SDL_OutOfMemory();
    }

    /// <summary>Premultiply the alpha on a block of pixels.</summary>
    /// <param name="width">the width of the block to convert, in pixels.</param>
    /// <param name="height">the height of the block to convert, in pixels.</param>
    /// <param name="src">a pointer to the source pixels.</param>
    /// <param name="dst">a pointer to be filled in with premultiplied pixel data.</param>
    /// <param name="linear"><see langword="true" /> to convert from sRGB to linear space for the alpha multiplication, <see langword="false" /> to do multiplication in sRGB space.</param>
    /// <remarks>
    /// This is safe to use with <paramref name="src"/> == <paramref name="dst"/>, but not for other overlapping areas.
    /// <para><strong>Thread Safety:</strong> The same destination pixels should not be used from two threads at once. It is safe to use the same source pixels from multiple threads.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool PremultiplyAlpha(int width, int height, PixelFormat srcFormat, nint src,
                int srcPitch, PixelFormat dstFormat, nint dst, int dstPitch, bool linear) {
        if (width <= 0 || height <= 0) {
            LogError(LogCategory.Error, "PremultiplyAlpha: Invalid width or height.");
            return false;
        }
        bool result = SDL_PremultiplyAlpha(width, height, srcFormat, src, srcPitch, dstFormat, dst, dstPitch,
                linear);
        if (!result) {
            LogError(LogCategory.Error, "PremultiplyAlpha: Failed to premultiply alpha.");
        }
        return result;
    }

    /// <summary>Premultiply the alpha on a block of pixels.</summary
    /// <param name="src">a pointer to the source pixels.</param>
    /// <param name="dst">a pointer to be filled in with premultiplied pixel data.</param>
    /// <param name="linear"><see langword="true" /> to convert from sRGB to linear space for the alpha multiplication, <see langword="false" /> to do multiplication in sRGB space.</param>
    /// <remarks>
    /// This is safe to use with <paramref name="src"/> == <paramref name="dst"/>, but not for other overlapping areas.
    /// <para><strong>Thread Safety:</strong> The same destination pixels should not be used from two threads at once. It is safe to use the same source pixels from multiple threads.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool PremultiplyAlpha(Rect rect, PixelFormat srcFormat, nint src,
                int srcPitch, PixelFormat dstFormat, nint dst, int dstPitch, bool linear) {
        if (rect.W <= 0 || rect.H <= 0) {
            LogError(LogCategory.Error, "PremultiplyAlpha: Invalid rectangle dimensions.");
            return false;
        }
        bool result = SDL_PremultiplyAlpha(rect.W, rect.H, srcFormat, src, srcPitch, dstFormat, dst, dstPitch,
                linear);
        if (!result) {
            LogError(LogCategory.Error, "PremultiplyAlpha: Failed to premultiply alpha.");
        }
        return result;
    }

    /// <summary>Premultiply the alpha in a surface.</summary>
    /// <param name="surface">the surface to modify.</param>
    /// <param name="linear"><see langword="true" /> to convert from sRGB to linear space for the alpha multiplication, <see langword="false" /> to do multiplication in sRGB space.</param>
    /// <remarks>
    /// This is safe to use with src == dst, but not for other overlapping areas.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool PremultiplySurfaceAlpha(nint surface, bool linear) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "PremultiplySurfaceAlpha: Surface pointer is null.");
            return false;
        }
        bool result = SDL_PremultiplySurfaceAlpha(surface, linear);
        if (!result) {
            LogError(LogCategory.Error, "PremultiplySurfaceAlpha: Failed to premultiply surface alpha.");
        }
        return result;
    }

    /// <summary>
    /// Clean up all initialized subsystems.
    /// </summary>
    /// <remarks>
    /// You should call this function even if you have already shutdown each initialized subsystem with <see cref="QuitSubSystem"/>. It is safe to call this function even in the case of errors in initialization.
    /// You can use this function with atexit() to ensure that it is run when your application is shutdown, but it is not wise to do this from a library or other dynamically loaded code.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="Init"/>
    /// <seealso cref="QuitSubSystem"/>
    /// </remarks>
    public static void Quit() {
        SDL_Quit();
    }

    /// <summary>Shut down specific SDL subsystems.</summary>
    /// <param name="flags">any of the flags used by <see cref="Init"/>; see <see cref="Init"/> for details.</param>
    /// <remarks>
    /// You still need to call <see cref="Quit"/> even if you close all open
    /// subsystems with <see cref="QuitSubSystem"/>.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="InitSubSystem"/>
    /// <seealso cref="Quit"/>
    /// </remarks>
    public static void QuitSubSystem(InitFlags flags) {
        if (!Enum.IsDefined(flags)) {
            LogError(LogCategory.Error, "QuitSubSystem: Invalid initialization flags.");
            return;
        }
        SDL_QuitSubSystem(flags);
    }

    /// <summary>
    /// Request that a window be raised above other windows and gain the input focus.
    /// </summary>
    /// <param name="window">the window to raise.</param>
    /// <returns>Returns <see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="GetError"/> for more information.</returns>
    public static bool RaiseWindow(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "RaiseWindow: Window pointer is null.");
            return false;
        }
        bool result = SDL_RaiseWindow(window);
        if (!result) {
            LogError(LogCategory.Error, "RaiseWindow: Failed to raise window.");
        }
        return result;
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
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
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
    /// <remarks>
    /// This function prioritizes correctness over speed: it is suitable for unit
    /// tests, but is not intended for use in a game engine.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool ReadSurfacePixel(nint surface, int x, int y, out Color color) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "ReadSurfacePixel: Surface pointer is null.");
            color = default;
            return false;
        }
        bool result = SDL_ReadSurfacePixel(surface, x, y, out byte r, out byte g, out byte b, out byte a);
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
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static Color ReadSurfacePixel(nint surface, int x, int y) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "ReadSurfacePixel: Surface pointer is null.");
            return default;
        }
        bool result = SDL_ReadSurfacePixel(surface, x, y, out byte r, out byte g, out byte b, out byte a);
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
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
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
    /// <param name="color">the <see cref="FColor"/> structure filled with color data, or discard to ignore.</param>
    /// <remarks>
    /// This function prioritizes correctness over speed: it is suitable for unit
    /// tests, but is not intended for use in a game engine.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool ReadSurfacePixelFloat(nint surface, int x, int y, out FColor color) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "ReadSurfacePixelFloat: Surface pointer is null.");
            color = default;
            return false;
        }
        bool result = SDL_ReadSurfacePixelFloat(surface, x, y, out float r, out float g, out float b, out float a);
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
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static FColor ReadSurfacePixelFloat(nint surface, int x, int y) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "ReadSurfacePixelFloat: Surface pointer is null.");
            return default;
        }
        bool result = SDL_ReadSurfacePixelFloat(surface, x, y, out float r, out float g, out float b, out float a);
        if (!result) {
            LogError(LogCategory.Error, "ReadSurfacePixelFloat: Failed to read surface pixel.");
            return default;
        }
        return new FColor() { R = r, G = g, B = b, A = a };
    }

    /// <summary>Remove a function watching a particular hint.</summary>
    /// <param name="name">the hint being watched.</param>
    /// <param name="callback">an <see cref="SdlHintCallback"/> function that will be called when the hint value changes.</param>
    /// <param name="userdata">a pointer being passed to the callback function.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="AddHintCallback"/>
    /// </remarks>
    public static void RemoveHintCallback(string name, SdlHintCallback callback, nint userdata) {
        if (string.IsNullOrEmpty(name)) {
            LogError(LogCategory.Error, "RemoveHintCallback: Hint name is null or empty.");
            return;
        }
        SDL_RemoveHintCallback(name, callback, userdata);
    }

    /// <summary>Remove all alternate versions of a surface.</summary>
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <remarks>
    /// This function removes a reference from all the alternative versions,
    /// destroying them if this is the last reference to them.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="AddSurfaceAlternateImage"/>
    /// <seealso cref="GetSurfaceImages"/>
    /// <seealso cref="SurfaceHasAlternateImages"/>
    /// </remarks>
    public static void RemoveSurfaceAlternateImages(nint surface) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "RemoveSurfaceAlternateImages: Surface pointer is null.");
            return;
        }
        SDL_RemoveSurfaceAlternateImages(surface);
    }

    /// <summary>Reset a hint to the default value.</summary>
    /// <param name="name">the hint to set.</param>
    /// <remarks>
    /// This will reset a hint to the value of the environment variable, or <see langword="null" /> if
    /// the environment isn't set. Callbacks will be called normally with this
    /// change.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetHint"/>
    /// <seealso cref="ResetHints"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool ResetHint(string name) {
        if (string.IsNullOrEmpty(name)) {
            LogError(LogCategory.Error, "ResetHint: Hint name is null or empty.");
            return false;
        }
        bool result = SDL_ResetHint(name);
        if (!result) {
            LogError(LogCategory.Error, "ResetHint: Failed to reset hint.");
        }
        return result;
    }

    /// <summary>Reset all hints to the default values.</summary>
    /// <remarks>
    /// This will reset all hints to the value of the associated environment
    /// variable, or <see langword="null" /> if the environment isn't set. Callbacks will be called
    /// normally with this change.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ResetHint"/>
    /// </remarks>
    public static void ResetHints() {
        SDL_ResetHints();
    }

    /// <summary>Clear the state of the keyboard.</summary>
    /// <remarks>
    /// This function will generate key up events for all pressed keys.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetKeyboardState"/>
    /// </remarks>
    public static void ResetKeyboard() {
        SDL_ResetKeyboard();
    }

    /// <summary>Request that the size and position of a minimized or maximized window be restored.</summary>
    /// <param name="window">the window to restore.</param>
    /// <remarks>
    /// If the window is in a fullscreen state, this request has no direct effect.
    /// It may alter the state the window is returned to when leaving fullscreen.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="MaximizeWindow"/>
    /// <seealso cref="MinimizeWindow"/>
    /// <seealso cref="SyncWindow"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool RestoreWindow(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "RestoreWindow: Window pointer is null.");
            return false;
        }
        bool result = SDL_RestoreWindow(window);
        if (!result) {
            LogError(LogCategory.Error, "RestoreWindow: Failed to restore window.");
        }
        return result;
    }

    /// <summary>Initializes and launches an SDL application, by doing platform-specific initialization before calling your mainFunction and cleanups after it returns, if that is needed for a specific platform, otherwise it just calls mainFunction.</summary>
    /// <param name="argc">the argc parameter from the application's main() function, or 0 if the platform's main-equivalent has no argc.</param>
    /// <param name="argv">the argv parameter from the application's main() function, or <see cref="nint.Zero"/> if the platform's main-equivalent has no argv.</param>
    /// <param name="mainFunction">your SDL app's C-style main(). NOT the function you're calling this from! Its name doesn't matter; it doesn't literally have to be main.</param>
    /// <param name="reserved">should be <see cref="nint.Zero"/> (reserved for future use, will probably be platform-specific then).</param>
    /// <remarks>
    /// You can use this if you want to use your own main() implementation without
    /// using SDL_main (like when using SDL_MAIN_HANDLED). When using this, you do not need <see cref="SetMainReady"/>.
    /// <para><strong>Thread Safety:</strong> Generally this is called once, near startup, from the process's initial thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the return value from mainFunction: 0 on success, otherwise failure; <see cref="GetError()" /> might have more information on the failure.</returns>
    public static int RunApp(int argc, nint argv, SdlMainFunc mainFunction, nint reserved) {
        ArgumentNullException.ThrowIfNull(mainFunction);

        LogDebug(LogCategory.System, "Running SDL application with provided main function.");

        SetMainReady();

        int result = SDL_RunApp(argc, argv, mainFunction, reserved);

        LogDebug(LogCategory.System, $"RunApp completed with result: {result}");

        return result;
    }

    /// <summary>Call a function on the main thread during event processing.</summary>
    /// <param name="callback">the callback to call on the main thread.</param>
    /// <param name="userdata">a pointer that is passed to callback.</param>
    /// <param name="waitComplete"><see langword="true" /> to wait for the callback to complete, <see langword="false" /> to return immediately.</param>
    /// <remarks>
    /// If this is called on the main thread, the callback is executed immediately.
    /// If this is called on another thread, this callback is queued for execution
    /// on the main thread during event processing.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="IsMainThread"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool RunOnMainThread(SdlMainThreadCallback callback, nint userdata, bool waitComplete) {
        if (callback == null) {
            LogError(LogCategory.Error, "RunOnMainThread: Callback is null.");
            return false;
        }
        bool result = SDL_RunOnMainThread(callback, userdata, waitComplete);
        if (!result) {
            LogError(LogCategory.Error, "RunOnMainThread: Failed to run on main thread.");
        }
        return result;
    }

    /// <summary>Save a surface to a file.</summary>
    /// <param name="surface">the <see cref="Surface"/> structure containing the image to be saved.</param>
    /// <param name="file">a file to save to.</param>
    /// <remarks>
    /// Surfaces with a 24-bit, 32-bit and paletted 8-bit format get saved in the
    /// BMP directly. Other RGB formats with 8-bit or higher get converted to a
    /// 24-bit surface or, if they have an alpha mask or a colorkey, to a 32-bit
    /// surface before they are saved. YUV and paletted 1-bit and 4-bit formats are
    /// not supported.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LoadBmp"/>
    /// <seealso cref="SaveBmpIo"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SaveBmp(nint surface, string file) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "SaveBmp: Surface pointer is null.");
            return false;
        }
        if (string.IsNullOrEmpty(file)) {
            LogError(LogCategory.Error, "SaveBmp: File path is null or empty.");
            return false;
        }
        bool result = SDL_SaveBMP(surface, file);
        if (!result) {
            LogError(LogCategory.Error, "SaveBmp: Failed to save BMP file.");
        }
        return result;
    }

    /// <summary>
    /// Save a surface to a seekable SDL data stream in BMP format.
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure containing the image to be saved.</param>
    /// <param name="dst">a data stream to save to.</param>
    /// <param name="closeIo">if <see langword="true"/>, calls <see cref="CloseIo"/> on <paramref name="dst"/> before returning, even in case of an error.</param>
    /// <remarks>
    /// Surfaces with a 24-bit, 32-bit and paletted 8-bit format get saved in the BMP directly. 
    /// Other RGB formats with 8-bit or higher get converted to a 24-bit surface or, if they have an alpha mask or a color key, to a 32-bit surface before they are saved. 
    /// YUV and paletted 1-bit and 4-bit formats are not supported.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LoadBmpIo(System.IntPtr,bool)"/>
    /// <seealso cref="SaveBmp"/>
    /// </remarks>
    /// <returns></returns>
    public static bool SaveBmpIo(nint surface, nint dst, bool closeIo) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "SaveBmpIp: Surface pointer is null.");
            return false;
        }
        if (dst == nint.Zero) {
            LogError(LogCategory.Error, "SaveBmpIp: Destination pointer is null.");
            return false;
        }
        bool result = SDL_SaveBMP_IO(surface, dst, closeIo);
        if (!result) {
            LogError(LogCategory.Error, "SaveBmpIp: Failed to save BMP to IO destination.");
        }
        return result;
    }

    /// <summary>Creates a new surface identical to the existing surface, scaled to the desired size.</summary>
    /// <param name="surface">the surface to duplicate and scale.</param>
    /// <param name="width">the width of the new surface.</param>
    /// <param name="height">the height of the new surface.</param>
    /// <param name="scaleMode">the <see cref="ScaleMode"/> to be used.</param>
    /// <remarks>
    /// The returned surface should be freed with
    /// <see cref="DestroySurface"/>.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="DestroySurface"/>
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
        nint scaledSurface = SDL_ScaleSurface(surface, width, height, scaleMode);
        if (scaledSurface == nint.Zero) {
            LogError(LogCategory.Error, "ScaleSurface: Failed to scale surface.");
        }
        return scaledSurface;
    }

    /// <summary>Creates a new surface identical to the existing surface, scaled to the desired size.</summary>
    /// <param name="surface">the surface to duplicate and scale.</param>
    /// <param name="width">the width of the new surface.</param>
    /// <param name="height">the height of the new surface.</param>
    /// <param name="scaleMode">the <see cref="ScaleMode"/> to be used.</param>
    /// <remarks>
    /// <para>The returned surface should be freed with <see cref="DestroySurface"/>.</para>
    /// <para>The referenced surface is immediately destroyed</para>
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="DestroySurface"/>
    /// </remarks>
    /// <returns>(SDL_Surface *) Returns a copy of the surface or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static Surface ScaleSurface(ref Surface surface, int width, int height, ScaleMode scaleMode) {
        nint oSurface = StructureToPointer(ref surface);
        nint newSurface = ScaleSurface(oSurface, width, height, scaleMode);
        var rSurface = PointerToStructure<Surface>(newSurface);
        DestroySurface(oSurface);
        return rSurface;
    }

    /// <summary>Check whether the screen keyboard is shown for given window.</summary>
    /// <param name="window">the window for which screen keyboard should be queried.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="HasScreenKeyboardSupport"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if screen keyboard is shown or <see langword="false" /> if not.</returns>
    public static bool ScreenKeyboardShown(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "ScreenKeyboardShown: Window pointer is null.");
            return false;
        }
        bool result = SDL_ScreenKeyboardShown(window);
        if (!result) {
            LogError(LogCategory.Error, "ScreenKeyboardShown: Failed to check screen keyboard visibility.");
        }
        return result;
    }

    /// <summary>Check whether the screensaver is currently enabled.</summary>
    /// <remarks>
    /// The screensaver is disabled by default.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="DisableScreenSaver"/>
    /// <seealso cref="EnableScreenSaver"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the screensaver is enabled, <see langword="false" /> if it is disabled.</returns>
    public static bool ScreenSaverEnabled() {
        bool result = SDL_ScreenSaverEnabled();
        if (!result) {
            LogError(LogCategory.Error, "ScreenSaverEnabled: Failed to check screen saver status.");
        }
        return result;
    }

    /// <summary>Specify basic metadata about your app.</summary>
    /// <param name="appname">The name of the application (&quot;My Game 2: Bad Guy's Revenge!&quot;).</param>
    /// <param name="appversion">The version of the application (&quot;1.0.0beta5&quot; or a git hash, or whatever makes sense).</param>
    /// <param name="appidentifier">A unique string in reverse-domain format that identifies this app (&quot;com.example.mygame2&quot;).</param>
    /// <remarks>
    /// You can optionally provide metadata about your app to SDL. This is not
    /// required, but strongly encouraged.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAppMetadataProperty"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetAppMetadata(string appname, string appversion, string appidentifier) {
        if (string.IsNullOrEmpty(appname) || string.IsNullOrEmpty(appversion) || string.IsNullOrEmpty(appidentifier)) {
            LogError(LogCategory.Error, "SetAppMetadata: App metadata is null or empty.");
            return false;
        }
        bool result = SDL_SetAppMetadata(appname, appversion, appidentifier);
        if (!result) {
            LogError(LogCategory.Error, "SetAppMetadata: Failed to set app metadata.");
        }
        return result;
    }

    /// <summary>Specify metadata about your app through a set of properties.</summary>
    /// <param name="name">the name of the metadata property to set.</param>
    /// <param name="value">the value of the property, or <see langword="null" /> to remove that property.</param>
    /// <remarks>
    /// You can optionally provide metadata about your app to SDL. This is not
    /// required, but strongly encouraged.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAppMetadataProperty"/>
    /// <seealso cref="SetAppMetadata"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetAppMetadataProperty(string name, string value) {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value)) {
            LogError(LogCategory.Error, "SetAppMetadataProperty: Name or value is null or empty.");
            return false;
        }
        bool result = SDL_SetAppMetadataProperty(name, value);
        if (!result) {
            LogError(LogCategory.Error, "SetAppMetadataProperty: Failed to set app metadata property.");
        }
        return result;
    }

    /// <summary>Set a boolean property in a group of properties.</summary>
    /// <param name="props">the properties to modify.</param>
    /// <param name="name">the name of the property to modify.</param>
    /// <param name="value">the new value of the property.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetBooleanProperty"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetBooleanProperty(uint props, string name, bool value) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            LogError(LogCategory.Error, "SetBooleanProperty: Properties are zero or name is null/empty.");
            return false;
        }
        bool result = SDL_SetBooleanProperty(props, name, value);
        if (!result) {
            LogError(LogCategory.Error, "SetBooleanProperty: Failed to set boolean property.");
        }
        return result;
    }

    /// <summary>Offer clipboard data to the OS.</summary>
    /// <param name="callback">a function pointer to the function that provides the clipboard data.</param>
    /// <param name="cleanup">a function pointer to the function that cleans up the clipboard data.</param>
    /// <param name="userdata">an opaque pointer that will be forwarded to the callbacks.</param>
    /// <param name="mimeTypes">a list of mime-types that are being offered.</param>
    /// <param name="numMimeTypes">the number of mime-types in the mime_types list.</param>
    /// <remarks>
    /// Tell the operating system that the application is offering clipboard data
    /// for each of the provided mime-types. Once another application requests the
    /// data the callback function will be called, allowing it to generate and
    /// respond with the data for the requested mime-type.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ClearClipboardData"/>
    /// <seealso cref="GetClipboardData"/>
    /// <seealso cref="HasClipboardData"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetClipboardData(SdlClipboardDataCallback callback,
                SdlClipboardCleanupCallback cleanup, nint userdata, nint mimeTypes, nuint numMimeTypes) {
        if (callback == null || cleanup == null || userdata == nint.Zero || mimeTypes == nint.Zero) {
            LogError(LogCategory.Error, "SetClipboardData: Invalid parameters.");
            return false;
        }
        bool result = SDL_SetClipboardData(callback, cleanup, userdata, mimeTypes, numMimeTypes);
        if (!result) {
            LogError(LogCategory.Error, "SetClipboardData: Failed to set clipboard data.");
        }
        return result;
    }

    /// <summary>Put UTF-8 text into the clipboard.</summary>
    /// <param name="text">the text to store in the clipboard.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetClipboardText"/>
    /// <seealso cref="HasClipboardText"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetClipboardText(string text) {
        if (string.IsNullOrEmpty(text)) {
            LogError(LogCategory.Error, "SetClipboardText: Text is null or empty.");
            return false;
        }
        bool result = SDL_SetClipboardText(text);
        if (!result) {
            LogError(LogCategory.Error, "SetClipboardText: Failed to set clipboard text.");
        }
        return result;
    }

    /// <summary>Set the priority for the current thread.</summary>
    /// <param name="priority">the SDL_ThreadPriority to set.</param>
    /// <remarks>
    /// Note that some platforms will not let you alter the priority (or at least,
    /// promote the thread to a higher priority) at all, and some require you to be
    /// an administrator account. Be prepared for this to fail.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetCurrentThreadPriority(SharpSDL3.Enums.ThreadPriority priority) {
        if (!Enum.IsDefined(priority)) {
            LogError(LogCategory.Error, "SetCurrentThreadPriority: Invalid thread priority.");
            return false;
        }
        bool result = SDL_SetCurrentThreadPriority(priority);
        if (!result) {
            LogError(LogCategory.Error, "SetCurrentThreadPriority: Failed to set thread priority.");
        }
        return result;
    }

    /// <summary>Set the SDL error message for the current thread.</summary>
    /// <param name="fmt">a printf()-style message format string.</param>
    /// <param name="args">additional parameters matching % tokens in the fmt string, if any.</param>
    /// <remarks>
    /// Calling this function will replace any previous error message that was set.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ClearError"/>
    /// <seealso cref="GetError"/>
    /// </remarks>
    /// <returns>Returns <see langword="false" />.</returns>
    public static bool SetError(string fmt, params object[] args) {
        if (string.IsNullOrEmpty(fmt)) {
            LogWarn(LogCategory.System, "SetError: Format string is null or empty.");
            return false;
        }

        string formatted = args.Length > 0 ? string.Format(fmt, args) : fmt;
        return SDL_SetError(formatted);
    }

    /// <summary>Set a floating point property in a group of properties.</summary>
    /// <param name="props">the properties to modify.</param>
    /// <param name="name">the name of the property to modify.</param>
    /// <param name="value">the new value of the property.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetFloatProperty"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetFloatProperty(uint props, string name, float value) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            LogError(LogCategory.Error, "SetFloatProperty: Properties are zero or name is null/empty.");
            return false;
        }
        bool result = SDL_SetFloatProperty(props, name, value);
        if (!result) {
            LogError(LogCategory.Error, "SetFloatProperty: Failed to set float property.");
        }
        return result;
    }

    // #TODO: Add documentation for Sdl.SetHint
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool SetHint(string name, string value) {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value)) {
            LogError(LogCategory.Error, "SetHint: Name or value is null or empty.");
            return false;
        }
        bool result = SDL_SetHint(name, value);
        if (!result) {
            LogError(LogCategory.Error, "SetHint: Failed to set hint.");
        }
        return result;
    }

    /// <summary>Set a hint with a specific priority.</summary>
    /// <param name="name">the hint to set.</param>
    /// <param name="value">the value of the hint variable.</param>
    /// <param name="priority">the SDL_HintPriority level for the hint.</param>
    /// <remarks>
    /// The priority controls the behavior when setting a hint that already has a
    /// value. Hints will replace existing hints of their priority and lower.
    /// Environment variables are considered to have override priority.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetHint"/>
    /// <seealso cref="ResetHint"/>
    /// <seealso cref="SetHint"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetHintWithPriority(string name, string value, HintPriority priority) {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value)) {
            LogError(LogCategory.Error, "SetHintWithPriority: Name or value is null or empty.");
            return false;
        }
        bool result = SDL_SetHintWithPriority(name, value, priority);
        if (!result) {
            LogError(LogCategory.Error, "SetHintWithPriority: Failed to set hint with priority.");
        }
        return result;
    }

    /// <summary>Circumvent failure of <see cref="Init"/> when not using SDL_main() as an entry point.</summary>
    /// <remarks>
    /// This function is defined in SDL_main.h, along with the
    /// preprocessor rule to redefine main() as SDL_main(). Thus to
    /// ensure that your main() function will not be changed it is necessary to
    /// define SDL_MAIN_HANDLED before including SDL.h.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="Init"/>
    /// </remarks>
    public static void SetMainReady() {
        SDL_SetMainReady();
    }

    /// <summary>Set the current key modifier state for the keyboard.</summary>
    /// <param name="modstate">the desired <see cref="KeyMod"/> for the keyboard.</param>
    /// <remarks>
    /// The inverse of <see cref="GetModState"/>,
    /// <see cref="SetModState"/> allows you to impose modifier key
    /// states on your application. Simply pass your desired modifier states into
    /// <paramref name="modstate"/>. This value may be a bitwise, OR'd combination of
    /// <see cref="KeyMod"/> values.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetModState"/>
    /// </remarks>
    public static void SetModState(KeyMod modstate) {
        if (!Enum.IsDefined(modstate)) {
            LogError(LogCategory.Error, "SetModState: Invalid key modifier state.");
            return;
        }
        SDL_SetModState(modstate);
    }

    /// <summary>Set an integer property in a group of properties.</summary>
    /// <param name="props">the properties to modify.</param>
    /// <param name="name">the name of the property to modify.</param>
    /// <param name="value">the new value of the property.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetNumberProperty"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetNumberProperty(uint props, string name, long value) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            LogError(LogCategory.Error, "SetNumberProperty: Properties are zero or name is null/empty.");
            return false;
        }
        bool result = SDL_SetNumberProperty(props, name, value);
        if (!result) {
            LogError(LogCategory.Error, "SetNumberProperty: Failed to set number property.");
        }
        return result;
    }

    /// <summary>Set a range of colors in a palette.</summary>
    /// <param name="palette">the SDL_Palette structure to modify.</param>
    /// <param name="colors">an array of SDL_Color structures to copy into the palette.</param>
    /// <param name="firstcolor">the index of the first palette entry to modify.</param>
    /// <param name="ncolors">the number of entries to modify.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, as long as the palette is not modified or destroyed in another thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetPaletteColors(nint palette, Span<Color> colors, int firstcolor, int ncolors) {
        if (palette == nint.Zero) {
            LogError(LogCategory.Error, "SetPaletteColors: Palette pointer is null.");
            return false;
        }
        if (firstcolor < 0 || ncolors <= 0) {
            LogError(LogCategory.Error, "SetPaletteColors: Invalid first color or number of colors.");
            return false;
        }
        bool result = SDL_SetPaletteColors(palette, colors, firstcolor, ncolors);
        if (!result) {
            LogError(LogCategory.Error, "SetPaletteColors: Failed to set palette colors.");
        }
        return result;
    }

    /// <summary>Set a pointer property in a group of properties.</summary>
    /// <param name="props">the properties to modify.</param>
    /// <param name="name">the name of the property to modify.</param>
    /// <param name="value">the new value of the property, or <see langword="null" /> to delete the property.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetPointerProperty"/>
    /// <seealso cref="HasProperty"/>
    /// <seealso cref="SetBooleanProperty"/>
    /// <seealso cref="SetFloatProperty"/>
    /// <seealso cref="SetNumberProperty"/>
    /// <seealso cref="SetPointerPropertyWithCleanup"/>
    /// <seealso cref="SetStringProperty"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetPointerProperty(uint props, string name, nint value) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            LogError(LogCategory.Error, "SetPointerProperty: Properties are zero or name is null/empty.");
            return false;
        }
        bool result = SDL_SetPointerProperty(props, name, value);
        if (!result) {
            LogError(LogCategory.Error, "SetPointerProperty: Failed to set pointer property.");
        }
        return result;
    }

    /// <summary>Set a pointer property in a group of properties with a cleanup function that is called when the property is deleted.</summary>
    /// <param name="props">the properties to modify.</param>
    /// <param name="name">the name of the property to modify.</param>
    /// <param name="value">the new value of the property, or <see langword="null" /> to delete the property.</param>
    /// <param name="cleanup">the function to call when this property is deleted, or <see langword="null" /> if no cleanup is necessary.</param>
    /// <param name="userdata">a pointer that is passed to the cleanup function.</param>
    /// <remarks>
    /// The cleanup function is also called if setting the property fails for any
    /// reason.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetPointerProperty"/>
    /// <seealso cref="SetPointerProperty"/>
    /// <seealso cref="CleanupPropertyCallback"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetPointerPropertyWithCleanup(uint props, string name, nint value,
            SdlCleanupPropertyCallback cleanup, nint userdata) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            LogError(LogCategory.Error, "SetPointerPropertyWithCleanup: Properties are zero or name is null/empty.");
            return false;
        }
        bool result = SDL_SetPointerPropertyWithCleanup(props, name, value, cleanup, userdata);
        if (!result) {
            LogError(LogCategory.Error, "SetPointerPropertyWithCleanup: Failed to set pointer property with cleanup.");
        }
        return result;
    }

    /// <summary>Put UTF-8 text into the primary selection.</summary>
    /// <param name="text">the text to store in the primary selection.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetPrimarySelectionText"/>
    /// <seealso cref="HasPrimarySelectionText"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetPrimarySelectionText(string text) {
        if (string.IsNullOrEmpty(text)) {
            LogError(LogCategory.Error, "SetPrimarySelectionText: Text is null or empty.");
            return false;
        }
        bool result = SDL_SetPrimarySelectionText(text);
        if (!result) {
            LogError(LogCategory.Error, "SetPrimarySelectionText: Failed to set primary selection text.");
        }
        return result;
    }

    /// <summary>Set a human-readable name for a scancode.</summary>
    /// <param name="scanCode">the desired SDL_Scancode.</param>
    /// <param name="name">the name to use for the scancode, encoded as UTF-8. The string is not copied, so the pointer given to this function must stay valid while SDL is being used.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetScancodeName"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetScancodeName(Scancode scanCode, string name) {
        if (string.IsNullOrEmpty(name)) {
            LogError(LogCategory.Error, "SetScancodeName: Name is null or empty.");
            return false;
        }
        bool result = SDL_SetScancodeName(scanCode, name);
        if (!result) {
            LogError(LogCategory.Error, "SetScancodeName: Failed to set scancode name.");
        }
        return result;
    }

    /// <summary>Set a string property in a group of properties.</summary>
    /// <param name="props">the properties to modify.</param>
    /// <param name="name">the name of the property to modify.</param>
    /// <param name="value">the new value of the property, or <see langword="null" /> to delete the property.</param>
    /// <remarks>
    /// This function makes a copy of the string; the caller does not have to
    /// preserve the data after this call completes.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetStringProperty"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetStringProperty(uint props, string name, string value) {
        if (props == 0 || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value)) {
            LogError(LogCategory.Error, "SetStringProperty: Properties are zero or name/value is null/empty.");
            return false;
        }
        bool result = SDL_SetStringProperty(props, name, value);
        if (!result) {
            LogError(LogCategory.Error, "SetStringProperty: Failed to set string property.");
        }
        return result;
    }

    /// <summary>Set an additional alpha value used in blit operations.</summary>
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <param name="alpha">the alpha value multiplied into blit operations.</param>
    /// <remarks>
    /// When this surface is blitted, during the blit operation the source alpha
    /// value is modulated by this alpha value according to the following formula:
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetSurfaceAlphaMod"/>
    /// <seealso cref="SetSurfaceColorMod"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
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
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <param name="blendMode">the <see cref="BlendMode"/> to use for blit blending.</param>
    /// <remarks>
    /// To copy a surface to another surface (or texture) without blending with the
    /// existing data, the blendmode of the SOURCE surface should be set to
    /// SDL_BLENDMODE_NONE.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetSurfaceBlendMode"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
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
    /// <param name="surface">the <see cref="Surface"/> structure to be clipped.</param>
    /// <param name="rect">the <see cref="Rect"/> structure representing the clipping rectangle, or <see langword="null" /> to disable clipping.</param>
    /// <remarks>
    /// When surface is the destination of a blit, only the area within the clip
    /// rectangle is drawn into.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetSurfaceClipRect"/>
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
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <param name="enabled"><see langword="true" /> to enable color key, <see langword="false" /> to disable color key.</param>
    /// <param name="key">the transparent pixel.</param>
    /// <remarks>
    /// The color key defines a pixel value that will be treated as transparent in
    /// a blit. For example, one can use this to specify that cyan pixels should be
    /// considered transparent, and therefore not rendered.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetSurfaceColorKey"/>
    /// <seealso cref="SetSurfaceRle"/>
    /// <seealso cref="SurfaceHasColorKey"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
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
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <param name="r">the red color value multiplied into blit operations.</param>
    /// <param name="g">the green color value multiplied into blit operations.</param>
    /// <param name="b">the blue color value multiplied into blit operations.</param>
    /// <remarks>
    /// When this surface is blitted, during the blit operation each source color
    /// channel is modulated by the appropriate color value according to the
    /// following formula:
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetSurfaceColorMod"/>
    /// <seealso cref="SetSurfaceAlphaMod"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
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
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <param name="r">the red color value multiplied into blit operations.</param>
    /// <param name="g">the green color value multiplied into blit operations.</param>
    /// <param name="b">the blue color value multiplied into blit operations.</param>
    /// <remarks>
    /// When this surface is blitted, during the blit operation each source color
    /// channel is modulated by the appropriate color value according to the
    /// following formula:
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetSurfaceColorMod"/>
    /// <seealso cref="SetSurfaceAlphaMod"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
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
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <param name="colorspace">an <see cref="Colorspace"/> value describing the surface colorspace.</param>
    /// <remarks>
    /// Setting the colorspace doesn't change the pixels, only how they are
    /// interpreted in color operations.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetSurfaceColorspace"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
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
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <param name="palette">the SDL_Palette structure to use.</param>
    /// <remarks>
    /// A single palette can be shared with many surfaces.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreatePalette"/>
    /// <seealso cref="GetSurfacePalette"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
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
    /// <param name="surface">the <see cref="Surface"/> structure to optimize.</param>
    /// <param name="enabled"><see langword="true" /> to enable RLE acceleration, <see langword="false" /> to disable it.</param>
    /// <remarks>
    /// If RLE is enabled, color key and alpha blending blits are much faster, but
    /// the surface must be locked before directly accessing the pixels.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BlitSurface"/>
    /// <seealso cref="LockSurface"/>
    /// <seealso cref="UnlockSurface"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
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

    /// <summary>Set the area used to type Unicode text input.</summary>
    /// <param name="window">the window for which to set the text input area.</param>
    /// <param name="rect">the <see cref="Rect"/> representing the text input area, in window coordinates, or <see langword="null" /> to clear it.</param>
    /// <param name="cursor">the offset of the current cursor location relative to rect-&gt;x, in window coordinates.</param>
    /// <remarks>
    /// Native input methods may place a window with word suggestions near the
    /// cursor, without covering the text being entered.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetTextInputArea"/>
    /// <seealso cref="StartTextInput"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetTextInputArea(nint window, ref Rect rect, int cursor) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetTextInputArea: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetTextInputArea(window, ref rect, cursor);
        if (!result) {
            LogError(LogCategory.Error, "SetTextInputArea: Failed to set text input area.");
        }
        return result;
    }

    /// <summary>Set the current thread's value associated with a thread local storage ID.</summary>
    /// <param name="id">a pointer to the thread local storage ID, may not be <see langword="null" />.</param>
    /// <param name="value">the value to associate with the ID for the current thread.</param>
    /// <param name="destructor">a function called when the thread exits, to free the value, may be discarded.</param>
    /// <remarks>
    /// If the thread local storage ID is not initialized (the value is 0), a new
    /// ID will be created in a thread-safe way, so all calls using a pointer to
    /// the same ID will refer to the same local storage.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetTls"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetTls(nint id, nint value, SdlTlsDestructorCallback destructor) {
        if (id == nint.Zero || value == nint.Zero) {
            LogError(LogCategory.Error, "SetTls: ID or value is null.");
            return false;
        }
        bool result = SDL_SetTLS(id, value, destructor);
        if (!result) {
            LogError(LogCategory.Error, "SetTls: Failed to set TLS.");
        }
        return result;
    }

    /// <summary>Set the window to always be above the others.</summary>
    /// <param name="window">the window of which to change the always on top state.</param>
    /// <param name="onTop"><see langword="true" /> to set the window always on top, <see langword="false" /> to disable.</param>
    /// <remarks>
    /// This will add or remove the window's
    /// SDL_WINDOW_ALWAYS_ON_TOP flag. This will
    /// bring the window to the front and keep the window above the rest.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowFlags"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetWindowAlwaysOnTop(nint window, bool onTop) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowAlwaysOnTop: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowAlwaysOnTop(window, onTop);
        if (!result) {
            LogError(LogCategory.Error, "SetWindowAlwaysOnTop: Failed to set window always on top.");
        }
        return result;
    }

    /// <summary>Request that the aspect ratio of a window's client area be set.</summary>
    /// <param name="window">the window to change.</param>
    /// <param name="min_aspect">the minimum aspect ratio of the window, or 0.0f for no limit.</param>
    /// <param name="max_aspect">the maximum aspect ratio of the window, or 0.0f for no limit.</param>
    /// <remarks>
    /// The aspect ratio is the ratio of width divided by height, e.g. 2560x1600
    /// would be 1.6. Larger aspect ratios are wider and smaller aspect ratios are
    /// narrower.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowAspectRatio"/>
    /// <seealso cref="SyncWindow"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetWindowAspectRatio(nint window, float minAspect, float maxAspect) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowAspectRatio: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowAspectRatio(window, minAspect, maxAspect);
        if (!result) {
            LogError(LogCategory.Error, "SetWindowAspectRatio: Failed to set window aspect ratio.");
        }
        return result;
    }

    /// <summary>Set the border state of a window.</summary>
    /// <param name="window">the window of which to change the border state.</param>
    /// <param name="bordered"><see langword="false" /> to remove border, <see langword="true" /> to add border.</param>
    /// <remarks>
    /// This will add or remove the window's
    /// SDL_WINDOW_BORDERLESS flag and add or remove the
    /// border from the actual window. This is a no-op if the window's border
    /// already matches the requested state.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowFlags"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetWindowBordered(nint window, bool bordered) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowBordered: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowBordered(window, bordered);
        if (!result) {
            LogError(LogCategory.Error, "SetWindowBordered: Failed to set window bordered.");
        }
        return result;
    }

    /// <summary>Set whether the window may have input focus.</summary>
    /// <param name="window">the window to set focusable state.</param>
    /// <param name="focusable"><see langword="true" /> to allow input focus, <see langword="false" /> to not allow input focus.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetWindowFocusable(nint window, bool focusable) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowFocusable: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowFocusable(window, focusable);
        if (!result) {
            LogError(LogCategory.Error, "SetWindowFocusable: Failed to set window focusable.");
        }
        return result;
    }

    /// <summary>Request that the window's fullscreen state be changed.</summary>
    /// <param name="window">the window to change.</param>
    /// <param name="fullscreen"><see langword="true" /> for fullscreen mode, <see langword="false" /> for windowed mode.</param>
    /// <remarks>
    /// By default a window in fullscreen state uses borderless fullscreen desktop
    /// mode, but a specific exclusive display mode can be set using
    /// SDL_SetWindowFullscreenMode().
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowFullscreenMode"/>
    /// <seealso cref="SetWindowFullscreenMode"/>
    /// <seealso cref="SyncWindow"/>
    /// <seealso cref="WINDOW_FULLSCREEN"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetWindowFullscreen(nint window, bool fullscreen) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowFullscreen: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowFullscreen(window, fullscreen);
        if (!result) {
            LogError(LogCategory.Error, "SetWindowFullscreen: Failed to set window fullscreen.");
        }
        return result;
    }

    /// <summary>Set the display mode to use when a window is visible and fullscreen.</summary>
    /// <param name="window">the window to affect.</param>
    /// <param name="mode">a pointer to the display mode to use, which can be <see langword="null" /> for borderless fullscreen desktop mode, or one of the fullscreen modes returned by SDL_GetFullscreenDisplayModes() to set an exclusive fullscreen mode.</param>
    /// <remarks>
    /// This only affects the display mode used when the window is fullscreen. To
    /// change the window size when the window is not fullscreen, use <seealso cref="SetWindowSize"/>.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowFullscreenMode"/>
    /// <seealso cref="SetWindowFullscreen"/>
    /// <seealso cref="SyncWindow"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetWindowFullscreenMode(nint window, ref DisplayMode mode) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowFullscreenMode: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowFullscreenMode(window, ref mode);
        if (!result) {
            LogError(LogCategory.Error, "SetWindowFullscreenMode: Failed to set window fullscreen mode.");
        }
        return result;
    }

    /// <summary>Provide a callback that decides if a window region has special properties.</summary>
    /// <param name="window">the window to set hit-testing on.</param>
    /// <param name="callback">the function to call when doing a hit-test.</param>
    /// <param name="callbackData">an app-defined void pointer passed to callback.</param>
    /// <remarks>
    /// Normally windows are dragged and resized by decorations provided by the
    /// system window manager (a title bar, borders, etc), but for some apps, it
    /// makes sense to drag them from somewhere else inside the window itself; for
    /// example, one might have a borderless window that wants to be draggable from
    /// any part, or simulate its own title bar, etc.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetWindowHitTest(nint window, SdlHitTest callback, nint callbackData) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowHitTest: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowHitTest(window, callback, callbackData);
        if (!result) {
            LogError(LogCategory.Error, "SetWindowHitTest: Failed to set window hit test.");
        }
        return result;
    }

    /// <summary>Set the icon for a window.</summary>
    /// <param name="window">the window to change.</param>
    /// <param name="icon">an SDL_Surface structure containing the icon for the window.</param>
    /// <remarks>
    /// <para>If this function is passed a surface with alternate representations, the
    /// surface will be interpreted as the content to be used for 100% display
    /// scale, and the alternate representations will be used for high DPI
    /// situations.</para>
    /// <para>For example, if the original surface is 32x32, then on a 2x
    /// macOS display or 200% display scale on Windows, a 64x64 version of the
    /// image will be used, if available.</para>
    /// <para>If a matching version of the image isn't
    /// available, the closest larger size image will be downscaled to the
    /// appropriate size and be used instead, if available. Otherwise, the closest
    /// smaller image will be upscaled and be used instead.</para>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetWindowIcon(nint window, nint icon) {
        // Impement an overloaded function that acceps an Icon from LoadIcon
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowIcon: Window pointer is null.");
            return false;
        }
        if (icon == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowIcon: Icon pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowIcon(window, icon);
        if (!result) {
            LogError(LogCategory.Error, "SetWindowIcon: Failed to set window icon.");
        }
        return result;
    }

    /// <summary>Set a window's keyboard grab mode.</summary>
    /// <param name="window">the window for which the keyboard grab mode should be set.</param>
    /// <param name="grabbed">this is <see langword="true" /> to grab keyboard, and <see langword="false" /> to release.</param>
    /// <remarks>
    /// Keyboard grab enables capture of system keyboard shortcuts like Alt+Tab or
    /// the Meta/Super key. Note that not all system keyboard shortcuts can be
    /// captured by applications (one example is Ctrl+Alt+Del on Windows).
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowKeyboardGrab"/>
    /// <seealso cref="SetWindowMouseGrab"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetWindowKeyboardGrab(nint window, bool grabbed) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowKeyboardGrab: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowKeyboardGrab(window, grabbed);
        if (!result) {
            LogError(LogCategory.Error, "SetWindowKeyboardGrab: Failed to set window keyboard grab.");
        }
        return result;
    }

    /// <summary>Set the maximum size of a window's client area.</summary>
    /// <param name="window">the window to change.</param>
    /// <param name="maxW">the maximum width of the window, or 0 for no limit.</param>
    /// <param name="maxH">the maximum height of the window, or 0 for no limit.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowMaximumSize"/>
    /// <seealso cref="SetWindowMinimumSize"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetWindowMaximumSize(nint window, int maxW, int maxH) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowMaximumSize: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowMaximumSize(window, maxW, maxH);
        if (!result) {
            LogError(LogCategory.Error, "SetWindowMaximumSize: Failed to set window maximum size.");
        }
        return result;
    }

    /// <summary>Set the minimum size of a window's client area.</summary>
    /// <param name="window">the window to change.</param>
    /// <param name="minW">the minimum width of the window, or 0 for no limit.</param>
    /// <param name="minH">the minimum height of the window, or 0 for no limit.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowMinimumSize"/>
    /// <seealso cref="SetWindowMaximumSize"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetWindowMinimumSize(nint window, int minW, int minH) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowMinimumSize: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowMinimumSize(window, minW, minH);
        if (!result) {
            LogError(LogCategory.Error, "SetWindowMinimumSize: Failed to set window minimum size.");
        }
        return result;
    }

    /// <summary>Toggle the state of the window as modal.</summary>
    /// <param name="window">the window on which to set the modal state.</param>
    /// <param name="modal"><see langword="true" /> to toggle modal status on, <see langword="false" /> to toggle it off.</param>
    /// <remarks>
    /// To enable modal status on a window, the window must currently be the child
    /// window of a parent, or toggling modal status on will fail.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetWindowParent"/>
    /// <seealso cref="WINDOW_MODAL"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetWindowModal(nint window, bool modal) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowModal: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowModal(window, modal);
        if (!result) {
            LogError(LogCategory.Error, "SetWindowModal: Failed to set window modal.");
        }
        return result;
    }

    /// <summary>Set a window's mouse grab mode.</summary>
    /// <param name="window">the window for which the mouse grab mode should be set.</param>
    /// <param name="grabbed">this is <see langword="true" /> to grab mouse, and <see langword="false" /> to release.</param>
    /// <remarks>
    /// Mouse grab confines the mouse cursor to the window.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowMouseRect"/>
    /// <seealso cref="SetWindowMouseRect"/>
    /// <seealso cref="SetWindowKeyboardGrab"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetWindowMouseGrab(nint window, bool grabbed) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowMouseGrab: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowMouseGrab(window, grabbed);
        if (!result) {
            LogError(LogCategory.Error, "SetWindowMouseGrab: Failed to set window mouse grab.");
        }
        return result;
    }

    /// <summary>Confines the cursor to the specified area of a window.</summary>
    /// <param name="window">the window that will be associated with the barrier.</param>
    /// <param name="rect">a rectangle area in window-relative coordinates. If <see langword="null" /> the barrier for the specified window will be destroyed.</param>
    /// <remarks>
    /// Note that this does NOT grab the cursor, it only defines the area a cursor
    /// is restricted to when the window has mouse focus.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowMouseRect"/>
    /// <seealso cref="GetWindowMouseGrab"/>
    /// <seealso cref="SetWindowMouseGrab"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetWindowMouseRect(nint window, ref Rect rect) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowMouseRect: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowMouseRect(window, ref rect);
        if (!result) {
            LogError(LogCategory.Error, "SetWindowMouseRect: Failed to set window mouse rect.");
        }
        return result;
    }

    /// <summary>Set the opacity for a window.</summary>
    /// <param name="window">the window which will be made transparent or opaque.</param>
    /// <param name="opacity">the opacity value (0.0f - transparent, 1.0f - opaque).</param>
    /// <remarks>
    /// The parameter opacity will be clamped internally between 0.0f
    /// (transparent) and 1.0f (opaque).
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowOpacity"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetWindowOpacity(nint window, float opacity) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowOpacity: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowOpacity(window, opacity);
        if (!result) {
            LogError(LogCategory.Error, "SetWindowOpacity: Failed to set window opacity.");
        }
        return result;
    }

    /// <summary>Set the window as a child of a parent window.</summary>
    /// <param name="window">the window that should become the child of a parent.</param>
    /// <param name="parent">the new parent window for the child window.</param>
    /// <remarks>
    /// If the window is already the child of an existing window, it will be
    /// reparented to the new owner. Setting the parent window to <see langword="null" /> unparents
    /// the window and removes child window status.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetWindowModal"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetWindowParent(nint window, nint parent) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowParent: Window pointer is null.");
            return false;
        }
        if (parent == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowParent: Parent pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowParent(window, parent);
        if (!result) {
            LogError(LogCategory.Error, "SetWindowParent: Failed to set window parent.");
        }
        return result;
    }

    /// <summary>Request that the window's position be set.</summary>
    /// <param name="window">the window to reposition.</param>
    /// <param name="x">the x coordinate of the window, or SDL_WINDOWPOS_CENTERED or SDL_WINDOWPOS_UNDEFINED.</param>
    /// <param name="y">the y coordinate of the window, or SDL_WINDOWPOS_CENTERED or SDL_WINDOWPOS_UNDEFINED.</param>
    /// <remarks>
    /// If the window is in an exclusive fullscreen or maximized state, this
    /// request has no effect.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowPosition"/>
    /// <seealso cref="SyncWindow"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetWindowPosition(nint window, int x, int y) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowPosition: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowPosition(window, x, y);
        if (!result) {
            LogError(LogCategory.Error, "SetWindowPosition: Failed to set window position.");
        }
        return result;
    }

    /// <summary>Request that the window's position be set.</summary>
    /// <param name="window">the window to reposition.</param>
    /// <param name="position">the <see cref="Point"/> </param>
    /// <remarks>
    /// If the window is in an exclusive fullscreen or maximized state, this
    /// request has no effect.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowPosition"/>
    /// <seealso cref="SyncWindow"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetWindowPosition(nint window, Point position) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowPosition: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowPosition(window, position.X, position.Y);
        if (!result) {
            LogError(LogCategory.Error, "SetWindowPosition: Failed to set window position.");
        }
        return result;
    }

    /// <summary>Set the user-resizable state of a window.</summary>
    /// <param name="window">the window of which to change the resizable state.</param>
    /// <param name="resizable"><see langword="true" /> to allow resizing, <see langword="false" /> to disallow.</param>
    /// <remarks>
    /// This will add or remove the window's
    /// SDL_WINDOW_RESIZABLE flag and allow/disallow user
    /// resizing of the window. This is a no-op if the window's resizable state
    /// already matches the requested state.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowFlags"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetWindowResizable(nint window, bool resizable) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowResizable: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowResizable(window, resizable);
        if (!result) {
            LogError(LogCategory.Error, "SetWindowResizable: Failed to set window resizable.");
        }
        return result;
    }

    /// <summary>Set the shape of a transparent window.</summary>
    /// <param name="window">the window.</param>
    /// <param name="shape">the surface representing the shape of the window, or <see langword="null" /> to remove any current shape.</param>
    /// <remarks>
    /// This sets the alpha channel of a transparent window and any fully
    /// transparent areas are also transparent to mouse clicks. If you are using
    /// something besides the SDL render API, then you are responsible for drawing
    /// the alpha channel of the window to match the shape alpha channel to get
    /// consistent cross-platform results.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetWindowShape(nint window, nint shape) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowShape: Window pointer is null.");
            return false;
        }
        if (shape == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowShape: Shape pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowShape(window, shape);
        if (!result) {
            LogError(LogCategory.Error, "SetWindowShape: Failed to set window shape.");
        }
        return result;
    }

    /// <summary>Request that the size of a window's client area be set.</summary>
    /// <param name="window">the window to change.</param>
    /// <param name="w">the width of the window, must be &gt; 0.</param>
    /// <param name="h">the height of the window, must be &gt; 0.</param>
    /// <remarks>
    /// If the window is in a fullscreen or maximized state, this request has no
    /// effect.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowSize"/>
    /// <seealso cref="SetWindowFullscreenMode"/>
    /// <seealso cref="SyncWindow"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetWindowSize(nint window, int w, int h) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowSize: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowSize(window, w, h);
        if (!result) {
            LogError(LogCategory.Error, "SetWindowSize: Failed to set window size.");
        }
        return result;
    }

    /// <summary>Request that the size of a window's client area be set.</summary>
    /// <param name="window">the window to change.</param>
    /// <param name="rect">the <see cref="Rect"/> with a Width and Height </param>
    /// <remarks>
    /// If the window is in a fullscreen or maximized state, this request has no
    /// effect.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowSize"/>
    /// <seealso cref="SetWindowFullscreenMode"/>
    /// <seealso cref="SyncWindow"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetWindowSize(nint window, Rect rect) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowSize: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowSize(window, rect.W, rect.H);
        if (!result) {
            LogError(LogCategory.Error, "SetWindowSize: Failed to set window size.");
        }
        return result;
    }

    /// <summary>Toggle VSync for the window surface.</summary>
    /// <param name="window">the window.</param>
    /// <param name="vsync">the vertical refresh sync interval.</param>
    /// <remarks>
    /// When a window surface is created, vsync defaults to
    /// SDL_WINDOW_SURFACE_VSYNC_DISABLED.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowSurfaceVSync"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetWindowSurfaceVSync(nint window, int vsync) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowSurfaceVSync: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowSurfaceVSync(window, vsync);
        if (!result) {
            LogError(LogCategory.Error, "SetWindowSurfaceVSync: Failed to set window surface VSync.");
        }
        return result;
    }

    /// <summary>Set the title of a window.</summary>
    /// <param name="window">the window to change.</param>
    /// <param name="title">the desired window title in UTF-8 format.</param>
    /// <remarks>
    /// This string is expected to be in UTF-8 encoding.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowTitle"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool SetWindowTitle(nint window, string title) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SetWindowTitle: Window handle is null.");
            return false;
        }
        bool result = SDL_SetWindowTitle(window, title);
        if (!result) {
            LogWarn(LogCategory.System, "SetWindowTitle: Failed to set window title.");
        }
        return result;
    }

    /// <summary>Show a window.</summary>
    /// <param name="window">the window to show.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="HideWindow"/>
    /// <seealso cref="RaiseWindow"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool ShowWindow(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "ShowWindow: Window pointer is null.");
            return false;
        }
        bool result = SDL_ShowWindow(window);
        if (!result) {
            LogError(LogCategory.Error, "ShowWindow: Failed to show window.");
        }
        return result;
    }

    /// <summary>Display the system-level window menu.</summary>
    /// <param name="window">the window for which the menu will be displayed.</param>
    /// <param name="x">the x coordinate of the menu, relative to the origin (top-left) of the client area.</param>
    /// <param name="y">the y coordinate of the menu, relative to the origin (top-left) of the client area.</param>
    /// <remarks>
    /// This default window menu is provided by the system and on some platforms
    /// provides functionality for setting or changing privileged state on the
    /// window, such as moving it between workspaces or displays, or toggling the
    /// always-on-top property.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool ShowWindowSystemMenu(nint window, int x, int y) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "ShowWindowSystemMenu: Window pointer is null.");
            return false;
        }
        bool result = SDL_ShowWindowSystemMenu(window, x, y);
        if (!result) {
            LogError(LogCategory.Error, "ShowWindowSystemMenu: Failed to show window system menu.");
        }
        return result;
    }

    /// <summary>Display the system-level window menu.</summary>
    /// <param name="window">the window for which the menu will be displayed.</param>
    /// <param name="x">the x coordinate of the menu, relative to the origin (top-left) of the client area.</param>
    /// <param name="y">the y coordinate of the menu, relative to the origin (top-left) of the client area.</param>
    /// <remarks>
    /// This default window menu is provided by the system and on some platforms
    /// provides functionality for setting or changing privileged state on the
    /// window, such as moving it between workspaces or displays, or toggling the
    /// always-on-top property.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool ShowWindowSystemMenu(nint window, Point position) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "ShowWindowSystemMenu: Window pointer is null.");
            return false;
        }
        return ShowWindowSystemMenu(window, position.X, position.Y);
    }

    public static unsafe nuint SizeOf<T>() where T : unmanaged {
        nuint size = (uint)sizeof(T);
        if (size == 0) {
            LogError(LogCategory.Error, "Sizeof: Failed to get size of type.");
        }
        return size;
    }

    /// <summary>Start accepting Unicode text input events in a window.</summary>
    /// <param name="window">the window to enable text input.</param>
    /// <remarks>
    /// This function will enable text input
    /// (SDL_EVENT_TEXT_INPUT and
    /// SDL_EVENT_TEXT_EDITING events) in the specified
    /// window. Please use this function paired with
    /// SDL_StopTextInput().
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetTextInputArea"/>
    /// <seealso cref="StartTextInputWithProperties"/>
    /// <seealso cref="StopTextInput"/>
    /// <seealso cref="TextInputActive"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool StartTextInput(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "StartTextInput: Window pointer is null.");
            return false;
        }
        bool result = SDL_StartTextInput(window);
        if (!result) {
            LogError(LogCategory.Error, "StartTextInput: Failed to start text input.");
        }
        return result;
    }

    /// <summary>Start accepting Unicode text input events in a window, with properties describing the input.</summary>
    /// <param name="window">the window to enable text input.</param>
    /// <param name="props">the properties to use.</param>
    /// <remarks>
    /// This function will enable text input
    /// (SDL_EVENT_TEXT_INPUT and
    /// SDL_EVENT_TEXT_EDITING events) in the specified
    /// window. Please use this function paired with
    /// SDL_StopTextInput().
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetTextInputArea"/>
    /// <seealso cref="StartTextInput"/>
    /// <seealso cref="StopTextInput"/>
    /// <seealso cref="TextInputActive"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool StartTextInputWithProperties(nint window, uint props) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "StartTextInputWithProperties: Window pointer is null.");
            return false;
        }
        bool result = SDL_StartTextInputWithProperties(window, props);
        if (!result) {
            LogError(LogCategory.Error, "StartTextInputWithProperties: Failed to start text input with properties.");
        }
        return result;
    }

    /// <summary>Stop receiving any text input events in a window.</summary>
    /// <param name="window">the window to disable text input.</param>
    /// <remarks>
    /// If SDL_StartTextInput() showed the screen keyboard,
    /// this function will hide it.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="StartTextInput"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool StopTextInput(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "StopTextInput: Window pointer is null.");
            return false;
        }
        bool result = SDL_StopTextInput(window);
        if (!result) {
            LogError(LogCategory.Error, "StopTextInput: Failed to stop text input.");
        }
        return result;
    }

    /// <summary>Convert a GUID string into a <see cref="SdlGuid"/> structure.</summary>
    /// <param name="pchGuid">string containing an ASCII representation of a GUID.</param>
    /// <remarks>
    /// Performs no error checking. If this function is given a string containing
    /// an invalid GUID, the function will silently succeed, but the GUID generated
    /// will not be useful.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GuidToString"/>
    /// </remarks>
    /// <returns>Returns a <see cref="SdlGuid"/> structure.</returns>
    public static SdlGuid StringToGuid(string pchGuid) {
        if (string.IsNullOrEmpty(pchGuid)) {
            LogError(LogCategory.Error, "StringToGUID: GUID string is null or empty.");
            return default;
        }
        SdlGuid result = SDL_StringToGUID(pchGuid);
        if (result.Data == null) {
            LogError(LogCategory.Error, "StringToGUID: Failed to convert string to GUID.");
        }
        return result;
    }

    /// <summary>Return whether a surface has alternate versions available.</summary>
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="AddSurfaceAlternateImage"/>
    /// <seealso cref="RemoveSurfaceAlternateImages"/>
    /// <seealso cref="GetSurfaceImages"/>
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
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <remarks>
    /// It is safe to pass a <see langword="null" /> surface here; it will return false.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetSurfaceColorKey"/>
    /// <seealso cref="GetSurfaceColorKey"/>
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
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <remarks>
    /// It is safe to pass a <see langword="null" /> surface here; it will return false.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetSurfaceRle"/>
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

    /// <summary>Block until any pending window state is finalized.</summary>
    /// <param name="window">the window for which to wait for the pending state to be applied.</param>
    /// <remarks>
    /// On asynchronous windowing systems, this acts as a synchronization barrier
    /// for pending window state. It will attempt to wait until any pending window
    /// state has been applied and is guaranteed to return within finite time. Note
    /// that for how long it can potentially block depends on the underlying window
    /// system, as window state changes may involve somewhat lengthy animations
    /// that must complete before the window is in its final requested state.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetWindowSize"/>
    /// <seealso cref="SetWindowPosition"/>
    /// <seealso cref="SetWindowFullscreen"/>
    /// <seealso cref="MinimizeWindow"/>
    /// <seealso cref="MaximizeWindow"/>
    /// <seealso cref="RestoreWindow"/>
    /// <seealso cref="HINT_VIDEO_SYNC_WINDOW_OPERATIONS"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> if the operation timed out beforethe window was in the requested state.</returns>
    public static bool SyncWindow(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "SyncWindow: Window pointer is null.");
            return false;
        }
        bool result = SDL_SyncWindow(window);
        if (!result) {
            LogError(LogCategory.Error, "SyncWindow: Failed to sync window.");
        }
        return result;
    }

    /// <summary>Check whether or not Unicode text input events are enabled for a window.</summary>
    /// <param name="window">the window to check.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="StartTextInput"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if text input events are enabled else <see langword="false" />.</returns>
    public static bool TextInputActive(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "TextInputActive: Window pointer is null.");
            return false;
        }
        bool result = SDL_TextInputActive(window);
        if (!result) {
            LogError(LogCategory.Error, "TextInputActive: Failed to check text input active.");
        }
        return result;
    }

    /// <summary>Unload a shared object from memory.</summary>
    /// <param name="handle">a valid shared object handle returned by SDL_LoadObject().</param>
    /// <remarks>
    /// Note that any pointers from this object looked up through
    /// SDL_LoadFunction() will no longer be valid.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LoadObject"/>
    /// </remarks>
    public static void UnloadObject(nint handle) {
        if (handle == nint.Zero) {
            LogError(LogCategory.Error, "UnloadObject: Handle pointer is null.");
            return;
        }
        SDL_UnloadObject(handle);
    }

    /// <summary>Unlock a group of properties.</summary>
    /// <param name="props">the properties to unlock.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LockProperties"/>
    /// </remarks>
    public static void UnlockProperties(uint props) {
        if (props == 0) {
            LogError(LogCategory.Error, "UnlockProperties: Properties are zero.");
            return;
        }
        SDL_UnlockProperties(props);
    }

    /// <summary>Release a surface after directly accessing the pixels.</summary>
    /// <param name="surface">the <see cref="Surface"/> structure to be unlocked.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function is not thread safe. The locking referred to by this functionis making the pixels available for direct access, not thread-safe locking.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LockSurface"/>
    /// </remarks>
    public static void UnlockSurface(nint surface) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "UnlockSurface: Surface pointer is null.");
            return;
        }
        SDL_UnlockSurface(surface);
    }

    /// <summary>Copy the window surface to the screen.</summary>
    /// <param name="window">the window to update.</param>
    /// <remarks>
    /// This is the function you use to reflect any changes to the surface on the
    /// screen.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowSurface"/>
    /// <seealso cref="UpdateWindowSurfaceRects"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool UpdateWindowSurface(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "UpdateWindowSurface: Window pointer is null.");
            return false;
        }
        bool result = SDL_UpdateWindowSurface(window);
        if (!result) {
            LogError(LogCategory.Error, "UpdateWindowSurface: Failed to update window surface.");
        }
        return result;
    }

    /// <summary>Copy areas of the window surface to the screen.</summary>
    /// <param name="window">the window to update.</param>
    /// <param name="rects">an array of <see cref="Rect"/> structures representing areas of the surface to copy, in pixels.</param>
    /// <param name="numrects">the number of rectangles.</param>
    /// <remarks>
    /// This is the function you use to reflect changes to portions of the surface
    /// on the screen.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowSurface"/>
    /// <seealso cref="UpdateWindowSurface"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool UpdateWindowSurfaceRects(nint window, Span<Rect> rects, int numrects) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "UpdateWindowSurfaceRects: Window pointer is null.");
            return false;
        }
        if (rects.Length == 0 || numrects <= 0) {
            LogError(LogCategory.Error, "UpdateWindowSurfaceRects: Rectangles are empty or number of rectangles is zero.");
            return false;
        }
        bool result = SDL_UpdateWindowSurfaceRects(window, rects, numrects);
        if (!result) {
            LogError(LogCategory.Error, "UpdateWindowSurfaceRects: Failed to update window surface rectangles.");
        }
        return result;
    }

    /// <summary>Copy areas of the window surface to the screen.</summary>
    /// <param name="window">the window to update.</param>
    /// <param name="rects">an array of <see cref="Rect"/> structures representing areas of the surface to copy, in pixels.</param>
    /// <remarks>
    /// This is the function you use to reflect changes to portions of the surface
    /// on the screen.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowSurface"/>
    /// <seealso cref="UpdateWindowSurface"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool UpdateWindowSurfaceRects(nint window, Rect[] rects) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "UpdateWindowSurfaceRects: Window pointer is null.");
            return false;
        }
        if (rects.Length == 0) {
            LogError(LogCategory.Error, "UpdateWindowSurfaceRects: Rectangles are empty.");
            return false;
        }
        bool result = SDL_UpdateWindowSurfaceRects(window, rects, rects.Length);
        if (!result) {
            LogError(LogCategory.Error, "UpdateWindowSurfaceRects: Failed to update window surface rectangles.");
        }
        return result;
    }

    /// <summary>Wait for a thread to finish.</summary>
    /// <param name="thread">the SDL_Thread pointer that was returned from the SDL_CreateThread() call that started this thread.</param>
    /// <param name="status">a pointer filled in with the value returned from the thread function by its 'return', or -1 if the thread has been detached or isn't valid, may be discarded.</param>
    /// <remarks>
    /// Threads that haven't been detached will remain until this function cleans
    /// them up. Not doing so is a resource leak.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateThread"/>
    /// <seealso cref="DetachThread"/>
    /// </remarks>
    public static void WaitThread(nint thread, nint status) {
        if (thread == nint.Zero) {
            LogError(LogCategory.Error, "WaitThread: Thread pointer is null.");
            return;
        }
        SDL_WaitThread(thread, status);
    }

    /// <summary>
    /// Get a mask of the specified subsystems which are currently initialized.
    /// </summary>
    /// <param name="flags">any of the flags used by <see cref="Init"/>; see <see cref="Init"/> for details.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0</para>
    /// <seealso cref="Init"/>
    /// <seealso cref="InitSubSystem"/>
    /// </remarks>
    /// <returns>Returns a mask of all initialized subsystems if flags is 0, otherise it returns the initialization status of the specified subsystems.</returns>
    public static InitFlags WasInit(InitFlags flags) {
        if (!Enum.IsDefined(flags)) {
            LogError(LogCategory.Error, "WasInit: Flags are not defined.");
            return 0;
        }
        if (flags == 0) {
            LogError(LogCategory.Error, "WasInit: Flags are zero.");
            return 0;
        }
        InitFlags result = SDL_WasInit(flags);
        if (result == 0) {
            LogError(LogCategory.Error, "WasInit: Failed to check SDL initialization.");
        }
        return result;
    }

    /// <summary>Return whether the window has a surface associated with it.</summary>
    /// <param name="window">the window to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowSurface"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if there is a surface associated with the window, or<see langword="false" /> otherwise.</returns>
    public static bool WindowHasSurface(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "WindowHasSurface: Window pointer is null.");
            return false;
        }
        bool result = SDL_WindowHasSurface(window);
        if (!result) {
            LogError(LogCategory.Error, "WindowHasSurface: Failed to check window surface.");
        }
        return result;
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
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
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
    /// <param name="color">the <see cref="Color"/> struct filled with data</param>
    /// <remarks>
    /// This function prioritizes correctness over speed: it is suitable for unit
    /// tests, but is not intended for use in a game engine.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
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
    /// <param name="location">the <see cref="Point"/> struct that provides xy coordinates</param>
    /// <param name="color">the <see cref="Color"/> struct filled with data</param>
    /// <remarks>
    /// This function prioritizes correctness over speed: it is suitable for unit
    /// tests, but is not intended for use in a game engine.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool WriteSurfacePixel(nint surface, Point location, Color color) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "WriteSurfacePixel: Surface pointer is null.");
            return false;
        }
        return WriteSurfacePixel(surface, location.X, location.Y, color.R, color.G, color.B, color.A);
    }

    /// <summary>Writes a single pixel to a surface.</summary>
    /// <param name="surface">the surface to write.</param>
    /// <param name="location">the <see cref="Point"/> struct that provides xy coordinates</param>
    /// <param name="r">the red channel value, 0-255.</param>
    /// <param name="g">the green channel value, 0-255.</param>
    /// <param name="b">the blue channel value, 0-255.</param>
    /// <param name="a">the alpha channel value, 0-255.</param>
    /// <remarks>
    /// This function prioritizes correctness over speed: it is suitable for unit
    /// tests, but is not intended for use in a game engine.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
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
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
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
    /// <param name="location">the <see cref="Point"/> struct that provides xy coordinates</param>
    /// <param name="color">the <see cref="Color"/> struct filled with data</param>
    /// <remarks>
    /// This function prioritizes correctness over speed: it is suitable for unit
    /// tests, but is not intended for use in a game engine.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
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
    /// <param name="location">the <see cref="Point"/> struct that provides xy coordinates</param>
    /// <param name="r">the red channel value, normally in the range 0-1.</param>
    /// <param name="g">the green channel value, normally in the range 0-1.</param>
    /// <param name="b">the blue channel value, normally in the range 0-1.</param>
    /// <param name="a">the alpha channel value, normally in the range 0-1.</param>
    /// <remarks>
    /// This function prioritizes correctness over speed: it is suitable for unit
    /// tests, but is not intended for use in a game engine.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool WriteSurfacePixelFloat(nint surface, Point location, float r, float g, float b, float a) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "WriteSurfacePixelFloat: Window pointer is null.");
            return false;
        }
        return WriteSurfacePixelFloat(surface, location.X, location.Y, r, g, b, a);
    }

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_AddHintCallback(string name, SdlHintCallback callback, nint userdata);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_AddSurfaceAlternateImage(nint surface, nint image);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_BlitSurface(nint src, nint srcrect, nint dst, nint dstrect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_BlitSurface9Grid(nint src, nint srcrect, int leftWidth, int rightWidth,
        int topHeight, int bottomHeight, float scale, ScaleMode scaleMode, nint dst,
        nint dstrect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_BlitSurfaceScaled(nint src, nint srcrect, nint dst, nint dstrect,
        ScaleMode scaleMode);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_BlitSurfaceTiled(nint src, nint srcrect, nint dst, nint dstrect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_BlitSurfaceTiledWithScale(nint src, nint srcrect, float scale,
        ScaleMode scaleMode, nint dst, nint dstrect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_BlitSurfaceUnchecked(nint src, nint srcrect, nint dst,
            nint dstrect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_BlitSurfaceUncheckedScaled(nint src, nint srcrect, nint dst, nint dstrect,
        ScaleMode scaleMode);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_CleanupTLS();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ClearClipboardData();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ClearComposition(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ClearError();

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ClearProperty(uint props, string name);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ClearSurface(nint surface, float r, float g, float b, float a);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_ComposeCustomBlendMode(BlendFactor srcColorFactor, BlendFactor dstColorFactor,
        BlendOperation colorOperation, BlendFactor srcAlphaFactor, BlendFactor dstAlphaFactor,
        BlendOperation alphaOperation);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ConvertPixels(int width, int height, PixelFormat srcFormat, nint src,
        int srcPitch, PixelFormat dstFormat, nint dst, int dstPitch);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ConvertPixelsAndColorspace(int width, int height, PixelFormat srcFormat,
        Colorspace srcColorspace, uint srcProperties, nint src, int srcPitch, PixelFormat dstFormat,
        Colorspace dstColorspace, uint dstProperties, nint dst, int dstPitch);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_ConvertSurface(nint surface, PixelFormat format);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_ConvertSurfaceAndColorspace(nint surface, PixelFormat format,
        nint palette, Colorspace colorspace, uint props);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CopyProperties(uint src, uint dst);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreatePalette(int ncolors);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreatePopupWindow(nint parent, int offsetX, int offsetY, int w, int h,
        WindowFlags flags);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_CreateProperties();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateSurface(int width, int height, PixelFormat format);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateSurfaceFrom(int width, int height, PixelFormat format, nint pixels,
        int pitch);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateSurfacePalette(nint surface);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateThreadRuntime(SdlThreadFunction fn, string name, nint data,
        nint pfnBeginThread, nint pfnEndThread);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateThreadWithPropertiesRuntime(uint props, nint pfnBeginThread,
        nint pfnEndThread);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateWindow(string title, int w, int h, WindowFlags flags);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateWindowWithProperties(uint props);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyPalette(nint palette);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyProperties(uint props);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroySurface(nint surface);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyWindow(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_DestroyWindowSurface(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DetachThread(nint thread);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_DisableScreenSaver();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_DuplicateSurface(nint surface);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_EnableScreenSaver();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_EnterAppMainCallbacks(int argc, nint argv, SdlAppInitFunc appInit,
        SdlAppIterateFunc appIter, SdlAppEventFunc sdlAppEvent, SdlAppQuitFunc appQuit);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_EnumerateProperties(uint props, SdlEnumeratePropertiesCallback callback,
        nint userdata);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_FillSurfaceRect(nint dst, nint rect, uint color);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_FillSurfaceRects(nint dst, Span<Rect> rects, int count, uint color);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_FlashWindow(nint window, FlashOperation operation);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_FlipSurface(nint surface, FlipMode flip);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_free(nint mem);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GDKSuspendComplete();

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetAppMetadataProperty(string name);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetBooleanProperty(uint props, string name, SdlBool defaultValue);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetClipboardData(string mimeType, out nuint size);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetClipboardMimeTypes(out nuint numMimeTypes);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CallerOwnedStringMarshaller))]
    private static partial string SDL_GetClipboardText();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetClosestFullscreenDisplayMode(uint displayId, int w, int h, float refreshRate,
        SdlBool includeHighDensityModes, out DisplayMode closest);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetCurrentDisplayMode(uint displayId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayOrientation SDL_GetCurrentDisplayOrientation(uint displayId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong SDL_GetCurrentThreadID();

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetCurrentVideoDriver();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetDesktopDisplayMode(uint displayId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetDisplayBounds(uint displayId, out Rect rect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetDisplayContentScale(uint displayId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetDisplayForPoint(ref Point point);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetDisplayForRect(ref Rect rect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetDisplayForWindow(nint window);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetDisplayName(uint displayId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetDisplayProperties(uint displayId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetDisplays(out int count);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetDisplayUsableBounds(uint displayId, out Rect rect);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetError();

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetFloatProperty(uint props, string name, float defaultValue);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetFullscreenDisplayModes(uint displayId, out int count);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetGlobalProperties();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetGrabbedWindow();

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetHint(string name);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetHintBoolean(string name, SdlBool defaultValue);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetKeyboardFocus();

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetKeyboardNameForID(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetKeyboards(out int count);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetKeyboardState(out int numkeys);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetKeyFromName(string name);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetKeyFromScancode(Scancode scanCode, KeyMod modstate, SdlBool keyEvent);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetKeyName(uint key);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetMasksForPixelFormat(PixelFormat format, out int bpp, out uint rmask,
        out uint gmask, out uint bmask, out uint amask);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial KeyMod SDL_GetModState();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayOrientation SDL_GetNaturalDisplayOrientation(uint displayId);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long SDL_GetNumberProperty(uint props, string name, long defaultValue);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumVideoDrivers();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetPixelFormatDetails(PixelFormat format);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PixelFormat SDL_GetPixelFormatForMasks(int bpp, uint rmask, uint gmask, uint bmask,
        uint amask);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetPixelFormatName(PixelFormat format);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetPointerProperty(uint props, string name, nint defaultValue);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PowerState SDL_GetPowerInfo(out int seconds, out int percent);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetPreferredLocales(out int count);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetPrimaryDisplay();

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CallerOwnedStringMarshaller))]
    private static partial string SDL_GetPrimarySelectionText();

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PropertyType SDL_GetPropertyType(uint props, string name);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRectAndLineIntersection(ref Rect rect, ref int x1, ref int y1, ref int x2,
        ref int y2);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRectAndLineIntersectionFloat(ref FRect rect, ref float x1, ref float y1,
        ref float x2, ref float y2);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRectEnclosingPoints(Span<Point> points, int count, ref Rect clip,
        out Rect result);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRectEnclosingPointsFloat(Span<FPoint> points, int count, ref FRect clip,
        out FRect result);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRectIntersection(ref Rect a, ref Rect b, out Rect result);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRectIntersectionFloat(ref FRect a, ref FRect b, out FRect result);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRectUnion(ref Rect a, ref Rect b, out Rect result);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRectUnionFloat(ref FRect a, ref FRect b, out FRect result);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GetRGB(uint pixel, nint format, nint palette, out byte r, out byte g,
        out byte b);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GetRGBA(uint pixel, nint format, nint palette, out byte r, out byte g,
        out byte b, out byte a);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Scancode SDL_GetScancodeFromKey(uint key, nint modstate);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Scancode SDL_GetScancodeFromName(string name);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetScancodeName(Scancode scanCode);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetStringProperty(uint props, string name, string defaultValue);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetSurfaceAlphaMod(nint surface, out byte alpha);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetSurfaceBlendMode(nint surface, nint blendMode);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetSurfaceClipRect(nint surface, out Rect rect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetSurfaceColorKey(nint surface, out uint key);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetSurfaceColorMod(nint surface, out byte r, out byte g, out byte b);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Colorspace SDL_GetSurfaceColorspace(nint surface);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetSurfaceImages(nint surface, out int count);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetSurfacePalette(nint surface);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetSurfaceProperties(nint surface);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SystemTheme SDL_GetSystemTheme();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetTextInputArea(nint window, out Rect rect, out int cursor);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong SDL_GetThreadID(nint thread);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetThreadName(nint thread);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SharpSDL3.Enums.ThreadState SDL_GetThreadState(nint thread);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetTLS(nint id);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetVideoDriver(int index);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetWindowAspectRatio(nint window, out float minAspect, out float maxAspect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetWindowBordersSize(nint window, out int top, out int left, out int bottom,
        out int right);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetWindowDisplayScale(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial WindowFlags SDL_GetWindowFlags(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetWindowFromID(uint id);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetWindowFullscreenMode(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetWindowICCProfile(nint window, out nuint size);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetWindowID(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetWindowKeyboardGrab(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetWindowMaximumSize(nint window, out int w, out int h);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetWindowMinimumSize(nint window, out int w, out int h);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetWindowMouseGrab(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetWindowMouseRect(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetWindowOpacity(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetWindowParent(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetWindowPixelDensity(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PixelFormat SDL_GetWindowPixelFormat(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetWindowPosition(nint window, out int x, out int y);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetWindowProperties(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetWindows(out int count);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetWindowSafeArea(nint window, out Rect rect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetWindowSize(nint window, out int w, out int h);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetWindowSizeInPixels(nint window, out int w, out int h);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetWindowSurface(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetWindowSurfaceVSync(nint window, out int vsync);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetWindowTitle(nint window);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GUIDToString(SdlGuid guid, string pszGuid, int cbGuid);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasClipboardData(string mimeType);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasClipboardText();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasKeyboard();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasPrimarySelectionText();

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasProperty(uint props, string name);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasRectIntersection(ref Rect a, ref Rect b);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasRectIntersectionFloat(ref FRect a, ref FRect b);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasScreenKeyboardSupport();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HideWindow(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_Init(InitFlags flags);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_InitSubSystem(InitFlags flags);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_IsMainThread();

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_LoadBMP(string file);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_LoadBMP_IO(nint src, SdlBool closeio);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_LoadFunction(nint handle, string name);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_LoadObject(string sofile);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_LockProperties(uint props);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_LockSurface(nint surface);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_malloc(nuint size);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_MapRGB(nint format, nint palette, byte r, byte g, byte b);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_MapRGBA(nint format, nint palette, byte r, byte g, byte b, byte a);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_MapSurfaceRGB(nint surface, byte r, byte g, byte b);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_MapSurfaceRGBA(nint surface, byte r, byte g, byte b, byte a);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_MaximizeWindow(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_MinimizeWindow(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_OutOfMemory();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_PremultiplyAlpha(int width, int height, PixelFormat srcFormat, nint src,
            int srcPitch, PixelFormat dstFormat, nint dst, int dstPitch, SdlBool linear);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_PremultiplySurfaceAlpha(nint surface, SdlBool linear);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_Quit();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_QuitSubSystem(InitFlags flags);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RaiseWindow(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadSurfacePixel(nint surface, int x, int y, out byte r, out byte g, out byte b,
        out byte a);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadSurfacePixelFloat(nint surface, int x, int y, out float r, out float g,
        out float b, out float a);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_RemoveHintCallback(string name, SdlHintCallback callback, nint userdata);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_RemoveSurfaceAlternateImages(nint surface);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ResetHint(string name);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ResetHints();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ResetKeyboard();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RestoreWindow(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_RunApp(int argc, nint argv, SdlMainFunc mainFunction, nint reserved);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RunOnMainThread(SdlMainThreadCallback callback, nint userdata,
            SdlBool waitComplete);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SaveBMP(nint surface, string file);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SaveBMP_IO(nint surface, nint dst, SdlBool closeio);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_ScaleSurface(nint surface, int width, int height, ScaleMode scaleMode);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ScreenKeyboardShown(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ScreenSaverEnabled();

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAppMetadata(string appname, string appversion, string appidentifier);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAppMetadataProperty(string name, string value);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetBooleanProperty(uint props, string name, SdlBool value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetClipboardData(SdlClipboardDataCallback callback,
            SdlClipboardCleanupCallback cleanup, nint userdata, nint mimeTypes, nuint numMimeTypes);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetClipboardText(string text);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetCurrentThreadPriority(SharpSDL3.Enums.ThreadPriority priority);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetError(string fmt);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetFloatProperty(uint props, string name, float value);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetHint(string name, string value);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetHintWithPriority(string name, string value, HintPriority priority);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetMainReady();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetModState(KeyMod modstate);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetNumberProperty(uint props, string name, long value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetPaletteColors(nint palette, Span<Color> colors, int firstcolor,
        int ncolors);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetPointerProperty(uint props, string name, nint value);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetPointerPropertyWithCleanup(uint props, string name, nint value,
        SdlCleanupPropertyCallback cleanup, nint userdata);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetPrimarySelectionText(string text);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetScancodeName(Scancode scanCode, string name);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetStringProperty(uint props, string name, string value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetSurfaceAlphaMod(nint surface, byte alpha);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetSurfaceBlendMode(nint surface, uint blendMode);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetSurfaceClipRect(nint surface, ref Rect rect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetSurfaceColorKey(nint surface, SdlBool enabled, uint key);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetSurfaceColorMod(nint surface, byte r, byte g, byte b);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetSurfaceColorspace(nint surface, Colorspace colorspace);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetSurfacePalette(nint surface, nint palette);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetSurfaceRLE(nint surface, SdlBool enabled);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetTextInputArea(nint window, ref Rect rect, int cursor);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetTLS(nint id, nint value, SdlTlsDestructorCallback destructor);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowAlwaysOnTop(nint window, SdlBool onTop);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowAspectRatio(nint window, float minAspect, float maxAspect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowBordered(nint window, SdlBool bordered);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowFocusable(nint window, SdlBool focusable);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowFullscreen(nint window, SdlBool fullscreen);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowFullscreenMode(nint window, ref DisplayMode mode);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowHitTest(nint window, SdlHitTest callback, nint callbackData);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowIcon(nint window, nint icon);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowKeyboardGrab(nint window, SdlBool grabbed);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowMaximumSize(nint window, int maxW, int maxH);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowMinimumSize(nint window, int minW, int minH);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowModal(nint window, SdlBool modal);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowMouseGrab(nint window, SdlBool grabbed);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowMouseRect(nint window, ref Rect rect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowOpacity(nint window, float opacity);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowParent(nint window, nint parent);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowPosition(nint window, int x, int y);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowResizable(nint window, SdlBool resizable);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowShape(nint window, nint shape);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowSize(nint window, int w, int h);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowSurfaceVSync(nint window, int vsync);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowTitle(nint window, string title);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ShowWindow(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ShowWindowSystemMenu(nint window, int x, int y);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_StartTextInput(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_StartTextInputWithProperties(nint window, uint props);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_StopTextInput(nint window);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlGuid SDL_StringToGUID(string pchGuid);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SurfaceHasAlternateImages(nint surface);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SurfaceHasColorKey(nint surface);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SurfaceHasRLE(nint surface);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SyncWindow(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_TextInputActive(nint window);

    // /usr/local/include/SDL3/SDL_loadso.h
    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnloadObject(nint handle);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnlockProperties(uint props);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnlockSurface(nint surface);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_UpdateWindowSurface(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_UpdateWindowSurfaceRects(nint window, Span<Rect> rects, int numrects);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_WaitThread(nint thread, nint status);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial InitFlags SDL_WasInit(InitFlags flags);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WindowHasSurface(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteSurfacePixel(nint surface, int x, int y, byte r, byte g, byte b, byte a);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteSurfacePixelFloat(nint surface, int x, int y, float r, float g, float b,
        float a);
}