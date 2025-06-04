namespace SharpSDL3.Enums;

public enum EventType : uint {
    First = 0,     /**< Unused (do not remove) */

    /* Application events */
    Quit = 0x100, /**< User-requested quit */

    /* These application events have special meaning on iOS and Android, see README-ios.md and README-android.md for details */
    Terminating,      /**< The application is being terminated by the OS. This event must be handled in a callback set with SDL_AddEventWatch().
                                     Called on iOS in applicationWillTerminate()
                                     Called on Android in onDestroy()
                                */
    LowMemory,       /**< The application is low on memory, free memory if possible. This event must be handled in a callback set with SDL_AddEventWatch().
                                     Called on iOS in applicationDidReceiveMemoryWarning()
                                     Called on Android in onTrimMemory()
                                */
    WillEnterBackground, /**< The application is about to enter the background. This event must be handled in a callback set with SDL_AddEventWatch().
                                     Called on iOS in applicationWillResignActive()
                                     Called on Android in onPause()
                                */
    DidEnterBackground, /**< The application did enter the background and may not get CPU for some time. This event must be handled in a callback set with SDL_AddEventWatch().
                                     Called on iOS in applicationDidEnterBackground()
                                     Called on Android in onPause()
                                */
    WillEnterForeground, /**< The application is about to enter the foreground. This event must be handled in a callback set with SDL_AddEventWatch().
                                     Called on iOS in applicationWillEnterForeground()
                                     Called on Android in onResume()
                                */
    DidEnterForeground, /**< The application is now interactive. This event must be handled in a callback set with SDL_AddEventWatch().
                                     Called on iOS in applicationDidBecomeActive()
                                     Called on Android in onResume()
                                */

    LocaleChanged,  /**< The user's locale preferences have changed. */

    SystemThemeChanged, /**< The system theme changed */

    /* Display events */
    /* 0x150 was SDL_DISPLAYEVENT, reserve the number for sdl2-compat */
    DisplayOrientation = 0x151,   /**< Display orientation has changed to data1 */
    DisplayAdded,                 /**< Display has been added to the system */
    DisplayRemoved,               /**< Display has been removed from the system */
    DisplayMoved,                 /**< Display has changed position */
    DisplayDesktopModeChanged,  /**< Display has changed desktop mode */
    DisplayCurrentModeChanged,  /**< Display has changed current mode */
    DisplayContentScaleChanged, /**< Display has changed content scale */
    DisplayFirst = DisplayOrientation,
    DisplayLast = DisplayContentScaleChanged,

    /* Window events */
    /* 0x200 was SDL_WINDOWEVENT, reserve the number for sdl2-compat */
    /* 0x201 was SDL_SYSWMEVENT, reserve the number for sdl2-compat */
    WindowShown = 0x202,     /**< Window has been shown */
    WindowHidden,            /**< Window has been hidden */
    WindowExposed,           /**< Window has been exposed and should be redrawn, and can be redrawn directly from event watchers for this event */
    WindowMoved,             /**< Window has been moved to data1, data2 */
    WindowResized,           /**< Window has been resized to data1xdata2 */
    WindowPixelSizeChanged,/**< The pixel size of the window has changed to data1xdata2 */
    WindowMetalViewResized,/**< The pixel size of a Metal view associated with the window has changed */
    WindowMinimized,         /**< Window has been minimized */
    WindowMaximized,         /**< Window has been maximized */
    WindowRestored,          /**< Window has been restored to normal size and position */
    WindowMouseEnter,       /**< Window has gained mouse focus */
    WindowMouseLeave,       /**< Window has lost mouse focus */
    WindowFocusGained,      /**< Window has gained keyboard focus */
    WindowFocusLost,        /**< Window has lost keyboard focus */
    WindowCloseRequested,   /**< The window manager requests that the window be closed */
    WindowHitTest,          /**< Window had a hit test that wasn't SDL_HITTEST_NORMAL */
    WindowIccprofChanged,   /**< The ICC profile of the window's display has changed */
    WindowDisplayChanged,   /**< Window has been moved to display data1 */
    WindowDisplayScaleChanged, /**< Window display scale has been changed */
    WindowSafeAreaChanged, /**< The window safe area has been changed */
    WindowOccluded,          /**< The window has been occluded */
    WindowEnterFullscreen,  /**< The window has entered fullscreen mode */
    WindowLeaveFullscreen,  /**< The window has left fullscreen mode */
    WindowDestroyed,         /**< The window with the associated ID is being or has been destroyed. If this message is being handled
                                             in an event watcher, the window handle is still valid and can still be used to retrieve any properties
                                             associated with the window. Otherwise, the handle has already been destroyed and all resources
                                             associated with it are invalid */
    WindowHdrStateChanged, /**< Window HDR properties have changed */
    WindowFirst = WindowShown,
    WindowLast = WindowHdrStateChanged,

    /* Keyboard events */
    KeyDown = 0x300, /**< Key pressed */
    KeyUp,                  /**< Key released */
    TextEditing,            /**< Keyboard text editing (composition) */
    TextInput,              /**< Keyboard text input */
    KeymapChanged,          /**< Keymap changed due to a system event such as an
                                            input language or keyboard layout change. */
    KeyboardAdded,          /**< A new keyboard has been inserted into the system */
    KeyboardRemoved,        /**< A keyboard has been removed */
    TextEditingCandidates, /**< Keyboard text editing candidates */

    /* Mouse events */
    MouseMotion = 0x400, /**< Mouse moved */
    MouseButtonDown,       /**< Mouse button pressed */
    MouseButtonUp,         /**< Mouse button released */
    MouseWheel,             /**< Mouse wheel motion */
    MouseAdded,             /**< A new mouse has been inserted into the system */
    MouseRemoved,           /**< A mouse has been removed */

    /* Joystick events */
    JoystickAxisMotion = 0x600, /**< Joystick axis motion */
    JoystickBallMotion,          /**< Joystick trackball motion */
    JoystickHatMotion,           /**< Joystick hat position change */
    JoystickButtonDown,          /**< Joystick button pressed */
    JoystickButtonUp,            /**< Joystick button released */
    JoystickAdded,                /**< A new joystick has been inserted into the system */
    JoystickRemoved,              /**< An opened joystick has been removed */
    JoystickBatteryUpdated,      /**< Joystick battery level change */
    JoystickUpdateComplete,      /**< Joystick update is complete */

    /* Gamepad events */
    GamepadAxisMotion = 0x650, /**< Gamepad axis motion */
    GamepadButtonDown,          /**< Gamepad button pressed */
    GamepadButtonUp,            /**< Gamepad button released */
    GamepadAdded,                /**< A new gamepad has been inserted into the system */
    GamepadRemoved,              /**< A gamepad has been removed */
    GamepadRemapped,             /**< The gamepad mapping was updated */
    GamepadTouchpadDown,        /**< Gamepad touchpad was touched */
    GamepadTouchpadMotion,      /**< Gamepad touchpad finger was moved */
    GamepadTouchpadUp,          /**< Gamepad touchpad finger was lifted */
    GamepadSensorUpdate,        /**< Gamepad sensor was updated */
    GamepadUpdateComplete,      /**< Gamepad update is complete */
    GamepadSteamHandleUpdated,  /**< Gamepad Steam handle has changed */

    /* Touch events */
    FingerDown = 0x700,
    FingerUp,
    FingerMotion,
    FingerCanceled,

    /* 0x800, 0x801, and 0x802 were the Gesture events from SDL2. Do not reuse these values! sdl2-compat needs them! */

    /* Clipboard events */
    ClipboardUpdate = 0x900, /**< The clipboard or primary selection changed */

    /* Drag and drop events */
    DropFile = 0x1000, /**< The system requests a file open */
    DropText,                 /**< text/plain drag-and-drop event */
<<<<<<< HEAD
    DropBegin,                /**< A new set of drops is beginning (<see langword="null" /> filename) */
    DropComplete,             /**< Current set of drops is now complete (<see langword="null" /> filename) */
=======
    DropBegin,                /**< A new set of drops is beginning (NULL filename) */
    DropComplete,             /**< Current set of drops is now complete (NULL filename) */
>>>>>>> main
    DropPosition,             /**< Position while moving over the window */

    /* Audio hotplug events */
    AudioDeviceAdded = 0x1100,  /**< A new audio device is available */
    AudioDeviceRemoved,         /**< An audio device has been removed. */
    AudioDeviceFormatChanged,  /**< An audio device's format has been changed by the system. */

    /* Sensor events */
    SensorUpdate = 0x1200,     /**< A sensor was updated */

    /* Pressure-sensitive pen events */
    PenProximityIn = 0x1300,  /**< Pressure-sensitive pen has become available */
    PenProximityOut,          /**< Pressure-sensitive pen has become unavailable */
    PenDown,                   /**< Pressure-sensitive pen touched drawing surface */
    PenUp,                     /**< Pressure-sensitive pen stopped touching drawing surface */
    PenButtonDown,            /**< Pressure-sensitive pen button pressed */
    PenButtonUp,              /**< Pressure-sensitive pen button released */
    PenMotion,                 /**< Pressure-sensitive pen is moving on the tablet */
    PenAxis,                   /**< Pressure-sensitive pen angle/pressure/etc changed */

    /* Camera hotplug events */
    CameraDeviceAdded = 0x1400,  /**< A new camera device is available */
    CameraDeviceRemoved,         /**< A camera device has been removed. */
    CameraDeviceApproved,        /**< A camera device has been approved for use by the user. */
    CameraDeviceDenied,          /**< A camera device has been denied for use by the user. */

    /* Render events */
    RenderTargetsReset = 0x2000, /**< The render targets have been reset and their contents need to be updated */
    RenderDeviceReset, /**< The device has been reset and all textures need to be recreated */
    RenderDeviceLost, /**< The device has been lost and can't be recovered. */

    /* Reserved events for private platforms */
    Private0 = 0x4000,
    Private1,
    Private2,
    Private3,

    /* Internal events */
    PollSentinel = 0x7F00, /**< Signals the end of an event poll cycle */

    /** Events USER through LAST are for your use,
     *  and should be allocated with SDL_RegisterEvents()
     */
    User = 0x8000,

    /**
     *  This last event is only for bounding internal arrays
     */
    Last = 0xFFFF,

    /* This just makes sure the enum is the size of Uint32 */
    EnumPadding = 0x7FFFFFFF
}