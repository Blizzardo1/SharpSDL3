using SDL3.Enums;
using SDL3.Structs;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static SDL3.Sdl;

namespace SDL3; 
public static unsafe partial class Texture {

    // nint refers to a Texture*
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateTexture(nint renderer, PixelFormat format, TextureAccess access,
        int w, int h);

    // nint refers to a Texture*
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateTextureFromSurface(nint renderer, nint surface);

    // nint refers to a Texture*
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateTextureWithProperties(nint renderer, uint props);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetTextureProperties(nint texture); // WARN_UNKNOWN_POINTER_PARAMETER


    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_GetTextureSize(nint texture, out float w, out float h); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_SetTextureColorMod(nint texture, byte r, byte g, byte b); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_SetTextureColorModFloat(nint texture, float r, float g, float b); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_GetTextureColorMod(nint texture, out byte r, out byte g, out byte b); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_GetTextureColorModFloat(nint texture, out float r, out float g,
            out float b); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetTextureAlphaMod(nint texture, byte alpha); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_SetTextureAlphaModFloat(nint texture, float alpha); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_GetTextureAlphaMod(nint texture, out byte alpha); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_GetTextureAlphaModFloat(nint texture, out float alpha); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_SetTextureBlendMode(nint texture, uint blendMode); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_GetTextureBlendMode(nint texture, nint blendMode); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_SetTextureScaleMode(nint texture, ScaleMode scaleMode); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_GetTextureScaleMode(nint texture, out ScaleMode scaleMode); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_UpdateTexture(nint texture, ref Rect rect, nint pixels, int pitch); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_UpdateYUVTexture(nint texture, ref Rect rect, nint yplane, int ypitch,
        nint uplane, int upitch, nint vplane, int vpitch); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_UpdateNVTexture(nint texture, ref Rect rect, nint yplane, int ypitch,
        nint uVplane, int uVpitch); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_LockTexture(nint texture, ref Rect rect, out nint pixels,
            out int pitch); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_LockTextureToSurface(nint texture, ref Rect rect,
            out nint surface); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnlockTexture(nint texture); // WARN_UNKNOWN_POINTER_PARAMETER
}
