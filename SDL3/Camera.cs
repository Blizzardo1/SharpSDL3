using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;

using static SharpSDL3.Sdl;

namespace SharpSDL3; 
public static unsafe partial class Sdl {
    public static nint AcquireCameraFrame(nint camera, out ulong timestampNs) {
        if (camera == nint.Zero) {
            throw new ArgumentNullException(nameof(camera), "Camera handle cannot be null.");
        }
        nint frame = SDL_AcquireCameraFrame(camera, out timestampNs);
        if (frame == nint.Zero) {
            throw new InvalidOperationException($"Failed to acquire camera frame. Camera handle may be invalid.");
        }
        return frame;
    }

    public static void CloseCamera(nint camera) {
        if (camera == nint.Zero) {
            throw new ArgumentNullException(nameof(camera), "Camera handle cannot be null.");
        }
        SDL_CloseCamera(camera);
    }

    public static string GetCameraDriver(int index) {
        if (index < 0 || index >= GetNumCameraDrivers()) {
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
        }

        string driverName = SDL_GetCameraDriver(index);
        if (string.IsNullOrEmpty(driverName)) {
            throw new InvalidOperationException($"Failed to retrieve camera driver at index {index}.");
        }

        return driverName;
    }

    public static SdlBool GetCameraFormat(nint camera, out CameraSpec spec) {
        if (camera == nint.Zero) {
            throw new ArgumentNullException(nameof(camera), "Camera handle cannot be null.");
        }
        SdlBool result = SDL_GetCameraFormat(camera, out spec);
        if (!result) {
            throw new InvalidOperationException($"Failed to get camera format. Camera handle may be invalid.");
        }
        return result;
    }

    public static uint GetCameraID(nint camera) {
        if (camera == nint.Zero) {
            throw new ArgumentNullException(nameof(camera), "Camera handle cannot be null.");
        }
        uint cameraId = SDL_GetCameraID(camera);
        if (cameraId == 0) {
            throw new InvalidOperationException($"Failed to get camera ID. Camera handle may be invalid.");
        }
        return cameraId;
    }

    public static string GetCameraName(uint instanceId) {
        string cameraName = SDL_GetCameraName(instanceId);
        if (string.IsNullOrEmpty(cameraName)) {
            throw new InvalidOperationException($"Failed to retrieve camera name for instance ID {instanceId}.");
        }
        return cameraName;
    }

    public static int GetCameraPermissionState(nint camera) {
        if (camera == nint.Zero) {
            throw new ArgumentNullException(nameof(camera), "Camera handle cannot be null.");
        }
        int permissionState = SDL_GetCameraPermissionState(camera);
        if (permissionState < 0) {
            throw new InvalidOperationException($"Failed to get camera permission state. Error code: {permissionState}");
        }
        return permissionState;
    }

    public static CameraPosition GetCameraPosition(uint instanceId) {
        CameraPosition position = SDL_GetCameraPosition(instanceId);

        // Add validation or additional logic to make the wrapper less trivial  
        if (!Enum.IsDefined(position)) {
            throw new InvalidOperationException($"Invalid camera position value '{position}' for instance ID {instanceId}.");
        }

        return position;
    }

    public static uint GetCameraProperties(nint camera) {
        if (camera == nint.Zero) {
            throw new ArgumentNullException(nameof(camera), "Camera handle cannot be null.");
        }
        uint properties = SDL_GetCameraProperties(camera);
        if (properties == 0) {
            throw new InvalidOperationException($"Failed to get camera properties. Camera handle may be invalid.");
        }
        return properties;
    }

    public static Span<nint> GetCameras(out int count) {
        nint result = SDL_GetCameras(out count);
        
        if(result == nint.Zero) {
            LogError(LogCategory.Error, "Failed to retrieve camera list.");
            return [];
        }

        if (count <= 0) {
            LogWarn(LogCategory.System, "No cameras found.");
            return [];
        }

        nint[] ptrs = new nint[count];

        for(int i = 0; i < count; i++) {
            ptrs[i] = Marshal.ReadIntPtr(result, i * sizeof(nint));
        }

        Span<nint> cameras = new(ptrs);

        return cameras.ToArray();

    }

    public static Span<nint> GetCameraSupportedFormats(uint devid, out int count) {
        nint result = SDL_GetCameraSupportedFormats(devid, out count);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, "Failed to retrieve camera formats.");
            return [];
        }

        if (count <= 0) {
            LogWarn(LogCategory.System, "No camera formats found.");
            return [];
        }

        nint[] ptrs = new nint[count];

        for (int i = 0; i < count; i++) {
            ptrs[i] = Marshal.ReadIntPtr(result, i * sizeof(nint));
        }

        Span<nint> cameraFormats = new(ptrs);

        return cameraFormats.ToArray();
    }

    public static string GetCurrentCameraDriver() {
        string driverName = SDL_GetCurrentCameraDriver();
        if (string.IsNullOrEmpty(driverName)) {
            throw new InvalidOperationException("Failed to retrieve current camera driver.");
        }
        return driverName;
    }

    public static int GetNumCameraDrivers() {
        return SDL_GetNumCameraDrivers();
    }

    public static nint OpenCamera(uint instanceId, ref CameraSpec spec) {
        nint camera = SDL_OpenCamera(instanceId, ref spec);
        if (camera == nint.Zero) {
            throw new InvalidOperationException($"Failed to open camera with instance ID {instanceId}.");
        }
        return camera;
    }

    public static void ReleaseCameraFrame(nint camera, nint frame) {
        if (camera == nint.Zero) {
            throw new ArgumentNullException(nameof(camera), "Camera handle cannot be null.");
        }
        if (frame == nint.Zero) {
            throw new ArgumentNullException(nameof(frame), "Frame handle cannot be null.");
        }
        SDL_ReleaseCameraFrame(camera, frame);
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_AcquireCameraFrame(nint camera, out ulong timestampNs);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_CloseCamera(nint camera);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetCameraDriver(int index);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetCameraFormat(nint camera, out CameraSpec spec);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetCameraID(nint camera);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetCameraName(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetCameraPermissionState(nint camera);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial CameraPosition SDL_GetCameraPosition(uint instanceId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetCameraProperties(nint camera);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetCameras(out int count);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetCameraSupportedFormats(uint devid, out int count);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetCurrentCameraDriver();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumCameraDrivers();
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenCamera(uint instanceId, ref CameraSpec spec);
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseCameraFrame(nint camera, nint frame);
}
