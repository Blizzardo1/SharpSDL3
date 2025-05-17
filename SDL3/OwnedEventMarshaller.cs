using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SharpSDL3;

[CustomMarshaller(typeof(Event), MarshalMode.ManagedToUnmanagedOut, typeof(OwnedEventMarshaller))]
[CustomMarshaller(typeof(Event), MarshalMode.ManagedToUnmanagedIn, typeof(OwnedEventMarshaller))]
[CustomMarshaller(typeof(Event), MarshalMode.ManagedToUnmanagedRef, typeof(OwnedEventMarshaller))]
[CustomMarshaller(typeof(Event[]), MarshalMode.ManagedToUnmanagedIn, typeof(OwnedEventMarshaller))]
public static unsafe class OwnedEventMarshaller {

    public static Event ConvertToManaged(nint unmanaged) {
        Event managed = new();
        
        if (unmanaged == nint.Zero) {
            return managed;
        }


        uint type = (uint)unmanaged.ToInt32();
        EventType eventType = (EventType)type;

        managed.Type = eventType;
        //switch (eventType) {
        //    case EventType.Quit:
        //        managed.Quit = Marshal.PtrToStructure<QuitEvent>(unmanaged + sizeof(QuitEvent));
        //        break;
        //    case EventType.First:
        //    case EventType.Terminating:
        //    case EventType.LowMemory:
        //    case EventType.JoystickUpdateComplete:
        //    case EventType.KeymapChanged:
        //    case EventType.SystemThemeChanged:
        //    case EventType.LocaleChanged:
        //    case EventType.Last:
        //    case EventType.GamepadRemapped:
        //    case EventType.GamepadUpdateComplete:
        //    case EventType.GamepadSteamHandleUpdated:
        //    case EventType.WillEnterBackground:
        //    case EventType.DidEnterBackground:
        //    case EventType.WillEnterForeground:
        //    case EventType.DidEnterForeground:
        //    case EventType.PollSentinel:
        //        managed.Common = Marshal.PtrToStructure<CommonEvent>(unmanaged + sizeof(CommonEvent));
        //        break;
        //    case EventType.DisplayOrientation:
        //    case EventType.DisplayAdded:
        //    case EventType.DisplayRemoved:
        //    case EventType.DisplayMoved:
        //    case EventType.DisplayDesktopModeChanged:
        //    case EventType.DisplayCurrentModeChanged:
        //    case EventType.DisplayContentScaleChanged:
        //        managed.Display = Marshal.PtrToStructure<DisplayEvent>(unmanaged + sizeof(DisplayEvent));
        //        break;
        //    case EventType.WindowShown:
        //    case EventType.WindowHidden:
        //    case EventType.WindowExposed:
        //    case EventType.WindowMoved:
        //    case EventType.WindowResized:
        //    case EventType.WindowPixelSizeChanged:
        //    case EventType.WindowMetalViewResized:
        //    case EventType.WindowMinimized:
        //    case EventType.WindowMaximized:
        //    case EventType.WindowRestored:
        //    case EventType.WindowMouseEnter:
        //    case EventType.WindowMouseLeave:
        //    case EventType.WindowFocusGained:
        //    case EventType.WindowFocusLost:
        //    case EventType.WindowCloseRequested:
        //    case EventType.WindowHitTest:
        //    case EventType.WindowIccProfChanged:
        //    case EventType.WindowDisplayChanged:
        //    case EventType.WindowDisplayScaleChanged:
        //    case EventType.WindowSafeAreaChanged:
        //    case EventType.WindowOccluded:
        //    case EventType.WindowEnterFullscreen:
        //    case EventType.WindowLeaveFullscreen:
        //    case EventType.WindowDestroyed:
        //    case EventType.WindowHdrStateChanged:
        //        managed.Window = Marshal.PtrToStructure<WindowEvent>(unmanaged + sizeof(WindowEvent));
        //        break;
        //    case EventType.KeyDown:
        //    case EventType.KeyUp:
        //        managed.Key = Marshal.PtrToStructure<KeyboardEvent>(unmanaged + sizeof(KeyboardEvent));
        //        break;
        //    case EventType.TextEditing:
        //        managed.Edit = Marshal.PtrToStructure<TextEditingEvent>(unmanaged + sizeof(TextEditingEvent));
        //        break;
        //    case EventType.TextInput:
        //        managed.Text = Marshal.PtrToStructure<TextInputEvent>(unmanaged + sizeof(TextInputEvent));
        //        break;
        //    case EventType.KeyboardAdded:
        //    case EventType.KeyboardRemoved:
        //        managed.KDevice = Marshal.PtrToStructure<KeyboardDeviceEvent>(unmanaged + sizeof(KeyboardDeviceEvent));
        //        break;
        //    case EventType.TextEditingCandidates:
        //        managed.EditCandidates = Marshal.PtrToStructure<TextEditingCandidatesEvent>(unmanaged + sizeof(TextEditingCandidatesEvent));
        //        break;
        //    case EventType.MouseMotion:
        //        managed.Motion = Marshal.PtrToStructure<MouseMotionEvent>(unmanaged + sizeof(MouseMotionEvent));
        //        break;
        //    case EventType.MouseButtonDown:
        //    case EventType.MouseButtonUp:
        //        managed.Button = Marshal.PtrToStructure<MouseButtonEvent>(unmanaged + sizeof(MouseButtonEvent));
        //        break;
        //    case EventType.MouseWheel:
        //        managed.Wheel = Marshal.PtrToStructure<MouseWheelEvent>(unmanaged + sizeof(MouseWheelEvent));
        //        break;
        //    case EventType.MouseAdded:
        //    case EventType.MouseRemoved:
        //        managed.MDevice = Marshal.PtrToStructure<MouseDeviceEvent>(unmanaged + sizeof(MouseDeviceEvent));
        //        break;
        //    case EventType.JoystickAxisMotion:
        //        managed.JAxis = Marshal.PtrToStructure<JoyAxisEvent>(unmanaged + sizeof(JoyAxisEvent));
        //        break;
        //    case EventType.JoystickBallMotion:
        //        managed.JBall = Marshal.PtrToStructure<JoyBallEvent>(unmanaged + sizeof(JoyBallEvent));
        //        break;
        //    case EventType.JoystickHatMotion:
        //        managed.JHat = Marshal.PtrToStructure<JoyHatEvent>(unmanaged + sizeof(JoyHatEvent));
        //        break;
        //    case EventType.JoystickButtonDown:
        //    case EventType.JoystickButtonUp:
        //        managed.JButton = Marshal.PtrToStructure<JoyButtonEvent>(unmanaged + sizeof(JoyButtonEvent));
        //        break;
        //    case EventType.JoystickAdded:
        //    case EventType.JoystickRemoved:
        //        managed.JDevice = Marshal.PtrToStructure<JoyDeviceEvent>(unmanaged + sizeof(JoyDeviceEvent));
        //        break;
        //    case EventType.JoystickBatteryUpdated:
        //        managed.JBattery = Marshal.PtrToStructure<JoyBatteryEvent>(unmanaged + sizeof(JoyBatteryEvent));
        //        break;
        //    case EventType.GamepadAxisMotion:
        //        managed.GAxis = Marshal.PtrToStructure<GamepadAxisEvent>(unmanaged + sizeof(GamepadAxisEvent));
        //        break;
        //    case EventType.GamepadButtonDown:
        //    case EventType.GamepadButtonUp:
        //        managed.GButton = Marshal.PtrToStructure<GamepadButtonEvent>(unmanaged + sizeof(GamepadButtonEvent));
        //        break;
        //    case EventType.GamepadAdded:
        //    case EventType.GamepadRemoved:
        //        managed.GDevice = Marshal.PtrToStructure<GamepadDeviceEvent>(unmanaged + sizeof(GamepadDeviceEvent));
        //        break;
        //    case EventType.GamepadTouchpadDown:
        //    case EventType.GamepadTouchpadMotion:
        //    case EventType.GamepadTouchpadUp:
        //        managed.GTouchpad = Marshal.PtrToStructure<GamepadTouchpadEvent>(unmanaged + sizeof(GamepadTouchpadEvent));
        //        break;
        //    case EventType.GamepadSensorUpdate:
        //        managed.GSensor = Marshal.PtrToStructure<GamepadSensorEvent>(unmanaged + sizeof(GamepadSensorEvent));
        //        break;
        //    case EventType.FingerDown:
        //    case EventType.FingerUp:
        //    case EventType.FingerMotion:
        //    case EventType.FingerCanceled:
        //        managed.TFinger = Marshal.PtrToStructure<TouchFingerEvent>(unmanaged + sizeof(TouchFingerEvent));
        //        break;
        //    case EventType.ClipboardUpdate:
        //        managed.Clipboard = Marshal.PtrToStructure<ClipboardEvent>(unmanaged + sizeof(ClipboardEvent));
        //        break;
        //    case EventType.DropFile:
        //    case EventType.DropText:
        //    case EventType.DropBegin:
        //    case EventType.DropComplete:
        //    case EventType.DropPosition:
        //        managed.Drop = Marshal.PtrToStructure<DropEvent>(unmanaged + sizeof(DropEvent));
        //        break;
        //    case EventType.AudioDeviceAdded:
        //    case EventType.AudioDeviceRemoved:
        //    case EventType.AudioDeviceFormatChanged:
        //        managed.ADevice = Marshal.PtrToStructure<AudioDeviceEvent>(unmanaged + sizeof(AudioDeviceEvent));
        //        break;
        //    case EventType.SensorUpdate:
        //        managed.Sensor = Marshal.PtrToStructure<SensorEvent>(unmanaged + sizeof(SensorEvent));
        //        break;
        //    case EventType.PenProximityIn:
        //    case EventType.PenProximityOut:
        //        managed.PProximity = Marshal.PtrToStructure<PenProximityEvent>(unmanaged + sizeof(PenProximityEvent));
        //        break;
        //    case EventType.PenDown:
        //    case EventType.PenUp:
        //        managed.PTouch = Marshal.PtrToStructure<PenTouchEvent>(unmanaged + sizeof(PenTouchEvent));
        //        break;
        //    case EventType.PenButtonDown:
        //    case EventType.PenButtonUp:
        //        managed.PButton = Marshal.PtrToStructure<PenButtonEvent>(unmanaged + sizeof(PenButtonEvent));
        //        break;
        //    case EventType.PenMotion:
        //        managed.PMotion = Marshal.PtrToStructure<PenMotionEvent>(unmanaged + sizeof(PenMotionEvent));
        //        break;
        //    case EventType.PenAxis:
        //        managed.PAxis = Marshal.PtrToStructure<PenAxisEvent>(unmanaged + sizeof(PenAxisEvent));
        //        break;
        //    case EventType.CameraDeviceAdded:
        //    case EventType.CameraDeviceRemoved:
        //    case EventType.CameraDeviceApproved:
        //    case EventType.CameraDeviceDenied:
        //        managed.CDevice = Marshal.PtrToStructure<CameraDeviceEvent>(unmanaged + sizeof(CameraDeviceEvent));
        //        break;
        //    case EventType.RenderTargetsReset:
        //    case EventType.RenderDeviceReset:
        //    case EventType.RenderDeviceLost:
        //        managed.Render = Marshal.PtrToStructure<RenderEvent>(unmanaged + sizeof(RenderEvent));
        //        break;
        //    case EventType.Private0:
        //    case EventType.Private1:
        //    case EventType.Private2:
        //    case EventType.Private3:
        //    case EventType.User:
        //        managed.User = Marshal.PtrToStructure<UserEvent>(unmanaged + sizeof(Event));
        //        break;
        //    case EventType.EnumPadding:
        //        break;
        //}

        return managed;
    }
    public static nint ConvertToUnmanaged(Event managed) {
        nuint ptr = new((nuint)managed.Type);
        return (nint)ptr;
    }

    public static nint ConvertToUnmanaged(Event[] managed) {
        if (managed == null) {
            return nint.Zero;
        }
        nint size = Marshal.SizeOf<Event>();
        nint ptr = Marshal.AllocHGlobal(size * managed.Length);
        for (int i = 0; i < managed.Length; i++) {
            Marshal.StructureToPtr(managed[i], ptr + (i * size), false);
        }
        return ptr;
    }
}
