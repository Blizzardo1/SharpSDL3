<<<<<<< HEAD
using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
=======
﻿using SharpSDL3.Enums;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;

using static SharpSDL3.Sdl;
using System;
using SharpSDL3.Structs;
>>>>>>> main

namespace SharpSDL3;

public static partial class Sdl {
<<<<<<< HEAD
    /// <summary>Return whether a mouse is currently connected.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetMice"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if a mouse is connected, <see langword="false" /> otherwise.</returns>

    public static bool HasMouse() => SDL_HasMouse();

    /// <summary>Get a list of currently connected mice.</summary>

    /// <param name="count">a pointer filled in with the number of mice returned, may be discarded.</param>
    /// <remarks>
    /// Note that this will include any device or virtual driver that includes
    /// mouse functionality, including some game controllers, KVM switches, etc.
    /// You should wait for input from a device before you consider it actively in
    /// use.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetMouseNameForID"/>
    /// <seealso cref="HasMouse"/>
    /// </remarks>
    /// <returns>(SDL_MouseID *) Returns a 0 terminated array of mouseinstance IDs or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information. This should be freed with <see cref="Free"/> when itis no longer needed.</returns>

=======

    public static bool HasMouse() => SDL_HasMouse();

>>>>>>> main
    public static nint GetMice(out int count) {
        nint result = SDL_GetMice(out count);
        if (result == nint.Zero) {
            throw new InvalidOperationException("Failed to get mice.");
        }

        return result;
    }

<<<<<<< HEAD
    /// <summary>Get the name of a mouse.</summary>

    /// <param name="instance_id">the mouse instance ID.</param>
    /// <remarks>
    /// This function returns &quot;&quot; if the mouse doesn't have a name.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetMice"/>
    /// </remarks>
    /// <returns>Returns the name of the selected mouse, or <see langword="null" /> on failure;call <see cref="GetError()" /> for more information.</returns>

=======
>>>>>>> main
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

<<<<<<< HEAD
    /// <summary>Get the window which currently has mouse focus.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(SDL_Window *) Returns the window with mouse focus.</returns>

    public static nint GetMouseFocus() => SDL_GetMouseFocus();

    /// <summary>Query SDL's cache for the synchronous mouse button state and the window-relative SDL-cursor position.</summary>

    /// <param name="x">a pointer to receive the SDL-cursor's x-position from the focused window's top left corner, can be <see langword="null" /> if unused.</param>
    /// <param name="y">a pointer to receive the SDL-cursor's y-position from the focused window's top left corner, can be <see langword="null" /> if unused.</param>
    /// <remarks>
    /// This function returns the cached synchronous state as SDL understands it
    /// from the last pump of the event queue.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGlobalMouseState"/>
    /// <seealso cref="GetRelativeMouseState"/>
    /// </remarks>
    /// <returns>Returns a 32-bit bitmask ofthe button state that can be bitwise-compared against theSDL_BUTTON_MASK(X) macro.</returns>

=======
    public static nint GetMouseFocus() => SDL_GetMouseFocus();

>>>>>>> main
    public static MouseButtonFlags GetMouseState(out float x, out float y) {
        MouseButtonFlags state = SDL_GetMouseState(out x, out y);

        // I SWEAR IF THIS IS AN ISSUE, I WILL BASH COPILOT IN THE HEAD
        if (x < 0 || y < 0) {
            throw new InvalidOperationException("Mouse coordinates are invalid.");
        }

        return state;
    }

<<<<<<< HEAD
    /// <summary>Query the platform for the asynchronous mouse button state and the desktop-relative platform-cursor position.</summary>

    /// <param name="x">a pointer to receive the platform-cursor's x-position from the desktop's top left corner, can be <see langword="null" /> if unused.</param>
    /// <param name="y">a pointer to receive the platform-cursor's y-position from the desktop's top left corner, can be <see langword="null" /> if unused.</param>
    /// <remarks>
    /// This function immediately queries the platform for the most recent
    /// asynchronous state, more costly than retrieving SDL's cached state in
    /// SDL_GetMouseState().
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CaptureMouse"/>
    /// <seealso cref="GetMouseState"/>
    /// <seealso cref="GetGlobalMouseState"/>
    /// </remarks>
    /// <returns>Returns a 32-bit bitmask ofthe button state that can be bitwise-compared against theSDL_BUTTON_MASK(X) macro.</returns>

=======
>>>>>>> main
    public static MouseButtonFlags GetGlobalMouseState(out float x, out float y) {
        MouseButtonFlags state = SDL_GetGlobalMouseState(out x, out y);

        // AND THIS
        if (x < 0 || y < 0) {
            throw new InvalidOperationException("Global mouse coordinates are invalid.");
        }

        return state;
    }

<<<<<<< HEAD
    /// <summary>Query SDL's cache for the synchronous mouse button state and accumulated mouse delta since last call.</summary>

    /// <param name="x">a pointer to receive the x mouse delta accumulated since last call, can be <see langword="null" /> if unused.</param>
    /// <param name="y">a pointer to receive the y mouse delta accumulated since last call, can be <see langword="null" /> if unused.</param>
    /// <remarks>
    /// This function returns the cached synchronous state as SDL understands it
    /// from the last pump of the event queue.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetMouseState"/>
    /// <seealso cref="GetGlobalMouseState"/>
    /// </remarks>
    /// <returns>Returns a 32-bit bitmask ofthe button state that can be bitwise-compared against theSDL_BUTTON_MASK(X) macro.</returns>

=======
>>>>>>> main
    public static MouseButtonFlags GetRelativeMouseState(out float x, out float y) {
        MouseButtonFlags state = SDL_GetRelativeMouseState(out x, out y);

        if (x < -10000 || x > 10000 || y < -10000 || y > 10000) {
            throw new InvalidOperationException("Relative mouse coordinates are out of expected range.");
        }

        return state;
    }

<<<<<<< HEAD
    /// <summary>Move the mouse cursor to the given position within the window.</summary>

    /// <param name="window">the window to move the mouse into, or <see langword="null" /> for the current mouse focus.</param>
    /// <param name="x">the x coordinate within the window.</param>
    /// <param name="y">the y coordinate within the window.</param>
    /// <remarks>
    /// This function generates a mouse motion event if relative mode is not
    /// enabled. If relative mode is enabled, you can force mouse events for the
    /// warp by setting the
    /// SDL_HINT_MOUSE_RELATIVE_WARP_MOTION
    /// hint.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="WarpMouseGlobal"/>
    /// </remarks>

=======
>>>>>>> main
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

<<<<<<< HEAD
    /// <summary>Move the mouse to the given position in global screen space.</summary>

    /// <param name="x">the x coordinate.</param>
    /// <param name="y">the y coordinate.</param>
    /// <remarks>
    /// This function generates a mouse motion event.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="WarpMouseInWindow"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
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

<<<<<<< HEAD
    /// <summary>Set relative mouse mode for a window.</summary>

    /// <param name="window">the window to change.</param>
    /// <param name="enabled"><see langword="true" /> to enable relative mode, <see langword="false" /> to disable.</param>
    /// <remarks>
    /// While the window has focus and relative mouse mode is enabled, the cursor
    /// is hidden, the mouse position is constrained to the window, and SDL will
    /// report continuous relative mouse motion even if the mouse is at the edge of
    /// the window.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetWindowRelativeMouseMode"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
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

<<<<<<< HEAD
    /// <summary>Query whether relative mouse mode is enabled for a window.</summary>

    /// <param name="window">the window to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetWindowRelativeMouseMode"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if relative mode is enabled for a window or <see langword="false" />otherwise.</returns>

=======
>>>>>>> main
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

<<<<<<< HEAD
    /// <summary>Capture the mouse and to track input outside an SDL window.</summary>

    /// <param name="enabled"><see langword="true" /> to enable capturing, <see langword="false" /> to disable.</param>
    /// <remarks>
    /// Capturing enables your app to obtain mouse events globally, instead of just
    /// within your window. Not all video targets support this function. When
    /// capturing is enabled, the current window will get all mouse events, but
    /// unlike relative mode, no change is made to the cursor and it is not
    /// restrained to your window.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGlobalMouseState"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static bool CaptureMouse(bool enabled) {
        SdlBool result = SDL_CaptureMouse(enabled);
        if (!result) {
            LogError(LogCategory.Error, "Failed to capture mouse.");
        }

        return result;
    }

<<<<<<< HEAD
    /// <summary>Create a cursor using the specified bitmap data and mask (in MSB format).</summary>

    /// <param name="data">the color value for each pixel of the cursor.</param>
    /// <param name="mask">the mask value for each pixel of the cursor.</param>
    /// <param name="w">the width of the cursor.</param>
    /// <param name="h">the height of the cursor.</param>
    /// <param name="hot_x">the x-axis offset from the left of the cursor image to the mouse x position, in the range of 0 to w - 1.</param>
    /// <param name="hot_y">the y-axis offset from the top of the cursor image to the mouse y position, in the range of 0 to h - 1.</param>
    /// <remarks>
    /// mask has to be in MSB (Most Significant Bit) format.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateColorCursor"/>
    /// <seealso cref="CreateSystemCursor"/>
    /// <seealso cref="DestroyCursor"/>
    /// <seealso cref="SetCursor"/>
    /// </remarks>
    /// <returns>(SDL_Cursor *) Returns a new cursor with the specifiedparameters on success or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
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

<<<<<<< HEAD
    /// <summary>Create a color cursor.</summary>

    /// <param name="surface">an SDL_Surface structure representing the cursor image.</param>
    /// <param name="hot_x">the x position of the cursor hot spot.</param>
    /// <param name="hot_y">the y position of the cursor hot spot.</param>
    /// <remarks>
    /// If this function is passed a surface with alternate representations, the
    /// surface will be interpreted as the content to be used for 100% display
    /// scale, and the alternate representations will be used for high DPI
    /// situations. For example, if the original surface is 32x32, then on a 2x
    /// macOS display or 200% display scale on Windows, a 64x64 version of the
    /// image will be used, if available. If a matching version of the image isn't
    /// available, the closest larger size image will be downscaled to the
    /// appropriate size and be used instead, if available. Otherwise, the closest
    /// smaller image will be upscaled and be used instead.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateCursor"/>
    /// <seealso cref="CreateSystemCursor"/>
    /// <seealso cref="DestroyCursor"/>
    /// <seealso cref="SetCursor"/>
    /// </remarks>
    /// <returns>(SDL_Cursor *) Returns the new cursor on success or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

=======
>>>>>>> main
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

<<<<<<< HEAD
    /// <summary>Create a system cursor.</summary>

    /// <param name="id">an SDL_SystemCursor enum value.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="DestroyCursor"/>
    /// </remarks>
    /// <returns>(SDL_Cursor *) Returns a cursor on success or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

=======
>>>>>>> main
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

<<<<<<< HEAD
    /// <summary>Set the active cursor.</summary>

    /// <param name="cursor">a cursor to make active.</param>
    /// <remarks>
    /// This function sets the currently active cursor to the specified one. If the
    /// cursor is currently visible, the change will be immediately represented on
    /// the display. SDL_SetCursor(<see langword="null" />) can be used to force
    /// cursor redraw, if this is desired for any reason.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetCursor"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
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

<<<<<<< HEAD
    /// <summary>Get the active cursor.</summary>
    /// <remarks>
    /// This function returns a pointer to the current cursor which is owned by the
    /// library. It is not necessary to free the cursor with
    /// SDL_DestroyCursor().
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetCursor"/>
    /// </remarks>
    /// <returns>(SDL_Cursor *) Returns the active cursor or <see langword="null" /> if there isno mouse.</returns>

    public static nint GetCursor() => SDL_GetCursor();

    /// <summary>Get the default cursor.</summary>
    /// <remarks>
    /// You do not have to call SDL_DestroyCursor() on the
    /// return value, but it is safe to do so.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(SDL_Cursor *) Returns the default cursor on success or <see langword="null" />on failuree; call <see cref="GetError()" /> for more information.</returns>

    public static nint GetDefaultCursor() => SDL_GetDefaultCursor();

    /// <summary>Free a previously-created cursor.</summary>

    /// <param name="cursor">the cursor to free.</param>
    /// <remarks>
    /// Use this function to free cursor resources created with
    /// SDL_CreateCursor(),
    /// SDL_CreateColorCursor() or
    /// SDL_CreateSystemCursor().
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateColorCursor"/>
    /// <seealso cref="CreateCursor"/>
    /// <seealso cref="CreateSystemCursor"/>
    /// </remarks>

=======
    public static nint GetCursor() => SDL_GetCursor();

    public static nint GetDefaultCursor() => SDL_GetDefaultCursor();

>>>>>>> main
    public static void DestroyCursor(nint cursor) {
        if (cursor == nint.Zero) {
            throw new ArgumentException("Cursor handle cannot be null.", nameof(cursor));
        }

        SDL_DestroyCursor(cursor);
    }

<<<<<<< HEAD
    /// <summary>Show the cursor.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CursorVisible"/>
    /// <seealso cref="HideCursor"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool ShowCursor() => SDL_ShowCursor();

    /// <summary>Hide the cursor.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CursorVisible"/>
    /// <seealso cref="ShowCursor"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool HideCursor() => SDL_HideCursor();

    /// <summary>Return whether the cursor is currently being shown.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="HideCursor"/>
    /// <seealso cref="ShowCursor"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the cursor is being shown, or <see langword="false" /> if thecursor is hidden.</returns>

=======
    public static bool ShowCursor() => SDL_ShowCursor();

    public static bool HideCursor() => SDL_HideCursor();

>>>>>>> main
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