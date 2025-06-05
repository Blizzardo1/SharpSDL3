<<<<<<< HEAD
using SharpSDL3.Enums;
=======
﻿using SharpSDL3.Enums;
>>>>>>> main
using SharpSDL3.Structs;
using System;
using System.Runtime.InteropServices;

namespace SharpSDL3;

[StructLayout(LayoutKind.Sequential)]
public struct Surface {
    // Public API definition
    public SurfaceFlags Flags;     /**< The flags of the surface, read-only */
    public PixelFormat Format;     /**< The format of the surface, read-only */
    public int W;                      /**< The width of the surface, read-only. */
    public int H;                      /**< The height of the surface, read-only. */
    public int Pitch;                  /**< The distance in bytes between rows of pixels, read-only */
    public nint Pixels;               /**< A pointer to the pixels of the surface, the pixels are writeable if non-NULL */

    public int Refcount;               /**< Application reference count, used when freeing surface */

    public nint Reserved;             /**< Reserved for internal use */

    // Private API definition

    /** flags for this surface */
    public SurfaceDataFlags InternalFlags;

    /** properties for this surface */
    public int Props;

    /** detailed format for this surface */
    public PixelFormatDetails Fmt;

    /** Pixel colorspace */
    public Colorspace Colorspace;

    /** palette for indexed surfaces */
    public Palette Palette;

    /** Alternate representation of images */
    public int NumImages;
    public Surface[] Images;

    /** information needed for surfaces requiring locks */
    public int Locked;

    /** clipping information */
    public Rect ClipRect;

    /** info for fast blit mapping to other surfaces */
    public BlitMap Map;

    public nint Handle;
}

[StructLayout(LayoutKind.Sequential)]
public struct BlitMap {
    public int Identity;
    public Blit Blit;
    public nint Data;
    public BlitInfo Info;

    /* the version count matches the destination; mismatch indicates
       an invalid mapping */
    public uint DstPaletteVersion;
    public uint SrcPaletteVersion;
}

[StructLayout(LayoutKind.Sequential)]
public struct BlitInfo {
    public nint SrcSurface; // SDL_Surface*
    public nint Src;         // IntPtr instead of byte*
    public int SrcW, SrcH;
    public int SrcPitch;
    public int SrcSkip;
    public nint DstSurface; // SDL_Surface*
    public nint Dst;         // IntPtr instead of byte*
    public int DstW, DstH;
    public int DstPitch;
    public int DstSkip;
    public nint SrcFmt;     // IntPtr instead of PixelFormatDetails*
    public nint SrcPal;     // IntPtr instead of Palette*
    public nint DstFmt;     // IntPtr instead of PixelFormatDetails*
    public nint DstPal;     // IntPtr instead of Palette*
    public nint Table;       // IntPtr instead of byte*
    public nint PaletteMap; // SDL_HashTable*
    public int Flags;
    public uint Colorkey;
    public byte R, G, B, A;
}

public delegate bool Blit(Surface src, Rect srcrect, Surface dst, Rect dstrect);
