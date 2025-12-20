using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SharpSDL3;

public static unsafe partial class Sdl {
    /// <summary>Add support for gamepads that SDL is unaware of or change the binding of an existing gamepad.</summary>

    /// <param name="mapping">the mapping string.</param>
    /// <remarks>
    /// The mapping string has the format &quot;GUID,name,mapping&quot;, where GUID is the
    /// string value from <see cref="SdlGuid" />ToString(), name is the human
    /// readable string for the device and mappings are gamepad mappings to
    /// joystick ones. Under Windows there is a reserved GUID of &quot;xinput&quot; that
    /// covers all XInput devices. The mapping format for joystick is:
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="AddGamepadMappingsFromFile" />
    /// <seealso cref="AddGamepadMappingsFromIo" />
    /// <seealso cref="GetGamepadMapping" />
    /// <seealso cref="GetGamepadMappingForGuid" />
    /// <seealso cref="HINT_GAMECONTROLLERCONFIG" />
    /// <seealso cref="HINT_GAMECONTROLLERCONFIG_FILE" />
    /// <seealso cref="EVENT_GAMEPAD_ADDED" />
    /// </remarks>
    /// <returns>Returns 1 if a new mapping is added, 0 if an existing mapping isupdated, -1 on failure; call <see cref="GetError()" /> for more information.</returns>

    public static int AddGamepadMapping(string mapping) {
        if (string.IsNullOrWhiteSpace(mapping)) {
            throw new ArgumentException("Mapping cannot be null or empty.", nameof(mapping));
        }
        return SDL_AddGamepadMapping(mapping);
    }

    /// <summary>Load a set of gamepad mappings from a file.</summary>

    /// <param name="file">the mappings file to load.</param>
    /// <remarks>
    /// You can call this function several times, if needed, to load different
    /// database files.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="AddGamepadMapping" />
    /// <seealso cref="AddGamepadMappingsFromIo" />
    /// <seealso cref="GetGamepadMapping" />
    /// <seealso cref="GetGamepadMappingForGuid" />
    /// <seealso cref="HINT_GAMECONTROLLERCONFIG" />
    /// <seealso cref="HINT_GAMECONTROLLERCONFIG_FILE" />
    /// <seealso cref="EVENT_GAMEPAD_ADDED" />
    /// </remarks>
    /// <returns>Returns the number of mappings added or -1 on failure; call <see cref="GetError()" /> for more information.</returns>

    public static int AddGamepadMappingsFromFile(string file) {
        if (string.IsNullOrWhiteSpace(file)) {
            throw new ArgumentException("File path cannot be null or empty.", nameof(file));
        }
        return SDL_AddGamepadMappingsFromFile(file);
    }

    public static int AddGamepadMappingsFromIo(nint src, SdlBool closeio) {
        if (src == nint.Zero) {
            throw new ArgumentException("Source pointer cannot be null.", nameof(src));
        }
        return SDL_AddGamepadMappingsFromIO(src, closeio);
    }

    /// <summary>Close a gamepad previously opened with SDL_OpenGamepad().</summary>

    /// <param name="gamepad">a gamepad identifier previously returned by SDL_OpenGamepad().</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="OpenGamepad" />
    /// </remarks>

    public static void CloseGamepad(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        SDL_CloseGamepad(gamepad);
    }

    /// <summary>Check if a gamepad has been opened and is currently connected.</summary>

    /// <param name="gamepad">a gamepad identifier previously returned by SDL_OpenGamepad().</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the gamepad has been opened and is currentlyconnected, or <see langword="false" /> if not.</returns>

    public static bool GamepadConnected(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GamepadConnected(gamepad);
    }

    /// <summary>Query whether a gamepad has a given axis.</summary>

    /// <param name="gamepad">a gamepad.</param>
    /// <param name="axis">an axis enum value (an SDL_GamepadAxis value).</param>
    /// <remarks>
    /// This merely reports whether the gamepad's mapping defined this axis, as
    /// that is all the information SDL has about the physical device.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GamepadHasButton" />
    /// <seealso cref="GetGamepadAxis" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the gamepad has this axis, <see langword="false" /> otherwise.</returns>

    public static bool GamepadHasAxis(nint gamepad, GamepadAxis axis) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (axis == GamepadAxis.Invalid) {
            throw new ArgumentException("Invalid axis specified.", nameof(axis));
        }
        return SDL_GamepadHasAxis(gamepad, axis);
    }

    /// <summary>Query whether a gamepad has a given button.</summary>

    /// <param name="gamepad">a gamepad.</param>
    /// <param name="button">a button enum value (an SDL_GamepadButton value).</param>
    /// <remarks>
    /// This merely reports whether the gamepad's mapping defined this button, as
    /// that is all the information SDL has about the physical device.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GamepadHasAxis" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the gamepad has this button, <see langword="false" /> otherwise.</returns>

    public static bool GamepadHasButton(nint gamepad, GamepadButton button) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (button == GamepadButton.Invalid) {
            throw new ArgumentException("Invalid button specified.", nameof(button));
        }
        return SDL_GamepadHasButton(gamepad, button);
    }

    /// <summary>Return whether a gamepad has a particular sensor.</summary>

    /// <param name="gamepad">the gamepad to query.</param>
    /// <param name="type">the type of sensor to query.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadSensorData" />
    /// <seealso cref="GetGamepadSensorDataRate" />
    /// <seealso cref="SetGamepadSensorEnabled" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the sensor exists, <see langword="false" /> otherwise.</returns>

    public static bool GamepadHasSensor(nint gamepad, SensorType type) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (type == SensorType.Invalid) {
            throw new ArgumentException("Invalid sensor type specified.", nameof(type));
        }
        return SDL_GamepadHasSensor(gamepad, type);
    }

    /// <summary>Query whether sensor data reporting is enabled for a gamepad.</summary>

    /// <param name="gamepad">the gamepad to query.</param>
    /// <param name="type">the type of sensor to query.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetGamepadSensorEnabled" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the sensor is enabled, <see langword="false" /> otherwise.</returns>

    public static bool GamepadSensorEnabled(nint gamepad, SensorType type) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (type == SensorType.Invalid) {
            throw new ArgumentException("Invalid sensor type specified.", nameof(type));
        }
        return SDL_GamepadSensorEnabled(gamepad, type);
    }

    /// <summary>Return the sfSymbolsName for a given axis on a gamepad on Apple platforms.</summary>

    /// <param name="gamepad">the gamepad to query.</param>
    /// <param name="axis">an axis on the gamepad.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadAppleSfSymbolsNameForButton" />
    /// </remarks>
    /// <returns>Returns the sfSymbolsName or <see langword="null" /> if the name can't befound.</returns>

    public static string GetGamepadAppleSfSymbolsNameForAxis(nint gamepad, GamepadAxis axis) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (axis == GamepadAxis.Invalid) {
            throw new ArgumentException("Invalid axis specified.", nameof(axis));
        }
        return SDL_GetGamepadAppleSFSymbolsNameForAxis(gamepad, axis);
    }

    /// <summary>Return the sfSymbolsName for a given button on a gamepad on Apple platforms.</summary>

    /// <param name="gamepad">the gamepad to query.</param>
    /// <param name="button">a button on the gamepad.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadAppleSfSymbolsNameForAxis" />
    /// </remarks>
    /// <returns>Returns the sfSymbolsName or <see langword="null" /> if the name can't befound.</returns>

    public static string GetGamepadAppleSfSymbolsNameForButton(nint gamepad, GamepadButton button) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (button == GamepadButton.Invalid) {
            throw new ArgumentException("Invalid button specified.", nameof(button));
        }
        return SDL_GetGamepadAppleSFSymbolsNameForButton(gamepad, button);
    }

    /// <summary>Get the current state of an axis control on a gamepad.</summary>

    /// <param name="gamepad">a gamepad.</param>
    /// <param name="axis">an axis index (one of the SDL_GamepadAxis values).</param>
    /// <remarks>
    /// The axis indices start at index 0.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GamepadHasAxis" />
    /// <seealso cref="GetGamepadButton" />
    /// </remarks>
    /// <returns>Returns axis state.</returns>

    public static short GetGamepadAxis(nint gamepad, GamepadAxis axis) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (axis == GamepadAxis.Invalid) {
            throw new ArgumentException("Invalid axis specified.", nameof(axis));
        }
        return SDL_GetGamepadAxis(gamepad, axis);
    }

    /// <summary>Convert a string into SDL_GamepadAxis enum.</summary>

    /// <param name="str">string representing a SDL_Gamepad axis.</param>
    /// <remarks>
    /// This function is called internally to translate SDL_Gamepad
    /// mapping strings for the underlying joystick device into the consistent
    /// SDL_Gamepad mapping. You do not normally need to call this
    /// function unless you are parsing SDL_Gamepad mappings in your
    /// own code.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadStringForAxis" />
    /// </remarks>
    /// <returns>Returns theSDL_GamepadAxis enum corresponding to the input string,  SDL_GAMEPAD_AXIS_INVALID if no match wasfound.</returns>

    public static GamepadAxis GetGamepadAxisFromString(string str) {
        if (string.IsNullOrWhiteSpace(str)) {
            throw new ArgumentException("Axis string cannot be null or empty.", nameof(str));
        }
        return SDL_GetGamepadAxisFromString(str);
    }

    /// <summary>Get the current state of a button on a gamepad.</summary>

    /// <param name="gamepad">a gamepad.</param>
    /// <param name="button">a button index (one of the SDL_GamepadButton values).</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GamepadHasButton" />
    /// <seealso cref="GetGamepadAxis" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the button is pressed, <see langword="false" /> otherwise.</returns>

    public static bool GetGamepadButton(nint gamepad, GamepadButton button) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (button == GamepadButton.Invalid) {
            throw new ArgumentException("Invalid button specified.", nameof(button));
        }
        return SDL_GetGamepadButton(gamepad, button);
    }

    /// <summary>Convert a string into an SDL_GamepadButton enum.</summary>

    /// <param name="str">string representing a SDL_Gamepad axis.</param>
    /// <remarks>
    /// This function is called internally to translate SDL_Gamepad
    /// mapping strings for the underlying joystick device into the consistent
    /// SDL_Gamepad mapping. You do not normally need to call this
    /// function unless you are parsing SDL_Gamepad mappings in your
    /// own code.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadStringForButton" />
    /// </remarks>
    /// <returns>Returns theSDL_GamepadButton enum corresponding to the inputstring, or SDL_GAMEPAD_BUTTON_INVALID if nomatch was found.</returns>

    public static GamepadButton GetGamepadButtonFromString(string str) {
        if (string.IsNullOrWhiteSpace(str)) {
            throw new ArgumentException("Button string cannot be null or empty.", nameof(str));
        }
        return SDL_GetGamepadButtonFromString(str);
    }

    /// <summary>Get the label of a button on a gamepad.</summary>

    /// <param name="gamepad">a gamepad.</param>
    /// <param name="button">a button index (one of the SDL_GamepadButton values).</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadButtonLabelForType" />
    /// </remarks>
    /// <returns>Returns theSDL_GamepadButtonLabel enum corresponding to thebutton label.</returns>

    public static GamepadButtonLabel GetGamepadButtonLabel(nint gamepad, GamepadButton button) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (button == GamepadButton.Invalid) {
            throw new ArgumentException("Invalid button specified.", nameof(button));
        }
        return SDL_GetGamepadButtonLabel(gamepad, button);
    }

    /// <summary>Get the label of a button on a gamepad.</summary>

    /// <param name="type">the type of gamepad to check.</param>
    /// <param name="button">a button index (one of the SDL_GamepadButton values).</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadButtonLabel" />
    /// </remarks>
    /// <returns>Returns theSDL_GamepadButtonLabel enum corresponding to thebutton label.</returns>

    public static GamepadButtonLabel GetGamepadButtonLabelForType(GamepadType type, GamepadButton button) {
        if (type == GamepadType.Unknown) {
            throw new ArgumentException("Invalid gamepad type specified.", nameof(type));
        }
        if (button == GamepadButton.Invalid) {
            throw new ArgumentException("Invalid button specified.", nameof(button));
        }
        return SDL_GetGamepadButtonLabelForType(type, button);
    }

    /// <summary>Get the connection state of a gamepad.</summary>

    /// <param name="gamepad">the gamepad object to query.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns theconnection state on success orSDL_JOYSTICK_CONNECTION_INVALID on failure; call <see cref="GetError()" /> for more information.</returns>

    public static JoystickConnectionState GetGamepadConnectionState(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadConnectionState(gamepad);
    }

    /// <summary>Get the firmware version of an opened gamepad, if available.</summary>

    /// <param name="gamepad">the gamepad object to query.</param>
    /// <remarks>
    /// If the firmware version isn't available this function returns 0.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the gamepad firmware version, or zero ifunavailable.</returns>

    public static ushort GetGamepadFirmwareVersion(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadFirmwareVersion(gamepad);
    }

    /// <summary>Get the SDL_Gamepad associated with a joystick instance ID, if it has been opened.</summary>

    /// <param name="instance_id">the joystick instance ID of the gamepad.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(SDL_Gamepad *) Returns an SDL_Gamepad on success or <see langword="null" /> on failure or if it hasn't been opened yet; call <see cref="GetError()" /> for more information.</returns>

    public static nint GetGamepadFromId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetGamepadFromID(instanceId);
    }

    /// <summary>Get the SDL_Gamepad associated with a player index.</summary>

    /// <param name="player_index">the player index, which different from the instance ID.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadPlayerIndex" />
    /// <seealso cref="SetGamepadPlayerIndex" />
    /// </remarks>
    /// <returns>(SDL_Gamepad *) Returns the SDL_Gamepadassociated with a player index.</returns>

    public static nint GetGamepadFromPlayerIndex(int playerIndex) {
        if (playerIndex < 0) {
            throw new ArgumentException("Player index cannot be negative.", nameof(playerIndex));
        }
        return SDL_GetGamepadFromPlayerIndex(playerIndex);
    }

    /// <summary>Get the implementation-dependent GUID of a gamepad.</summary>

    /// <param name="instance_id">the joystick instance ID.</param>
    /// <remarks>
    /// This can be called before any gamepads are opened.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GUIDToString" />
    /// <seealso cref="GetGamepads" />
    /// </remarks>
    /// <returns>Returns the GUID of the selected gamepad. If calledon an invalid index, this function returns a zero GUID.</returns>

    public static SdlGuid GetGamepadGuidForId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetGamepadGUIDForID(instanceId);
    }

    /// <summary>Get the instance ID of an opened gamepad.</summary>

    /// <param name="gamepad">a gamepad identifier previously returned by SDL_OpenGamepad().</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the instance ID of the specifiedgamepad on success or 0 on failure; call <see cref="GetError()" /> for more information.</returns>

    public static uint GetGamepadId(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadID(gamepad);
    }

    /// <summary>Get the underlying joystick from a gamepad.</summary>

    /// <param name="gamepad">the gamepad object that you want to get a joystick from.</param>
    /// <remarks>
    /// This function will give you a SDL_Joystick object, which
    /// allows you to use the SDL_Joystick functions with a
    /// SDL_Gamepad object. This would be useful for getting a
    /// joystick's position at any given time, even if it hasn't moved (moving it
    /// would produce an event, which would have the axis' value).
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(SDL_Joystick *) Returns an SDL_Joystickobject, or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint GetGamepadJoystick(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadJoystick(gamepad);
    }

    /// <summary>Get the current mapping of a gamepad.</summary>

    /// <param name="gamepad">the gamepad you want to get the current mapping for.</param>
    /// <remarks>
    /// Details about mappings are discussed with
    /// SDL_AddGamepadMapping().
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="AddGamepadMapping" />
    /// <seealso cref="GetGamepadMappingForId" />
    /// <seealso cref="GetGamepadMappingForGuid" />
    /// <seealso cref="SetGamepadMapping" />
    /// </remarks>
    /// <returns>(char *) Returns a string that has the gamepad's mapping or <see langword="null" /> if nomapping is available; call <see cref="GetError()" /> for more information. This should be freed with <see cref="Free" /> when it is no longer needed.</returns>

    public static string GetGamepadMapping(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadMapping(gamepad);
    }

    /// <summary>Get the gamepad mapping string for a given GUID.</summary>

    /// <param name="guid">a structure containing the GUID for which a mapping is desired.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoystickGUIDForID" />
    /// <seealso cref="GetJoystickGUID" />
    /// </remarks>
    /// <returns>(char *) Returns a mapping string or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information. This should be freedwith <see cref="Free" /> when it is no longer needed.</returns>

    public static string GetGamepadMappingForGuid(SdlGuid guid) {
        if (guid.Data == null) {
            throw new ArgumentException("GUID data cannot be null.", nameof(guid));
        }

        string mapping = SDL_GetGamepadMappingForGUID(guid);
        if (string.IsNullOrEmpty(mapping)) {
            throw new InvalidOperationException("No mapping found for the provided GUID.");
        }

        return mapping;
    }

    /// <summary>Get the mapping of a gamepad.</summary>

    /// <param name="instance_id">the joystick instance ID.</param>
    /// <remarks>
    /// This can be called before any gamepads are opened.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepads" />
    /// <seealso cref="GetGamepadMapping" />
    /// </remarks>
    /// <returns>(char *) Returns the mapping string. Returns <see langword="null" /> if no mapping isavailable. This should be freed with <see cref="Free" /> when it is no longer needed.</returns>

    public static string GetGamepadMappingForId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetGamepadMappingForID(instanceId);
    }

    /// <summary>Get the implementation-dependent name for an opened gamepad.</summary>

    /// <param name="gamepad">a gamepad identifier previously returned by SDL_OpenGamepad().</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadNameForId" />
    /// </remarks>
    /// <returns>Returns the implementation dependent name for the gamepad,  <see langword="null" /> if there is no name or the identifier passed is invalid.</returns>

    public static string GetGamepadName(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadName(gamepad);
    }

    /// <summary>Get the implementation dependent name of a gamepad.</summary>

    /// <param name="instance_id">the joystick instance ID.</param>
    /// <remarks>
    /// This can be called before any gamepads are opened.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadName" />
    /// <seealso cref="GetGamepads" />
    /// </remarks>
    /// <returns>Returns the name of the selected gamepad. If no name can befound, this function returns <see langword="null" />; call <see cref="GetError()" /> for more information.</returns>

    public static string GetGamepadNameForId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetGamepadNameForID(instanceId);
    }

    /// <summary>Get the implementation-dependent path for an opened gamepad.</summary>

    /// <param name="gamepad">a gamepad identifier previously returned by SDL_OpenGamepad().</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadPathForId" />
    /// </remarks>
    /// <returns>Returns the implementation dependent path for the gamepad,  <see langword="null" /> if there is no path or the identifier passed is invalid.</returns>

    public static string GetGamepadPath(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadPath(gamepad);
    }

    /// <summary>Get the implementation dependent path of a gamepad.</summary>

    /// <param name="instance_id">the joystick instance ID.</param>
    /// <remarks>
    /// This can be called before any gamepads are opened.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadPath" />
    /// <seealso cref="GetGamepads" />
    /// </remarks>
    /// <returns>Returns the path of the selected gamepad. If no path can befound, this function returns <see langword="null" />; call <see cref="GetError()" /> for more information.</returns>

    public static string GetGamepadPathForId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetGamepadPathForID(instanceId);
    }

    /// <summary>Get the player index of an opened gamepad.</summary>

    /// <param name="gamepad">the gamepad object to query.</param>
    /// <remarks>
    /// For XInput gamepads this returns the XInput user index.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetGamepadPlayerIndex" />
    /// </remarks>
    /// <returns>Returns the player index for gamepad, or -1 if it's not available.</returns>

    public static int GetGamepadPlayerIndex(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadPlayerIndex(gamepad);
    }

    /// <summary>Get the player index of a gamepad.</summary>

    /// <param name="instance_id">the joystick instance ID.</param>
    /// <remarks>
    /// This can be called before any gamepads are opened.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadPlayerIndex" />
    /// <seealso cref="GetGamepads" />
    /// </remarks>
    /// <returns>Returns the player index of a gamepad, or -1 if it's not available.</returns>

    public static int GetGamepadPlayerIndexForId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetGamepadPlayerIndexForID(instanceId);
    }

    /// <summary>Get the battery state of a gamepad.</summary>

    /// <param name="gamepad">the gamepad object to query.</param>
    /// <param name="percent">a pointer filled in with the percentage of battery life left, between 0 and 100, or discard to ignore. This will be filled in with -1 we can't determine a value or there is no battery.</param>
    /// <remarks>
    /// You should never take a battery status as absolute truth. Batteries
    /// (especially failing batteries) are delicate hardware, and the values
    /// reported here are best estimates based on what that hardware reports. It's
    /// not uncommon for older batteries to lose stored power much faster than it
    /// reports, or completely drain when reporting it has 20 percent left, etc.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the current battery state.</returns>

    public static PowerState GetGamepadPowerInfo(nint gamepad, out int percent) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadPowerInfo(gamepad, out percent);
    }

    /// <summary>Get the USB product ID of an opened gamepad, if available.</summary>

    /// <param name="gamepad">the gamepad object to query.</param>
    /// <remarks>
    /// If the product ID isn't available this function returns 0.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadProductForId" />
    /// </remarks>
    /// <returns>Returns the USB product ID, or zero if unavailable.</returns>

    public static ushort GetGamepadProduct(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadProduct(gamepad);
    }

    /// <summary>Get the USB product ID of a gamepad, if available.</summary>

    /// <param name="instance_id">the joystick instance ID.</param>
    /// <remarks>
    /// This can be called before any gamepads are opened. If the product ID isn't
    /// available this function returns 0.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadProduct" />
    /// <seealso cref="GetGamepads" />
    /// </remarks>
    /// <returns>Returns the USB product ID of the selected gamepad. Ifcalled on an invalid index, this function returns zero.</returns>

    public static ushort GetGamepadProductForId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetGamepadProductForID(instanceId);
    }

    /// <summary>Get the product version of an opened gamepad, if available.</summary>

    /// <param name="gamepad">the gamepad object to query.</param>
    /// <remarks>
    /// If the product version isn't available this function returns 0.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadProductVersionForId" />
    /// </remarks>
    /// <returns>Returns the USB product version, or zero if unavailable.</returns>

    public static ushort GetGamepadProductVersion(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadProductVersion(gamepad);
    }

    /// <summary>Get the product version of a gamepad, if available.</summary>

    /// <param name="instance_id">the joystick instance ID.</param>
    /// <remarks>
    /// This can be called before any gamepads are opened. If the product version
    /// isn't available this function returns 0.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadProductVersion" />
    /// <seealso cref="GetGamepads" />
    /// </remarks>
    /// <returns>Returns the product version of the selected gamepad. Ifcalled on an invalid index, this function returns zero.</returns>

    public static ushort GetGamepadProductVersionForId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetGamepadProductVersionForID(instanceId);
    }

    /// <summary>Get the properties associated with an opened gamepad.</summary>

    /// <param name="gamepad">a gamepad identifier previously returned by SDL_OpenGamepad().</param>
    /// <remarks>
    /// These properties are shared with the underlying joystick object.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns a valid property ID on success or 0 on failure; call <see cref="GetError()" /> for more information.</returns>

    public static uint GetGamepadProperties(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadProperties(gamepad);
    }

    /// <summary>Get the data rate (number of events per second) of a gamepad sensor.</summary>

    /// <param name="gamepad">the gamepad to query.</param>
    /// <param name="type">the type of sensor to query.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the data rate, or 0.0f if the data rate is not available.</returns>

    public static float GetGamepadSensorDataRate(nint gamepad, SensorType type) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (type == SensorType.Invalid) {
            throw new ArgumentException("Invalid sensor type specified.", nameof(type));
        }
        return SDL_GetGamepadSensorDataRate(gamepad, type);
    }

    /// <summary>Get the serial number of an opened gamepad, if available.</summary>

    /// <param name="gamepad">the gamepad object to query.</param>
    /// <remarks>
    /// Returns the serial number of the gamepad, or <see langword="null" /> if it is not available.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the serial number, or <see langword="null" /> if unavailable.</returns>

    public static string GetGamepadSerial(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadSerial(gamepad);
    }

    /// <summary>Get the Steam Input handle of an opened gamepad, if available.</summary>

    /// <param name="gamepad">the gamepad object to query.</param>
    /// <remarks>
    /// Returns an InputHandle_t for the gamepad that can be used with Steam Input
    /// API: https://partner.steamgames.com/doc/api/ISteamInput
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the gamepad handle, or 0 if unavailable.</returns>

    public static ulong GetGamepadSteamHandle(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadSteamHandle(gamepad);
    }

    /// <summary>Convert from an SDL_GamepadAxis enum to a string.</summary>

    /// <param name="axis">an enum value for a given SDL_GamepadAxis.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadAxisFromString" />
    /// </remarks>
    /// <returns>Returns a string for the given axis, or <see langword="null" /> if an invalidaxis is specified. The string returned is of the format used bySDL_Gamepad mapping strings.</returns>

    public static string GetGamepadStringForAxis(GamepadAxis axis) {
        if (axis == GamepadAxis.Invalid) {
            throw new ArgumentException("Invalid axis specified.", nameof(axis));
        }
        return SDL_GetGamepadStringForAxis(axis);
    }

    /// <summary>Convert from an SDL_GamepadButton enum to a string.</summary>

    /// <param name="button">an enum value for a given SDL_GamepadButton.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadButtonFromString" />
    /// </remarks>
    /// <returns>Returns a string for the given button, or <see langword="null" /> if an invalidbutton is specified. The string returned is of the format used bySDL_Gamepad mapping strings.</returns>

    public static string GetGamepadStringForButton(GamepadButton button) {
        if (button == GamepadButton.Invalid) {
            throw new ArgumentException("Invalid button specified.", nameof(button));
        }
        return SDL_GetGamepadStringForButton(button);
    }

    /// <summary>Convert from an SDL_GamepadType enum to a string.</summary>

    /// <param name="type">an enum value for a given SDL_GamepadType.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadTypeFromString" />
    /// </remarks>
    /// <returns>Returns a string for the given type, or <see langword="null" /> if an invalidtype is specified. The string returned is of the format used bySDL_Gamepad mapping strings.</returns>

    public static string GetGamepadStringForType(GamepadType type) {
        if (type == GamepadType.Unknown) {
            throw new ArgumentException("Invalid gamepad type specified.", nameof(type));
        }
        return SDL_GetGamepadStringForType(type);
    }

    /// <summary>Get the current state of a finger on a touchpad on a gamepad.</summary>

    /// <param name="gamepad">a gamepad.</param>
    /// <param name="touchpad">a touchpad.</param>
    /// <param name="finger">a finger.</param>
    /// <param name="down">a pointer filled with <see langword="true" /> if the finger is down, <see langword="false" /> otherwise, may be discarded.</param>
    /// <param name="x">a pointer filled with the x position, normalized 0 to 1, with the origin in the upper left, may be discarded.</param>
    /// <param name="y">a pointer filled with the y position, normalized 0 to 1, with the origin in the upper left, may be discarded.</param>
    /// <param name="pressure">a pointer filled with pressure value, may be discarded.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetNumGamepadTouchpadFingers" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool GetGamepadTouchpadFinger(nint gamepad, int touchpad, int finger,
            out bool down, out float x, out float y, out float pressure) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        SdlBool result = SDL_GetGamepadTouchpadFinger(gamepad, touchpad, finger, out SdlBool sdlDown, out x, out y, out pressure);
        down = sdlDown;
        return result;
    }

    /// <summary>Get the type of an opened gamepad.</summary>

    /// <param name="gamepad">the gamepad object to query.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadTypeForId" />
    /// </remarks>
    /// <returns>Returns the gamepad type, orSDL_GAMEPAD_TYPE_UNKNOWN if it's not available.</returns>

    public static GamepadType GetGamepadType(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadType(gamepad);
    }

    /// <summary>Get the type of a gamepad.</summary>

    /// <param name="instance_id">the joystick instance ID.</param>
    /// <remarks>
    /// This can be called before any gamepads are opened.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadType" />
    /// <seealso cref="GetGamepads" />
    /// <seealso cref="GetRealGamepadTypeForId" />
    /// </remarks>
    /// <returns>Returns the gamepad type.</returns>

    public static GamepadType GetGamepadTypeForId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetGamepadTypeForID(instanceId);
    }

    /// <summary>Convert a string into SDL_GamepadType enum.</summary>

    /// <param name="str">string representing a SDL_GamepadType type.</param>
    /// <remarks>
    /// This function is called internally to translate SDL_Gamepad
    /// mapping strings for the underlying joystick device into the consistent
    /// SDL_Gamepad mapping. You do not normally need to call this
    /// function unless you are parsing SDL_Gamepad mappings in your
    /// own code.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadStringForType" />
    /// </remarks>
    /// <returns>Returns theSDL_GamepadType enum corresponding to the input string,  SDL_GAMEPAD_TYPE_UNKNOWN if no match wasfound.</returns>

    public static GamepadType GetGamepadTypeFromString(string str) {
        if (string.IsNullOrWhiteSpace(str)) {
            throw new ArgumentException("Type string cannot be null or empty.", nameof(str));
        }
        return SDL_GetGamepadTypeFromString(str);
    }

    /// <summary>Get the USB vendor ID of an opened gamepad, if available.</summary>

    /// <param name="gamepad">the gamepad object to query.</param>
    /// <remarks>
    /// If the vendor ID isn't available this function returns 0.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadVendorForId" />
    /// </remarks>
    /// <returns>Returns the USB vendor ID, or zero if unavailable.</returns>

    public static ushort GetGamepadVendor(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetGamepadVendor(gamepad);
    }

    /// <summary>Get the USB vendor ID of a gamepad, if available.</summary>

    /// <param name="instance_id">the joystick instance ID.</param>
    /// <remarks>
    /// This can be called before any gamepads are opened. If the vendor ID isn't
    /// available this function returns 0.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadVendor" />
    /// <seealso cref="GetGamepads" />
    /// </remarks>
    /// <returns>Returns the USB vendor ID of the selected gamepad. Ifcalled on an invalid index, this function returns zero.</returns>

    public static ushort GetGamepadVendorForId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetGamepadVendorForID(instanceId);
    }

    /// <summary>Get the number of supported simultaneous fingers on a touchpad on a game gamepad.</summary>

    /// <param name="gamepad">a gamepad.</param>
    /// <param name="touchpad">a touchpad.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadTouchpadFinger" />
    /// <seealso cref="GetNumGamepadTouchpads" />
    /// </remarks>
    /// <returns>Returns number of supported simultaneous fingers.</returns>

    public static int GetNumGamepadTouchpadFingers(nint gamepad, int touchpad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetNumGamepadTouchpadFingers(gamepad, touchpad);
    }

    /// <summary>Get the number of touchpads on a gamepad.</summary>

    /// <param name="gamepad">a gamepad.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetNumGamepadTouchpadFingers" />
    /// </remarks>
    /// <returns>Returns number of touchpads.</returns>

    public static int GetNumGamepadTouchpads(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetNumGamepadTouchpads(gamepad);
    }

    /// <summary>Get the type of an opened gamepad, ignoring any mapping override.</summary>

    /// <param name="gamepad">the gamepad object to query.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRealGamepadTypeForId" />
    /// </remarks>
    /// <returns>Returns the gamepad type, orSDL_GAMEPAD_TYPE_UNKNOWN if it's not available.</returns>

    public static GamepadType GetRealGamepadType(nint gamepad) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_GetRealGamepadType(gamepad);
    }

    /// <summary>Get the type of a gamepad, ignoring any mapping override.</summary>

    /// <param name="instance_id">the joystick instance ID.</param>
    /// <remarks>
    /// This can be called before any gamepads are opened.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadTypeForId" />
    /// <seealso cref="GetGamepads" />
    /// <seealso cref="GetRealGamepadType" />
    /// </remarks>
    /// <returns>Returns the gamepad type.</returns>

    public static GamepadType GetRealGamepadTypeForId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetRealGamepadTypeForID(instanceId);
    }

    /// <summary>Check if the given joystick is supported by the gamepad interface.</summary>

    /// <param name="instance_id">the joystick instance ID.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoysticks" />
    /// <seealso cref="OpenGamepad" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the given joystick is supported by the gamepadinterface, <see langword="false" /> if it isn't or it's an invalid index.</returns>

    public static bool IsGamepad(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_IsGamepad(instanceId);
    }

    /// <summary>Open a gamepad for use.</summary>

    /// <param name="instance_id">the joystick instance ID.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CloseGamepad" />
    /// <seealso cref="IsGamepad" />
    /// </remarks>
    /// <returns>(SDL_Gamepad *) Returns a gamepad identifier or <see langword="null" /> if anerror occurred; call <see cref="GetError()" /> for more information.</returns>

    public static nint OpenGamepad(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_OpenGamepad(instanceId);
    }

    /// <summary>Start a rumble effect on a gamepad.</summary>

    /// <param name="gamepad">the gamepad to vibrate.</param>
    /// <param name="low_frequency_rumble">the intensity of the low frequency (left) rumble motor, from 0 to 0xFFFF.</param>
    /// <param name="high_frequency_rumble">the intensity of the high frequency (right) rumble motor, from 0 to 0xFFFF.</param>
    /// <param name="duration_ms">the duration of the rumble effect, in milliseconds.</param>
    /// <remarks>
    /// Each call to this function cancels any previous rumble effect, and calling
    /// it with 0 intensity stops any rumbling.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RumbleGamepad(nint gamepad, ushort lowFrequencyRumble, ushort highFrequencyRumble, uint durationMs) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_RumbleGamepad(gamepad, lowFrequencyRumble, highFrequencyRumble, durationMs);
    }

    /// <summary>Start a rumble effect in the gamepad's triggers.</summary>

    /// <param name="gamepad">the gamepad to vibrate.</param>
    /// <param name="left_rumble">the intensity of the left trigger rumble motor, from 0 to 0xFFFF.</param>
    /// <param name="right_rumble">the intensity of the right trigger rumble motor, from 0 to 0xFFFF.</param>
    /// <param name="duration_ms">the duration of the rumble effect, in milliseconds.</param>
    /// <remarks>
    /// Each call to this function cancels any previous trigger rumble effect, and
    /// calling it with 0 intensity stops any rumbling.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RumbleGamepad" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RumbleGamepadTriggers(nint gamepad, ushort leftRumble, ushort rightRumble, uint durationMs) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_RumbleGamepadTriggers(gamepad, leftRumble, rightRumble, durationMs);
    }

    /// <summary>Send a gamepad specific effect packet.</summary>

    /// <param name="gamepad">the gamepad to affect.</param>
    /// <param name="data">the data to send to the gamepad.</param>
    /// <param name="size">the size of the data to send to the gamepad.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

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

    /// <summary>Set the state of gamepad event processing.</summary>

    /// <param name="enabled">whether to process gamepad events or not.</param>
    /// <remarks>
    /// If gamepad events are disabled, you must call
    /// SDL_UpdateGamepads() yourself and check the state of
    /// the gamepad when you want gamepad information.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GamepadEventsEnabled" />
    /// <seealso cref="UpdateGamepads" />
    /// </remarks>

    public static void SetGamepadEventsEnabled(bool enabled) {
        SDL_SetGamepadEventsEnabled(enabled);

        LogInfo(LogCategory.System, $"Gamepad events enabled: {enabled}");
    }

    /// <summary>Update a gamepad's LED color.</summary>

    /// <param name="gamepad">the gamepad to update.</param>
    /// <param name="red">the intensity of the red LED.</param>
    /// <param name="green">the intensity of the green LED.</param>
    /// <param name="blue">the intensity of the blue LED.</param>
    /// <remarks>
    /// An example of a joystick LED is the light on the back of a PlayStation 4's
    /// DualShock 4 controller.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool SetGamepadLed(nint gamepad, byte red, byte green, byte blue) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        return SDL_SetGamepadLED(gamepad, red, green, blue);
    }

    /// <summary>Set the current mapping of a joystick or gamepad.</summary>

    /// <param name="instance_id">the joystick instance ID.</param>
    /// <param name="mapping">the mapping to use for this device, or <see langword="null" /> to clear the mapping.</param>
    /// <remarks>
    /// Details about mappings are discussed with
    /// SDL_AddGamepadMapping().
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="AddGamepadMapping" />
    /// <seealso cref="GetGamepadMapping" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool SetGamepadMapping(uint instanceId, string mapping) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        if (string.IsNullOrWhiteSpace(mapping)) {
            throw new ArgumentException("Mapping cannot be null or empty.", nameof(mapping));
        }
        return SDL_SetGamepadMapping(instanceId, mapping);
    }

    /// <summary>Set the player index of an opened gamepad.</summary>

    /// <param name="gamepad">the gamepad object to adjust.</param>
    /// <param name="player_index">player index to assign to this gamepad, or -1 to clear the player index and turn off player LEDs.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetGamepadPlayerIndex" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool SetGamepadPlayerIndex(nint gamepad, int playerIndex) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (playerIndex < 0) {
            throw new ArgumentException("Player index cannot be negative.", nameof(playerIndex));
        }
        return SDL_SetGamepadPlayerIndex(gamepad, playerIndex);
    }

    /// <summary>Set whether data reporting for a gamepad sensor is enabled.</summary>

    /// <param name="gamepad">the gamepad to update.</param>
    /// <param name="type">the type of sensor to enable/disable.</param>
    /// <param name="enabled">whether data reporting should be enabled.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GamepadHasSensor" />
    /// <seealso cref="GamepadSensorEnabled" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool SetGamepadSensorEnabled(nint gamepad, SensorType type, bool enabled) {
        if (gamepad == nint.Zero) {
            throw new ArgumentException("Gamepad handle cannot be null.", nameof(gamepad));
        }
        if (type == SensorType.Invalid) {
            throw new ArgumentException("Invalid sensor type specified.", nameof(type));
        }
        return SDL_SetGamepadSensorEnabled(gamepad, type, enabled);
    }

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_AddGamepadMapping(string mapping);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_AddGamepadMappingsFromFile(string file);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_AddGamepadMappingsFromIO(nint src, SdlBool closeio);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_CloseGamepad(nint gamepad);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GamepadConnected(nint gamepad);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GamepadEventsEnabled();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GamepadHasAxis(nint gamepad, GamepadAxis axis);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GamepadHasButton(nint gamepad, GamepadButton button);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GamepadHasSensor(nint gamepad, SensorType type);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GamepadSensorEnabled(nint gamepad, SensorType type);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetGamepadAppleSFSymbolsNameForAxis(nint gamepad, GamepadAxis axis);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetGamepadAppleSFSymbolsNameForButton(nint gamepad, GamepadButton button);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial short SDL_GetGamepadAxis(nint gamepad, GamepadAxis axis);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadAxis SDL_GetGamepadAxisFromString(string str);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetGamepadBindings(nint gamepad, out int count);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetGamepadButton(nint gamepad, GamepadButton button);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadButton SDL_GetGamepadButtonFromString(string str);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadButtonLabel SDL_GetGamepadButtonLabel(nint gamepad, GamepadButton button);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadButtonLabel SDL_GetGamepadButtonLabelForType(GamepadType type,
        GamepadButton button);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial JoystickConnectionState SDL_GetGamepadConnectionState(nint gamepad);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadFirmwareVersion(nint gamepad);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetGamepadFromID(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetGamepadFromPlayerIndex(int playerIndex);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlGuid SDL_GetGamepadGUIDForID(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetGamepadID(nint gamepad);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetGamepadJoystick(nint gamepad);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CallerOwnedStringMarshaller))]
    private static partial string SDL_GetGamepadMapping(nint gamepad);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CallerOwnedStringMarshaller))]
    private static partial string SDL_GetGamepadMappingForGUID(SdlGuid guid);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CallerOwnedStringMarshaller))]
    private static partial string SDL_GetGamepadMappingForID(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetGamepadMappings(out int count);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetGamepadName(nint gamepad);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetGamepadNameForID(uint instanceId);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetGamepadPath(nint gamepad);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetGamepadPathForID(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetGamepadPlayerIndex(nint gamepad);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetGamepadPlayerIndexForID(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PowerState SDL_GetGamepadPowerInfo(nint gamepad, out int percent);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadProduct(nint gamepad);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadProductForID(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadProductVersion(nint gamepad);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadProductVersionForID(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetGamepadProperties(nint gamepad);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetGamepads(out int count);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetGamepadSensorData(nint gamepad, SensorType type, nint data,
        int numValues);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetGamepadSensorDataRate(nint gamepad, SensorType type);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetGamepadSerial(nint gamepad);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong SDL_GetGamepadSteamHandle(nint gamepad);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetGamepadStringForAxis(GamepadAxis axis);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetGamepadStringForButton(GamepadButton button);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetGamepadStringForType(GamepadType type);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetGamepadTouchpadFinger(nint gamepad, int touchpad, int finger,
        out SdlBool down, out float x, out float y, out float pressure);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetGamepadType(nint gamepad);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetGamepadTypeForID(uint instanceId);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetGamepadTypeFromString(string str);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadVendor(nint gamepad);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadVendorForID(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumGamepadTouchpadFingers(nint gamepad, int touchpad);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumGamepadTouchpads(nint gamepad);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetRealGamepadType(nint gamepad);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetRealGamepadTypeForID(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasGamepad();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_IsGamepad(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenGamepad(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReloadGamepadMappings();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RumbleGamepad(nint gamepad, ushort lowFrequencyRumble,
        ushort highFrequencyRumble, uint durationMs);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RumbleGamepadTriggers(nint gamepad, ushort leftRumble, ushort rightRumble,
        uint durationMs);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SendGamepadEffect(nint gamepad, nint data, int size);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetGamepadEventsEnabled(SdlBool enabled);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetGamepadLED(nint gamepad, byte red, byte green, byte blue);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetGamepadMapping(uint instanceId, string mapping);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetGamepadPlayerIndex(nint gamepad, int playerIndex);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetGamepadSensorEnabled(nint gamepad, SensorType type, SdlBool enabled);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UpdateGamepads();
}