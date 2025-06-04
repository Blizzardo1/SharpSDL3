using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SharpSDL3;

public static unsafe partial class Sdl {
    /// <summary>Acquire a frame.</summary>

    /// <param name="camera">opened camera device.</param>
    /// <param name="timestampNS">a pointer filled in with the frame's timestamp, or 0 on error. Can be <see langword="null" />.</param>
    /// <remarks>
    /// The frame is a memory pointer to the image data, whose size and format are
    /// given by the spec requested when opening the device.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ReleaseCameraFrame"/>
    /// </remarks>
    /// <returns>(SDL_Surface *) Returns a new frame of video on success,<see langword="null" /> if none is currently available.</returns>

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

    /// <summary>Use this function to shut down camera processing and close the camera device.</summary>

    /// <param name="camera">opened camera device.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, but no thread mayreference device once this function is called.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="OpenCamera"/>
    /// </remarks>

    public static void CloseCamera(nint camera) {
        if (camera == nint.Zero) {
            throw new ArgumentNullException(nameof(camera), "Camera handle cannot be null.");
        }
        SDL_CloseCamera(camera);
    }

    /// <summary>Use this function to get the name of a built in camera driver.</summary>

    /// <param name="index">the index of the camera driver; the value ranges from 0 to SDL_GetNumCameraDrivers() - 1.</param>
    /// <remarks>
    /// The list of camera drivers is given in the order that they are normally
    /// initialized by default; the drivers that seem more reasonable to choose
    /// first (as far as the SDL developers believe) are earlier in the list.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetNumCameraDrivers"/>
    /// </remarks>
    /// <returns>Returns the name of the camera driver at the requestedindex, or <see langword="null" /> if an invalid index was specified.</returns>

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

    /// <summary>Get the spec that a camera is using when generating images.</summary>

    /// <param name="camera">opened camera device.</param>
    /// <param name="spec">the SDL_CameraSpec to be initialized by this function.</param>
    /// <remarks>
    /// Note that this might not be the native format of the hardware, as SDL might
    /// be converting to this format behind the scenes.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="OpenCamera"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

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

    /// <summary>Get the instance ID of an opened camera.</summary>

    /// <param name="camera">an SDL_Camera to query.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="OpenCamera"/>
    /// </remarks>
    /// <returns>Returns the instance ID of the specifiedcamera on success or 0 on failure; call <see cref="GetError()" /> for more information.</returns>

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

    /// <summary>Get the human-readable device name for a camera.</summary>

    /// <param name="instance_id">the camera device instance ID.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetCameras"/>
    /// </remarks>
    /// <returns>Returns a human-readable device name or <see langword="null" /> on failure;call <see cref="GetError()" /> for more information.</returns>

    public static string GetCameraName(uint instanceId) {
        string cameraName = SDL_GetCameraName(instanceId);
        if (string.IsNullOrEmpty(cameraName)) {
            throw new InvalidOperationException($"Failed to retrieve camera name for instance ID {instanceId}.");
        }
        return cameraName;
    }

    /// <summary>Query if camera access has been approved by the user.</summary>

    /// <param name="camera">the opened camera device to query.</param>
    /// <remarks>
    /// Cameras will not function between when the device is opened by the app and
    /// when the user permits access to the hardware. On some platforms, this
    /// presents as a popup dialog where the user has to explicitly approve access;
    /// on others the approval might be implicit and not alert the user at all.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="OpenCamera"/>
    /// <seealso cref="CloseCamera"/>
    /// </remarks>
    /// <returns>Returns -1 if user denied access to the camera, 1 if user approvedaccess, 0 if no decision has been made yet.</returns>

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

    /// <summary>Get the position of the camera in relation to the system.</summary>

    /// <param name="instance_id">the camera device instance ID.</param>
    /// <remarks>
    /// Most platforms will report UNKNOWN, but mobile devices, like phones, can
    /// often make a distinction between cameras on the front of the device (that
    /// points towards the user, for taking &quot;selfies&quot;) and cameras on the back (for
    /// filming in the direction the user is facing).
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetCameras"/>
    /// </remarks>
    /// <returns>Returns the position of thecamera on the system hardware.</returns>

    public static CameraPosition GetCameraPosition(uint instanceId) {
        CameraPosition position = SDL_GetCameraPosition(instanceId);

        // Add validation or additional logic to make the wrapper less trivial
        if (!Enum.IsDefined(position)) {
            throw new InvalidOperationException($"Invalid camera position value '{position}' for instance ID {instanceId}.");
        }

        return position;
    }

    /// <summary>Get the properties associated with an opened camera.</summary>

    /// <param name="camera">the SDL_Camera obtained from SDL_OpenCamera().</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns a valid property ID on success or 0 on failure; call <see cref="GetError()" /> for more information.</returns>

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

    /// <summary>Get a list of currently connected camera devices.</summary>

    /// <param name="count">a pointer filled in with the number of cameras returned, may be discarded.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="OpenCamera"/>
    /// </remarks>
    /// <returns>(SDL_CameraID *) Returns a 0 terminated array of camerainstance IDs or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information. This should be freed with <see cref="Free"/> when itis no longer needed.</returns>

    public static Span<nint> GetCameras(out int count) {
        nint result = SDL_GetCameras(out count);

        if (result == nint.Zero) {
            LogError(LogCategory.Error, "Failed to retrieve camera list.");
            return [];
        }

        if (count <= 0) {
            LogWarn(LogCategory.System, "No cameras found.");
            return [];
        }

        nint[] ptrs = new nint[count];

        for (int i = 0; i < count; i++) {
            ptrs[i] = Marshal.ReadIntPtr(result, i * sizeof(nint));
        }

        Span<nint> cameras = new(ptrs);

        return cameras.ToArray();
    }

    /// <summary>Get the list of native formats/sizes a camera supports.</summary>

    /// <param name="instance_id">the camera device instance ID.</param>
    /// <param name="count">a pointer filled in with the number of elements in the list, may be discarded.</param>
    /// <remarks>
    /// This returns a list of all formats and frame sizes that a specific camera
    /// can offer. This is useful if your app can accept a variety of image formats
    /// and sizes and so want to find the optimal spec that doesn't require
    /// conversion.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetCameras"/>
    /// <seealso cref="OpenCamera"/>
    /// </remarks>
    /// <returns>(SDL_CameraSpec **) Returns a <see langword="null" /> terminated array ofpointers to SDL_CameraSpec or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information. This is a single allocation that should be freed with <see cref="Free"/> when it is no longer needed.</returns>

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

    /// <summary>Get the name of the current camera driver.</summary>
    /// <remarks>
    /// The names of drivers are all simple, low-ASCII identifiers, like &quot;v4l2&quot;,
    /// &quot;coremedia&quot; or &quot;android&quot;. These never have Unicode characters, and are not
    /// meant to be proper names.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the name of the current camera driver or <see langword="null" /> if nodriver has been initialized.</returns>

    public static string GetCurrentCameraDriver() {
        string driverName = SDL_GetCurrentCameraDriver();
        if (string.IsNullOrEmpty(driverName)) {
            throw new InvalidOperationException("Failed to retrieve current camera driver.");
        }
        return driverName;
    }

    /// <summary>Use this function to get the number of built-in camera drivers.</summary>
    /// <remarks>
    /// This function returns a hardcoded number. This never returns a negative
    /// value; if there are no drivers compiled into this build of SDL, this
    /// function returns zero. The presence of a driver in this list does not mean
    /// it will function, it just means SDL is capable of interacting with that
    /// interface. For example, a build of SDL might have v4l2 support, but if
    /// there's no kernel support available, SDL's v4l2 driver would fail if used.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetCameraDriver"/>
    /// </remarks>
    /// <returns>Returns the number of built-in camera drivers.</returns>

    public static int GetNumCameraDrivers() {
        return SDL_GetNumCameraDrivers();
    }

    /// <summary>Open a video recording device (a &quot;camera&quot;).</summary>

    /// <param name="instance_id">the camera device instance ID.</param>
    /// <param name="spec">the desired format for data the device will provide. Can be <see langword="null" />.</param>
    /// <remarks>
    /// You can open the device with any reasonable spec, and if the hardware can't
    /// directly support it, it will convert data seamlessly to the requested
    /// format. This might incur overhead, including scaling of image data.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetCameras"/>
    /// <seealso cref="GetCameraFormat"/>
    /// </remarks>
    /// <returns>(SDL_Camera *) Returns an SDL_Camera object or<see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint OpenCamera(uint instanceId, ref CameraSpec spec) {
        nint camera = SDL_OpenCamera(instanceId, ref spec);
        if (camera == nint.Zero) {
            throw new InvalidOperationException($"Failed to open camera with instance ID {instanceId}.");
        }
        return camera;
    }

    /// <summary>Release a frame of video acquired from a camera.</summary>

    /// <param name="camera">opened camera device.</param>
    /// <param name="frame">the video frame surface to release.</param>
    /// <remarks>
    /// Let the back-end re-use the internal buffer for camera.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="AcquireCameraFrame"/>
    /// </remarks>

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