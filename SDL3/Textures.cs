using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static SharpSDL3.Sdl;

namespace SharpSDL3; 
public static unsafe partial class Textures {

    public static nint CreateTexture(nint renderer, PixelFormat format, TextureAccess access,
            int w, int h) {

        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Renderer is null");
        }

        if (!Enum.IsDefined(format)) {
            Logger.LogError(LogCategory.Render, "Format is not defined");
        }

        if (!Enum.IsDefined(access)) {
            Logger.LogError(LogCategory.Render, "Access is not defined");
        }

        return SDL_CreateTexture(renderer, format, access, w, h);
    }

    public static nint CreateTextureFromSurface(nint renderer, nint surface) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Renderer is null");
        }
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Surface is null");
        }
        return SDL_CreateTextureFromSurface(renderer, surface);
    }

    public static nint CreateTextureWithProperties(nint renderer, uint props) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Renderer is null");
        }
        return SDL_CreateTextureWithProperties(renderer, props);
    }

    public static bool GetTextureAlphaMod(nint texture, out byte alpha) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_GetTextureAlphaMod(texture, out alpha);
    }

    public static byte GetTextureAlphaMod(nint texture) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        SDL_GetTextureAlphaMod(texture, out byte alpha);
        return alpha;
    }

    public static bool GetTextureAlphaModFloat(nint texture, out float alpha) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_GetTextureAlphaModFloat(texture, out alpha);
    }

    public static float GetTextureAlphaModFloat(nint texture) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        SDL_GetTextureAlphaModFloat(texture, out float alpha);
        return alpha;
    }

    public static bool GetTextureBlendMode(nint texture, nint blendMode) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_GetTextureBlendMode(texture, blendMode);
    }

    public static bool GetTextureColorMod(nint texture, out byte r, out byte g, out byte b) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_GetTextureColorMod(texture, out r, out g, out b);
    }

    public static Color GetTextureColorMod(nint texture) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        SDL_GetTextureColorMod(texture, out byte r, out byte g, out byte b);
        return new Color() { R = r, G = g, B = b };
    }

    public static bool GetTextureColorModFloat(nint texture, out float r, out float g, out float b) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_GetTextureColorModFloat(texture, out r, out g, out b);
    }

    public static FColor GetTextureColorModFloat(nint texture) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        SDL_GetTextureColorModFloat(texture, out float r, out float g, out float b);
        return new FColor() { R = r, G = g, B = b };
    }

    public static uint GetTextureProperties(nint texture) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_GetTextureProperties(texture);
    }

    public static bool GetTextureScaleMode(nint texture, out ScaleMode scaleMode) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_GetTextureScaleMode(texture, out scaleMode);
    }

    public static ScaleMode GetTextureScaleMode(nint texture) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        SDL_GetTextureScaleMode(texture, out ScaleMode scaleMode);
        return scaleMode;
    }

    public static bool GetTextureSize(nint texture, out float w, out float h) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_GetTextureSize(texture, out w, out h);
    }

    public static Vector2 GetTextureSize(nint texture) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        SDL_GetTextureSize(texture, out float w, out float h);
        return new(w, h);
    }

    public static bool LockTexture(nint texture, ref Rect rect, out nint pixels,
            out int pitch) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_LockTexture(texture, ref rect, out pixels, out pitch);
    }

    public static bool LockTexture(nint texture, nint rect, out nint pixels,
            out int pitch) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        Rect trect = Marshal.PtrToStructure<Rect>(rect);
        return SDL_LockTexture(texture, ref trect, out pixels, out pitch);
    }

    public static bool LockTextureToSurface(nint texture, ref Rect rect,
            out nint surface) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_LockTextureToSurface(texture, ref rect, out surface);
    }

    public static bool SetTextureAlphaMod(nint texture, byte alpha) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_SetTextureAlphaMod(texture, alpha);
    }

    public static bool SetTextureAlphaModFloat(nint texture, float alpha) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_SetTextureAlphaModFloat(texture, alpha);
    }

    public static bool SetTextureBlendMode(nint texture, uint blendMode) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_SetTextureBlendMode(texture, blendMode);
    }

    public static bool SetTextureColorMod(nint texture, byte r, byte g, byte b) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_SetTextureColorMod(texture, r, g, b);
    }

    public static bool SetTextureColorMod(nint texture, Color color) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_SetTextureColorMod(texture, color.R, color.G, color.B);
    }

    public static bool SetTextureColorModFloat(nint texture, float r, float g, float b) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_SetTextureColorModFloat(texture, r, g, b);
    }

    public static bool SetTextureColorModFloat(nint texture, FColor color) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_SetTextureColorModFloat(texture, color.R, color.G, color.B);
    }

    public static bool SetTextureScaleMode(nint texture, ScaleMode scaleMode) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_SetTextureScaleMode(texture, scaleMode);
    }

    public static void UnlockTexture(nint texture) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        SDL_UnlockTexture(texture);
    }

    public static bool UpdateNVTexture(nint texture, ref Rect rect, nint yplane, int ypitch,
            nint uVplane, int uVpitch) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_UpdateNVTexture(texture, ref rect, yplane, ypitch, uVplane, uVpitch);
    }

    public static bool UpdateTexture(nint texture, ref Rect rect, nint pixels, int pitch) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_UpdateTexture(texture, ref rect, pixels, pitch);
    }

    public static bool UpdateYUVTexture(nint texture, ref Rect rect, nint yplane, int ypitch,
            nint uplane, int upitch, nint vplane, int vpitch) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_UpdateYUVTexture(texture, ref rect, yplane, ypitch, uplane, upitch, vplane, vpitch);
    }

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
    private static partial SdlBool
        SDL_GetTextureAlphaMod(nint texture, out byte alpha);

   
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_GetTextureAlphaModFloat(nint texture, out float alpha);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_GetTextureBlendMode(nint texture, nint blendMode);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_GetTextureColorMod(nint texture, out byte r, out byte g, out byte b);

   
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_GetTextureColorModFloat(nint texture, out float r, out float g,
            out float b);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetTextureProperties(nint texture);
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_GetTextureScaleMode(nint texture, out ScaleMode scaleMode);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_GetTextureSize(nint texture, out float w, out float h);
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_LockTexture(nint texture, ref Rect rect, out nint pixels,
            out int pitch);

   
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_LockTextureToSurface(nint texture, ref Rect rect,
            out nint surface);

   
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetTextureAlphaMod(nint texture, byte alpha);

   
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_SetTextureAlphaModFloat(nint texture, float alpha);

   
   
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_SetTextureBlendMode(nint texture, uint blendMode);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_SetTextureColorMod(nint texture, byte r, byte g, byte b);
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_SetTextureColorModFloat(nint texture, float r, float g, float b);

    
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_SetTextureScaleMode(nint texture, ScaleMode scaleMode);
                                                                   
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnlockTexture(nint texture);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_UpdateNVTexture(nint texture, ref Rect rect, nint yplane, int ypitch,
            nint uVplane, int uVpitch);

   
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_UpdateTexture(nint texture, ref Rect rect, nint pixels, int pitch);
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_UpdateYUVTexture(nint texture, ref Rect rect, nint yplane, int ypitch,
        nint uplane, int upitch, nint vplane, int vpitch);
    

}
