using SharpSDL3.Enums;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SharpSDL3;

public static unsafe partial class Sdl {
    /// <summary>Get the touch device name as reported from the driver.</summary>

    /// <param name="touchID">the touch device instance ID.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(const char *) Returns touch device name, or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information.</returns>

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

    /// <summary>Get a list of registered touch devices.</summary>

    /// <param name="count">a pointer filled in with the number of devices returned, discarded.</param>
    /// <remarks>
    /// On some platforms SDL first sees the touch device if it was actually used.
    /// Therefore the returned list might be empty, although devices are available.
    /// After using all devices at least once the number will be correct.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(SDL_TouchID *) Returns a 0 terminated array of touch deviceIDs or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information. This should be freed with <see cref="Free"/> when it is nolonger needed.</returns>

    public static Span<nint> GetTouchDevices() {
        nint result = SDL_GetTouchDevices(out int count);

        if (result == nint.Zero) {
            LogError(LogCategory.Error, "Failed to retrieve touch devices.");
        }

        nint[] data = new nint[count];
        Marshal.Copy(result, data, 0, count);

        return data;
    }

    /// <summary>Get the type of the given touch device.</summary>

    /// <param name="touchID">the ID of a touch device.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns touch device type.</returns>

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

    /// <summary>Get a list of active fingers for a given touch device.</summary>

    /// <param name="touchID">the ID of a touch device.</param>
    /// <param name="count">a pointer filled in with the number of fingers returned, can be <see langword="null" />.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(SDL_Finger **) Returns a <see langword="null" /> terminated array ofSDL_Finger pointers or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information. This is a singleallocation that should be freed with <see cref="Free"/> when it is nolonger needed.</returns>

    public static Span<nint> GetTouchFingers(ulong touchId) {
        nint result = SDL_GetTouchFingers(touchId, out int count);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "Failed to retrieve touch devices.");
        }

        nint[] data = new nint[count];
        Marshal.Copy(result, data, 0, count);

        return data;
    }

    /// <summary>Get a list of active fingers for a given touch device.</summary>

    /// <param name="touchID">the ID of a touch device.</param>
    /// <param name="count">a pointer filled in with the number of fingers returned, can be <see langword="null" />.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(SDL_Finger **) Returns a <see langword="null" /> terminated array ofSDL_Finger pointers or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information. This is a singleallocation that should be freed with <see cref="Free"/> when it is nolonger needed.</returns>

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