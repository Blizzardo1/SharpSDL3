using SharpSDL3.Enums;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;

using static SharpSDL3.Sdl;

namespace SharpSDL3; 
public static unsafe partial class Sensors {


    public static void CloseSensor(nint sensor) {
        if (sensor == nint.Zero) {
            throw new ArgumentException("Sensor handle cannot be null.", nameof(sensor));
        }

        try {
            SDL_CloseSensor(sensor);
        } catch (Exception ex) {
            throw new InvalidOperationException("An error occurred while closing the sensor.", ex);
        }
    }

    public static SdlBool GetSensorData(nint sensor, float* data, int numValues) {
        SdlBool result = SDL_GetSensorData(sensor, data, numValues);
        if (!result) {
            throw new InvalidOperationException("SDL_GetSensorData failed");
        }
        return result;
    }

    public static nint GetSensorFromID(uint instanceId) {
        nint sensor = SDL_GetSensorFromID(instanceId);
        if (sensor == nint.Zero) {
            throw new InvalidOperationException("SDL_GetSensorFromID failed");
        }
        return sensor;
    }

    public static uint GetSensorID(nint sensor) {
        uint id = SDL_GetSensorID(sensor);
        if (id == 0) {
            throw new InvalidOperationException("SDL_GetSensorID failed");
        }
        return id;
    }

    public static string GetSensorName(nint sensor) {
        string name = SDL_GetSensorName(sensor);
        if (name == null) {
            throw new InvalidOperationException("SDL_GetSensorName failed");
        }
        return name;
    }

    public static string GetSensorNameForID(uint instanceId) {
        string name = SDL_GetSensorNameForID(instanceId);
        if (name == null) {
            throw new InvalidOperationException("SDL_GetSensorNameForID failed");
        }
        return name;
    }

    public static int GetSensorNonPortableType(nint sensor) {
        int type = SDL_GetSensorNonPortableType(sensor);
        if (type == -1) {
            throw new InvalidOperationException("SDL_GetSensorNonPortableType failed");
        }
        return type;
    }

    public static int GetSensorNonPortableTypeForID(uint instanceId) {
        int type = SDL_GetSensorNonPortableTypeForID(instanceId);
        if (type == -1) {
            throw new InvalidOperationException("SDL_GetSensorNonPortableTypeForID failed");
        }
        return type;
    }

    public static uint GetSensorProperties(nint sensor) {
        uint properties = SDL_GetSensorProperties(sensor);
        if (properties == 0) {
            throw new InvalidOperationException("SDL_GetSensorProperties failed");
        }
        return properties;
    }

    public static nint GetSensors(out int count) {
        nint sensors = SDL_GetSensors(out count);
        if (sensors == nint.Zero) {
            throw new InvalidOperationException("SDL_GetSensors failed");
        }
        return sensors;
    }

    public static SensorType GetSensorType(nint sensor) {
        SensorType type = SDL_GetSensorType(sensor);
        if (type == SensorType.Unknown) {
            throw new InvalidOperationException("SDL_GetSensorType failed");
        }
        return type;
    }

    public static SensorType GetSensorTypeForID(uint instanceId) {
        SensorType type = SDL_GetSensorTypeForID(instanceId);
        if (type == SensorType.Unknown) {
            throw new InvalidOperationException("SDL_GetSensorTypeForID failed");
        }
        return type;
    }

    public static nint OpenSensor(uint instanceId) {
        nint sensor = SDL_OpenSensor(instanceId);
        if (sensor == nint.Zero) {
            throw new InvalidOperationException("SDL_OpenSensor failed");
        }
        return sensor;
    }

    public static void UpdateSensors() {
        SDL_UpdateSensors();
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_CloseSensor(nint sensor);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetSensorData(nint sensor, float* data, int numValues);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetSensorFromID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetSensorID(nint sensor);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetSensorName(nint sensor);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetSensorNameForID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetSensorNonPortableType(nint sensor);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetSensorNonPortableTypeForID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetSensorProperties(nint sensor);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetSensors(out int count);
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SensorType SDL_GetSensorType(nint sensor);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SensorType SDL_GetSensorTypeForID(uint instanceId);
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenSensor(uint instanceId);
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UpdateSensors();
}
