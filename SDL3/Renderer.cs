using SharpSDL3.Enums;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;

using static SharpSDL3.Sdl;
using SharpSDL3.Structs;

namespace SharpSDL3; 
public static unsafe partial class Renderer {
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_SetRenderTarget(nint renderer, nint texture); // WARN_UNKNOWN_POINTER_PARAMETER

    // nint refers to a Texture*
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetRenderTarget(nint renderer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderLogicalPresentation(nint renderer, int w, int h,
        RendererLogicalPresentation mode);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderLogicalPresentation(nint renderer, out int w, out int h,
        out RendererLogicalPresentation mode);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderLogicalPresentationRect(nint renderer, out FRect rect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderCoordinatesFromWindow(nint renderer, float windowX, float windowY,
        out float x, out float y);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderCoordinatesToWindow(nint renderer, float x, float y, out float windowX,
        out float windowY);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ConvertEventToRenderCoordinates(nint renderer, ref Event @event);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderViewport(nint renderer, ref Rect rect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderViewport(nint renderer, out Rect rect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderViewportSet(nint renderer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderSafeArea(nint renderer, out Rect rect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderClipRect(nint renderer, ref Rect rect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderClipRect(nint renderer, out Rect rect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderClipEnabled(nint renderer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderScale(nint renderer, float scaleX, float scaleY);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderScale(nint renderer, out float scaleX, out float scaleY);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderDrawColor(nint renderer, byte r, byte g, byte b, byte a);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderDrawColorFloat(nint renderer, float r, float g, float b, float a);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderDrawColor(nint renderer, out byte r, out byte g, out byte b,
        out byte a);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderDrawColorFloat(nint renderer, out float r, out float g, out float b,
        out float a);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderColorScale(nint renderer, float scale);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderColorScale(nint renderer, out float scale);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderDrawBlendMode(nint renderer, uint blendMode);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderDrawBlendMode(nint renderer, nint blendMode);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderClear(nint renderer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderPoint(nint renderer, float x, float y);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderPoints(nint renderer, Span<FPoint> points, int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderLine(nint renderer, float x1, float y1, float x2, float y2);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderLines(nint renderer, Span<FPoint> points, int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderRect(nint renderer, ref FRect rect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderRects(nint renderer, Span<FRect> rects, int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderFillRect(nint renderer, ref FRect rect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderFillRects(nint renderer, Span<FRect> rects, int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderTexture(nint renderer, nint texture, ref FRect srcrect,
        ref FRect dstrect); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderTextureRotated(nint renderer, nint texture, ref FRect srcrect,
        ref FRect dstrect, double angle, ref FPoint center, FlipMode flip); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderTextureAffine(nint renderer, nint texture, in FRect srcrect,
        in FPoint origin, in FPoint right, in FPoint down);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderTextureTiled(nint renderer, nint texture, ref FRect srcrect,
        float scale, ref FRect dstrect); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderTexture9Grid(nint renderer, nint texture, ref FRect srcrect,
        float leftWidth, float rightWidth, float topHeight, float bottomHeight, float scale,
        ref FRect dstrect); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderGeometry(nint renderer, nint texture, Span<Vertex> vertices,
        int numVertices, Span<int> indices, int numIndices); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderGeometryRaw(nint renderer, nint texture, nint xy, int xyStride,
        nint color, int colorStride, nint uv, int uvStride, int numVertices, nint indices, int numIndices,
        int sizeIndices); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* SDL_RenderReadPixels(nint renderer, ref Rect rect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderPresent(nint renderer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyTexture(nint texture); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyRenderer(nint renderer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_FlushRenderer(nint renderer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetRenderMetalLayer(nint renderer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetRenderMetalCommandEncoder(nint renderer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_AddVulkanRenderSemaphores(nint renderer, uint waitStageMask, long waitSemaphore,
        long signalSemaphore);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetRenderVSync(nint renderer, int vsync);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderVSync(nint renderer, out int vsync);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderDebugText(nint renderer, float x, float y, string str);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenderDebugTextFormat(nint renderer, float x, float y, string fmt);


    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetRendererFromTexture(nint texture); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumRenderDrivers();

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetRenderDriver(int index);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CreateWindowAndRenderer(string title, int width, int height,
        WindowFlags windowFlags, out nint window, out nint renderer);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateRenderer(nint window, string name);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateRendererWithProperties(uint props);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateSoftwareRenderer(nint surface);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetRenderer(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetRenderWindow(nint renderer);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetRendererName(nint renderer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetRendererProperties(nint renderer);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRenderOutputSize(nint renderer, out int w, out int h);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetCurrentRenderOutputSize(nint renderer, out int w, out int h);

}
