using SharpSDL3.Enums;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;

using static SharpSDL3.Sdl;
using System;
using SharpSDL3.Structs;

namespace SharpSDL3;

public static partial class Sdl {

    public static bool HasMouse() => SDL_HasMouse();

    public static nint GetMice(out int count) {
        nint result = SDL_GetMice(out count);
        if (result == nint.Zero) {
            throw new InvalidOperationException("Failed to get mice.");
        }

        return result;
    }

    public static string GetMouseNameForID(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }

        string mouseName = SDL_GetMouseNameForID(instanceId);
        if (string.IsNullOrEmpty(mouseName)) {
            throw new InvalidOperationException($"Failed to retrieve mouse name for instance ID {instanceId}.");
        }

        return mouseName;
    }

    public static nint GetMouseFocus() => SDL_GetMouseFocus();

    public static MouseButtonFlags GetMouseState(out float x, out float y) {
        MouseButtonFlags state = SDL_GetMouseState(out x, out y);

        // I SWEAR IF THIS IS AN ISSUE, I WILL BASH COPILOT IN THE HEAD
        if (x < 0 || y < 0) {
            throw new InvalidOperationException("Mouse coordinates are invalid.");
        }

        return state;
    }

    public static MouseButtonFlags GetGlobalMouseState(out float x, out float y) {
        MouseButtonFlags state = SDL_GetGlobalMouseState(out x, out y);

        // AND THIS
        if (x < 0 || y < 0) {
            throw new InvalidOperationException("Global mouse coordinates are invalid.");
        }

        return state;
    }

    public static MouseButtonFlags GetRelativeMouseState(out float x, out float y) {
        MouseButtonFlags state = SDL_GetRelativeMouseState(out x, out y);

        if (x < -10000 || x > 10000 || y < -10000 || y > 10000) {
            throw new InvalidOperationException("Relative mouse coordinates are out of expected range.");
        }

        return state;
    }

    public static void WarpMouseInWindow(nint window, float x, float y) {
        if (window == nint.Zero) {
            throw new ArgumentException("Window handle cannot be null.", nameof(window));
        }

        if (x < 0) {
            throw new ArgumentOutOfRangeException(nameof(x), "Mouse coordinates must be non-negative.");
        }

        if (y < 0) {
            throw new ArgumentOutOfRangeException(nameof(y), "Mouse coordinates must be non-negative.");
        }

        SDL_WarpMouseInWindow(window, x, y);
    }

    public static bool WarpMouseGlobal(float x, float y) {
        if (x < 0) {
            throw new ArgumentOutOfRangeException(nameof(x), "Mouse coordinates must be non-negative.");
        }

        if (y < 0) {
            throw new ArgumentOutOfRangeException(nameof(y), "Mouse coordinates must be non-negative.");
        }

        SdlBool result = SDL_WarpMouseGlobal(x, y);
        if (!result) {
            LogError(LogCategory.Error, "Failed to warp mouse globally.");
        }

        return result;
    }

    public static bool SetWindowRelativeMouseMode(nint window, bool enabled) {
        if (window == nint.Zero) {
            throw new ArgumentException("Window handle cannot be null.", nameof(window));
        }

        SdlBool result = SDL_SetWindowRelativeMouseMode(window, enabled);
        if (!result) {
            LogError(LogCategory.Error, $"Failed to set relative mouse mode for the specified window. {GetError()}");
        }

        return result;
    }

    public static bool GetWindowRelativeMouseMode(nint window) {
        if (window == nint.Zero) {
            throw new ArgumentException("Window handle cannot be null.", nameof(window));
        }

        SdlBool result = SDL_GetWindowRelativeMouseMode(window);
        if (!result) {
            LogError(LogCategory.Error, $"Failed to retrieve relative mouse mode for the specified window. {GetError()}");
        }

        return result;
    }

    public static bool CaptureMouse(bool enabled) {
        SdlBool result = SDL_CaptureMouse(enabled);
        if (!result) {
            LogError(LogCategory.Error, "Failed to capture mouse.");
        }

        return result;
    }

    public static nint CreateCursor(nint data, nint mask, int w, int h, int hotX, int hotY) {
        if (data == nint.Zero) {
            throw new ArgumentException("Data handle cannot be null.", nameof(data));
        }

        if (mask == nint.Zero) {
            throw new ArgumentException("Mask handle cannot be null.", nameof(mask));
        }

        if (w <= 0) {
            throw new ArgumentOutOfRangeException(nameof(w), "Width must be greater than zero.");
        }

        if (h <= 0) {
            throw new ArgumentOutOfRangeException(nameof(h), "Height must be greater than zero.");
        }

        if (hotX < 0 || hotX >= w) {
            throw new ArgumentOutOfRangeException(nameof(hotX), "Hotspot X coordinate must be within the cursor dimensions.");
        }

        if (hotY < 0 || hotY >= h) {
            throw new ArgumentOutOfRangeException(nameof(hotY), "Hotspot Y coordinate must be within the cursor dimensions.");
        }

        nint cursor = SDL_CreateCursor(data, mask, w, h, hotX, hotY);
        if (cursor == nint.Zero) {
            throw new InvalidOperationException("Failed to create cursor.");
        }

        return cursor;
    }

    public static nint CreateColorCursor(nint surface, int hotX, int hotY) {
        if (surface == nint.Zero) {
            throw new ArgumentException("Surface handle cannot be null.", nameof(surface));
        }

        // AND THIS
        if (hotX < 0) {
            throw new ArgumentOutOfRangeException(nameof(hotX), "Hotspot coordinates must be non-negative.");
        }

        if (hotY < 0) {
            throw new ArgumentOutOfRangeException(nameof(hotY), "Hotspot coordinates must be non-negative.");
        }

        nint cursor = SDL_CreateColorCursor(surface, hotX, hotY);
        if (cursor == nint.Zero) {
            throw new InvalidOperationException("Failed to create color cursor.");
        }

        return cursor;
    }

    public static nint CreateSystemCursor(SystemCursor id) {
        if (!Enum.IsDefined(id)) {
            throw new ArgumentOutOfRangeException(nameof(id), "Invalid system cursor ID.");
        }

        nint cursor = SDL_CreateSystemCursor(id);
        if (cursor == nint.Zero) {
            throw new InvalidOperationException("Failed to create system cursor.");
        }

        return cursor;
    }

    public static bool SetCursor(nint cursor) {
        if (cursor == nint.Zero) {
            throw new ArgumentException("Cursor handle cannot be null.", nameof(cursor));
        }

        SdlBool result = SDL_SetCursor(cursor);
        if (!result) {
            LogError(LogCategory.Error, "Failed to set the specified cursor.");
        }

        return result;
    }

    public static nint GetCursor() => SDL_GetCursor();

    public static nint GetDefaultCursor() => SDL_GetDefaultCursor();

    public static void DestroyCursor(nint cursor) {
        if (cursor == nint.Zero) {
            throw new ArgumentException("Cursor handle cannot be null.", nameof(cursor));
        }

        SDL_DestroyCursor(cursor);
    }

    public static bool ShowCursor() => SDL_ShowCursor();

    public static bool HideCursor() => SDL_HideCursor();

    public static bool CursorVisible() => SDL_CursorVisible();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasMouse();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetMice(out int count);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetMouseNameForID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetMouseFocus();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial MouseButtonFlags SDL_GetMouseState(out float x, out float y);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial MouseButtonFlags SDL_GetGlobalMouseState(out float x, out float y);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial MouseButtonFlags SDL_GetRelativeMouseState(out float x, out float y);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_WarpMouseInWindow(nint window, float x, float y);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WarpMouseGlobal(float x, float y);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetWindowRelativeMouseMode(nint window, SdlBool enabled);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetWindowRelativeMouseMode(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CaptureMouse(SdlBool enabled);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateCursor(nint data, nint mask, int w, int h, int hotX, int hotY);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateColorCursor(nint surface, int hotX, int hotY);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateSystemCursor(SystemCursor id);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetCursor(nint cursor);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetCursor();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetDefaultCursor();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyCursor(nint cursor);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ShowCursor();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HideCursor();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CursorVisible();
}