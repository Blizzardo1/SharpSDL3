using SharpSDL3.Enums;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;

using static SharpSDL3.Sdl;
using SharpSDL3.Structs;

namespace SharpSDL3;

public static unsafe partial class Render {

    public static bool AddVulkanRenderSemaphores(nint renderer, uint waitStageMask, long waitSemaphore,
        long signalSemaphore) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_AddVulkanRenderSemaphores(renderer, waitStageMask, waitSemaphore, signalSemaphore);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to add Vulkan render semaphores");
        }
        return result;
    }

    public static bool ConvertEventToRenderCoordinates(nint renderer, ref Event @event) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_ConvertEventToRenderCoordinates(renderer, ref @event);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to convert event to render coordinates");
        }
        return result;
    }

    public static nint CreateRenderer(nint window, string? name) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "Window is null");
            return nint.Zero;
        }

        nint result = SDL_CreateRenderer(window, name);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.System, $"Failed to create renderer: {Sdl.GetError()}");
        }
        return result;
    }

    public static nint CreateRendererWithProperties(uint props) {
        nint result = SDL_CreateRendererWithProperties(props);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.System, "Failed to create renderer with properties");
        }
        return result;
    }

    public static nint CreateSoftwareRenderer(nint surface) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "Surface is null");
            return nint.Zero;
        }
        nint result = SDL_CreateSoftwareRenderer(surface);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.System, "Failed to create software renderer");
        }
        return result;
    }

    public static bool CreateWindowAndRenderer(string title, int width, int height, WindowFlags windowFlags,
        out nint window, out nint renderer) {
        if (string.IsNullOrEmpty(title)) {
            Logger.LogError(LogCategory.System, "Title is null or empty");
            window = nint.Zero;
            renderer = nint.Zero;
            return false;
        }
        SdlBool result = SDL_CreateWindowAndRenderer(title, width, height, windowFlags, out window, out renderer);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to create window and renderer");
        }
        return result;
    }

    public static nint CreateWindowAndRenderer(string title, int width, int height,
        WindowFlags windowFlags, out nint renderer) {
        if (string.IsNullOrEmpty(title)) {
            Logger.LogError(LogCategory.System, "Title is null or empty");
            renderer = nint.Zero;
            return nint.Zero;
        }
        SdlBool result = CreateWindowAndRenderer(title, width, height, windowFlags, out nint window, out renderer);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to create window and renderer");
        }
        return window;
    }

    public static void DestroyRenderer(nint renderer) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return;
        }
        SDL_DestroyRenderer(renderer);
    }

    public static void DestroyTexture(nint texture) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.System, "Texture is null");
            return;
        }
        SDL_DestroyTexture(texture);
    }

    public static bool FlushRenderer(nint renderer) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_FlushRenderer(renderer);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to flush render");
        }
        return result;
    }

    public static bool GetCurrentRenderOutputSize(nint renderer, out int w, out int h) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            w = 0;
            h = 0;
            return false;
        }
        SdlBool result = SDL_GetCurrentRenderOutputSize(renderer, out w, out h);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to get current render output size");
        }
        return result;
    }

    public static Rect GetCurrentRenderOutputSize(nint renderer) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return default;
        }
        _ = GetCurrentRenderOutputSize(renderer, out int w, out int h);
        return new() { W = w, H = h };
    }

    public static int GetNumRenderDrivers() {
        int result = SDL_GetNumRenderDrivers();
        if (result < 0) {
            Logger.LogError(LogCategory.System, "Failed to get number of render drivers");
        }
        return result;
    }

    public static bool GetRenderClipRect(nint renderer, out Rect rect) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            rect = default;
            return false;
        }
        SdlBool result = SDL_GetRenderClipRect(renderer, out rect);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to get render clip rect");
        }
        return result;
    }

    public static Rect GetRenderClipRect(nint renderer) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return default;
        }
        _ = GetRenderClipRect(renderer, out Rect rect);
        return rect;
    }

    public static bool GetRenderColorScale(nint renderer, out float scale) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            scale = 0;
            return false;
        }
        SdlBool result = SDL_GetRenderColorScale(renderer, out scale);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to get render color scale");
        }
        return result;
    }

    public static float GetRenderColorScale(nint renderer) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return 0;
        }
        _ = GetRenderColorScale(renderer, out float scale);
        return scale;
    }

    public static bool GetRenderDrawBlendMode(nint renderer, nint blendMode) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_GetRenderDrawBlendMode(renderer, blendMode);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to get render draw blend mode");
        }
        return result;
    }

    public static bool GetRenderDrawColor(nint renderer, out byte r, out byte g, out byte b, out byte a) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            r = 0;
            g = 0;
            b = 0;
            a = 0;
            return false;
        }
        SdlBool result = SDL_GetRenderDrawColor(renderer, out r, out g, out b, out a);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to get render draw color");
        }
        return result;
    }

    public static Color GetRenderDrawColor(nint renderer) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return default;
        }
        _ = GetRenderDrawColor(renderer, out byte r, out byte g, out byte b, out byte a);
        return new() { R = r, G = g, B = b, A = a };
    }

    public static bool GetRenderDrawColorFloat(nint renderer, out float r, out float g, out float b, out float a) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            r = 0;
            g = 0;
            b = 0;
            a = 0;
            return false;
        }
        SdlBool result = SDL_GetRenderDrawColorFloat(renderer, out r, out g, out b, out a);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to get render draw color float");
        }
        return result;
    }

    public static FColor GetRenderDrawColorFloat(nint renderer) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return default;
        }
        _ = GetRenderDrawColorFloat(renderer, out float r, out float g, out float b, out float a);
        return new() { R = r, G = g, B = b, A = a };
    }

    public static string GetRenderDriver(int index) {
        if (index < 0) {
            Logger.LogError(LogCategory.System, "Index is negative");
            return string.Empty;
        }
        string result = SDL_GetRenderDriver(index);
        if (string.IsNullOrEmpty(result)) {
            Logger.LogError(LogCategory.System, "Failed to get render driver");
        }
        return result;
    }

    public static nint GetRenderer(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "Window is null");
            return nint.Zero;
        }
        nint result = SDL_GetRenderer(window);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.System, "Failed to get renderer");
        }
        return result;
    }

    public static nint GetRendererFromTexture(nint texture) {
        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.System, "Texture is null");
            return nint.Zero;
        }
        nint result = SDL_GetRendererFromTexture(texture);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.System, "Failed to get renderer from texture");
        }
        return result;
    }

    public static string GetRendererName(nint renderer) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return string.Empty;
        }
        string result = SDL_GetRendererName(renderer);
        if (string.IsNullOrEmpty(result)) {
            Logger.LogError(LogCategory.System, "Failed to get renderer name");
        }
        return result;
    }

    public static uint GetRendererProperties(nint renderer) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return 0;
        }
        uint result = SDL_GetRendererProperties(renderer);
        if (result == 0) {
            Logger.LogError(LogCategory.System, "Failed to get renderer properties");
        }
        return result;
    }

    public static bool GetRenderLogicalPresentation(nint renderer, out int w, out int h,
        out RendererLogicalPresentation mode) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            w = 0;
            h = 0;
            mode = RendererLogicalPresentation.Disabled;
            return false;
        }
        SdlBool result = SDL_GetRenderLogicalPresentation(renderer, out w, out h, out mode);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to get render logical presentation");
        }
        return result;
    }

    public static Rect GetRenderLogicalPresentation(nint renderer, out RendererLogicalPresentation mode) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            mode = RendererLogicalPresentation.Disabled;
            return default;
        }
        SdlBool result = GetRenderLogicalPresentation(renderer, out int w, out int h, out mode);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to get render logical presentation");
        }
        return new() { W = w, H = h };
    }

    public static bool GetRenderLogicalPresentationRect(nint renderer, out FRect rect) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            rect = default;
            return false;
        }
        SdlBool result = SDL_GetRenderLogicalPresentationRect(renderer, out rect);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to get render logical presentation rect");
        }
        return result;
    }

    public static FRect GetRenderLogicalPresentationRect(nint renderer) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return default;
        }
        SdlBool result = SDL_GetRenderLogicalPresentationRect(renderer, out FRect rect);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to get render logical presentation rect");
        }
        return rect;
    }

    public static nint GetRenderMetalCommandEncoder(nint renderer) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return nint.Zero;
        }
        nint result = SDL_GetRenderMetalCommandEncoder(renderer);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.System, "Failed to get render metal command encoder");
        }
        return result;
    }

    public static nint GetRenderMetalLayer(nint renderer) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return nint.Zero;
        }
        nint result = SDL_GetRenderMetalLayer(renderer);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.System, "Failed to get render metal layer");
        }
        return result;
    }

    public static bool GetRenderOutputSize(nint renderer, out int w, out int h) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            w = 0;
            h = 0;
            return false;
        }
        SdlBool result = SDL_GetRenderOutputSize(renderer, out w, out h);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to get render output size");
        }
        return result;
    }

    public static Rect GetRenderOutputSize(nint renderer) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return default;
        }
        _ = GetRenderOutputSize(renderer, out int w, out int h);
        return new() { W = w, H = h };
    }

    public static bool GetRenderSafeArea(nint renderer, out Rect rect) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            rect = default;
            return false;
        }
        SdlBool result = SDL_GetRenderSafeArea(renderer, out rect);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to get render safe area");
        }
        return result;
    }

    public static Rect GetRenderSafeArea(nint renderer) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return default;
        }
        _ = GetRenderSafeArea(renderer, out Rect rect);
        return rect;
    }

    public static bool GetRenderScale(nint renderer, out float scaleX, out float scaleY) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            scaleX = 0;
            scaleY = 0;
            return false;
        }
        SdlBool result = SDL_GetRenderScale(renderer, out scaleX, out scaleY);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to get render scale");
        }
        return result;
    }

    public static FPoint GetRenderScale(nint renderer) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return default;
        }
        _ = GetRenderScale(renderer, out float scaleX, out float scaleY);
        return new() { X = scaleX, Y = scaleY };
    }

    public static nint GetRenderTarget(nint renderer) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return nint.Zero;
        }
        nint result = SDL_GetRenderTarget(renderer);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.System, "Failed to get render target");
        }
        return result;
    }

    public static bool GetRenderViewport(nint renderer, out Rect rect) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            rect = default;
            return false;
        }
        SdlBool result = SDL_GetRenderViewport(renderer, out rect);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to get render viewport");
        }
        return result;
    }

    public static int GetRenderVsync(nint renderer) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return 0;
        }
        _ = GetRenderVSync(renderer, out int vsync);
        return vsync;
    }

    public static bool GetRenderVSync(nint renderer, out int vsync) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            vsync = 0;
            return false;
        }
        SdlBool result = SDL_GetRenderVSync(renderer, out vsync);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to get render VSync");
        }
        return result;
    }

    public static nint GetRenderWindow(nint renderer) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return nint.Zero;
        }
        nint result = SDL_GetRenderWindow(renderer);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.System, "Failed to get render window");
        }
        return result;
    }

    public static bool RenderClear(nint renderer) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_RenderClear(renderer);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to clear render");
        }
        return result;
    }

    public static bool RenderClipEnabled(nint renderer) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_RenderClipEnabled(renderer);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to get render clip enabled");
        }
        return result;
    }

    public static SdlBool RenderCoordinatesFromWindow(nint renderer, float windowX, float windowY,
        out float x, out float y) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            x = 0;
            y = 0;
            return false;
        }
        SdlBool result = SDL_RenderCoordinatesFromWindow(renderer, windowX, windowY, out x, out y);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to convert coordinates from window");
        }
        return result;
    }

    public static FPoint RenderCoordinatesFromWindow(nint renderer, FPoint windowPoint) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return default;
        }
        SdlBool result = SDL_RenderCoordinatesFromWindow(renderer, windowPoint.X, windowPoint.Y, out float x, out float y);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to convert coordinates from window");
        }
        return new() { X = x, Y = y };
    }

    public static bool RenderCoordinatesToWindow(nint renderer, float x, float y, out float windowX,
        out float windowY) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            windowX = 0;
            windowY = 0;
            return false;
        }
        SdlBool result = SDL_RenderCoordinatesToWindow(renderer, x, y, out windowX, out windowY);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to convert coordinates to window");
        }
        return result;
    }

    public static FPoint RenderCoordinatesToWindow(nint renderer, float x, float y) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return default;
        }
        _ = RenderCoordinatesToWindow(renderer, x, y, out float windowX, out float windowY);
        return new() { X = windowX, Y = windowY };
    }

    public static FPoint RenderCoordinatesToWindow(nint renderer, FPoint point) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return default;
        }
        return RenderCoordinatesToWindow(renderer, point.X, point.Y);
    }

    public static bool RenderDebugText(nint renderer, float x, float y, string str) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_RenderDebugText(renderer, x, y, str);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to render debug text");
        }
        return result;
    }

    public static bool RenderDebugText(nint renderer, FPoint location, string str) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        return RenderDebugText(renderer, location.X, location.Y, str);
    }

    public static bool RenderDebugTextFormat(nint renderer, float x, float y, string fmt) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_RenderDebugTextFormat(renderer, x, y, fmt);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to render debug text format");
        }
        return result;
    }

    public static bool RenderDebugTextFormat(nint renderer, FPoint location, string fmt) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        return RenderDebugTextFormat(renderer, location.X, location.Y, fmt);
    }

    public static bool RenderFillRect(nint renderer, ref FRect rect) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_RenderFillRect(renderer, ref rect);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to render fill rect");
        }
        return result;
    }

    public static bool RenderFillRects(nint renderer, Span<FRect> rects) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_RenderFillRects(renderer, rects, rects.Length);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to render fill rects");
        }
        return result;
    }

    public static bool RenderFillRects(nint renderer, FRect[] rects) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        return RenderFillRects(renderer, rects.AsSpan());
    }

    public static bool RenderGeometry(nint renderer, nint texture, Span<Vertex> vertices, Span<int> indices) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_RenderGeometry(renderer, texture, vertices, vertices.Length, indices,
            indices.Length);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to render geometry");
        }
        return result;
    }

    public static bool RenderGeometry(nint renderer, nint texture, Vertex[] vertices, int[] indices) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        return RenderGeometry(renderer, texture, vertices.AsSpan(), indices.AsSpan());
    }

    public static bool RenderGeometry(nint renderer, nint texture, Span<Vertex> vertices, int[] indices) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        return RenderGeometry(renderer, texture, vertices, indices.AsSpan());
    }

    public static bool RenderGeometry(nint renderer, nint texture, Vertex[] vertices, Span<int> indices) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        return RenderGeometry(renderer, texture, vertices.AsSpan(), indices);
    }

    public static bool RenderGeometryRaw(nint renderer, nint texture, nint xy, int xyStride, nint color,
        int colorStride, nint uv, int uvStride, int numVertices, nint indices, int numIndices, int sizeIndices) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_RenderGeometryRaw(renderer, texture, xy, xyStride, color, colorStride, uv, uvStride,
            numVertices, indices, numIndices, sizeIndices);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to render geometry raw");
        }
        return result;
    }

    public static bool RenderLine(nint renderer, float x1, float y1, float x2, float y2) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_RenderLine(renderer, x1, y1, x2, y2);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to render line");
        }
        return result;
    }

    public static bool RenderLine(nint renderer, FPoint point1, FPoint point2) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        return RenderLine(renderer, point1.X, point1.Y, point2.X, point2.Y);
    }

    public static bool RenderLines(nint renderer, Span<FPoint> points) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_RenderLines(renderer, points, points.Length);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to render lines");
        }
        return result;
    }

    public static bool RenderLines(nint renderer, FPoint[] points) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        return RenderLines(renderer, points.AsSpan());
    }

    public static bool RenderPoint(nint renderer, float x, float y) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_RenderPoint(renderer, x, y);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to render point");
        }
        return result;
    }

    public static bool RenderPoint(nint renderer, FPoint point) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        return RenderPoint(renderer, point.X, point.Y);
    }

    public static bool RenderPoints(nint renderer, Span<FPoint> points) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_RenderPoints(renderer, points, points.Length);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to render points");
        }
        return result;
    }

    public static bool RenderPoints(nint renderer, FPoint[] points) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        return RenderPoints(renderer, points.AsSpan());
    }

    public static bool RenderPresent(nint renderer) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_RenderPresent(renderer);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to present render");
        }
        return result;
    }

    public static Surface* RenderReadPixels(nint renderer, ref Rect rect) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return null;
        }
        Surface* result = SDL_RenderReadPixels(renderer, ref rect);
        if (result == null) {
            Logger.LogError(LogCategory.System, "Failed to read pixels from render");
        }
        return result;
    }

    public static bool RenderRect(nint renderer, ref FRect rect) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_RenderRect(renderer, ref rect);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to render rect");
        }
        return result;
    }

    public static bool RenderRects(nint renderer, Span<FRect> rects) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_RenderRects(renderer, rects, rects.Length);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to render rects");
        }
        return result;
    }

    public static bool RenderRects(nint renderer, FRect[] rects) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        return RenderRects(renderer, rects.AsSpan());
    }

    public static bool RenderTexture(nint renderer, nint texture, ref FRect srcrect, ref FRect dstrect) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_RenderTexture(renderer, texture, ref srcrect, ref dstrect);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to render texture");
        }
        return result;
    }

    public static bool RenderTexture(nint renderer, nint texture, nint srcrect, nint destrect) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        FRect srect = new();
        FRect drect = new();

        Marshal.PtrToStructure(srcrect, srect);
        Marshal.PtrToStructure(destrect, drect);

        return SDL_RenderTexture(renderer, texture, ref srect, ref drect);
    }

    public static bool RenderTexture(nint renderer, nint texture, ref FRect srcrect, nint destrect) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        FRect drect = new();

        Marshal.PtrToStructure(destrect, drect);

        return RenderTexture(renderer, texture, ref srcrect, ref drect);
    }

    public static bool RenderTexture(nint renderer, nint texture, nint srcrect, ref FRect destrect) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        FRect srect = new();

        Marshal.PtrToStructure(srcrect, srect);

        return RenderTexture(renderer, texture, ref srect, ref destrect);
    }

    public static bool RenderTexture9Grid(nint renderer, nint texture, ref FRect srcrect, float leftWidth,
        float rightWidth, float topHeight, float bottomHeight, float scale, ref FRect dstrect) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_RenderTexture9Grid(renderer, texture, ref srcrect, leftWidth, rightWidth,
            topHeight, bottomHeight, scale, ref dstrect);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to render texture 9 grid");
        }
        return result;
    }

    public static bool RenderTextureAffine(nint renderer, nint texture, in FRect srcrect, in FPoint origin,
        in FPoint right, in FPoint down) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_RenderTextureAffine(renderer, texture, in srcrect, in origin, in right, in down);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to render texture affine");
        }
        return result;
    }

    public static bool RenderTextureRotated(nint renderer, nint texture, ref FRect srcrect, ref FRect dstrect,
        double angle, ref FPoint center, FlipMode flip) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_RenderTextureRotated(renderer, texture, ref srcrect, ref dstrect, angle, ref center,
            flip);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to render texture rotated");
        }
        return result;
    }

    public static bool RenderTextureTiled(nint renderer, nint texture, ref FRect srcrect, float scale,
        ref FRect dstrect) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_RenderTextureTiled(renderer, texture, ref srcrect, scale, ref dstrect);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to render texture tiled");
        }
        return result;
    }

    public static bool RenderViewportSet(nint renderer) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_RenderViewportSet(renderer);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to set render viewport");
        }
        return result;
    }

    public static bool SetRenderClipRect(nint renderer, ref Rect rect) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_SetRenderClipRect(renderer, ref rect);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to set render clip rect");
        }
        return result;
    }

    public static bool SetRenderColorScale(nint renderer, float scale) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_SetRenderColorScale(renderer, scale);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to set render color scale");
        }
        return result;
    }

    public static bool SetRenderDrawBlendMode(nint renderer, uint blendMode) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_SetRenderDrawBlendMode(renderer, blendMode);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to set render draw blend mode");
        }
        return result;
    }

    public static bool SetRenderDrawBlendMode(nint renderer, BlendMode mode) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_SetRenderDrawBlendMode(renderer, (uint)mode);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to set render draw blend mode");
        }
        return result;
    }

    public static bool SetRenderDrawColor(nint renderer, byte r, byte g, byte b, byte a) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_SetRenderDrawColor(renderer, r, g, b, a);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to set render draw color");
        }
        return result;
    }

    public static bool SetRenderDrawColor(nint renderer, Color color) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        return SetRenderDrawColor(renderer, color.R, color.G, color.B, color.A);
    }

    public static bool SetRenderDrawColorFloat(nint renderer, float r, float g, float b, float a) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_SetRenderDrawColorFloat(renderer, r, g, b, a);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to set render draw color float");
        }
        return result;
    }

    public static bool SetRenderDrawColorFloat(nint renderer, FColor color) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        return SetRenderDrawColorFloat(renderer, color.R, color.G, color.B, color.A);
    }

    public static bool SetRenderLogicalPresentation(nint renderer, int w, int h,
        RendererLogicalPresentation mode) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_SetRenderLogicalPresentation(renderer, w, h, mode);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to set render logical presentation");
        }
        return result;
    }

    public static bool SetRenderScale(nint renderer, float scaleX, float scaleY) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_SetRenderScale(renderer, scaleX, scaleY);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to set render scale");
        }
        return result;
    }

    public static SdlBool SetRenderTarget(nint renderer, nint texture) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }

        if (texture == nint.Zero) {
            Logger.LogError(LogCategory.System, "Texture is null");
            return false;
        }

        SdlBool result = SDL_SetRenderTarget(renderer, texture);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to set render target");
        }

        return result;
    }

    public static bool SetRenderViewport(nint renderer, ref Rect rect) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_SetRenderViewport(renderer, ref rect);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to set render viewport");
        }
        return result;
    }

    public static bool SetRenderVSync(nint renderer, int vsync) {
        if (renderer == nint.Zero) {
            Logger.LogError(LogCategory.System, "Renderer is null");
            return false;
        }
        SdlBool result = SDL_SetRenderVSync(renderer, vsync);
        if (!result) {
            Logger.LogError(LogCategory.System, "Failed to set render VSync");
        }
        return result;
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_AddVulkanRenderSemaphores(nint renderer, uint waitStageMask, long waitSemaphore,
        long signalSemaphore);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ConvertEventToRenderCoordinates(nint renderer, ref Event @event);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateRenderer(nint window, string? name);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateRendererWithProperties(uint props);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateSoftwareRenderer(nint surface);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CreateWindowAndRenderer(string title, int width, int height,
        WindowFlags windowFlags, out nint window, out nint renderer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyRenderer(nint renderer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyTexture(nint texture);

   
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_FlushRenderer(nint renderer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetCurrentRenderOutputSize(nint renderer, out int w, out int h);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumRenderDrivers();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderClipRect(nint renderer, out Rect rect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderColorScale(nint renderer, out float scale);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderDrawBlendMode(nint renderer, nint blendMode);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderDrawColor(nint renderer, out byte r, out byte g, out byte b,
        out byte a);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderDrawColorFloat(nint renderer, out float r, out float g, out float b,
        out float a);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetRenderDriver(int index);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetRenderer(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetRendererFromTexture(nint texture);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetRendererName(nint renderer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetRendererProperties(nint renderer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderLogicalPresentation(nint renderer, out int w, out int h,
        out RendererLogicalPresentation mode);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderLogicalPresentationRect(nint renderer, out FRect rect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetRenderMetalCommandEncoder(nint renderer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetRenderMetalLayer(nint renderer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderOutputSize(nint renderer, out int w, out int h);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderSafeArea(nint renderer, out Rect rect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderScale(nint renderer, out float scaleX, out float scaleY);

    // nint refers to a Texture*
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetRenderTarget(nint renderer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderViewport(nint renderer, out Rect rect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderVSync(nint renderer, out int vsync);

   
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetRenderWindow(nint renderer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderClear(nint renderer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderClipEnabled(nint renderer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderCoordinatesFromWindow(nint renderer, float windowX, float windowY,
        out float x, out float y);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderCoordinatesToWindow(nint renderer, float x, float y, out float windowX,
        out float windowY);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderDebugText(nint renderer, float x, float y, string str);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderDebugTextFormat(nint renderer, float x, float y, string fmt);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderFillRect(nint renderer, ref FRect rect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderFillRects(nint renderer, Span<FRect> rects, int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderGeometry(nint renderer, nint texture, Span<Vertex> vertices,
        int numVertices, Span<int> indices, int numIndices);

   
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderGeometryRaw(nint renderer, nint texture, nint xy, int xyStride,
        nint color, int colorStride, nint uv, int uvStride, int numVertices, nint indices, int numIndices,
        int sizeIndices);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderLine(nint renderer, float x1, float y1, float x2, float y2);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderLines(nint renderer, Span<FPoint> points, int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderPoint(nint renderer, float x, float y);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderPoints(nint renderer, Span<FPoint> points, int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderPresent(nint renderer);

   
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* SDL_RenderReadPixels(nint renderer, ref Rect rect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderRect(nint renderer, ref FRect rect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderRects(nint renderer, Span<FRect> rects, int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderTexture(nint renderer, nint texture, ref FRect srcrect,
        ref FRect dstrect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderTexture9Grid(nint renderer, nint texture, ref FRect srcrect,
        float leftWidth, float rightWidth, float topHeight, float bottomHeight, float scale,
        ref FRect dstrect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderTextureAffine(nint renderer, nint texture, in FRect srcrect,
        in FPoint origin, in FPoint right, in FPoint down);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderTextureRotated(nint renderer, nint texture, ref FRect srcrect,
        ref FRect dstrect, double angle, ref FPoint center, FlipMode flip);

   
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderTextureTiled(nint renderer, nint texture, ref FRect srcrect,
        float scale, ref FRect dstrect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderViewportSet(nint renderer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderClipRect(nint renderer, ref Rect rect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderColorScale(nint renderer, float scale);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderDrawBlendMode(nint renderer, uint blendMode);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderDrawColor(nint renderer, byte r, byte g, byte b, byte a);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderDrawColorFloat(nint renderer, float r, float g, float b, float a);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderLogicalPresentation(nint renderer, int w, int h,
        RendererLogicalPresentation mode);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderScale(nint renderer, float scaleX, float scaleY);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_SetRenderTarget(nint renderer, nint texture);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderViewport(nint renderer, ref Rect rect);

   
   
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderVSync(nint renderer, int vsync);
}