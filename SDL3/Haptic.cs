using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SharpSDL3;

public static partial class Sdl {
    /// <summary>Close a haptic device previously opened with SDL_OpenHaptic().</summary>

    /// <param name="haptic">the SDL_Haptic device to close.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="OpenHaptic" />
    /// </remarks>

    public static void CloseHaptic(nint haptic) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }
        SDL_CloseHaptic(haptic);
    }

    /// <summary>Create a new haptic effect on a specified device.</summary>

    /// <param name="haptic">an SDL_Haptic device to create the effect on.</param>
    /// <param name="effect">an SDL_HapticEffect structure containing the properties of the effect to create.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="DestroyHapticEffect" />
    /// <seealso cref="RunHapticEffect" />
    /// <seealso cref="UpdateHapticEffect" />
    /// </remarks>
    /// <returns>Returns the ID of the effect on success or -1 on failure; call <see cref="GetError()" /> for more information.</returns>

    public static int CreateHapticEffect(nint haptic, ref HapticEffect effect) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }
        int effectId = SDL_CreateHapticEffect(haptic, ref effect);
        if (effectId < 0) {
            LogError(LogCategory.Error, "Failed to create haptic effect.");
        }
        return effectId;
    }

    /// <summary>Destroy a haptic effect on the device.</summary>

    /// <param name="haptic">the SDL_Haptic device to destroy the effect on.</param>
    /// <param name="effect">the ID of the haptic effect to destroy.</param>
    /// <remarks>
    /// This will stop the effect if it's running. Effects are automatically
    /// destroyed when the device is closed.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateHapticEffect" />
    /// </remarks>

    public static void DestroyHapticEffect(nint haptic, int effect) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }
        SDL_DestroyHapticEffect(haptic, effect);
    }

    /// <summary>Get the status of the current effect on the specified haptic device.</summary>

    /// <param name="haptic">the SDL_Haptic device to query for the effect status on.</param>
    /// <param name="effect">the ID of the haptic effect to query its status.</param>
    /// <remarks>
    /// Device must support the SDL_HAPTIC_STATUS feature.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetHapticFeatures" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if it is playing, <see langword="false" /> if it isn't playing or hapticstatus isn't supported.</returns>

    public static bool GetHapticEffectStatus(nint haptic, int effect) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }
        bool status = SDL_GetHapticEffectStatus(haptic, effect);
        if (!status) {
            LogError(LogCategory.Error, "Failed to retrieve haptic effect status.");
        }
        return status;
    }

    /// <summary>Get the haptic device's supported features in bitwise manner.</summary>

    /// <param name="haptic">the SDL_Haptic device to query.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="HapticEffectSupported" />
    /// <seealso cref="GetMaxHapticEffects" />
    /// </remarks>
    /// <returns>Returns a list of supported haptic features in bitwisemanner (OR'd), or 0 on failure; call <see cref="GetError()" /> for more information.</returns>

    public static uint GetHapticFeatures(nint haptic) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }
        uint features = SDL_GetHapticFeatures(haptic);
        if (features == 0) {
            LogError(LogCategory.Error, "Failed to retrieve haptic features.");
        }
        return features;
    }

    /// <summary>Get the SDL_Haptic associated with an instance ID, if it has been opened.</summary>

    /// <param name="instance_id">the instance ID to get the SDL_Haptic for.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(SDL_Haptic *) Returns an SDL_Haptic on successor <see langword="null" /> on failure or if it hasn't been opened yet; call <see cref="GetError()" /> for more information.</returns>

    public static nint GetHapticFromId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        nint haptic = SDL_GetHapticFromID(instanceId);
        if (haptic == nint.Zero) {
            LogError(LogCategory.Error, $"Failed to retrieve haptic device with ID: {instanceId}");
        }
        return haptic;
    }

    /// <summary>Get the instance ID of an opened haptic device.</summary>

    /// <param name="haptic">the SDL_Haptic device to query.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the instance ID of the specifiedhaptic device on success or 0 on failure; call <see cref="GetError()" /> for more information.</returns>

    public static uint GetHapticId(nint haptic) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }
        uint id = SDL_GetHapticID(haptic);
        if (id == 0) {
            LogError(LogCategory.Error, "Failed to retrieve haptic ID.");
        }
        return id;
    }

    /// <summary>Get the implementation dependent name of a haptic device.</summary>

    /// <param name="haptic">the SDL_Haptic obtained from SDL_OpenJoystick().</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetHapticNameForID" />
    /// </remarks>
    /// <returns>Returns the name of the selected haptic device. If no namecan be found, this function returns <see langword="null" />; call <see cref="GetError()" /> for more information.</returns>

    public static string GetHapticName(nint haptic) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }
        string name = SDL_GetHapticName(haptic);
        if (string.IsNullOrEmpty(name)) {
            LogError(LogCategory.Error, "Failed to retrieve haptic name.");
        }
        return name;
    }

    /// <summary>Get the implementation dependent name of a haptic device.</summary>

    /// <param name="instance_id">the haptic device instance ID.</param>
    /// <remarks>
    /// This can be called before any haptic devices are opened.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetHapticName" />
    /// <seealso cref="OpenHaptic" />
    /// </remarks>
    /// <returns>Returns the name of the selected haptic device. If no namecan be found, this function returns <see langword="null" />; call <see cref="GetError()" /> for more information.</returns>

    public static string GetHapticNameforId(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        string name = SDL_GetHapticNameForID(instanceId);
        if (string.IsNullOrEmpty(name)) {
            LogError(LogCategory.Error, $"Failed to retrieve haptic name for ID: {instanceId}");
            return string.Empty;
        }
        return name;
    }

    /// <summary>Get a list of currently connected haptic devices.</summary>

    /// <param name="count">a pointer filled in with the number of haptic devices returned, may be discarded.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="OpenHaptic" />
    /// </remarks>
    /// <returns>(SDL_HapticID *) Returns a 0 terminated array of hapticdevice instance IDs or <see langword="null" /> on failure; call <see cref="GetError()" />for more information. This should be freed with <see cref="Free" /> whenit is no longer needed.</returns>

    public static Span<nint> GetHaptics() {
        nint result = SDL_GetHaptics(out int count);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "Failed to retrieve haptic devices.");
            return [];
        }
        if (count <= 0) {
            LogWarn(LogCategory.System, "No haptic devices found.");
            return [];
        }
        nint[] ptrs = new nint[count];
        for (int i = 0; i < count; i++) {
            ptrs[i] = Marshal.ReadIntPtr(result, i * Marshal.SizeOf<nint>());
        }
        return new Span<nint>(ptrs);
    }

    /// <summary>Get the number of effects a haptic device can store.</summary>

    /// <param name="haptic">the SDL_Haptic device to query.</param>
    /// <remarks>
    /// On some platforms this isn't fully supported, and therefore is an
    /// approximation. Always check to see if your created effect was actually
    /// created and do not rely solely on
    /// SDL_GetMaxHapticEffects().
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetMaxHapticEffectsPlaying" />
    /// <seealso cref="GetHapticFeatures" />
    /// </remarks>
    /// <returns>Returns the number of effects the haptic device can store or anegative error code on failure; call <see cref="GetError()" /> for more information.</returns>

    public static int GetMaxHapticEffects(nint haptic) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }
        int maxEffects = SDL_GetMaxHapticEffects(haptic);
        if (maxEffects < 0) {
            LogError(LogCategory.Error, "Failed to retrieve maximum haptic effects.");
        }
        return maxEffects;
    }

    /// <summary>Get the number of effects a haptic device can play at the same time.</summary>

    /// <param name="haptic">the SDL_Haptic device to query maximum playing effects.</param>
    /// <remarks>
    /// This is not supported on all platforms, but will always return a value.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetMaxHapticEffects" />
    /// <seealso cref="GetHapticFeatures" />
    /// </remarks>
    /// <returns>Returns the number of effects the haptic device can play at the sametime or -1 on failure; call <see cref="GetError()" /> for more information.</returns>

    public static int GetMaxHapticEffectsPlaying(nint haptic) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }
        int maxEffectsPlaying = SDL_GetMaxHapticEffectsPlaying(haptic);
        if (maxEffectsPlaying < 0) {
            LogError(LogCategory.Error, "Failed to retrieve maximum haptic effects playing.");
        }
        return maxEffectsPlaying;
    }

    /// <summary>Get the number of haptic axes the device has.</summary>

    /// <param name="haptic">the SDL_Haptic device to query.</param>
    /// <remarks>
    /// The number of haptic axes might be useful if working with the
    /// SDL_HapticDirection effect.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the number of axes on success or -1 on failure; call <see cref="GetError()" /> for more information.</returns>

    public static int GetNumHapticAxes(nint haptic) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }
        int numAxes = SDL_GetNumHapticAxes(haptic);
        if (numAxes < 0) {
            LogError(LogCategory.Error, "Failed to retrieve number of haptic axes.");
        }
        return numAxes;
    }

    /// <summary>Initialize a haptic device for simple rumble playback.</summary>

    /// <param name="haptic">the haptic device to initialize for simple rumble playback.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PlayHapticRumble" />
    /// <seealso cref="StopHapticRumble" />
    /// <seealso cref="HapticRumbleSupported" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool InitHapticRumble(nint haptic) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }
        bool initialized = SDL_InitHapticRumble(haptic);
        if (!initialized) {
            LogError(LogCategory.Error, "Failed to initialize haptic rumble.");
        }
        return initialized;
    }

    public static bool IsHapticEffectSupported(nint haptic, ref HapticEffect effect) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }

        bool isSupported = SDL_HapticEffectSupported(haptic, ref effect);
        if (!isSupported) {
            LogError(LogCategory.Error, "Haptic effect is not supported.");
        }
        return isSupported;
    }

    public static bool IsHapticRumbleSupported(nint haptic) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }
        bool isSupported = SDL_HapticRumbleSupported(haptic);
        if (!isSupported) {
            LogError(LogCategory.Error, "Haptic rumble is not supported.");
        }
        return isSupported;
    }

    /// <summary>Query if a joystick has haptic features.</summary>

    /// <param name="joystick">the SDL_Joystick to test for haptic capabilities.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="OpenHapticFromJoystick" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the joystick is haptic or <see langword="false" /> if it isn't.</returns>

    public static bool IsJoystickHaptic(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick handle cannot be null.", nameof(joystick));
        }
        bool isHaptic = SDL_IsJoystickHaptic(joystick);
        if (!isHaptic) {
            LogWarn(LogCategory.System, "Joystick haptic feedback is not supported.");
        }
        return isHaptic;
    }

    /// <summary>Query whether or not the current mouse has haptic capabilities.</summary>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="OpenHapticFromMouse" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the mouse is haptic or <see langword="false" /> if it isn't.</returns>

    public static bool IsMouseHaptic() {
        bool isHaptic = SDL_IsMouseHaptic();
        if (!isHaptic) {
            LogWarn(LogCategory.System, "Mouse haptic feedback is not supported.");
        }
        return isHaptic;
    }

    /// <summary>Open a haptic device for use.</summary>

    /// <param name="instance_id">the haptic device instance ID.</param>
    /// <remarks>
    /// The index passed as an argument refers to the N'th haptic device on this
    /// system.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CloseHaptic" />
    /// <seealso cref="GetHaptics" />
    /// <seealso cref="OpenHapticFromJoystick" />
    /// <seealso cref="OpenHapticFromMouse" />
    /// <seealso cref="SetHapticAutocenter" />
    /// <seealso cref="SetHapticGain" />
    /// </remarks>
    /// <returns>(SDL_Haptic *) Returns the device identifier or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint OpenHaptic(uint instanceId) {
        if (instanceId == 0) {
            throw new ArgumentException("Instance ID cannot be zero.", nameof(instanceId));
        }
        nint haptic = SDL_OpenHaptic(instanceId);
        if (haptic == nint.Zero) {
            LogError(LogCategory.Error, $"Failed to open haptic device with ID: {instanceId}");
        }
        return haptic;
    }

    /// <summary>Open a haptic device for use from a joystick device.</summary>

    /// <param name="joystick">the SDL_Joystick to create a haptic device from.</param>
    /// <remarks>
    /// You must still close the haptic device separately. It will not be closed
    /// with the joystick.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CloseHaptic" />
    /// <seealso cref="IsJoystickHaptic" />
    /// </remarks>
    /// <returns>(SDL_Haptic *) Returns a valid haptic device identifier on success or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint OpenHapticFromJoystick(nint joystick) {
        if (joystick == nint.Zero) {
            throw new ArgumentException("Joystick handle cannot be null.", nameof(joystick));
        }
        nint haptic = SDL_OpenHapticFromJoystick(joystick);
        if (haptic == nint.Zero) {
            LogError(LogCategory.Error, "Failed to open haptic from joystick.");
        }
        return haptic;
    }

    /// <summary>Try to open a haptic device from the current mouse.</summary>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CloseHaptic" />
    /// <seealso cref="IsMouseHaptic" />
    /// </remarks>
    /// <returns>(SDL_Haptic *) Returns the haptic device identifier or <see langword="null" />on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint OpenHapticFromMouse() {
        nint haptic = SDL_OpenHapticFromMouse();
        if (haptic == nint.Zero) {
            LogError(LogCategory.Error, "Failed to open haptic from mouse.");
        }
        return haptic;
    }

    /// <summary>Pause a haptic device.</summary>

    /// <param name="haptic">the SDL_Haptic device to pause.</param>
    /// <remarks>
    /// Device must support the SDL_HAPTIC_PAUSE feature.
    /// Call SDL_ResumeHaptic() to resume playback.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ResumeHaptic" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool PauseHaptic(nint haptic) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }
        bool paused = SDL_PauseHaptic(haptic);
        if (!paused) {
            LogError(LogCategory.Error, "Failed to pause haptic device.");
        }
        return paused;
    }

    /// <summary>Run a simple rumble effect on a haptic device.</summary>

    /// <param name="haptic">the haptic device to play the rumble effect on.</param>
    /// <param name="strength">strength of the rumble to play as a 0-1 float value.</param>
    /// <param name="length">length of the rumble to play in milliseconds.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="InitHapticRumble" />
    /// <seealso cref="StopHapticRumble" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool PlayHapticRumble(nint haptic, float strength, uint length) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }
        if (strength < 0.0f || strength > 1.0f) {
            throw new ArgumentOutOfRangeException(nameof(strength), "Strength must be between 0.0 and 1.0.");
        }
        bool played = SDL_PlayHapticRumble(haptic, strength, length);
        if (!played) {
            LogError(LogCategory.Error, "Failed to play haptic rumble.");
        }
        return played;
    }

    /// <summary>Resume a haptic device.</summary>

    /// <param name="haptic">the SDL_Haptic device to unpause.</param>
    /// <remarks>
    /// Call to unpause after SDL_PauseHaptic().
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PauseHaptic" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool ResumeHaptic(nint haptic) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }
        bool resumed = SDL_ResumeHaptic(haptic);
        if (!resumed) {
            LogError(LogCategory.Error, "Failed to resume haptic device.");
        }
        return resumed;
    }

    /// <summary>Run the haptic effect on its associated haptic device.</summary>

    /// <param name="haptic">the SDL_Haptic device to run the effect on.</param>
    /// <param name="effect">the ID of the haptic effect to run.</param>
    /// <param name="iterations">the number of iterations to run the effect; use SDL_HAPTIC_INFINITY to repeat forever.</param>
    /// <remarks>
    /// To repeat the effect over and over indefinitely, set iterations to
    /// SDL_HAPTIC_INFINITY. (Repeats the envelope -
    /// attack and fade.) To make one instance of the effect last indefinitely (so
    /// the effect does not fade), set the effect's length in its structure/union
    /// to SDL_HAPTIC_INFINITY instead.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetHapticEffectStatus" />
    /// <seealso cref="StopHapticEffect" />
    /// <seealso cref="StopHapticEffects" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool RunHapticEffect(nint haptic, int effect, uint iterations) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }
        bool ran = SDL_RunHapticEffect(haptic, effect, iterations);
        if (!ran) {
            LogError(LogCategory.Error, "Failed to run haptic effect.");
        }
        return ran;
    }

    /// <summary>Set the global autocenter of the device.</summary>

    /// <param name="haptic">the SDL_Haptic device to set autocentering on.</param>
    /// <param name="autocenter">value to set autocenter to (0-100).</param>
    /// <remarks>
    /// Autocenter should be between 0 and 100. Setting it to 0 will disable
    /// autocentering.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetHapticFeatures" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool SetHapticAutocenter(nint haptic, int autocenter) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }
        bool set = SDL_SetHapticAutocenter(haptic, autocenter);
        if (!set) {
            LogError(LogCategory.Error, "Failed to set haptic autocenter.");
        }
        return set;
    }

    /// <summary>Set the global gain of the specified haptic device.</summary>

    /// <param name="haptic">the SDL_Haptic device to set the gain on.</param>
    /// <param name="gain">value to set the gain to, should be between 0 and 100 (0 - 100).</param>
    /// <remarks>
    /// Device must support the SDL_HAPTIC_GAIN feature.
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetHapticFeatures" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool SetHapticGain(nint haptic, int gain) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }
        bool set = SDL_SetHapticGain(haptic, gain);
        if (!set) {
            LogError(LogCategory.Error, "Failed to set haptic gain.");
        }
        return set;
    }

    /// <summary>Stop the haptic effect on its associated haptic device.</summary>

    /// <param name="haptic">the SDL_Haptic device to stop the effect on.</param>
    /// <param name="effect">the ID of the haptic effect to stop.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RunHapticEffect" />
    /// <seealso cref="StopHapticEffects" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool StopHapticEffect(nint haptic, int effect) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }
        bool stopped = SDL_StopHapticEffect(haptic, effect);
        if (!stopped) {
            LogError(LogCategory.Error, "Failed to stop haptic effect.");
        }
        return stopped;
    }

    /// <summary>Stop all the currently playing effects on a haptic device.</summary>

    /// <param name="haptic">the SDL_Haptic device to stop.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RunHapticEffect" />
    /// <seealso cref="StopHapticEffects" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool StopHapticEffects(nint haptic) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }
        bool stopped = SDL_StopHapticEffects(haptic);
        if (!stopped) {
            LogError(LogCategory.Error, "Failed to stop all haptic effects.");
        }
        return stopped;
    }

    /// <summary>Stop the simple rumble on a haptic device.</summary>

    /// <param name="haptic">the haptic device to stop the rumble effect on.</param>
    /// <remarks>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PlayHapticRumble" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool StopHapticRumble(nint haptic) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }
        bool stopped = SDL_StopHapticRumble(haptic);
        if (!stopped) {
            LogError(LogCategory.Error, "Failed to stop haptic rumble.");
        }
        return stopped;
    }

    /// <summary>Update the properties of an effect.</summary>

    /// <param name="haptic">the SDL_Haptic device that has the effect.</param>
    /// <param name="effect">the identifier of the effect to update.</param>
    /// <param name="data">an SDL_HapticEffect structure containing the new effect properties to use.</param>
    /// <remarks>
    /// Can be used dynamically, although behavior when dynamically changing
    /// direction may be strange. Specifically the effect may re-upload itself and
    /// start playing from the start. You also cannot change the type either when
    /// running SDL_UpdateHapticEffect().
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateHapticEffect" />
    /// <seealso cref="RunHapticEffect" />
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool UpdateHapticEffect(nint haptic, int effect, ref HapticEffect data) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }
        bool updated = SDL_UpdateHapticEffect(haptic, effect, ref data);
        if (!updated) {
            LogError(LogCategory.Error, "Failed to update haptic effect.");
        }
        return updated;
    }

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_CloseHaptic(nint haptic);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_CreateHapticEffect(nint haptic, ref HapticEffect effect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyHapticEffect(nint haptic, int effect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetHapticEffectStatus(nint haptic, int effect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetHapticFeatures(nint haptic);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetHapticFromID(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetHapticID(nint haptic);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetHapticName(nint haptic);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetHapticNameForID(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetHaptics(out int count);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetMaxHapticEffects(nint haptic);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetMaxHapticEffectsPlaying(nint haptic);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumHapticAxes(nint haptic);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HapticEffectSupported(nint haptic, ref HapticEffect effect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HapticRumbleSupported(nint haptic);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_InitHapticRumble(nint haptic);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_IsJoystickHaptic(nint joystick);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_IsMouseHaptic();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenHaptic(uint instanceId);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenHapticFromJoystick(nint joystick);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenHapticFromMouse();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_PauseHaptic(nint haptic);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_PlayHapticRumble(nint haptic, float strength, uint length);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ResumeHaptic(nint haptic);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RunHapticEffect(nint haptic, int effect, uint iterations);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetHapticAutocenter(nint haptic, int autocenter);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetHapticGain(nint haptic, int gain);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_StopHapticEffect(nint haptic, int effect);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_StopHapticEffects(nint haptic);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_StopHapticRumble(nint haptic);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_UpdateHapticEffect(nint haptic, int effect, ref HapticEffect data);
}