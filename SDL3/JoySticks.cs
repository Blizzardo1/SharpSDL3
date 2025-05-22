using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SharpSDL3; 
public static unsafe partial class Sdl {


    public static uint AttachVirtualJoystick(ref VirtualJoystickDesc desc) {
        // VirtualJoystickDesc is a struct, which is a value type and cannot be null.
        // Instead of checking for null, we can validate its fields if necessary.
        if (desc.Name == nint.Zero) {
            throw new ArgumentException("Virtual joystick description must have a valid name.", nameof(desc));
        }
        uint instanceId = SDL_AttachVirtualJoystick(ref desc);
        if (instanceId == 0) {
            throw new InvalidOperationException("Failed to attach virtual joystick.");
        }
        return instanceId;
    }

    public static void CloseJoystick(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        SDL_CloseJoystick(joystick);
    }

    public static SdlBool DetachVirtualJoystick(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        SdlBool result = SDL_DetachVirtualJoystick(instanceId);
        if (!result) {
            throw new InvalidOperationException($"Failed to detach virtual joystick with instance ID {instanceId}.");
        }
        return result;
    }

    public static short GetJoystickAxis(nint joystick, int axis) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        if (axis < 0) {
            throw new ArgumentException("Axis cannot be negative.", nameof(axis));
        }
        short axisValue = SDL_GetJoystickAxis(joystick, axis);
        return axisValue;
    }

    public static SdlBool GetJoystickAxisInitialState(nint joystick, int axis, out short state) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        if (axis < 0) {
            throw new ArgumentException("Axis cannot be negative.", nameof(axis));
        }
        SdlBool result = SDL_GetJoystickAxisInitialState(joystick, axis, out state);
        return result;
    }

    public static SdlBool GetJoystickBall(nint joystick, int ball, out int dx, out int dy) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        if (ball < 0) {
            throw new ArgumentException("Ball cannot be negative.", nameof(ball));
        }
        SdlBool result = SDL_GetJoystickBall(joystick, ball, out dx, out dy);
        return result;
    }

    public static SdlBool GetJoystickButton(nint joystick, int button) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        if (button < 0) {
            throw new ArgumentException("Button cannot be negative.", nameof(button));
        }
        SdlBool buttonState = SDL_GetJoystickButton(joystick, button);
        return buttonState;
    }

    public static JoystickConnectionState GetJoystickConnectionState(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        JoystickConnectionState connectionState = SDL_GetJoystickConnectionState(joystick);
        return connectionState;
    }

    public static ushort GetJoystickFirmwareVersion(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        ushort firmwareVersion = SDL_GetJoystickFirmwareVersion(joystick);
        return firmwareVersion;
    }

    public static nint GetJoystickFromId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        nint joystick = SDL_GetJoystickFromID(instanceId);
        if (joystick == nint.Zero) {
            throw new InvalidOperationException($"Failed to get joystick from ID {instanceId}.");
        }
        return joystick;
    }

    public static nint GetJoystickFromPlayerIndex(int playerIndex) {
        if (playerIndex < 0) {
            throw new ArgumentException("Player index cannot be negative.", nameof(playerIndex));
        }
        nint joystick = SDL_GetJoystickFromPlayerIndex(playerIndex);
        if (joystick == nint.Zero) {
            throw new InvalidOperationException($"Failed to get joystick from player index {playerIndex}.");
        }
        return joystick;
    }

    public static SdlGuid GetJoystickGuid(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        SdlGuid guid = SDL_GetJoystickGUID(joystick);
        if (guid.Data == null) {
            throw new InvalidOperationException("Failed to get joystick GUID.");
        }
        return guid;
    }

    public static SdlGuid GetJoystickGuidForId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetJoystickGUIDForID(instanceId);
    }

    public static void GetJoystickGUIDInfo(SdlGuid guid, out ushort vendor, out ushort product, out ushort version, out ushort crc16) {
        if (guid.Data == null) {
            throw new ArgumentException("GUID cannot be null.", nameof(guid));
        }
        SDL_GetJoystickGUIDInfo(guid, out vendor, out product, out version, out crc16);
    }

    public static byte GetJoystickHat(nint joystick, int hat) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        if (hat < 0) {
            throw new ArgumentException("Hat cannot be negative.", nameof(hat));
        }
        byte hatValue = SDL_GetJoystickHat(joystick, hat);
        return hatValue;
    }

    public static uint GetJoystickID(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        uint id = SDL_GetJoystickID(joystick);
        return id;
    }

    public static string GetJoystickName(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        string joystickName = SDL_GetJoystickName(joystick);
        if (string.IsNullOrEmpty(joystickName)) {
            throw new InvalidOperationException("Failed to get joystick name.");
        }
        return joystickName;
    }

    public static string GetJoystickNameForId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }

        string joystickName = SDL_GetJoystickNameForID(instanceId);
        if (string.IsNullOrEmpty(joystickName)) {
            throw new InvalidOperationException($"No joystick found for instance ID {instanceId}.");
        }

        return joystickName;
    }

    public static string GetJoystickPath(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        string joystickPath = SDL_GetJoystickPath(joystick);
        if (string.IsNullOrEmpty(joystickPath)) {
            throw new InvalidOperationException("Failed to get joystick path.");
        }
        return joystickPath;
    }

    public static string GetJoystickPathForId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        string joystickPath = SDL_GetJoystickPathForID(instanceId);
        if (string.IsNullOrEmpty(joystickPath)) {
            throw new InvalidOperationException($"No joystick found for instance ID {instanceId}.");
        }
        return joystickPath;
    }

    public static int GetJoystickPlayerIndex(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        int playerIndex = SDL_GetJoystickPlayerIndex(joystick);
        if (playerIndex < 0) {
            throw new InvalidOperationException("Failed to get joystick player index.");
        }
        return playerIndex;
    }

    public static int GetJoystickPlayerIndexForId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetJoystickPlayerIndexForID(instanceId);
    }

    public static PowerState GetJoystickPowerInfo(nint joystick, out int percent) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        PowerState powerState = SDL_GetJoystickPowerInfo(joystick, out percent);
        return powerState;
    }

    public static ushort GetJoystickProduct(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        ushort product = SDL_GetJoystickProduct(joystick);
        return product;
    }

    public static ushort GetJoystickProductForId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetJoystickProductForID(instanceId);
    }

    public static ushort GetJoystickProductVersion(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        ushort productVersion = SDL_GetJoystickProductVersion(joystick);
        return productVersion;
    }

    public static ushort GetJoystickProductVersionForId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetJoystickProductVersionForID(instanceId);
    }

    public static uint GetJoystickProperties(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        uint properties = SDL_GetJoystickProperties(joystick);
        return properties;
    }

    public static List<nint> GetJoysticks(out int count) {
        nint joystickArrayPtr = SDL_GetJoysticks(out count);
        if (joystickArrayPtr == 0 || count <= 0) {
            return [];
        }

        List<nint> joysticks = new(count);
        for (int i = 0; i < count; i++) {
            nint joystick = Marshal.ReadInt32(joystickArrayPtr, i * nint.Size);
            joysticks.Add(joystick);
        }

        return joysticks;
    }

    public static string GetJoystickSerial(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        string serial = SDL_GetJoystickSerial(joystick);
        if (string.IsNullOrEmpty(serial)) {
            throw new InvalidOperationException("Failed to get joystick serial.");
        }
        return serial;
    }

    public static JoystickType GetJoystickType(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        JoystickType joystickType = SDL_GetJoystickType(joystick);
        return joystickType;
    }

    public static JoystickType GetJoystickTypeForId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetJoystickTypeForID(instanceId);
    }

    public static ushort GetJoystickVendor(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        ushort vendor = SDL_GetJoystickVendor(joystick);
        return vendor;
    }

    public static ushort GetJoystickVendorForId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetJoystickVendorForID(instanceId);
    }

    public static int GetNumJoystickAxes(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        int numAxes = SDL_GetNumJoystickAxes(joystick);
        return numAxes;
    }

    public static int GetNumJoystickBalls(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        int numBalls = SDL_GetNumJoystickBalls(joystick);
        return numBalls;
    }

    public static int GetNumJoystickButtons(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        int numButtons = SDL_GetNumJoystickButtons(joystick);
        return numButtons;
    }

    public static int GetNumJoystickHats(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        int numHats = SDL_GetNumJoystickHats(joystick);
        return numHats;
    }

    public static SdlBool HasJoystick() {
        return SDL_HasJoystick();
    }

    public static SdlBool IsJoystickVirtual(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        SdlBool result = SDL_IsJoystickVirtual(instanceId);
        return result;
    }

    public static SdlBool JoystickConnected(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        SdlBool connected = SDL_JoystickConnected(joystick);
        return connected;
    }

    public static SdlBool JoystickEventsEnabled() {
        return SDL_JoystickEventsEnabled();
    }

    public static void LockJoysticks() {
        SDL_LockJoysticks();
    }

    public static nint OpenJoystick(uint instanceId) {
        nint joystick = SDL_OpenJoystick(instanceId);
        if (joystick == nint.Zero) {
            throw new InvalidOperationException($"Failed to open joystick with instance ID {instanceId}.");
        }
        return joystick;
    }

    public static SdlBool RumbleJoystick(nint joystick, ushort lowFrequencyRumble, ushort highFrequencyRumble, uint durationMs) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        if (lowFrequencyRumble < 0 || highFrequencyRumble < 0) {
            throw new ArgumentException("Rumble values cannot be negative.");
        }
        SdlBool result = SDL_RumbleJoystick(joystick, lowFrequencyRumble, highFrequencyRumble, durationMs);
        return result;
    }

    public static SdlBool RumbleJoystickTriggers(nint joystick, ushort leftRumble, ushort rightRumble, uint durationMs) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        if (leftRumble < 0 || rightRumble < 0) {
            throw new ArgumentException("Rumble values cannot be negative.");
        }
        SdlBool result = SDL_RumbleJoystickTriggers(joystick, leftRumble, rightRumble, durationMs);
        return result;
    }

    public static SdlBool SendJoystickEffect(nint joystick, nint data, int size) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        if (data == nint.Zero) {
            throw new ArgumentException("Data cannot be null.", nameof(data));
        }
        if (size <= 0) {
            throw new ArgumentException("Size must be positive.", nameof(size));
        }
        SdlBool result = SDL_SendJoystickEffect(joystick, data, size);
        return result;
    }

    public static SdlBool SendJoystickVirtualSensorData(nint joystick, SensorType type, ulong sensorTimestamp, nint data, int numValues) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        if (type < SensorType.Invalid || type > SensorType.GyroR) {
            throw new ArgumentOutOfRangeException(nameof(type), "Invalid sensor type.");
        }
        if (numValues <= 0) {
            throw new ArgumentException("Number of values must be positive.", nameof(numValues));
        }
        SdlBool result = SDL_SendJoystickVirtualSensorData(joystick, type, sensorTimestamp, data, numValues);
        return result;
    }

    public static SdlBool SendJoystickVirtualSensorData(nint joystick, SensorType type, ulong sensorTimestamp, float[] data) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        if (type < SensorType.Invalid || type > SensorType.GyroR) {
            throw new ArgumentOutOfRangeException(nameof(type), "Invalid sensor type.");
        }
        if (data == null || data.Length == 0) {
            throw new ArgumentException("Data array cannot be null or empty.", nameof(data));
        }
        
        nint pData = Marshal.AllocHGlobal(data.Length * sizeof(float));

        bool result = SendJoystickVirtualSensorData(joystick, type, sensorTimestamp, pData, data.Length);
        
        Marshal.Copy(data, 0, pData, data.Length);
        
        Free(pData);

        return result;
    }

    public static void SetJoystickEventsEnabled(bool enabled) {
        SDL_SetJoystickEventsEnabled(enabled);

        Logger.LogInfo(LogCategory.System, $"Joystick events enabled: {enabled}");

    }

    public static SdlBool SetJoystickLED(nint joystick, byte red, byte green, byte blue) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        SdlBool result = SDL_SetJoystickLED(joystick, red, green, blue);
        return result;
    }

    public static SdlBool SetJoystickPlayerIndex(nint joystick, int playerIndex) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        if (playerIndex < 0) {
            throw new ArgumentException("Player index cannot be negative.", nameof(playerIndex));
        }
        SdlBool result = SDL_SetJoystickPlayerIndex(joystick, playerIndex);
        return result;
    }

    public static SdlBool SetJoystickVirtualAxis(nint joystick, int axis, short value) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        if (axis < 0) {
            throw new ArgumentException("Axis cannot be negative.", nameof(axis));
        }
        SdlBool result = SDL_SetJoystickVirtualAxis(joystick, axis, value);
        return result;
    }

    public static SdlBool SetJoystickVirtualBall(nint joystick, int ball, short xrel, short yrel) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        if (ball < 0) {
            throw new ArgumentException("Ball cannot be negative.", nameof(ball));
        }
        SdlBool result = SDL_SetJoystickVirtualBall(joystick, ball, xrel, yrel);
        return result;
    }

    public static SdlBool SetJoystickVirtualButton(nint joystick, int button, SdlBool down) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        if (button < 0) {
            throw new ArgumentException("Button cannot be negative.", nameof(button));
        }
        SdlBool result = SDL_SetJoystickVirtualButton(joystick, button, down);
        return result;
    }

    public static SdlBool SetJoystickVirtualHat(nint joystick, int hat, byte value) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        if (hat < 0) {
            throw new ArgumentException("Hat cannot be negative.", nameof(hat));
        }
        SdlBool result = SDL_SetJoystickVirtualHat(joystick, hat, value);
        return result;
    }

    public static SdlBool SetJoystickVirtualTouchpad(nint joystick, int touchpad, int finger, SdlBool down, float x, float y, float pressure) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        if (touchpad < 0) {
            throw new ArgumentException("Touchpad cannot be negative.", nameof(touchpad));
        }
        if (finger < 0) {
            throw new ArgumentException("Finger cannot be negative.", nameof(finger));
        }
        SdlBool result = SDL_SetJoystickVirtualTouchpad(joystick, touchpad, finger, down, x, y, pressure);
        return result;
    }

    public static void UnlockJoysticks() {
        SDL_UnlockJoysticks();
    }

    public static void UpdateJoysticks() {
        SDL_UpdateJoysticks();
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_AttachVirtualJoystick([MarshalUsing(typeof(OwnedVirtualJoystickDescMarshaller))] ref VirtualJoystickDesc desc);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_CloseJoystick(nint joystick);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_DetachVirtualJoystick(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial short SDL_GetJoystickAxis(nint joystick, int axis);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetJoystickAxisInitialState(nint joystick, int axis, out short state);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetJoystickBall(nint joystick, int ball, out int dx, out int dy);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetJoystickButton(nint joystick, int button);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial JoystickConnectionState SDL_GetJoystickConnectionState(nint joystick);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetJoystickFirmwareVersion(nint joystick);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetJoystickFromID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetJoystickFromPlayerIndex(int playerIndex);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlGuid SDL_GetJoystickGUID(nint joystick);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlGuid SDL_GetJoystickGUIDForID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GetJoystickGUIDInfo(SdlGuid guid, out ushort vendor, out ushort product,
        out ushort version, out ushort crc16);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte SDL_GetJoystickHat(nint joystick, int hat);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetJoystickID(nint joystick);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetJoystickName(nint joystick);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetJoystickNameForID(uint instanceId);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetJoystickPath(nint joystick);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetJoystickPathForID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetJoystickPlayerIndex(nint joystick);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetJoystickPlayerIndexForID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PowerState SDL_GetJoystickPowerInfo(nint joystick, out int percent);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetJoystickProduct(nint joystick);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetJoystickProductForID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetJoystickProductVersion(nint joystick);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetJoystickProductVersionForID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetJoystickProperties(nint joystick);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetJoysticks(out int count);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetJoystickSerial(nint joystick);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial JoystickType SDL_GetJoystickType(nint joystick);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial JoystickType SDL_GetJoystickTypeForID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetJoystickVendor(nint joystick);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetJoystickVendorForID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumJoystickAxes(nint joystick);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumJoystickBalls(nint joystick);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumJoystickButtons(nint joystick);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumJoystickHats(nint joystick);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasJoystick();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_IsJoystickVirtual(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_JoystickConnected(nint joystick);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_JoystickEventsEnabled();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LockJoysticks();
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenJoystick(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RumbleJoystick(nint joystick, ushort lowFrequencyRumble,
        ushort highFrequencyRumble, uint durationMs);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RumbleJoystickTriggers(nint joystick, ushort leftRumble, ushort rightRumble,
        uint durationMs);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SendJoystickEffect(nint joystick, nint data, int size);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SendJoystickVirtualSensorData(nint joystick, SensorType type,
        ulong sensorTimestamp, nint data, int numValues);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetJoystickEventsEnabled(SdlBool enabled);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetJoystickLED(nint joystick, byte red, byte green, byte blue);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetJoystickPlayerIndex(nint joystick, int playerIndex);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetJoystickVirtualAxis(nint joystick, int axis, short value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetJoystickVirtualBall(nint joystick, int ball, short xrel, short yrel);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetJoystickVirtualButton(nint joystick, int button, SdlBool down);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetJoystickVirtualHat(nint joystick, int hat, byte value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetJoystickVirtualTouchpad(nint joystick, int touchpad, int finger,
        SdlBool down, float x, float y, float pressure);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnlockJoysticks();
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UpdateJoysticks();
}
