﻿using SharpSDL3.Enums;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;

namespace SharpSDL3;

public static unsafe partial class Sdl {

    public static string GetTouchDeviceName(ulong touchId) {
        if (touchId == 0) {
            throw new ArgumentException("Touch ID cannot be zero.", nameof(touchId));
        }

        string deviceName = SDL_GetTouchDeviceName(touchId);

        if (string.IsNullOrEmpty(deviceName)) {
            throw new InvalidOperationException($"Failed to retrieve the name for touch device with ID {touchId}.");
        }

        return deviceName;
    }

    public static Span<nint> GetTouchDevices() {
        nint result = SDL_GetTouchDevices(out int count);

        if (result == nint.Zero) {
            LogError(LogCategory.Error, "Failed to retrieve touch devices.");
        }

        nint[] data = new nint[count];
        Marshal.Copy(result, data, 0, count);

        return data;
    }

    public static TouchDeviceType GetTouchDeviceType(ulong touchId) {
        if (touchId == 0) {
            throw new ArgumentException("Touch ID cannot be zero.", nameof(touchId));
        }
        TouchDeviceType deviceType = SDL_GetTouchDeviceType(touchId);
        if (deviceType == TouchDeviceType.Invalid) {
            throw new InvalidOperationException($"Failed to retrieve the type for touch device with ID {touchId}.");
        }
        return deviceType;
    }

    public static Span<nint> GetTouchFingers(ulong touchId) {
        nint result = SDL_GetTouchFingers(touchId, out int count);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "Failed to retrieve touch devices.");
        }

        nint[] data = new nint[count];
        Marshal.Copy(result, data, 0, count);

        return data;
    }

    public static Span<nint> GetTouchFingers(ulong touchId, out int count) {
        nint result = SDL_GetTouchFingers(touchId, out count);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "Failed to retrieve touch devices.");
        }

        nint[] data = new nint[count];
        Marshal.Copy(result, data, 0, count);

        return data;
    }

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetTouchDeviceName(ulong touchId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetTouchDevices(out int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial TouchDeviceType SDL_GetTouchDeviceType(ulong touchId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetTouchFingers(ulong touchId, out int count);
}