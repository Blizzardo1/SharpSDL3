using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SharpSDL3;

public static unsafe partial class Sdl {
    /// <summary>Add a set of synchronization semaphores for the current frame.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="wait_stage_mask">the VkPipelineStageFlags for the wait.</param>
    /// <param name="wait_semaphore">a VkSempahore to wait on before rendering the current frame, or 0 if not needed.</param>
    /// <param name="signal_semaphore">a VkSempahore that SDL will signal when rendering for the current frame is complete, or 0 if not needed.</param>
    /// <remarks>
    /// The Vulkan renderer will wait for wait_semaphore before submitting
    /// rendering commands and signal signal_semaphore after rendering commands
    /// are complete for this frame.
    /// <para><strong>Thread Safety</strong>: It is NOT safe to call this function from two threads at once.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool AddVulkanRenderSemaphores(nint renderer, uint waitStageMask, long waitSemaphore,
            long signalSemaphore) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_AddVulkanRenderSemaphores(renderer, waitStageMask, waitSemaphore, signalSemaphore);
        if (!result) {
            LogError(LogCategory.Error, "Failed to add Vulkan render semaphores");
        }
        return result;
    }

    /// <summary>Convert the coordinates in an event to render coordinates.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="event">the event to modify.</param>
    /// <remarks>
    /// This takes into account several states:
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderCoordinatesFromWindow" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool ConvertEventToRenderCoordinates(nint renderer, ref Event @event) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }

        Event* eventPtr = (Event*)Marshal.AllocHGlobal(Marshal.SizeOf<Event>());
        bool result = SDL_ConvertEventToRenderCoordinates(renderer, ref eventPtr);

        if (!result) {
            LogError(LogCategory.Error, "Failed to convert event to render coordinates");
        }

        @event = *eventPtr; // Update the original event with the modified data
        return result;
    }

    /// <summary>Create a 2D rendering context for a window.</summary>
    /// <param name="window">the window where rendering is displayed.</param>
    /// <param name="name">the name of the rendering driver to initialize, or <see langword="null" /> to let SDL choose one.</param>
    /// <remarks>
    /// If you want a specific renderer, you can specify its name here. A list of
    /// available renderers can be obtained by calling
    /// SDL_GetRenderDriver() multiple times, with indices
    /// from 0 to SDL_GetNumRenderDrivers()-1. If you
    /// don't need a specific renderer, specify <see langword="null" /> and SDL will attempt to choose
    /// the best option for you, based on what is available on the user's system.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateRendererWithProperties" />
    /// <seealso cref="CreateSoftwareRenderer" />
    /// <seealso cref="DestroyRenderer" />
    /// <seealso cref="GetNumRenderDrivers" />
    /// <seealso cref="GetRenderDriver" />
    /// <seealso cref="GetRendererName" />
    /// </remarks>
    /// <returns>(SDL_Renderer *) Returns a valid rendering context or <see langword="null" />if there was an error; call <see cref="GetError()" /> for more information.</returns>

    public static nint CreateRenderer(nint window, string? name) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "Window is null");
            return nint.Zero;
        }

        nint result = SDL_CreateRenderer(window, name);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, $"Failed to create renderer: {GetError()}");
        }
        return result;
    }

    /// <summary>Create a 2D rendering context for a window, with the specified properties.</summary>

    /// <param name="props">the properties to use.</param>
    /// <remarks>
    /// These are the supported properties:
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateProperties" />
    /// <seealso cref="CreateRenderer" />
    /// <seealso cref="CreateSoftwareRenderer" />
    /// <seealso cref="DestroyRenderer" />
    /// <seealso cref="GetRendererName" />
    /// </remarks>
    /// <returns>(SDL_Renderer *) Returns a valid rendering context or <see langword="null" />if there was an error; call <see cref="GetError()" /> for more information.</returns>

    public static nint CreateRendererWithProperties(uint props) {
        nint result = SDL_CreateRendererWithProperties(props);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "Failed to create renderer with properties");
        }
        return result;
    }

    /// <summary>Create a 2D software rendering context for a surface.</summary>

    /// <param name="surface">the <see cref="Surface" /> structure representing the surface where rendering is done.</param>
    /// <remarks>
    /// Two other API which can be used to create SDL_Renderer:
    /// SDL_CreateRenderer() and
    /// SDL_CreateWindowAndRenderer(). These can
    /// also create a software renderer, but they are intended to be used with an
    /// SDL_Window as the final destination and not an
    /// SDL_Surface.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="DestroyRenderer" />
    /// </remarks>
    /// <returns>(SDL_Renderer *) Returns a valid rendering context or <see langword="null" />if there was an error; call <see cref="GetError()" /> for more information.</returns>

    public static nint CreateSoftwareRenderer(nint surface) {
        if (surface == nint.Zero) {
            LogError(LogCategory.Error, "Surface is null");
            return nint.Zero;
        }
        nint result = SDL_CreateSoftwareRenderer(surface);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "Failed to create software renderer");
        }
        return result;
    }

    public static bool CreateWindowAndRenderer(string title, int width, int height, WindowFlags windowFlags,
        out nint window, out nint renderer) {
        if (string.IsNullOrEmpty(title)) {
            LogError(LogCategory.Error, "Title is null or empty");
            window = nint.Zero;
            renderer = nint.Zero;
            return false;
        }
        SdlBool result = SDL_CreateWindowAndRenderer(title, width, height, windowFlags, out window, out renderer);
        if (!result) {
            LogError(LogCategory.Error, "Failed to create window and renderer");
        }
        return result;
    }

    /// <summary>Create a window and default renderer.</summary>

    /// <param name="title">the title of the window, in UTF-8 encoding.</param>
    /// <param name="width">the width of the window.</param>
    /// <param name="height">the height of the window.</param>
    /// <param name="window_flags">the flags used to create the window (see SDL_CreateWindow()).</param>
    /// <param name="window">a pointer filled with the window, or <see langword="null" /> on error.</param>
    /// <param name="renderer">a pointer filled with the renderer, or <see langword="null" /> on error.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateRenderer" />
    /// <seealso cref="CreateWindow" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint CreateWindowAndRenderer(string title, int width, int height,
            WindowFlags windowFlags, out nint renderer) {
        if (string.IsNullOrEmpty(title)) {
            LogError(LogCategory.Error, "Title is null or empty");
            renderer = nint.Zero;
            return nint.Zero;
        }
        SdlBool result = CreateWindowAndRenderer(title, width, height, windowFlags, out nint window, out renderer);
        if (!result) {
            LogError(LogCategory.Error, "Failed to create window and renderer");
        }
        return window;
    }

    /// <summary>Destroy the rendering context for a window and free all associated textures.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <remarks>
    /// This should be called before destroying the associated window.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateRenderer" />
    /// </remarks>

    public static void DestroyRenderer(nint renderer) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SDL_DestroyRenderer(renderer);
    }

    /// <summary>Destroy the specified texture.</summary>

    /// <param name="texture">the texture to destroy.</param>
    /// <remarks>
    /// Passing <see langword="null" /> or an otherwise invalid texture will set the SDL error message
    /// to &quot;Invalid texture&quot;.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateTexture" />
    /// <seealso cref="CreateTextureFromSurface" />
    /// </remarks>

    public static void DestroyTexture(nint texture) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Error, "Texture is null");
            return;
        }
        SDL_DestroyTexture(texture);
    }

    /// <summary>Force the rendering context to flush any pending commands and state.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <remarks>
    /// You do not need to (and in fact, shouldn't) call this function unless you
    /// are planning to call into OpenGL/Direct3D/Metal/whatever directly, in
    /// addition to using an SDL_Renderer.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool FlushRenderer(nint renderer) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_FlushRenderer(renderer);
        if (!result) {
            LogError(LogCategory.Error, "Failed to flush render");
        }
        return result;
    }

    public static bool GetCurrentRenderOutputSize(nint renderer, out int w, out int h) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_GetCurrentRenderOutputSize(renderer, out w, out h);
        if (!result) {
            LogError(LogCategory.Error, "Failed to get current render output size");
        }
        return result;
    }

    /// <summary>Get the current output size in pixels of a rendering context.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="w">a pointer filled in with the current width.</param>
    /// <param name="h">a pointer filled in with the current height.</param>
    /// <remarks>
    /// If a rendering target is active, this will return the size of the rendering
    /// target in pixels, otherwise return the value of
    /// SDL_GetRenderOutputSize().
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRenderOutputSize" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static Rect GetCurrentRenderOutputSize(nint renderer) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        _ = GetCurrentRenderOutputSize(renderer, out int w, out int h);
        return new() { W = w, H = h };
    }

    /// <summary>Get the number of 2D rendering drivers available for the current display.</summary>
    /// <remarks>
    /// A render driver is a set of code that handles rendering and texture
    /// management on a particular display. Normally there is only one, but some
    /// drivers may have several available with different capabilities.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateRenderer" />
    /// <seealso cref="GetRenderDriver" />
    /// </remarks>
    /// <returns>Returns the number of built in render drivers.</returns>

    public static int GetNumRenderDrivers() {
        int result = SDL_GetNumRenderDrivers();
        if (result < 0) {
            LogError(LogCategory.Error, "Failed to get number of render drivers");
        }
        return result;
    }

    public static bool GetRenderClipRect(nint renderer, out Rect rect) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_GetRenderClipRect(renderer, out rect);
        if (!result) {
            LogError(LogCategory.Error, "Failed to get render clip rect");
        }
        return result;
    }

    /// <summary>Get the clip rectangle for the current target.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="rect">an <see cref="Rect" /> structure filled in with the current clipping area or an empty rectangle if clipping is disabled.</param>
    /// <remarks>
    /// Each render target has its own clip rectangle. This function gets the
    /// cliprect for the current render target.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderClipEnabled" />
    /// <seealso cref="SetRenderClipRect" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static Rect GetRenderClipRect(nint renderer) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        _ = GetRenderClipRect(renderer, out Rect rect);
        return rect;
    }

    public static bool GetRenderColorScale(nint renderer, out float scale) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_GetRenderColorScale(renderer, out scale);
        if (!result) {
            LogError(LogCategory.Error, "Failed to get render color scale");
        }
        return result;
    }

    /// <summary>Get the color scale used for render operations.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="scale">a pointer filled in with the current color scale value.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetRenderColorScale" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static float GetRenderColorScale(nint renderer) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        _ = GetRenderColorScale(renderer, out float scale);
        return scale;
    }

    /// <summary>Get the blend mode used for drawing operations.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="blendMode">a pointer filled in with the current <see cref="BlendMode" />.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetRenderDrawBlendMode" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool GetRenderDrawBlendMode(nint renderer, nint blendMode) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_GetRenderDrawBlendMode(renderer, blendMode);
        if (!result) {
            LogError(LogCategory.Error, "Failed to get render draw blend mode");
        }
        return result;
    }

    /// <summary>Get the color used for drawing operations (Rect, Line and Clear).</summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="r">a pointer filled in with the red value used to draw on the rendering target.</param>
    /// <param name="g">a pointer filled in with the green value used to draw on the rendering target.</param>
    /// <param name="b">a pointer filled in with the blue value used to draw on the rendering target.</param>
    /// <param name="a">a pointer filled in with the alpha value used to draw on the rendering target; usually SDL_ALPHA_OPAQUE (255).</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRenderDrawColorFloat" />
    /// <seealso cref="SetRenderDrawColor" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool GetRenderDrawColor(nint renderer, out byte r, out byte g, out byte b, out byte a) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_GetRenderDrawColor(renderer, out r, out g, out b, out a);
        if (!result) {
            LogError(LogCategory.Error, "Failed to get render draw color");
        }
        return result;
    }

    /// <summary>Get the color used for drawing operations (Rect, Line and Clear).</summary>
    /// <param name="renderer">the rendering context.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRenderDrawColorFloat" />
    /// <seealso cref="SetRenderDrawColor" />
    /// </remarks>
    /// <returns>Returns a <see cref="Color" /> on success or a blank <see cref="Color" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static Color GetRenderDrawColor(nint renderer) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        _ = GetRenderDrawColor(renderer, out byte r, out byte g, out byte b, out byte a);
        return new() { R = r, G = g, B = b, A = a };
    }

    public static bool GetRenderDrawColorFloat(nint renderer, out float r, out float g, out float b, out float a) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_GetRenderDrawColorFloat(renderer, out r, out g, out b, out a);
        if (!result) {
            LogError(LogCategory.Error, "Failed to get render draw color float");
        }
        return result;
    }

    /// <summary>Get the color used for drawing operations (Rect, Line and Clear).</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="r">a pointer filled in with the red value used to draw on the rendering target.</param>
    /// <param name="g">a pointer filled in with the green value used to draw on the rendering target.</param>
    /// <param name="b">a pointer filled in with the blue value used to draw on the rendering target.</param>
    /// <param name="a">a pointer filled in with the alpha value used to draw on the rendering target.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetRenderDrawColorFloat" />
    /// <seealso cref="GetRenderDrawColor" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static FColor GetRenderDrawColorFloat(nint renderer) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        _ = GetRenderDrawColorFloat(renderer, out float r, out float g, out float b, out float a);
        return new() { R = r, G = g, B = b, A = a };
    }

    /// <summary>Use this function to get the name of a built in 2D rendering driver.</summary>

    /// <param name="index">the index of the rendering driver; the value ranges from 0 to SDL_GetNumRenderDrivers() - 1.</param>
    /// <remarks>
    /// The list of rendering drivers is given in the order that they are normally
    /// initialized by default; the drivers that seem more reasonable to choose
    /// first (as far as the SDL developers believe) are earlier in the list.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetNumRenderDrivers" />
    /// </remarks>
    /// <returns>Returns the name of the rendering driver at the requestedindex, or <see langword="null" /> if an invalid index was specified.</returns>

    public static string GetRenderDriver(int index) {
        if (index < 0) {
            LogError(LogCategory.Error, "Index is negative");
            return string.Empty;
        }
        string result = SDL_GetRenderDriver(index);
        if (string.IsNullOrEmpty(result)) {
            LogError(LogCategory.Error, "Failed to get render driver");
        }
        return result;
    }

    /// <summary>Get the renderer associated with a window.</summary>

    /// <param name="window">the window to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(SDL_Renderer *) Returns the rendering context on successor <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint GetRenderer(nint window) {
        if (window == nint.Zero) {
            LogError(LogCategory.Error, "Window is null");
            return nint.Zero;
        }
        nint result = SDL_GetRenderer(window);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "Failed to get renderer");
        }
        return result;
    }

    /// <summary>Get the renderer that created an SDL_Texture.</summary>

    /// <param name="texture">the texture to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(SDL_Renderer *) Returns a pointer to theSDL_Renderer that created the texture, or <see langword="null" /> on failure;call <see cref="GetError()" /> for more information.</returns>

    public static nint GetRendererFromTexture(nint texture) {
        if (texture == nint.Zero) {
            LogError(LogCategory.Error, "Texture is null");
            return nint.Zero;
        }
        nint result = SDL_GetRendererFromTexture(texture);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "Failed to get renderer from texture");
        }
        return result;
    }

    /// <summary>Get the name of a renderer.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateRenderer" />
    /// <seealso cref="CreateRendererWithProperties" />
    /// </remarks>
    /// <returns>Returns the name of the selected renderer, or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static string GetRendererName(nint renderer) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        string result = SDL_GetRendererName(renderer);
        if (string.IsNullOrEmpty(result)) {
            LogError(LogCategory.Error, "Failed to get renderer name");
        }
        return result;
    }

    /// <summary>Get the properties associated with a renderer.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <remarks>
    /// The following read-only properties are provided by SDL:
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns a valid property ID on success or 0 on failure; call <see cref="GetError()" /> for more information.</returns>

    public static uint GetRendererProperties(nint renderer) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        uint result = SDL_GetRendererProperties(renderer);
        if (result == 0) {
            LogError(LogCategory.Error, "Failed to get renderer properties");
        }
        return result;
    }

    public static bool GetRenderLogicalPresentation(nint renderer, out int w, out int h,
        out RendererLogicalPresentation mode) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_GetRenderLogicalPresentation(renderer, out w, out h, out mode);
        if (!result) {
            LogError(LogCategory.Error, "Failed to get render logical presentation");
        }
        return result;
    }

    /// <summary>Get device independent resolution and presentation mode for rendering.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="w">an int to be filled with the width.</param>
    /// <param name="h">an int to be filled with the height.</param>
    /// <param name="mode">the presentation mode used.</param>
    /// <remarks>
    /// This function gets the width and height of the logical rendering output, or
    /// the output size in pixels if a logical resolution is not enabled.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetRenderLogicalPresentation" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static Rect GetRenderLogicalPresentation(nint renderer, out RendererLogicalPresentation mode) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = GetRenderLogicalPresentation(renderer, out int w, out int h, out mode);
        if (!result) {
            LogError(LogCategory.Error, "Failed to get render logical presentation");
        }
        return new() { W = w, H = h };
    }

    public static bool GetRenderLogicalPresentationRect(nint renderer, out FRect rect) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_GetRenderLogicalPresentationRect(renderer, out rect);
        if (!result) {
            LogError(LogCategory.Error, "Failed to get render logical presentation rect");
        }
        return result;
    }

    /// <summary>Get the final presentation rectangle for rendering.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="rect">a pointer filled in with the final presentation rectangle, may be discarded.</param>
    /// <remarks>
    /// This function returns the calculated rectangle used for logical
    /// presentation, based on the presentation mode and output size. If logical
    /// presentation is disabled, it will fill the rectangle with the output size,
    /// in pixels.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetRenderLogicalPresentation" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static FRect GetRenderLogicalPresentationRect(nint renderer) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_GetRenderLogicalPresentationRect(renderer, out FRect rect);
        if (!result) {
            LogError(LogCategory.Error, "Failed to get render logical presentation rect");
        }
        return rect;
    }

    /// <summary>Get the Metal command encoder for the current frame.</summary>

    /// <param name="renderer">the renderer to query.</param>
    /// <remarks>
    /// This function returns void *, so SDL doesn't have to include Metal's
    /// headers, but it can be safely cast to an id&lt;MTLRenderCommandEncoder&gt;.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRenderMetalLayer" />
    /// </remarks>
    /// <returns>(void *) Returns an id&lt;MTLRenderCommandEncoder&gt; on success, or <see langword="null" /> ifthe renderer isn't a Metal renderer or there was an error.</returns>

    public static nint GetRenderMetalCommandEncoder(nint renderer) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        nint result = SDL_GetRenderMetalCommandEncoder(renderer);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "Failed to get render metal command encoder");
        }
        return result;
    }

    /// <summary>Get the CAMetalLayer associated with the given Metal renderer.</summary>

    /// <param name="renderer">the renderer to query.</param>
    /// <remarks>
    /// This function returns void *, so SDL doesn't have to include Metal's
    /// headers, but it can be safely cast to a CAMetalLayer *.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRenderMetalCommandEncoder" />
    /// </remarks>
    /// <returns>(void *) Returns a CAMetalLayer * on success, or <see langword="null" /> if the rendererisn't a Metal renderer.</returns>

    public static nint GetRenderMetalLayer(nint renderer) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        nint result = SDL_GetRenderMetalLayer(renderer);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "Failed to get render metal layer");
        }
        return result;
    }

    public static bool GetRenderOutputSize(nint renderer, out int w, out int h) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_GetRenderOutputSize(renderer, out w, out h);
        if (!result) {
            LogError(LogCategory.Error, "Failed to get render output size");
        }
        return result;
    }

    /// <summary>Get the output size in pixels of a rendering context.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="w">a pointer filled in with the width in pixels.</param>
    /// <param name="h">a pointer filled in with the height in pixels.</param>
    /// <remarks>
    /// This returns the <see langword="true" /> output size in pixels, ignoring any render targets or
    /// logical size and presentation.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetCurrentRenderOutputSize" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static Rect GetRenderOutputSize(nint renderer) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        _ = GetRenderOutputSize(renderer, out int w, out int h);
        return new() { W = w, H = h };
    }

    public static bool GetRenderSafeArea(nint renderer, out Rect rect) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_GetRenderSafeArea(renderer, out rect);
        if (!result) {
            LogError(LogCategory.Error, "Failed to get render safe area");
        }
        return result;
    }

    /// <summary>Get the safe area for rendering within the current viewport.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="rect">a pointer filled in with the area that is safe for interactive content.</param>
    /// <remarks>
    /// Some devices have portions of the screen which are partially obscured or
    /// not interactive, possibly due to on-screen controls, curved edges, camera
    /// notches, TV overscan, etc. This function provides the area of the current
    /// viewport which is safe to have interactible content. You should continue
    /// rendering into the rest of the render target, but it should not contain
    /// visually important or interactible content.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static Rect GetRenderSafeArea(nint renderer) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        _ = GetRenderSafeArea(renderer, out Rect rect);
        return rect;
    }

    public static bool GetRenderScale(nint renderer, out float scaleX, out float scaleY) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_GetRenderScale(renderer, out scaleX, out scaleY);
        if (!result) {
            LogError(LogCategory.Error, "Failed to get render scale");
        }
        return result;
    }

    /// <summary>Get the drawing scale for the current target.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="scaleX">a pointer filled in with the horizontal scaling factor.</param>
    /// <param name="scaleY">a pointer filled in with the vertical scaling factor.</param>
    /// <remarks>
    /// Each render target has its own scale. This function gets the scale for the
    /// current render target.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetRenderScale" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static FPoint GetRenderScale(nint renderer) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        _ = GetRenderScale(renderer, out float scaleX, out float scaleY);
        return new() { X = scaleX, Y = scaleY };
    }

    /// <summary>Get the current render target.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <remarks>
    /// The default render target is the window for which the renderer was created,
    /// and is reported a <see langword="null" /> here.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetRenderTarget" />
    /// </remarks>
    /// <returns>(SDL_Texture *) Returns the current render target or <see langword="null" />for the default render target.</returns>

    public static nint GetRenderTarget(nint renderer) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        nint result = SDL_GetRenderTarget(renderer);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "Failed to get render target");
        }
        return result;
    }

    /// <summary>Get the drawing area for the current target.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="rect">an <see cref="Rect" /> structure filled in with the current drawing area.</param>
    /// <remarks>
    /// Each render target has its own viewport. This function gets the viewport
    /// for the current render target.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderViewportSet" />
    /// <seealso cref="SetRenderViewport" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool GetRenderViewport(nint renderer, out Rect rect) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_GetRenderViewport(renderer, out rect);
        if (!result) {
            LogError(LogCategory.Error, "Failed to get render viewport");
        }
        return result;
    }

    /// <summary>Get VSync of the given renderer.</summary>

    /// <param name="renderer">the renderer to toggle.</param>
    /// <param name="vsync">an int filled with the current vertical refresh sync interval. See SDL_SetRenderVSync() for the meaning of the value.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetRenderVSync" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static int GetRenderVsync(nint renderer) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        _ = GetRenderVSync(renderer, out int vsync);
        return vsync;
    }

    /// <summary>Get VSync of the given renderer.</summary>

    /// <param name="renderer">the renderer to toggle.</param>
    /// <param name="vsync">an int filled with the current vertical refresh sync interval. See SDL_SetRenderVSync() for the meaning of the value.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetRenderVSync" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool GetRenderVSync(nint renderer, out int vsync) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_GetRenderVSync(renderer, out vsync);
        if (!result) {
            LogError(LogCategory.Error, "Failed to get render VSync");
        }
        return result;
    }

    /// <summary>Get the window associated with a renderer.</summary>

    /// <param name="renderer">the renderer to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(SDL_Window *) Returns the window on success or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint GetRenderWindow(nint renderer) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        nint result = SDL_GetRenderWindow(renderer);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "Failed to get render window");
        }
        return result;
    }

    /// <summary>Clear the current rendering target with the drawing color.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <remarks>
    /// This function clears the entire rendering target, ignoring the viewport and
    /// the clip rectangle. Note, that clearing will also set/fill all pixels of
    /// the rendering target to current renderer draw color, so make sure to invoke
    /// SDL_SetRenderDrawColor() when needed.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetRenderDrawColor" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderClear(nint renderer) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_RenderClear(renderer);
        if (!result) {
            LogError(LogCategory.Error, "Failed to clear render");
        }
        return result;
    }

    /// <summary>Get whether clipping is enabled on the given render target.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <remarks>
    /// Each render target has its own clip rectangle. This function checks the
    /// cliprect for the current render target.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRenderClipRect" />
    /// <seealso cref="SetRenderClipRect" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if clipping is enabled or <see langword="false" /> if not; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderClipEnabled(nint renderer) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_RenderClipEnabled(renderer);
        if (!result) {
            LogError(LogCategory.Error, "Failed to get render clip enabled");
        }
        return result;
    }

    public static SdlBool RenderCoordinatesFromWindow(nint renderer, float windowX, float windowY,
        out float x, out float y) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_RenderCoordinatesFromWindow(renderer, windowX, windowY, out x, out y);
        if (!result) {
            LogError(LogCategory.Error, "Failed to convert coordinates from window");
        }
        return result;
    }

    /// <summary>Get a point in render coordinates when given a point in window coordinates.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="window_x">the x coordinate in window coordinates.</param>
    /// <param name="window_y">the y coordinate in window coordinates.</param>
    /// <param name="x">a pointer filled with the x coordinate in render coordinates.</param>
    /// <param name="y">a pointer filled with the y coordinate in render coordinates.</param>
    /// <remarks>
    /// This takes into account several states:
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetRenderLogicalPresentation" />
    /// <seealso cref="SetRenderScale" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static FPoint RenderCoordinatesFromWindow(nint renderer, FPoint windowPoint) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_RenderCoordinatesFromWindow(renderer, windowPoint.X, windowPoint.Y, out float x, out float y);
        if (!result) {
            LogError(LogCategory.Error, "Failed to convert coordinates from window");
        }
        return new() { X = x, Y = y };
    }

    public static bool RenderCoordinatesToWindow(nint renderer, float x, float y, out float windowX,
        out float windowY) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_RenderCoordinatesToWindow(renderer, x, y, out windowX, out windowY);
        if (!result) {
            LogError(LogCategory.Error, "Failed to convert coordinates to window");
        }
        return result;
    }

    /// <summary>Get a point in window coordinates when given a point in render coordinates.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="x">the x coordinate in render coordinates.</param>
    /// <param name="y">the y coordinate in render coordinates.</param>
    /// <param name="window_x">a pointer filled with the x coordinate in window coordinates.</param>
    /// <param name="window_y">a pointer filled with the y coordinate in window coordinates.</param>
    /// <remarks>
    /// This takes into account several states:
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetRenderLogicalPresentation" />
    /// <seealso cref="SetRenderScale" />
    /// <seealso cref="SetRenderViewport" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static FPoint RenderCoordinatesToWindow(nint renderer, float x, float y) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        _ = RenderCoordinatesToWindow(renderer, x, y, out float windowX, out float windowY);
        return new() { X = windowX, Y = windowY };
    }

    /// <summary>Get a point in window coordinates when given a point in render coordinates.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="x">the x coordinate in render coordinates.</param>
    /// <param name="y">the y coordinate in render coordinates.</param>
    /// <param name="window_x">a pointer filled with the x coordinate in window coordinates.</param>
    /// <param name="window_y">a pointer filled with the y coordinate in window coordinates.</param>
    /// <remarks>
    /// This takes into account several states:
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetRenderLogicalPresentation" />
    /// <seealso cref="SetRenderScale" />
    /// <seealso cref="SetRenderViewport" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static FPoint RenderCoordinatesToWindow(nint renderer, FPoint point) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        return RenderCoordinatesToWindow(renderer, point.X, point.Y);
    }

    /// <summary>Draw debug text to an SDL_Renderer.</summary>

    /// <param name="renderer">the renderer which should draw a line of text.</param>
    /// <param name="x">the x coordinate where the top-left corner of the text will draw.</param>
    /// <param name="y">the y coordinate where the top-left corner of the text will draw.</param>
    /// <param name="str">the string to render.</param>
    /// <remarks>
    /// This function will render a string of text to an
    /// SDL_Renderer. Note that this is a convenience function for
    /// debugging, with severe limitations, and not intended to be used for
    /// production apps and games.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderDebugTextFormat" />
    /// <seealso cref="DEBUG_TEXT_FONT_CHARACTER_SIZE" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderDebugText(nint renderer, float x, float y, string str) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_RenderDebugText(renderer, x, y, str);
        if (!result) {
            LogError(LogCategory.Error, "Failed to render debug text");
        }
        return result;
    }

    /// <summary>Draw debug text to an SDL_Renderer.</summary>

    /// <param name="renderer">the renderer which should draw a line of text.</param>
    /// <param name="x">the x coordinate where the top-left corner of the text will draw.</param>
    /// <param name="y">the y coordinate where the top-left corner of the text will draw.</param>
    /// <param name="str">the string to render.</param>
    /// <remarks>
    /// This function will render a string of text to an
    /// SDL_Renderer. Note that this is a convenience function for
    /// debugging, with severe limitations, and not intended to be used for
    /// production apps and games.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderDebugTextFormat" />
    /// <seealso cref="DEBUG_TEXT_FONT_CHARACTER_SIZE" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderDebugText(nint renderer, FPoint location, string str) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        return RenderDebugText(renderer, location.X, location.Y, str);
    }

    /// <summary>Draw debug text to an SDL_Renderer.</summary>

    /// <param name="renderer">the renderer which should draw the text.</param>
    /// <param name="x">the x coordinate where the top-left corner of the text will draw.</param>
    /// <param name="y">the y coordinate where the top-left corner of the text will draw.</param>
    /// <param name="fmt">the format string to draw.</param>
    /// <param name="...">additional parameters matching % tokens in the fmt string, if any.</param>
    /// <remarks>
    /// This function will render a printf()-style format string to a renderer.
    /// Note that this is a convinence function for debugging, with severe
    /// limitations, and is not intended to be used for production apps and games.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderDebugText" />
    /// <seealso cref="DEBUG_TEXT_FONT_CHARACTER_SIZE" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderDebugTextFormat(nint renderer, float x, float y, string fmt) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_RenderDebugTextFormat(renderer, x, y, fmt);
        if (!result) {
            LogError(LogCategory.Error, "Failed to render debug text format");
        }
        return result;
    }

    /// <summary>Draw debug text to an SDL_Renderer.</summary>

    /// <param name="renderer">the renderer which should draw the text.</param>
    /// <param name="x">the x coordinate where the top-left corner of the text will draw.</param>
    /// <param name="y">the y coordinate where the top-left corner of the text will draw.</param>
    /// <param name="fmt">the format string to draw.</param>
    /// <param name="...">additional parameters matching % tokens in the fmt string, if any.</param>
    /// <remarks>
    /// This function will render a printf()-style format string to a renderer.
    /// Note that this is a convinence function for debugging, with severe
    /// limitations, and is not intended to be used for production apps and games.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderDebugText" />
    /// <seealso cref="DEBUG_TEXT_FONT_CHARACTER_SIZE" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderDebugTextFormat(nint renderer, FPoint location, string fmt) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        return RenderDebugTextFormat(renderer, location.X, location.Y, fmt);
    }

    /// <summary>Fill a rectangle on the current rendering target with the drawing color at subpixel precision.</summary>

    /// <param name="renderer">the renderer which should fill a rectangle.</param>
    /// <param name="rect">a pointer to the destination rectangle, or <see langword="null" /> for the entire rendering target.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderFillRects" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderFillRect(nint renderer, ref FRect rect) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_RenderFillRect(renderer, ref rect);
        if (!result) {
            LogError(LogCategory.Error, "Failed to render fill rect");
        }
        return result;
    }

    /// <summary>Fill some number of rectangles on the current rendering target with the drawing color at subpixel precision.</summary>

    /// <param name="renderer">the renderer which should fill multiple rectangles.</param>
    /// <param name="rects">a pointer to an array of destination rectangles.</param>
    /// <param name="count">the number of rectangles.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderFillRect" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderFillRects(nint renderer, Span<FRect> rects) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_RenderFillRects(renderer, rects, rects.Length);
        if (!result) {
            LogError(LogCategory.Error, "Failed to render fill rects");
        }
        return result;
    }

    /// <summary>Fill some number of rectangles on the current rendering target with the drawing color at subpixel precision.</summary>

    /// <param name="renderer">the renderer which should fill multiple rectangles.</param>
    /// <param name="rects">a pointer to an array of destination rectangles.</param>
    /// <param name="count">the number of rectangles.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderFillRect" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderFillRects(nint renderer, FRect[] rects) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        return RenderFillRects(renderer, rects.AsSpan());
    }

    /// <summary>Render a list of triangles, optionally using a texture and indices into the vertex array Color and alpha modulation is done per vertex (SDL_SetTextureColorMod and SDL_SetTextureAlphaMod are ignored).</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="texture">(optional) The SDL texture to use.</param>
    /// <param name="vertices">vertices.</param>
    /// <param name="num_vertices">number of vertices.</param>
    /// <param name="indices">(optional) An array of integer indices into the 'vertices' array, if <see langword="null" /> all vertices will be rendered in sequential order.</param>
    /// <param name="num_indices">number of indices.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderGeometryRaw" />
    /// <seealso cref="SetRenderTextureAddressMode" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderGeometry(nint renderer, nint texture, Span<Vertex> vertices, Span<int> indices) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_RenderGeometry(renderer, texture, vertices, vertices.Length, indices,
            indices.Length);
        if (!result) {
            LogError(LogCategory.Error, "Failed to render geometry");
        }
        return result;
    }

    /// <summary>Render a list of triangles, optionally using a texture and indices into the vertex array Color and alpha modulation is done per vertex (SDL_SetTextureColorMod and SDL_SetTextureAlphaMod are ignored).</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="texture">(optional) The SDL texture to use.</param>
    /// <param name="vertices">vertices.</param>
    /// <param name="num_vertices">number of vertices.</param>
    /// <param name="indices">(optional) An array of integer indices into the 'vertices' array, if <see langword="null" /> all vertices will be rendered in sequential order.</param>
    /// <param name="num_indices">number of indices.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderGeometryRaw" />
    /// <seealso cref="SetRenderTextureAddressMode" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderGeometry(nint renderer, nint texture, Vertex[] vertices, int[] indices) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        return RenderGeometry(renderer, texture, vertices.AsSpan(), indices.AsSpan());
    }

    /// <summary>Render a list of triangles, optionally using a texture and indices into the vertex array Color and alpha modulation is done per vertex (SDL_SetTextureColorMod and SDL_SetTextureAlphaMod are ignored).</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="texture">(optional) The SDL texture to use.</param>
    /// <param name="vertices">vertices.</param>
    /// <param name="num_vertices">number of vertices.</param>
    /// <param name="indices">(optional) An array of integer indices into the 'vertices' array, if <see langword="null" /> all vertices will be rendered in sequential order.</param>
    /// <param name="num_indices">number of indices.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderGeometryRaw" />
    /// <seealso cref="SetRenderTextureAddressMode" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderGeometry(nint renderer, nint texture, Span<Vertex> vertices, int[] indices) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        return RenderGeometry(renderer, texture, vertices, indices.AsSpan());
    }

    /// <summary>Render a list of triangles, optionally using a texture and indices into the vertex array Color and alpha modulation is done per vertex (SDL_SetTextureColorMod and SDL_SetTextureAlphaMod are ignored).</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="texture">(optional) The SDL texture to use.</param>
    /// <param name="vertices">vertices.</param>
    /// <param name="num_vertices">number of vertices.</param>
    /// <param name="indices">(optional) An array of integer indices into the 'vertices' array, if <see langword="null" /> all vertices will be rendered in sequential order.</param>
    /// <param name="num_indices">number of indices.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderGeometryRaw" />
    /// <seealso cref="SetRenderTextureAddressMode" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderGeometry(nint renderer, nint texture, Vertex[] vertices, Span<int> indices) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        return RenderGeometry(renderer, texture, vertices.AsSpan(), indices);
    }

    /// <summary>Render a list of triangles, optionally using a texture and indices into the vertex arrays Color and alpha modulation is done per vertex (SDL_SetTextureColorMod and SDL_SetTextureAlphaMod are ignored).</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="texture">(optional) The SDL texture to use.</param>
    /// <param name="xy">vertex positions.</param>
    /// <param name="xy_stride">byte size to move from one element to the next element.</param>
    /// <param name="color">vertex colors (as SDL_FColor).</param>
    /// <param name="color_stride">byte size to move from one element to the next element.</param>
    /// <param name="uv">vertex normalized texture coordinates.</param>
    /// <param name="uv_stride">byte size to move from one element to the next element.</param>
    /// <param name="num_vertices">number of vertices.</param>
    /// <param name="indices">(optional) An array of indices into the 'vertices' arrays, if <see langword="null" /> all vertices will be rendered in sequential order.</param>
    /// <param name="num_indices">number of indices.</param>
    /// <param name="size_indices">index size: 1 (byte), 2 (short), 4 (int).</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderGeometry" />
    /// <seealso cref="SetRenderTextureAddressMode" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderGeometryRaw(nint renderer, nint texture, nint xy, int xyStride, nint color,
            int colorStride, nint uv, int uvStride, int numVertices, nint indices, int numIndices, int sizeIndices) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_RenderGeometryRaw(renderer, texture, xy, xyStride, color, colorStride, uv, uvStride,
            numVertices, indices, numIndices, sizeIndices);
        if (!result) {
            LogError(LogCategory.Error, "Failed to render geometry raw");
        }
        return result;
    }

    /// <summary>Draw a line on the current rendering target at subpixel precision.</summary>

    /// <param name="renderer">the renderer which should draw a line.</param>
    /// <param name="x1">the x coordinate of the start point.</param>
    /// <param name="y1">the y coordinate of the start point.</param>
    /// <param name="x2">the x coordinate of the end point.</param>
    /// <param name="y2">the y coordinate of the end point.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderLines" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderLine(nint renderer, float x1, float y1, float x2, float y2) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_RenderLine(renderer, x1, y1, x2, y2);
        if (!result) {
            LogError(LogCategory.Error, "Failed to render line");
        }
        return result;
    }

    /// <summary>Draw a line on the current rendering target at subpixel precision.</summary>

    /// <param name="renderer">the renderer which should draw a line.</param>
    /// <param name="x1">the x coordinate of the start point.</param>
    /// <param name="y1">the y coordinate of the start point.</param>
    /// <param name="x2">the x coordinate of the end point.</param>
    /// <param name="y2">the y coordinate of the end point.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderLines" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderLine(nint renderer, FPoint point1, FPoint point2) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        return RenderLine(renderer, point1.X, point1.Y, point2.X, point2.Y);
    }

    /// <summary>Draw a series of connected lines on the current rendering target at subpixel precision.</summary>

    /// <param name="renderer">the renderer which should draw multiple lines.</param>
    /// <param name="points">the points along the lines.</param>
    /// <param name="count">the number of points, drawing count-1 lines.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderLine" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderLines(nint renderer, Span<FPoint> points) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_RenderLines(renderer, points, points.Length);
        if (!result) {
            LogError(LogCategory.Error, "Failed to render lines");
        }
        return result;
    }

    /// <summary>Draw a series of connected lines on the current rendering target at subpixel precision.</summary>

    /// <param name="renderer">the renderer which should draw multiple lines.</param>
    /// <param name="points">the points along the lines.</param>
    /// <param name="count">the number of points, drawing count-1 lines.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderLine" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderLines(nint renderer, FPoint[] points) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        return RenderLines(renderer, points.AsSpan());
    }

    /// <summary>Draw a point on the current rendering target at subpixel precision.</summary>

    /// <param name="renderer">the renderer which should draw a point.</param>
    /// <param name="x">the x coordinate of the point.</param>
    /// <param name="y">the y coordinate of the point.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderPoints" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderPoint(nint renderer, float x, float y) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_RenderPoint(renderer, x, y);
        if (!result) {
            LogError(LogCategory.Error, "Failed to render point");
        }
        return result;
    }

    /// <summary>Draw a point on the current rendering target at subpixel precision.</summary>

    /// <param name="renderer">the renderer which should draw a point.</param>
    /// <param name="x">the x coordinate of the point.</param>
    /// <param name="y">the y coordinate of the point.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderPoints" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderPoint(nint renderer, FPoint point) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        return RenderPoint(renderer, point.X, point.Y);
    }

    /// <summary>Draw multiple points on the current rendering target at subpixel precision.</summary>

    /// <param name="renderer">the renderer which should draw multiple points.</param>
    /// <param name="points">the points to draw.</param>
    /// <param name="count">the number of points to draw.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderPoint" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderPoints(nint renderer, Span<FPoint> points) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_RenderPoints(renderer, points, points.Length);
        if (!result) {
            LogError(LogCategory.Error, "Failed to render points");
        }
        return result;
    }

    /// <summary>Draw multiple points on the current rendering target at subpixel precision.</summary>

    /// <param name="renderer">the renderer which should draw multiple points.</param>
    /// <param name="points">the points to draw.</param>
    /// <param name="count">the number of points to draw.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderPoint" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderPoints(nint renderer, FPoint[] points) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        return RenderPoints(renderer, points.AsSpan());
    }

    /// <summary>Update the screen with any rendering performed since the previous call.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <remarks>
    /// SDL's rendering functions operate on a backbuffer; that is, calling a
    /// rendering function such as SDL_RenderLine() does not
    /// directly put a line on the screen, but rather updates the backbuffer. As
    /// such, you compose your entire scene and present the composed backbuffer
    /// to the screen as a complete picture.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateRenderer" />
    /// <seealso cref="RenderClear" />
    /// <seealso cref="RenderFillRect" />
    /// <seealso cref="RenderFillRects" />
    /// <seealso cref="RenderLine" />
    /// <seealso cref="RenderLines" />
    /// <seealso cref="RenderPoint" />
    /// <seealso cref="RenderPoints" />
    /// <seealso cref="RenderRect" />
    /// <seealso cref="RenderRects" />
    /// <seealso cref="SetRenderDrawBlendMode" />
    /// <seealso cref="SetRenderDrawColor" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderPresent(nint renderer) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_RenderPresent(renderer);
        if (!result) {
            LogError(LogCategory.Error, "Failed to present render");
        }
        return result;
    }

    /// <summary>Read pixels from the current rendering target.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="rect">an <see cref="Rect" /> structure representing the area to read, which will be clipped to the current viewport, or <see langword="null" /> for the entire viewport.</param>
    /// <remarks>
    /// The returned surface contains pixels inside the desired area clipped to the
    /// current viewport, and should be freed with
    /// SDL_DestroySurface().
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(SDL_Surface *) Returns a new SDL_Surface on success or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint RenderReadPixels(nint renderer, ref Rect rect) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        nint result = SDL_RenderReadPixels(renderer, ref rect);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "Failed to read pixels from render");
        }
        return result;
    }

    /// <summary>Draw a rectangle on the current rendering target at subpixel precision.</summary>

    /// <param name="renderer">the renderer which should draw a rectangle.</param>
    /// <param name="rect">a pointer to the destination rectangle, or <see langword="null" /> to outline the entire rendering target.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderRects" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderRect(nint renderer, ref FRect rect) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_RenderRect(renderer, ref rect);
        if (!result) {
            LogError(LogCategory.Error, "Failed to render rect");
        }
        return result;
    }

    /// <summary>Draw some number of rectangles on the current rendering target at subpixel precision.</summary>

    /// <param name="renderer">the renderer which should draw multiple rectangles.</param>
    /// <param name="rects">a pointer to an array of destination rectangles.</param>
    /// <param name="count">the number of rectangles.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderRect" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderRects(nint renderer, Span<FRect> rects) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_RenderRects(renderer, rects, rects.Length);
        if (!result) {
            LogError(LogCategory.Error, "Failed to render rects");
        }
        return result;
    }

    /// <summary>Draw some number of rectangles on the current rendering target at subpixel precision.</summary>

    /// <param name="renderer">the renderer which should draw multiple rectangles.</param>
    /// <param name="rects">a pointer to an array of destination rectangles.</param>
    /// <param name="count">the number of rectangles.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderRect" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderRects(nint renderer, FRect[] rects) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        return RenderRects(renderer, rects.AsSpan());
    }

    /// <summary>Copy a portion of the texture to the current rendering target at subpixel precision.</summary>

    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <see langword="null" /> for the entire texture.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <see langword="null" /> for the entire rendering target.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderTextureRotated" />
    /// <seealso cref="RenderTextureTiled" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderTexture(nint renderer, nint texture, ref FRect srcrect, ref FRect dstrect) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_RenderTexture(renderer, texture, ref srcrect, ref dstrect);
        if (!result) {
            LogError(LogCategory.Error, "Failed to render texture");
        }
        return result;
    }

    /// <summary>Copy a portion of the texture to the current rendering target at subpixel precision.</summary>

    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <see langword="null" /> for the entire texture.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <see langword="null" /> for the entire rendering target.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderTextureRotated" />
    /// <seealso cref="RenderTextureTiled" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderTexture(nint renderer, nint texture, nint srcrect, nint dstrect) {
        FRect srect = new();
        FRect drect = new();
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        if (srcrect != nint.Zero) {
            Marshal.PtrToStructure(srcrect, srect);
        }

        if (dstrect != nint.Zero) {
            Marshal.PtrToStructure(dstrect, drect);
        }

        return RenderTexture(renderer, texture, ref srect, ref drect);
    }

    /// <summary>Copy a portion of the texture to the current rendering target at subpixel precision.</summary>

    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <see langword="null" /> for the entire texture.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <see langword="null" /> for the entire rendering target.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderTextureRotated" />
    /// <seealso cref="RenderTextureTiled" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderTexture(nint renderer, nint texture, ref FRect srcrect, nint dstrect) {
        FRect drect = new();
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        if (dstrect != nint.Zero) {
            Marshal.PtrToStructure(dstrect, drect);
        }

        return RenderTexture(renderer, texture, ref srcrect, ref drect);
    }

    /// <summary>Copy a portion of the texture to the current rendering target at subpixel precision.</summary>

    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <see langword="null" /> for the entire texture.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <see langword="null" /> for the entire rendering target.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderTextureRotated" />
    /// <seealso cref="RenderTextureTiled" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderTexture(nint renderer, nint texture, nint srcrect, ref FRect dstrect) {
        FRect srect = new();
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        if (srcrect != nint.Zero) {
            Marshal.PtrToStructure(srcrect, srect);
        }

        return RenderTexture(renderer, texture, ref srect, ref dstrect);
    }

    /// <summary>Perform a scaled copy using the 9-grid algorithm to the current rendering target at subpixel precision.</summary>

    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">the <see cref="Rect" /> structure representing the rectangle to be used for the 9-grid, or <see langword="null" /> to use the entire texture.</param>
    /// <param name="leftWidth">the width, in pixels, of the left corners in srcrect.</param>
    /// <param name="rightWidth">the width, in pixels, of the right corners in srcrect.</param>
    /// <param name="topHeight">the height, in pixels, of the top corners in srcrect.</param>
    /// <param name="bottomHeight">the height, in pixels, of the bottom corners in srcrect.</param>
    /// <param name="scale">the scale used to transform the corner of srcrect into the corner of dstrect, or 0.0f for an unscaled copy.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <see langword="null" /> for the entire rendering target.</param>
    /// <remarks>
    /// The pixels in the texture are split into a 3x3 grid, using the different
    /// corner sizes for each corner, and the sides and center making up the
    /// remaining pixels. The corners are then scaled using scale and fit into
    /// the corners of the destination rectangle. The sides and center are then
    /// stretched into place to cover the remaining destination rectangle.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderTexture" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderTexture9Grid(nint renderer, nint texture, ref FRect srcrect, float leftWidth,
            float rightWidth, float topHeight, float bottomHeight, float scale, ref FRect dstrect) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_RenderTexture9Grid(renderer, texture, ref srcrect, leftWidth, rightWidth,
            topHeight, bottomHeight, scale, ref dstrect);
        if (!result) {
            LogError(LogCategory.Error, "Failed to render texture 9 grid");
        }
        return result;
    }

    /// <summary>Copy a portion of the source texture to the current rendering target, with affine transform, at subpixel precision.</summary>

    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <see langword="null" /> for the entire texture.</param>
    /// <param name="origin">a pointer to a point indicating where the top-left corner of srcrect should be mapped to, or <see langword="null" /> for the rendering target's origin.</param>
    /// <param name="right">a pointer to a point indicating where the top-right corner of srcrect should be mapped to, or <see langword="null" /> for the rendering target's top-right corner.</param>
    /// <param name="down">a pointer to a point indicating where the bottom-left corner of srcrect should be mapped to, or <see langword="null" /> for the rendering target's bottom-left corner.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: You may only call this function from the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderTexture" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderTextureAffine(nint renderer, nint texture, in FRect srcrect, in FPoint origin,
            in FPoint right, in FPoint down) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_RenderTextureAffine(renderer, texture, in srcrect, in origin, in right, in down);
        if (!result) {
            LogError(LogCategory.Error, "Failed to render texture affine");
        }
        return result;
    }

    /// <summary>Copy a portion of the source texture to the current rendering target, with rotation and flipping, at subpixel precision.</summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <see langword="null" /> for the entire texture.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <see langword="null" /> for the entire rendering target.</param>
    /// <param name="angle">an angle in degrees that indicates the rotation that will be applied to dstrect, rotating it in a clockwise direction.</param>
    /// <param name="center">a pointer to a point indicating the point around which dstrect will be rotated (if <see langword="null" />, rotation will be done around dstrect.w/2, dstrect.h/2).</param>
    /// <param name="flip">an SDL_FlipMode value stating which flipping actions should be performed on the texture.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderTexture" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool RenderTextureRotated(nint renderer, nint texture, ref FRect srcrect, ref FRect dstrect,
            double angle, ref FPoint center, FlipMode flip) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_RenderTextureRotated(renderer, texture, ref srcrect, ref dstrect, angle, ref center,
            flip);
        if (!result) {
            LogError(LogCategory.Error, "Failed to render texture rotated");
        }
        return result;
    }

    /// <summary>Copy a portion of the source texture to the current rendering target, with rotation and flipping, at subpixel precision.</summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <see langword="null" /> for the entire texture.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <see langword="null" /> for the entire rendering target.</param>
    /// <param name="angle">an angle in degrees that indicates the rotation that will be applied to dstrect, rotating it in a clockwise direction.</param>
    /// <param name="center">a pointer to a point indicating the point around which dstrect will be rotated (if <see langword="null" />, rotation will be done around dstrect.w/2, dstrect.h/2).</param>
    /// <param name="flip">an SDL_FlipMode value stating which flipping actions should be performed on the texture.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderTexture" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool RenderTextureRotated(nint renderer, nint texture, nint srcrect, ref FRect dstrect, double angle, ref FPoint center, FlipMode flip) {
        FRect srect = new();
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        if (srcrect != nint.Zero) {
            Marshal.PtrToStructure(srcrect, srect);
        }
        return RenderTextureRotated(renderer, texture, ref srect, ref dstrect, angle, ref center, flip);
    }

    /// <summary>Copy a portion of the source texture to the current rendering target, with rotation and flipping, at subpixel precision.</summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <see langword="null" /> for the entire texture.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <see langword="null" /> for the entire rendering target.</param>
    /// <param name="angle">an angle in degrees that indicates the rotation that will be applied to dstrect, rotating it in a clockwise direction.</param>
    /// <param name="center">a pointer to a point indicating the point around which dstrect will be rotated (if <see langword="null" />, rotation will be done around dstrect.w/2, dstrect.h/2).</param>
    /// <param name="flip">an SDL_FlipMode value stating which flipping actions should be performed on the texture.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderTexture" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool RenderTextureRotated(nint renderer, nint texture, ref FRect srcrect, nint dstrect, double angle, ref FPoint center, FlipMode flip) {
        FRect drect = new();
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        if (dstrect != nint.Zero) {
            Marshal.PtrToStructure(dstrect, drect);
        }
        return RenderTextureRotated(renderer, texture, ref srcrect, ref drect, angle, ref center, flip);
    }

    /// <summary>Copy a portion of the source texture to the current rendering target, with rotation and flipping, at subpixel precision.</summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <see langword="null" /> for the entire texture.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <see langword="null" /> for the entire rendering target.</param>
    /// <param name="angle">an angle in degrees that indicates the rotation that will be applied to dstrect, rotating it in a clockwise direction.</param>
    /// <param name="center">a pointer to a point indicating the point around which dstrect will be rotated (if <see langword="null" />, rotation will be done around dstrect.w/2, dstrect.h/2).</param>
    /// <param name="flip">an SDL_FlipMode value stating which flipping actions should be performed on the texture.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderTexture" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool RenderTextureRotated(nint renderer, nint texture, nint srcrect, ref FRect dstrect, double angle, nint center, FlipMode flip) {
        FRect srect = new();
        FPoint centerPoint = new();
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        if (srcrect != nint.Zero) {
            Marshal.PtrToStructure(srcrect, srect);
        }
        if(center != nint.Zero) {
            centerPoint = Marshal.PtrToStructure<FPoint>(center);
        }

        return RenderTextureRotated(renderer, texture, ref srect, ref dstrect, angle, ref centerPoint, flip);
    }

    /// <summary>Copy a portion of the source texture to the current rendering target, with rotation and flipping, at subpixel precision.</summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <see langword="null" /> for the entire texture.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <see langword="null" /> for the entire rendering target.</param>
    /// <param name="angle">an angle in degrees that indicates the rotation that will be applied to dstrect, rotating it in a clockwise direction.</param>
    /// <param name="center">a pointer to a point indicating the point around which dstrect will be rotated (if <see langword="null" />, rotation will be done around dstrect.w/2, dstrect.h/2).</param>
    /// <param name="flip">an SDL_FlipMode value stating which flipping actions should be performed on the texture.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderTexture" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool RenderTextureRotated(nint renderer, nint texture, ref FRect srcrect, nint dstrect, double angle, nint center, FlipMode flip) {
        FRect drect = new();
        FPoint centerPoint = new();
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        if (dstrect != nint.Zero) {
            Marshal.PtrToStructure(dstrect, drect);
        }

        if (center != nint.Zero) {
            centerPoint = Marshal.PtrToStructure<FPoint>(center);
        }

        return RenderTextureRotated(renderer, texture, ref srcrect, ref drect, angle, ref centerPoint, flip);
    }


    /// <summary>Tile a portion of the texture to the current rendering target at subpixel precision.</summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <see langword="null" /> for the entire texture.</param>
    /// <param name="scale">the scale used to transform srcrect into the destination rectangle, e.g. a 32x32 texture with a scale of 2 would fill 64x64 tiles.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <see langword="null" /> for the entire rendering target.</param>
    /// <remarks>
    /// The pixels in srcrect will be repeated as many times as needed to
    /// completely fill dstrect.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RenderTexture" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RenderTextureTiled(nint renderer, nint texture, ref FRect srcrect, float scale,
            ref FRect dstrect) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_RenderTextureTiled(renderer, texture, ref srcrect, scale, ref dstrect);
        if (!result) {
            LogError(LogCategory.Error, "Failed to render texture tiled");
        }
        return result;
    }

    /// <summary>Return whether an explicit rectangle was set as the viewport.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <remarks>
    /// This is useful if you're saving and restoring the viewport and want to know
    /// whether you should restore a specific rectangle or <see langword="null" />.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRenderViewport" />
    /// <seealso cref="SetRenderViewport" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the viewport was set to a specific rectangle, or<see langword="false" /> if it was set to <see langword="null" /> (the entire target).</returns>

    public static bool RenderViewportSet(nint renderer) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_RenderViewportSet(renderer);
        if (!result) {
            LogError(LogCategory.Error, "Failed to set render viewport");
        }
        return result;
    }

    /// <summary>Set the clip rectangle for rendering on the specified target.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="rect">an <see cref="Rect" /> structure representing the clip area, relative to the viewport, or <see langword="null" /> to disable clipping.</param>
    /// <remarks>
    /// Each render target has its own clip rectangle. This function sets the
    /// cliprect for the current render target.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRenderClipRect" />
    /// <seealso cref="RenderClipEnabled" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool SetRenderClipRect(nint renderer, ref Rect rect) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_SetRenderClipRect(renderer, ref rect);
        if (!result) {
            LogError(LogCategory.Error, "Failed to set render clip rect");
        }
        return result;
    }

    /// <summary>Set the color scale used for render operations.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="scale">the color scale value.</param>
    /// <remarks>
    /// The color scale is an additional scale multiplied into the pixel color
    /// value while rendering. This can be used to adjust the brightness of colors
    /// during HDR rendering, or changing HDR video brightness when playing on an
    /// SDR display.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRenderColorScale" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool SetRenderColorScale(nint renderer, float scale) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_SetRenderColorScale(renderer, scale);
        if (!result) {
            LogError(LogCategory.Error, "Failed to set render color scale");
        }
        return result;
    }

    /// <summary>Set the blend mode used for drawing operations (Fill and Line).</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="blendMode">the <see cref="BlendMode" /> to use for blending.</param>
    /// <remarks>
    /// If the blend mode is not supported, the closest supported mode is chosen.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRenderDrawBlendMode" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool SetRenderDrawBlendMode(nint renderer, uint blendMode) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_SetRenderDrawBlendMode(renderer, blendMode);
        if (!result) {
            LogError(LogCategory.Error, "Failed to set render draw blend mode");
        }
        return result;
    }

    /// <summary>Set the blend mode used for drawing operations (Fill and Line).</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="blendMode">the <see cref="BlendMode" /> to use for blending.</param>
    /// <remarks>
    /// If the blend mode is not supported, the closest supported mode is chosen.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRenderDrawBlendMode" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool SetRenderDrawBlendMode(nint renderer, BlendMode mode) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_SetRenderDrawBlendMode(renderer, (uint)mode);
        if (!result) {
            LogError(LogCategory.Error, "Failed to set render draw blend mode");
        }
        return result;
    }

    /// <summary>Set the color used for drawing operations.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="r">the red value used to draw on the rendering target.</param>
    /// <param name="g">the green value used to draw on the rendering target.</param>
    /// <param name="b">the blue value used to draw on the rendering target.</param>
    /// <param name="a">the alpha value used to draw on the rendering target; usually SDL_ALPHA_OPAQUE (255). Use SDL_SetRenderDrawBlendMode to specify how the alpha channel is used.</param>
    /// <remarks>
    /// Set the color for drawing or filling rectangles, lines, and points, and for
    /// SDL_RenderClear().
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRenderDrawColor" />
    /// <seealso cref="SetRenderDrawColorFloat" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool SetRenderDrawColor(nint renderer, byte r, byte g, byte b, byte a) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_SetRenderDrawColor(renderer, r, g, b, a);
        if (!result) {
            LogError(LogCategory.Error, "Failed to set render draw color");
        }
        return result;
    }

    /// <summary>Set the color used for drawing operations.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="r">the red value used to draw on the rendering target.</param>
    /// <param name="g">the green value used to draw on the rendering target.</param>
    /// <param name="b">the blue value used to draw on the rendering target.</param>
    /// <param name="a">the alpha value used to draw on the rendering target; usually SDL_ALPHA_OPAQUE (255). Use SDL_SetRenderDrawBlendMode to specify how the alpha channel is used.</param>
    /// <remarks>
    /// Set the color for drawing or filling rectangles, lines, and points, and for
    /// SDL_RenderClear().
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRenderDrawColor" />
    /// <seealso cref="SetRenderDrawColorFloat" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool SetRenderDrawColor(nint renderer, Color color) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        return SetRenderDrawColor(renderer, color.R, color.G, color.B, color.A);
    }

    /// <summary>Set the color used for drawing operations (Rect, Line and Clear).</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="r">the red value used to draw on the rendering target.</param>
    /// <param name="g">the green value used to draw on the rendering target.</param>
    /// <param name="b">the blue value used to draw on the rendering target.</param>
    /// <param name="a">the alpha value used to draw on the rendering target. Use SDL_SetRenderDrawBlendMode to specify how the alpha channel is used.</param>
    /// <remarks>
    /// Set the color for drawing or filling rectangles, lines, and points, and for
    /// SDL_RenderClear().
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRenderDrawColorFloat" />
    /// <seealso cref="SetRenderDrawColor" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool SetRenderDrawColorFloat(nint renderer, float r, float g, float b, float a) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_SetRenderDrawColorFloat(renderer, r, g, b, a);
        if (!result) {
            LogError(LogCategory.Error, "Failed to set render draw color float");
        }
        return result;
    }

    /// <summary>Set the color used for drawing operations (Rect, Line and Clear).</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="r">the red value used to draw on the rendering target.</param>
    /// <param name="g">the green value used to draw on the rendering target.</param>
    /// <param name="b">the blue value used to draw on the rendering target.</param>
    /// <param name="a">the alpha value used to draw on the rendering target. Use SDL_SetRenderDrawBlendMode to specify how the alpha channel is used.</param>
    /// <remarks>
    /// Set the color for drawing or filling rectangles, lines, and points, and for
    /// SDL_RenderClear().
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRenderDrawColorFloat" />
    /// <seealso cref="SetRenderDrawColor" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool SetRenderDrawColorFloat(nint renderer, FColor color) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        return SetRenderDrawColorFloat(renderer, color.R, color.G, color.B, color.A);
    }

    /// <summary>Set a device-independent resolution and presentation mode for rendering.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="w">the width of the logical resolution.</param>
    /// <param name="h">the height of the logical resolution.</param>
    /// <param name="mode">the presentation mode used.</param>
    /// <remarks>
    /// This function sets the width and height of the logical rendering output.
    /// The renderer will act as if the current render target is always the
    /// requested dimensions, scaling to the actual resolution as necessary.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ConvertEventToRenderCoordinates" />
    /// <seealso cref="GetRenderLogicalPresentation" />
    /// <seealso cref="GetRenderLogicalPresentationRect" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool SetRenderLogicalPresentation(nint renderer, int w, int h,
            RendererLogicalPresentation mode) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_SetRenderLogicalPresentation(renderer, w, h, mode);
        if (!result) {
            LogError(LogCategory.Error, "Failed to set render logical presentation");
        }
        return result;
    }

    /// <summary>Set the drawing scale for rendering on the current target.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="scaleX">the horizontal scaling factor.</param>
    /// <param name="scaleY">the vertical scaling factor.</param>
    /// <remarks>
    /// The drawing coordinates are scaled by the x/y scaling factors before they
    /// are used by the renderer. This allows resolution independent drawing with a
    /// single coordinate system.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRenderScale" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool SetRenderScale(nint renderer, float scaleX, float scaleY) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_SetRenderScale(renderer, scaleX, scaleY);
        if (!result) {
            LogError(LogCategory.Error, "Failed to set render scale");
        }
        return result;
    }

    /// <summary>Set a texture as the current rendering target.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="texture">the targeted texture, which must be created with the SDL_TEXTUREACCESS_TARGET flag, or <see langword="null" /> to render to the window instead of a texture.</param>
    /// <remarks>
    /// The default render target is the window for which the renderer was created.
    /// To stop rendering to a texture and render to the window again, call this
    /// function with a <see langword="null" /> texture.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRenderTarget" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static SdlBool SetRenderTarget(nint renderer, nint texture) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }

        if (texture == nint.Zero) {
            LogError(LogCategory.Error, "Texture is null");
            return false;
        }

        SdlBool result = SDL_SetRenderTarget(renderer, texture);
        if (!result) {
            LogError(LogCategory.Error, "Failed to set render target");
        }

        return result;
    }

    /// <summary>Set the drawing area for rendering on the current target.</summary>

    /// <param name="renderer">the rendering context.</param>
    /// <param name="rect">the <see cref="Rect" /> structure representing the drawing area, or <see langword="null" /> to set the viewport to the entire target.</param>
    /// <remarks>
    /// Drawing will clip to this area (separately from any clipping done with
    /// SDL_SetRenderClipRect), and the top left of the
    /// area will become coordinate (0, 0) for future drawing commands.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRenderViewport" />
    /// <seealso cref="RenderViewportSet" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool SetRenderViewport(nint renderer, ref Rect rect) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_SetRenderViewport(renderer, ref rect);
        if (!result) {
            LogError(LogCategory.Error, "Failed to set render viewport");
        }
        return result;
    }

    /// <summary>Toggle VSync of the given renderer.</summary>

    /// <param name="renderer">the renderer to toggle.</param>
    /// <param name="vsync">the vertical refresh sync interval.</param>
    /// <remarks>
    /// When a renderer is created, vsync defaults to
    /// SDL_RENDERER_VSYNC_DISABLED.
    /// <para><strong>Thread Safety</strong>: This function should only be called on the main thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRenderVSync" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool SetRenderVSync(nint renderer, int vsync) {
        if (renderer == nint.Zero) {
            throw new SdlException("Renderer is null");
        }
        SdlBool result = SDL_SetRenderVSync(renderer, vsync);
        if (!result) {
            LogError(LogCategory.Error, "Failed to set render VSync");
        }
        return result;
    }

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_AddVulkanRenderSemaphores(nint renderer, uint waitStageMask, long waitSemaphore,
        long signalSemaphore);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ConvertEventToRenderCoordinates(nint renderer, ref Event* @event);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateRenderer(nint window, string? name);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateRendererWithProperties(uint props);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateSoftwareRenderer(nint surface);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CreateWindowAndRenderer(string title, int width, int height,
        WindowFlags windowFlags, out nint window, out nint renderer);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyRenderer(nint renderer);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyTexture(nint texture);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_FlushRenderer(nint renderer);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetCurrentRenderOutputSize(nint renderer, out int w, out int h);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumRenderDrivers();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderClipRect(nint renderer, out Rect rect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderColorScale(nint renderer, out float scale);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderDrawBlendMode(nint renderer, nint blendMode);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderDrawColor(nint renderer, out byte r, out byte g, out byte b,
        out byte a);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderDrawColorFloat(nint renderer, out float r, out float g, out float b,
        out float a);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetRenderDriver(int index);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetRenderer(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetRendererFromTexture(nint texture);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetRendererName(nint renderer);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetRendererProperties(nint renderer);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderLogicalPresentation(nint renderer, out int w, out int h,
        out RendererLogicalPresentation mode);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderLogicalPresentationRect(nint renderer, out FRect rect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetRenderMetalCommandEncoder(nint renderer);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetRenderMetalLayer(nint renderer);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderOutputSize(nint renderer, out int w, out int h);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderSafeArea(nint renderer, out Rect rect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderScale(nint renderer, out float scaleX, out float scaleY);

    // nint refers to a nint
    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetRenderTarget(nint renderer);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderViewport(nint renderer, out Rect rect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderVSync(nint renderer, out int vsync);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetRenderWindow(nint renderer);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderClear(nint renderer);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderClipEnabled(nint renderer);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderCoordinatesFromWindow(nint renderer, float windowX, float windowY,
        out float x, out float y);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderCoordinatesToWindow(nint renderer, float x, float y, out float windowX,
        out float windowY);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderDebugText(nint renderer, float x, float y, string str);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderDebugTextFormat(nint renderer, float x, float y, string fmt);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderFillRect(nint renderer, ref FRect rect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderFillRects(nint renderer, Span<FRect> rects, int count);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderGeometry(nint renderer, nint texture, Span<Vertex> vertices,
        int numVertices, Span<int> indices, int numIndices);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderGeometryRaw(nint renderer, nint texture, nint xy, int xyStride,
        nint color, int colorStride, nint uv, int uvStride, int numVertices, nint indices, int numIndices,
        int sizeIndices);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderLine(nint renderer, float x1, float y1, float x2, float y2);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderLines(nint renderer, Span<FPoint> points, int count);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderPoint(nint renderer, float x, float y);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderPoints(nint renderer, Span<FPoint> points, int count);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderPresent(nint renderer);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_RenderReadPixels(nint renderer, ref Rect rect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderRect(nint renderer, ref FRect rect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderRects(nint renderer, Span<FRect> rects, int count);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderTexture(nint renderer, nint texture, ref FRect srcrect,
        ref FRect dstrect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderTexture9Grid(nint renderer, nint texture, ref FRect srcrect,
        float leftWidth, float rightWidth, float topHeight, float bottomHeight, float scale,
        ref FRect dstrect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderTextureAffine(nint renderer, nint texture, in FRect srcrect,
        in FPoint origin, in FPoint right, in FPoint down);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderTextureRotated(nint renderer, nint texture, ref FRect srcrect,
        ref FRect dstrect, double angle, ref FPoint center, FlipMode flip);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderTextureTiled(nint renderer, nint texture, ref FRect srcrect,
        float scale, ref FRect dstrect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderViewportSet(nint renderer);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderClipRect(nint renderer, ref Rect rect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderColorScale(nint renderer, float scale);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderDrawBlendMode(nint renderer, uint blendMode);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderDrawColor(nint renderer, byte r, byte g, byte b, byte a);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderDrawColorFloat(nint renderer, float r, float g, float b, float a);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderLogicalPresentation(nint renderer, int w, int h,
        RendererLogicalPresentation mode);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderScale(nint renderer, float scaleX, float scaleY);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_SetRenderTarget(nint renderer, nint texture);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderViewport(nint renderer, ref Rect rect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderVSync(nint renderer, int vsync);
}