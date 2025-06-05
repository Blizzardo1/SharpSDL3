<<<<<<< HEAD
using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SharpSDL3;

public static unsafe partial class Sdl {
    /// <summary>Close a sensor previously opened with SDL_OpenSensor().</summary>

    /// <param name="sensor">the SDL_Sensor object to close.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

=======
﻿using SharpSDL3.Enums;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;

using SharpSDL3.Structs;

namespace SharpSDL3; 
public static unsafe partial class Sdl {
>>>>>>> main
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

<<<<<<< HEAD
    /// <summary>Get the current state of an opened sensor.</summary>

    /// <param name="sensor">the SDL_Sensor object to query.</param>
    /// <param name="data">a pointer filled with the current sensor state.</param>
    /// <param name="num_values">the number of values to write to data.</param>
    /// <remarks>
    /// The number of values and interpretation of the data is sensor dependent.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool GetSensorData(nint sensor, nint data, int numValues) {
        SdlBool result = SDL_GetSensorData(sensor, data, numValues);
        if (!result) {
            throw new InvalidOperationException("GetSensorData failed");
=======
    public static bool GetSensorData(nint sensor, nint data, int numValues) {
        SdlBool result = SDL_GetSensorData(sensor, data, numValues);
        if (!result) {
            throw new InvalidOperationException("SDL_GetSensorData failed");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Return the SDL_Sensor associated with an instance ID.</summary>

    /// <param name="instance_id">the sensor instance ID.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(SDL_Sensor *) Returns an SDL_Sensor object or<see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint GetSensorFromID(uint instanceId) {
        nint sensor = SDL_GetSensorFromID(instanceId);
        if (sensor == nint.Zero) {
            throw new InvalidOperationException("GetSensorFromID failed");
=======
    public static nint GetSensorFromID(uint instanceId) {
        nint sensor = SDL_GetSensorFromID(instanceId);
        if (sensor == nint.Zero) {
            throw new InvalidOperationException("SDL_GetSensorFromID failed");
>>>>>>> main
        }
        return sensor;
    }

<<<<<<< HEAD
    /// <summary>Get the instance ID of a sensor.</summary>

    /// <param name="sensor">the SDL_Sensor object to inspect.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the sensor instance ID, or 0 on failure; call <see cref="GetError()" /> for more information.</returns>

    public static uint GetSensorID(nint sensor) {
        uint id = SDL_GetSensorID(sensor);
        if (id == 0) {
            throw new InvalidOperationException("GetSensorID failed");
=======
    public static uint GetSensorID(nint sensor) {
        uint id = SDL_GetSensorID(sensor);
        if (id == 0) {
            throw new InvalidOperationException("SDL_GetSensorID failed");
>>>>>>> main
        }
        return id;
    }

<<<<<<< HEAD
    /// <summary>Get the implementation dependent name of a sensor.</summary>

    /// <param name="sensor">the SDL_Sensor object.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the sensor name or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static string GetSensorName(nint sensor) {
        string name = SDL_GetSensorName(sensor);
        if (name == null) {
            throw new InvalidOperationException("GetSensorName failed");
=======
    public static string GetSensorName(nint sensor) {
        string name = SDL_GetSensorName(sensor);
        if (name == null) {
            throw new InvalidOperationException("SDL_GetSensorName failed");
>>>>>>> main
        }
        return name;
    }

<<<<<<< HEAD
    /// <summary>Get the implementation dependent name of a sensor.</summary>

    /// <param name="instance_id">the sensor instance ID.</param>
    /// <remarks>
    /// This can be called before any sensors are opened.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the sensor name, or <see langword="null" /> if instance_id is notvalid.</returns>

    public static string GetSensorNameForID(uint instanceId) {
        string name = SDL_GetSensorNameForID(instanceId);
        if (name == null) {
            throw new InvalidOperationException("GetSensorNameForID failed");
=======
    public static string GetSensorNameForID(uint instanceId) {
        string name = SDL_GetSensorNameForID(instanceId);
        if (name == null) {
            throw new InvalidOperationException("SDL_GetSensorNameForID failed");
>>>>>>> main
        }
        return name;
    }

<<<<<<< HEAD
    /// <summary>Get the platform dependent type of a sensor.</summary>

    /// <param name="sensor">the SDL_Sensor object to inspect.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the sensor platform dependent type, or -1 if sensor is<see langword="null" />.</returns>

    public static int GetSensorNonPortableType(nint sensor) {
        int type = SDL_GetSensorNonPortableType(sensor);
        if (type == -1) {
            throw new InvalidOperationException("GetSensorNonPortableType failed");
=======
    public static int GetSensorNonPortableType(nint sensor) {
        int type = SDL_GetSensorNonPortableType(sensor);
        if (type == -1) {
            throw new InvalidOperationException("SDL_GetSensorNonPortableType failed");
>>>>>>> main
        }
        return type;
    }

<<<<<<< HEAD
    /// <summary>Get the platform dependent type of a sensor.</summary>

    /// <param name="instance_id">the sensor instance ID.</param>
    /// <remarks>
    /// This can be called before any sensors are opened.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the sensor platform dependent type, or -1 if instance_id is not valid.</returns>

    public static int GetSensorNonPortableTypeForID(uint instanceId) {
        int type = SDL_GetSensorNonPortableTypeForID(instanceId);
        if (type == -1) {
            throw new InvalidOperationException("GetSensorNonPortableTypeForID failed");
=======
    public static int GetSensorNonPortableTypeForID(uint instanceId) {
        int type = SDL_GetSensorNonPortableTypeForID(instanceId);
        if (type == -1) {
            throw new InvalidOperationException("SDL_GetSensorNonPortableTypeForID failed");
>>>>>>> main
        }
        return type;
    }

<<<<<<< HEAD
    /// <summary>Get the properties associated with a sensor.</summary>

    /// <param name="sensor">the SDL_Sensor object.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns a valid property ID on success or 0 on failure; call <see cref="GetError()" /> for more information.</returns>

    public static uint GetSensorProperties(nint sensor) {
        uint properties = SDL_GetSensorProperties(sensor);
        if (properties == 0) {
            throw new InvalidOperationException("GetSensorProperties failed");
=======
    public static uint GetSensorProperties(nint sensor) {
        uint properties = SDL_GetSensorProperties(sensor);
        if (properties == 0) {
            throw new InvalidOperationException("SDL_GetSensorProperties failed");
>>>>>>> main
        }
        return properties;
    }

<<<<<<< HEAD
    /// <summary>Get a list of currently connected sensors.</summary>

    /// <param name="count">a pointer filled in with the number of sensors returned, may be discarded.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(SDL_SensorID *) Returns a 0 terminated array of sensorinstance IDs or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information. This should be freed with <see cref="Free"/> when itis no longer needed.</returns>

    public static nint GetSensors(out int count) {
        nint sensors = SDL_GetSensors(out count);
        if (sensors == nint.Zero) {
            throw new InvalidOperationException("GetSensors failed");
=======
    public static nint GetSensors(out int count) {
        nint sensors = SDL_GetSensors(out count);
        if (sensors == nint.Zero) {
            throw new InvalidOperationException("SDL_GetSensors failed");
>>>>>>> main
        }
        return sensors;
    }

<<<<<<< HEAD
    /// <summary>Get the type of a sensor.</summary>

    /// <param name="sensor">the SDL_Sensor object to inspect.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns theSDL_SensorType type, orSDL_SENSOR_INVALID if sensor is <see langword="null" />.</returns>

    public static SensorType GetSensorType(nint sensor) {
        SensorType type = SDL_GetSensorType(sensor);
        if (type == SensorType.Unknown) {
            throw new InvalidOperationException("GetSensorType failed");
=======
    public static SensorType GetSensorType(nint sensor) {
        SensorType type = SDL_GetSensorType(sensor);
        if (type == SensorType.Unknown) {
            throw new InvalidOperationException("SDL_GetSensorType failed");
>>>>>>> main
        }
        return type;
    }

<<<<<<< HEAD
    /// <summary>Get the type of a sensor.</summary>

    /// <param name="instance_id">the sensor instance ID.</param>
    /// <remarks>
    /// This can be called before any sensors are opened.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns theSDL_SensorType, orSDL_SENSOR_INVALID if instance_id is not valid.</returns>

    public static SensorType GetSensorTypeForID(uint instanceId) {
        SensorType type = SDL_GetSensorTypeForID(instanceId);
        if (type == SensorType.Unknown) {
            throw new InvalidOperationException("GetSensorTypeForID failed");
=======
    public static SensorType GetSensorTypeForID(uint instanceId) {
        SensorType type = SDL_GetSensorTypeForID(instanceId);
        if (type == SensorType.Unknown) {
            throw new InvalidOperationException("SDL_GetSensorTypeForID failed");
>>>>>>> main
        }
        return type;
    }

<<<<<<< HEAD
    /// <summary>Open a sensor for use.</summary>

    /// <param name="instance_id">the sensor instance ID.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(SDL_Sensor *) Returns an SDL_Sensor object or<see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint OpenSensor(uint instanceId) {
        nint sensor = SDL_OpenSensor(instanceId);
        if (sensor == nint.Zero) {
            throw new InvalidOperationException("OpenSensor failed");
=======
    public static nint OpenSensor(uint instanceId) {
        nint sensor = SDL_OpenSensor(instanceId);
        if (sensor == nint.Zero) {
            throw new InvalidOperationException("SDL_OpenSensor failed");
>>>>>>> main
        }
        return sensor;
    }

<<<<<<< HEAD
    /// <summary>Update the current state of the open sensors.</summary>
    /// <remarks>
    /// This is called automatically by the event loop if sensor events are
    /// enabled.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

=======
>>>>>>> main
    public static void UpdateSensors() {
        SDL_UpdateSensors();
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_CloseSensor(nint sensor);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetSensorData(nint sensor, nint data, int numValues);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetSensorFromID(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetSensorID(nint sensor);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetSensorName(nint sensor);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
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
<<<<<<< HEAD

=======
>>>>>>> main
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SensorType SDL_GetSensorType(nint sensor);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SensorType SDL_GetSensorTypeForID(uint instanceId);
<<<<<<< HEAD

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenSensor(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UpdateSensors();
}
=======
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenSensor(uint instanceId);
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UpdateSensors();
}
>>>>>>> main
