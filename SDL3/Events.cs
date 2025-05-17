using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using static SharpSDL3.Delegates;
using static SharpSDL3.Sdl;

namespace SharpSDL3; 
public static unsafe partial class Events {

    public static bool AddEventWatch(SdlEventFilter filter, nint userdata) {
        // Validate the filter function pointer to ensure it is not null
        if (filter == null) {
            throw new ArgumentNullException(nameof(filter), "Filter function pointer cannot be null.");
        }
        // Call the native method and check the result
        var result = SDL_AddEventWatch(filter, userdata);
        return result;
    }

    public static bool EventEnabled(uint type) {
        // Validate the event type to ensure it is within a valid range  
        if (type == 0) {
            throw new ArgumentException("Event type cannot be zero.", nameof(type));
        }
        // Call the native method and return the result  
        return SDL_EventEnabled(type);
    }

    public static void FilterEvents(SdlEventFilter filter, nint userdata) {
        // Validate the filter function pointer to ensure it is not null
        if (filter == null) {
            throw new ArgumentNullException(nameof(filter), "Filter function pointer cannot be null.");
        }
        // Call the native method
        SDL_FilterEvents(filter, userdata);
    }

    public static void FlushEvent(uint type) {
        // Validate the event type to ensure it is within a valid range  
        if (type == 0) {
            throw new ArgumentException("Event type cannot be zero.", nameof(type));
        }
        // Call the native method  
        SDL_FlushEvent(type);
    }

    public static void FlushEvents(uint minType, uint maxType) {
        // Validate the event type range to ensure it is within a valid range  
        if (minType == 0 || maxType == 0) {
            throw new ArgumentException("Event type cannot be zero.", nameof(minType));
        }
        if (minType > maxType) {
            throw new ArgumentException("minType cannot be greater than maxType.", nameof(minType));
        }
        // Call the native method  
        SDL_FlushEvents(minType, maxType);
    }

    public static bool GetEventFilter(out SdlEventFilter filter, out nint userdata) {
        // Call the native method and check the result
        var result = SDL_GetEventFilter(out filter, out userdata);
        return result;
    }

    public static nint GetWindowFromEvent(ref Event @event) {
        // Validate the event structure to ensure it is in a valid state
        if (@event.Type == 0) {
            throw new ArgumentException("Event type cannot be zero.", nameof(@event));
        }
        
        try {
            // Call the native method and check the result
            var result = SDL_GetWindowFromEvent(ref @event);
            // Perform additional validation or processing if needed
            if (result == nint.Zero) {
                Logger.LogError(LogCategory.Error, "Failed to get window from event.");
            }
            return result;
        } catch (Exception ex) {
            throw new InvalidOperationException("Failed to copy event structure to unmanaged memory.", ex);
        }
    }

    public static bool HasEvent(uint type) {
        // Validate the event type to ensure it is within a valid range  
        if (type == 0) {
            throw new ArgumentException("Event type cannot be zero.", nameof(type));
        }

        // Call the native method and return the result  
        return SDL_HasEvent(type);
    }

    public static bool HasEvents(uint minType, uint maxType) {
        // Validate the event type range to ensure it is within a valid range  
        if (minType == 0 || maxType == 0) {
            throw new ArgumentException("Event type cannot be zero.", nameof(minType));
        }
        if (minType > maxType) {
            throw new ArgumentException("minType cannot be greater than maxType.", nameof(minType));
        }
        // Call the native method and return the result  
        return SDL_HasEvents(minType, maxType);
    }

    public static int PeepEvents(Event[] events, int numevents, EventAction action, uint minType, uint maxType) {
        if (numevents <= 0) {
            throw new ArgumentOutOfRangeException(nameof(numevents), "Number of events must be greater than zero.");
        }

        if (events.Length < numevents) {
            throw new ArgumentException("The provided array is smaller than the specified number of events.", nameof(events));
        }

        if (minType > maxType) {
            throw new ArgumentException("minType cannot be greater than maxType.", nameof(minType));
        }
        // Call the native method
        int result = SDL_PeepEvents(events, numevents, action, minType, maxType);
        return result;

    }

    public static bool PollEvent(out Event @event) {
        // Call the native method and check the result  
        var result = SDL_PollEvent(out @event);

        if (!result) {
            // No event was polled, return false
            Logger.LogError(LogCategory.Error, "Failed to poll event.");
            @event = default;
            return false;
        }

        return true;
    }

    public static void PumpEvents() {
        SDL_PumpEvents();
    }

    public static bool PushEvent(ref Event @event) {
        // Validate the event structure to ensure it is in a valid state
        if (@event.Type == 0) {
            throw new ArgumentException("Event type cannot be zero.", nameof(@event));
        }

        bool result = SDL_PushEvent(ref @event);
        
        if(!result) {
            // Failed to push the event, handle the error
            Logger.LogError(LogCategory.Error, "Failed to push event.");
        }

        return result;
    }

    public static uint RegisterEvents(int numevents) {
        // Validate the number of events to ensure it is greater than zero  
        if (numevents <= 0) {
            throw new ArgumentOutOfRangeException(nameof(numevents), "Number of events must be greater than zero.");
        }
        // Call the native method and return the result  
        return SDL_RegisterEvents(numevents);
    }

    public static void RemoveEventWatch(SdlEventFilter filter, nint userdata) {
        // Validate the filter function pointer to ensure it is not null
        if (filter == null) {
            throw new ArgumentNullException(nameof(filter), "Filter function pointer cannot be null.");
        }
        // Call the native method
        SDL_RemoveEventWatch(filter, userdata);
    }

    public static void SetEventEnabled(uint type, bool enabled) {
        // Validate the event type to ensure it is within a valid range  
        if (type == 0) {
            throw new ArgumentException("Event type cannot be zero.", nameof(type));
        }
        // Call the native method  
        SDL_SetEventEnabled(type, enabled);
    }

    public static void SetEventFilter(SdlEventFilter filter, nint userdata) {
        // Validate the filter function pointer to ensure it is not null
        if (filter == null) {
            throw new ArgumentNullException(nameof(filter), "Filter function pointer cannot be null.");
        }
        // Call the native method
        SDL_SetEventFilter(filter, userdata);
    }

    public static bool WaitEvent(out Event @event) {
        var result = SDL_WaitEvent(out @event);

        if (!result) {
            Logger.LogError(LogCategory.Error, "Failed to wait for event.");
        }

        return result;
    }

    public static bool WaitEventTimeout(out Event @event, int timeoutMs) {
        // Initialize the event structure to ensure it is in a valid state
        @event = default;
        // Validate the timeout value to ensure it is non-negative
        if (timeoutMs < 0) {
            throw new ArgumentOutOfRangeException(nameof(timeoutMs), "Timeout value cannot be negative.");
        }

        var result = SDL_WaitEventTimeout(out @event, timeoutMs);
        if (!result) {
            Logger.LogError(LogCategory.Error, "Failed to wait for event with timeout.");
        }
        return result;
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_AddEventWatch(SdlEventFilter filter, nint userdata);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_EventEnabled(uint type);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_FilterEvents(SdlEventFilter filter, nint userdata);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_FlushEvent(uint type);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_FlushEvents(uint minType, uint maxType);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetEventFilter(out SdlEventFilter filter, out nint userdata);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetWindowFromEvent([MarshalUsing(typeof(OwnedEventMarshaller))] ref Event @event);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasEvent(uint type);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasEvents(uint minType, uint maxType);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_PeepEvents([MarshalUsing(typeof(OwnedEventMarshaller))] Event[] events, int numevents, EventAction action, uint minType, uint maxType);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_PollEvent([MarshalUsing(typeof(OwnedEventMarshaller))] out Event @event);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_PumpEvents();
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_PushEvent([MarshalUsing(typeof(OwnedEventMarshaller))] ref Event @event);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_RegisterEvents(int numevents);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_RemoveEventWatch(SdlEventFilter filter, nint userdata);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetEventEnabled(uint type, [MarshalAs(UnmanagedType.I1)] bool enabled);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetEventFilter(SdlEventFilter filter, nint userdata);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WaitEvent([MarshalUsing(typeof(OwnedEventMarshaller))] out Event @event);
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WaitEventTimeout([MarshalUsing(typeof(OwnedEventMarshaller))] out Event @event, int timeoutMs);
}
