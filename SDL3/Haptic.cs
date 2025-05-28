using SharpSDL3.Structs;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;

using System;
using SharpSDL3.Enums;

namespace SharpSDL3;

public static partial class Sdl {

    public static void CloseHaptic(nint haptic) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }
        SDL_CloseHaptic(haptic);
    }

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

    public static void DestroyHapticEffect(nint haptic, int effect) {
        if (haptic == nint.Zero) {
            throw new ArgumentException("Haptic handle cannot be null.", nameof(haptic));
        }
        SDL_DestroyHapticEffect(haptic, effect);
    }

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

    public static bool IsMouseHaptic() {
        bool isHaptic = SDL_IsMouseHaptic();
        if (!isHaptic) {
            LogWarn(LogCategory.System, "Mouse haptic feedback is not supported.");
        }
        return isHaptic;
    }

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

    public static nint OpenHapticFromMouse() {
        nint haptic = SDL_OpenHapticFromMouse();
        if (haptic == nint.Zero) {
            LogError(LogCategory.Error, "Failed to open haptic from mouse.");
        }
        return haptic;
    }

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

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_CloseHaptic(nint haptic);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_CreateHapticEffect(nint haptic, ref HapticEffect effect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyHapticEffect(nint haptic, int effect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetHapticEffectStatus(nint haptic, int effect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetHapticFeatures(nint haptic);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetHapticFromID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetHapticID(nint haptic);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetHapticName(nint haptic);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetHapticNameForID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetHaptics(out int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetMaxHapticEffects(nint haptic);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetMaxHapticEffectsPlaying(nint haptic);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumHapticAxes(nint haptic);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HapticEffectSupported(nint haptic, ref HapticEffect effect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HapticRumbleSupported(nint haptic);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_InitHapticRumble(nint haptic);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_IsJoystickHaptic(nint joystick);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_IsMouseHaptic();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenHaptic(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenHapticFromJoystick(nint joystick);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenHapticFromMouse();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_PauseHaptic(nint haptic);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_PlayHapticRumble(nint haptic, float strength, uint length);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ResumeHaptic(nint haptic);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RunHapticEffect(nint haptic, int effect, uint iterations);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetHapticAutocenter(nint haptic, int autocenter);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SetHapticGain(nint haptic, int gain);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_StopHapticEffect(nint haptic, int effect);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_StopHapticEffects(nint haptic);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_StopHapticRumble(nint haptic);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_UpdateHapticEffect(nint haptic, int effect, ref HapticEffect data);
}