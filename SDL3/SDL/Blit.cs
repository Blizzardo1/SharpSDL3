using SharpSDL3.Enums;
using SharpSDL3.Structs;

namespace SharpSDL3;

public static partial class Sdl {
    
    /// <summary>Performs a fast blit from the source surface to the destination surface with clipping.</summary>
    /// <param name="src">the <see cref="Surface" /> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect" /> structure representing the rectangle to be copied, or <see langword="null" /> to copy the entire surface.</param>
    /// <param name="dst">the <see cref="Surface" /> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect" /> structure representing the x and y position in the destination surface, or <see langword="null" /> for (0,0). The width and height are ignored, and are copied from srcrect. If you want a specific width and height, you should use <see cref="BlitSurfaceScaled" />.</param>
    /// <remarks>
    /// If either srcrect or dstrect are <see langword="null" />, the entire surface (src or dst) is copied while ensuring clipping to dst-&gt;clip_rect.
    /// <para><strong>Thread Safety</strong>: Only one thread should be using the src and dst surfaces at any given time.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BlitSurfaceScaled" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool BlitSurface(nint src, nint srcrect, nint dst, nint dstrect) {
        if (src != nint.Zero && dst != nint.Zero) return SDL_BlitSurface(src, srcrect, dst, dstrect);
        LogWarn(LogCategory.System, "BlitSurface: Source or destination pointer is null.");
        return false;
    }

    /// <summary>Perform a scaled blit using the 9-grid algorithm to a destination surface, which may be of a different format.</summary>
    /// <param name="src">the <see cref="Surface" /> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect" /> structure representing the rectangle to be used for the 9-grid, or <see langword="null" /> to use the entire surface.</param>
    /// <param name="leftWidth">the width, in pixels, of the left corners in srcrect.</param>
    /// <param name="rightWidth">the width, in pixels, of the right corners in srcrect.</param>
    /// <param name="topHeight">the height, in pixels, of the top corners in srcrect.</param>
    /// <param name="bottomHeight">the height, in pixels, of the bottom corners in srcrect.</param>
    /// <param name="scale">the scale used to transform the corner of srcrect into the corner of dstrect, or 0.0f for an unscaled blit.</param>
    /// <param name="scaleMode">scale algorithm to be used.</param>
    /// <param name="dst">the <see cref="Surface" /> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect" /> structure representing the target rectangle in the destination surface, or <see langword="null" /> to fill the entire surface.</param>
    /// <remarks>
    /// The pixels in the source surface are split into a 3x3 grid, using the
    /// different corner sizes for each corner, and the sides and center making up
    /// the remaining pixels. The corners are then scaled using scale and fit
    /// into the corners of the destination rectangle. The sides and center are
    /// then stretched into place to cover the remaining destination rectangle.
    /// <para><strong>Thread Safety</strong>: Only one thread should be using the src and dst surfaces at any given time.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BlitSurface" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool BlitSurface9Grid(nint src, nint srcrect, int leftWidth, int rightWidth, int topHeight, int bottomHeight, float scale, ScaleMode scaleMode, nint dst, nint dstrect) {
        if (src != nint.Zero && dst != nint.Zero)
            return SDL_BlitSurface9Grid(src, srcrect, leftWidth, rightWidth, topHeight, bottomHeight, scale, scaleMode,
                dst, dstrect);
        LogWarn(LogCategory.System, "BlitSurface9Grid: Source or destination pointer is null.");
        return false;
    }

    /// <summary>Perform a scaled blit to a destination surface, which may be of a different format.</summary>
    /// <param name="src">the <see cref="Surface" /> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect" /> structure representing the rectangle to be copied, or <see langword="null" /> to copy the entire surface.</param>
    /// <param name="dst">the <see cref="Surface" /> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect" /> structure representing the target rectangle in the destination surface, or <see langword="null" /> to fill the entire destination surface.</param>
    /// <param name="scaleMode">the <see cref="ScaleMode" /> to be used.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: Only one thread should be using the src and dst surfaces at any given time.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BlitSurface" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool BlitSurfaceScaled(nint src, nint srcrect, nint dst, nint dstrect, ScaleMode scaleMode) {
        if (src != nint.Zero && dst != nint.Zero) return SDL_BlitSurfaceScaled(src, srcrect, dst, dstrect, scaleMode);
        LogWarn(LogCategory.System, "BlitSurfaceScaled: Source or destination pointer is null.");
        return false;
    }

    /// <summary>Perform a tiled blit to a destination surface, which may be of a different format.</summary>
    /// <param name="src">the <see cref="Surface" /> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect" /> structure representing the rectangle to be copied, or <see langword="null" /> to copy the entire surface.</param>
    /// <param name="dst">the <see cref="Surface" /> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect" /> structure representing the target rectangle in the destination surface, or <see langword="null" /> to fill the entire surface.</param>
    /// <remarks>
    /// The pixels in srcrect will be repeated as many times as needed to completely fill dstrect.
    /// <para><strong>Thread Safety</strong>: Only one thread should be using the src and dst surfaces at any given time.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BlitSurface" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool BlitSurfaceTiled(nint src, nint srcrect, nint dst, nint dstrect) {
        if (src != nint.Zero && dst != nint.Zero) return SDL_BlitSurfaceTiled(src, srcrect, dst, dstrect);
        LogWarn(LogCategory.System, "BlitSurfaceTiled: Source or destination pointer is null.");
        return false;
    }

    /// <summary>Perform a scaled and tiled blit to a destination surface, which may be of a different format.</summary>
    /// <param name="src">the <see cref="Surface" /> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect" /> structure representing the rectangle to be copied, or <see langword="null" /> to copy the entire surface.</param>
    /// <param name="scale">the scale used to transform srcrect into the destination rectangle, e.g. a 32x32 texture with a scale of 2 would fill 64x64 tiles.</param>
    /// <param name="scaleMode">scale algorithm to be used.</param>
    /// <param name="dst">the <see cref="Surface" /> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect" /> structure representing the target rectangle in the destination surface, or <see langword="null" /> to fill the entire surface.</param>
    /// <remarks>
    /// The pixels in srcrect will be scaled and repeated as many times as needed
    /// to completely fill dstrect.
    /// <para><strong>Thread Safety</strong>: Only one thread should be using the src and dst surfaces at any given time.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BlitSurface" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool BlitSurfaceTiledWithScale(nint src, nint srcrect, float scale, ScaleMode scaleMode, nint dst, nint dstrect) {
        if (src != nint.Zero && dst != nint.Zero)
            return SDL_BlitSurfaceTiledWithScale(src, srcrect, scale, scaleMode, dst, dstrect);
        LogWarn(LogCategory.System, "BlitSurfaceTiledWithScale: Source or destination pointer is null.");
        return false;
    }

    /// <summary>Perform low-level surface blitting only.</summary>
    /// <param name="src">the <see cref="Surface" /> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect" /> structure representing the rectangle to be copied, may not be <see langword="null" />.</param>
    /// <param name="dst">the <see cref="Surface" /> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect" /> structure representing the target rectangle in the destination surface, may not be <see langword="null" />.</param>
    /// <remarks>
    /// This is a semi-private blit function and it performs low-level surface
    /// blitting, assuming the input rectangles have already been clipped.
    /// <para><strong>Thread Safety</strong>: Only one thread should be using the src and dst surfaces at any given time.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BlitSurface" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool BlitSurfaceUnchecked(nint src, nint srcrect, nint dst, nint dstrect) {
        if (src != nint.Zero && dst != nint.Zero) return SDL_BlitSurfaceUnchecked(src, srcrect, dst, dstrect);
        LogWarn(LogCategory.System, "BlitSurfaceUnchecked: Source or destination pointer is null.");
        return false;
    }

    /// <summary>Perform low-level surface scaled blitting only.</summary>
    /// <param name="src">the <see cref="Surface" /> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect" /> structure representing the rectangle to be copied, may not be <see langword="null" />.</param>
    /// <param name="dst">the <see cref="Surface" /> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect" /> structure representing the target rectangle in the destination surface, may not be <see langword="null" />.</param>
    /// <param name="scaleMode">the <see cref="ScaleMode" /> to be used.</param>
    /// <remarks>
    /// This is a semi-private function and it performs low-level surface blitting,
    /// assuming the input rectangles have already been clipped.
    /// <para><strong>Thread Safety</strong>: Only one thread should be using the src and dst surfaces at any given time.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="BlitSurfaceScaled" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool BlitSurfaceUncheckedScaled(nint src, nint srcrect, nint dst, nint dstrect, ScaleMode scaleMode) {
        if (src != nint.Zero && dst != nint.Zero)
            return SDL_BlitSurfaceUncheckedScaled(src, srcrect, dst, dstrect, scaleMode);
        LogWarn(LogCategory.System, "BlitSurfaceUncheckedScaled: Source or destination pointer is null.");
        return false;
    }
}