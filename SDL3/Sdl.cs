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

    internal const UnmanagedType StringType = UnmanagedType.LPUTF8Str;
    internal const UnmanagedType BoolType = UnmanagedType.I1;
    internal const StringMarshalling marshalling = StringMarshalling.Utf8;

    public static unsafe nint StructureToPointer<T>(ref T str) where T : unmanaged {
        int size = sizeof(T);
        nint ptr = Marshal.AllocHGlobal(size);
        Unsafe.CopyBlockUnaligned((void*)ptr, Unsafe.AsPointer(ref str), (uint)size);
        return ptr;
    }

    public static unsafe T PointerToStructure<T>(nint ptr) where T : unmanaged {
        T str = default;
        Unsafe.Copy(ref str, (void*)ptr);
        return str;
    }
   

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

    public static bool AddHintCallback(string name, SdlHintCallback callback, nint userdata) {
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

    public static bool AddSurfaceAlternateImage(nint surface, nint image) {
        if (surface == nint.Zero || image == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "AddSurfaceAlternateImage: Surface or image pointer is null.");
            return false;
        }
        return SDL_AddSurfaceAlternateImage(surface, image);
    }

    public static bool BlitSurface(nint src, nint srcrect, nint dst, nint dstrect) {
        if (src == nint.Zero || dst == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "BlitSurface: Source or destination pointer is null.");
            return false;
        }
        return SDL_BlitSurface(src, srcrect, dst, dstrect);
    }

    public static bool BlitSurface9Grid(nint src, nint srcrect, int leftWidth, int rightWidth, int topHeight, int bottomHeight, float scale, ScaleMode scaleMode, nint dst, nint dstrect) {
        if (src == nint.Zero || dst == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "BlitSurface9Grid: Source or destination pointer is null.");
            return false;
        }
        return SDL_BlitSurface9Grid(src, srcrect, leftWidth, rightWidth, topHeight, bottomHeight, scale, scaleMode, dst, dstrect);
    }

    public static bool BlitSurfaceScaled(nint src, nint srcrect, nint dst, nint dstrect, ScaleMode scaleMode) {
        if (src == nint.Zero || dst == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "BlitSurfaceScaled: Source or destination pointer is null.");
            return false;
        }
        return SDL_BlitSurfaceScaled(src, srcrect, dst, dstrect, scaleMode);
    }

    public static bool BlitSurfaceTiled(nint src, nint srcrect, nint dst, nint dstrect) {
        if (src == nint.Zero || dst == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "BlitSurfaceTiled: Source or destination pointer is null.");
            return false;
        }
        return SDL_BlitSurfaceTiled(src, srcrect, dst, dstrect);
    }

    public static bool BlitSurfaceTiledWithScale(nint src, nint srcrect, float scale, ScaleMode scaleMode, nint dst, nint dstrect) {
        if (src == nint.Zero || dst == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "BlitSurfaceTiledWithScale: Source or destination pointer is null.");
            return false;
        }
        return SDL_BlitSurfaceTiledWithScale(src, srcrect, scale, scaleMode, dst, dstrect);
    }

    public static bool BlitSurfaceUnchecked(nint src, nint srcrect, nint dst, nint dstrect) {
        if (src == nint.Zero || dst == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "BlitSurfaceUnchecked: Source or destination pointer is null.");
            return false;
        }
        return SDL_BlitSurfaceUnchecked(src, srcrect, dst, dstrect);
    }

    public static bool BlitSurfaceUncheckedScaled(nint src, nint srcrect, nint dst, nint dstrect, ScaleMode scaleMode) {
        if (src == nint.Zero || dst == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "BlitSurfaceUncheckedScaled: Source or destination pointer is null.");
            return false;
        }
        return SDL_BlitSurfaceUncheckedScaled(src, srcrect, dst, dstrect, scaleMode);
    }

    public static void CleanupTLS() {
        SDL_CleanupTLS();
    }

    public static bool ClearClipboardData() {
        return SDL_ClearClipboardData();
    }

    public static bool ClearComposition(nint window) {
        if (window == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "ClearComposition: Window handle is null.");
            return false;
        }
        return SDL_ClearComposition(window);
    }

    public static bool ClearError() {
        return SDL_ClearError();
    }

    public static bool ClearProperty(uint props, string name) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            Logger.LogWarn(LogCategory.System, "ClearProperty: Properties handle is zero or name is null/empty.");
            return false;
        }
        return SDL_ClearProperty(props, name);
    }

    public static bool ClearSurface(nint surface, float r, float g, float b, float a) {
        if (surface == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "ClearSurface: Surface pointer is null.");
            return false;
        }
        return SDL_ClearSurface(surface, r, g, b, a);
    }

    public static uint ComposeCustomBlendMode(BlendFactor srcColorFactor, BlendFactor dstColorFactor, BlendOperation colorOperation, BlendFactor srcAlphaFactor, BlendFactor dstAlphaFactor, BlendOperation alphaOperation) {
        if (!Enum.IsDefined(srcColorFactor) ||
            !Enum.IsDefined(dstColorFactor) ||
            !Enum.IsDefined(colorOperation) ||
            !Enum.IsDefined(srcAlphaFactor) ||
            !Enum.IsDefined(dstAlphaFactor) ||
            !Enum.IsDefined(alphaOperation)) {
            Logger.LogError(LogCategory.Error, "ComposeCustomBlendMode: Invalid blend factors or operations provided.");
            throw new ArgumentException("Invalid blend factors or operations.");
        }

        uint blendMode = SDL_ComposeCustomBlendMode(srcColorFactor, dstColorFactor, colorOperation, srcAlphaFactor, dstAlphaFactor, alphaOperation);
        if (blendMode == 0) {
            Logger.LogError(LogCategory.Error, "ComposeCustomBlendMode: Failed to compose custom blend mode.");
        }

        return blendMode;
    }

    public static bool ConvertPixels(int width, int height, PixelFormat srcFormat, nint src, int srcPitch, PixelFormat dstFormat, nint dst, int dstPitch) {
        if (src == nint.Zero || dst == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "ConvertPixels: Source or destination pointer is null.");
            return false;
        }
        return SDL_ConvertPixels(width, height, srcFormat, src, srcPitch, dstFormat, dst, dstPitch);
    }

    public static bool ConvertPixelsAndColorspace(int width, int height, PixelFormat srcFormat, Colorspace srcColorspace, uint srcProperties, nint src, int srcPitch, PixelFormat dstFormat, Colorspace dstColorspace, uint dstProperties, nint dst, int dstPitch) {
        if (src == nint.Zero || dst == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "ConvertPixelsAndColorspace: Source or destination pointer is null.");
            return false;
        }
        return SDL_ConvertPixelsAndColorspace(width, height, srcFormat, srcColorspace, srcProperties, src, srcPitch, dstFormat, dstColorspace, dstProperties, dst, dstPitch);
    }

    public static nint ConvertSurface(nint surface, PixelFormat format) {
        if (surface == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "ConvertSurface: Surface pointer is null.");
            return nint.Zero;
        }
        return SDL_ConvertSurface(surface, format);
    }

    public static nint ConvertSurfaceAndColorspace(nint surface, PixelFormat format, nint palette, Colorspace colorspace, uint props) {
        if (surface == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "ConvertSurfaceAndColorspace: Surface pointer is null.");
            return nint.Zero;
        }
        return SDL_ConvertSurfaceAndColorspace(surface, format, palette, colorspace, props);
    }

    public static bool CopyProperties(uint src, uint dst) {
        if (src == 0 || dst == 0) {
            Logger.LogWarn(LogCategory.System, "CopyProperties: Source or destination properties handle is zero.");
            return false;
        }
        return SDL_CopyProperties(src, dst);
    }

    public static nint CreatePalette(int ncolors) {
        if (ncolors <= 0) {
            Logger.LogError(LogCategory.Error, "CreatePalette: Number of colors must be greater than zero.");
        }

        nint palette = SDL_CreatePalette(ncolors);
        if (palette == nint.Zero) {
            Logger.LogError(LogCategory.Error, "CreatePalette: Failed to create palette.");
        }

        return palette;
    }

    public static nint CreatePopupWindow(nint parent, int offsetX, int offsetY, int w, int h, WindowFlags flags) {
        if (parent == nint.Zero) {
            Logger.LogError(LogCategory.Error, "CreatePopupWindow: Parent window handle is null.");
            return nint.Zero;
        }
        if (w <= 0 || h <= 0) {
            Logger.LogError(LogCategory.Error, "CreatePopupWindow: Invalid width or height.");
            return nint.Zero;
        }

        if(!Enum.IsDefined(flags)) {
            Logger.LogError(LogCategory.Error, "CreatePopupWindow: Invalid window flags.");
            return nint.Zero;
        }

        nint popupWindow = SDL_CreatePopupWindow(parent, offsetX, offsetY, w, h, flags);
        if (popupWindow == nint.Zero) {
            Logger.LogError(LogCategory.Error, "CreatePopupWindow: Failed to create popup window.");
        }
        return popupWindow;
    }

    public static uint CreateProperties() {
        uint props = SDL_CreateProperties();
        if (props == 0) {
            Logger.LogError(LogCategory.Error, "CreateProperties: Failed to create properties.");
        }

        return props;
    }

    public static nint CreateSurface(int width, int height, PixelFormat format) {

        if (width <= 0 || height <= 0) {
            Logger.LogError(LogCategory.Error, "CreateSurface: Invalid width or height.");
            return nint.Zero;
        }

        return SDL_CreateSurface(width, height, format);
    }

    public static nint CreateSurfaceFrom(int width, int height, PixelFormat format, nint pixels, int pitch) {
        if (pixels == nint.Zero) {
            Logger.LogError(LogCategory.System, "CreateSurfaceFrom: Pixels pointer is null.");
            return nint.Zero;
        }

        if(!Enum.IsDefined(format)) {
            Logger.LogError(LogCategory.Error, "CreateSurfaceFrom: Invalid pixel format.");
            return nint.Zero;
        }

        return SDL_CreateSurfaceFrom(width, height, format, pixels, pitch);
    }

    public static nint CreateSurfacePalette(nint surface) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.System, "CreateSurfacePalette: Surface pointer is null.");
            return nint.Zero;
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
            Logger.LogError(LogCategory.Error, "CreateThreadWithPropertiesRuntime: Failed to create thread with properties.");
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
            Logger.LogError(LogCategory.Error, "CreateWindowWithProperties: Failed to create window with properties.");
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

    public static bool DestroyWindowSurface(nint window) {
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

    public static bool DisableScreenSaver() {
        return SDL_DisableScreenSaver();
    }

    public static nint DuplicateSurface(nint surface) {
        if (surface == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "DuplicateSurface: Surface pointer is null.");
            return nint.Zero;
        }
        return SDL_DuplicateSurface(surface);
    }

    public static bool EnableScreenSaver() {
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

    public static bool EnumerateProperties(uint props, SdlEnumeratePropertiesCallback callback, nint userdata) {
        if (props == 0 || callback == null) {
            Logger.LogWarn(LogCategory.System, "EnumerateProperties: Properties handle is zero or callback is null.");
            return false;
        }
        return SDL_EnumerateProperties(props, callback, userdata);
    }

    public static unsafe bool FillSurfaceRect(nint dst, Rect rect, uint color) {
        if (dst == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "FillSurfaceRect: Destination pointer is null.");
            return false;
        }
        nint rectPtr = Marshal.AllocHGlobal(sizeof(Rect));
        *(Rect*)rectPtr = rect;
        bool result = SDL_FillSurfaceRect(dst, rectPtr, color);
        if (!result) {
            Logger.LogError(LogCategory.Error, "FillSurfaceRect: Failed to fill surface rectangle.");
        }
        Marshal.FreeHGlobal(rectPtr);
        return result;
    }

    public static bool FillSurfaceRects(nint dst, Span<Rect> rects, uint color) {
        if (dst == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "FillSurfaceRects: Destination pointer is null.");
            return false;
        }
        if (rects.IsEmpty) {
            Logger.LogWarn(LogCategory.System, "FillSurfaceRects: Rectangles span is empty.");
            return false;
        }
        bool result = SDL_FillSurfaceRects(dst, rects, rects.Length, color);
        if (!result) {
            Logger.LogError(LogCategory.Error, "FillSurfaceRects: Failed to fill surface rectangles.");
        }
        return result;
    }

    public static bool FlashWindow(nint window, FlashOperation operation) {
        if (window == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "FlashWindow: Window handle is null.");
            return false;
        }

        if (!Enum.IsDefined(operation)) {
            Logger.LogError(LogCategory.Error, "FlashWindow: Invalid flash operation.");
            return false;
        }

        bool result = SDL_FlashWindow(window, operation);
        if (!result) {
            Logger.LogError(LogCategory.Error, "FlashWindow: Failed to flash window.");
        }
        return result;
    }

    public static bool FlipSurface(nint surface, FlipMode flip) {
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
            Logger.LogError(LogCategory.Error, "GetAppMetadataProperty: Failed to retrieve property.");
        }
        return result;
    }

    public static bool GetBooleanProperty(uint props, string name, bool defaultValue) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            Logger.LogWarn(LogCategory.System, "GetBooleanProperty: Properties handle is zero or name is null/empty.");
            return defaultValue;
        }
        bool result = SDL_GetBooleanProperty(props, name, defaultValue);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetBooleanProperty: Failed to retrieve boolean property.");
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
            Logger.LogError(LogCategory.Error, "GetClipboardData: Failed to retrieve clipboard data.");
            return [];
        }

        if (size == 0) {
            Logger.LogWarn(LogCategory.System, "GetClipboardData: Retrieved data size is zero.");
            return [];
        }

        nint[] data = new nint[size];
        Marshal.Copy(result, data, 0, (int)size);

        return new Span<nint>(data);
    }

    public static Span<nint> GetClipboardMimeTypes() {
        nint result = SDL_GetClipboardMimeTypes(out nuint numMimeTypes);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetClipboardMimeTypes: Failed to retrieve clipboard mime types.");
            return [];
        }

        nint[] data = new nint[numMimeTypes];
        Marshal.Copy(result, data, 0, (int)numMimeTypes);

        return new Span<nint>(data);
    }

    public static nint GetClipboardMimeTypes(out nuint numMimeTypes) {
        nint result = SDL_GetClipboardMimeTypes(out numMimeTypes);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetClipboardMimeTypes: Failed to retrieve clipboard mime types.");
            return nint.Zero;
        }
        return result;
    }

    public static string GetClipboardText() {
        string result = SDL_GetClipboardText();
        if (string.IsNullOrEmpty(result)) {
            Logger.LogError(LogCategory.Error, "GetClipboardText: Failed to retrieve clipboard text.");
        }
        return result;
    }

    public static bool GetClosestFullscreenDisplayMode(uint displayId, int w, int h, float refreshRate,
            bool includeHighDensityModes, out DisplayMode closest) {
        if (displayId == 0) {
            Logger.LogWarn(LogCategory.System, "GetClosestFullscreenDisplayMode: Display ID is zero.");
            closest = default;
            return false;
        }
        bool result = SDL_GetClosestFullscreenDisplayMode(displayId, w, h, refreshRate, includeHighDensityModes,
            out closest);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetClosestFullscreenDisplayMode: Failed to retrieve closest mode.");
        }
        return result;
    }

    public static nint GetCurrentDisplayMode(uint displayId) {
        if (displayId == 0) {
            Logger.LogWarn(LogCategory.System, "GetCurrentDisplayMode: Display ID is zero.");
            return nint.Zero;
        }
        nint mode = SDL_GetCurrentDisplayMode(displayId);
        if (mode == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetCurrentDisplayMode: Failed to retrieve current mode.");
        }
        return mode;
    }

    public static unsafe void GetCurrentDisplayMode(uint displayId, out DisplayMode mode) {
        if (displayId == 0) {
            Logger.LogWarn(LogCategory.System, "GetCurrentDisplayMode: Display ID is zero.");
            mode = default;
            return;
        }
        nint modePtr = SDL_GetCurrentDisplayMode(displayId);
        if (modePtr == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetCurrentDisplayMode: Failed to retrieve current mode.");
            mode = default;
            return;
        }

        mode = *(DisplayMode*)modePtr;
    }

    public static DisplayOrientation GetCurrentDisplayOrientation(uint displayId) {
        if (displayId == 0) {
            Logger.LogWarn(LogCategory.System, "GetCurrentDisplayOrientation: Display ID is zero.");
            return DisplayOrientation.Unknown;
        }
        DisplayOrientation orientation = SDL_GetCurrentDisplayOrientation(displayId);
        if (orientation == DisplayOrientation.Unknown) {
            Logger.LogError(LogCategory.Error, "GetCurrentDisplayOrientation: Failed to retrieve orientation.");
        }
        return orientation;
    }

    public static ulong GetCurrentThreadID() {
        ulong threadId = SDL_GetCurrentThreadID();
        if (threadId == 0) {
            Logger.LogError(LogCategory.Error, "GetCurrentThreadID: Failed to retrieve thread ID.");
        }
        return threadId;
    }

    public static string GetCurrentVideoDriver() {
        return SDL_GetCurrentVideoDriver();
    }

    public static DisplayMode GetDesktopDisplayMode(uint displayId) {
        GetDesktopDisplayMode(displayId, out DisplayMode mode);
        return mode;
    }

    public static unsafe void GetDesktopDisplayMode(uint displayId, out DisplayMode mode) {
        if (displayId == 0) {
            Logger.LogWarn(LogCategory.System, "GetDesktopDisplayMode: Display ID is zero.");
            mode = default;
            return;
        }
        nint modePtr = SDL_GetDesktopDisplayMode(displayId);
        if (modePtr == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetDesktopDisplayMode: Failed to retrieve desktop mode.");
            mode = default;
            return;
        }
        mode = *(DisplayMode*)modePtr;
    }

    public static bool GetDisplayBounds(uint displayId, out Rect rect) {
        if (displayId == 0) {
            Logger.LogWarn(LogCategory.System, "GetDisplayBounds: Display ID is zero.");
            rect = default;
            return false;
        }
        bool result = SDL_GetDisplayBounds(displayId, out rect);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetDisplayBounds: Failed to retrieve display bounds.");
        }
        return result;
    }

    public static float GetDisplayContentScale(uint displayId) {
        if (displayId == 0) {
            Logger.LogWarn(LogCategory.System, "GetDisplayContentScale: Display ID is zero.");
            return 0f;
        }
        float scale = SDL_GetDisplayContentScale(displayId);
        if (scale <= 0.01f) {
            Logger.LogError(LogCategory.Error, "GetDisplayContentScale: Failed to retrieve content scale.");
        }
        return scale;
    }

    public static uint GetDisplayForPoint(ref Point point) {
        uint displayId = SDL_GetDisplayForPoint(ref point);
        if (displayId == 0) {
            Logger.LogError(LogCategory.Error, "GetDisplayForPoint: Failed to retrieve display ID.");
        }
        return displayId;
    }

    public static uint GetDisplayForRect(ref Rect rect) {
        uint displayId = SDL_GetDisplayForRect(ref rect);
        if (displayId == 0) {
            Logger.LogError(LogCategory.Error, "GetDisplayForRect: Failed to retrieve display ID.");
        }
        return displayId;
    }

    public static uint GetDisplayForWindow(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetDisplayForWindow: Window handle is null.");
            return 0;
        }
        uint displayId = SDL_GetDisplayForWindow(window);
        if (displayId == 0) {
            Logger.LogError(LogCategory.Error, "GetDisplayForWindow: Failed to retrieve display ID.");
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
            Logger.LogError(LogCategory.Error, "GetDisplayName: Failed to retrieve display name.");
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
            Logger.LogError(LogCategory.Error, "GetDisplayProperties: Failed to retrieve display properties.");
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
            Logger.LogError(LogCategory.Error, "GetDisplays: Failed to retrieve display handles.");
            return [];
        }

        nint[] data = new nint[count];
        Marshal.Copy(result, data, 0, count);

        return data;
    }

    public static bool GetDisplayUsableBounds(uint displayId, out Rect rect) {
        if (displayId == 0) {
            Logger.LogWarn(LogCategory.System, "GetDisplayUsableBounds: Display ID is zero.");
            rect = default;
            return false;
        }
        bool result = SDL_GetDisplayUsableBounds(displayId, out rect);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetDisplayUsableBounds: Failed to retrieve usable bounds.");
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
            Logger.LogError(LogCategory.Error, "GetFloatProperty: Failed to retrieve float property.");
        }
        return result;
    }

    public static Span<int> GetFullscreenDisplayModes(uint displayId) {
        nint result = SDL_GetFullscreenDisplayModes(displayId, out int count);

        if (result == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetFullscreenDisplayModes: Failed to retrieve fullscreen display modes.");
            return [];
        }

        if (count <= 0) {
            Logger.LogWarn(LogCategory.System, "GetFullscreenDisplayModes: Retrieved count is zero or negative.");
            return [];
        }

        int[] data = new int[count];
        Marshal.Copy(result, data, 0, count);

        return data;
    }

    public static Span<nint> GetFullscreenDisplayModes(uint displayId, out int count) {
        nint result = SDL_GetFullscreenDisplayModes(displayId, out count);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetFullscreenDisplayModes: Failed to retrieve fullscreen display modes.");
            return [];
        }

        if (count <= 0) {
            Logger.LogWarn(LogCategory.System, "GetFullscreenDisplayModes: Retrieved count is zero or negative.");
            return [];
        }

        nint[] data = new nint[count];
        Marshal.Copy(result, data, 0, count);


        return data;
    }

    public static uint GetGlobalProperties() {
        return SDL_GetGlobalProperties();
    }

    public static nint GetGrabbedWindow() {
        nint window = SDL_GetGrabbedWindow();
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetGrabbedWindow: Failed to retrieve grabbed window.");
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
            Logger.LogError(LogCategory.Error, "GetHint: Failed to retrieve hint.");
        }
        return result;
    }

    public static bool GetHintBoolean(string name, bool defaultValue) {
        if (string.IsNullOrEmpty(name)) {
            Logger.LogWarn(LogCategory.System, "GetHintBoolean: Name is null or empty.");
            return defaultValue;
        }
        bool result = SDL_GetHintBoolean(name, defaultValue);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetHintBoolean: Failed to retrieve hint boolean.");
        }
        return result;
    }

    public static nint GetKeyboard(out int count) {
        nint result = SDL_GetKeyboards(out count);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetKeyboard: Failed to retrieve keyboard handles.");
            return nint.Zero;
        }
        return result;
    }

    public static Span<nint> GetKeyboard() {
        nint result = GetKeyboard(out int count);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetKeyboard: Failed to retrieve keyboard handles.");
            return [];
        }

        if (count <= 0) {
            Logger.LogWarn(LogCategory.System, "GetKeyboard: Retrieved count is zero or negative.");
            return [];
        }

        nint[] data = new nint[count];
        Marshal.Copy(result, data, 0, count);

        return data;
    }

    public static nint GetKeyboardFocus() {
        nint window = SDL_GetKeyboardFocus();
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetKeyboardFocus: Failed to retrieve keyboard focus.");
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
            Logger.LogError(LogCategory.Error, "GetKeyboardNameForID: Failed to retrieve keyboard name.");
        }
        return name;
    }

    public static Span<bool> GetKeyboardState(out int numkeys) {
        nint result = SDL_GetKeyboardState(out numkeys);

        if (result == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetKeyboardState: Failed to retrieve keyboard state.");
            return [];
        }

        if (numkeys <= 0) {
            Logger.LogWarn(LogCategory.System, "GetKeyboardState: Retrieved count is zero or negative.");
            return [];
        }

        bool[] state = new bool[numkeys];
        for(int i = 0; i < numkeys; i++) {
            state[i] = Marshal.ReadByte(result, i) != 0;
        }

        return state;
    }

    public static uint GetKeyFromName(string name) {
        if (string.IsNullOrEmpty(name)) {
            Logger.LogWarn(LogCategory.System, "GetKeyFromName: Name is null or empty.");
            return 0;
        }
        uint key = SDL_GetKeyFromName(name);
        if (key == 0) {
            Logger.LogError(LogCategory.Error, "GetKeyFromName: Failed to retrieve key from name.");
        }
        return key;
    }

    public static uint GetKeyFromScancode(ScanCode scanCode, KeyMod modstate, bool keyEvent) {
        if (scanCode == ScanCode.Unknown) {
            Logger.LogWarn(LogCategory.System, "GetKeyFromScancode: Scan code is unknown.");
            return 0;
        }
        uint key = SDL_GetKeyFromScancode(scanCode, modstate, keyEvent);
        if (key == 0) {
            Logger.LogError(LogCategory.Error, "GetKeyFromScancode: Failed to retrieve key from scan code.");
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
            Logger.LogError(LogCategory.Error, "GetKeyName: Failed to retrieve key name.");
        }
        return name;
    }

    public static bool GetMasksForPixelFormat(PixelFormat format, out int bpp, out uint rmask, out uint gmask,
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
        bool result = SDL_GetMasksForPixelFormat(format, out bpp, out rmask, out gmask, out bmask, out amask);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetMasksForPixelFormat: Failed to retrieve masks for pixel format.");
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
            Logger.LogError(LogCategory.Error, "GetNaturalDisplayOrientation: Failed to retrieve orientation.");
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
            Logger.LogError(LogCategory.Error, "GetNumberProperty: Failed to retrieve number property.");
        }
        return result;
    }

    public static int GetNumVideoDrivers() {
        int numDrivers = SDL_GetNumVideoDrivers();
        if (numDrivers <= 0) {
            Logger.LogError(LogCategory.Error, "GetNumVideoDrivers: Failed to retrieve number of video drivers.");
        }
        return numDrivers;
    }

    public static nint GetPixelFormatDetails(PixelFormat format) {
        if (format == PixelFormat.Unknown) {
            Logger.LogWarn(LogCategory.System, "GetPixelFormatDetails: Format is unknown.");
            return nint.Zero;
        }
        nint details = SDL_GetPixelFormatDetails(format);
        if (details == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetPixelFormatDetails: Failed to retrieve pixel format details.");
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
            Logger.LogError(LogCategory.Error, "GetPixelFormatForMasks: Failed to retrieve pixel format.");
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
            Logger.LogError(LogCategory.Error, "GetPixelFormatName: Failed to retrieve pixel format name.");
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
            Logger.LogError(LogCategory.Error, "GetPointerProperty: Failed to retrieve pointer property.");
        }
        return result;
    }

    public static PowerState GetPowerInfo(out int seconds, out int percent) {
        PowerState state = SDL_GetPowerInfo(out seconds, out percent);
        if (state == PowerState.Unknown) {
            Logger.LogError(LogCategory.Error, "GetPowerInfo: Failed to retrieve power info.");
        }
        return state;
    }

    public static Span<nint> GetPreferredLocales() {
        nint result = SDL_GetPreferredLocales(out int count);
        if(result == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetPreferredLocales: Failed to retrieve preferred locales.");
            return [];
        }

        if(count <= 0) {
            Logger.LogWarn(LogCategory.System, "GetPreferredLocales: Retrieved count is zero or negative.");
            return [];
        }

        nint[] data = new nint[count];
        Marshal.Copy(result, data, 0, count);

        return data;
    }

    public static uint GetPrimaryDisplay() {
        uint displayId = SDL_GetPrimaryDisplay();
        if (displayId == 0) {
            Logger.LogError(LogCategory.Error, "GetPrimaryDisplay: Failed to retrieve primary display ID.");
        }
        return displayId;
    }

    public static string GetPrimarySelectionText() {
        string text = SDL_GetPrimarySelectionText();
        if (string.IsNullOrEmpty(text)) {
            Logger.LogError(LogCategory.Error, "GetPrimarySelectionText: Failed to retrieve primary selection text.");
        }
        return text;
    }

    public static PropertyType GetPropertyType(uint props, string name) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            Logger.LogWarn(LogCategory.System, "GetPropertyType: Properties handle is zero or name is null/empty.");
        }
        return SDL_GetPropertyType(props, name);
    }

    public static bool GetRectAndLineIntersection(ref Rect rect, ref int x1, ref int y1, ref int x2, ref int y2) {
        bool result = SDL_GetRectAndLineIntersection(ref rect, ref x1, ref y1, ref x2, ref y2);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetRectAndLineIntersection: Failed to retrieve intersection.");
        }
        return result;
    }

    public static bool GetRectAndLineIntersectionFloat(ref FRect rect, ref float x1, ref float y1, ref float x2,
        ref float y2) {
        bool result = SDL_GetRectAndLineIntersectionFloat(ref rect, ref x1, ref y1, ref x2, ref y2);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetRectAndLineIntersectionFloat: Failed to retrieve intersection.");
        }
        return result;
    }

    public static bool GetRectEnclosingPoints(Span<Point> points, int count, ref Rect clip, out Rect result) {
        bool resultBool = SDL_GetRectEnclosingPoints(points, count, ref clip, out result);
        if (!resultBool) {
            Logger.LogError(LogCategory.Error, "GetRectEnclosingPoints: Failed to retrieve enclosing points.");
        }
        return resultBool;
    }

    public static bool GetRectEnclosingPointsFloat(Span<FPoint> points, int count, ref FRect clip, out FRect result) {
        bool resultBool = SDL_GetRectEnclosingPointsFloat(points, count, ref clip, out result);
        if (!resultBool) {
            Logger.LogError(LogCategory.Error, "GetRectEnclosingPointsFloat: Failed to retrieve enclosing points.");
        }
        return resultBool;
    }

    public static bool GetRectIntersection(ref Rect a, ref Rect b, out Rect result) {
        bool resultBool = SDL_GetRectIntersection(ref a, ref b, out result);
        if (!resultBool) {
            Logger.LogError(LogCategory.Error, "GetRectIntersection: Failed to retrieve intersection.");
        }
        return resultBool;
    }

    public static bool GetRectIntersectionFloat(ref FRect a, ref FRect b, out FRect result) {
        bool resultBool = SDL_GetRectIntersectionFloat(ref a, ref b, out result);
        if (!resultBool) {
            Logger.LogError(LogCategory.Error, "GetRectIntersectionFloat: Failed to retrieve intersection.");
        }
        return resultBool;
    }

    public static bool GetRectUnion(ref Rect a, ref Rect b, out Rect result) {
        bool resultBool = SDL_GetRectUnion(ref a, ref b, out result);
        if (!resultBool) {
            Logger.LogError(LogCategory.Error, "GetRectUnion: Failed to retrieve union.");
        }
        return resultBool;
    }

    public static bool GetRectUnionFloat(ref FRect a, ref FRect b, out FRect result) {
        bool resultBool = SDL_GetRectUnionFloat(ref a, ref b, out result);
        if (!resultBool) {
            Logger.LogError(LogCategory.Error, "GetRectUnionFloat: Failed to retrieve union.");
        }
        return resultBool;
    }

    public static Color GetRGB(uint pixel, nint format, nint palette) {
        if (format == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetRGB: Format pointer is null.");
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
            Logger.LogError(LogCategory.Error, "GetRGBA: Format pointer is null.");
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
            Logger.LogError(LogCategory.Error, "GetScancodeFromKey: Failed to retrieve scan code from key.");
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
            Logger.LogError(LogCategory.Error, "GetScancodeFromName: Failed to retrieve scan code from name.");
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
            Logger.LogError(LogCategory.Error, "GetScancodeName: Failed to retrieve scan code name.");
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
            Logger.LogError(LogCategory.Error, "GetStringProperty: Failed to retrieve string property.");
        }
        return result;
    }

    public static bool GetSurfaceAlphaMod(nint surface, out byte alpha) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetSurfaceAlphaMod: Surface pointer is null.");
            alpha = 0;
            return false;
        }
        bool result = SDL_GetSurfaceAlphaMod(surface, out alpha);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetSurfaceAlphaMod: Failed to retrieve surface alpha mod.");
        }
        return result;
    }

    public static nint GetSurfaceBalette(nint surface) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetSurfacePalette: Surface pointer is null.");
            return nint.Zero;
        }
        nint palette = SDL_GetSurfacePalette(surface);
        if (palette == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetSurfacePalette: Failed to retrieve surface palette.");
        }

        return palette;
    }

    public static bool GetSurfaceBlendMode(nint surface, nint blendMode) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetSurfaceBlendMode: Surface pointer is null.");
            return false;
        }
        bool result = SDL_GetSurfaceBlendMode(surface, blendMode);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetSurfaceBlendMode: Failed to retrieve surface blend mode.");
        }
        return result;
    }

    public static bool GetSurfaceClipRect(nint surface, out Rect rect) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetSurfaceClipRect: Surface pointer is null.");
            rect = default;
            return false;
        }
        bool result = SDL_GetSurfaceClipRect(surface, out rect);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetSurfaceClipRect: Failed to retrieve surface clip rect.");
        }
        return result;
    }

    public static bool GetSurfaceColorKey(nint surface, out uint key) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetSurfaceColorKey: Surface pointer is null.");
            key = 0;
            return false;
        }
        bool result = SDL_GetSurfaceColorKey(surface, out key);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetSurfaceColorKey: Failed to retrieve surface color key.");
        }
        return result;
    }

    public static bool GetSurfaceColorMod(nint surface, out byte r, out byte g, out byte b) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetSurfaceColorMod: Surface pointer is null.");
            r = g = b = 0;
            return false;
        }
        bool result = SDL_GetSurfaceColorMod(surface, out r, out g, out b);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetSurfaceColorMod: Failed to retrieve surface color mod.");
        }
        return result;
    }

    public static Span<nint> GetSurfaceImages(nint surface, out int count) {
        nint result = SDL_GetSurfaceImages(surface, out count);
        if (result == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetSurfaceImages: Failed to retrieve surface images.");
            return [];
        }

        if (count <= 0) {
            Logger.LogError(LogCategory.Error, "GetSurfaceImages: No images found.");
            return [];
        }

        Span<nint> images = new(ref result);
        if (images == []) {
            Logger.LogError(LogCategory.Error, "GetSurfaceImages: Failed to create span for surface images.");
            return [];
        }

        if (images.Length != count) {
            Logger.LogError(LogCategory.Error, "GetSurfaceImages: Mismatch between count and span length.");
            return [];
        }

        for (int i = 0; i < count; i++) {
            if (images[i] == nint.Zero) {
                Logger.LogError(LogCategory.Error, $"GetSurfaceImages: Image at index {i} is null.");
                return [];
            }
        }

        return images.ToArray();
    }

    public static uint GetSurfaceProperties(nint surface) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetSurfaceProperties: Surface pointer is null.");
            return 0;
        }
        uint properties = SDL_GetSurfaceProperties(surface);
        if (properties == 0) {
            Logger.LogError(LogCategory.Error, "GetSurfaceProperties: Failed to retrieve surface properties.");
        }
        return properties;
    }

    public static SystemTheme GetSystemTheme() {
        SystemTheme theme = SDL_GetSystemTheme();
        if (theme == SystemTheme.Unknown) {
            Logger.LogError(LogCategory.Error, "GetSystemTheme: Failed to retrieve system theme.");
        }
        return theme;
    }

    public static bool GetTextInputArea(nint window, out Rect rect, out int cursor) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetTextInputArea: Window pointer is null.");
            rect = default;
            cursor = 0;
            return false;
        }
        bool result = SDL_GetTextInputArea(window, out rect, out cursor);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetTextInputArea: Failed to retrieve text input area.");
        }
        return result;
    }

    public static ulong GetThreadId(nint thread) {
        if (thread == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetThreadId: Thread pointer is null.");
            return 0;
        }
        ulong threadId = SDL_GetThreadID(thread);
        if (threadId == 0) {
            Logger.LogError(LogCategory.Error, "GetThreadId: Failed to retrieve thread ID.");
        }
        return threadId;
    }

    public static string GetThreadName(nint thread) {
        if (thread == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetThreadName: Thread pointer is null.");
            return string.Empty;
        }
        string name = SDL_GetThreadName(thread);
        if (string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.Error, "GetThreadName: Failed to retrieve thread name.");
        }
        return name;
    }

    public static ThreadState GetThreadState(nint thread) {
        if (thread == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetThreadState: Thread pointer is null.");
            return ThreadState.Unknown;
        }
        ThreadState state = SDL_GetThreadState(thread);
        if (state == ThreadState.Unknown) {
            Logger.LogError(LogCategory.Error, "GetThreadState: Failed to retrieve thread state.");
        }
        return state;
    }

    public static nint GetTLS(nint id) {
        if (id == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetTLS: ID is zero.");
            return nint.Zero;
        }
        nint tls = SDL_GetTLS(id);
        if (tls == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetTLS: Failed to retrieve TLS value.");
        }
        return tls;
    }

    public static string GetVideoDriver(int index) {
        if (index < 0) {
            Logger.LogError(LogCategory.Error, "GetVideoDriver: Index is negative.");
            return string.Empty;
        }
        string driver = SDL_GetVideoDriver(index);
        if (string.IsNullOrEmpty(driver)) {
            Logger.LogError(LogCategory.Error, "GetVideoDriver: Failed to retrieve video driver.");
        }
        return driver;
    }

    public static bool GetWindowAspectRatio(nint window, out float minAspect, out float maxAspect) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowAspectRatio: Window pointer is null.");
            minAspect = maxAspect = 0;
            return false;
        }
        bool result = SDL_GetWindowAspectRatio(window, out minAspect, out maxAspect);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetWindowAspectRatio: Failed to retrieve window aspect ratio.");
        }
        return result;
    }

    public static bool GetWindowBorderSize(nint window, out int top, out int left, out int bottom, out int right) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowBorderSize: Window pointer is null.");
            top = left = bottom = right = 0;
            return false;
        }
        bool result = SDL_GetWindowBordersSize(window, out top, out left, out bottom, out right);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetWindowBorderSize: Failed to retrieve window border size.");
        }
        return result;
    }

    public static Rect GetWindowBorderSize(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowBorderSize: Window pointer is null.");
            return default;
        }
        bool result = SDL_GetWindowBordersSize(window, out int top, out int left, out int bottom, out int right);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetWindowBorderSize: Failed to retrieve window border size.");
        }
        return new Rect() { X = left, Y = top, W = right - left, H = bottom - top };
    }

    public static float GetWindowDisplayScale(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowDisplayScale: Window pointer is null.");
            return 0;
        }
        float scale = SDL_GetWindowDisplayScale(window);
        if (scale <= 0) {
            Logger.LogError(LogCategory.Error, "GetWindowDisplayScale: Failed to retrieve window display scale.");
        }
        return scale;
    }

    public static WindowFlags GetWindowFlags(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowFlags: Window handle is null.");
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
            Logger.LogError(LogCategory.Error, "GetWindowFromId: Window ID is zero.");
            return nint.Zero;
        }
        nint windowHandle = SDL_GetWindowFromID(id);
        if (windowHandle == nint.Zero) {
            Logger.LogWarn(LogCategory.System, "GetWindowFromId: Failed to retrieve window handle.");
        }
        return windowHandle;
    }

    public static nint GetWindowFullscreenMode(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowFullscreenMode: Window pointer is null.");
            return nint.Zero;
        }
        nint mode = SDL_GetWindowFullscreenMode(window);
        if (mode == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowFullscreenMode: Failed to retrieve window fullscreen mode.");
        }
        return mode;
    }

    public static unsafe DisplayMode GetWindowFullScreenMode(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowFullScreenMode: Window pointer is null.");
            return default;
        }
        DisplayMode mode = *(DisplayMode*)SDL_GetWindowFullscreenMode(window);
        if (mode.DisplayId == 0) {
            Logger.LogError(LogCategory.Error, "GetWindowFullScreenMode: Failed to retrieve window fullscreen mode.");
        }
        return mode;
    }

    public static nint GetWindowICCProfile(nint window, out nuint size) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowICCProfile: Window pointer is null.");
            size = 0;
            return nint.Zero;
        }
        nint profile = SDL_GetWindowICCProfile(window, out size);
        if (profile == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowICCProfile: Failed to retrieve window ICC profile.");
        }
        return profile;
    }

    public static uint GetWindowId(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowId: Window handle is null.");
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
            Logger.LogError(LogCategory.Error, "GetWindowKeyboardGrab: Window pointer is null.");
            return false;
        }
        bool result = SDL_GetWindowKeyboardGrab(window);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetWindowKeyboardGrab: Failed to retrieve window keyboard grab.");
        }
        return result;
    }

    public static bool GetWindowMaximumSize(nint window, out int w, out int h) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowMaximumSize: Window pointer is null.");
            w = h = 0;
            return false;
        }
        bool result = SDL_GetWindowMaximumSize(window, out w, out h);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetWindowMaximumSize: Failed to retrieve window maximum size.");
        }
        return result;
    }

    public static Rect GetWindowMaximumSize(nint window) {
        if (window == nint.Zero) {
            return default;
        }

        bool result = SDL_GetWindowMaximumSize(window, out int w, out int h);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetWindowMaximumSize: Failed to retrieve window maximum size.");
            return default;
        }
        return new Rect() { W = w, H = h };
    }

    public static bool GetWindowMinimumSize(nint window, out int w, out int h) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowMinimumSize: Window pointer is null.");
            w = h = 0;
            return false;
        }
        bool result = SDL_GetWindowMinimumSize(window, out w, out h);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetWindowMinimumSize: Failed to retrieve window minimum size.");
        }
        return result;
    }

    public static Rect GetWindowMinumSize(nint window) {
        if (window == nint.Zero) {
            return default;
        }
        bool result = SDL_GetWindowMinimumSize(window, out int w, out int h);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetWindowMinimumSize: Failed to retrieve window minimum size.");
            return default;
        }
        return new Rect() { W = w, H = h };
    }

    public static bool GetWindowMouseGrab(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowMouseGrab: Window pointer is null.");
            return false;
        }
        bool result = SDL_GetWindowMouseGrab(window);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetWindowMouseGrab: Failed to retrieve window mouse grab.");
        }
        return result;
    }

    public static nint GetWindowMouseRectPtr(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowMouseRect: Window pointer is null.");
            return nint.Zero;
        }
        nint rect = SDL_GetWindowMouseRect(window);
        if (rect == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowMouseRect: Failed to retrieve window mouse rect.");
        }
        return rect;
    }

    public static unsafe Rect GetWindowMouseRect(nint window) {
        nint result = GetWindowMouseRectPtr(window);
        if(result == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowMouseRect: Failed to retrieve window mouse rect.");
            return new();
        }

        Rect rect = *(Rect*)result;
        
        return rect;
    }

    public static float GetWindowOpacity(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowOpacity: Window pointer is null.");
            return 0;
        }
        float opacity = SDL_GetWindowOpacity(window);
        if (opacity < 0) {
            Logger.LogError(LogCategory.Error, "GetWindowOpacity: Failed to retrieve window opacity.");
        }
        return opacity;
    }

    public static nint GetWindowParent(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowParent: Window handle is null.");
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
            Logger.LogError(LogCategory.Error, "GetWindowPixelDensity: Window pointer is null.");
            return 0;
        }
        float pixelDensity = SDL_GetWindowPixelDensity(window);
        if (pixelDensity < 0) {
            Logger.LogError(LogCategory.Error, "GetWindowPixelDensity: Failed to retrieve window pixel density.");
        }
        return pixelDensity;
    }

    public static PixelFormat GetWindowPixelFormat(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowPixelFormat: Window pointer is null.");
            return PixelFormat.Unknown;
        }
        PixelFormat pixelFormat = SDL_GetWindowPixelFormat(window);
        if (pixelFormat == PixelFormat.Unknown) {
            Logger.LogError(LogCategory.Error, "GetWindowPixelFormat: Failed to retrieve window pixel format.");
        }
        return pixelFormat;
    }

    public static bool GetWindowPosition(nint window, out int x, out int y) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowPosition: Window pointer is null.");
            x = y = 0;
            return false;
        }
        bool result = SDL_GetWindowPosition(window, out x, out y);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetWindowPosition: Failed to retrieve window position.");
        }
        return result;
    }

    public static Point GetWindowPosition(nint window) {
        if (window == nint.Zero) {
            return default;
        }
        bool result = SDL_GetWindowPosition(window, out int x, out int y);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetWindowPosition: Failed to retrieve window position.");
            return default;
        }
        return new Point() { X = x, Y = y };
    }

    public static uint GetWindowProperties(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowProperties: Window handle is null.");
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

        if (result == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindows: Failed to retrieve windows.");
            count = 0;
            return [];
        }

        nint[] nints = new nint[count];
        if (nints == null) {
            Logger.LogError(LogCategory.Error, "GetWindows: Failed to create array for windows.");
            count = 0;
            return [];
        }

        Span<nint> windows = new(nints);

        return windows.ToArray();
    }

    public static Span<nint> GetWindows() {
        return GetWindows(out _);
    }

    public static bool GetWindowSafeArea(nint window, out Rect rect) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowSafeArea: Window pointer is null.");
            rect = default;
            return false;
        }
        bool result = SDL_GetWindowSafeArea(window, out rect);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetWindowSafeArea: Failed to retrieve window safe area.");
        }
        return result;
    }

    public static Rect GetWindowSafeArea(nint window) {
        if (window == nint.Zero) {
            return default;
        }
        bool result = SDL_GetWindowSafeArea(window, out Rect rect);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetWindowSafeArea: Failed to retrieve window safe area.");
            return default;
        }
        return rect;
    }

    public static bool GetWindowSize(nint window, out int w, out int h) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowSize: Window pointer is null.");
            w = h = 0;
            return false;
        }
        bool result = SDL_GetWindowSize(window, out w, out h);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetWindowSize: Failed to retrieve window size.");
        }
        return result;
    }

    public static Rect GetWindowSize(nint window) {
        if (window == nint.Zero) {
            return default;
        }
        bool result = SDL_GetWindowSize(window, out int w, out int h);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetWindowSize: Failed to retrieve window size.");
            return default;
        }
        return new Rect() { W = w, H = h };
    }

    public static bool GetWindowSizeInPixels(nint window, out int w, out int h) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowSizeInPixels: Window pointer is null.");
            w = h = 0;
            return false;
        }
        bool result = SDL_GetWindowSizeInPixels(window, out w, out h);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetWindowSizeInPixels: Failed to retrieve window size in pixels.");
        }
        return result;
    }

    public static Rect GetWindowSizeInPixels(nint window) {
        if (window == nint.Zero) {
            return default;
        }
        bool result = SDL_GetWindowSizeInPixels(window, out int w, out int h);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetWindowSizeInPixels: Failed to retrieve window size in pixels.");
            return default;
        }
        return new Rect() { W = w, H = h };
    }

    public static nint GetWindowSurface(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowSurface: Window pointer is null.");
            return nint.Zero;
        }
        nint surface = SDL_GetWindowSurface(window);
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowSurface: Failed to retrieve window surface.");
        }
        return surface;
    }

    public static bool GetWindowSurfaceVSync(nint window, out int vsync) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowSurfaceVSync: Window pointer is null.");
            vsync = 0;
            return false;
        }
        bool result = SDL_GetWindowSurfaceVSync(window, out vsync);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetWindowSurfaceVSync: Failed to retrieve window surface VSync.");
        }
        return result;
    }

    public static int GetWindowSurfaceVSync(nint window) {
        if (window == nint.Zero) {
            return 0;
        }
        bool result = SDL_GetWindowSurfaceVSync(window, out int vsync);
        if (!result) {
            Logger.LogError(LogCategory.Error, "GetWindowSurfaceVSync: Failed to retrieve window surface VSync.");
            return 0;
        }
        return vsync;
    }

    public static string GetWindowTitle(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "GetWindowTitle: Window handle is null.");
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
            Logger.LogError(LogCategory.Error, "GuidToString: GUID is null.");
            return;
        }
        SDL_GUIDToString(guid, pszGuid, cbGuid);
    }

    public static bool HasClipboardData(string mimeType) {
        if (string.IsNullOrEmpty(mimeType)) {
            Logger.LogError(LogCategory.Error, "HasClipboardData: MIME type is null or empty.");
            return false;
        }
        bool result = SDL_HasClipboardData(mimeType);
        if (!result) {
            Logger.LogError(LogCategory.Error, "HasClipboardData: Failed to check clipboard data.");
        }
        return result;
    }

    public static bool HasClipboardText() {
        bool result = SDL_HasClipboardText();
        if (!result) {
            Logger.LogError(LogCategory.Error, "HasClipboardText: Failed to check clipboard text.");
        }
        return result;
    }

    public static bool HasKeyboard() {
        bool result = SDL_HasKeyboard();
        if (!result) {
            Logger.LogError(LogCategory.Error, "HasKeyboard: Failed to check keyboard.");
        }
        return result;
    }

    public static bool HasPrimarySelectionText() {
        bool result = SDL_HasPrimarySelectionText();
        if (!result) {
            Logger.LogError(LogCategory.Error, "HasPrimarySelectionText: Failed to check primary selection text.");
        }
        return result;
    }

    public static bool HasProperty(uint props, string name) {
        if (string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.Error, "HasProperty: Property name is null or empty.");
            return false;
        }
        bool result = SDL_HasProperty(props, name);
        if (!result) {
            Logger.LogError(LogCategory.Error, "HasProperty: Failed to check property.");
        }
        return result;
    }

    public static bool HasRectIntersection(ref Rect a, ref Rect b) {
        bool result = SDL_HasRectIntersection(ref a, ref b);
        if (!result) {
            Logger.LogError(LogCategory.Error, "HasRectIntersection: Failed to check rectangle intersection.");
        }
        return result;
    }

    public static bool HasRectIntersectionFloat(ref FRect a, ref FRect b) {
        bool result = SDL_HasRectIntersectionFloat(ref a, ref b);
        if (!result) {
            Logger.LogError(LogCategory.Error, "HasRectIntersectionFloat: Failed to check rectangle intersection.");
        }
        return result;
    }

    public static bool HasScreenKeyboardSupport() {
        bool result = SDL_HasScreenKeyboardSupport();
        if (!result) {
            Logger.LogError(LogCategory.Error, "HasScreenKeyboardSupport: Failed to check screen keyboard support.");
        }
        return result;
    }

    public static bool HideWindow(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "HideWindow: Window pointer is null.");
            return false;
        }
        bool result = SDL_HideWindow(window);
        if (!result) {
            Logger.LogError(LogCategory.Error, "HideWindow: Failed to hide window.");
        }
        return result;
    }

    public static bool Init(InitFlags flags) {
        if (!Enum.IsDefined(flags)) {
            Logger.LogError(LogCategory.Error, "Init: Invalid initialization flags.");
            return false;
        }

        bool result = SDL_Init(flags);
        if (!result) {
            Logger.LogError(LogCategory.Error, "Init: Failed to initialize SDL.");
        }
        return result;
    }

    public static bool InitSubSystem(InitFlags flags) {
        if (!Enum.IsDefined(flags)) {
            Logger.LogError(LogCategory.Error, "InitSubSystem: Invalid initialization flags.");
            return false;
        }
        bool result = SDL_InitSubSystem(flags);
        if (!result) {
            Logger.LogError(LogCategory.Error, "InitSubSystem: Failed to initialize SDL subsystem.");
        }
        return result;
    }

    public static bool IsMainThread() {
        bool result = SDL_IsMainThread();
        if (!result) {
            Logger.LogError(LogCategory.Error, "IsMainThread: Failed to check if current thread is main thread.");
        }
        return result;
    }

    public static nint LoadBmp(string file) {
        if (string.IsNullOrEmpty(file)) {
            Logger.LogError(LogCategory.Error, "LoadBmp: File path is null or empty.");
            return nint.Zero;
        }
        nint surface = SDL_LoadBMP(file);
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "LoadBmp: Failed to load BMP file.");
        }
        return surface;
    }

    public static nint LoadBmpIo(nint src, bool closeIo) {
        if (src == nint.Zero) {
            Logger.LogError(LogCategory.Error, "LoadBmpIo: Source pointer is null.");
            return nint.Zero;
        }
        nint surface = SDL_LoadBMP_IO(src, closeIo);
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "LoadBmpIo: Failed to load BMP from IO source.");
        }
        return surface;
    }

    public static nint LoadFunction(nint handle, string name) {
        if (handle == nint.Zero) {
            Logger.LogError(LogCategory.Error, "LoadFunction: Handle pointer is null.");
            return nint.Zero;
        }
        if (string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.Error, "LoadFunction: Function name is null or empty.");
            return nint.Zero;
        }
        nint function = SDL_LoadFunction(handle, name);
        if (function == nint.Zero) {
            Logger.LogError(LogCategory.Error, "LoadFunction: Failed to load function.");
        }
        return function;
    }

    public static nint LoadObject(string sofile) {
        if (string.IsNullOrEmpty(sofile)) {
            Logger.LogError(LogCategory.Error, "LoadObject: Shared object file path is null or empty.");
            return nint.Zero;
        }
        nint handle = SDL_LoadObject(sofile);
        if (handle == nint.Zero) {
            Logger.LogError(LogCategory.Error, "LoadObject: Failed to load shared object.");
        }
        return handle;
    }

    public static bool LockProperties(uint props) {
        if (props == 0) {
            Logger.LogError(LogCategory.Error, "LockProperties: Properties are zero.");
            return false;
        }
        bool result = SDL_LockProperties(props);
        if (!result) {
            Logger.LogError(LogCategory.Error, "LockProperties: Failed to lock properties.");
        }
        return result;
    }

    public static bool LockSurface(nint surface) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "LockSurface: Surface pointer is null.");
            return false;
        }
        bool result = SDL_LockSurface(surface);
        if (!result) {
            Logger.LogError(LogCategory.Error, "LockSurface: Failed to lock surface.");
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
            Logger.LogError(LogCategory.Error, "Malloc: Memory allocation failed.");
            SDL_OutOfMemory();
            return nint.Zero;
        }

        return res;
    }

    public static uint MapRgb(nint format, nint palette, byte r, byte g, byte b) {
        if (format == nint.Zero || palette == nint.Zero) {
            Logger.LogError(LogCategory.Error, "MapRgb: Format or palette pointer is null.");
            return 0;
        }
        uint color = SDL_MapRGB(format, palette, r, g, b);
        if (color == 0) {
            Logger.LogError(LogCategory.Error, "MapRgb: Failed to map RGB color.");
        }
        return color;
    }

    public static uint MapRgba(nint format, nint palette, byte r, byte g, byte b, byte a) {
        if (format == nint.Zero || palette == nint.Zero) {
            Logger.LogError(LogCategory.Error, "MapRgba: Format or palette pointer is null.");
            return 0;
        }
        uint color = SDL_MapRGBA(format, palette, r, g, b, a);
        if (color == 0) {
            Logger.LogError(LogCategory.Error, "MapRgba: Failed to map RGBA color.");
        }
        return color;
    }

    public static uint MapSurfaceRgb(nint surface, byte r, byte g, byte b) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "MapSurfaceRgb: Surface pointer is null.");
            return 0;
        }
        uint color = SDL_MapSurfaceRGB(surface, r, g, b);
        if (color == 0) {
            Logger.LogError(LogCategory.Error, "MapSurfaceRgb: Failed to map surface RGB color.");
        }
        return color;
    }

    public static uint MapSurfaceRgb(nint surface, Color color) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "MapSurfaceRgb: Surface pointer is null.");
            return 0;
        }

        uint colorValue = SDL_MapSurfaceRGB(surface, color.R, color.G, color.B);
        if (colorValue == 0) {
            Logger.LogError(LogCategory.Error, "MapSurfaceRgb: Failed to map surface RGB color.");
        }
        return colorValue;
    }

    public static uint MapSurfaceRgba(nint surface, byte r, byte g, byte b, byte a) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "MapSurfaceRgba: Surface pointer is null.");
            return 0;
        }
        uint color = SDL_MapSurfaceRGBA(surface, r, g, b, a);
        if (color == 0) {
            Logger.LogError(LogCategory.Error, "MapSurfaceRgba: Failed to map surface RGBA color.");
        }
        return color;
    }

    public static uint MapSurfaceRgba(nint surface, Color color) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "MapSurfaceRgba: Surface pointer is null.");
            return 0;
        }
        uint colorValue = SDL_MapSurfaceRGBA(surface, color.R, color.G, color.B, color.A);
        if (colorValue == 0) {
            Logger.LogError(LogCategory.Error, "MapSurfaceRgba: Failed to map surface RGBA color.");
        }
        return colorValue;
    }

    public static bool MaximizeWindow(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "MaximizeWindow: Window pointer is null.");
            return false;
        }
        bool result = SDL_MaximizeWindow(window);
        if (!result) {
            Logger.LogError(LogCategory.Error, "MaximizeWindow: Failed to maximize window.");
        }
        return result;
    }

    public static bool MinimizeWindow(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "MinimizeWindow: Window pointer is null.");
            return false;
        }
        bool result = SDL_MinimizeWindow(window);
        if (!result) {
            Logger.LogError(LogCategory.Error, "MinimizeWindow: Failed to minimize window.");
        }
        return result;
    }

    public static bool OutOfMemory() {
        return SDL_OutOfMemory();
    }

    public static bool PremultiplyAlpha(int width, int height, PixelFormat srcFormat, nint src,
            int srcPitch, PixelFormat dstFormat, nint dst, int dstPitch, bool linear) {
        if (width <= 0 || height <= 0) {
            Logger.LogError(LogCategory.Error, "PremultiplyAlpha: Invalid width or height.");
            return false;
        }
        bool result = SDL_PremultiplyAlpha(width, height, srcFormat, src, srcPitch, dstFormat, dst, dstPitch,
                linear);
        if (!result) {
            Logger.LogError(LogCategory.Error, "PremultiplyAlpha: Failed to premultiply alpha.");
        }
        return result;
    }

    public static bool PremultiplyAlpha(Rect rect, PixelFormat srcFormat, nint src,
            int srcPitch, PixelFormat dstFormat, nint dst, int dstPitch, bool linear) {
        if (rect.W <= 0 || rect.H <= 0) {
            Logger.LogError(LogCategory.Error, "PremultiplyAlpha: Invalid rectangle dimensions.");
            return false;
        }
        bool result = SDL_PremultiplyAlpha(rect.W, rect.H, srcFormat, src, srcPitch, dstFormat, dst, dstPitch,
                linear);
        if (!result) {
            Logger.LogError(LogCategory.Error, "PremultiplyAlpha: Failed to premultiply alpha.");
        }
        return result;
    }

    public static bool PremultiplySurfaceAlpha(nint surface, bool linear) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "PremultiplySurfaceAlpha: Surface pointer is null.");
            return false;
        }
        bool result = SDL_PremultiplySurfaceAlpha(surface, linear);
        if (!result) {
            Logger.LogError(LogCategory.Error, "PremultiplySurfaceAlpha: Failed to premultiply surface alpha.");
        }
        return result;
    }

    public static void Quit() {
        SDL_Quit();
    }

    public static void QuitSubSystem(InitFlags flags) {
        if (!Enum.IsDefined(flags)) {
            Logger.LogError(LogCategory.Error, "QuitSubSystem: Invalid initialization flags.");
            return;
        }
        SDL_QuitSubSystem(flags);
    }

    public static bool RaiseWindow(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "RaiseWindow: Window pointer is null.");
            return false;
        }
        bool result = SDL_RaiseWindow(window);
        if (!result) {
            Logger.LogError(LogCategory.Error, "RaiseWindow: Failed to raise window.");
        }
        return result;
    }

    public static bool ReadSurfacePixel(nint surface, int x, int y, out byte r, out byte g, out byte b, out byte a) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "ReadSurfacePixel: Surface pointer is null.");
            r = g = b = a = 0;
            return false;
        }
        bool result = SDL_ReadSurfacePixel(surface, x, y, out r, out g, out b, out a);
        if (!result) {
            Logger.LogError(LogCategory.Error, "ReadSurfacePixel: Failed to read surface pixel.");
        }
        return result;
    }

    public static bool ReadSurfacePixel(nint surface, int x, int y, out Color color) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "ReadSurfacePixel: Surface pointer is null.");
            color = default;
            return false;
        }
        bool result = SDL_ReadSurfacePixel(surface, x, y, out byte r, out byte g, out byte b, out byte a);
        if (!result) {
            Logger.LogError(LogCategory.Error, "ReadSurfacePixel: Failed to read surface pixel.");
            color = default;
            return false;
        }
        color = new Color() { R = r, G = g, B = b, A = a };
        return true;
    }

    public static Color ReadSurfacePixel(nint surface, int x, int y) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "ReadSurfacePixel: Surface pointer is null.");
            return default;
        }
        bool result = SDL_ReadSurfacePixel(surface, x, y, out byte r, out byte g, out byte b, out byte a);
        if (!result) {
            Logger.LogError(LogCategory.Error, "ReadSurfacePixel: Failed to read surface pixel.");
            return default;
        }
        return new Color() { R = r, G = g, B = b, A = a };
    }

    public static bool ReadSurfacePixelFloat(nint surface, int x, int y, out float r, out float g, out float b, out float a) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "ReadSurfacePixelFloat: Surface pointer is null.");
            r = g = b = a = 0;
            return false;
        }
        bool result = SDL_ReadSurfacePixelFloat(surface, x, y, out r, out g, out b, out a);
        if (!result) {
            Logger.LogError(LogCategory.Error, "ReadSurfacePixelFloat: Failed to read surface pixel.");
        }
        return result;
    }

    public static bool ReadSurfacePixelFloat(nint surface, int x, int y, out FColor color) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "ReadSurfacePixelFloat: Surface pointer is null.");
            color = default;
            return false;
        }
        bool result = SDL_ReadSurfacePixelFloat(surface, x, y, out float r, out float g, out float b, out float a);
        if (!result) {
            Logger.LogError(LogCategory.Error, "ReadSurfacePixelFloat: Failed to read surface pixel.");
            color = default;
            return false;
        }
        color = new FColor() { R = r, G = g, B = b, A = a };
        return true;
    }

    public static FColor ReadSurfacePixelFloat(nint surface, int x, int y) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "ReadSurfacePixelFloat: Surface pointer is null.");
            return default;
        }
        bool result = SDL_ReadSurfacePixelFloat(surface, x, y, out float r, out float g, out float b, out float a);
        if (!result) {
            Logger.LogError(LogCategory.Error, "ReadSurfacePixelFloat: Failed to read surface pixel.");
            return default;
        }
        return new FColor() { R = r, G = g, B = b, A = a };
    }

    public static void RemoveHintCallback(string name, SdlHintCallback callback, nint userdata) {
        if (string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.Error, "RemoveHintCallback: Hint name is null or empty.");
            return;
        }
        SDL_RemoveHintCallback(name, callback, userdata);
    }

    public static void RemoveSurfaceAlternateImages(nint surface) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "RemoveSurfaceAlternateImages: Surface pointer is null.");
            return;
        }
        SDL_RemoveSurfaceAlternateImages(surface);
    }

    public static bool ResetHint(string name) {
        if (string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.Error, "ResetHint: Hint name is null or empty.");
            return false;
        }
        bool result = SDL_ResetHint(name);
        if (!result) {
            Logger.LogError(LogCategory.Error, "ResetHint: Failed to reset hint.");
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
            Logger.LogError(LogCategory.Error, "RestoreWindow: Window pointer is null.");
            return false;
        }
        bool result = SDL_RestoreWindow(window);
        if (!result) {
            Logger.LogError(LogCategory.Error, "RestoreWindow: Failed to restore window.");
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
            Logger.LogError(LogCategory.Error, "RunOnMainThread: Callback is null.");
            return false;
        }
        bool result = SDL_RunOnMainThread(callback, userdata, waitComplete);
        if (!result) {
            Logger.LogError(LogCategory.Error, "RunOnMainThread: Failed to run on main thread.");
        }
        return result;
    }

    public static bool SaveBmp(nint surface, string file) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SaveBmp: Surface pointer is null.");
            return false;
        }
        if (string.IsNullOrEmpty(file)) {
            Logger.LogError(LogCategory.Error, "SaveBmp: File path is null or empty.");
            return false;
        }
        bool result = SDL_SaveBMP(surface, file);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SaveBmp: Failed to save BMP file.");
        }
        return result;
    }

    public static bool SaveBmpIp(nint surface, nint dst, bool closeIo) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SaveBmpIp: Surface pointer is null.");
            return false;
        }
        if (dst == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SaveBmpIp: Destination pointer is null.");
            return false;
        }
        bool result = SDL_SaveBMP_IO(surface, dst, closeIo);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SaveBmpIp: Failed to save BMP to IO destination.");
        }
        return result;
    }

    public static nint ScaleSurface(nint surface, int width, int height, ScaleMode scaleMode) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "ScaleSurface: Surface pointer is null.");
            return nint.Zero;
        }

        if (!Enum.IsDefined(scaleMode)) {
            Logger.LogError(LogCategory.Error, "ScaleSurface: Invalid scale mode.");
            return nint.Zero;
        }

        if (width <= 0 || height <= 0) {
            Logger.LogError(LogCategory.Error, "ScaleSurface: Invalid width or height.");
            return nint.Zero;
        }
        nint scaledSurface = SDL_ScaleSurface(surface, width, height, scaleMode);
        if (scaledSurface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "ScaleSurface: Failed to scale surface.");
        }
        return scaledSurface;
    }

    public static bool ScreenKeyboardShown(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "ScreenKeyboardShown: Window pointer is null.");
            return false;
        }
        bool result = SDL_ScreenKeyboardShown(window);
        if (!result) {
            Logger.LogError(LogCategory.Error, "ScreenKeyboardShown: Failed to check screen keyboard visibility.");
        }
        return result;
    }

    public static bool ScreenSaverEnabled() {
        bool result = SDL_ScreenSaverEnabled();
        if (!result) {
            Logger.LogError(LogCategory.Error, "ScreenSaverEnabled: Failed to check screen saver status.");
        }
        return result;
    }

    public static bool SetAppMetadata(string appname, string appversion, string appidentifier) {
        if (string.IsNullOrEmpty(appname) || string.IsNullOrEmpty(appversion) || string.IsNullOrEmpty(appidentifier)) {
            Logger.LogError(LogCategory.Error, "SetAppMetadata: App metadata is null or empty.");
            return false;
        }
        bool result = SDL_SetAppMetadata(appname, appversion, appidentifier);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetAppMetadata: Failed to set app metadata.");
        }
        return result;
    }

    public static bool SetAppMetadataProperty(string name, string value) {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value)) {
            Logger.LogError(LogCategory.Error, "SetAppMetadataProperty: Name or value is null or empty.");
            return false;
        }
        bool result = SDL_SetAppMetadataProperty(name, value);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetAppMetadataProperty: Failed to set app metadata property.");
        }
        return result;
    }

    public static bool SetBooleanProperty(uint props, string name, bool value) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.Error, "SetBooleanProperty: Properties are zero or name is null/empty.");
            return false;
        }
        bool result = SDL_SetBooleanProperty(props, name, value);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetBooleanProperty: Failed to set boolean property.");
        }
        return result;
    }

    public static bool SetClipboardData(SdlClipboardDataCallback callback,
            SdlClipboardCleanupCallback cleanup, nint userdata, nint mimeTypes, nuint numMimeTypes) {
        if (callback == null || cleanup == null || userdata == nint.Zero || mimeTypes == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetClipboardData: Invalid parameters.");
            return false;
        }
        bool result = SDL_SetClipboardData(callback, cleanup, userdata, mimeTypes, numMimeTypes);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetClipboardData: Failed to set clipboard data.");
        }
        return result;
    }

    public static bool SetClipboardText(string text) {
        if (string.IsNullOrEmpty(text)) {
            Logger.LogError(LogCategory.Error, "SetClipboardText: Text is null or empty.");
            return false;
        }
        bool result = SDL_SetClipboardText(text);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetClipboardText: Failed to set clipboard text.");
        }
        return result;
    }

    public static bool SetCurrentThreadPriority(ThreadPriority priority) {
        if (!Enum.IsDefined(priority)) {
            Logger.LogError(LogCategory.Error, "SetCurrentThreadPriority: Invalid thread priority.");
            return false;
        }
        bool result = SDL_SetCurrentThreadPriority(priority);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetCurrentThreadPriority: Failed to set thread priority.");
        }
        return result;
    }

    public static bool SetError(string fmt, params object[] args) {
        if (string.IsNullOrEmpty(fmt)) {
            Logger.LogWarn(LogCategory.System, "SetError: Format string is null or empty.");
            return false;
        }

        string formatted = args.Length > 0 ? string.Format(fmt, args) : fmt;
        return SDL_SetError(formatted);
    }

    public static bool SetFloatProperty(uint props, string name, float value) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.Error, "SetFloatProperty: Properties are zero or name is null/empty.");
            return false;
        }
        bool result = SDL_SetFloatProperty(props, name, value);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetFloatProperty: Failed to set float property.");
        }
        return result;
    }

    public static bool SetHint(string name, string value) {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value)) {
            Logger.LogError(LogCategory.Error, "SetHint: Name or value is null or empty.");
            return false;
        }
        bool result = SDL_SetHint(name, value);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetHint: Failed to set hint.");
        }
        return result;
    }

    public static bool SetHintWithPriority(string name, string value, HintPriority priority) {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value)) {
            Logger.LogError(LogCategory.Error, "SetHintWithPriority: Name or value is null or empty.");
            return false;
        }
        bool result = SDL_SetHintWithPriority(name, value, priority);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetHintWithPriority: Failed to set hint with priority.");
        }
        return result;
    }

    public static void SetMainReady() {
        SDL_SetMainReady();
    }

    public static void SetModState(KeyMod modstate) {
        if (!Enum.IsDefined(modstate)) {
            Logger.LogError(LogCategory.Error, "SetModState: Invalid key modifier state.");
            return;
        }
        SDL_SetModState(modstate);
    }

    public static bool SetNumberProperty(uint props, string name, long value) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.Error, "SetNumberProperty: Properties are zero or name is null/empty.");
            return false;
        }
        bool result = SDL_SetNumberProperty(props, name, value);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetNumberProperty: Failed to set number property.");
        }
        return result;
    }

    public static bool SetPaletteColors(nint palette, Span<Color> colors, int firstcolor, int ncolors) {
        if (palette == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetPaletteColors: Palette pointer is null.");
            return false;
        }
        if (firstcolor < 0 || ncolors <= 0) {
            Logger.LogError(LogCategory.Error, "SetPaletteColors: Invalid first color or number of colors.");
            return false;
        }
        bool result = SDL_SetPaletteColors(palette, colors, firstcolor, ncolors);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetPaletteColors: Failed to set palette colors.");
        }
        return result;
    }

    public static bool SetPointerProperty(uint props, string name, nint value) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.Error, "SetPointerProperty: Properties are zero or name is null/empty.");
            return false;
        }
        bool result = SDL_SetPointerProperty(props, name, value);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetPointerProperty: Failed to set pointer property.");
        }
        return result;
    }

    public static bool SetPointerPropertyWithCleanup(uint props, string name, nint value,
        SdlCleanupPropertyCallback cleanup, nint userdata) {
        if (props == 0 || string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.Error, "SetPointerPropertyWithCleanup: Properties are zero or name is null/empty.");
            return false;
        }
        bool result = SDL_SetPointerPropertyWithCleanup(props, name, value, cleanup, userdata);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetPointerPropertyWithCleanup: Failed to set pointer property with cleanup.");
        }
        return result;
    }

    public static bool SetPrimarySelectionText(string text) {
        if (string.IsNullOrEmpty(text)) {
            Logger.LogError(LogCategory.Error, "SetPrimarySelectionText: Text is null or empty.");
            return false;
        }
        bool result = SDL_SetPrimarySelectionText(text);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetPrimarySelectionText: Failed to set primary selection text.");
        }
        return result;
    }

    public static bool SetScancodeName(ScanCode scanCode, string name) {
        if (string.IsNullOrEmpty(name)) {
            Logger.LogError(LogCategory.Error, "SetScancodeName: Name is null or empty.");
            return false;
        }
        bool result = SDL_SetScancodeName(scanCode, name);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetScancodeName: Failed to set scancode name.");
        }
        return result;
    }

    public static bool SetStringProperty(uint props, string name, string value) {
        if (props == 0 || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value)) {
            Logger.LogError(LogCategory.Error, "SetStringProperty: Properties are zero or name/value is null/empty.");
            return false;
        }
        bool result = SDL_SetStringProperty(props, name, value);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetStringProperty: Failed to set string property.");
        }
        return result;
    }

    public static bool SetSurfaceAlphaMod(nint surface, byte alpha) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetSurfaceAlphaMod: Surface pointer is null.");
            return false;
        }
        bool result = SDL_SetSurfaceAlphaMod(surface, alpha);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetSurfaceAlphaMod: Failed to set surface alpha mod.");
        }
        return result;
    }

    public static bool SetSurfaceBlendMode(nint surface, uint blendMode) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetSurfaceBlendMode: Surface pointer is null.");
            return false;
        }
        bool result = SDL_SetSurfaceBlendMode(surface, blendMode);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetSurfaceBlendMode: Failed to set surface blend mode.");
        }
        return result;
    }

    public static bool SetSurfaceClipRect(nint surface, ref Rect rect) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetSurfaceClipRect: Surface pointer is null.");
            return false;
        }
        bool result = SDL_SetSurfaceClipRect(surface, ref rect);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetSurfaceClipRect: Failed to set surface clip rect.");
        }
        return result;
    }

    public static bool SetSurfaceColorKey(nint surface, bool enabled, uint key) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetSurfaceColorKey: Surface pointer is null.");
            return false;
        }
        bool result = SDL_SetSurfaceColorKey(surface, enabled, key);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetSurfaceColorKey: Failed to set surface color key.");
        }
        return result;
    }

    public static bool SetSurfaceColorMod(nint surface, byte r, byte g, byte b) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetSurfaceColorMod: Surface pointer is null.");
            return false;
        }
        bool result = SDL_SetSurfaceColorMod(surface, r, g, b);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetSurfaceColorMod: Failed to set surface color mod.");
        }
        return result;
    }

    public static bool SetSurfaceColorMod(nint surface, Color color) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetSurfaceColorMod: Surface pointer is null.");
            return false;
        }
        bool result = SDL_SetSurfaceColorMod(surface, color.R, color.G, color.B);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetSurfaceColorMod: Failed to set surface color mod.");
        }
        return result;
    }

    public static bool SetSurfaceColorspace(nint surface, Colorspace colorspace) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetSurfaceColorspace: Surface pointer is null.");
            return false;
        }
        bool result = SDL_SetSurfaceColorspace(surface, colorspace);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetSurfaceColorspace: Failed to set surface colorspace.");
        }
        return result;
    }

    public static bool SetSurfacePalette(nint surface, nint palette) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetSurfacePalette: Surface pointer is null.");
            return false;
        }
        if (palette == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetSurfacePalette: Palette pointer is null.");
            return false;
        }
        bool result = SDL_SetSurfacePalette(surface, palette);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetSurfacePalette: Failed to set surface palette.");
        }
        return result;
    }

    public static bool SetSurfaceRLE(nint surface, bool enabled) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetSurfaceRLE: Surface pointer is null.");
            return false;
        }
        bool result = SDL_SetSurfaceRLE(surface, enabled);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetSurfaceRLE: Failed to set surface RLE.");
        }
        return result;
    }

    public static bool SetTextInputArea(nint window, ref Rect rect, int cursor) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetTextInputArea: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetTextInputArea(window, ref rect, cursor);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetTextInputArea: Failed to set text input area.");
        }
        return result;
    }

    public static bool SetTls(nint id, nint value, SdlTlsDestructorCallback destructor) {
        if (id == nint.Zero || value == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetTls: ID or value is null.");
            return false;
        }
        bool result = SDL_SetTLS(id, value, destructor);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetTls: Failed to set TLS.");
        }
        return result;
    }

    public static bool SetWindowAlwaysOnTop(nint window, bool onTop) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowAlwaysOnTop: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowAlwaysOnTop(window, onTop);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetWindowAlwaysOnTop: Failed to set window always on top.");
        }
        return result;
    }

    public static bool SetWindowAspectRatio(nint window, float minAspect, float maxAspect) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowAspectRatio: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowAspectRatio(window, minAspect, maxAspect);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetWindowAspectRatio: Failed to set window aspect ratio.");
        }
        return result;
    }

    public static bool SetWindowBordered(nint window, bool bordered) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowBordered: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowBordered(window, bordered);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetWindowBordered: Failed to set window bordered.");
        }
        return result;
    }

    public static bool SetWindowFocusable(nint window, bool focusable) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowFocusable: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowFocusable(window, focusable);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetWindowFocusable: Failed to set window focusable.");
        }
        return result;
    }

    public static bool SetWindowFullscreen(nint window, bool fullscreen) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowFullscreen: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowFullscreen(window, fullscreen);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetWindowFullscreen: Failed to set window fullscreen.");
        }
        return result;
    }

    public static bool SetWindowFullscreenMode(nint window, ref DisplayMode mode) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowFullscreenMode: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowFullscreenMode(window, ref mode);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetWindowFullscreenMode: Failed to set window fullscreen mode.");
        }
        return result;
    }

    public static bool SetWindowHitTest(nint window, SdlHitTest callback, nint callbackData) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowHitTest: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowHitTest(window, callback, callbackData);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetWindowHitTest: Failed to set window hit test.");
        }
        return result;
    }

    public static bool SetWindowIcon(nint window, nint icon) {
        // Impement an overloaded function that acceps an Icon from LoadIcon
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowIcon: Window pointer is null.");
            return false;
        }
        if (icon == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowIcon: Icon pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowIcon(window, icon);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetWindowIcon: Failed to set window icon.");
        }
        return result;
    }

    public static bool SetWindowKeyboardGrab(nint window, bool grabbed) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowKeyboardGrab: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowKeyboardGrab(window, grabbed);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetWindowKeyboardGrab: Failed to set window keyboard grab.");
        }
        return result;
    }

    public static bool SetWindowMaximumSize(nint window, int maxW, int maxH) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowMaximumSize: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowMaximumSize(window, maxW, maxH);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetWindowMaximumSize: Failed to set window maximum size.");
        }
        return result;
    }

    public static bool SetWindowMinimumSize(nint window, int minW, int minH) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowMinimumSize: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowMinimumSize(window, minW, minH);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetWindowMinimumSize: Failed to set window minimum size.");
        }
        return result;
    }

    public static bool SetWindowModal(nint window, bool modal) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowModal: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowModal(window, modal);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetWindowModal: Failed to set window modal.");
        }
        return result;
    }

    public static bool SetWindowMouseGrab(nint window, bool grabbed) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowMouseGrab: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowMouseGrab(window, grabbed);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetWindowMouseGrab: Failed to set window mouse grab.");
        }
        return result;
    }

    public static bool SetWindowMouseRect(nint window, ref Rect rect) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowMouseRect: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowMouseRect(window, ref rect);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetWindowMouseRect: Failed to set window mouse rect.");
        }
        return result;
    }

    public static bool SetWindowOpacity(nint window, float opacity) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowOpacity: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowOpacity(window, opacity);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetWindowOpacity: Failed to set window opacity.");
        }
        return result;
    }

    public static bool SetWindowParent(nint window, nint parent) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowParent: Window pointer is null.");
            return false;
        }
        if (parent == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowParent: Parent pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowParent(window, parent);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetWindowParent: Failed to set window parent.");
        }
        return result;
    }

    public static bool SetWindowPosition(nint window, int x, int y) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowPosition: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowPosition(window, x, y);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetWindowPosition: Failed to set window position.");
        }
        return result;
    }

    public static bool SetWindowPosition(nint window, Point position) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowPosition: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowPosition(window, position.X, position.Y);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetWindowPosition: Failed to set window position.");
        }
        return result;
    }

    public static bool SetWindowResizable(nint window, bool resizable) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowResizable: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowResizable(window, resizable);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetWindowResizable: Failed to set window resizable.");
        }
        return result;
    }

    public static bool SetWindowShape(nint window, nint shape) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowShape: Window pointer is null.");
            return false;
        }
        if (shape == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowShape: Shape pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowShape(window, shape);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetWindowShape: Failed to set window shape.");
        }
        return result;
    }

    public static bool SetWindowSize(nint window, int w, int h) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowSize: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowSize(window, w, h);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetWindowSize: Failed to set window size.");
        }
        return result;
    }

    public static bool SetWindowSize(nint window, Rect rect) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowSize: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowSize(window, rect.W, rect.H);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetWindowSize: Failed to set window size.");
        }
        return result;
    }

    public static bool SetWindowSurfaceVSync(nint window, int vsync) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowSurfaceVSync: Window pointer is null.");
            return false;
        }
        bool result = SDL_SetWindowSurfaceVSync(window, vsync);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SetWindowSurfaceVSync: Failed to set window surface VSync.");
        }
        return result;
    }

    public static bool SetWindowTitle(nint window, string title) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SetWindowTitle: Window handle is null.");
            return false;
        }
        bool result = SDL_SetWindowTitle(window, title);
        if (!result) {
            Logger.LogWarn(LogCategory.System, "SetWindowTitle: Failed to set window title.");
        }
        return result;
    }

    public static bool ShowWindow(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "ShowWindow: Window pointer is null.");
            return false;
        }
        bool result = SDL_ShowWindow(window);
        if (!result) {
            Logger.LogError(LogCategory.Error, "ShowWindow: Failed to show window.");
        }
        return result;
    }

    public static bool ShowWindowSystemMenu(nint window, int x, int y) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "ShowWindowSystemMenu: Window pointer is null.");
            return false;
        }
        bool result = SDL_ShowWindowSystemMenu(window, x, y);
        if (!result) {
            Logger.LogError(LogCategory.Error, "ShowWindowSystemMenu: Failed to show window system menu.");
        }
        return result;
    }

    public static bool ShowWindowSystemMenu(nint window, Point position) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "ShowWindowSystemMenu: Window pointer is null.");
            return false;
        }
        return ShowWindowSystemMenu(window, position.X, position.Y);
    }

    public static unsafe nuint SizeOf<T>() where T : unmanaged {
        nuint size = (uint)sizeof(T);
        if (size == 0) {
            Logger.LogError(LogCategory.Error, "Sizeof: Failed to get size of type.");
        }
        return size;
    }

    public static bool StartTextInput(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "StartTextInput: Window pointer is null.");
            return false;
        }
        bool result = SDL_StartTextInput(window);
        if (!result) {
            Logger.LogError(LogCategory.Error, "StartTextInput: Failed to start text input.");
        }
        return result;
    }

    public static bool StartTextInputWithProperties(nint window, uint props) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "StartTextInputWithProperties: Window pointer is null.");
            return false;
        }
        bool result = SDL_StartTextInputWithProperties(window, props);
        if (!result) {
            Logger.LogError(LogCategory.Error, "StartTextInputWithProperties: Failed to start text input with properties.");
        }
        return result;
    }

    public static bool StopTextInput(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "StopTextInput: Window pointer is null.");
            return false;
        }
        bool result = SDL_StopTextInput(window);
        if (!result) {
            Logger.LogError(LogCategory.Error, "StopTextInput: Failed to stop text input.");
        }
        return result;
    }

    public static SdlGuid StringToGUID(string pchGuid) {
        if (string.IsNullOrEmpty(pchGuid)) {
            Logger.LogError(LogCategory.Error, "StringToGUID: GUID string is null or empty.");
            return default;
        }
        SdlGuid result = SDL_StringToGUID(pchGuid);
        if (result.Data == null) {
            Logger.LogError(LogCategory.Error, "StringToGUID: Failed to convert string to GUID.");
        }
        return result;
    }

    public static bool SurfaceHasAlternateImages(nint surface) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SurfaceHasAlternateImages: Surface pointer is null.");
            return false;
        }
        bool result = SDL_SurfaceHasAlternateImages(surface);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SurfaceHasAlternateImages: Failed to check surface alternate images.");
        }
        return result;
    }

    public static bool SurfaceHasColorKey(nint surface) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SurfaceHasColorKey: Surface pointer is null.");
            return false;
        }
        bool result = SDL_SurfaceHasColorKey(surface);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SurfaceHasColorKey: Failed to check surface color key.");
        }
        return result;
    }

    public static bool SurfaceHasRLE(nint surface) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SurfaceHasRLE: Surface pointer is null.");
            return false;
        }
        bool result = SDL_SurfaceHasRLE(surface);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SurfaceHasRLE: Failed to check surface RLE.");
        }
        return result;
    }

    public static bool SyncWindow(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "SyncWindow: Window pointer is null.");
            return false;
        }
        bool result = SDL_SyncWindow(window);
        if (!result) {
            Logger.LogError(LogCategory.Error, "SyncWindow: Failed to sync window.");
        }
        return result;
    }

    public static bool TextInputActive(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "TextInputActive: Window pointer is null.");
            return false;
        }
        bool result = SDL_TextInputActive(window);
        if (!result) {
            Logger.LogError(LogCategory.Error, "TextInputActive: Failed to check text input active.");
        }
        return result;
    }

    public static void UnloadObject(nint handle) {
        if (handle == nint.Zero) {
            Logger.LogError(LogCategory.Error, "UnloadObject: Handle pointer is null.");
            return;
        }
        SDL_UnloadObject(handle);
    }

    public static void UnlockProperties(uint props) {
        if (props == 0) {
            Logger.LogError(LogCategory.Error, "UnlockProperties: Properties are zero.");
            return;
        }
        SDL_UnlockProperties(props);
    }

    public static void UnlockSurface(nint surface) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "UnlockSurface: Surface pointer is null.");
            return;
        }
        SDL_UnlockSurface(surface);
    }

    public static bool UpdateWindowSurface(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "UpdateWindowSurface: Window pointer is null.");
            return false;
        }
        bool result = SDL_UpdateWindowSurface(window);
        if (!result) {
            Logger.LogError(LogCategory.Error, "UpdateWindowSurface: Failed to update window surface.");
        }
        return result;
    }

    public static bool UpdateWindowSurfaceRects(nint window, Span<Rect> rects, int numrects) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "UpdateWindowSurfaceRects: Window pointer is null.");
            return false;
        }
        if (rects.Length == 0 || numrects <= 0) {
            Logger.LogError(LogCategory.Error, "UpdateWindowSurfaceRects: Rectangles are empty or number of rectangles is zero.");
            return false;
        }
        bool result = SDL_UpdateWindowSurfaceRects(window, rects, numrects);
        if (!result) {
            Logger.LogError(LogCategory.Error, "UpdateWindowSurfaceRects: Failed to update window surface rectangles.");
        }
        return result;
    }

    public static bool UpdateWindowSurfaceRects(nint window, Rect[] rects) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "UpdateWindowSurfaceRects: Window pointer is null.");
            return false;
        }
        if (rects.Length == 0) {
            Logger.LogError(LogCategory.Error, "UpdateWindowSurfaceRects: Rectangles are empty.");
            return false;
        }
        bool result = SDL_UpdateWindowSurfaceRects(window, rects, rects.Length);
        if (!result) {
            Logger.LogError(LogCategory.Error, "UpdateWindowSurfaceRects: Failed to update window surface rectangles.");
        }
        return result;
    }

    public static void WaitThread(nint thread, nint status) {
        if (thread == nint.Zero) {
            Logger.LogError(LogCategory.Error, "WaitThread: Thread pointer is null.");
            return;
        }
        SDL_WaitThread(thread, status);
    }

    public static InitFlags WasInit(InitFlags flags) {
        if (!Enum.IsDefined(flags)) {
            Logger.LogError(LogCategory.Error, "WasInit: Flags are not defined.");
            return 0;
        }
        if (flags == 0) {
            Logger.LogError(LogCategory.Error, "WasInit: Flags are zero.");
            return 0;
        }
        InitFlags result = SDL_WasInit(flags);
        if (result == 0) {
            Logger.LogError(LogCategory.Error, "WasInit: Failed to check SDL initialization.");
        }
        return result;
    }

    public static bool WindowHasSurface(nint window) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "WindowHasSurface: Window pointer is null.");
            return false;
        }
        bool result = SDL_WindowHasSurface(window);
        if (!result) {
            Logger.LogError(LogCategory.Error, "WindowHasSurface: Failed to check window surface.");
        }
        return result;
    }

    public static bool WriteSurfacePixel(nint surface, int x, int y, byte r, byte g, byte b, byte a) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "WriteSurfacePixel: Surface pointer is null.");
            return false;
        }
        bool result = SDL_WriteSurfacePixel(surface, x, y, r, g, b, a);
        if (!result) {
            Logger.LogError(LogCategory.Error, "WriteSurfacePixel: Failed to write surface pixel.");
        }
        return result;
    }

    public static bool WriteSurfacePixel(nint surface, int x, int y, Color color) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "WriteSurfacePixel: Surface pointer is null.");
            return false;
        }
        bool result = SDL_WriteSurfacePixel(surface, x, y, color.R, color.G, color.B, color.A);
        if (!result) {
            Logger.LogError(LogCategory.Error, "WriteSurfacePixel: Failed to write surface pixel.");
        }
        return result;
    }

    public static bool WriteSurfacePixel(nint surface, Point location, Color color) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "WriteSurfacePixel: Surface pointer is null.");
            return false;
        }
        return WriteSurfacePixel(surface, location.X, location.Y, color.R, color.G, color.B, color.A);
    }

    public static bool WriteSurfacePixel(nint surface, Point location, byte r, byte g, byte b, byte a) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "WriteSurfacePixel: Surface pointer is null.");
            return false;
        }
        return WriteSurfacePixel(surface, location.X, location.Y, r, g, b, a);
    }

    public static bool WriteSurfacePixelFloat(nint surface, int x, int y, float r, float g, float b, float a) {
        if (surface == nint.Zero) {
            Logger.LogError(LogCategory.Error, "WriteSurfacePixelFloat: Surface pointer is null.");
            return false;
        }
        bool result = SDL_WriteSurfacePixelFloat(surface, x, y, r, g, b, a);
        if (!result) {
            Logger.LogError(LogCategory.Error, "WriteSurfacePixelFloat: Failed to write surface pixel float.");
        }
        return result;
    }

    public static bool WriteSurfacePixelFloat(nint window, Point location, FColor color) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "WriteSurfacePixelFloat: Window pointer is null.");
            return false;
        }
        return WriteSurfacePixelFloat(window, location.X, location.Y, color.R, color.G, color.B,
            color.A);
    }

    public static bool WriteSurfacePixelFloat(nint window, Point location, float r, float g, float b, float a) {
        if (window == nint.Zero) {
            Logger.LogError(LogCategory.Error, "WriteSurfacePixelFloat: Window pointer is null.");
            return false;
        }
        return WriteSurfacePixelFloat(window, location.X, location.Y, r, g, b, a);
    }

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
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

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
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
    private static partial nint SDL_ConvertSurface(nint surface, PixelFormat format);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_ConvertSurfaceAndColorspace(nint surface, PixelFormat format,
        nint palette, Colorspace colorspace, uint props);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CopyProperties(uint src, uint dst);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreatePalette(int ncolors);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreatePopupWindow(nint parent, int offsetX, int offsetY, int w, int h,
        WindowFlags flags);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_CreateProperties();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateSurface(int width, int height, PixelFormat format);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateSurfaceFrom(int width, int height, PixelFormat format, nint pixels,
        int pitch);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateSurfacePalette(nint surface);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateThreadRuntime(SdlThreadFunction fn, string name, nint data,
        nint pfnBeginThread, nint pfnEndThread);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateThreadWithPropertiesRuntime(uint props, nint pfnBeginThread,
        nint pfnEndThread);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
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
    private static partial nint SDL_DuplicateSurface(nint surface);

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

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetAppMetadataProperty(string name);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetBooleanProperty(uint props, string name, SdlBool defaultValue);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetClipboardData(string mimeType, out nuint size);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetClipboardMimeTypes(out nuint numMimeTypes);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CallerOwnedStringMarshaller))]
    private static partial string SDL_GetClipboardText();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetClosestFullscreenDisplayMode(uint displayId, int w, int h, float refreshRate,
        SdlBool includeHighDensityModes, out DisplayMode closest);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetCurrentDisplayMode(uint displayId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayOrientation SDL_GetCurrentDisplayOrientation(uint displayId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong SDL_GetCurrentThreadID();

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetCurrentVideoDriver();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetDesktopDisplayMode(uint displayId);

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

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
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

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetError();

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
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

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetHint(string name);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetHintBoolean(string name, SdlBool defaultValue);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetKeyboardFocus();

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetKeyboardNameForID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetKeyboards(out int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetKeyboardState(out int numkeys);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetKeyFromName(string name);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetKeyFromScancode(ScanCode scanCode, KeyMod modstate, SdlBool keyEvent);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
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

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long SDL_GetNumberProperty(uint props, string name, long defaultValue);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumVideoDrivers();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetPixelFormatDetails(PixelFormat format);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PixelFormat SDL_GetPixelFormatForMasks(int bpp, uint rmask, uint gmask, uint bmask,
        uint amask);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetPixelFormatName(PixelFormat format);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
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

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CallerOwnedStringMarshaller))]
    private static partial string SDL_GetPrimarySelectionText();

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
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

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ScanCode SDL_GetScancodeFromName(string name);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetScancodeName(ScanCode scanCode);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
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
    private static partial nint SDL_GetSurfacePalette(nint surface);

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

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetThreadName(nint thread);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ThreadState SDL_GetThreadState(nint thread);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetTLS(nint id);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
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
    private static partial nint SDL_GetWindowFullscreenMode(nint window);

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
    private static partial nint SDL_GetWindowMouseRect(nint window);

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
    private static partial nint SDL_GetWindowSurface(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetWindowSurfaceVSync(nint window, out int vsync);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetWindowTitle(nint window);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GUIDToString(SdlGuid guid, string pszGuid, int cbGuid);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
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

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
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

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_LoadBMP(string file);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_LoadBMP_IO(nint src, SdlBool closeio);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_LoadFunction(nint handle, string name);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
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

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_RemoveHintCallback(string name, SdlHintCallback callback, nint userdata);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_RemoveSurfaceAlternateImages(nint surface);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
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

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SaveBMP(nint surface, string file);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SaveBMP_IO(nint surface, nint dst, SdlBool closeio);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_ScaleSurface(nint surface, int width, int height, ScaleMode scaleMode);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ScreenKeyboardShown(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ScreenSaverEnabled();

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAppMetadata(string appname, string appversion, string appidentifier);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetAppMetadataProperty(string name, string value);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetBooleanProperty(uint props, string name, SdlBool value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetClipboardData(SdlClipboardDataCallback callback,
            SdlClipboardCleanupCallback cleanup, nint userdata, nint mimeTypes, nuint numMimeTypes);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetClipboardText(string text);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetCurrentThreadPriority(ThreadPriority priority);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetError(string fmt);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetFloatProperty(uint props, string name, float value);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetHint(string name, string value);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetHintWithPriority(string name, string value, HintPriority priority);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetMainReady();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetModState(KeyMod modstate);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetNumberProperty(uint props, string name, long value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetPaletteColors(nint palette, Span<Color> colors, int firstcolor,
        int ncolors);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetPointerProperty(uint props, string name, nint value);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetPointerPropertyWithCleanup(uint props, string name, nint value,
        SdlCleanupPropertyCallback cleanup, nint userdata);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetPrimarySelectionText(string text);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetScancodeName(ScanCode scanCode, string name);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
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

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
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

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
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