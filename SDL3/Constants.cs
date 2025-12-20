namespace SharpSDL3;

/// <summary>
/// SDL String Properties
/// </summary>
public class Constants {
    // /usr/local/include/SDL3/SDL_thread.h
    public const string SdlPropThreadCreateEntryFunctionPointer = "SDL.thread.create.entry_function";
    public const string SdlPropThreadCreateNameString = "SDL.thread.create.name";
    public const string SdlPropThreadCreateUserdataPointer = "SDL.thread.create.userdata";
    public const string SdlPropThreadCreateStackSizeNumber = "SDL.thread.create.stacksize";

    // /usr/local/include/SDL3/SDL_iostream.h

    public const string SdlPropIoStreamWindowsHandlePointer = "SDL.iostream.windows.handle";
    public const string SdlPropIoStreamStdioFilePointer = "SDL.iostream.stdio.file";
    public const string SdlPropIoStreamFileDescriptorNumber = "SDL.iostream.file_descriptor";
    public const string SdlPropIoStreamAndroidAssetPointer = "SDL.iostream.android.aasset";
    public const string SdlPropIoStreamMemoryPointer = "SDL.iostream.memory.base";
    public const string SdlPropIoStreamMemorySizeNumber = "SDL.iostream.memory.size";
    public const string SdlPropIoStreamDynamicMemoryPointer = "SDL.iostream.dynamic.memory";
    public const string SdlPropIoStreamDynamicChunkSizeNumber = "SDL.iostream.dynamic.chunksize";

    // /usr/local/include/SDL3/SDL_surface.h

    public const string SdlPropSurfaceSdrWhitePointFloat = "SDL.surface.SDR_white_point";
    public const string SdlPropSurfaceHdrHeadroomFloat = "SDL.surface.HDR_headroom";
    public const string SdlPropSurfaceToneMapOperatorString = "SDL.surface.tonemap";
    public const string SdlPropSurfaceHotspotXNumber = "SDL.surface.hotspot.x";
    public const string SdlPropSurfaceHotspotYNumber = "SDL.surface.hotspot.y";

    // /usr/local/include/SDL3/SDL_video.h

    public const string SdlPropGlobalVideoWaylandWlDisplayPointer = "SDL.video.wayland.wl_display";
    public const string SdlPropDisplayHdrEnabledBoolean = "SDL.display.HDR_enabled";
    public const string SdlPropDisplayKmsDrmPanelOrientationNumber = "SDL.display.KMSDRM.panel_orientation";
    public const string SdlPropDisplayWaylandWlOutputPointer = "SDL.display.wayland.wl_output";
    public const string SdlPropWindowCreateAlwaysOnTopBoolean = "SDL.window.create.always_on_top";
    public const string SdlPropWindowCreateBorderlessBoolean = "SDL.window.create.borderless";
    public const string SdlPropWindowCreateConstrainPopupBoolean = "SDL.window.create.constrain_popup";
    public const string SdlPropWindowCreateFocusableBoolean = "SDL.window.create.focusable";
    public const string SdlPropWindowCreateExternalGraphicsContextBoolean = "SDL.window.create.external_graphics_context";
    public const string SdlPropWindowCreateFlagsNumber = "SDL.window.create.flags";
    public const string SdlPropWindowCreateFullscreenBoolean = "SDL.window.create.fullscreen";
    public const string SdlPropWindowCreateHeightNumber = "SDL.window.create.height";
    public const string SdlPropWindowCreateHiddenBoolean = "SDL.window.create.hidden";
    public const string SdlPropWindowCreateHighPixelDensityBoolean = "SDL.window.create.high_pixel_density";
    public const string SdlPropWindowCreateMaximizedBoolean = "SDL.window.create.maximized";
    public const string SdlPropWindowCreateMenuBoolean = "SDL.window.create.menu";
    public const string SdlPropWindowCreateMetalBoolean = "SDL.window.create.metal";
    public const string SdlPropWindowCreateMinimizedBoolean = "SDL.window.create.minimized";
    public const string SdlPropWindowCreateModalBoolean = "SDL.window.create.modal";
    public const string SdlPropWindowCreateMouseGrabbedBoolean = "SDL.window.create.mouse_grabbed";
    public const string SdlPropWindowCreateOpenglBoolean = "SDL.window.create.opengl";
    public const string SdlPropWindowCreateParentPointer = "SDL.window.create.parent";
    public const string SdlPropWindowCreateResizableBoolean = "SDL.window.create.resizable";
    public const string SdlPropWindowCreateTitleString = "SDL.window.create.title";
    public const string SdlPropWindowCreateTransparentBoolean = "SDL.window.create.transparent";
    public const string SdlPropWindowCreateTooltipBoolean = "SDL.window.create.tooltip";
    public const string SdlPropWindowCreateUtilityBoolean = "SDL.window.create.utility";
    public const string SdlPropWindowCreateVulkanBoolean = "SDL.window.create.vulkan";
    public const string SdlPropWindowCreateWidthNumber = "SDL.window.create.width";
    public const string SdlPropWindowCreateXNumber = "SDL.window.create.x";
    public const string SdlPropWindowCreateYNumber = "SDL.window.create.y";
    public const string SdlPropWindowCreateCocoaWindowPointer = "SDL.window.create.cocoa.window";
    public const string SdlPropWindowCreateCocoaViewPointer = "SDL.window.create.cocoa.view";
    public const string SdlPropWindowCreateWaylandSurfaceRoleCustomBoolean = "SDL.window.create.wayland.surface_role_custom";
    public const string SdlPropWindowCreateWaylandCreateEglWindowBoolean = "SDL.window.create.wayland.create_egl_window";
    public const string SdlPropWindowCreateWaylandWlSurfacePointer = "SDL.window.create.wayland.wl_surface";
    public const string SdlPropWindowCreateWin32HwndPointer = "SDL.window.create.win32.hwnd";
    public const string SdlPropWindowCreateWin32PixelFormatHwndPointer = "SDL.window.create.win32.pixel_format_hwnd";
    public const string SdlPropWindowCreateX11WindowNumber = "SDL.window.create.x11.window";
    public const string SdlPropWindowCreateEmscriptenCanvasIdString = "SDL.window.create.emscripten.canvas_id";
    public const string SdlPropWindowCreateEmscriptenKeyboardElementString = "SDL.window.create.emscripten.keyboard_element";
    public const string SdlPropWindowShapePointer = "SDL.window.shape";
    public const string SdlPropWindowHdrEnabledBoolean = "SDL.window.HDR_enabled";
    public const string SdlPropWindowSdrWhiteLevelFloat = "SDL.window.SDR_white_level";
    public const string SdlPropWindowHdrHeadroomFloat = "SDL.window.HDR_headroom";
    public const string SdlPropWindowAndroidWindowPointer = "SDL.window.android.window";
    public const string SdlPropWindowAndroidSurfacePointer = "SDL.window.android.surface";
    public const string SdlPropWindowUikitWindowPointer = "SDL.window.uikit.window";
    public const string SdlPropWindowUikitMetalViewTagNumber = "SDL.window.uikit.metal_view_tag";
    public const string SdlPropWindowUikitOpenglFramebufferNumber = "SDL.window.uikit.opengl.framebuffer";
    public const string SdlPropWindowUikitOpenglRenderbufferNumber = "SDL.window.uikit.opengl.renderbuffer";
    public const string SdlPropWindowUikitOpenglResolveFramebufferNumber = "SDL.window.uikit.opengl.resolve_framebuffer";
    public const string SdlPropWindowKmsDrmDeviceIndexNumber = "SDL.window.kmsdrm.dev_index";
    public const string SdlPropWindowKmsDrmDrmFdNumber = "SDL.window.kmsdrm.drm_fd";
    public const string SdlPropWindowKmsDrmGbmDevicePointer = "SDL.window.kmsdrm.gbm_dev";
    public const string SdlPropWindowCocoaWindowPointer = "SDL.window.cocoa.window";
    public const string SdlPropWindowCocoaMetalViewTagNumber = "SDL.window.cocoa.metal_view_tag";
    public const string SdlPropWindowOpenvrOverlayIdNumber = "SDL.window.openvr.overlay_id";
    public const string SdlPropWindowVivanteDisplayPointer = "SDL.window.vivante.display";
    public const string SdlPropWindowVivanteWindowPointer = "SDL.window.vivante.window";
    public const string SdlPropWindowVivanteSurfacePointer = "SDL.window.vivante.surface";
    public const string SdlPropWindowWin32HwndPointer = "SDL.window.win32.hwnd";
    public const string SdlPropWindowWin32HdcPointer = "SDL.window.win32.hdc";
    public const string SdlPropWindowWin32InstancePointer = "SDL.window.win32.instance";
    public const string SdlPropWindowWaylandDisplayPointer = "SDL.window.wayland.display";
    public const string SdlPropWindowWaylandSurfacePointer = "SDL.window.wayland.surface";
    public const string SdlPropWindowWaylandViewportPointer = "SDL.window.wayland.viewport";
    public const string SdlPropWindowWaylandEglWindowPointer = "SDL.window.wayland.egl_window";
    public const string SdlPropWindowWaylandXdgSurfacePointer = "SDL.window.wayland.xdg_surface";
    public const string SdlPropWindowWaylandXdgToplevelPointer = "SDL.window.wayland.xdg_toplevel";
    public const string SdlPropWindowWaylandXdgToplevelExportHandleString = "SDL.window.wayland.xdg_toplevel_export_handle";
    public const string SdlPropWindowWaylandXdgPopupPointer = "SDL.window.wayland.xdg_popup";
    public const string SdlPropWindowWaylandXdgPositionerPointer = "SDL.window.wayland.xdg_positioner";
    public const string SdlPropWindowX11DisplayPointer = "SDL.window.x11.display";
    public const string SdlPropWindowX11ScreenNumber = "SDL.window.x11.screen";
    public const string SdlPropWindowX11WindowNumber = "SDL.window.x11.window";
    public const string SdlPropWindowEmscriptenCanvasIdString = "SDL.window.emscripten.canvas_id";
    public const string SdlPropWindowEmscriptenKeyboardElementString = "SDL.window.emscripten.keyboard_element";

    // /usr/local/include/SDL3/SDL_dialog.h

    public const string SdlPropFileDialogFiltersPointer = "SDL.filedialog.filters";
    public const string SdlPropFileDialogNfiltersNumber = "SDL.filedialog.nfilters";
    public const string SdlPropFileDialogWindowPointer = "SDL.filedialog.window";
    public const string SdlPropFileDialogLocationString = "SDL.filedialog.location";
    public const string SdlPropFileDialogManyBoolean = "SDL.filedialog.many";
    public const string SdlPropFileDialogTitleString = "SDL.filedialog.title";
    public const string SdlPropFileDialogAcceptString = "SDL.filedialog.accept";
    public const string SdlPropFileDialogCancelString = "SDL.filedialog.cancel";

    // /usr/local/include/SDL3/SDL_joystick.h

    public const string SdlPropJoystickCapMonoLedBoolean = "SDL.joystick.cap.mono_led";
    public const string SdlPropJoystickCapRgbLedBoolean = "SDL.joystick.cap.rgb_led";
    public const string SdlPropJoystickCapPlayerLedBoolean = "SDL.joystick.cap.player_led";
    public const string SdlPropJoystickCapRumbleBoolean = "SDL.joystick.cap.rumble";
    public const string SdlPropJoystickCapTriggerRumbleBoolean = "SDL.joystick.cap.trigger_rumble";

    // /usr/local/include/SDL3/SDL_keyboard.h

    /// <summary>
    /// Please refer to <see cref="Sdl.StartTextInputWithProperties"/> for details.
    /// </summary>
    public const string SdlPropTextInputTypeNumber = "SDL.textinput.type";

    /// <summary>
    /// Please refer to <see cref="Sdl.StartTextInputWithProperties"/> for details.
    /// </summary>
    public const string SdlPropTextInputCapitalizationNumber = "SDL.textinput.capitalization";

    /// <summary>
    /// Please refer to <see cref="Sdl.StartTextInputWithProperties"/> for details.
    /// </summary>
    public const string SdlPropTextInputAutocorrectBoolean = "SDL.textinput.autocorrect";

    /// <summary>
    /// Please refer to <see cref="Sdl.StartTextInputWithProperties"/> for details.
    /// </summary>
    public const string SdlPropTextInputMultilineBoolean = "SDL.textinput.multiline";

    /// <summary>
    /// Please refer to <see cref="Sdl.StartTextInputWithProperties"/> for details.
    /// </summary>
    public const string SdlPropTextInputAndroidInputTypeNumber = "SDL.textinput.android.inputtype";

    // /usr/local/include/SDL3/SDL_gpu.h

    public const string SdlPropGpuDeviceCreateDebugModeBoolean = "SDL.gpu.device.create.debugmode";
    public const string SdlPropGpuDeviceCreatePreferLowPowerBoolean = "SDL.gpu.device.create.preferlowpower";
    public const string SdlPropGpuDeviceCreateVerboseBoolean = "SDL.gpu.device.create.verbose";
    public const string SdlPropGpuDeviceCreateNameString = "SDL.gpu.device.create.name";
    public const string SdlPropGpuDeviceCreateShadersPrivateBoolean = "SDL.gpu.device.create.shaders.private";
    public const string SdlPropGpuDeviceCreateShadersSpirvBoolean = "SDL.gpu.device.create.shaders.spirv";
    public const string SdlPropGpuDeviceCreateShadersDxbcBoolean = "SDL.gpu.device.create.shaders.dxbc";
    public const string SdlPropGpuDeviceCreateShadersDxilBoolean = "SDL.gpu.device.create.shaders.dxil";
    public const string SdlPropGpuDeviceCreateShadersMslBoolean = "SDL.gpu.device.create.shaders.msl";
    public const string SdlPropGpuDeviceCreateShadersMetallibBoolean = "SDL.gpu.device.create.shaders.metallib";
    public const string SdlPropGpuDeviceCreateD3D12SemanticNameString = "SDL.gpu.device.create.d3d12.semantic";
    public const string SdlPropGpuDeviceNameString = "SDL.gpu.device.name";
    public const string SdlPropGpuDeviceDriverNameString = "SDL.gpu.device.driver_name";
    public const string SdlPropGpuDeviceDriverVersionString = "SDL.gpu.device.driver_version";
    public const string SdlPropGpuDeviceDriverInfoString = "SDL.gpu.device.driver_info";
    public const string SdlPropGpuComputePipelineCreateNameString = "SDL.gpu.computepipeline.create.name";
    public const string SdlPropGpuGraphicsPipelineCreateNameString = "SDL.gpu.graphicspipeline.create.name";
    public const string SdlPropGpuSamplerCreateNameString = "SDL.gpu.sampler.create.name";
    public const string SdlPropGpuShaderCreateNameString = "SDL.gpu.shader.create.name";

    /// <summary>
    /// Please refer to <see cref="Sdl.CreateGpuTexture"/> for details.
    /// </summary>
    public const string SdlPropGpuTextureCreateD3D12ClearRFloat = "SDL.gpu.texture.create.d3d12.clear.r";
    
    /// <summary>
    /// Please refer to <see cref="Sdl.CreateGpuTexture"/> for details.
    /// </summary>
    public const string SdlPropGpuTextureCreateD3D12ClearGFloat = "SDL.gpu.texture.create.d3d12.clear.g";

    /// <summary>
    /// Please refer to <see cref="Sdl.CreateGpuTexture"/> for details.
    /// </summary>
    public const string SdlPropGpuTextureCreateD3D12ClearBFloat = "SDL.gpu.texture.create.d3d12.clear.b";

    /// <summary>
    /// Please refer to <see cref="Sdl.CreateGpuTexture"/> for details.
    /// </summary>
    public const string SdlPropGpuTextureCreateD3D12ClearAFloat = "SDL.gpu.texture.create.d3d12.clear.a";

    /// <summary>
    /// Please refer to <see cref="Sdl.CreateGpuTexture"/> for details.
    /// </summary>
    public const string SdlPropGpuTextureCreateD3D12ClearDepthFloat = "SDL.gpu.texture.create.d3d12.clear.depth";

    /// <summary>
    /// Please refer to <see cref="Sdl.CreateGpuTexture"/> for details.
    /// </summary>
    public const string SdlPropGpuTextureCreateD3D12ClearStencilNumber = "SDL.gpu.texture.create.d3d12.clear.stencil";

    /// <summary>
    /// Please refer to <see cref="Sdl.CreateGpuTexture"/> for details.
    /// </summary>
    public const string SdlPropGpuTextureCreateNameString = "SDL.gpu.texture.create.name";


    public const string SdlPropGpuBufferCreateNameString = "SDL.gpu.buffer.create.name";
    public const string SdlPropGpuTransferBufferCreateNameString = "SDL.gpu.transferbuffer.create.name";

    // /usr/local/include/SDL3/SDL_hints.h

    public const string SdlHintAllowAltTabWhileGrabbed = "ALLOW_ALT_TAB_WHILE_GRABBED";
    public const string SdlHintAndroidAllowRecreateActivity = "ANDROID_ALLOW_RECREATE_ACTIVITY";
    public const string SdlHintAndroidBlockOnPause = "ANDROID_BLOCK_ON_PAUSE";
    public const string SdlHintAndroidLowLatencyAudio = "ANDROID_LOW_LATENCY_AUDIO";
    public const string SdlHintAndroidTrapBackButton = "ANDROID_TRAP_BACK_BUTTON";
    public const string SdlHintAppId = "APP_ID";
    public const string SdlHintAppName = "APP_NAME";
    public const string SdlHintAppleTvControllerUiEvents = "APPLE_TV_CONTROLLER_UI_EVENTS";
    public const string SdlHintAppleTvRemoteAllowRotation = "APPLE_TV_REMOTE_ALLOW_ROTATION";
    public const string SdlHintAudioAlsaDefaultDevice = "AUDIO_ALSA_DEFAULT_DEVICE";
    public const string SdlHintAudioAlsaDefaultPlaybackDevice = "AUDIO_ALSA_DEFAULT_PLAYBACK_DEVICE";
    public const string SdlHintAudioAlsaDefaultRecordingDevice = "AUDIO_ALSA_DEFAULT_RECORDING_DEVICE";
    public const string SdlHintAudioCategory = "AUDIO_CATEGORY";
    public const string SdlHintAudioChannels = "AUDIO_CHANNELS";
    public const string SdlHintAudioDeviceAppIconName = "AUDIO_DEVICE_APP_ICON_NAME";
    public const string SdlHintAudioDeviceSampleFrames = "AUDIO_DEVICE_SAMPLE_FRAMES";
    public const string SdlHintAudioDeviceStreamName = "AUDIO_DEVICE_STREAM_NAME";
    public const string SdlHintAudioDeviceStreamRole = "AUDIO_DEVICE_STREAM_ROLE";
    public const string SdlHintAudioDiskInputFile = "AUDIO_DISK_INPUT_FILE";
    public const string SdlHintAudioDiskOutputFile = "AUDIO_DISK_OUTPUT_FILE";
    public const string SdlHintAudioDiskTimescale = "AUDIO_DISK_TIMESCALE";
    public const string SdlHintAudioDriver = "AUDIO_DRIVER";
    public const string SdlHintAudioDummyTimescale = "AUDIO_DUMMY_TIMESCALE";
    public const string SdlHintAudioFormat = "AUDIO_FORMAT";
    public const string SdlHintAudioFrequency = "AUDIO_FREQUENCY";
    public const string SdlHintAudioIncludeMonitors = "AUDIO_INCLUDE_MONITORS";
    public const string SdlHintAutoUpdateJoysticks = "AUTO_UPDATE_JOYSTICKS";
    public const string SdlHintAutoUpdateSensors = "AUTO_UPDATE_SENSORS";
    public const string SdlHintBmpSaveLegacyFormat = "BMP_SAVE_LEGACY_FORMAT";
    public const string SdlHintCameraDriver = "CAMERA_DRIVER";
    public const string SdlHintCpuFeatureMask = "CPU_FEATURE_MASK";
    public const string SdlHintJoystickDirectInput = "JOYSTICK_DIRECTINPUT";
    public const string SdlHintFileDialogDriver = "FILE_DIALOG_DRIVER";
    public const string SdlHintDisplayUsableBounds = "DISPLAY_USABLE_BOUNDS";
    public const string SdlHintEmscriptenAsyncify = "EMSCRIPTEN_ASYNCIFY";
    public const string SdlHintEmscriptenCanvasSelector = "EMSCRIPTEN_CANVAS_SELECTOR";
    public const string SdlHintEmscriptenKeyboardElement = "EMSCRIPTEN_KEYBOARD_ELEMENT";
    public const string SdlHintEnableScreenKeyboard = "ENABLE_SCREEN_KEYBOARD";
    public const string SdlHintEvdevDevices = "EVDEV_DEVICES";
    public const string SdlHintEventLogging = "EVENT_LOGGING";
    public const string SdlHintForceRaiseWindow = "FORCE_RAISEWINDOW";
    public const string SdlHintFramebufferAcceleration = "FRAMEBUFFER_ACCELERATION";
    public const string SdlHintGameControllerConfig = "GAMECONTROLLERCONFIG";
    public const string SdlHintGameControllerConfigFile = "GAMECONTROLLERCONFIG_FILE";
    public const string SdlHintGameControllerType = "GAMECONTROLLERTYPE";
    public const string SdlHintGameControllerIgnoreDevices = "GAMECONTROLLER_IGNORE_DEVICES";
    public const string SdlHintGameControllerIgnoreDevicesExcept = "GAMECONTROLLER_IGNORE_DEVICES_EXCEPT";
    public const string SdlHintGameControllerSensorFusion = "GAMECONTROLLER_SENSOR_FUSION";
    public const string SdlHintGdkTextInputDefaultText = "GDK_TEXTINPUT_DEFAULT_TEXT";
    public const string SdlHintGdkTextInputDescription = "GDK_TEXTINPUT_DESCRIPTION";
    public const string SdlHintGdkTextInputMaxLength = "GDK_TEXTINPUT_MAX_LENGTH";
    public const string SdlHintGdkTextInputScope = "GDK_TEXTINPUT_SCOPE";
    public const string SdlHintGdkTextInputTitle = "GDK_TEXTINPUT_TITLE";
    public const string SdlHintHidapiLibUsb = "HIDAPI_LIBUSB";
    public const string SdlHintHidapiLibUsbWhitelist = "HIDAPI_LIBUSB_WHITELIST";
    public const string SdlHintHidapiUdev = "HIDAPI_UDEV";
    public const string SdlHintGpuDriver = "GPU_DRIVER";
    public const string SdlHintHidapiEnumerateOnlyControllers = "HIDAPI_ENUMERATE_ONLY_CONTROLLERS";
    public const string SdlHintHidapiIgnoreDevices = "HIDAPI_IGNORE_DEVICES";
    public const string SdlHintImeImplementedUi = "IME_IMPLEMENTED_UI";
    public const string SdlHintIosHideHomeIndicator = "IOS_HIDE_HOME_INDICATOR";
    public const string SdlHintJoystickAllowBackgroundEvents = "JOYSTICK_ALLOW_BACKGROUND_EVENTS";
    public const string SdlHintJoystickArcadeStickDevices = "JOYSTICK_ARCADESTICK_DEVICES";
    public const string SdlHintJoystickArcadeStickDevicesExcluded = "JOYSTICK_ARCADESTICK_DEVICES_EXCLUDED";
    public const string SdlHintJoystickBlacklistDevices = "JOYSTICK_BLACKLIST_DEVICES";
    public const string SdlHintJoystickBlacklistDevicesExcluded = "JOYSTICK_BLACKLIST_DEVICES_EXCLUDED";
    public const string SdlHintJoystickDevice = "JOYSTICK_DEVICE";
    public const string SdlHintJoystickEnhancedReports = "JOYSTICK_ENHANCED_REPORTS";
    public const string SdlHintJoystickFlightStickDevices = "JOYSTICK_FLIGHTSTICK_DEVICES";
    public const string SdlHintJoystickFlightStickDevicesExcluded = "JOYSTICK_FLIGHTSTICK_DEVICES_EXCLUDED";
    public const string SdlHintJoystickGameInput = "JOYSTICK_GAMEINPUT";
    public const string SdlHintJoystickGameCubeDevices = "JOYSTICK_GAMECUBE_DEVICES";
    public const string SdlHintJoystickGameCubeDevicesExcluded = "JOYSTICK_GAMECUBE_DEVICES_EXCLUDED";
    public const string SdlHintJoystickHidapi = "JOYSTICK_HIDAPI";
    public const string SdlHintJoystickHidapiCombineJoyCons = "JOYSTICK_HIDAPI_COMBINE_JOY_CONS";
    public const string SdlHintJoystickHidapiGameCube = "JOYSTICK_HIDAPI_GAMECUBE";
    public const string SdlHintJoystickHidapiGameCubeRumbleBrake = "JOYSTICK_HIDAPI_GAMECUBE_RUMBLE_BRAKE";
    public const string SdlHintJoystickHidapiJoyCons = "JOYSTICK_HIDAPI_JOY_CONS";
    public const string SdlHintJoystickHidapiJoyConHomeLed = "JOYSTICK_HIDAPI_JOYCON_HOME_LED";
    public const string SdlHintJoystickHidapiLuna = "JOYSTICK_HIDAPI_LUNA";
    public const string SdlHintJoystickHidapiNintendoClassic = "JOYSTICK_HIDAPI_NINTENDO_CLASSIC";
    public const string SdlHintJoystickHidapiPs3 = "JOYSTICK_HIDAPI_PS3";
    public const string SdlHintJoystickHidapiPs3SixAxisDriver = "JOYSTICK_HIDAPI_PS3_SIXAXIS_DRIVER";
    public const string SdlHintJoystickHidapiPs4 = "JOYSTICK_HIDAPI_PS4";
    public const string SdlHintJoystickHidapiPs4ReportInterval = "JOYSTICK_HIDAPI_PS4_REPORT_INTERVAL";
    public const string SdlHintJoystickHidapiPs5 = "JOYSTICK_HIDAPI_PS5";
    public const string SdlHintJoystickHidapiPs5PlayerLed = "JOYSTICK_HIDAPI_PS5_PLAYER_LED";
    public const string SdlHintJoystickHidapiShield = "JOYSTICK_HIDAPI_SHIELD";
    public const string SdlHintJoystickHidapiStadia = "JOYSTICK_HIDAPI_STADIA";
    public const string SdlHintJoystickHidapiSteam = "JOYSTICK_HIDAPI_STEAM";
    public const string SdlHintJoystickHidapiSteamHomeLed = "JOYSTICK_HIDAPI_STEAM_HOME_LED";
    public const string SdlHintJoystickHidapiSteamDeck = "JOYSTICK_HIDAPI_STEAMDECK";
    public const string SdlHintJoystickHidapiSteamHori = "JOYSTICK_HIDAPI_STEAM_HORI";
    public const string SdlHintJoystickHidapiLg4Ff = "JOYSTICK_HIDAPI_LG4FF";
    public const string SdlHintJoystickHidapi8BitDo = "JOYSTICK_HIDAPI_8BITDO";
    public const string SdlHintJoystickHidapiSwitch = "JOYSTICK_HIDAPI_SWITCH";
    public const string SdlHintJoystickHidapiSwitchHomeLed = "JOYSTICK_HIDAPI_SWITCH_HOME_LED";
    public const string SdlHintJoystickHidapiSwitchPlayerLed = "JOYSTICK_HIDAPI_SWITCH_PLAYER_LED";
    public const string SdlHintJoystickHidapiVerticalJoyCons = "JOYSTICK_HIDAPI_VERTICAL_JOY_CONS";
    public const string SdlHintJoystickHidapiWii = "JOYSTICK_HIDAPI_WII";
    public const string SdlHintJoystickHidapiWiiPlayerLed = "JOYSTICK_HIDAPI_WII_PLAYER_LED";
    public const string SdlHintJoystickHidapiXbox = "JOYSTICK_HIDAPI_XBOX";
    public const string SdlHintJoystickHidapiXbox360 = "JOYSTICK_HIDAPI_XBOX_360";
    public const string SdlHintJoystickHidapiXbox360PlayerLed = "JOYSTICK_HIDAPI_XBOX_360_PLAYER_LED";
    public const string SdlHintJoystickHidapiXbox360Wireless = "JOYSTICK_HIDAPI_XBOX_360_WIRELESS";
    public const string SdlHintJoystickHidapiXboxOne = "JOYSTICK_HIDAPI_XBOX_ONE";
    public const string SdlHintJoystickHidapiXboxOneHomeLed = "JOYSTICK_HIDAPI_XBOX_ONE_HOME_LED";
    public const string SdlHintJoystickHidapiGip = "JOYSTICK_HIDAPI_GIP";
    public const string SdlHintJoystickHidapiGipResetForMetadata = "JOYSTICK_HIDAPI_GIP_RESET_FOR_METADATA";
    public const string SdlHintJoystickIokit = "JOYSTICK_IOKIT";
    public const string SdlHintJoystickLinuxClassic = "JOYSTICK_LINUX_CLASSIC";
    public const string SdlHintJoystickLinuxDeadZones = "JOYSTICK_LINUX_DEADZONES";
    public const string SdlHintJoystickLinuxDigitalHats = "JOYSTICK_LINUX_DIGITAL_HATS";
    public const string SdlHintJoystickLinuxHatDeadZones = "JOYSTICK_LINUX_HAT_DEADZONES";
    public const string SdlHintJoystickMfi = "JOYSTICK_MFI";
    public const string SdlHintJoystickRawInput = "JOYSTICK_RAWINPUT";
    public const string SdlHintJoystickRawInputCorrelateXInput = "JOYSTICK_RAWINPUT_CORRELATE_XINPUT";
    public const string SdlHintJoystickRogChakram = "JOYSTICK_ROG_CHAKRAM";
    public const string SdlHintJoystickThread = "JOYSTICK_THREAD";
    public const string SdlHintJoystickThrottleDevices = "JOYSTICK_THROTTLE_DEVICES";
    public const string SdlHintJoystickThrottleDevicesExcluded = "JOYSTICK_THROTTLE_DEVICES_EXCLUDED";
    public const string SdlHintJoystickWgi = "JOYSTICK_WGI";
    public const string SdlHintJoystickWheelDevices = "JOYSTICK_WHEEL_DEVICES";
    public const string SdlHintJoystickWheelDevicesExcluded = "JOYSTICK_WHEEL_DEVICES_EXCLUDED";
    public const string SdlHintJoystickZeroCenteredDevices = "JOYSTICK_ZERO_CENTERED_DEVICES";
    public const string SdlHintJoystickHapticAxes = "JOYSTICK_HAPTIC_AXES";
    public const string SdlHintKeycodeOptions = "KEYCODE_OPTIONS";
    public const string SdlHintKmsDrmDeviceIndex = "KMSDRM_DEVICE_INDEX";
    public const string SdlHintKmsDrmRequireDrmMaster = "KMSDRM_REQUIRE_DRM_MASTER";
    public const string SdlHintLogging = "LOGGING";
    public const string SdlHintMacBackgroundApp = "MAC_BACKGROUND_APP";
    public const string SdlHintMacCtrlClickEmulateRightClick = "MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK";
    public const string SdlHintMacOpenglAsyncDispatch = "MAC_OPENGL_ASYNC_DISPATCH";
    public const string SdlHintMacOptionAsAlt = "MAC_OPTION_AS_ALT";
    public const string SdlHintMacScrollMomentum = "MAC_SCROLL_MOMENTUM";
    public const string SdlHintMainCallbackRate = "MAIN_CALLBACK_RATE";
    public const string SdlHintMouseAutoCapture = "MOUSE_AUTO_CAPTURE";
    public const string SdlHintMouseDoubleClickRadius = "MOUSE_DOUBLE_CLICK_RADIUS";
    public const string SdlHintMouseDoubleClickTime = "MOUSE_DOUBLE_CLICK_TIME";
    public const string SdlHintMouseDefaultSystemCursor = "MOUSE_DEFAULT_SYSTEM_CURSOR";
    public const string SdlHintMouseEmulateWarpWithRelative = "MOUSE_EMULATE_WARP_WITH_RELATIVE";
    public const string SdlHintMouseFocusClickThrough = "MOUSE_FOCUS_CLICKTHROUGH";
    public const string SdlHintMouseNormalSpeedScale = "MOUSE_NORMAL_SPEED_SCALE";
    public const string SdlHintMouseRelativeModeCenter = "MOUSE_RELATIVE_MODE_CENTER";
    public const string SdlHintMouseRelativeSpeedScale = "MOUSE_RELATIVE_SPEED_SCALE";
    public const string SdlHintMouseRelativeSystemScale = "MOUSE_RELATIVE_SYSTEM_SCALE";
    public const string SdlHintMouseRelativeWarpMotion = "MOUSE_RELATIVE_WARP_MOTION";
    public const string SdlHintMouseRelativeCursorVisible = "MOUSE_RELATIVE_CURSOR_VISIBLE";
    public const string SdlHintMouseTouchEvents = "MOUSE_TOUCH_EVENTS";
    public const string SdlHintMuteConsoleKeyboard = "MUTE_CONSOLE_KEYBOARD";
    public const string SdlHintNoSignalHandlers = "NO_SIGNAL_HANDLERS";
    public const string SdlHintOpenglLibrary = "OPENGL_LIBRARY";
    public const string SdlHintEglLibrary = "EGL_LIBRARY";
    public const string SdlHintOpenglEsDriver = "OPENGL_ES_DRIVER";
    public const string SdlHintOpenvrLibrary = "OPENVR_LIBRARY";
    public const string SdlHintOrientations = "ORIENTATIONS";
    public const string SdlHintPollSentinel = "POLL_SENTINEL";
    public const string SdlHintPreferredLocales = "PREFERRED_LOCALES";
    public const string SdlHintQuitOnLastWindowClose = "QUIT_ON_LAST_WINDOW_CLOSE";
    public const string SdlHintRenderDirect3DThreadsafe = "RENDER_DIRECT3D_THREADSAFE";
    public const string SdlHintRenderDirect3D11Debug = "RENDER_DIRECT3D11_DEBUG";
    public const string SdlHintRenderVulkanDebug = "RENDER_VULKAN_DEBUG";
    public const string SdlHintRenderGpuDebug = "RENDER_GPU_DEBUG";
    public const string SdlHintRenderGpuLowPower = "RENDER_GPU_LOW_POWER";
    public const string SdlHintRenderDriver = "RENDER_DRIVER";
    public const string SdlHintRenderLineMethod = "RENDER_LINE_METHOD";
    public const string SdlHintRenderMetalPreferLowPowerDevice = "RENDER_METAL_PREFER_LOW_POWER_DEVICE";
    public const string SdlHintRenderVsync = "RENDER_VSYNC";
    public const string SdlHintReturnKeyHidesIme = "RETURN_KEY_HIDES_IME";
    public const string SdlHintRogGamepadMice = "ROG_GAMEPAD_MICE";
    public const string SdlHintRogGamepadMiceExcluded = "ROG_GAMEPAD_MICE_EXCLUDED";
    public const string SdlHintRpiVideoLayer = "RPI_VIDEO_LAYER";
    public const string SdlHintScreensaverInhibitActivityName = "SCREENSAVER_INHIBIT_ACTIVITY_NAME";
    public const string SdlHintShutdownDbusOnQuit = "SHUTDOWN_DBUS_ON_QUIT";
    public const string SdlHintStorageTitleDriver = "STORAGE_TITLE_DRIVER";
    public const string SdlHintStorageUserDriver = "STORAGE_USER_DRIVER";
    public const string SdlHintThreadForceRealtimeTimeCritical = "THREAD_FORCE_REALTIME_TIME_CRITICAL";
    public const string SdlHintThreadPriorityPolicy = "THREAD_PRIORITY_POLICY";
    public const string SdlHintTimerResolution = "TIMER_RESOLUTION";
    public const string SdlHintTouchMouseEvents = "TOUCH_MOUSE_EVENTS";
    public const string SdlHintTrackpadIsTouchOnly = "TRACKPAD_IS_TOUCH_ONLY";
    public const string SdlHintTvRemoteAsJoystick = "TV_REMOTE_AS_JOYSTICK";
    public const string SdlHintVideoAllowScreensaver = "VIDEO_ALLOW_SCREENSAVER";
    public const string SdlHintVideoDisplayPriority = "VIDEO_DISPLAY_PRIORITY";
    public const string SdlHintVideoDoubleBuffer = "VIDEO_DOUBLE_BUFFER";
    public const string SdlHintVideoDriver = "VIDEO_DRIVER";
    public const string SdlHintVideoDummySaveFrames = "VIDEO_DUMMY_SAVE_FRAMES";
    public const string SdlHintVideoEglAllowGetDisplayFallback = "VIDEO_EGL_ALLOW_GETDISPLAY_FALLBACK";
    public const string SdlHintVideoForceEgl = "VIDEO_FORCE_EGL";
    public const string SdlHintVideoMacFullscreenSpaces = "VIDEO_MAC_FULLSCREEN_SPACES";
    public const string SdlHintVideoMacFullscreenMenuVisibility = "VIDEO_MAC_FULLSCREEN_MENU_VISIBILITY";
    public const string SdlHintVideoMatchExclusiveModeOnMove = "VIDEO_MATCH_EXCLUSIVE_MODE_ON_MOVE";
    public const string SdlHintVideoMinimizeOnFocusLoss = "VIDEO_MINIMIZE_ON_FOCUS_LOSS";
    public const string SdlHintVideoOffscreenSaveFrames = "VIDEO_OFFSCREEN_SAVE_FRAMES";
    public const string SdlHintVideoSyncWindowOperations = "VIDEO_SYNC_WINDOW_OPERATIONS";
    public const string SdlHintVideoWaylandAllowLibDecor = "VIDEO_WAYLAND_ALLOW_LIBDECOR";
    public const string SdlHintVideoWaylandModeEmulation = "VIDEO_WAYLAND_MODE_EMULATION";
    public const string SdlHintVideoWaylandModeScaling = "VIDEO_WAYLAND_MODE_SCALING";
    public const string SdlHintVideoWaylandPreferLibDecor = "VIDEO_WAYLAND_PREFER_LIBDECOR";
    public const string SdlHintVideoWaylandScaleToDisplay = "VIDEO_WAYLAND_SCALE_TO_DISPLAY";
    public const string SdlHintVideoWinD3DCompiler = "VIDEO_WIN_D3DCOMPILER";
    public const string SdlHintVideoX11ExternalWindowInput = "VIDEO_X11_EXTERNAL_WINDOW_INPUT";
    public const string SdlHintVideoX11NetWmBypassCompositor = "VIDEO_X11_NET_WM_BYPASS_COMPOSITOR";
    public const string SdlHintVideoX11NetWmPing = "VIDEO_X11_NET_WM_PING";
    public const string SdlHintVideoX11NoDirectColor = "VIDEO_X11_NODIRECTCOLOR";
    public const string SdlHintVideoX11ScalingFactor = "VIDEO_X11_SCALING_FACTOR";
    public const string SdlHintVideoX11VisualId = "VIDEO_X11_VISUALID";
    public const string SdlHintVideoX11WindowVisualId = "VIDEO_X11_WINDOW_VISUALID";
    public const string SdlHintVideoX11Xrandr = "VIDEO_X11_XRANDR";
    public const string SdlHintVitaEnableBackTouch = "VITA_ENABLE_BACK_TOUCH";
    public const string SdlHintVitaEnableFrontTouch = "VITA_ENABLE_FRONT_TOUCH";
    public const string SdlHintVitaModulePath = "VITA_MODULE_PATH";
    public const string SdlHintVitaPvrInit = "VITA_PVR_INIT";
    public const string SdlHintVitaResolution = "VITA_RESOLUTION";
    public const string SdlHintVitaPvrOpengl = "VITA_PVR_OPENGL";
    public const string SdlHintVitaTouchMouseDevice = "VITA_TOUCH_MOUSE_DEVICE";
    public const string SdlHintVulkanDisplay = "VULKAN_DISPLAY";
    public const string SdlHintVulkanLibrary = "VULKAN_LIBRARY";
    public const string SdlHintWaveFactChunk = "WAVE_FACT_CHUNK";
    public const string SdlHintWaveChunkLimit = "WAVE_CHUNK_LIMIT";
    public const string SdlHintWaveRiffChunkSize = "WAVE_RIFF_CHUNK_SIZE";
    public const string SdlHintWaveTruncation = "WAVE_TRUNCATION";
    public const string SdlHintWindowActivateWhenRaised = "WINDOW_ACTIVATE_WHEN_RAISED";
    public const string SdlHintWindowActivateWhenShown = "WINDOW_ACTIVATE_WHEN_SHOWN";
    public const string SdlHintWindowAllowTopmost = "WINDOW_ALLOW_TOPMOST";
    public const string SdlHintWindowFrameUsableWhileCursorHidden = "WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN";
    public const string SdlHintWindowsCloseOnAltF4 = "WINDOWS_CLOSE_ON_ALT_F4";
    public const string SdlHintWindowsEnableMenuMnemonics = "WINDOWS_ENABLE_MENU_MNEMONICS";
    public const string SdlHintWindowsEnableMessageLoop = "WINDOWS_ENABLE_MESSAGELOOP";
    public const string SdlHintWindowsGameInput = "WINDOWS_GAMEINPUT";
    public const string SdlHintWindowsRawKeyboard = "WINDOWS_RAW_KEYBOARD";
    public const string SdlHintWindowsForceSemaphoreKernel = "WINDOWS_FORCE_SEMAPHORE_KERNEL";
    public const string SdlHintWindowsIntResourceIcon = "WINDOWS_INTRESOURCE_ICON";
    public const string SdlHintWindowsIntResourceIconSmall = "WINDOWS_INTRESOURCE_ICON_SMALL";
    public const string SdlHintWindowsUseD3D9Ex = "WINDOWS_USE_D3D9EX";
    public const string SdlHintWindowsEraseBackgroundMode = "WINDOWS_ERASE_BACKGROUND_MODE";
    public const string SdlHintX11ForceOverrideRedirect = "X11_FORCE_OVERRIDE_REDIRECT";
    public const string SdlHintX11WindowType = "X11_WINDOW_TYPE";
    public const string SdlHintX11XcbLibrary = "X11_XCB_LIBRARY";
    public const string SdlHintXInputEnabled = "XINPUT_ENABLED";
    public const string SdlHintAssert = "ASSERT";
    public const string SdlHintPenMouseEvents = "PEN_MOUSE_EVENTS";
    public const string SdlHintPenTouchEvents = "PEN_TOUCH_EVENTS";

    // /usr/local/include/SDL3/SDL_init.h

    public const string SdlPropAppMetadataNameString = "SDL.app.metadata.name";
    public const string SdlPropAppMetadataVersionString = "SDL.app.metadata.version";
    public const string SdlPropAppMetadataIdentifierString = "SDL.app.metadata.identifier";
    public const string SdlPropAppMetadataCreatorString = "SDL.app.metadata.creator";
    public const string SdlPropAppMetadataCopyrightString = "SDL.app.metadata.copyright";
    public const string SdlPropAppMetadataUrlString = "SDL.app.metadata.url";
    public const string SdlPropAppMetadataTypeString = "SDL.app.metadata.type";

    // /usr/local/include/SDL3/SDL_process.h

    public const string SdlPropProcessCreateArgsPointer = "SDL.process.create.args";
    public const string SdlPropProcessCreateEnvironmentPointer = "SDL.process.create.environment";
    public const string SdlPropProcessCreateWorkingDirectoryString = "SDL.process.create.working_directory";
    public const string SdlPropProcessCreateStdinNumber = "SDL.process.create.stdin_option";
    public const string SdlPropProcessCreateStdinPointer = "SDL.process.create.stdin_source";
    public const string SdlPropProcessCreateStdoutNumber = "SDL.process.create.stdout_option";
    public const string SdlPropProcessCreateStdoutPointer = "SDL.process.create.stdout_source";
    public const string SdlPropProcessCreateStderrNumber = "SDL.process.create.stderr_option";
    public const string SdlPropProcessCreateStderrPointer = "SDL.process.create.stderr_source";
    public const string SdlPropProcessCreateStderrToStdoutBoolean = "SDL.process.create.stderr_to_stdout";
    public const string SdlPropProcessCreateBackgroundBoolean = "SDL.process.create.background";
    public const string SdlPropProcessPidNumber = "SDL.process.pid";
    public const string SdlPropProcessStdinPointer = "SDL.process.stdin";
    public const string SdlPropProcessStdoutPointer = "SDL.process.stdout";
    public const string SdlPropProcessStderrPointer = "SDL.process.stderr";
    public const string SdlPropProcessBackgroundBoolean = "SDL.process.background";

    // /usr/local/include/SDL3/SDL_render.h

    public const string SdlPropRendererCreateNameString = "SDL.renderer.create.name";
    public const string SdlPropRendererCreateWindowPointer = "SDL.renderer.create.window";
    public const string SdlPropRendererCreateSurfacePointer = "SDL.renderer.create.surface";
    public const string SdlPropRendererCreateOutputColorspaceNumber = "SDL.renderer.create.output_colorspace";
    public const string SdlPropRendererCreatePresentVsyncNumber = "SDL.renderer.create.present_vsync";
    public const string SdlPropRendererCreateGpuShadersSpirvBoolean = "SDL.renderer.create.gpu.shaders_spirv";
    public const string SdlPropRendererCreateGpuShadersDxilBoolean = "SDL.renderer.create.gpu.shaders_dxil";
    public const string SdlPropRendererCreateGpuShadersMslBoolean = "SDL.renderer.create.gpu.shaders_msl";
    public const string SdlPropRendererCreateVulkanInstancePointer = "SDL.renderer.create.vulkan.instance";
    public const string SdlPropRendererCreateVulkanSurfaceNumber = "SDL.renderer.create.vulkan.surface";
    public const string SdlPropRendererCreateVulkanPhysicalDevicePointer = "SDL.renderer.create.vulkan.physical_device";
    public const string SdlPropRendererCreateVulkanDevicePointer = "SDL.renderer.create.vulkan.device";
    public const string SdlPropRendererCreateVulkanGraphicsQueueFamilyIndexNumber = "SDL.renderer.create.vulkan.graphics_queue_family_index";
    public const string SdlPropRendererCreateVulkanPresentQueueFamilyIndexNumber = "SDL.renderer.create.vulkan.present_queue_family_index";
    public const string SdlPropRendererNameString = "SDL.renderer.name";
    public const string SdlPropRendererWindowPointer = "SDL.renderer.window";
    public const string SdlPropRendererSurfacePointer = "SDL.renderer.surface";
    public const string SdlPropRendererVsyncNumber = "SDL.renderer.vsync";
    public const string SdlPropRendererMaxTextureSizeNumber = "SDL.renderer.max_texture_size";
    public const string SdlPropRendererTextureFormatsPointer = "SDL.renderer.texture_formats";
    public const string SdlPropRendererOutputColorspaceNumber = "SDL.renderer.output_colorspace";
    public const string SdlPropRendererHdrEnabledBoolean = "SDL.renderer.HDR_enabled";
    public const string SdlPropRendererSdrWhitePointFloat = "SDL.renderer.SDR_white_point";
    public const string SdlPropRendererHdrHeadroomFloat = "SDL.renderer.HDR_headroom";
    public const string SdlPropRendererD3D9DevicePointer = "SDL.renderer.d3d9.device";
    public const string SdlPropRendererD3D11DevicePointer = "SDL.renderer.d3d11.device";
    public const string SdlPropRendererD3D11SwapchainPointer = "SDL.renderer.d3d11.swap_chain";
    public const string SdlPropRendererD3D12DevicePointer = "SDL.renderer.d3d12.device";
    public const string SdlPropRendererD3D12SwapchainPointer = "SDL.renderer.d3d12.swap_chain";
    public const string SdlPropRendererD3D12CommandQueuePointer = "SDL.renderer.d3d12.command_queue";
    public const string SdlPropRendererVulkanInstancePointer = "SDL.renderer.vulkan.instance";
    public const string SdlPropRendererVulkanSurfaceNumber = "SDL.renderer.vulkan.surface";
    public const string SdlPropRendererVulkanPhysicalDevicePointer = "SDL.renderer.vulkan.physical_device";
    public const string SdlPropRendererVulkanDevicePointer = "SDL.renderer.vulkan.device";
    public const string SdlPropRendererVulkanGraphicsQueueFamilyIndexNumber = "SDL.renderer.vulkan.graphics_queue_family_index";
    public const string SdlPropRendererVulkanPresentQueueFamilyIndexNumber = "SDL.renderer.vulkan.present_queue_family_index";
    public const string SdlPropRendererVulkanSwapchainImageCountNumber = "SDL.renderer.vulkan.swapchain_image_count";
    public const string SdlPropRendererGpuDevicePointer = "SDL.renderer.gpu.device";
    public const string SdlPropTextureCreateColorspaceNumber = "SDL.texture.create.colorspace";
    public const string SdlPropTextureCreateFormatNumber = "SDL.texture.create.format";
    public const string SdlPropTextureCreateAccessNumber = "SDL.texture.create.access";
    public const string SdlPropTextureCreateWidthNumber = "SDL.texture.create.width";
    public const string SdlPropTextureCreateHeightNumber = "SDL.texture.create.height";
    public const string SdlPropTextureCreateSdrWhitePointFloat = "SDL.texture.create.SDR_white_point";
    public const string SdlPropTextureCreateHdrHeadroomFloat = "SDL.texture.create.HDR_headroom";
    public const string SdlPropTextureCreateD3D11TexturePointer = "SDL.texture.create.d3d11.texture";
    public const string SdlPropTextureCreateD3D11TextureUPointer = "SDL.texture.create.d3d11.texture_u";
    public const string SdlPropTextureCreateD3D11TextureVPointer = "SDL.texture.create.d3d11.texture_v";
    public const string SdlPropTextureCreateD3D12TexturePointer = "SDL.texture.create.d3d12.texture";
    public const string SdlPropTextureCreateD3D12TextureUPointer = "SDL.texture.create.d3d12.texture_u";
    public const string SdlPropTextureCreateD3D12TextureVPointer = "SDL.texture.create.d3d12.texture_v";
    public const string SdlPropTextureCreateMetalPixelBufferPointer = "SDL.texture.create.metal.pixelbuffer";
    public const string SdlPropTextureCreateOpenglTextureNumber = "SDL.texture.create.opengl.texture";
    public const string SdlPropTextureCreateOpenglTextureUvNumber = "SDL.texture.create.opengl.texture_uv";
    public const string SdlPropTextureCreateOpenglTextureUNumber = "SDL.texture.create.opengl.texture_u";
    public const string SdlPropTextureCreateOpenglTextureVNumber = "SDL.texture.create.opengl.texture_v";
    public const string SdlPropTextureCreateOpenGles2TextureNumber = "SDL.texture.create.opengles2.texture";
    public const string SdlPropTextureCreateOpenGles2TextureUvNumber = "SDL.texture.create.opengles2.texture_uv";
    public const string SdlPropTextureCreateOpenGles2TextureUNumber = "SDL.texture.create.opengles2.texture_u";
    public const string SdlPropTextureCreateOpenGles2TextureVNumber = "SDL.texture.create.opengles2.texture_v";
    public const string SdlPropTextureCreateVulkanTextureNumber = "SDL.texture.create.vulkan.texture";
    public const string SdlPropTextureColorspaceNumber = "SDL.texture.colorspace";
    public const string SdlPropTextureFormatNumber = "SDL.texture.format";
    public const string SdlPropTextureAccessNumber = "SDL.texture.access";
    public const string SdlPropTextureWidthNumber = "SDL.texture.width";
    public const string SdlPropTextureHeightNumber = "SDL.texture.height";
    public const string SdlPropTextureSdrWhitePointFloat = "SDL.texture.SDR_white_point";
    public const string SdlPropTextureHdrHeadroomFloat = "SDL.texture.HDR_headroom";
    public const string SdlPropTextureD3D11TexturePointer = "SDL.texture.d3d11.texture";
    public const string SdlPropTextureD3D11TextureUPointer = "SDL.texture.d3d11.texture_u";
    public const string SdlPropTextureD3D11TextureVPointer = "SDL.texture.d3d11.texture_v";
    public const string SdlPropTextureD3D12TexturePointer = "SDL.texture.d3d12.texture";
    public const string SdlPropTextureD3D12TextureUPointer = "SDL.texture.d3d12.texture_u";
    public const string SdlPropTextureD3D12TextureVPointer = "SDL.texture.d3d12.texture_v";
    public const string SdlPropTextureOpenglTextureNumber = "SDL.texture.opengl.texture";
    public const string SdlPropTextureOpenglTextureUvNumber = "SDL.texture.opengl.texture_uv";
    public const string SdlPropTextureOpenglTextureUNumber = "SDL.texture.opengl.texture_u";
    public const string SdlPropTextureOpenglTextureVNumber = "SDL.texture.opengl.texture_v";
    public const string SdlPropTextureOpenglTextureTargetNumber = "SDL.texture.opengl.target";
    public const string SdlPropTextureOpenglTexWFloat = "SDL.texture.opengl.tex_w";
    public const string SdlPropTextureOpenglTexHFloat = "SDL.texture.opengl.tex_h";
    public const string SdlPropTextureOpenGles2TextureNumber = "SDL.texture.opengles2.texture";
    public const string SdlPropTextureOpenGles2TextureUvNumber = "SDL.texture.opengles2.texture_uv";
    public const string SdlPropTextureOpenGles2TextureUNumber = "SDL.texture.opengles2.texture_u";
    public const string SdlPropTextureOpenGles2TextureVNumber = "SDL.texture.opengles2.texture_v";
    public const string SdlPropTextureOpenGles2TextureTargetNumber = "SDL.texture.opengles2.target";
    public const string SdlPropTextureVulkanTextureNumber = "SDL.texture.vulkan.texture";
}