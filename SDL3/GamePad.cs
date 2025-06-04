using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using static SharpSDL3.Sdl;

namespace SharpSDL3; 
public static unsafe partial class Sdl {

    public static int AddGamepadMapping(string mapping) {
        if (string.IsNullOrWhiteSpace(mapping)) {
            throw new ArgumentException("Mapping cannot be null or empty.", nameof(mapping));
        }
        return SDL_AddGamepadMapping(mapping);
    }

    public static int AddGamepadMappingsFromFile(string file) {
        if (string.IsNullOrWhiteSpace(file)) {
            throw new ArgumentException("File path cannot be null or empty.", nameof(file));
        }
        return SDL_AddGamepadMappingsFromFile(file);
    }

    public static int AddGamepadMappingsFromIO(nint src, SdlBool closeio) {
        if (src == nint.Zero) {
            throw new ArgumentException("Source pointer cannot be null.", nameof(src));
        }
        return SDL_AddGamepadMappingsFromIO(src, closeio);
    }

    public static void CloseGamepad(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        SDL_CloseGamepad(gamepad);
    }

    public static bool GamepadConnected(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GamepadConnected(gamepad);
    }

    public static bool GamepadHasAxis(nint gamepad, GamepadAxis axis) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (axis == GamepadAxis.Invalid) {
            throw new ArgumentException("Invalid axis specified.", nameof(axis));
        }
        return SDL_GamepadHasAxis(gamepad, axis);
    }

    public static bool GamepadHasButton(nint gamepad, GamepadButton button) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (button == GamepadButton.Invalid) {
            throw new ArgumentException("Invalid button specified.", nameof(button));
        }
        return SDL_GamepadHasButton(gamepad, button);
    }

    public static bool GamepadHasSensor(nint gamepad, SensorType type) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (type == SensorType.Invalid) {
            throw new ArgumentException("Invalid sensor type specified.", nameof(type));
        }
        return SDL_GamepadHasSensor(gamepad, type);
    }

    public static bool GamepadSensorEnabled(nint gamepad, SensorType type) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (type == SensorType.Invalid) {
            throw new ArgumentException("Invalid sensor type specified.", nameof(type));
        }
        return SDL_GamepadSensorEnabled(gamepad, type);
    }

    public static string GetGamepadAppleSFSymbolsNameForAxis(nint gamepad, GamepadAxis axis) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (axis == GamepadAxis.Invalid) {
            throw new ArgumentException("Invalid axis specified.", nameof(axis));
        }
        return SDL_GetGamepadAppleSFSymbolsNameForAxis(gamepad, axis);
    }

    public static string GetGamepadAppleSFSymbolsNameForButton(nint gamepad, GamepadButton button) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (button == GamepadButton.Invalid) {
            throw new ArgumentException("Invalid button specified.", nameof(button));
        }
        return SDL_GetGamepadAppleSFSymbolsNameForButton(gamepad, button);
    }

    public static short GetGamepadAxis(nint gamepad, GamepadAxis axis) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (axis == GamepadAxis.Invalid) {
            throw new ArgumentException("Invalid axis specified.", nameof(axis));
        }
        return SDL_GetGamepadAxis(gamepad, axis);
    }

    public static GamepadAxis GetGamepadAxisFromString(string str) {
        if (string.IsNullOrWhiteSpace(str)) {
            throw new ArgumentException("Axis string cannot be null or empty.", nameof(str));
        }
        return SDL_GetGamepadAxisFromString(str);
    }

    public static bool GetGamepadButton(nint gamepad, GamepadButton button) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (button == GamepadButton.Invalid) {
            throw new ArgumentException("Invalid button specified.", nameof(button));
        }
        return SDL_GetGamepadButton(gamepad, button);
    }

    public static GamepadButton GetGamepadButtonFromString(string str) {
        if (string.IsNullOrWhiteSpace(str)) {
            throw new ArgumentException("Button string cannot be null or empty.", nameof(str));
        }
        return SDL_GetGamepadButtonFromString(str);
    }

    public static GamepadButtonLabel GetGamepadButtonLabel(nint gamepad, GamepadButton button) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (button == GamepadButton.Invalid) {
            throw new ArgumentException("Invalid button specified.", nameof(button));
        }
        return SDL_GetGamepadButtonLabel(gamepad, button);
    }

    public static GamepadButtonLabel GetGamepadButtonLabelForType(GamepadType type, GamepadButton button) {
        if (type == GamepadType.Unknown) {
            throw new ArgumentException("Invalid gamepad type specified.", nameof(type));
        }
        if (button == GamepadButton.Invalid) {
            throw new ArgumentException("Invalid button specified.", nameof(button));
        }
        return SDL_GetGamepadButtonLabelForType(type, button);
    }

    public static JoystickConnectionState GetGamepadConnectionState(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadConnectionState(gamepad);
    }

    public static ushort GetGamepadFirmwareVersion(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadFirmwareVersion(gamepad);
    }

    public static nint GetGamepadFromID(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetGamepadFromID(instanceId);
    }

    public static nint GetGamepadFromPlayerIndex(int playerIndex) {
        if (playerIndex < 0) {
            throw new ArgumentException("Player index cannot be negative.", nameof(playerIndex));
        }
        return SDL_GetGamepadFromPlayerIndex(playerIndex);
    }

    public static SdlGuid GetGamepadGUIDForID(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetGamepadGUIDForID(instanceId);
    }

    public static uint GetGamepadID(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadID(gamepad);
    }

    public static nint GetGamepadJoystick(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadJoystick(gamepad);
    }

    public static string GetGamepadMapping(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadMapping(gamepad);
    }

    public static string GetGamepadMappingForGUID(SdlGuid guid) {
        if (guid.Data == null) {
            throw new ArgumentException("GUID data cannot be null.", nameof(guid));
        }

        string mapping = SDL_GetGamepadMappingForGUID(guid);
        if (string.IsNullOrEmpty(mapping)) {
            throw new InvalidOperationException("No mapping found for the provided GUID.");
        }

        return mapping;
    }

    public static string GetGamepadMappingForID(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetGamepadMappingForID(instanceId);
    }

    public static string GetGamepadName(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadName(gamepad);
    }

    public static string GetGamepadNameForID(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetGamepadNameForID(instanceId);
    }

    public static string GetGamepadPath(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadPath(gamepad);
    }

    public static string GetGamepadPathForID(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetGamepadPathForID(instanceId);
    }

    public static int GetGamepadPlayerIndex(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadPlayerIndex(gamepad);
    }

    public static int GetGamepadPlayerIndexForID(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetGamepadPlayerIndexForID(instanceId);
    }

    public static PowerState GetGamepadPowerInfo(nint gamepad, out int percent) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadPowerInfo(gamepad, out percent);
    }

    public static ushort GetGamepadProduct(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadProduct(gamepad);
    }

    public static ushort GetGamepadProductForID(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetGamepadProductForID(instanceId);
    }

    public static ushort GetGamepadProductVersion(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadProductVersion(gamepad);
    }

    public static ushort GetGamepadProductVersionForID(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetGamepadProductVersionForID(instanceId);
    }

    public static uint GetGamepadProperties(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadProperties(gamepad);
    }

    public static float GetGamepadSensorDataRate(nint gamepad, SensorType type) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (type == SensorType.Invalid) {
            throw new ArgumentException("Invalid sensor type specified.", nameof(type));
        }
        return SDL_GetGamepadSensorDataRate(gamepad, type);
    }

    public static string GetGamepadSerial(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadSerial(gamepad);
    }

    public static ulong GetGamepadSteamHandle(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadSteamHandle(gamepad);
    }

    public static string GetGamepadStringForAxis(GamepadAxis axis) {
        if (axis == GamepadAxis.Invalid) {
            throw new ArgumentException("Invalid axis specified.", nameof(axis));
        }
        return SDL_GetGamepadStringForAxis(axis);
    }

    public static string GetGamepadStringForButton(GamepadButton button) {
        if (button == GamepadButton.Invalid) {
            throw new ArgumentException("Invalid button specified.", nameof(button));
        }
        return SDL_GetGamepadStringForButton(button);
    }

    public static string GetGamepadStringForType(GamepadType type) {
        if (type == GamepadType.Unknown) {
            throw new ArgumentException("Invalid gamepad type specified.", nameof(type));
        }
        return SDL_GetGamepadStringForType(type);
    }

    public static bool GetGamepadTouchpadFinger(nint gamepad, int touchpad, int finger,
        out bool down, out float x, out float y, out float pressure) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        var result = SDL_GetGamepadTouchpadFinger(gamepad, touchpad, finger, out var sdlDown, out x, out y, out pressure);
        down = sdlDown;
        return result;
    }

    public static GamepadType GetGamepadType(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadType(gamepad);
    }

    public static GamepadType GetGamepadTypeForID(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetGamepadTypeForID(instanceId);
    }

    public static GamepadType GetGamepadTypeFromString(string str) {
        if (string.IsNullOrWhiteSpace(str)) {
            throw new ArgumentException("Type string cannot be null or empty.", nameof(str));
        }
        return SDL_GetGamepadTypeFromString(str);
    }

    public static ushort GetGamepadVendor(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadVendor(gamepad);
    }

    public static ushort GetGamepadVendorForID(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetGamepadVendorForID(instanceId);
    }

    public static int GetNumGamepadTouchpadFingers(nint gamepad, int touchpad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetNumGamepadTouchpadFingers(gamepad, touchpad);
    }

    public static int GetNumGamepadTouchpads(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetNumGamepadTouchpads(gamepad);
    }

    public static GamepadType GetRealGamepadType(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetRealGamepadType(gamepad);
    }

    public static GamepadType GetRealGamepadTypeForID(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetRealGamepadTypeForID(instanceId);
    }

    public static bool IsGamepad(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_IsGamepad(instanceId);
    }

    public static nint OpenGamepad(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_OpenGamepad(instanceId);
    }

    public static bool RumbleGamepad(nint gamepad, ushort lowFrequencyRumble, ushort highFrequencyRumble, uint durationMs) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_RumbleGamepad(gamepad, lowFrequencyRumble, highFrequencyRumble, durationMs);
    }

    public static bool RumbleGamepadTriggers(nint gamepad, ushort leftRumble, ushort rightRumble, uint durationMs) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_RumbleGamepadTriggers(gamepad, leftRumble, rightRumble, durationMs);
    }

    public static bool SendGamepadEffect(nint gamepad, nint data, int size) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (data == nint.Zero) {
            throw new ArgumentException("Effect data cannot be null.", nameof(data));
        }
        if (size <= 0) {
            throw new ArgumentException("Effect size must be greater than zero.", nameof(size));
        }
        return SDL_SendGamepadEffect(gamepad, data, size);
    }

    public static void SetGamepadEventsEnabled(bool enabled) {
        SDL_SetGamepadEventsEnabled(enabled);

        LogInfo(LogCategory.System, $"Gamepad events enabled: {enabled}");
    }

    public static bool SetGamepadLED(nint gamepad, byte red, byte green, byte blue) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_SetGamepadLED(gamepad, red, green, blue);
    }

    public static bool SetGamepadMapping(uint instanceId, string mapping) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        if (string.IsNullOrWhiteSpace(mapping)) {
            throw new ArgumentException("Mapping cannot be null or empty.", nameof(mapping));
        }
        return SDL_SetGamepadMapping(instanceId, mapping);
    }

    public static bool SetGamepadPlayerIndex(nint gamepad, int playerIndex) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (playerIndex < 0) {
            throw new ArgumentException("Player index cannot be negative.", nameof(playerIndex));
        }
        return SDL_SetGamepadPlayerIndex(gamepad, playerIndex);
    }

    public static bool SetGamepadSensorEnabled(nint gamepad, SensorType type, bool enabled) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (type == SensorType.Invalid) {
            throw new ArgumentException("Invalid sensor type specified.", nameof(type));
        }
        return SDL_SetGamepadSensorEnabled(gamepad, type, enabled);
    }
    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_AddGamepadMapping(string mapping);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_AddGamepadMappingsFromFile(string file);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_AddGamepadMappingsFromIO(nint src, SdlBool closeio);
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_CloseGamepad(nint gamepad);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GamepadConnected(nint gamepad);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GamepadEventsEnabled();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GamepadHasAxis(nint gamepad, GamepadAxis axis);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GamepadHasButton(nint gamepad, GamepadButton button);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GamepadHasSensor(nint gamepad, SensorType type);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GamepadSensorEnabled(nint gamepad, SensorType type);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetGamepadAppleSFSymbolsNameForAxis(nint gamepad, GamepadAxis axis);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetGamepadAppleSFSymbolsNameForButton(nint gamepad, GamepadButton button);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial short SDL_GetGamepadAxis(nint gamepad, GamepadAxis axis);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadAxis SDL_GetGamepadAxisFromString(string str);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetGamepadBindings(nint gamepad, out int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetGamepadButton(nint gamepad, GamepadButton button);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadButton SDL_GetGamepadButtonFromString(string str);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadButtonLabel SDL_GetGamepadButtonLabel(nint gamepad, GamepadButton button);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadButtonLabel SDL_GetGamepadButtonLabelForType(GamepadType type,
        GamepadButton button);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial JoystickConnectionState SDL_GetGamepadConnectionState(nint gamepad);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadFirmwareVersion(nint gamepad);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetGamepadFromID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetGamepadFromPlayerIndex(int playerIndex);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlGuid SDL_GetGamepadGUIDForID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetGamepadID(nint gamepad);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetGamepadJoystick(nint gamepad);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CallerOwnedStringMarshaller))]
    private static partial string SDL_GetGamepadMapping(nint gamepad);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CallerOwnedStringMarshaller))]
    private static partial string SDL_GetGamepadMappingForGUID(SdlGuid guid);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CallerOwnedStringMarshaller))]
    private static partial string SDL_GetGamepadMappingForID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetGamepadMappings(out int count);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetGamepadName(nint gamepad);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetGamepadNameForID(uint instanceId);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetGamepadPath(nint gamepad);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetGamepadPathForID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetGamepadPlayerIndex(nint gamepad);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetGamepadPlayerIndexForID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PowerState SDL_GetGamepadPowerInfo(nint gamepad, out int percent);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadProduct(nint gamepad);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadProductForID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadProductVersion(nint gamepad);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadProductVersionForID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetGamepadProperties(nint gamepad);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetGamepads(out int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetGamepadSensorData(nint gamepad, SensorType type, nint data,
        int numValues);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetGamepadSensorDataRate(nint gamepad, SensorType type);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetGamepadSerial(nint gamepad);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong SDL_GetGamepadSteamHandle(nint gamepad);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetGamepadStringForAxis(GamepadAxis axis);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetGamepadStringForButton(GamepadButton button);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetGamepadStringForType(GamepadType type);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetGamepadTouchpadFinger(nint gamepad, int touchpad, int finger,
        out SdlBool down, out float x, out float y, out float pressure);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetGamepadType(nint gamepad);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetGamepadTypeForID(uint instanceId);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetGamepadTypeFromString(string str);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadVendor(nint gamepad);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadVendorForID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumGamepadTouchpadFingers(nint gamepad, int touchpad);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumGamepadTouchpads(nint gamepad);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetRealGamepadType(nint gamepad);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetRealGamepadTypeForID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasGamepad();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_IsGamepad(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenGamepad(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReloadGamepadMappings();
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RumbleGamepad(nint gamepad, ushort lowFrequencyRumble,
        ushort highFrequencyRumble, uint durationMs);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RumbleGamepadTriggers(nint gamepad, ushort leftRumble, ushort rightRumble,
        uint durationMs);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SendGamepadEffect(nint gamepad, nint data, int size);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetGamepadEventsEnabled(SdlBool enabled);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetGamepadLED(nint gamepad, byte red, byte green, byte blue);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetGamepadMapping(uint instanceId, string mapping);
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetGamepadPlayerIndex(nint gamepad, int playerIndex);
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetGamepadSensorEnabled(nint gamepad, SensorType type, SdlBool enabled);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UpdateGamepads();
}
