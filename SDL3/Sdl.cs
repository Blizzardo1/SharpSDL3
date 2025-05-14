using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using SharpSDL3.Enums;
using SharpSDL3.Structs;
using static SharpSDL3.Delegates;

namespace SharpSDL3;

public static unsafe partial class Sdl {
    internal const string NativeLibName = "SDL3";

    /// <summary>
    /// This macro turns the version numbers into a numeric value.
    /// </summary>
    /// <remarks>
    /// This macro is available since SDL 3.2.0.
    /// </remarks>
    /// <param name="major">The Major versiom number</param>
    /// <param name="minor">The Minor version number</param>
    /// <param name="patch">The Patch version number</param>
    /// <returns>The version as a number</returns>
    public static int VersionNum(int major, int minor, int patch) => major * 1000000 + minor * 1000 + patch;

    public static SdlBool AddHintCallback(string name, SdlHintCallback callback, nint userdata) {
        if (string.IsNullOrEmpty(name)) {
            Logger.LogWarn(LogCategory.System, "AddHintCallback: Name is null or empty.");
            return false;
        }
        if (callback == null) {
            Logger.LogWarn(LogCategory.System, "AddHintCallback: Callback is null.");
            return false;
        }
        return SDL_AddHintCallback(name, callback, userdata);
    }

    public static SdlBool AddSurfaceAlternateImage(nint surface, nint image) {
        if (surface == nint.Zero || image == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "AddSurfaceAlternateImage: Surface or image pointer is null.");
            return false;
        }
        return SDL_AddSurfaceAlternateImage(surface, image);
    }

    public static SdlBool BlitSurface(nint src, nint srcrect, nint dst, nint dstrect) {
        if (src == nint.Zero || dst == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "BlitSurface: Source or destination pointer is null.");
            return false;
        }
        return SDL_BlitSurface(src, srcrect, dst, dstrect);
    }

    public static SdlBool BlitSurface9Grid(nint src, nint srcrect, int leftWidth, int rightWidth, int topHeight, int bottomHeight, float scale, ScaleMode scaleMode, nint dst, nint dstrect) {
        if (src == nint.Zero || dst == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "BlitSurface9Grid: Source or destination pointer is null.");
            return false;
        }
        return SDL_BlitSurface9Grid(src, srcrect, leftWidth, rightWidth, topHeight, bottomHeight, scale, scaleMode, dst, dstrect);
    }

    public static SdlBool BlitSurfaceScaled(nint src, nint srcrect, nint dst, nint dstrect, ScaleMode scaleMode) {
        if (src == nint.Zero || dst == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "BlitSurfaceScaled: Source or destination pointer is null.");
            return false;
        }
        return SDL_BlitSurfaceScaled(src, srcrect, dst, dstrect, scaleMode);
    }

    public static SdlBool BlitSurfaceTiled(nint src, nint srcrect, nint dst, nint dstrect) {
        if (src == nint.Zero || dst == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "BlitSurfaceTiled: Source or destination pointer is null.");
            return false;
        }
        return SDL_BlitSurfaceTiled(src, srcrect, dst, dstrect);
    }

    public static SdlBool BlitSurfaceTiledWithScale(nint src, nint srcrect, float scale, ScaleMode scaleMode, nint dst, nint dstrect) {
        if (src == nint.Zero || dst == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "BlitSurfaceTiledWithScale: Source or destination pointer is null.");
            return false;
        }
        return SDL_BlitSurfaceTiledWithScale(src, srcrect, scale, scaleMode, dst, dstrect);
    }

    public static SdlBool BlitSurfaceUnchecked(nint src, nint srcrect, nint dst, nint dstrect) {
        if (src == nint.Zero || dst == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "BlitSurfaceUnchecked: Source or destination pointer is null.");
            return false;
        }
        return SDL_BlitSurfaceUnchecked(src, srcrect, dst, dstrect);
    }

    public static SdlBool BlitSurfaceUncheckedScaled(nint src, nint srcrect, nint dst, nint dstrect, ScaleMode scaleMode) {
        if (src == nint.Zero || dst == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "BlitSurfaceUncheckedScaled: Source or destination pointer is null.");
            return false;
        }
        return SDL_BlitSurfaceUncheckedScaled(src, srcrect, dst, dstrect, scaleMode);
    }

    public static void CleanupTLS() {
        SDL_CleanupTLS();
    }

    public static SdlBool ClearClipboardData() {
        return SDL_ClearClipboardData();
    }

    public static SdlBool ClearComposition(nint window) {
        if (window == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "ClearComposition: Window handle is null.");
            return false;
        }
        return SDL_ClearComposition(window);
    }

    public static SdlBool ClearError() {
        return SDL_ClearError();
    }

    public static SdlBool ClearProperty(uint props, string name) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            Logger.LogWarn(LogCategory.System, "ClearProperty: Properties handle is zero or name is null/empty.");
            return false;
        }
        return SDL_ClearProperty(props, name);
    }

    public static SdlBool ClearSurface(nint surface, float r, float g, float b, float a) {
        if (surface == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "ClearSurface: Surface pointer is null.");
            return false;
        }
        return SDL_ClearSurface(surface, r, g, b, a);
    }

    public static uint ComposeCustomBlendMode(BlendFactor srcColorFactor, BlendFactor dstColorFactor, BlendOperation colorOperation, BlendFactor srcAlphaFactor, BlendFactor dstAlphaFactor, BlendOperation alphaOperation) {
        return SDL_ComposeCustomBlendMode(srcColorFactor, dstColorFactor, colorOperation, srcAlphaFactor, dstAlphaFactor, alphaOperation);
    }

    public static SdlBool ConvertPixels(int width, int height, PixelFormat srcFormat, nint src, int srcPitch, PixelFormat dstFormat, nint dst, int dstPitch) {
        if (src == nint.Zero || dst == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "ConvertPixels: Source or destination pointer is null.");
            return false;
        }
        return SDL_ConvertPixels(width, height, srcFormat, src, srcPitch, dstFormat, dst, dstPitch);
    }

    public static SdlBool ConvertPixelsAndColorspace(int width, int height, PixelFormat srcFormat, Colorspace srcColorspace, uint srcProperties, nint src, int srcPitch, PixelFormat dstFormat, Colorspace dstColorspace, uint dstProperties, nint dst, int dstPitch) {
        if (src == nint.Zero || dst == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "ConvertPixelsAndColorspace: Source or destination pointer is null.");
            return false;
        }
        return SDL_ConvertPixelsAndColorspace(width, height, srcFormat, srcColorspace, srcProperties, src, srcPitch, dstFormat, dstColorspace, dstProperties, dst, dstPitch);
    }

    public static unsafe Surface* ConvertSurface(nint surface, PixelFormat format) {
        if (surface == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "ConvertSurface: Surface pointer is null.");
            return null;
        }
        return SDL_ConvertSurface(surface, format);
    }

    public static unsafe Surface* ConvertSurfaceAndColorspace(nint surface, PixelFormat format, nint palette, Colorspace colorspace, uint props) {
        if (surface == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "ConvertSurfaceAndColorspace: Surface pointer is null.");
            return null;
        }
        return SDL_ConvertSurfaceAndColorspace(surface, format, palette, colorspace, props);
    }

    public static SdlBool CopyProperties(uint src, uint dst) {
        if (src == 0 || dst == 0) {
            Logger.LogWarn(LogCategory.System, "CopyProperties: Source or destination properties handle is zero.");
            return false;
        }
        return SDL_CopyProperties(src, dst);
    }

    public static unsafe Palette* CreatePalette(int ncolors) {
        return SDL_CreatePalette(ncolors);
    }

    public static nint CreatePopupWindow(nint parent, int offsetX, int offsetY, int w, int h, WindowFlags flags) {
        return SDL_CreatePopupWindow(parent, offsetX, offsetY, w, h, flags);
    }

    public static uint CreateProperties() {
        uint props = SDL_CreateProperties();
        if (props == 0) {
            Logger.LogError(LogCategory.System, "CreateProperties: Failed to create properties.");
            throw new InvalidOperationException("SDL_CreateProperties failed.");
        }

        return props;
    }

    public static unsafe Surface* CreateSurface(int width, int height, PixelFormat format) {
        return SDL_CreateSurface(width, height, format);
    }

    public static unsafe Surface* CreateSurfaceFrom(int width, int height, PixelFormat format, nint pixels, int pitch) {
        if (pixels == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "CreateSurfaceFrom: Pixels pointer is null.");
            return null;
        }
        return SDL_CreateSurfaceFrom(width, height, format, pixels, pitch);
    }

    public static unsafe Palette* CreateSurfacePalette(nint surface) {
        if (surface == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "CreateSurfacePalette: Surface pointer is null.");
            return null;
        }
        return SDL_CreateSurfacePalette(surface);
    }

    public static nint CreateThreadRuntime(SdlThreadFunction fn, string name, nint data, nint pfnBeginThread, nint pfnEndThread) {
        if (fn == null) {
            Logger.LogWarn(LogCategory.System, "CreateThreadRuntime: Thread function is null.");
            return nint.Zero;
        }
        return SDL_CreateThreadRuntime(fn, name, data, pfnBeginThread, pfnEndThread);
    }

    public static nint CreateThreadWithPropertiesRuntime(uint props, nint pfnBeginThread, nint pfnEndThread) {
        if (props == 0) {
            Logger.LogWarn(LogCategory.System, "CreateThreadWithPropertiesRuntime: Properties handle is zero.");
            return nint.Zero;
        }
        if (pfnBeginThread == nint.Zero || pfnEndThread == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "CreateThreadWithPropertiesRuntime: Begin or End thread function pointer is null.");
            return nint.Zero;
        }

        nint threadHandle = SDL_CreateThreadWithPropertiesRuntime(props, pfnBeginThread, pfnEndThread);
        if (threadHandle == nint.Zero) {
            Logger.LogError(LogCategory.System, "CreateThreadWithPropertiesRuntime: Failed to create thread with properties.");
        }

        return threadHandle;
    }

    public static nint CreateWindow(string title, int w, int h, WindowFlags flags) {
        if (string.IsNullOrEmpty(title)) {
            Logger.LogWarn(LogCategory.System, "CreateWindow: Title is null or empty.");
            return nint.Zero;
        }
        return SDL_CreateWindow(title, w, h, flags);
    }

    public static nint CreateWindowWithProperties(uint props) {
        if (props == 0) {
            Logger.LogWarn(LogCategory.System, "CreateWindowWithProperties: Properties handle is zero.");
            return nint.Zero;
        }

        nint windowHandle = SDL_CreateWindowWithProperties(props);
        if (windowHandle == nint.Zero) {
            Logger.LogError(LogCategory.System, "CreateWindowWithProperties: Failed to create window with properties.");
            throw new InvalidOperationException("SDL_CreateWindowWithProperties failed.");
        }

        return windowHandle;
    }
    public static void DestroyPalette(nint palette) {
        if (palette == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "DestroyPalette: Palette pointer is null.");
            return;
        }
        SDL_DestroyPalette(palette);
    }

    public static void DestroyProperties(uint props) {
        if (props == 0) {
            Logger.LogWarn(LogCategory.System, "DestroyProperties: Properties handle is zero.");
            return;
        }
        SDL_DestroyProperties(props);
    }

    public static void DestroySurface(nint surface) {
        if (surface == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "DestroySurface: Surface pointer is null.");
            return;
        }
        SDL_DestroySurface(surface);
    }

    public static void DestroyWindow(nint window) {
        if (window == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "DestroyWindow: Window handle is null.");
            return;
        }
        SDL_DestroyWindow(window);
    }

    public static SdlBool DestroyWindowSurface(nint window) {
        if (window == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "DestroyWindowSurface: Window handle is null.");
            return false;
        }
        return SDL_DestroyWindowSurface(window);
    }

    public static void DetachThread(nint thread) {
        if (thread == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "DetachThread: Thread handle is null.");
            return;
        }
        SDL_DetachThread(thread);
    }

    public static SdlBool DisableScreenSaver() {
        return SDL_DisableScreenSaver();
    }

    public static unsafe Surface* DuplicateSurface(nint surface) {
        if (surface == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "DuplicateSurface: Surface pointer is null.");
            return null;
        }
        return SDL_DuplicateSurface(surface);
    }

    public static SdlBool EnableScreenSaver() {
        return SDL_EnableScreenSaver();
    }

    public static int EnterAppMainCallbacks(int argc, nint argv, SdlAppInitFunc appInit,
                                                                   SdlAppIterateFunc appIter, SdlAppEventFunc sdlAppEvent, SdlAppQuitFunc appQuit) {
        ArgumentNullException.ThrowIfNull(appInit);
        ArgumentNullException.ThrowIfNull(appIter);
        ArgumentNullException.ThrowIfNull(sdlAppEvent);
        ArgumentNullException.ThrowIfNull(appQuit);

        Logger.LogDebug(LogCategory.System, "Entering App Main Callbacks with provided delegates.");

        return SDL_EnterAppMainCallbacks(argc, argv, appInit, appIter, sdlAppEvent, appQuit);
    }

    public static SdlBool EnumerateProperties(uint props, SdlEnumeratePropertiesCallback callback, nint userdata) {
        if (props == 0 || callback == null) {
            Logger.LogWarn(LogCategory.System, "EnumerateProperties: Properties handle is zero or callback is null.");
            return false;
        }
        return SDL_EnumerateProperties(props, callback, userdata);
    }

    public static bool FillSurfaceRect(nint dst, Rect rect, uint color) {
        if (dst == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "FillSurfaceRect: Destination pointer is null.");
            return false;
        }
        nint rectPtr = Marshal.AllocHGlobal(Marshal.SizeOf<Rect>());
        Marshal.StructureToPtr(rect, rectPtr, false);
        bool result = SDL_FillSurfaceRect(dst, rectPtr, color);
        if (!result) {
            Logger.LogError(LogCategory.System, "FillSurfaceRect: Failed to fill surface rectangle.");
        }
        Marshal.FreeHGlobal(rectPtr);
        return result;
    }

    public static SdlBool FillSurfaceRects(nint dst, Span<Rect> rects, uint color) {
        if (dst == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "FillSurfaceRects: Destination pointer is null.");
            return false;
        }
        if (rects.IsEmpty) {
            Logger.LogWarn(LogCategory.System, "FillSurfaceRects: Rectangles span is empty.");
            return false;
        }
        bool result = SDL_FillSurfaceRects(dst, rects, (int)rects.Length, color);
        if (!result) {
            Logger.LogError(LogCategory.System, "FillSurfaceRects: Failed to fill surface rectangles.");
        }
        return result;
    }

    public static bool FlashWindow(nint window, FlashOperation operation) {
        if (window == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "FlashWindow: Window handle is null.");
            return false;
        }

        if (!Enum.IsDefined(operation)) {
            Logger.LogError(LogCategory.System, "FlashWindow: Invalid flash operation.");
            return false;
        }

        bool result = SDL_FlashWindow(window, operation);
        if (!result) {
            Logger.LogError(LogCategory.System, "FlashWindow: Failed to flash window.");
        }
        return result;
    }

    public static SdlBool FlipSurface(nint surface, FlipMode flip) {
        if (surface == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "FlipSurface: Surface pointer is null.");
            return false;
        }
        return SDL_FlipSurface(surface, flip);
    }

    public static void Free(nint mem) {
        if (mem == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "Free: Memory pointer is null.");
            return;
        }

        SDL_free(mem);
    }

    public static void GDKSuspendComplete() {
        SDL_GDKSuspendComplete();
    }

    public static string GetAppMetadataProperty(string name) {
        if (string.IsNullOrEmpty(name)) {
            Logger.LogWarn(LogCategory.System, "GetAppMetadataProperty: Name is null or empty.");
            return string.Empty;
        }
        string result = SDL_GetAppMetadataProperty(name);
        if (string.IsNullOrEmpty(result)) {
            Logger.LogError(LogCategory.System, "GetAppMetadataProperty: Failed to retrieve property.");
        }
        return result;
    }

    public static bool GetBooleanProperty(uint props, string name, bool defaultValue) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            Logger.LogWarn(LogCategory.System, "GetBooleanProperty: Properties handle is zero or name is null/empty.");
            return defaultValue;
        }
        SdlBool result = SDL_GetBooleanProperty(props, name, defaultValue);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetBooleanProperty: Failed to retrieve boolean property.");
        }
        return result;
    }

    public static Span<nint> GetClipboardData(string mimeType) {
        if (string.IsNullOrEmpty(mimeType)) {
            Logger.LogWarn(LogCategory.System, "GetClipboardData: MimeType is null or empty.");
            return [];
        }
        nint result = SDL_GetClipboardData(mimeType, out nuint size);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetClipboardData: Failed to retrieve clipboard data.");
            return [];
        }
        return new Span<nint>((void*)result, (int)size);
    }

    public static Span<nint> GetClipboardMimeTypes() {
        nint result = SDL_GetClipboardMimeTypes(out nuint numMimeTypes);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetClipboardMimeTypes: Failed to retrieve clipboard mime types.");
            return [];
        }
        return new Span<nint>((void*)result, (int)numMimeTypes);
    }

    public static nint GetClipboardMimeTypes(out nuint numMimeTypes) {
        nint result = SDL_GetClipboardMimeTypes(out numMimeTypes);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetClipboardMimeTypes: Failed to retrieve clipboard mime types.");
            return nint.Zero;
        }
        return result;
    }

    public static string GetClipboardText() {
        string result = SDL_GetClipboardText();
        if (string.IsNullOrEmpty(result)) {
            Logger.LogError(LogCategory.System, "GetClipboardText: Failed to retrieve clipboard text.");
        }
        return result;
    }

    public static bool GetClosestFullscreenDisplayMode(uint displayId, int w, int h, float refreshRate,
            SdlBool includeHighDensityModes, out DisplayMode closest) {
        if (displayId == 0) {
            Logger.LogWarn(LogCategory.System, "GetClosestFullscreenDisplayMode: Display ID is zero.");
            closest = default;
            return false;
        }
        SdlBool result = SDL_GetClosestFullscreenDisplayMode(displayId, w, h, refreshRate, includeHighDensityModes,
            out closest);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetClosestFullscreenDisplayMode: Failed to retrieve closest mode.");
        }
        return result;
    }

    public static DisplayMode* GetCurrentDisplayMode(uint displayId) {
        if (displayId == 0) {
            Logger.LogWarn(LogCategory.System, "GetCurrentDisplayMode: Display ID is zero.");
            return null;
        }
        DisplayMode* mode = SDL_GetCurrentDisplayMode(displayId);
        if (mode == null) {
            Logger.LogError(LogCategory.System, "GetCurrentDisplayMode: Failed to retrieve current mode.");
        }
        return mode;
    }

    public static void GetCurrentDisplayMode(uint displayId, out DisplayMode mode) {
        if (displayId == 0) {
            Logger.LogWarn(LogCategory.System, "GetCurrentDisplayMode: Display ID is zero.");
            mode = default;
            return;
        }
        DisplayMode* modePtr = SDL_GetCurrentDisplayMode(displayId);
        if (modePtr == null) {
            Logger.LogError(LogCategory.System, "GetCurrentDisplayMode: Failed to retrieve current mode.");
            mode = default;
            return;
        }
        mode = *modePtr;
    }

    public static DisplayOrientation GetCurrentDisplayOrientation(uint displayId) {
        if (displayId == 0) {
            Logger.LogWarn(LogCategory.System, "GetCurrentDisplayOrientation: Display ID is zero.");
            return DisplayOrientation.Unknown;
        }
        DisplayOrientation orientation = SDL_GetCurrentDisplayOrientation(displayId);
        if (orientation == DisplayOrientation.Unknown) {
            Logger.LogError(LogCategory.System, "GetCurrentDisplayOrientation: Failed to retrieve orientation.");
        }
        return orientation;
    }

    public static ulong GetCurrentThreadID() {
        ulong threadId = SDL_GetCurrentThreadID();
        if (threadId == 0) {
            Logger.LogError(LogCategory.System, "GetCurrentThreadID: Failed to retrieve thread ID.");
        }
        return threadId;
    }

    public static string GetCurrentVideoDriver() {
        return SDL_GetCurrentVideoDriver();
    }

    public static DisplayMode* GetDesktopDisplayMode(uint displayId) {
        if (displayId == 0) {
            Logger.LogWarn(LogCategory.System, "GetDesktopDisplayMode: Display ID is zero.");
            return null;
        }
        DisplayMode* mode = SDL_GetDesktopDisplayMode(displayId);
        if (mode == null) {
            Logger.LogError(LogCategory.System, "GetDesktopDisplayMode: Failed to retrieve desktop mode.");
        }
        return mode;
    }

    public static void GetDesktopDisplayMode(uint displayId, out DisplayMode mode) {
        if (displayId == 0) {
            Logger.LogWarn(LogCategory.System, "GetDesktopDisplayMode: Display ID is zero.");
            mode = default;
            return;
        }
        DisplayMode* modePtr = SDL_GetDesktopDisplayMode(displayId);
        if (modePtr == null) {
            Logger.LogError(LogCategory.System, "GetDesktopDisplayMode: Failed to retrieve desktop mode.");
            mode = default;
            return;
        }
        mode = *modePtr;
    }

    public static bool GetDisplayBounds(uint displayId, out Rect rect) {
        if (displayId == 0) {
            Logger.LogWarn(LogCategory.System, "GetDisplayBounds: Display ID is zero.");
            rect = default;
            return false;
        }
        SdlBool result = SDL_GetDisplayBounds(displayId, out rect);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetDisplayBounds: Failed to retrieve display bounds.");
        }
        return result;
    }

    public static float GetDisplayContentScale(uint displayId) {
        if (displayId == 0) {
            Logger.LogWarn(LogCategory.System, "GetDisplayContentScale: Display ID is zero.");
            return 0f;
        }
        float scale = SDL_GetDisplayContentScale(displayId);
        if (scale == 0f) {
            Logger.LogError(LogCategory.System, "GetDisplayContentScale: Failed to retrieve content scale.");
        }
        return scale;
    }

    public static uint GetDisplayForPoint(ref Point point) {
        uint displayId = SDL_GetDisplayForPoint(ref point);
        if (displayId == 0) {
            Logger.LogError(LogCategory.System, "GetDisplayForPoint: Failed to retrieve display ID.");
        }
        return displayId;
    }

    public static uint GetDisplayForRect(ref Rect rect) {
        uint displayId = SDL_GetDisplayForRect(ref rect);
        if (displayId == 0) {
            Logger.LogError(LogCategory.System, "GetDisplayForRect: Failed to retrieve display ID.");
        }
        return displayId;
    }

    public static uint GetDisplayForWindow(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetDisplayForWindow: Window handle is null.");
            return 0;
        }
        uint displayId = SDL_GetDisplayForWindow(window);
        if (displayId == 0) {
            Logger.LogError(LogCategory.System, "GetDisplayForWindow: Failed to retrieve display ID.");
        }
        return displayId;
    }

    public static string GetDisplayName(uint displayId) {
        if (displayId == 0) {
            Logger.LogWarn(LogCategory.System, "GetDisplayName: Display ID is zero.");
            return string.Empty;
        }
        string name = SDL_GetDisplayName(displayId);
        if (string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.System, "GetDisplayName: Failed to retrieve display name.");
        }
        return name;
    }

    public static uint GetDisplayProperties(uint displayId) {
        if (displayId == 0) {
            Logger.LogWarn(LogCategory.System, "GetDisplayProperties: Display ID is zero.");
            return 0;
        }
        uint props = SDL_GetDisplayProperties(displayId);
        if (props == 0) {
            Logger.LogError(LogCategory.System, "GetDisplayProperties: Failed to retrieve display properties.");
        }
        return props;
    }

    public static Span<nint> GetDisplays() {
        Span<nint> result = GetDisplays(out int _);
        if (result == []) {
            return [];
        }
        return result;
    }

    public static Span<nint> GetDisplays(out int count) {
        nint result = SDL_GetDisplays(out count);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetDisplays: Failed to retrieve display handles.");
            return [];
        }
        return new Span<nint>((void*)result, count);
    }

    public static bool GetDisplayUsableBounds(uint displayId, out Rect rect) {
        if (displayId == 0) {
            Logger.LogWarn(LogCategory.System, "GetDisplayUsableBounds: Display ID is zero.");
            rect = default;
            return false;
        }
        SdlBool result = SDL_GetDisplayUsableBounds(displayId, out rect);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetDisplayUsableBounds: Failed to retrieve usable bounds.");
        }
        return result;
    }

    // Safe wrapper method
    public static string GetError() {
        string error = SDL_GetError();
        return string.IsNullOrEmpty(error) ? "No error." : error;
    }

    public static float GetFloatProperty(uint props, string name, float defaultValue) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            Logger.LogWarn(LogCategory.System, "GetFloatProperty: Properties handle is zero or name is null/empty.");
            return defaultValue;
        }
        float result = SDL_GetFloatProperty(props, name, defaultValue);
        if (result <= 0.1f) {
            Logger.LogError(LogCategory.System, "GetFloatProperty: Failed to retrieve float property.");
        }
        return result;
    }

    public static Span<int> GetFullscreenDisplayModes(uint displayId) {
        nint result = SDL_GetFullscreenDisplayModes(displayId, out int count);
        return new Span<int>((void*)result, count);
    }

    public static Span<nint> GetFullscreenDisplayModes(uint displayId, out int count) {
        nint result = SDL_GetFullscreenDisplayModes(displayId, out count);
        return new Span<nint>((void*)result, count);
    }

    public static uint GetGlobalProperties() {
        return SDL_GetGlobalProperties();
    }

    public static nint GetGrabbedWindow() {
        nint window = SDL_GetGrabbedWindow();
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetGrabbedWindow: Failed to retrieve grabbed window.");
        }
        return window;
    }

    public static string GetHint(string name) {
        if (string.IsNullOrEmpty(name)) {
            Logger.LogWarn(LogCategory.System, "GetHint: Name is null or empty.");
            return string.Empty;
        }
        string result = SDL_GetHint(name);
        if (string.IsNullOrEmpty(result)) {
            Logger.LogError(LogCategory.System, "GetHint: Failed to retrieve hint.");
        }
        return result;
    }

    public static bool GetHintBoolean(string name, bool defaultValue) {
        if (string.IsNullOrEmpty(name)) {
            Logger.LogWarn(LogCategory.System, "GetHintBoolean: Name is null or empty.");
            return defaultValue;
        }
        SdlBool result = SDL_GetHintBoolean(name, defaultValue);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetHintBoolean: Failed to retrieve hint boolean.");
        }
        return result;
    }

    public static nint GetKeyboard(out int count) {
        nint result = SDL_GetKeyboards(out count);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetKeyboard: Failed to retrieve keyboard handles.");
            return nint.Zero;
        }
        return result;
    }

    public static Span<nint> GetKeyboard() {
        nint result = GetKeyboard(out int count);
        if (result == nint.Zero) {
            return [];
        }
        return new Span<nint>((void*)result, count);
    }

    public static nint GetKeyboardFocus() {
        nint window = SDL_GetKeyboardFocus();
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetKeyboardFocus: Failed to retrieve keyboard focus.");
        }
        return window;
    }

    public static string GetKeyboardNameForID(uint instanceId) {
        if (instanceId == 0) {
            Logger.LogWarn(LogCategory.System, "GetKeyboardNameForID: Instance ID is zero.");
            return string.Empty;
        }
        string name = SDL_GetKeyboardNameForID(instanceId);
        if (string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.System, "GetKeyboardNameForID: Failed to retrieve keyboard name.");
        }
        return name;
    }

    public static Span<SdlBool> GetKeyboardState(out int numkeys) {
        nint result = SDL_GetKeyboardState(out numkeys);
        return new Span<SdlBool>((void*)result, numkeys);
    }

    public static uint GetKeyFromName(string name) {
        if (string.IsNullOrEmpty(name)) {
            Logger.LogWarn(LogCategory.System, "GetKeyFromName: Name is null or empty.");
            return 0;
        }
        uint key = SDL_GetKeyFromName(name);
        if (key == 0) {
            Logger.LogError(LogCategory.System, "GetKeyFromName: Failed to retrieve key from name.");
        }
        return key;
    }

    public static uint GetKeyFromScancode(ScanCode scanCode, KeyMod modstate, SdlBool keyEvent) {
        if (scanCode == ScanCode.Unknown) {
            Logger.LogWarn(LogCategory.System, "GetKeyFromScancode: Scan code is unknown.");
            return 0;
        }
        uint key = SDL_GetKeyFromScancode(scanCode, modstate, keyEvent);
        if (key == 0) {
            Logger.LogError(LogCategory.System, "GetKeyFromScancode: Failed to retrieve key from scan code.");
        }
        return key;
    }

    public static string GetKeyName(uint key) {
        if (key == 0) {
            Logger.LogWarn(LogCategory.System, "GetKeyName: Key is zero.");
            return string.Empty;
        }
        string name = SDL_GetKeyName(key);
        if (string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.System, "GetKeyName: Failed to retrieve key name.");
        }
        return name;
    }

    public static SdlBool GetMasksForPixelFormat(PixelFormat format, out int bpp, out uint rmask, out uint gmask,
        out uint bmask, out uint amask) {
        if (format == PixelFormat.Unknown) {
            Logger.LogWarn(LogCategory.System, "GetMasksForPixelFormat: Format is unknown.");
            bpp = 0;
            rmask = 0;
            gmask = 0;
            bmask = 0;
            amask = 0;
            return false;
        }
        SdlBool result = SDL_GetMasksForPixelFormat(format, out bpp, out rmask, out gmask, out bmask, out amask);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetMasksForPixelFormat: Failed to retrieve masks for pixel format.");
        }
        return result;
    }

    public static KeyMod GetModState() {
        return SDL_GetModState();
    }

    public static DisplayOrientation GetNaturalDisplayOrientation(uint displayId) {
        if (displayId == 0) {
            Logger.LogWarn(LogCategory.System, "GetNaturalDisplayOrientation: Display ID is zero.");
            return DisplayOrientation.Unknown;
        }
        DisplayOrientation orientation = SDL_GetNaturalDisplayOrientation(displayId);
        if (orientation == DisplayOrientation.Unknown) {
            Logger.LogError(LogCategory.System, "GetNaturalDisplayOrientation: Failed to retrieve orientation.");
        }
        return orientation;
    }

    public static long GetNumberProperty(uint props, string name, long defaultValue) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            Logger.LogWarn(LogCategory.System, "GetNumberProperty: Properties handle is zero or name is null/empty.");
            return defaultValue;
        }
        long result = SDL_GetNumberProperty(props, name, defaultValue);
        if (result <= 0) {
            Logger.LogError(LogCategory.System, "GetNumberProperty: Failed to retrieve number property.");
        }
        return result;
    }

    public static int GetNumVideoDrivers() {
        int numDrivers = SDL_GetNumVideoDrivers();
        if (numDrivers <= 0) {
            Logger.LogError(LogCategory.System, "GetNumVideoDrivers: Failed to retrieve number of video drivers.");
        }
        return numDrivers;
    }

    public static PixelFormatDetails* GetPixelFormatDetails(PixelFormat format) {
        if (format == PixelFormat.Unknown) {
            Logger.LogWarn(LogCategory.System, "GetPixelFormatDetails: Format is unknown.");
            return null;
        }
        PixelFormatDetails* details = SDL_GetPixelFormatDetails(format);
        if (details == null) {
            Logger.LogError(LogCategory.System, "GetPixelFormatDetails: Failed to retrieve pixel format details.");
        }
        return details;
    }

    public static PixelFormat GetPixelFormatForMasks(int bpp, uint rmask, uint gmask, uint bmask, uint amask) {
        if (bpp <= 0 || rmask == 0 || gmask == 0 || bmask == 0) {
            Logger.LogWarn(LogCategory.System, "GetPixelFormatForMasks: Invalid parameters.");
            return PixelFormat.Unknown;
        }
        PixelFormat format = SDL_GetPixelFormatForMasks(bpp, rmask, gmask, bmask, amask);
        if (format == PixelFormat.Unknown) {
            Logger.LogError(LogCategory.System, "GetPixelFormatForMasks: Failed to retrieve pixel format.");
        }
        return format;
    }

    public static string GetPixelFormatName(PixelFormat format) {
        if (format == PixelFormat.Unknown) {
            Logger.LogWarn(LogCategory.System, "GetPixelFormatName: Format is unknown.");
            return string.Empty;
        }
        string name = SDL_GetPixelFormatName(format);
        if (string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.System, "GetPixelFormatName: Failed to retrieve pixel format name.");
        }
        return name;
    }

    public static nint GetPointerProperty(uint props, string name, nint defaultValue) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            Logger.LogWarn(LogCategory.System, "GetPointerProperty: Properties handle is zero or name is null/empty.");
            return defaultValue;
        }
        nint result = SDL_GetPointerProperty(props, name, defaultValue);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetPointerProperty: Failed to retrieve pointer property.");
        }
        return result;
    }

    public static PowerState GetPowerInfo(out int seconds, out int percent) {
        PowerState state = SDL_GetPowerInfo(out seconds, out percent);
        if (state == PowerState.Unknown) {
            Logger.LogError(LogCategory.System, "GetPowerInfo: Failed to retrieve power info.");
        }
        return state;
    }

    public static Span<nint> GetPreferredLocales() {
        nint result = SDL_GetPreferredLocales(out int count);
        return new Span<nint>((void*)result, count);
    }

    public static uint GetPrimaryDisplay() {
        uint displayId = SDL_GetPrimaryDisplay();
        if (displayId == 0) {
            Logger.LogError(LogCategory.System, "GetPrimaryDisplay: Failed to retrieve primary display ID.");
        }
        return displayId;
    }

    public static string GetPrimarySelectionText() {
        string text = SDL_GetPrimarySelectionText();
        if (string.IsNullOrEmpty(text)) {
            Logger.LogError(LogCategory.System, "GetPrimarySelectionText: Failed to retrieve primary selection text.");
        }
        return text;
    }

    public static PropertyType GetPropertyType(uint props, string name) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            Logger.LogWarn(LogCategory.System, "GetPropertyType: Properties handle is zero or name is null/empty.");
        }
        return SDL_GetPropertyType(props, name);
    }

    public static SdlBool GetRectAndLineIntersection(ref Rect rect, ref int x1, ref int y1, ref int x2, ref int y2) {
        SdlBool result = SDL_GetRectAndLineIntersection(ref rect, ref x1, ref y1, ref x2, ref y2);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetRectAndLineIntersection: Failed to retrieve intersection.");
        }
        return result;
    }

    public static SdlBool GetRectAndLineIntersectionFloat(ref FRect rect, ref float x1, ref float y1, ref float x2,
        ref float y2) {
        SdlBool result = SDL_GetRectAndLineIntersectionFloat(ref rect, ref x1, ref y1, ref x2, ref y2);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetRectAndLineIntersectionFloat: Failed to retrieve intersection.");
        }
        return result;
    }

    public static SdlBool GetRectEnclosingPoints(Span<Point> points, int count, ref Rect clip, out Rect result) {
        SdlBool resultBool = SDL_GetRectEnclosingPoints(points, count, ref clip, out result);
        if (!resultBool) {
            Logger.LogError(LogCategory.System, "GetRectEnclosingPoints: Failed to retrieve enclosing points.");
        }
        return resultBool;
    }

    public static SdlBool GetRectEnclosingPointsFloat(Span<FPoint> points, int count, ref FRect clip, out FRect result) {
        SdlBool resultBool = SDL_GetRectEnclosingPointsFloat(points, count, ref clip, out result);
        if (!resultBool) {
            Logger.LogError(LogCategory.System, "GetRectEnclosingPointsFloat: Failed to retrieve enclosing points.");
        }
        return resultBool;
    }

    public static SdlBool GetRectIntersection(ref Rect a, ref Rect b, out Rect result) {
        SdlBool resultBool = SDL_GetRectIntersection(ref a, ref b, out result);
        if (!resultBool) {
            Logger.LogError(LogCategory.System, "GetRectIntersection: Failed to retrieve intersection.");
        }
        return resultBool;
    }

    public static SdlBool GetRectIntersectionFloat(ref FRect a, ref FRect b, out FRect result) {
        SdlBool resultBool = SDL_GetRectIntersectionFloat(ref a, ref b, out result);
        if (!resultBool) {
            Logger.LogError(LogCategory.System, "GetRectIntersectionFloat: Failed to retrieve intersection.");
        }
        return resultBool;
    }

    public static SdlBool GetRectUnion(ref Rect a, ref Rect b, out Rect result) {
        SdlBool resultBool = SDL_GetRectUnion(ref a, ref b, out result);
        if (!resultBool) {
            Logger.LogError(LogCategory.System, "GetRectUnion: Failed to retrieve union.");
        }
        return resultBool;
    }

    public static SdlBool GetRectUnionFloat(ref FRect a, ref FRect b, out FRect result) {
        SdlBool resultBool = SDL_GetRectUnionFloat(ref a, ref b, out result);
        if (!resultBool) {
            Logger.LogError(LogCategory.System, "GetRectUnionFloat: Failed to retrieve union.");
        }
        return resultBool;
    }

    public static Color GetRGB(uint pixel, nint format, nint palette) {
        if (format == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetRGB: Format pointer is null.");
            throw new ArgumentNullException(nameof(format), "Format pointer cannot be null.");
        }

        if (palette == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "GetRGB: Palette pointer is null. Defaulting to no palette.");
        }

        SDL_GetRGB(pixel, format, palette, out byte r, out byte g, out byte b);
        return new Color() { R = r, G = g, B = b, A = 255 };
    }

    public static Color GetRGBA(uint pixel, nint format, nint palette) {
        if (format == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetRGBA: Format pointer is null.");
            throw new ArgumentNullException(nameof(format), "Format pointer cannot be null.");
        }
        if (palette == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "GetRGBA: Palette pointer is null. Defaulting to no palette.");
        }
        SDL_GetRGBA(pixel, format, palette, out byte r, out byte g, out byte b, out byte a);
        return new Color() { R = r, G = g, B = b, A = a };
    }

    public static ScanCode GetScancodeFromKey(uint key, nint modstate) {
        if (key == 0) {
            Logger.LogWarn(LogCategory.System, "GetScancodeFromKey: Key is zero.");
            return ScanCode.Unknown;
        }
        ScanCode scanCode = SDL_GetScancodeFromKey(key, modstate);
        if (scanCode == ScanCode.Unknown) {
            Logger.LogError(LogCategory.System, "GetScancodeFromKey: Failed to retrieve scan code from key.");
        }
        return scanCode;
    }

    public static ScanCode GetScancodeFromName(string name) {
        if (string.IsNullOrEmpty(name)) {
            Logger.LogWarn(LogCategory.System, "GetScancodeFromName: Name is null or empty.");
            return ScanCode.Unknown;
        }
        ScanCode scanCode = SDL_GetScancodeFromName(name);
        if (scanCode == ScanCode.Unknown) {
            Logger.LogError(LogCategory.System, "GetScancodeFromName: Failed to retrieve scan code from name.");
        }
        return scanCode;
    }

    public static string GetScancodeName(ScanCode scanCode) {
        if (scanCode == ScanCode.Unknown) {
            Logger.LogWarn(LogCategory.System, "GetScancodeName: Scan code is unknown.");
            return string.Empty;
        }
        string name = SDL_GetScancodeName(scanCode);
        if (string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.System, "GetScancodeName: Failed to retrieve scan code name.");
        }
        return name;
    }

    public static string GetStringProperty(uint props, string name, string defaultValue) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            Logger.LogWarn(LogCategory.System, "GetStringProperty: Properties handle is zero or name is null/empty.");
            return defaultValue;
        }
        string result = SDL_GetStringProperty(props, name, defaultValue);
        if (string.IsNullOrEmpty(result)) {
            Logger.LogError(LogCategory.System, "GetStringProperty: Failed to retrieve string property.");
        }
        return result;
    }

    public static bool GetSurfaceAlphaMod(nint surface, out byte alpha) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetSurfaceAlphaMod: Surface pointer is null.");
            alpha = 0;
            return false;
        }
        SdlBool result = SDL_GetSurfaceAlphaMod(surface, out alpha);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetSurfaceAlphaMod: Failed to retrieve surface alpha mod.");
        }
        return result;
    }

    public static Palette* GetSurfaceBalette(nint surface) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetSurfacePalette: Surface pointer is null.");
            return null;
        }
        Palette* palette = SDL_GetSurfacePalette(surface);
        if (palette == null) {
            Logger.LogError(LogCategory.System, "GetSurfacePalette: Failed to retrieve surface palette.");
        }
        return palette;
    }

    public static bool GetSurfaceBlendMode(nint surface, nint blendMode) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetSurfaceBlendMode: Surface pointer is null.");
            return false;
        }
        SdlBool result = SDL_GetSurfaceBlendMode(surface, blendMode);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetSurfaceBlendMode: Failed to retrieve surface blend mode.");
        }
        return result;
    }

    public static bool GetSurfaceClipRect(nint surface, out Rect rect) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetSurfaceClipRect: Surface pointer is null.");
            rect = default;
            return false;
        }
        SdlBool result = SDL_GetSurfaceClipRect(surface, out rect);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetSurfaceClipRect: Failed to retrieve surface clip rect.");
        }
        return result;
    }

    public static bool GetSurfaceColorKey(nint surface, out uint key) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetSurfaceColorKey: Surface pointer is null.");
            key = 0;
            return false;
        }
        SdlBool result = SDL_GetSurfaceColorKey(surface, out key);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetSurfaceColorKey: Failed to retrieve surface color key.");
        }
        return result;
    }

    public static bool GetSurfaceColorMod(nint surface, out byte r, out byte g, out byte b) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetSurfaceColorMod: Surface pointer is null.");
            r = g = b = 0;
            return false;
        }
        SdlBool result = SDL_GetSurfaceColorMod(surface, out r, out g, out b);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetSurfaceColorMod: Failed to retrieve surface color mod.");
        }
        return result;
    }

    public static Span<nint> GetSurfaceImages(nint surface, out int count) {
        nint result = SDL_GetSurfaceImages(surface, out count);
        return new Span<nint>((void*)result, count);
    }

    public static uint GetSurfaceProperties(nint surface) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetSurfaceProperties: Surface pointer is null.");
            return 0;
        }
        uint properties = SDL_GetSurfaceProperties(surface);
        if (properties == 0) {
            Logger.LogError(LogCategory.System, "GetSurfaceProperties: Failed to retrieve surface properties.");
        }
        return properties;
    }

    public static SystemTheme GetSystemTheme() {
        SystemTheme theme = SDL_GetSystemTheme();
        if (theme == SystemTheme.Unknown) {
            Logger.LogError(LogCategory.System, "GetSystemTheme: Failed to retrieve system theme.");
        }
        return theme;
    }

    public static bool GetTextInputArea(nint window, out Rect rect, out int cursor) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetTextInputArea: Window pointer is null.");
            rect = default;
            cursor = 0;
            return false;
        }
        SdlBool result = SDL_GetTextInputArea(window, out rect, out cursor);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetTextInputArea: Failed to retrieve text input area.");
        }
        return result;
    }

    public static ulong GetThreadId(nint thread) {
        if (thread == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetThreadId: Thread pointer is null.");
            return 0;
        }
        ulong threadId = SDL_GetThreadID(thread);
        if (threadId == 0) {
            Logger.LogError(LogCategory.System, "GetThreadId: Failed to retrieve thread ID.");
        }
        return threadId;
    }

    public static string GetThreadName(nint thread) {
        if (thread == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetThreadName: Thread pointer is null.");
            return string.Empty;
        }
        string name = SDL_GetThreadName(thread);
        if (string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.System, "GetThreadName: Failed to retrieve thread name.");
        }
        return name;
    }

    public static ThreadState GetThreadState(nint thread) {
        if (thread == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetThreadState: Thread pointer is null.");
            return ThreadState.Unknown;
        }
        ThreadState state = SDL_GetThreadState(thread);
        if (state == ThreadState.Unknown) {
            Logger.LogError(LogCategory.System, "GetThreadState: Failed to retrieve thread state.");
        }
        return state;
    }

    public static nint GetTLS(nint id) {
        if (id == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetTLS: ID is zero.");
            return nint.Zero;
        }
        nint tls = SDL_GetTLS(id);
        if (tls == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetTLS: Failed to retrieve TLS value.");
        }
        return tls;
    }

    public static string GetVideoDriver(int index) {
        if (index < 0) {
            Logger.LogError(LogCategory.System, "GetVideoDriver: Index is negative.");
            return string.Empty;
        }
        string driver = SDL_GetVideoDriver(index);
        if (string.IsNullOrEmpty(driver)) {
            Logger.LogError(LogCategory.System, "GetVideoDriver: Failed to retrieve video driver.");
        }
        return driver;
    }

    public static bool GetWindowAspectRatio(nint window, out float minAspect, out float maxAspect) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowAspectRatio: Window pointer is null.");
            minAspect = maxAspect = 0;
            return false;
        }
        SdlBool result = SDL_GetWindowAspectRatio(window, out minAspect, out maxAspect);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetWindowAspectRatio: Failed to retrieve window aspect ratio.");
        }
        return result;
    }

    public static bool GetWindowBorderSize(nint window, out int top, out int left, out int bottom, out int right) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowBorderSize: Window pointer is null.");
            top = left = bottom = right = 0;
            return false;
        }
        SdlBool result = SDL_GetWindowBordersSize(window, out top, out left, out bottom, out right);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetWindowBorderSize: Failed to retrieve window border size.");
        }
        return result;
    }

    public static Rect GetWindowBorderSize(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowBorderSize: Window pointer is null.");
            return default;
        }
        SdlBool result = SDL_GetWindowBordersSize(window, out int top, out int left, out int bottom, out int right);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetWindowBorderSize: Failed to retrieve window border size.");
        }
        return new Rect() { X = left, Y = top, W = right - left, H = bottom - top };
    }

    public static float GetWindowDisplayScale(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowDisplayScale: Window pointer is null.");
            return 0;
        }
        float scale = SDL_GetWindowDisplayScale(window);
        if (scale <= 0) {
            Logger.LogError(LogCategory.System, "GetWindowDisplayScale: Failed to retrieve window display scale.");
        }
        return scale;
    }

    public static WindowFlags GetWindowFlags(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowFlags: Window handle is null.");
            return 0;
        }
        WindowFlags flags = SDL_GetWindowFlags(window);
        if (flags == 0) {
            Logger.LogWarn(LogCategory.System, "GetWindowFlags: Failed to retrieve window flags.");
        }
        return flags;
    }

    public static nint GetWindowFromId(uint id) {
        if (id == 0) {
            Logger.LogError(LogCategory.System, "GetWindowFromId: Window ID is zero.");
            return nint.Zero;
        }
        nint windowHandle = SDL_GetWindowFromID(id);
        if (windowHandle == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "GetWindowFromId: Failed to retrieve window handle.");
        }
        return windowHandle;
    }

    public static DisplayMode* GetWindowFullscreenMode(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowFullscreenMode: Window pointer is null.");
            return null;
        }
        DisplayMode* mode = SDL_GetWindowFullscreenMode(window);
        if (mode == null) {
            Logger.LogError(LogCategory.System, "GetWindowFullscreenMode: Failed to retrieve window fullscreen mode.");
        }
        return mode;
    }

    public static nint GetWindowICCProfile(nint window, out nuint size) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowICCProfile: Window pointer is null.");
            size = 0;
            return nint.Zero;
        }
        nint profile = SDL_GetWindowICCProfile(window, out size);
        if (profile == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowICCProfile: Failed to retrieve window ICC profile.");
        }
        return profile;
    }

    public static uint GetWindowId(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowId: Window handle is null.");
            return 0;
        }

        uint windowId = SDL_GetWindowID(window);
        if (windowId == 0) {
            Logger.LogWarn(LogCategory.System, "GetWindowId: Failed to retrieve window ID.");
        }

        return windowId;
    }

    public static bool GetWindowKeyboardGrab(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowKeyboardGrab: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_GetWindowKeyboardGrab(window);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetWindowKeyboardGrab: Failed to retrieve window keyboard grab.");
        }
        return result;
    }

    public static bool GetWindowMaximumSize(nint window, out int w, out int h) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowMaximumSize: Window pointer is null.");
            w = h = 0;
            return false;
        }
        SdlBool result = SDL_GetWindowMaximumSize(window, out w, out h);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetWindowMaximumSize: Failed to retrieve window maximum size.");
        }
        return result;
    }

    public static Rect GetWindowMaximumSize(nint window) {
        if (window == nint.Zero) {
            return default;
        }

        SdlBool result = SDL_GetWindowMaximumSize(window, out int w, out int h);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetWindowMaximumSize: Failed to retrieve window maximum size.");
            return default;
        }
        return new Rect() { W = w, H = h };
    }

    public static bool GetWindowMinimumSize(nint window, out int w, out int h) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowMinimumSize: Window pointer is null.");
            w = h = 0;
            return false;
        }
        SdlBool result = SDL_GetWindowMinimumSize(window, out w, out h);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetWindowMinimumSize: Failed to retrieve window minimum size.");
        }
        return result;
    }

    public static Rect GetWindowMinumSize(nint window) {
        if (window == nint.Zero) {
            return default;
        }
        SdlBool result = SDL_GetWindowMinimumSize(window, out int w, out int h);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetWindowMinimumSize: Failed to retrieve window minimum size.");
            return default;
        }
        return new Rect() { W = w, H = h };
    }

    public static bool GetWindowMouseGrab(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowMouseGrab: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_GetWindowMouseGrab(window);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetWindowMouseGrab: Failed to retrieve window mouse grab.");
        }
        return result;
    }

    public static Rect* GetWindowMouseRect(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowMouseRect: Window pointer is null.");
            return null;
        }
        Rect* rect = SDL_GetWindowMouseRect(window);
        if (rect == null) {
            Logger.LogError(LogCategory.System, "GetWindowMouseRect: Failed to retrieve window mouse rect.");
        }
        return rect;
    }

    public static float GetWindowOpacity(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowOpacity: Window pointer is null.");
            return 0;
        }
        float opacity = SDL_GetWindowOpacity(window);
        if (opacity < 0) {
            Logger.LogError(LogCategory.System, "GetWindowOpacity: Failed to retrieve window opacity.");
        }
        return opacity;
    }

    public static nint GetWindowParent(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowParent: Window handle is null.");
            return nint.Zero;
        }
        nint parentHandle = SDL_GetWindowParent(window);
        if (parentHandle == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "GetWindowParent: Failed to retrieve parent window handle.");
        }
        return parentHandle;
    }

    public static float GetWindowPixelDensity(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowPixelDensity: Window pointer is null.");
            return 0;
        }
        float pixelDensity = SDL_GetWindowPixelDensity(window);
        if (pixelDensity < 0) {
            Logger.LogError(LogCategory.System, "GetWindowPixelDensity: Failed to retrieve window pixel density.");
        }
        return pixelDensity;
    }

    public static PixelFormat GetWindowPixelFormat(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowPixelFormat: Window pointer is null.");
            return PixelFormat.Unknown;
        }
        PixelFormat pixelFormat = SDL_GetWindowPixelFormat(window);
        if (pixelFormat == PixelFormat.Unknown) {
            Logger.LogError(LogCategory.System, "GetWindowPixelFormat: Failed to retrieve window pixel format.");
        }
        return pixelFormat;
    }

    public static bool GetWindowPosition(nint window, out int x, out int y) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowPosition: Window pointer is null.");
            x = y = 0;
            return false;
        }
        SdlBool result = SDL_GetWindowPosition(window, out x, out y);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetWindowPosition: Failed to retrieve window position.");
        }
        return result;
    }

    public static Point GetWindowPosition(nint window) {
        if (window == nint.Zero) {
            return default;
        }
        SdlBool result = SDL_GetWindowPosition(window, out int x, out int y);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetWindowPosition: Failed to retrieve window position.");
            return default;
        }
        return new Point() { X = x, Y = y };
    }

    public static uint GetWindowProperties(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowProperties: Window handle is null.");
            return 0;
        }
        uint properties = SDL_GetWindowProperties(window);
        if (properties == 0) {
            Logger.LogWarn(LogCategory.System, "GetWindowProperties: Failed to retrieve window properties.");
        }
        return properties;
    }

    public static Span<nint> GetWindows(out int count) {
        nint result = SDL_GetWindows(out count);
        return new Span<nint>((void*)result, count);
    }

    public static Span<nint> GetWindows() {
        nint result = SDL_GetWindows(out int count);
        return new Span<nint>((void*)result, count);
    }

    public static bool GetWindowSafeArea(nint window, out Rect rect) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowSafeArea: Window pointer is null.");
            rect = default;
            return false;
        }
        SdlBool result = SDL_GetWindowSafeArea(window, out rect);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetWindowSafeArea: Failed to retrieve window safe area.");
        }
        return result;
    }

    public static Rect GetWindowSafeArea(nint window) {
        if (window == nint.Zero) {
            return default;
        }
        SdlBool result = SDL_GetWindowSafeArea(window, out Rect rect);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetWindowSafeArea: Failed to retrieve window safe area.");
            return default;
        }
        return rect;
    }

    public static bool GetWindowSize(nint window, out int w, out int h) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowSize: Window pointer is null.");
            w = h = 0;
            return false;
        }
        SdlBool result = SDL_GetWindowSize(window, out w, out h);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetWindowSize: Failed to retrieve window size.");
        }
        return result;
    }

    public static Rect GetWindowSize(nint window) {
        if (window == nint.Zero) {
            return default;
        }
        SdlBool result = SDL_GetWindowSize(window, out int w, out int h);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetWindowSize: Failed to retrieve window size.");
            return default;
        }
        return new Rect() { W = w, H = h };
    }

    public static bool GetWindowSizeInPixels(nint window, out int w, out int h) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowSizeInPixels: Window pointer is null.");
            w = h = 0;
            return false;
        }
        SdlBool result = SDL_GetWindowSizeInPixels(window, out w, out h);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetWindowSizeInPixels: Failed to retrieve window size in pixels.");
        }
        return result;
    }

    public static Rect GetWindowSizeInPixels(nint window) {
        if (window == nint.Zero) {
            return default;
        }
        SdlBool result = SDL_GetWindowSizeInPixels(window, out int w, out int h);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetWindowSizeInPixels: Failed to retrieve window size in pixels.");
            return default;
        }
        return new Rect() { W = w, H = h };
    }

    public static Surface* GetWindowSurfaace(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowSurface: Window pointer is null.");
            return null;
        }
        Surface* surface = SDL_GetWindowSurface(window);
        if (surface == null) {
            Logger.LogError(LogCategory.System, "GetWindowSurface: Failed to retrieve window surface.");
        }
        return surface;
    }

    public static bool GetWindowSurfaceVSync(nint window, out int vsync) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowSurfaceVSync: Window pointer is null.");
            vsync = 0;
            return false;
        }
        SdlBool result = SDL_GetWindowSurfaceVSync(window, out vsync);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetWindowSurfaceVSync: Failed to retrieve window surface VSync.");
        }
        return result;
    }

    public static int GetWindowSurfaceVSync(nint window) {
        if (window == nint.Zero) {
            return 0;
        }
        SdlBool result = SDL_GetWindowSurfaceVSync(window, out int vsync);
        if (!result) {
            Logger.LogError(LogCategory.System, "GetWindowSurfaceVSync: Failed to retrieve window surface VSync.");
            return 0;
        }
        return vsync;
    }

    public static string GetWindowTitle(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "GetWindowTitle: Window handle is null.");
            return string.Empty;
        }
        string title = SDL_GetWindowTitle(window);
        if (string.IsNullOrEmpty(title)) {
            Logger.LogWarn(LogCategory.System, "GetWindowTitle: Failed to retrieve window title.");
        }
        return title;
    }

    public static void GuidToString(SdlGuid guid, string pszGuid, int cbGuid) {
        if (guid.Data is null) {
            Logger.LogError(LogCategory.System, "GuidToString: GUID is null.");
            return;
        }
        SDL_GUIDToString(guid, pszGuid, cbGuid);
    }

    public static bool HasClipboardData(string mimeType) {
        if (string.IsNullOrEmpty(mimeType)) {
            Logger.LogError(LogCategory.System, "HasClipboardData: MIME type is null or empty.");
            return false;
        }
        SdlBool result = SDL_HasClipboardData(mimeType);
        if (!result) {
            Logger.LogError(LogCategory.System, "HasClipboardData: Failed to check clipboard data.");
        }
        return result;
    }

    public static bool HasClipboardText() {
        SdlBool result = SDL_HasClipboardText();
        if (!result) {
            Logger.LogError(LogCategory.System, "HasClipboardText: Failed to check clipboard text.");
        }
        return result;
    }

    public static bool HasKeyboard() {
        SdlBool result = SDL_HasKeyboard();
        if (!result) {
            Logger.LogError(LogCategory.System, "HasKeyboard: Failed to check keyboard.");
        }
        return result;
    }

    public static bool HasPrimarySelectionText() {
        SdlBool result = SDL_HasPrimarySelectionText();
        if (!result) {
            Logger.LogError(LogCategory.System, "HasPrimarySelectionText: Failed to check primary selection text.");
        }
        return result;
    }

    public static bool HasProperty(uint props, string name) {
        if (string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.System, "HasProperty: Property name is null or empty.");
            return false;
        }
        SdlBool result = SDL_HasProperty(props, name);
        if (!result) {
            Logger.LogError(LogCategory.System, "HasProperty: Failed to check property.");
        }
        return result;
    }

    public static bool HasRectIntersection(ref Rect a, ref Rect b) {
        SdlBool result = SDL_HasRectIntersection(ref a, ref b);
        if (!result) {
            Logger.LogError(LogCategory.System, "HasRectIntersection: Failed to check rectangle intersection.");
        }
        return result;
    }

    public static bool HasRectIntersectionFloat(ref FRect a, ref FRect b) {
        SdlBool result = SDL_HasRectIntersectionFloat(ref a, ref b);
        if (!result) {
            Logger.LogError(LogCategory.System, "HasRectIntersectionFloat: Failed to check rectangle intersection.");
        }
        return result;
    }

    public static bool HasScreenKeyboardSupport() {
        SdlBool result = SDL_HasScreenKeyboardSupport();
        if (!result) {
            Logger.LogError(LogCategory.System, "HasScreenKeyboardSupport: Failed to check screen keyboard support.");
        }
        return result;
    }

    public static bool HideWindow(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "HideWindow: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_HideWindow(window);
        if (!result) {
            Logger.LogError(LogCategory.System, "HideWindow: Failed to hide window.");
        }
        return result;
    }

    public static bool Init(InitFlags flags) {
        if (!Enum.IsDefined(flags)) {
            Logger.LogError(LogCategory.System, "Init: Invalid initialization flags.");
            return false;
        }

        SdlBool result = SDL_Init(flags);
        if (!result) {
            Logger.LogError(LogCategory.System, "Init: Failed to initialize SDL.");
        }
        return result;
    }

    public static bool InitSubSystem(InitFlags flags) {
        if (!Enum.IsDefined(flags)) {
            Logger.LogError(LogCategory.System, "InitSubSystem: Invalid initialization flags.");
            return false;
        }
        SdlBool result = SDL_InitSubSystem(flags);
        if (!result) {
            Logger.LogError(LogCategory.System, "InitSubSystem: Failed to initialize SDL subsystem.");
        }
        return result;
    }

    public static bool IsMainThread() {
        SdlBool result = SDL_IsMainThread();
        if (!result) {
            Logger.LogError(LogCategory.System, "IsMainThread: Failed to check if current thread is main thread.");
        }
        return result;
    }

    public static Surface* LoadBmp(string file) {
        if (string.IsNullOrEmpty(file)) {
            Logger.LogError(LogCategory.System, "LoadBmp: File path is null or empty.");
            return null;
        }
        Surface* surface = SDL_LoadBMP(file);
        if (surface == null) {
            Logger.LogError(LogCategory.System, "LoadBmp: Failed to load BMP file.");
        }
        return surface;
    }

    public static Surface* LoadBmpIo(nint src, bool closeIo) {
        if (src == nint.Zero) {
            Logger.LogError(LogCategory.System, "LoadBmpIo: Source pointer is null.");
            return null;
        }
        Surface* surface = SDL_LoadBMP_IO(src, closeIo);
        if (surface == null) {
            Logger.LogError(LogCategory.System, "LoadBmpIo: Failed to load BMP from IO source.");
        }
        return surface;
    }

    public static nint LoadFunction(nint handle, string name) {
        if (handle == nint.Zero) {
            Logger.LogError(LogCategory.System, "LoadFunction: Handle pointer is null.");
            return nint.Zero;
        }
        if (string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.System, "LoadFunction: Function name is null or empty.");
            return nint.Zero;
        }
        nint function = SDL_LoadFunction(handle, name);
        if (function == nint.Zero) {
            Logger.LogError(LogCategory.System, "LoadFunction: Failed to load function.");
        }
        return function;
    }

    public static nint LoadObject(string sofile) {
        if (string.IsNullOrEmpty(sofile)) {
            Logger.LogError(LogCategory.System, "LoadObject: Shared object file path is null or empty.");
            return nint.Zero;
        }
        nint handle = SDL_LoadObject(sofile);
        if (handle == nint.Zero) {
            Logger.LogError(LogCategory.System, "LoadObject: Failed to load shared object.");
        }
        return handle;
    }

    public static bool LockProperties(uint props) {
        if (props == 0) {
            Logger.LogError(LogCategory.System, "LockProperties: Properties are zero.");
            return false;
        }
        SdlBool result = SDL_LockProperties(props);
        if (!result) {
            Logger.LogError(LogCategory.System, "LockProperties: Failed to lock properties.");
        }
        return result;
    }

    public static bool LockSurface(nint surface) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "LockSurface: Surface pointer is null.");
            return false;
        }
        SdlBool result = SDL_LockSurface(surface);
        if (!result) {
            Logger.LogError(LogCategory.System, "LockSurface: Failed to lock surface.");
        }
        return result;
    }

    public static nint Malloc(nuint size) {
        if (size == nuint.Zero) {
            Logger.LogWarn(LogCategory.System, "Malloc: Size is zero.");
            return nint.Zero;
        }

        nint res = SDL_malloc(size);
        if (res == nint.Zero) {
            Logger.LogError(LogCategory.System, "Malloc: Memory allocation failed.");
            SDL_OutOfMemory();
            return nint.Zero;
        }

        return res;
    }

    public static uint MapRgb(nint format, nint palette, byte r, byte g, byte b) {
        if (format == nint.Zero || palette == nint.Zero) {
            Logger.LogError(LogCategory.System, "MapRgb: Format or palette pointer is null.");
            return 0;
        }
        uint color = SDL_MapRGB(format, palette, r, g, b);
        if (color == 0) {
            Logger.LogError(LogCategory.System, "MapRgb: Failed to map RGB color.");
        }
        return color;
    }

    public static uint MapRgba(nint format, nint palette, byte r, byte g, byte b, byte a) {
        if (format == nint.Zero || palette == nint.Zero) {
            Logger.LogError(LogCategory.System, "MapRgba: Format or palette pointer is null.");
            return 0;
        }
        uint color = SDL_MapRGBA(format, palette, r, g, b, a);
        if (color == 0) {
            Logger.LogError(LogCategory.System, "MapRgba: Failed to map RGBA color.");
        }
        return color;
    }

    public static uint MapSurfaceRgb(nint surface, byte r, byte g, byte b) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "MapSurfaceRgb: Surface pointer is null.");
            return 0;
        }
        uint color = SDL_MapSurfaceRGB(surface, r, g, b);
        if (color == 0) {
            Logger.LogError(LogCategory.System, "MapSurfaceRgb: Failed to map surface RGB color.");
        }
        return color;
    }

    public static uint MapSurfaceRgb(nint surface, Color color) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "MapSurfaceRgb: Surface pointer is null.");
            return 0;
        }

        uint colorValue = SDL_MapSurfaceRGB(surface, color.R, color.G, color.B);
        if (colorValue == 0) {
            Logger.LogError(LogCategory.System, "MapSurfaceRgb: Failed to map surface RGB color.");
        }
        return colorValue;
    }

    public static uint MapSurfaceRgba(nint surface, byte r, byte g, byte b, byte a) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "MapSurfaceRgba: Surface pointer is null.");
            return 0;
        }
        uint color = SDL_MapSurfaceRGBA(surface, r, g, b, a);
        if (color == 0) {
            Logger.LogError(LogCategory.System, "MapSurfaceRgba: Failed to map surface RGBA color.");
        }
        return color;
    }

    public static uint MapSurfaceRgba(nint surface, Color color) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "MapSurfaceRgba: Surface pointer is null.");
            return 0;
        }
        uint colorValue = SDL_MapSurfaceRGBA(surface, color.R, color.G, color.B, color.A);
        if (colorValue == 0) {
            Logger.LogError(LogCategory.System, "MapSurfaceRgba: Failed to map surface RGBA color.");
        }
        return colorValue;
    }

    public static bool MaximizeWindow(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "MaximizeWindow: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_MaximizeWindow(window);
        if (!result) {
            Logger.LogError(LogCategory.System, "MaximizeWindow: Failed to maximize window.");
        }
        return result;
    }

    public static bool MinimizeWindow(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "MinimizeWindow: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_MinimizeWindow(window);
        if (!result) {
            Logger.LogError(LogCategory.System, "MinimizeWindow: Failed to minimize window.");
        }
        return result;
    }

    public static SdlBool OutOfMemory() {
        return SDL_OutOfMemory();
    }

    public static bool PremultiplyAlpha(int width, int height, PixelFormat srcFormat, nint src,
            int srcPitch, PixelFormat dstFormat, nint dst, int dstPitch, bool linear) {
        if (width <= 0 || height <= 0) {
            Logger.LogError(LogCategory.System, "PremultiplyAlpha: Invalid width or height.");
            return false;
        }
        SdlBool result = SDL_PremultiplyAlpha(width, height, srcFormat, src, srcPitch, dstFormat, dst, dstPitch,
                linear);
        if (!result) {
            Logger.LogError(LogCategory.System, "PremultiplyAlpha: Failed to premultiply alpha.");
        }
        return result;
    }

    public static bool PremultiplyAlpha(Rect rect, PixelFormat srcFormat, nint src,
            int srcPitch, PixelFormat dstFormat, nint dst, int dstPitch, bool linear) {
        if (rect.W <= 0 || rect.H <= 0) {
            Logger.LogError(LogCategory.System, "PremultiplyAlpha: Invalid rectangle dimensions.");
            return false;
        }
        SdlBool result = SDL_PremultiplyAlpha(rect.W, rect.H, srcFormat, src, srcPitch, dstFormat, dst, dstPitch,
                linear);
        if (!result) {
            Logger.LogError(LogCategory.System, "PremultiplyAlpha: Failed to premultiply alpha.");
        }
        return result;
    }

    public static bool PremultiplySurfaceAlpha(nint surface, bool linear) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "PremultiplySurfaceAlpha: Surface pointer is null.");
            return false;
        }
        SdlBool result = SDL_PremultiplySurfaceAlpha(surface, linear);
        if (!result) {
            Logger.LogError(LogCategory.System, "PremultiplySurfaceAlpha: Failed to premultiply surface alpha.");
        }
        return result;
    }

    public static void Quit() {
        SDL_Quit();
    }

    public static void QuitSubSystem(InitFlags flags) {
        if (!Enum.IsDefined(flags)) {
            Logger.LogError(LogCategory.System, "QuitSubSystem: Invalid initialization flags.");
            return;
        }
        SDL_QuitSubSystem(flags);
    }

    public static bool RaiseWindow(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "RaiseWindow: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_RaiseWindow(window);
        if (!result) {
            Logger.LogError(LogCategory.System, "RaiseWindow: Failed to raise window.");
        }
        return result;
    }

    public static bool ReadSurfacePixel(nint surface, int x, int y, out byte r, out byte g, out byte b, out byte a) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "ReadSurfacePixel: Surface pointer is null.");
            r = g = b = a = 0;
            return false;
        }
        SdlBool result = SDL_ReadSurfacePixel(surface, x, y, out r, out g, out b, out a);
        if (!result) {
            Logger.LogError(LogCategory.System, "ReadSurfacePixel: Failed to read surface pixel.");
        }
        return result;
    }

    public static bool ReadSurfacePixel(nint surface, int x, int y, out Color color) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "ReadSurfacePixel: Surface pointer is null.");
            color = default;
            return false;
        }
        SdlBool result = SDL_ReadSurfacePixel(surface, x, y, out byte r, out byte g, out byte b, out byte a);
        if (!result) {
            Logger.LogError(LogCategory.System, "ReadSurfacePixel: Failed to read surface pixel.");
            color = default;
            return false;
        }
        color = new Color() { R = r, G = g, B = b, A = a };
        return true;
    }

    public static Color ReadSurfacePixel(nint surface, int x, int y) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "ReadSurfacePixel: Surface pointer is null.");
            return default;
        }
        SdlBool result = SDL_ReadSurfacePixel(surface, x, y, out byte r, out byte g, out byte b, out byte a);
        if (!result) {
            Logger.LogError(LogCategory.System, "ReadSurfacePixel: Failed to read surface pixel.");
            return default;
        }
        return new Color() { R = r, G = g, B = b, A = a };
    }

    public static bool ReadSurfacePixelFloat(nint surface, int x, int y, out float r, out float g, out float b, out float a) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "ReadSurfacePixelFloat: Surface pointer is null.");
            r = g = b = a = 0;
            return false;
        }
        SdlBool result = SDL_ReadSurfacePixelFloat(surface, x, y, out r, out g, out b, out a);
        if (!result) {
            Logger.LogError(LogCategory.System, "ReadSurfacePixelFloat: Failed to read surface pixel.");
        }
        return result;
    }

    public static bool ReadSurfacePixelFloat(nint surface, int x, int y, out FColor color) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "ReadSurfacePixelFloat: Surface pointer is null.");
            color = default;
            return false;
        }
        SdlBool result = SDL_ReadSurfacePixelFloat(surface, x, y, out float r, out float g, out float b, out float a);
        if (!result) {
            Logger.LogError(LogCategory.System, "ReadSurfacePixelFloat: Failed to read surface pixel.");
            color = default;
            return false;
        }
        color = new FColor() { R = r, G = g, B = b, A = a };
        return true;
    }

    public static FColor ReadSurfacePixelFloat(nint surface, int x, int y) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "ReadSurfacePixelFloat: Surface pointer is null.");
            return default;
        }
        SdlBool result = SDL_ReadSurfacePixelFloat(surface, x, y, out float r, out float g, out float b, out float a);
        if (!result) {
            Logger.LogError(LogCategory.System, "ReadSurfacePixelFloat: Failed to read surface pixel.");
            return default;
        }
        return new FColor() { R = r, G = g, B = b, A = a };
    }

    public static void RemoveHintCallback(string name, SdlHintCallback callback, nint userdata) {
        if (string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.System, "RemoveHintCallback: Hint name is null or empty.");
            return;
        }
        SDL_RemoveHintCallback(name, callback, userdata);
    }

    public static void RemoveSurfaceAlternateImages(nint surface) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "RemoveSurfaceAlternateImages: Surface pointer is null.");
            return;
        }
        SDL_RemoveSurfaceAlternateImages(surface);
    }

    public static bool ResetHint(string name) {
        if (string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.System, "ResetHint: Hint name is null or empty.");
            return false;
        }
        SdlBool result = SDL_ResetHint(name);
        if (!result) {
            Logger.LogError(LogCategory.System, "ResetHint: Failed to reset hint.");
        }
        return result;
    }

    public static void ResetHints() {
        SDL_ResetHints();
    }

    public static void ResetKeyboard() {
        SDL_ResetKeyboard();
    }

    public static bool RestoreWindow(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "RestoreWindow: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_RestoreWindow(window);
        if (!result) {
            Logger.LogError(LogCategory.System, "RestoreWindow: Failed to restore window.");
        }
        return result;
    }

    public static int RunApp(int argc, nint argv, SdlMainFunc mainFunction, nint reserved) {
        ArgumentNullException.ThrowIfNull(mainFunction);

        Logger.LogDebug(LogCategory.System, "Running SDL application with provided main function.");

        SetMainReady();

        int result = SDL_RunApp(argc, argv, mainFunction, reserved);

        Logger.LogDebug(LogCategory.System, $"SDL_RunApp completed with result: {result}");

        return result;
    }

    public static bool RunOnMainThread(SdlMainThreadCallback callback, nint userdata, bool waitComplete) {
        if (callback == null) {
            Logger.LogError(LogCategory.System, "RunOnMainThread: Callback is null.");
            return false;
        }
        SdlBool result = SDL_RunOnMainThread(callback, userdata, waitComplete);
        if (!result) {
            Logger.LogError(LogCategory.System, "RunOnMainThread: Failed to run on main thread.");
        }
        return result;
    }

    public static bool SaveBmp(nint surface, string file) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "SaveBmp: Surface pointer is null.");
            return false;
        }
        if (string.IsNullOrEmpty(file)) {
            Logger.LogError(LogCategory.System, "SaveBmp: File path is null or empty.");
            return false;
        }
        SdlBool result = SDL_SaveBMP(surface, file);
        if (!result) {
            Logger.LogError(LogCategory.System, "SaveBmp: Failed to save BMP file.");
        }
        return result;
    }

    public static bool SaveBmpIp(nint surface, nint dst, bool closeIo) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "SaveBmpIp: Surface pointer is null.");
            return false;
        }
        if (dst == nint.Zero) {
            Logger.LogError(LogCategory.System, "SaveBmpIp: Destination pointer is null.");
            return false;
        }
        SdlBool result = SDL_SaveBMP_IO(surface, dst, closeIo);
        if (!result) {
            Logger.LogError(LogCategory.System, "SaveBmpIp: Failed to save BMP to IO destination.");
        }
        return result;
    }

    public static Surface* ScaleSurface(nint surface, int width, int height, ScaleMode scaleMode) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "ScaleSurface: Surface pointer is null.");
            return null;
        }

        if (!Enum.IsDefined(scaleMode)) {
            Logger.LogError(LogCategory.System, "ScaleSurface: Invalid scale mode.");
            return null;
        }

        if (width <= 0 || height <= 0) {
            Logger.LogError(LogCategory.System, "ScaleSurface: Invalid width or height.");
            return null;
        }
        Surface* scaledSurface = SDL_ScaleSurface(surface, width, height, scaleMode);
        if (scaledSurface == null) {
            Logger.LogError(LogCategory.System, "ScaleSurface: Failed to scale surface.");
        }
        return scaledSurface;
    }

    public static bool ScreenKeyboardShown(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "ScreenKeyboardShown: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_ScreenKeyboardShown(window);
        if (!result) {
            Logger.LogError(LogCategory.System, "ScreenKeyboardShown: Failed to check screen keyboard visibility.");
        }
        return result;
    }

    public static bool ScreenSaverEnabled() {
        SdlBool result = SDL_ScreenSaverEnabled();
        if (!result) {
            Logger.LogError(LogCategory.System, "ScreenSaverEnabled: Failed to check screen saver status.");
        }
        return result;
    }

    public static bool SetAppMetadata(string appname, string appversion, string appidentifier) {
        if (string.IsNullOrEmpty(appname) || string.IsNullOrEmpty(appversion) || string.IsNullOrEmpty(appidentifier)) {
            Logger.LogError(LogCategory.System, "SetAppMetadata: App metadata is null or empty.");
            return false;
        }
        SdlBool result = SDL_SetAppMetadata(appname, appversion, appidentifier);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetAppMetadata: Failed to set app metadata.");
        }
        return result;
    }

    public static bool SetAppMetadataProperty(string name, string value) {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value)) {
            Logger.LogError(LogCategory.System, "SetAppMetadataProperty: Name or value is null or empty.");
            return false;
        }
        SdlBool result = SDL_SetAppMetadataProperty(name, value);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetAppMetadataProperty: Failed to set app metadata property.");
        }
        return result;
    }

    public static bool SetBooleanProperty(uint props, string name, bool value) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.System, "SetBooleanProperty: Properties are zero or name is null/empty.");
            return false;
        }
        SdlBool result = SDL_SetBooleanProperty(props, name, value);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetBooleanProperty: Failed to set boolean property.");
        }
        return result;
    }

    public static bool SetClipboardData(SdlClipboardDataCallback callback,
            SdlClipboardCleanupCallback cleanup, nint userdata, nint mimeTypes, nuint numMimeTypes) {
        if (callback == null || cleanup == null || userdata == nint.Zero || mimeTypes == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetClipboardData: Invalid parameters.");
            return false;
        }
        SdlBool result = SDL_SetClipboardData(callback, cleanup, userdata, mimeTypes, numMimeTypes);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetClipboardData: Failed to set clipboard data.");
        }
        return result;
    }

    public static bool SetClipboardText(string text) {
        if (string.IsNullOrEmpty(text)) {
            Logger.LogError(LogCategory.System, "SetClipboardText: Text is null or empty.");
            return false;
        }
        SdlBool result = SDL_SetClipboardText(text);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetClipboardText: Failed to set clipboard text.");
        }
        return result;
    }

    public static bool SetCurrentThreadPriority(ThreadPriority priority) {
        if (!Enum.IsDefined(priority)) {
            Logger.LogError(LogCategory.System, "SetCurrentThreadPriority: Invalid thread priority.");
            return false;
        }
        SdlBool result = SDL_SetCurrentThreadPriority(priority);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetCurrentThreadPriority: Failed to set thread priority.");
        }
        return result;
    }

    public static SdlBool SetError(string fmt, params object[] args) {
        if (string.IsNullOrEmpty(fmt)) {
            Logger.LogWarn(LogCategory.System, "SetError: Format string is null or empty.");
            return false;
        }

        string formatted = args.Length > 0 ? string.Format(fmt, args) : fmt;
        return SDL_SetError(formatted);
    }

    public static bool SetFloatProperty(uint props, string name, float value) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.System, "SetFloatProperty: Properties are zero or name is null/empty.");
            return false;
        }
        SdlBool result = SDL_SetFloatProperty(props, name, value);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetFloatProperty: Failed to set float property.");
        }
        return result;
    }

    public static bool SetHint(string name, string value) {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value)) {
            Logger.LogError(LogCategory.System, "SetHint: Name or value is null or empty.");
            return false;
        }
        SdlBool result = SDL_SetHint(name, value);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetHint: Failed to set hint.");
        }
        return result;
    }

    public static bool SetHintWithPriority(string name, string value, HintPriority priority) {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value)) {
            Logger.LogError(LogCategory.System, "SetHintWithPriority: Name or value is null or empty.");
            return false;
        }
        SdlBool result = SDL_SetHintWithPriority(name, value, priority);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetHintWithPriority: Failed to set hint with priority.");
        }
        return result;
    }

    public static void SetMainReady() {
        SDL_SetMainReady();
    }

    public static void SetModState(KeyMod modstate) {
        if (!Enum.IsDefined(modstate)) {
            Logger.LogError(LogCategory.System, "SetModState: Invalid key modifier state.");
            return;
        }
        SDL_SetModState(modstate);
    }

    public static bool SetNumberProperty(uint props, string name, long value) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.System, "SetNumberProperty: Properties are zero or name is null/empty.");
            return false;
        }
        SdlBool result = SDL_SetNumberProperty(props, name, value);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetNumberProperty: Failed to set number property.");
        }
        return result;
    }

    public static bool SetPaletteColors(nint palette, Span<Color> colors, int firstcolor, int ncolors) {
        if (palette == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetPaletteColors: Palette pointer is null.");
            return false;
        }
        if (firstcolor < 0 || ncolors <= 0) {
            Logger.LogError(LogCategory.System, "SetPaletteColors: Invalid first color or number of colors.");
            return false;
        }
        SdlBool result = SDL_SetPaletteColors(palette, colors, firstcolor, ncolors);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetPaletteColors: Failed to set palette colors.");
        }
        return result;
    }

    public static bool SetPointerProperty(uint props, string name, nint value) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.System, "SetPointerProperty: Properties are zero or name is null/empty.");
            return false;
        }
        SdlBool result = SDL_SetPointerProperty(props, name, value);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetPointerProperty: Failed to set pointer property.");
        }
        return result;
    }

    public static bool SetPointerPropertyWithCleanup(uint props, string name, nint value,
        SdlCleanupPropertyCallback cleanup, nint userdata) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.System, "SetPointerPropertyWithCleanup: Properties are zero or name is null/empty.");
            return false;
        }
        SdlBool result = SDL_SetPointerPropertyWithCleanup(props, name, value, cleanup, userdata);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetPointerPropertyWithCleanup: Failed to set pointer property with cleanup.");
        }
        return result;
    }

    public static bool SetPrimarySelectionText(string text) {
        if (string.IsNullOrEmpty(text)) {
            Logger.LogError(LogCategory.System, "SetPrimarySelectionText: Text is null or empty.");
            return false;
        }
        SdlBool result = SDL_SetPrimarySelectionText(text);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetPrimarySelectionText: Failed to set primary selection text.");
        }
        return result;
    }

    public static bool SetScancodeName(ScanCode scanCode, string name) {
        if (string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.System, "SetScancodeName: Name is null or empty.");
            return false;
        }
        SdlBool result = SDL_SetScancodeName(scanCode, name);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetScancodeName: Failed to set scancode name.");
        }
        return result;
    }

    public static bool SetStringProperty(uint props, string name, string value) {
        if (props == 0 || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value)) {
            Logger.LogError(LogCategory.System, "SetStringProperty: Properties are zero or name/value is null/empty.");
            return false;
        }
        SdlBool result = SDL_SetStringProperty(props, name, value);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetStringProperty: Failed to set string property.");
        }
        return result;
    }

    public static bool SetSurfaceAlphaMod(nint surface, byte alpha) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetSurfaceAlphaMod: Surface pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetSurfaceAlphaMod(surface, alpha);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetSurfaceAlphaMod: Failed to set surface alpha mod.");
        }
        return result;
    }

    public static bool SetSurfaceBlendMode(nint surface, uint blendMode) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetSurfaceBlendMode: Surface pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetSurfaceBlendMode(surface, blendMode);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetSurfaceBlendMode: Failed to set surface blend mode.");
        }
        return result;
    }

    public static bool SetSurfaceClipRect(nint surface, ref Rect rect) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetSurfaceClipRect: Surface pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetSurfaceClipRect(surface, ref rect);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetSurfaceClipRect: Failed to set surface clip rect.");
        }
        return result;
    }

    public static bool SetSurfaceColorKey(nint surface, SdlBool enabled, uint key) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetSurfaceColorKey: Surface pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetSurfaceColorKey(surface, enabled, key);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetSurfaceColorKey: Failed to set surface color key.");
        }
        return result;
    }

    public static bool SetSurfaceColorMod(nint surface, byte r, byte g, byte b) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetSurfaceColorMod: Surface pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetSurfaceColorMod(surface, r, g, b);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetSurfaceColorMod: Failed to set surface color mod.");
        }
        return result;
    }

    public static bool SetSurfaceColorMod(nint surface, Color color) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetSurfaceColorMod: Surface pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetSurfaceColorMod(surface, color.R, color.G, color.B);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetSurfaceColorMod: Failed to set surface color mod.");
        }
        return result;
    }

    public static bool SetSurfaceColorspace(nint surface, Colorspace colorspace) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetSurfaceColorspace: Surface pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetSurfaceColorspace(surface, colorspace);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetSurfaceColorspace: Failed to set surface colorspace.");
        }
        return result;
    }

    public static bool SetSurfacePalette(nint surface, nint palette) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetSurfacePalette: Surface pointer is null.");
            return false;
        }
        if (palette == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetSurfacePalette: Palette pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetSurfacePalette(surface, palette);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetSurfacePalette: Failed to set surface palette.");
        }
        return result;
    }

    public static bool SetSurfaceRLE(nint surface, bool enabled) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetSurfaceRLE: Surface pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetSurfaceRLE(surface, enabled);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetSurfaceRLE: Failed to set surface RLE.");
        }
        return result;
    }

    public static bool SetTextInputArea(nint window, ref Rect rect, int cursor) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetTextInputArea: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetTextInputArea(window, ref rect, cursor);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetTextInputArea: Failed to set text input area.");
        }
        return result;
    }

    public static bool SetTls(nint id, nint value, SdlTlsDestructorCallback destructor) {
        if (id == nint.Zero || value == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetTls: ID or value is null.");
            return false;
        }
        SdlBool result = SDL_SetTLS(id, value, destructor);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetTls: Failed to set TLS.");
        }
        return result;
    }

    public static bool SetWindowAlwaysOnTop(nint window, bool onTop) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowAlwaysOnTop: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetWindowAlwaysOnTop(window, onTop);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetWindowAlwaysOnTop: Failed to set window always on top.");
        }
        return result;
    }

    public static bool SetWindowAspectRatio(nint window, float minAspect, float maxAspect) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowAspectRatio: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetWindowAspectRatio(window, minAspect, maxAspect);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetWindowAspectRatio: Failed to set window aspect ratio.");
        }
        return result;
    }

    public static bool SetWindowBordered(nint window, bool bordered) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowBordered: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetWindowBordered(window, bordered);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetWindowBordered: Failed to set window bordered.");
        }
        return result;
    }

    public static bool SetWindowFocusable(nint window, bool focusable) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowFocusable: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetWindowFocusable(window, focusable);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetWindowFocusable: Failed to set window focusable.");
        }
        return result;
    }

    public static bool SetWindowFullscreen(nint window, bool fullscreen) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowFullscreen: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetWindowFullscreen(window, fullscreen);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetWindowFullscreen: Failed to set window fullscreen.");
        }
        return result;
    }

    public static bool SetWindowFullscreenMode(nint window, ref DisplayMode mode) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowFullscreenMode: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetWindowFullscreenMode(window, ref mode);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetWindowFullscreenMode: Failed to set window fullscreen mode.");
        }
        return result;
    }

    public static bool SetWindowHitTest(nint window, SdlHitTest callback, nint callbackData) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowHitTest: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetWindowHitTest(window, callback, callbackData);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetWindowHitTest: Failed to set window hit test.");
        }
        return result;
    }

    public static bool SetWindowIcon(nint window, nint icon) {
        // Impement an overloaded function that acceps an Icon from LoadIcon
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowIcon: Window pointer is null.");
            return false;
        }
        if (icon == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowIcon: Icon pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetWindowIcon(window, icon);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetWindowIcon: Failed to set window icon.");
        }
        return result;
    }

    public static bool SetWindowKeyboardGrab(nint window, bool grabbed) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowKeyboardGrab: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetWindowKeyboardGrab(window, grabbed);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetWindowKeyboardGrab: Failed to set window keyboard grab.");
        }
        return result;
    }

    public static bool SetWindowMaximumSize(nint window, int maxW, int maxH) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowMaximumSize: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetWindowMaximumSize(window, maxW, maxH);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetWindowMaximumSize: Failed to set window maximum size.");
        }
        return result;
    }

    public static bool SetWindowMinimumSize(nint window, int minW, int minH) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowMinimumSize: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetWindowMinimumSize(window, minW, minH);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetWindowMinimumSize: Failed to set window minimum size.");
        }
        return result;
    }

    public static bool SetWindowModal(nint window, bool modal) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowModal: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetWindowModal(window, modal);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetWindowModal: Failed to set window modal.");
        }
        return result;
    }

    public static bool SetWindowMouseGrab(nint window, bool grabbed) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowMouseGrab: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetWindowMouseGrab(window, grabbed);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetWindowMouseGrab: Failed to set window mouse grab.");
        }
        return result;
    }

    public static bool SetWindowMouseRect(nint window, ref Rect rect) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowMouseRect: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetWindowMouseRect(window, ref rect);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetWindowMouseRect: Failed to set window mouse rect.");
        }
        return result;
    }

    public static bool SetWindowOpacity(nint window, float opacity) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowOpacity: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetWindowOpacity(window, opacity);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetWindowOpacity: Failed to set window opacity.");
        }
        return result;
    }

    public static bool SetWindowParent(nint window, nint parent) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowParent: Window pointer is null.");
            return false;
        }
        if (parent == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowParent: Parent pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetWindowParent(window, parent);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetWindowParent: Failed to set window parent.");
        }
        return result;
    }

    public static bool SetWindowPosition(nint window, int x, int y) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowPosition: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetWindowPosition(window, x, y);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetWindowPosition: Failed to set window position.");
        }
        return result;
    }

    public static bool SetWindowPosition(nint window, Point position) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowPosition: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetWindowPosition(window, position.X, position.Y);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetWindowPosition: Failed to set window position.");
        }
        return result;
    }

    public static bool SetWindowResizable(nint window, bool resizable) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowResizable: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetWindowResizable(window, resizable);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetWindowResizable: Failed to set window resizable.");
        }
        return result;
    }

    public static bool SetWindowShape(nint window, nint shape) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowShape: Window pointer is null.");
            return false;
        }
        if (shape == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowShape: Shape pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetWindowShape(window, shape);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetWindowShape: Failed to set window shape.");
        }
        return result;
    }

    public static bool SetWindowSize(nint window, int w, int h) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowSize: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetWindowSize(window, w, h);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetWindowSize: Failed to set window size.");
        }
        return result;
    }

    public static bool SetWindowSize(nint window, Rect rect) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowSize: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetWindowSize(window, rect.W, rect.H);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetWindowSize: Failed to set window size.");
        }
        return result;
    }

    public static bool SetWindowSurfaceVSync(nint window, int vsync) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowSurfaceVSync: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_SetWindowSurfaceVSync(window, vsync);
        if (!result) {
            Logger.LogError(LogCategory.System, "SetWindowSurfaceVSync: Failed to set window surface VSync.");
        }
        return result;
    }

    public static SdlBool SetWindowTitle(nint window, string title) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SetWindowTitle: Window handle is null.");
            return false;
        }
        SdlBool result = SDL_SetWindowTitle(window, title);
        if (!result) {
            Logger.LogWarn(LogCategory.System, "SetWindowTitle: Failed to set window title.");
        }
        return result;
    }

    public static bool ShowWindow(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "ShowWindow: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_ShowWindow(window);
        if (!result) {
            Logger.LogError(LogCategory.System, "ShowWindow: Failed to show window.");
        }
        return result;
    }

    public static bool ShowWindowSystemMenu(nint window, int x, int y) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "ShowWindowSystemMenu: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_ShowWindowSystemMenu(window, x, y);
        if (!result) {
            Logger.LogError(LogCategory.System, "ShowWindowSystemMenu: Failed to show window system menu.");
        }
        return result;
    }

    public static bool ShowWindowSystemMenu(nint window, Point position) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "ShowWindowSystemMenu: Window pointer is null.");
            return false;
        }
        return ShowWindowSystemMenu(window, position.X, position.Y);
    }

    public static bool StartTextInput(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "StartTextInput: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_StartTextInput(window);
        if (!result) {
            Logger.LogError(LogCategory.System, "StartTextInput: Failed to start text input.");
        }
        return result;
    }

    public static bool StartTextInputWithProperties(nint window, uint props) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "StartTextInputWithProperties: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_StartTextInputWithProperties(window, props);
        if (!result) {
            Logger.LogError(LogCategory.System, "StartTextInputWithProperties: Failed to start text input with properties.");
        }
        return result;
    }

    public static bool StopTextInput(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "StopTextInput: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_StopTextInput(window);
        if (!result) {
            Logger.LogError(LogCategory.System, "StopTextInput: Failed to stop text input.");
        }
        return result;
    }

    public static SdlGuid StringToGUID(string pchGuid) {
        if (string.IsNullOrEmpty(pchGuid)) {
            Logger.LogError(LogCategory.System, "StringToGUID: GUID string is null or empty.");
            return default;
        }
        SdlGuid result = SDL_StringToGUID(pchGuid);
        if (result.Data == null) {
            Logger.LogError(LogCategory.System, "StringToGUID: Failed to convert string to GUID.");
        }
        return result;
    }

    public static bool SurfaceHasAlternateImages(nint surface) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "SurfaceHasAlternateImages: Surface pointer is null.");
            return false;
        }
        SdlBool result = SDL_SurfaceHasAlternateImages(surface);
        if (!result) {
            Logger.LogError(LogCategory.System, "SurfaceHasAlternateImages: Failed to check surface alternate images.");
        }
        return result;
    }

    public static bool SurfaceHasColorKey(nint surface) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "SurfaceHasColorKey: Surface pointer is null.");
            return false;
        }
        SdlBool result = SDL_SurfaceHasColorKey(surface);
        if (!result) {
            Logger.LogError(LogCategory.System, "SurfaceHasColorKey: Failed to check surface color key.");
        }
        return result;
    }

    public static bool SurfaceHasRLE(nint surface) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "SurfaceHasRLE: Surface pointer is null.");
            return false;
        }
        SdlBool result = SDL_SurfaceHasRLE(surface);
        if (!result) {
            Logger.LogError(LogCategory.System, "SurfaceHasRLE: Failed to check surface RLE.");
        }
        return result;
    }

    public static bool SyncWindow(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "SyncWindow: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_SyncWindow(window);
        if (!result) {
            Logger.LogError(LogCategory.System, "SyncWindow: Failed to sync window.");
        }
        return result;
    }

    public static bool TextInputActive(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "TextInputActive: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_TextInputActive(window);
        if (!result) {
            Logger.LogError(LogCategory.System, "TextInputActive: Failed to check text input active.");
        }
        return result;
    }

    public static void UnloadObject(nint handle) {
        if (handle == nint.Zero) {
            Logger.LogError(LogCategory.System, "UnloadObject: Handle pointer is null.");
            return;
        }
        SDL_UnloadObject(handle);
    }

    public static void UnlockProperties(uint props) {
        if (props == 0) {
            Logger.LogError(LogCategory.System, "UnlockProperties: Properties are zero.");
            return;
        }
        SDL_UnlockProperties(props);
    }

    public static void UnlockSurface(nint surface) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "UnlockSurface: Surface pointer is null.");
            return;
        }
        SDL_UnlockSurface(surface);
    }

    public static bool UpdateWindowSurface(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "UpdateWindowSurface: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_UpdateWindowSurface(window);
        if (!result) {
            Logger.LogError(LogCategory.System, "UpdateWindowSurface: Failed to update window surface.");
        }
        return result;
    }

    public static bool UpdateWindowSurfaceRects(nint window, Span<Rect> rects, int numrects) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "UpdateWindowSurfaceRects: Window pointer is null.");
            return false;
        }
        if (rects.Length == 0 || numrects <= 0) {
            Logger.LogError(LogCategory.System, "UpdateWindowSurfaceRects: Rectangles are empty or number of rectangles is zero.");
            return false;
        }
        SdlBool result = SDL_UpdateWindowSurfaceRects(window, rects, numrects);
        if (!result) {
            Logger.LogError(LogCategory.System, "UpdateWindowSurfaceRects: Failed to update window surface rectangles.");
        }
        return result;
    }

    public static bool UpdateWindowSurfaceRects(nint window, Rect[] rects) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "UpdateWindowSurfaceRects: Window pointer is null.");
            return false;
        }
        if (rects.Length == 0) {
            Logger.LogError(LogCategory.System, "UpdateWindowSurfaceRects: Rectangles are empty.");
            return false;
        }
        SdlBool result = SDL_UpdateWindowSurfaceRects(window, rects, rects.Length);
        if (!result) {
            Logger.LogError(LogCategory.System, "UpdateWindowSurfaceRects: Failed to update window surface rectangles.");
        }
        return result;
    }

    public static void WaitThread(nint thread, nint status) {
        if (thread == nint.Zero) {
            Logger.LogError(LogCategory.System, "WaitThread: Thread pointer is null.");
            return;
        }
        SDL_WaitThread(thread, status);
    }

    public static InitFlags WasInit(InitFlags flags) {
        if (!Enum.IsDefined(flags)) {
            Logger.LogError(LogCategory.System, "WasInit: Flags are not defined.");
            return 0;
        }
        if (flags == 0) {
            Logger.LogError(LogCategory.System, "WasInit: Flags are zero.");
            return 0;
        }
        InitFlags result = SDL_WasInit(flags);
        if (result == 0) {
            Logger.LogError(LogCategory.System, "WasInit: Failed to check SDL initialization.");
        }
        return result;
    }

    public static bool WindowHasSurface(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "WindowHasSurface: Window pointer is null.");
            return false;
        }
        SdlBool result = SDL_WindowHasSurface(window);
        if (!result) {
            Logger.LogError(LogCategory.System, "WindowHasSurface: Failed to check window surface.");
        }
        return result;
    }

    public static bool WriteSurfacePixel(nint surface, int x, int y, byte r, byte g, byte b, byte a) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "WriteSurfacePixel: Surface pointer is null.");
            return false;
        }
        SdlBool result = SDL_WriteSurfacePixel(surface, x, y, r, g, b, a);
        if (!result) {
            Logger.LogError(LogCategory.System, "WriteSurfacePixel: Failed to write surface pixel.");
        }
        return result;
    }

    public static bool WriteSurfacePixel(nint surface, int x, int y, Color color) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "WriteSurfacePixel: Surface pointer is null.");
            return false;
        }
        SdlBool result = SDL_WriteSurfacePixel(surface, x, y, color.R, color.G, color.B, color.A);
        if (!result) {
            Logger.LogError(LogCategory.System, "WriteSurfacePixel: Failed to write surface pixel.");
        }
        return result;
    }

    public static bool WriteSurfacePixel(nint surface, Point location, Color color) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "WriteSurfacePixel: Surface pointer is null.");
            return false;
        }
        return WriteSurfacePixel(surface, location.X, location.Y, color.R, color.G, color.B, color.A);
    }

    public static bool WriteSurfacePixel(nint surface, Point location, byte r, byte g, byte b, byte a) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "WriteSurfacePixel: Surface pointer is null.");
            return false;
        }
        return WriteSurfacePixel(surface, location.X, location.Y, r, g, b, a);
    }

    public static bool WriteSurfacePixelFloat(nint surface, int x, int y, float r, float g, float b, float a) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "WriteSurfacePixelFloat: Surface pointer is null.");
            return false;
        }
        SdlBool result = SDL_WriteSurfacePixelFloat(surface, x, y, r, g, b, a);
        if (!result) {
            Logger.LogError(LogCategory.System, "WriteSurfacePixelFloat: Failed to write surface pixel float.");
        }
        return result;
    }

    public static bool WriteSurfacePixelFloat(nint window, Point location, FColor color) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "WriteSurfacePixelFloat: Window pointer is null.");
            return false;
        }
        return WriteSurfacePixelFloat(window, location.X, location.Y, color.R, color.G, color.B,
            color.A);
    }

    public static bool WriteSurfacePixelFloat(nint window, Point location, float r, float g, float b, float a) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.System, "WriteSurfacePixelFloat: Window pointer is null.");
            return false;
        }
        return WriteSurfacePixelFloat(window, location.X, location.Y, r, g, b, a);
    }

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_AddHintCallback(string name, SdlHintCallback callback, nint userdata);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_AddSurfaceAlternateImage(nint surface, nint image);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_BlitSurface(nint src, nint srcrect, nint dst, nint dstrect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_BlitSurface9Grid(nint src, nint srcrect, int leftWidth, int rightWidth,
        int topHeight, int bottomHeight, float scale, ScaleMode scaleMode, nint dst,
        nint dstrect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_BlitSurfaceScaled(nint src, nint srcrect, nint dst, nint dstrect,
        ScaleMode scaleMode);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_BlitSurfaceTiled(nint src, nint srcrect, nint dst, nint dstrect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_BlitSurfaceTiledWithScale(nint src, nint srcrect, float scale,
        ScaleMode scaleMode, nint dst, nint dstrect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_BlitSurfaceUnchecked(nint src, nint srcrect, nint dst,
            nint dstrect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_BlitSurfaceUncheckedScaled(nint src, nint srcrect, nint dst, nint dstrect,
        ScaleMode scaleMode);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_CleanupTLS();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ClearClipboardData();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ClearComposition(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ClearError();

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ClearProperty(uint props, string name);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ClearSurface(nint surface, float r, float g, float b, float a);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_ComposeCustomBlendMode(BlendFactor srcColorFactor, BlendFactor dstColorFactor,
        BlendOperation colorOperation, BlendFactor srcAlphaFactor, BlendFactor dstAlphaFactor,
        BlendOperation alphaOperation);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ConvertPixels(int width, int height, PixelFormat srcFormat, nint src,
        int srcPitch, PixelFormat dstFormat, nint dst, int dstPitch);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ConvertPixelsAndColorspace(int width, int height, PixelFormat srcFormat,
        Colorspace srcColorspace, uint srcProperties, nint src, int srcPitch, PixelFormat dstFormat,
        Colorspace dstColorspace, uint dstProperties, nint dst, int dstPitch);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* SDL_ConvertSurface(nint surface, PixelFormat format);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* SDL_ConvertSurfaceAndColorspace(nint surface, PixelFormat format,
        nint palette, Colorspace colorspace, uint props);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CopyProperties(uint src, uint dst);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Palette* SDL_CreatePalette(int ncolors);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreatePopupWindow(nint parent, int offsetX, int offsetY, int w, int h,
        WindowFlags flags);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_CreateProperties();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* SDL_CreateSurface(int width, int height, PixelFormat format);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* SDL_CreateSurfaceFrom(int width, int height, PixelFormat format, nint pixels,
        int pitch);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Palette* SDL_CreateSurfacePalette(nint surface);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateThreadRuntime(SdlThreadFunction fn, string name, nint data,
        nint pfnBeginThread, nint pfnEndThread);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateThreadWithPropertiesRuntime(uint props, nint pfnBeginThread,
        nint pfnEndThread);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateWindow(string title, int w, int h, WindowFlags flags);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateWindowWithProperties(uint props);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyPalette(nint palette);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyProperties(uint props);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroySurface(nint surface);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyWindow(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_DestroyWindowSurface(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DetachThread(nint thread);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_DisableScreenSaver();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* SDL_DuplicateSurface(nint surface);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_EnableScreenSaver();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_EnterAppMainCallbacks(int argc, nint argv, SdlAppInitFunc AppInit,
        SdlAppIterateFunc AppIter, SdlAppEventFunc sdlAppEvent, SdlAppQuitFunc AppQuit);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_EnumerateProperties(uint props, SdlEnumeratePropertiesCallback callback,
        nint userdata);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool
        SDL_FillSurfaceRect(nint dst, nint rect, uint color);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_FillSurfaceRects(nint dst, Span<Rect> rects, int count, uint color);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_FlashWindow(nint window, FlashOperation operation);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_FlipSurface(nint surface, FlipMode flip);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_free(nint mem);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GDKSuspendComplete();

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetAppMetadataProperty(string name);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetBooleanProperty(uint props, string name, SdlBool defaultValue);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetClipboardData(string mimeType, out nuint size);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetClipboardMimeTypes(out nuint numMimeTypes);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CallerOwnedStringMarshaller))]
    private static partial string SDL_GetClipboardText();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetClosestFullscreenDisplayMode(uint displayId, int w, int h, float refreshRate,
        SdlBool includeHighDensityModes, out DisplayMode closest);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayMode* SDL_GetCurrentDisplayMode(uint displayId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayOrientation SDL_GetCurrentDisplayOrientation(uint displayId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong SDL_GetCurrentThreadID();

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetCurrentVideoDriver();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayMode* SDL_GetDesktopDisplayMode(uint displayId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetDisplayBounds(uint displayId, out Rect rect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetDisplayContentScale(uint displayId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetDisplayForPoint(ref Point point);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetDisplayForRect(ref Rect rect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetDisplayForWindow(nint window);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetDisplayName(uint displayId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetDisplayProperties(uint displayId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetDisplays(out int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetDisplayUsableBounds(uint displayId, out Rect rect);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetError();

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetFloatProperty(uint props, string name, float defaultValue);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetFullscreenDisplayModes(uint displayId, out int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetGlobalProperties();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetGrabbedWindow();

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetHint(string name);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetHintBoolean(string name, SdlBool defaultValue);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetKeyboardFocus();

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetKeyboardNameForID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetKeyboards(out int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetKeyboardState(out int numkeys);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetKeyFromName(string name);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetKeyFromScancode(ScanCode scanCode, KeyMod modstate, SdlBool keyEvent);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetKeyName(uint key);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetMasksForPixelFormat(PixelFormat format, out int bpp, out uint rmask,
        out uint gmask, out uint bmask, out uint amask);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial KeyMod SDL_GetModState();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayOrientation SDL_GetNaturalDisplayOrientation(uint displayId);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long SDL_GetNumberProperty(uint props, string name, long defaultValue);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumVideoDrivers();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PixelFormatDetails* SDL_GetPixelFormatDetails(PixelFormat format);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PixelFormat SDL_GetPixelFormatForMasks(int bpp, uint rmask, uint gmask, uint bmask,
        uint amask);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetPixelFormatName(PixelFormat format);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetPointerProperty(uint props, string name, nint defaultValue);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PowerState SDL_GetPowerInfo(out int seconds, out int percent);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetPreferredLocales(out int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetPrimaryDisplay();

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CallerOwnedStringMarshaller))]
    private static partial string SDL_GetPrimarySelectionText();

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PropertyType SDL_GetPropertyType(uint props, string name);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRectAndLineIntersection(ref Rect rect, ref int x1, ref int y1, ref int x2,
        ref int y2);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRectAndLineIntersectionFloat(ref FRect rect, ref float x1, ref float y1,
        ref float x2, ref float y2);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRectEnclosingPoints(Span<Point> points, int count, ref Rect clip,
        out Rect result);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRectEnclosingPointsFloat(Span<FPoint> points, int count, ref FRect clip,
        out FRect result);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRectIntersection(ref Rect a, ref Rect b, out Rect result);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRectIntersectionFloat(ref FRect a, ref FRect b, out FRect result);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRectUnion(ref Rect a, ref Rect b, out Rect result);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetRectUnionFloat(ref FRect a, ref FRect b, out FRect result);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GetRGB(uint pixel, nint format, nint palette, out byte r, out byte g,
        out byte b);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GetRGBA(uint pixel, nint format, nint palette, out byte r, out byte g,
        out byte b, out byte a);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ScanCode SDL_GetScancodeFromKey(uint key, nint modstate);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ScanCode SDL_GetScancodeFromName(string name);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetScancodeName(ScanCode scanCode);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetStringProperty(uint props, string name, string defaultValue);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetSurfaceAlphaMod(nint surface, out byte alpha);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetSurfaceBlendMode(nint surface, nint blendMode);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetSurfaceClipRect(nint surface, out Rect rect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetSurfaceColorKey(nint surface, out uint key);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetSurfaceColorMod(nint surface, out byte r, out byte g, out byte b);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Colorspace SDL_GetSurfaceColorspace(nint surface);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetSurfaceImages(nint surface, out int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Palette* SDL_GetSurfacePalette(nint surface);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetSurfaceProperties(nint surface);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SystemTheme SDL_GetSystemTheme();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetTextInputArea(nint window, out Rect rect, out int cursor);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong SDL_GetThreadID(nint thread);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetThreadName(nint thread);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ThreadState SDL_GetThreadState(nint thread);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetTLS(nint id);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetVideoDriver(int index);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetWindowAspectRatio(nint window, out float minAspect, out float maxAspect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetWindowBordersSize(nint window, out int top, out int left, out int bottom,
        out int right);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetWindowDisplayScale(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial WindowFlags SDL_GetWindowFlags(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetWindowFromID(uint id);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayMode* SDL_GetWindowFullscreenMode(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetWindowICCProfile(nint window, out nuint size);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetWindowID(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetWindowKeyboardGrab(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetWindowMaximumSize(nint window, out int w, out int h);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetWindowMinimumSize(nint window, out int w, out int h);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetWindowMouseGrab(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Rect* SDL_GetWindowMouseRect(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetWindowOpacity(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetWindowParent(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetWindowPixelDensity(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PixelFormat SDL_GetWindowPixelFormat(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetWindowPosition(nint window, out int x, out int y);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetWindowProperties(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetWindows(out int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetWindowSafeArea(nint window, out Rect rect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetWindowSize(nint window, out int w, out int h);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetWindowSizeInPixels(nint window, out int w, out int h);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* SDL_GetWindowSurface(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetWindowSurfaceVSync(nint window, out int vsync);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetWindowTitle(nint window);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GUIDToString(SdlGuid guid, string pszGuid, int cbGuid);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasClipboardData(string mimeType);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasClipboardText();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasKeyboard();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasPrimarySelectionText();

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasProperty(uint props, string name);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasRectIntersection(ref Rect a, ref Rect b);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasRectIntersectionFloat(ref FRect a, ref FRect b);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasScreenKeyboardSupport();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HideWindow(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_Init(InitFlags flags);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_InitSubSystem(InitFlags flags);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_IsMainThread();

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* SDL_LoadBMP(string file);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* SDL_LoadBMP_IO(nint src, SdlBool closeio);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_LoadFunction(nint handle, string name);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_LoadObject(string sofile);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_LockProperties(uint props);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_LockSurface(nint surface);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_malloc(nuint size);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_MapRGB(nint format, nint palette, byte r, byte g, byte b);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_MapRGBA(nint format, nint palette, byte r, byte g, byte b, byte a);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_MapSurfaceRGB(nint surface, byte r, byte g, byte b);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_MapSurfaceRGBA(nint surface, byte r, byte g, byte b, byte a);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_MaximizeWindow(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_MinimizeWindow(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_OutOfMemory();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_PremultiplyAlpha(int width, int height, PixelFormat srcFormat, nint src,
            int srcPitch, PixelFormat dstFormat, nint dst, int dstPitch, SdlBool linear);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_PremultiplySurfaceAlpha(nint surface, SdlBool linear);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_Quit();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_QuitSubSystem(InitFlags flags);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RaiseWindow(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadSurfacePixel(nint surface, int x, int y, out byte r, out byte g, out byte b,
        out byte a);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadSurfacePixelFloat(nint surface, int x, int y, out float r, out float g,
        out float b, out float a);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_RemoveHintCallback(string name, SdlHintCallback callback, nint userdata);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_RemoveSurfaceAlternateImages(nint surface);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ResetHint(string name);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ResetHints();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ResetKeyboard();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RestoreWindow(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_RunApp(int argc, nint argv, SdlMainFunc mainFunction, nint reserved);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RunOnMainThread(SdlMainThreadCallback callback, nint userdata,
            SdlBool waitComplete);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SaveBMP(nint surface, string file);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SaveBMP_IO(nint surface, nint dst, SdlBool closeio);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* SDL_ScaleSurface(nint surface, int width, int height, ScaleMode scaleMode);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ScreenKeyboardShown(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ScreenSaverEnabled();

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAppMetadata(string appname, string appversion, string appidentifier);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAppMetadataProperty(string name, string value);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetBooleanProperty(uint props, string name, SdlBool value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetClipboardData(SdlClipboardDataCallback callback,
            SdlClipboardCleanupCallback cleanup, nint userdata, nint mimeTypes, nuint numMimeTypes);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetClipboardText(string text);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetCurrentThreadPriority(ThreadPriority priority);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetError(string fmt);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetFloatProperty(uint props, string name, float value);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetHint(string name, string value);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetHintWithPriority(string name, string value, HintPriority priority);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetMainReady();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetModState(KeyMod modstate);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetNumberProperty(uint props, string name, long value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetPaletteColors(nint palette, Span<Color> colors, int firstcolor,
        int ncolors);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetPointerProperty(uint props, string name, nint value);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetPointerPropertyWithCleanup(uint props, string name, nint value,
        SdlCleanupPropertyCallback cleanup, nint userdata);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetPrimarySelectionText(string text);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetScancodeName(ScanCode scanCode, string name);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetStringProperty(uint props, string name, string value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetSurfaceAlphaMod(nint surface, byte alpha);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetSurfaceBlendMode(nint surface, uint blendMode);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetSurfaceClipRect(nint surface, ref Rect rect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetSurfaceColorKey(nint surface, SdlBool enabled, uint key);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetSurfaceColorMod(nint surface, byte r, byte g, byte b);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetSurfaceColorspace(nint surface, Colorspace colorspace);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetSurfacePalette(nint surface, nint palette);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetSurfaceRLE(nint surface, SdlBool enabled);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetTextInputArea(nint window, ref Rect rect, int cursor);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetTLS(nint id, nint value, SdlTlsDestructorCallback destructor);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowAlwaysOnTop(nint window, SdlBool onTop);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowAspectRatio(nint window, float minAspect, float maxAspect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowBordered(nint window, SdlBool bordered);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowFocusable(nint window, SdlBool focusable);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowFullscreen(nint window, SdlBool fullscreen);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowFullscreenMode(nint window, ref DisplayMode mode);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowHitTest(nint window, SdlHitTest callback, nint callbackData);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowIcon(nint window, nint icon);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowKeyboardGrab(nint window, SdlBool grabbed);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowMaximumSize(nint window, int maxW, int maxH);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowMinimumSize(nint window, int minW, int minH);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowModal(nint window, SdlBool modal);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowMouseGrab(nint window, SdlBool grabbed);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowMouseRect(nint window, ref Rect rect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowOpacity(nint window, float opacity);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowParent(nint window, nint parent);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowPosition(nint window, int x, int y);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowResizable(nint window, SdlBool resizable);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowShape(nint window, nint shape);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowSize(nint window, int w, int h);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowSurfaceVSync(nint window, int vsync);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowTitle(nint window, string title);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ShowWindow(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ShowWindowSystemMenu(nint window, int x, int y);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_StartTextInput(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_StartTextInputWithProperties(nint window, uint props);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_StopTextInput(nint window);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlGuid SDL_StringToGUID(string pchGuid);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SurfaceHasAlternateImages(nint surface);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SurfaceHasColorKey(nint surface);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SurfaceHasRLE(nint surface);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SyncWindow(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_TextInputActive(nint window);

    // /usr/local/include/SDL3/SDL_loadso.h
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnloadObject(nint handle);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnlockProperties(uint props);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnlockSurface(nint surface);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_UpdateWindowSurface(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_UpdateWindowSurfaceRects(nint window, Span<Rect> rects, int numrects);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_WaitThread(nint thread, nint status);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial InitFlags SDL_WasInit(InitFlags flags);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WindowHasSurface(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteSurfacePixel(nint surface, int x, int y, byte r, byte g, byte b, byte a);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteSurfacePixelFloat(nint surface, int x, int y, float r, float g, float b,
        float a);
}