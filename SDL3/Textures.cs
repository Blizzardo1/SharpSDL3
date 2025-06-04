using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSDL3;

public static unsafe partial class Sdl {
    /// <summary>Create a texture for a rendering context.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="format">one of the enumerated values in <see cref="PixelFormat"/>.</param>
    /// <param name="access">one of the enumerated values in SDL_TextureAccess.</param>
    /// <param name="w">the width of the texture in pixels.</param>
    /// <param name="h">the height of the texture in pixels.</param>
    /// <remarks>
    /// The contents of a texture when first created are not defined.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateTextureFromSurface"/>
    /// <seealso cref="CreateTextureWithProperties"/>
    /// <seealso cref="DestroyTexture"/>
    /// <seealso cref="GetTextureSize"/>
    /// <seealso cref="UpdateTexture"/>
    /// </remarks>
    /// <returns>(SDL_Texture *) Returns the created texture or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint CreateTexture(nint renderer, PixelFormat format, TextureAccess access,
                int w, int h) {
        if (renderer == nint.Zero) {
            LogError(LogCategory.Render, "Renderer is null");
        }

        if (!Enum.IsDefined(format)) {
            LogError(LogCategory.Render, "Format is not defined");
        }

        if (!Enum.IsDefined(access)) {
            LogError(LogCategory.Render, "Access is not defined");
        }

        return SDL_CreateTexture(renderer, format, access, w, h);
    }

    /// <summary>Create a texture from an existing surface.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="surface">the <see cref="Surface"/> structure containing pixel data used to fill the texture.</param>
    /// <remarks>
    /// The surface is not modified or freed by this function.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateTexture"/>
    /// <seealso cref="CreateTextureWithProperties"/>
    /// <seealso cref="DestroyTexture"/>
    /// </remarks>
    /// <returns>(SDL_Texture *) Returns the created texture or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint CreateTextureFromSurface(nint renderer, nint surface) {
        if (renderer == nint.Zero) {
            LogError(LogCategory.Render, "Renderer is null");
        }
        if (surface == nint.Zero) {
            LogError(LogCategory.Render, "Surface is null");
        }
        return SDL_CreateTextureFromSurface(renderer, surface);
    }

    /// <summary>Create a texture for a rendering context with the specified properties.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="props">the properties to use.</param>
    /// <remarks>
    /// These are the supported properties:
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateProperties"/>
    /// <seealso cref="CreateTexture"/>
    /// <seealso cref="CreateTextureFromSurface"/>
    /// <seealso cref="DestroyTexture"/>
    /// <seealso cref="GetTextureSize"/>
    /// <seealso cref="UpdateTexture"/>
    /// </remarks>
    /// <returns>(SDL_Texture *) Returns the created texture or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint CreateTextureWithProperties(nint renderer, uint props) {
        if (renderer == nint.Zero) {
            LogError(LogCategory.Render, "Renderer is null");
        }
        return SDL_CreateTextureWithProperties(renderer, props);
    }

    public static bool GetTextureAlphaMod(nint texture, out byte alpha) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_GetTextureAlphaMod(texture, out alpha);
    }

    /// <summary>Get the additional alpha value multiplied into render copy operations.</summary>

    /// <param name="texture">the texture to query.</param>
    /// <param name="alpha">a pointer filled in with the current alpha value.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetTextureAlphaModFloat"/>
    /// <seealso cref="GetTextureColorMod"/>
    /// <seealso cref="SetTextureAlphaMod"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static byte GetTextureAlphaMod(nint texture) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        SDL_GetTextureAlphaMod(texture, out byte alpha);
        return alpha;
    }

    public static bool GetTextureAlphaModFloat(nint texture, out float alpha) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_GetTextureAlphaModFloat(texture, out alpha);
    }

    /// <summary>Get the additional alpha value multiplied into render copy operations.</summary>

    /// <param name="texture">the texture to query.</param>
    /// <param name="alpha">a pointer filled in with the current alpha value.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetTextureAlphaMod"/>
    /// <seealso cref="GetTextureColorModFloat"/>
    /// <seealso cref="SetTextureAlphaModFloat"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static float GetTextureAlphaModFloat(nint texture) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        SDL_GetTextureAlphaModFloat(texture, out float alpha);
        return alpha;
    }

    /// <summary>Get the blend mode used for texture copy operations.</summary>

    /// <param name="texture">the texture to query.</param>
    /// <param name="blendMode">a pointer filled in with the current <see cref="BlendMode"/>.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetTextureBlendMode"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool GetTextureBlendMode(nint texture, nint blendMode) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_GetTextureBlendMode(texture, blendMode);
    }

    public static bool GetTextureColorMod(nint texture, out byte r, out byte g, out byte b) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_GetTextureColorMod(texture, out r, out g, out b);
    }

    /// <summary>Get the additional color value multiplied into render copy operations.</summary>

    /// <param name="texture">the texture to query.</param>
    /// <param name="r">a pointer filled in with the current red color value.</param>
    /// <param name="g">a pointer filled in with the current green color value.</param>
    /// <param name="b">a pointer filled in with the current blue color value.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetTextureAlphaMod"/>
    /// <seealso cref="GetTextureColorModFloat"/>
    /// <seealso cref="SetTextureColorMod"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static Color GetTextureColorMod(nint texture) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        SDL_GetTextureColorMod(texture, out byte r, out byte g, out byte b);
        return new Color() { R = r, G = g, B = b };
    }

    public static bool GetTextureColorModFloat(nint texture, out float r, out float g, out float b) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_GetTextureColorModFloat(texture, out r, out g, out b);
    }

    /// <summary>Get the additional color value multiplied into render copy operations.</summary>

    /// <param name="texture">the texture to query.</param>
    /// <param name="r">a pointer filled in with the current red color value.</param>
    /// <param name="g">a pointer filled in with the current green color value.</param>
    /// <param name="b">a pointer filled in with the current blue color value.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetTextureAlphaModFloat"/>
    /// <seealso cref="GetTextureColorMod"/>
    /// <seealso cref="SetTextureColorModFloat"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static FColor GetTextureColorModFloat(nint texture) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        SDL_GetTextureColorModFloat(texture, out float r, out float g, out float b);
        return new FColor() { R = r, G = g, B = b };
    }

    /// <summary>Get the properties associated with a texture.</summary>

    /// <param name="texture">the texture to query.</param>
    /// <remarks>
    /// The following read-only properties are provided by SDL:
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns a valid property ID on success or 0 on failure; call <see cref="GetError()" /> for more information.</returns>

    public static uint GetTextureProperties(nint texture) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_GetTextureProperties(texture);
    }

    public static bool GetTextureScaleMode(nint texture, out ScaleMode scaleMode) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_GetTextureScaleMode(texture, out scaleMode);
    }

    /// <summary>Get the scale mode used for texture scale operations.</summary>

    /// <param name="texture">the texture to query.</param>
    /// <param name="scaleMode">a pointer filled in with the current scale mode.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetTextureScaleMode"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static ScaleMode GetTextureScaleMode(nint texture) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        SDL_GetTextureScaleMode(texture, out ScaleMode scaleMode);
        return scaleMode;
    }

    public static bool GetTextureSize(nint texture, out float w, out float h) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_GetTextureSize(texture, out w, out h);
    }

    /// <summary>Get the size of a texture, as floating point values.</summary>

    /// <param name="texture">the texture to query.</param>
    /// <param name="w">a pointer filled in with the width of the texture in pixels. This argument can be <see langword="null" /> if you don't need this information.</param>
    /// <param name="h">a pointer filled in with the height of the texture in pixels. This argument can be <see langword="null" /> if you don't need this information.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static Vector2 GetTextureSize(nint texture) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        SDL_GetTextureSize(texture, out float w, out float h);
        return new(w, h);
    }

    /// <summary>Lock a portion of the texture for write-only pixel access.</summary>

    /// <param name="texture">the texture to lock for access, which was created with SDL_TEXTUREACCESS_STREAMING.</param>
    /// <param name="rect">an <see cref="Rect"/> structure representing the area to lock for access; <see langword="null" /> to lock the entire texture.</param>
    /// <param name="pixels">this is filled in with a pointer to the locked pixels, appropriately offset by the locked area.</param>
    /// <param name="pitch">this is filled in with the pitch of the locked pixels; the pitch is the length of one row in bytes.</param>
    /// <remarks>
    /// As an optimization, the pixels made available for editing don't necessarily
    /// contain the old texture data. This is a write-only operation, and if you
    /// need to keep a copy of the texture data you should do that at the
    /// application level.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LockTextureToSurface"/>
    /// <seealso cref="UnlockTexture"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> if the texture is not valid or wasnot created withSDL_TEXTUREACCESS_STREAMING; call <see cref="GetError()"/> for more information.</returns>

    public static bool LockTexture(nint texture, ref Rect rect, out nint pixels,
                out int pitch) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_LockTexture(texture, ref rect, out pixels, out pitch);
    }

    /// <summary>Lock a portion of the texture for write-only pixel access.</summary>

    /// <param name="texture">the texture to lock for access, which was created with SDL_TEXTUREACCESS_STREAMING.</param>
    /// <param name="rect">an <see cref="Rect"/> structure representing the area to lock for access; <see langword="null" /> to lock the entire texture.</param>
    /// <param name="pixels">this is filled in with a pointer to the locked pixels, appropriately offset by the locked area.</param>
    /// <param name="pitch">this is filled in with the pitch of the locked pixels; the pitch is the length of one row in bytes.</param>
    /// <remarks>
    /// As an optimization, the pixels made available for editing don't necessarily
    /// contain the old texture data. This is a write-only operation, and if you
    /// need to keep a copy of the texture data you should do that at the
    /// application level.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LockTextureToSurface"/>
    /// <seealso cref="UnlockTexture"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> if the texture is not valid or wasnot created withSDL_TEXTUREACCESS_STREAMING; call <see cref="GetError()"/> for more information.</returns>

    public static bool LockTexture(nint texture, nint rect, out nint pixels,
                out int pitch) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        Rect trect = Marshal.PtrToStructure<Rect>(rect);
        return SDL_LockTexture(texture, ref trect, out pixels, out pitch);
    }

    /// <summary>Lock a portion of the texture for write-only pixel access, and expose it as a SDL surface.</summary>

    /// <param name="texture">the texture to lock for access, which must be created with SDL_TEXTUREACCESS_STREAMING.</param>
    /// <param name="rect">a pointer to the rectangle to lock for access. If the rect is <see langword="null" />, the entire texture will be locked.</param>
    /// <param name="surface">a pointer to an SDL surface of size rect. Don't assume any specific pixel content.</param>
    /// <remarks>
    /// Besides providing an SDL_Surface instead of raw pixel data,
    /// this function operates like SDL_LockTexture.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LockTexture"/>
    /// <seealso cref="UnlockTexture"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool LockTextureToSurface(nint texture, ref Rect rect,
                out nint surface) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_LockTextureToSurface(texture, ref rect, out surface);
    }

    /// <summary>Set an additional alpha value multiplied into render copy operations.</summary>

    /// <param name="texture">the texture to update.</param>
    /// <param name="alpha">the source alpha value multiplied into copy operations.</param>
    /// <remarks>
    /// When this texture is rendered, during the copy operation the source alpha
    /// value is modulated by this alpha value according to the following formula:
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetTextureAlphaMod"/>
    /// <seealso cref="SetTextureAlphaModFloat"/>
    /// <seealso cref="SetTextureColorMod"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool SetTextureAlphaMod(nint texture, byte alpha) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_SetTextureAlphaMod(texture, alpha);
    }

    /// <summary>Set an additional alpha value multiplied into render copy operations.</summary>

    /// <param name="texture">the texture to update.</param>
    /// <param name="alpha">the source alpha value multiplied into copy operations.</param>
    /// <remarks>
    /// When this texture is rendered, during the copy operation the source alpha
    /// value is modulated by this alpha value according to the following formula:
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetTextureAlphaModFloat"/>
    /// <seealso cref="SetTextureAlphaMod"/>
    /// <seealso cref="SetTextureColorModFloat"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool SetTextureAlphaModFloat(nint texture, float alpha) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_SetTextureAlphaModFloat(texture, alpha);
    }

    /// <summary>Set the blend mode for a texture, used by SDL_RenderTexture().</summary>

    /// <param name="texture">the texture to update.</param>
    /// <param name="blendMode">the <see cref="BlendMode"/> to use for texture blending.</param>
    /// <remarks>
    /// If the blend mode is not supported, the closest supported mode is chosen
    /// and this function returns <see langword="false" />.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetTextureBlendMode"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool SetTextureBlendMode(nint texture, uint blendMode) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_SetTextureBlendMode(texture, blendMode);
    }

    /// <summary>Set an additional color value multiplied into render copy operations.</summary>

    /// <param name="texture">the texture to update.</param>
    /// <param name="r">the red color value multiplied into copy operations.</param>
    /// <param name="g">the green color value multiplied into copy operations.</param>
    /// <param name="b">the blue color value multiplied into copy operations.</param>
    /// <remarks>
    /// When this texture is rendered, during the copy operation each source color
    /// channel is modulated by the appropriate color value according to the
    /// following formula:
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetTextureColorMod"/>
    /// <seealso cref="SetTextureAlphaMod"/>
    /// <seealso cref="SetTextureColorModFloat"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool SetTextureColorMod(nint texture, byte r, byte g, byte b) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_SetTextureColorMod(texture, r, g, b);
    }

    /// <summary>Set an additional color value multiplied into render copy operations.</summary>

    /// <param name="texture">the texture to update.</param>
    /// <param name="r">the red color value multiplied into copy operations.</param>
    /// <param name="g">the green color value multiplied into copy operations.</param>
    /// <param name="b">the blue color value multiplied into copy operations.</param>
    /// <remarks>
    /// When this texture is rendered, during the copy operation each source color
    /// channel is modulated by the appropriate color value according to the
    /// following formula:
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetTextureColorMod"/>
    /// <seealso cref="SetTextureAlphaMod"/>
    /// <seealso cref="SetTextureColorModFloat"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool SetTextureColorMod(nint texture, Color color) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_SetTextureColorMod(texture, color.R, color.G, color.B);
    }

    /// <summary>Set an additional color value multiplied into render copy operations.</summary>

    /// <param name="texture">the texture to update.</param>
    /// <param name="r">the red color value multiplied into copy operations.</param>
    /// <param name="g">the green color value multiplied into copy operations.</param>
    /// <param name="b">the blue color value multiplied into copy operations.</param>
    /// <remarks>
    /// When this texture is rendered, during the copy operation each source color
    /// channel is modulated by the appropriate color value according to the
    /// following formula:
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetTextureColorModFloat"/>
    /// <seealso cref="SetTextureAlphaModFloat"/>
    /// <seealso cref="SetTextureColorMod"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool SetTextureColorModFloat(nint texture, float r, float g, float b) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_SetTextureColorModFloat(texture, r, g, b);
    }

    /// <summary>Set an additional color value multiplied into render copy operations.</summary>

    /// <param name="texture">the texture to update.</param>
    /// <param name="r">the red color value multiplied into copy operations.</param>
    /// <param name="g">the green color value multiplied into copy operations.</param>
    /// <param name="b">the blue color value multiplied into copy operations.</param>
    /// <remarks>
    /// When this texture is rendered, during the copy operation each source color
    /// channel is modulated by the appropriate color value according to the
    /// following formula:
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetTextureColorModFloat"/>
    /// <seealso cref="SetTextureAlphaModFloat"/>
    /// <seealso cref="SetTextureColorMod"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool SetTextureColorModFloat(nint texture, FColor color) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_SetTextureColorModFloat(texture, color.R, color.G, color.B);
    }

    /// <summary>Set the scale mode used for texture scale operations.</summary>

    /// <param name="texture">the texture to update.</param>
    /// <param name="scaleMode">the <see cref="ScaleMode"/> to use for texture scaling.</param>
    /// <remarks>
    /// The default texture scale mode is
    /// SDL_SCALEMODE_LINEAR.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetTextureScaleMode"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool SetTextureScaleMode(nint texture, ScaleMode scaleMode) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_SetTextureScaleMode(texture, scaleMode);
    }

    /// <summary>Unlock a texture, uploading the changes to video memory, if needed.</summary>

    /// <param name="texture">a texture locked by SDL_LockTexture().</param>
    /// <remarks>
    /// Warning: Please note that SDL_LockTexture() is
    /// intended to be write-only; it will not guarantee the previous contents of
    /// the texture will be provided. You must fully initialize any area of a
    /// texture that you lock before unlocking it, as the pixels might otherwise be
    /// uninitialized memory.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LockTexture"/>
    /// </remarks>

    public static void UnlockTexture(nint texture) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        SDL_UnlockTexture(texture);
    }

    /// <summary>Update a rectangle within a planar NV12 or NV21 texture with new pixels.</summary>

    /// <param name="texture">the texture to update.</param>
    /// <param name="rect">a pointer to the rectangle of pixels to update, or <see langword="null" /> to update the entire texture.</param>
    /// <param name="Yplane">the raw pixel data for the Y plane.</param>
    /// <param name="Ypitch">the number of bytes between rows of pixel data for the Y plane.</param>
    /// <param name="UVplane">the raw pixel data for the UV plane.</param>
    /// <param name="UVpitch">the number of bytes between rows of pixel data for the UV plane.</param>
    /// <remarks>
    /// You can use SDL_UpdateTexture() as long as your pixel
    /// data is a contiguous block of NV12/21 planes in the proper order, but this
    /// function is available if your pixel data is not contiguous.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="UpdateTexture"/>
    /// <seealso cref="UpdateYUVTexture"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool UpdateNVTexture(nint texture, ref Rect rect, nint yplane, int ypitch,
                nint uVplane, int uVpitch) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_UpdateNVTexture(texture, ref rect, yplane, ypitch, uVplane, uVpitch);
    }

    /// <summary>Update the given texture rectangle with new pixel data.</summary>

    /// <param name="texture">the texture to update.</param>
    /// <param name="rect">an <see cref="Rect"/> structure representing the area to update, or <see langword="null" /> to update the entire texture.</param>
    /// <param name="pixels">the raw pixel data in the format of the texture.</param>
    /// <param name="pitch">the number of bytes in a row of pixel data, including padding between lines.</param>
    /// <remarks>
    /// The pixel data must be in the pixel format of the texture, which can be
    /// queried using the
    /// SDL_PROP_TEXTURE_FORMAT_NUMBER property.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LockTexture"/>
    /// <seealso cref="UnlockTexture"/>
    /// <seealso cref="UpdateNVTexture"/>
    /// <seealso cref="UpdateYUVTexture"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool UpdateTexture(nint texture, ref Rect rect, nint pixels, int pitch) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_UpdateTexture(texture, ref rect, pixels, pitch);
    }

    /// <summary>Update a rectangle within a planar YV12 or IYUV texture with new pixel data.</summary>

    /// <param name="texture">the texture to update.</param>
    /// <param name="rect">a pointer to the rectangle of pixels to update, or <see langword="null" /> to update the entire texture.</param>
    /// <param name="Yplane">the raw pixel data for the Y plane.</param>
    /// <param name="Ypitch">the number of bytes between rows of pixel data for the Y plane.</param>
    /// <param name="Uplane">the raw pixel data for the U plane.</param>
    /// <param name="Upitch">the number of bytes between rows of pixel data for the U plane.</param>
    /// <param name="Vplane">the raw pixel data for the V plane.</param>
    /// <param name="Vpitch">the number of bytes between rows of pixel data for the V plane.</param>
    /// <remarks>
    /// You can use SDL_UpdateTexture() as long as your pixel
    /// data is a contiguous block of Y and U/V planes in the proper order, but
    /// this function is available if your pixel data is not contiguous.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="UpdateNVTexture"/>
    /// <seealso cref="UpdateTexture"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool UpdateYUVTexture(nint texture, ref Rect rect, nint yplane, int ypitch,
                nint uplane, int upitch, nint vplane, int vpitch) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Render, "Texture is null");
        }
        return SDL_UpdateYUVTexture(texture, ref rect, yplane, ypitch, uplane, upitch, vplane, vpitch);
    }

    // nint refers to a nint
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateTexture(nint renderer, PixelFormat format, TextureAccess access,
        int w, int h);

    // nint refers to a nint
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateTextureFromSurface(nint renderer, nint surface);

    // nint refers to a nint
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