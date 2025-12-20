using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SharpSDL3;

public static unsafe partial class Sdl {
    /// <summary>Attach a new virtual joystick.</summary>

    /// <param name="desc">joystick description, initialized using SDL_INIT_INTERFACE().</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="DetachVirtualJoystick" />
    /// </remarks>
    /// <returns>Returns the joystick instance ID, or 0on failure; call <see cref="GetError()" /> for more information.</returns>

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

    /// <summary>Close a joystick previously opened with SDL_OpenJoystick().</summary>

    /// <param name="joystick">the joystick device to close.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="OpenJoystick" />
    /// </remarks>

    public static void CloseJoystick(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        SDL_CloseJoystick(joystick);
    }

    /// <summary>Detach a virtual joystick.</summary>

    /// <param name="instance_id">the joystick instance ID, previously returned from SDL_AttachVirtualJoystick().</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="AttachVirtualJoystick" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

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

    /// <summary>Get the current state of an axis control on a joystick.</summary>

    /// <param name="joystick">an SDL_Joystick structure containing joystick information.</param>
    /// <param name="axis">the axis to query; the axis indices start at index 0.</param>
    /// <remarks>
    /// SDL makes no promises about what part of the joystick any given axis refers
    /// to. Your game should have some sort of configuration UI to let users
    /// specify what each axis should be bound to. Alternately, SDL's higher-level
    /// Game Controller API makes a great effort to apply order to this lower-level
    /// interface, so you know that a specific axis is the &quot;left thumb stick,&quot; etc.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetNumJoystickAxes" />
    /// </remarks>
    /// <returns>Returns a 16-bit signed integer representing the current position of the axis or 0 on failure; call <see cref="GetError()" />for more information.</returns>

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

    /// <summary>Get the initial state of an axis control on a joystick.</summary>

    /// <param name="joystick">an SDL_Joystick structure containing joystick information.</param>
    /// <param name="axis">the axis to query; the axis indices start at index 0.</param>
    /// <param name="state">upon return, the initial value is supplied here.</param>
    /// <remarks>
    /// The state is a value ranging from -32768 to 32767.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if this axis has any initial value, or <see langword="false" /> if not.</returns>

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

    /// <summary>Get the ball axis change since the last poll.</summary>

    /// <param name="joystick">the SDL_Joystick to query.</param>
    /// <param name="ball">the ball index to query; ball indices start at index 0.</param>
    /// <param name="dx">stores the difference in the x axis position since the last poll.</param>
    /// <param name="dy">stores the difference in the y axis position since the last poll.</param>
    /// <remarks>
    /// Trackballs can only return relative motion since the last call to
    /// SDL_GetJoystickBall(), these motion deltas are
    /// placed into dx and dy.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetNumJoystickBalls" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

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

    /// <summary>Get the current state of a button on a joystick.</summary>

    /// <param name="joystick">an SDL_Joystick structure containing joystick information.</param>
    /// <param name="button">the button index to get the state from; indices start at index 0.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetNumJoystickButtons" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the button is pressed, <see langword="false" /> otherwise.</returns>

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

    /// <summary>Get the connection state of a joystick.</summary>

    /// <param name="joystick">the joystick to query.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns theconnection state on success orSDL_JOYSTICK_CONNECTION_INVALID on failure; call <see cref="GetError()" /> for more information.</returns>

    public static JoystickConnectionState GetJoystickConnectionState(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        JoystickConnectionState connectionState = SDL_GetJoystickConnectionState(joystick);
        return connectionState;
    }

    /// <summary>Get the firmware version of an opened joystick, if available.</summary>

    /// <param name="joystick">the SDL_Joystick obtained from SDL_OpenJoystick().</param>
    /// <remarks>
    /// If the firmware version isn't available this function returns 0.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the firmware version of the selected joystick,  0 if unavailable.</returns>

    public static ushort GetJoystickFirmwareVersion(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        ushort firmwareVersion = SDL_GetJoystickFirmwareVersion(joystick);
        return firmwareVersion;
    }

    /// <summary>Get the SDL_Joystick associated with an instance ID, if it has been opened.</summary>

    /// <param name="instance_id">the instance ID to get the SDL_Joystick for.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(SDL_Joystick *) Returns an SDL_Joystick on success or <see langword="null" /> on failure or if it hasn't been opened yet; call <see cref="GetError()" /> for more information.</returns>

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

    /// <summary>Get the SDL_Joystick associated with a player index.</summary>

    /// <param name="player_index">the player index to get the SDL_Joystick for.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoystickPlayerIndex" />
    /// <seealso cref="SetJoystickPlayerIndex" />
    /// </remarks>
    /// <returns>(SDL_Joystick *) Returns an SDL_Joystick on success or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

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

    /// <summary>Get the implementation-dependent GUID for the joystick.</summary>

    /// <param name="joystick">the SDL_Joystick obtained from SDL_OpenJoystick().</param>
    /// <remarks>
    /// This function requires an open joystick.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoystickGUIDForID" />
    /// <seealso cref="GUIDToString" />
    /// </remarks>
    /// <returns>Returns the GUID of the given joystick. If called onan invalid index, this function returns a zero GUID; call <see cref="GetError()" /> for more information.</returns>

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

    /// <summary>Get the implementation-dependent GUID of a joystick.</summary>

    /// <param name="instance_id">the joystick instance ID.</param>
    /// <remarks>
    /// This can be called before any joysticks are opened.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoystickGUID" />
    /// <seealso cref="GUIDToString" />
    /// </remarks>
    /// <returns>Returns the GUID of the selected joystick. If calledwith an invalid instance_id, this function returns a zero GUID.</returns>

    public static SdlGuid GetJoystickGuidForId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetJoystickGUIDForID(instanceId);
    }

    /// <summary>Get the device information encoded in a <see cref="SdlGuid" /> structure.</summary>

    /// <param name="guid">the <see cref="SdlGuid" /> you wish to get info about.</param>
    /// <param name="vendor">a pointer filled in with the device VID, or 0 if not available.</param>
    /// <param name="product">a pointer filled in with the device PID, or 0 if not available.</param>
    /// <param name="version">a pointer filled in with the device version, or 0 if not available.</param>
    /// <param name="crc16">a pointer filled in with a CRC used to distinguish different products with the same VID/PID, or 0 if not available.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoystickGUIDForID" />
    /// </remarks>

    public static void GetJoystickGuidInfo(SdlGuid guid, out ushort vendor, out ushort product, out ushort version, out ushort crc16) {
        if (guid.Data == null) {
            throw new ArgumentException("GUID cannot be null.", nameof(guid));
        }
        SDL_GetJoystickGUIDInfo(guid, out vendor, out product, out version, out crc16);
    }

    /// <summary>Get the current state of a POV hat on a joystick.</summary>

    /// <param name="joystick">an SDL_Joystick structure containing joystick information.</param>
    /// <param name="hat">the hat index to get the state from; indices start at index 0.</param>
    /// <remarks>
    /// The returned value will be one of the SDL_HAT_* values.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetNumJoystickHats" />
    /// </remarks>
    /// <returns>Returns the current hat position.</returns>

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

    /// <summary>Get the instance ID of an opened joystick.</summary>

    /// <param name="joystick">an SDL_Joystick structure containing joystick information.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the instance ID of the specifiedjoystick on success or 0 on failure; call <see cref="GetError()" />for more information.</returns>

    public static uint GetJoystickId(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        uint id = SDL_GetJoystickID(joystick);
        return id;
    }

    /// <summary>Get the implementation dependent name of a joystick.</summary>

    /// <param name="joystick">the SDL_Joystick obtained from SDL_OpenJoystick().</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoystickNameForID" />
    /// </remarks>
    /// <returns>Returns the name of the selected joystick. If no name can befound, this function returns <see langword="null" />; call <see cref="GetError()" /> for more information.</returns>

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

    /// <summary>Get the implementation dependent name of a joystick.</summary>

    /// <param name="instance_id">the joystick instance ID.</param>
    /// <remarks>
    /// This can be called before any joysticks are opened.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoystickName" />
    /// <seealso cref="GetJoysticks" />
    /// </remarks>
    /// <returns>Returns the name of the selected joystick. If no name can befound, this function returns <see langword="null" />; call <see cref="GetError()" /> for more information.</returns>

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

    /// <summary>Get the implementation dependent path of a joystick.</summary>

    /// <param name="joystick">the SDL_Joystick obtained from SDL_OpenJoystick().</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoystickPathForID" />
    /// </remarks>
    /// <returns>Returns the path of the selected joystick. If no path can befound, this function returns <see langword="null" />; call <see cref="GetError()" /> for more information.</returns>

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

    /// <summary>Get the implementation dependent path of a joystick.</summary>

    /// <param name="instance_id">the joystick instance ID.</param>
    /// <remarks>
    /// This can be called before any joysticks are opened.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoystickPath" />
    /// <seealso cref="GetJoysticks" />
    /// </remarks>
    /// <returns>Returns the path of the selected joystick. If no path can befound, this function returns <see langword="null" />; call <see cref="GetError()" /> for more information.</returns>

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

    /// <summary>Get the player index of an opened joystick.</summary>

    /// <param name="joystick">the SDL_Joystick obtained from SDL_OpenJoystick().</param>
    /// <remarks>
    /// For XInput controllers this returns the XInput user index. Many joysticks
    /// will not be able to supply this information.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetJoystickPlayerIndex" />
    /// </remarks>
    /// <returns>Returns the player index, or -1 if it's not available.</returns>

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

    /// <summary>Get the player index of a joystick.</summary>

    /// <param name="instance_id">the joystick instance ID.</param>
    /// <remarks>
    /// This can be called before any joysticks are opened.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoystickPlayerIndex" />
    /// <seealso cref="GetJoysticks" />
    /// </remarks>
    /// <returns>Returns the player index of a joystick, or -1 if it's not available.</returns>

    public static int GetJoystickPlayerIndexForId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetJoystickPlayerIndexForID(instanceId);
    }

    /// <summary>Get the battery state of a joystick.</summary>

    /// <param name="joystick">the joystick to query.</param>
    /// <param name="percent">a pointer filled in with the percentage of battery life left, between 0 and 100, or discard to ignore. This will be filled in with -1 we can't determine a value or there is no battery.</param>
    /// <remarks>
    /// You should never take a battery status as absolute truth. Batteries
    /// (especially failing batteries) are delicate hardware, and the values
    /// reported here are best estimates based on what that hardware reports. It's
    /// not uncommon for older batteries to lose stored power much faster than it
    /// reports, or completely drain when reporting it has 20 percent left, etc.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the current battery state orSDL_POWERSTATE_ERROR on failure; call <see cref="GetError()" /> for more information.</returns>

    public static PowerState GetJoystickPowerInfo(nint joystick, out int percent) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        PowerState powerState = SDL_GetJoystickPowerInfo(joystick, out percent);
        return powerState;
    }

    /// <summary>Get the USB product ID of an opened joystick, if available.</summary>

    /// <param name="joystick">the SDL_Joystick obtained from SDL_OpenJoystick().</param>
    /// <remarks>
    /// If the product ID isn't available this function returns 0.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoystickProductForID" />
    /// </remarks>
    /// <returns>Returns the USB product ID of the selected joystick, or0 if unavailable.</returns>

    public static ushort GetJoystickProduct(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        ushort product = SDL_GetJoystickProduct(joystick);
        return product;
    }

    /// <summary>Get the USB product ID of a joystick, if available.</summary>

    /// <param name="instance_id">the joystick instance ID.</param>
    /// <remarks>
    /// This can be called before any joysticks are opened. If the product ID isn't
    /// available this function returns 0.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoystickProduct" />
    /// <seealso cref="GetJoysticks" />
    /// </remarks>
    /// <returns>Returns the USB product ID of the selected joystick. Ifcalled with an invalid instance_id, this function returns 0.</returns>

    public static ushort GetJoystickProductForId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetJoystickProductForID(instanceId);
    }

    /// <summary>Get the product version of an opened joystick, if available.</summary>

    /// <param name="joystick">the SDL_Joystick obtained from SDL_OpenJoystick().</param>
    /// <remarks>
    /// If the product version isn't available this function returns 0.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoystickProductVersionForID" />
    /// </remarks>
    /// <returns>Returns the product version of the selected joystick, or0 if unavailable.</returns>

    public static ushort GetJoystickProductVersion(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        ushort productVersion = SDL_GetJoystickProductVersion(joystick);
        return productVersion;
    }

    /// <summary>Get the product version of a joystick, if available.</summary>

    /// <param name="instance_id">the joystick instance ID.</param>
    /// <remarks>
    /// This can be called before any joysticks are opened. If the product version
    /// isn't available this function returns 0.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoystickProductVersion" />
    /// <seealso cref="GetJoysticks" />
    /// </remarks>
    /// <returns>Returns the product version of the selected joystick. Ifcalled with an invalid instance_id, this function returns 0.</returns>

    public static ushort GetJoystickProductVersionForId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetJoystickProductVersionForID(instanceId);
    }

    /// <summary>Get the properties associated with a joystick.</summary>

    /// <param name="joystick">the SDL_Joystick obtained from SDL_OpenJoystick().</param>
    /// <remarks>
    /// The following read-only properties are provided by SDL:
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns a valid property ID on success or 0 on failure; call <see cref="GetError()" /> for more information.</returns>

    public static uint GetJoystickProperties(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        uint properties = SDL_GetJoystickProperties(joystick);
        return properties;
    }

    /// <summary>Get a list of currently connected joysticks.</summary>

    /// <param name="count">a pointer filled in with the number of joysticks returned, may be discarded.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="HasJoystick" />
    /// <seealso cref="OpenJoystick" />
    /// </remarks>
    /// <returns>(SDL_JoystickID *) Returns a 0 terminated array ofjoystick instance IDs or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information. This should be freedwith <see cref="Free" /> when it is no longer needed.</returns>

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

    /// <summary>Get the serial number of an opened joystick, if available.</summary>

    /// <param name="joystick">the SDL_Joystick obtained from SDL_OpenJoystick().</param>
    /// <remarks>
    /// Returns the serial number of the joystick, or <see langword="null" /> if it is not available.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the serial number of the selected joystick, or <see langword="null" />if unavailable.</returns>

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

    /// <summary>Get the type of an opened joystick.</summary>

    /// <param name="joystick">the SDL_Joystick obtained from SDL_OpenJoystick().</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoystickTypeForID" />
    /// </remarks>
    /// <returns>Returns theSDL_JoystickType of the selected joystick.</returns>

    public static JoystickType GetJoystickType(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        JoystickType joystickType = SDL_GetJoystickType(joystick);
        return joystickType;
    }

    /// <summary>Get the type of a joystick, if available.</summary>

    /// <param name="instance_id">the joystick instance ID.</param>
    /// <remarks>
    /// This can be called before any joysticks are opened.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoystickType" />
    /// <seealso cref="GetJoysticks" />
    /// </remarks>
    /// <returns>Returns theSDL_JoystickType of the selected joystick. If calledwith an invalid instance_id, this function returnsSDL_JOYSTICK_TYPE_UNKNOWN.</returns>

    public static JoystickType GetJoystickTypeForId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetJoystickTypeForID(instanceId);
    }

    /// <summary>Get the USB vendor ID of an opened joystick, if available.</summary>

    /// <param name="joystick">the SDL_Joystick obtained from SDL_OpenJoystick().</param>
    /// <remarks>
    /// If the vendor ID isn't available this function returns 0.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoystickVendorForID" />
    /// </remarks>
    /// <returns>Returns the USB vendor ID of the selected joystick, or 0if unavailable.</returns>

    public static ushort GetJoystickVendor(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        ushort vendor = SDL_GetJoystickVendor(joystick);
        return vendor;
    }

    /// <summary>Get the USB vendor ID of a joystick, if available.</summary>

    /// <param name="instance_id">the joystick instance ID.</param>
    /// <remarks>
    /// This can be called before any joysticks are opened. If the vendor ID isn't
    /// available this function returns 0.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoystickVendor" />
    /// <seealso cref="GetJoysticks" />
    /// </remarks>
    /// <returns>Returns the USB vendor ID of the selected joystick. Ifcalled with an invalid instance_id, this function returns 0.</returns>

    public static ushort GetJoystickVendorForId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        return SDL_GetJoystickVendorForID(instanceId);
    }

    /// <summary>Get the number of general axis controls on a joystick.</summary>

    /// <param name="joystick">an SDL_Joystick structure containing joystick information.</param>
    /// <remarks>
    /// Often, the directional pad on a game controller will either look like 4
    /// separate buttons or a POV hat, and not axes, but all of this is up to the
    /// device and platform.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoystickAxis" />
    /// <seealso cref="GetNumJoystickBalls" />
    /// <seealso cref="GetNumJoystickButtons" />
    /// <seealso cref="GetNumJoystickHats" />
    /// </remarks>
    /// <returns>Returns the number of axis controls/number of axes on success or -1on failure; call <see cref="GetError()" /> for more information.</returns>

    public static int GetNumJoystickAxes(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        int numAxes = SDL_GetNumJoystickAxes(joystick);
        return numAxes;
    }

    /// <summary>Get the number of trackballs on a joystick.</summary>

    /// <param name="joystick">an SDL_Joystick structure containing joystick information.</param>
    /// <remarks>
    /// Joystick trackballs have only relative motion events associated with them
    /// and their state cannot be polled.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoystickBall" />
    /// <seealso cref="GetNumJoystickAxes" />
    /// <seealso cref="GetNumJoystickButtons" />
    /// <seealso cref="GetNumJoystickHats" />
    /// </remarks>
    /// <returns>Returns the number of trackballs on success or -1 on failure; call <see cref="GetError()" /> for more information.</returns>

    public static int GetNumJoystickBalls(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        int numBalls = SDL_GetNumJoystickBalls(joystick);
        return numBalls;
    }

    /// <summary>Get the number of buttons on a joystick.</summary>

    /// <param name="joystick">an SDL_Joystick structure containing joystick information.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoystickButton" />
    /// <seealso cref="GetNumJoystickAxes" />
    /// <seealso cref="GetNumJoystickBalls" />
    /// <seealso cref="GetNumJoystickHats" />
    /// </remarks>
    /// <returns>Returns the number of buttons on success or -1 on failure; call <see cref="GetError()" /> for more information.</returns>

    public static int GetNumJoystickButtons(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        int numButtons = SDL_GetNumJoystickButtons(joystick);
        return numButtons;
    }

    /// <summary>Get the number of POV hats on a joystick.</summary>

    /// <param name="joystick">an SDL_Joystick structure containing joystick information.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoystickHat" />
    /// <seealso cref="GetNumJoystickAxes" />
    /// <seealso cref="GetNumJoystickBalls" />
    /// <seealso cref="GetNumJoystickButtons" />
    /// </remarks>
    /// <returns>Returns the number of POV hats on success or -1 on failure; call <see cref="GetError()" /> for more information.</returns>

    public static int GetNumJoystickHats(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        int numHats = SDL_GetNumJoystickHats(joystick);
        return numHats;
    }

    /// <summary>Return whether a joystick is currently connected.</summary>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoysticks" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if a joystick is connected, <see langword="false" /> otherwise.</returns>

    public static SdlBool HasJoystick() {
        return SDL_HasJoystick();
    }

    /// <summary>Query whether or not a joystick is virtual.</summary>

    /// <param name="instance_id">the joystick instance ID.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the joystick is virtual, <see langword="false" /> otherwise.</returns>

    public static SdlBool IsJoystickVirtual(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        SdlBool result = SDL_IsJoystickVirtual(instanceId);
        return result;
    }

    /// <summary>Get the status of a specified joystick.</summary>

    /// <param name="joystick">the joystick to query.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the joystick has been opened, <see langword="false" /> if it has not;call <see cref="GetError()" /> for more information.</returns>

    public static SdlBool JoystickConnected(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        SdlBool connected = SDL_JoystickConnected(joystick);
        return connected;
    }

    /// <summary>Query the state of joystick event processing.</summary>
    /// <remarks>
    /// If joystick events are disabled, you must call
    /// SDL_UpdateJoysticks() yourself and check the state
    /// of the joystick when you want joystick information.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetJoystickEventsEnabled" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if joystick events are being processed, <see langword="false" />otherwise.</returns>

    public static SdlBool JoystickEventsEnabled() {
        return SDL_JoystickEventsEnabled();
    }

    /// <summary>Locking for atomic access to the joystick API.</summary>
    /// <remarks>
    /// The SDL joystick functions are thread-safe, however you can lock the
    /// joysticks while processing to guarantee that the joystick list won't change
    /// and joystick and gamepad events will not be delivered.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void LockJoysticks() {
        SDL_LockJoysticks();
    }

    /// <summary>Open a joystick for use.</summary>

    /// <param name="instance_id">the joystick instance ID.</param>
    /// <remarks>
    /// The joystick subsystem must be initialized before a joystick can be opened
    /// for use.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CloseJoystick" />
    /// </remarks>
    /// <returns>(SDL_Joystick *) Returns a joystick identifier or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint OpenJoystick(uint instanceId) {
        nint joystick = SDL_OpenJoystick(instanceId);
        if (joystick == nint.Zero) {
            throw new SdlException($"Failed to open joystick with instance ID {instanceId}. {GetError()}");
        }
        return joystick;
    }

    /// <summary>Start a rumble effect.</summary>

    /// <param name="joystick">the joystick to vibrate.</param>
    /// <param name="low_frequency_rumble">the intensity of the low frequency (left) rumble motor, from 0 to 0xFFFF.</param>
    /// <param name="high_frequency_rumble">the intensity of the high frequency (right) rumble motor, from 0 to 0xFFFF.</param>
    /// <param name="duration_ms">the duration of the rumble effect, in milliseconds.</param>
    /// <remarks>
    /// Each call to this function cancels any previous rumble effect, and calling
    /// it with 0 intensity stops any rumbling.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" />, or <see langword="false" /> if rumble isn't supported on this joystick.</returns>

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

    /// <summary>Start a rumble effect in the joystick's triggers.</summary>

    /// <param name="joystick">the joystick to vibrate.</param>
    /// <param name="left_rumble">the intensity of the left trigger rumble motor, from 0 to 0xFFFF.</param>
    /// <param name="right_rumble">the intensity of the right trigger rumble motor, from 0 to 0xFFFF.</param>
    /// <param name="duration_ms">the duration of the rumble effect, in milliseconds.</param>
    /// <remarks>
    /// Each call to this function cancels any previous trigger rumble effect, and
    /// calling it with 0 intensity stops any rumbling.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RumbleJoystick" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

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

    /// <summary>Send a joystick specific effect packet.</summary>

    /// <param name="joystick">the joystick to affect.</param>
    /// <param name="data">the data to send to the joystick.</param>
    /// <param name="size">the size of the data to send to the joystick.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

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

    /// <summary>Send a sensor update for an opened virtual joystick.</summary>

    /// <param name="joystick">the virtual joystick on which to set state.</param>
    /// <param name="type">the type of the sensor on the virtual joystick to update.</param>
    /// <param name="sensor_timestamp">a 64-bit timestamp in nanoseconds associated with the sensor reading.</param>
    /// <param name="data">the data associated with the sensor reading.</param>
    /// <param name="num_values">the number of values pointed to by data.</param>
    /// <remarks>
    /// Please note that values set here will not be applied until the next call to
    /// SDL_UpdateJoysticks, which can either be called
    /// directly, or can be called indirectly through various other SDL APIs,
    /// including, but not limited to the following:
    /// SDL_PollEvent, SDL_PumpEvents,
    /// SDL_WaitEventTimeout,
    /// SDL_WaitEvent.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

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

    /// <summary>Send a sensor update for an opened virtual joystick.</summary>

    /// <param name="joystick">the virtual joystick on which to set state.</param>
    /// <param name="type">the type of the sensor on the virtual joystick to update.</param>
    /// <param name="sensor_timestamp">a 64-bit timestamp in nanoseconds associated with the sensor reading.</param>
    /// <param name="data">the data associated with the sensor reading.</param>
    /// <param name="num_values">the number of values pointed to by data.</param>
    /// <remarks>
    /// Please note that values set here will not be applied until the next call to
    /// SDL_UpdateJoysticks, which can either be called
    /// directly, or can be called indirectly through various other SDL APIs,
    /// including, but not limited to the following:
    /// SDL_PollEvent, SDL_PumpEvents,
    /// SDL_WaitEventTimeout,
    /// SDL_WaitEvent.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

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

    /// <summary>Set the state of joystick event processing.</summary>

    /// <param name="enabled">whether to process joystick events or not.</param>
    /// <remarks>
    /// If joystick events are disabled, you must call
    /// SDL_UpdateJoysticks() yourself and check the state
    /// of the joystick when you want joystick information.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="JoystickEventsEnabled" />
    /// <seealso cref="UpdateJoysticks" />
    /// </remarks>

    public static void SetJoystickEventsEnabled(bool enabled) {
        SDL_SetJoystickEventsEnabled(enabled);

        LogInfo(LogCategory.System, $"Joystick events enabled: {enabled}");
    }

    /// <summary>Update a joystick's LED color.</summary>

    /// <param name="joystick">the joystick to update.</param>
    /// <param name="red">the intensity of the red LED.</param>
    /// <param name="green">the intensity of the green LED.</param>
    /// <param name="blue">the intensity of the blue LED.</param>
    /// <remarks>
    /// An example of a joystick LED is the light on the back of a PlayStation 4's
    /// DualShock 4 controller.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static SdlBool SetJoystickLed(nint joystick, byte red, byte green, byte blue) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick cannot be null.", nameof(joystick));
        }
        SdlBool result = SDL_SetJoystickLED(joystick, red, green, blue);
        return result;
    }

    /// <summary>Set the player index of an opened joystick.</summary>

    /// <param name="joystick">the SDL_Joystick obtained from SDL_OpenJoystick().</param>
    /// <param name="player_index">player index to assign to this joystick, or -1 to clear the player index and turn off player LEDs.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetJoystickPlayerIndex" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

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

    /// <summary>Set the state of an axis on an opened virtual joystick.</summary>

    /// <param name="joystick">the virtual joystick on which to set state.</param>
    /// <param name="axis">the index of the axis on the virtual joystick to update.</param>
    /// <param name="value">the new value for the specified axis.</param>
    /// <remarks>
    /// Please note that values set here will not be applied until the next call to
    /// SDL_UpdateJoysticks, which can either be called
    /// directly, or can be called indirectly through various other SDL APIs,
    /// including, but not limited to the following:
    /// SDL_PollEvent, SDL_PumpEvents,
    /// SDL_WaitEventTimeout,
    /// SDL_WaitEvent.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

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

    /// <summary>Generate ball motion on an opened virtual joystick.</summary>

    /// <param name="joystick">the virtual joystick on which to set state.</param>
    /// <param name="ball">the index of the ball on the virtual joystick to update.</param>
    /// <param name="xrel">the relative motion on the X axis.</param>
    /// <param name="yrel">the relative motion on the Y axis.</param>
    /// <remarks>
    /// Please note that values set here will not be applied until the next call to
    /// SDL_UpdateJoysticks, which can either be called
    /// directly, or can be called indirectly through various other SDL APIs,
    /// including, but not limited to the following:
    /// SDL_PollEvent, SDL_PumpEvents,
    /// SDL_WaitEventTimeout,
    /// SDL_WaitEvent.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

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

    /// <summary>Set the state of a button on an opened virtual joystick.</summary>

    /// <param name="joystick">the virtual joystick on which to set state.</param>
    /// <param name="button">the index of the button on the virtual joystick to update.</param>
    /// <param name="down"><see langword="true" /> if the button is pressed, <see langword="false" /> otherwise.</param>
    /// <remarks>
    /// Please note that values set here will not be applied until the next call to
    /// SDL_UpdateJoysticks, which can either be called
    /// directly, or can be called indirectly through various other SDL APIs,
    /// including, but not limited to the following:
    /// SDL_PollEvent, SDL_PumpEvents,
    /// SDL_WaitEventTimeout,
    /// SDL_WaitEvent.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

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

    /// <summary>Set the state of a hat on an opened virtual joystick.</summary>

    /// <param name="joystick">the virtual joystick on which to set state.</param>
    /// <param name="hat">the index of the hat on the virtual joystick to update.</param>
    /// <param name="value">the new value for the specified hat.</param>
    /// <remarks>
    /// Please note that values set here will not be applied until the next call to
    /// SDL_UpdateJoysticks, which can either be called
    /// directly, or can be called indirectly through various other SDL APIs,
    /// including, but not limited to the following:
    /// SDL_PollEvent, SDL_PumpEvents,
    /// SDL_WaitEventTimeout,
    /// SDL_WaitEvent.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

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

    /// <summary>Set touchpad finger state on an opened virtual joystick.</summary>

    /// <param name="joystick">the virtual joystick on which to set state.</param>
    /// <param name="touchpad">the index of the touchpad on the virtual joystick to update.</param>
    /// <param name="finger">the index of the finger on the touchpad to set.</param>
    /// <param name="down"><see langword="true" /> if the finger is pressed, <see langword="false" /> if the finger is released.</param>
    /// <param name="x">the x coordinate of the finger on the touchpad, normalized 0 to 1, with the origin in the upper left.</param>
    /// <param name="y">the y coordinate of the finger on the touchpad, normalized 0 to 1, with the origin in the upper left.</param>
    /// <param name="pressure">the pressure of the finger.</param>
    /// <remarks>
    /// Please note that values set here will not be applied until the next call to
    /// SDL_UpdateJoysticks, which can either be called
    /// directly, or can be called indirectly through various other SDL APIs,
    /// including, but not limited to the following:
    /// SDL_PollEvent, SDL_PumpEvents,
    /// SDL_WaitEventTimeout,
    /// SDL_WaitEvent.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

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

    /// <summary>Unlocking for atomic access to the joystick API.</summary>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void UnlockJoysticks() {
        SDL_UnlockJoysticks();
    }

    /// <summary>Update the current state of the open joysticks.</summary>
    /// <remarks>
    /// This is called automatically by the event loop if any joystick events are
    /// enabled.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void UpdateJoysticks() {
        SDL_UpdateJoysticks();
    }

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_AttachVirtualJoystick(ref VirtualJoystickDesc desc);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_CloseJoystick(nint joystick);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_DetachVirtualJoystick(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial short SDL_GetJoystickAxis(nint joystick, int axis);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetJoystickAxisInitialState(nint joystick, int axis, out short state);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetJoystickBall(nint joystick, int ball, out int dx, out int dy);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetJoystickButton(nint joystick, int button);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial JoystickConnectionState SDL_GetJoystickConnectionState(nint joystick);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetJoystickFirmwareVersion(nint joystick);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetJoystickFromID(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetJoystickFromPlayerIndex(int playerIndex);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlGuid SDL_GetJoystickGUID(nint joystick);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlGuid SDL_GetJoystickGUIDForID(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GetJoystickGUIDInfo(SdlGuid guid, out ushort vendor, out ushort product,
        out ushort version, out ushort crc16);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte SDL_GetJoystickHat(nint joystick, int hat);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetJoystickID(nint joystick);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetJoystickName(nint joystick);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetJoystickNameForID(uint instanceId);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetJoystickPath(nint joystick);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetJoystickPathForID(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetJoystickPlayerIndex(nint joystick);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetJoystickPlayerIndexForID(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PowerState SDL_GetJoystickPowerInfo(nint joystick, out int percent);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetJoystickProduct(nint joystick);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetJoystickProductForID(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetJoystickProductVersion(nint joystick);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetJoystickProductVersionForID(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetJoystickProperties(nint joystick);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetJoysticks(out int count);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetJoystickSerial(nint joystick);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial JoystickType SDL_GetJoystickType(nint joystick);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial JoystickType SDL_GetJoystickTypeForID(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetJoystickVendor(nint joystick);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetJoystickVendorForID(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumJoystickAxes(nint joystick);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumJoystickBalls(nint joystick);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumJoystickButtons(nint joystick);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumJoystickHats(nint joystick);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasJoystick();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_IsJoystickVirtual(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_JoystickConnected(nint joystick);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_JoystickEventsEnabled();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LockJoysticks();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenJoystick(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RumbleJoystick(nint joystick, ushort lowFrequencyRumble,
        ushort highFrequencyRumble, uint durationMs);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RumbleJoystickTriggers(nint joystick, ushort leftRumble, ushort rightRumble,
        uint durationMs);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SendJoystickEffect(nint joystick, nint data, int size);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SendJoystickVirtualSensorData(nint joystick, SensorType type,
        ulong sensorTimestamp, nint data, int numValues);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetJoystickEventsEnabled(SdlBool enabled);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetJoystickLED(nint joystick, byte red, byte green, byte blue);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetJoystickPlayerIndex(nint joystick, int playerIndex);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetJoystickVirtualAxis(nint joystick, int axis, short value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetJoystickVirtualBall(nint joystick, int ball, short xrel, short yrel);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetJoystickVirtualButton(nint joystick, int button, SdlBool down);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetJoystickVirtualHat(nint joystick, int hat, byte value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetJoystickVirtualTouchpad(nint joystick, int touchpad, int finger,
        SdlBool down, float x, float y, float pressure);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnlockJoysticks();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UpdateJoysticks();
}