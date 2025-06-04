using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static SharpSDL3.Delegates;

namespace SharpSDL3;

public static unsafe partial class Sdl {
    /// <summary>Add a callback to be triggered when an event is added to the event queue.</summary>

    /// <param name="filter">an SDL_EventFilter function to call when an event happens.</param>
    /// <param name="userdata">a pointer that is passed to filter.</param>
    /// <remarks>
    /// filter will be called when an event happens, and its return value is
    /// ignored.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RemoveEventWatch"/>
    /// <seealso cref="SetEventFilter"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool AddEventWatch(SdlEventFilter filter, nint userdata) {
        // Validate the filter function pointer to ensure it is not null
        if (filter == null) {
            throw new ArgumentNullException(nameof(filter), "Filter function pointer cannot be null.");
        }
        // Call the native method and check the result
        var result = SDL_AddEventWatch(filter, userdata);
        return result;
    }

    /// <summary>Query the state of processing events by type.</summary>

    /// <param name="type">the type of event; see SDL_EventType for details.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetEventEnabled"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the event is being processed, <see langword="false" /> otherwise.</returns>

    public static bool EventEnabled(uint type) {
        // Validate the event type to ensure it is within a valid range
        if (type == 0) {
            throw new ArgumentException("Event type cannot be zero.", nameof(type));
        }
        // Call the native method and return the result
        return SDL_EventEnabled(type);
    }

    /// <summary>Run a specific filter function on the current event queue, removing any events for which the filter returns <see langword="false" />.</summary>

    /// <param name="filter">the SDL_EventFilter function to call when an event happens.</param>
    /// <param name="userdata">a pointer that is passed to filter.</param>
    /// <remarks>
    /// See SDL_SetEventFilter() for more information. Unlike
    /// SDL_SetEventFilter(), this function does not change
    /// the filter permanently, it only uses the supplied filter until this
    /// function returns.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetEventFilter"/>
    /// <seealso cref="SetEventFilter"/>
    /// </remarks>

    public static void FilterEvents(SdlEventFilter filter, nint userdata) {
        // Validate the filter function pointer to ensure it is not null
        if (filter == null) {
            throw new ArgumentNullException(nameof(filter), "Filter function pointer cannot be null.");
        }
        // Call the native method
        SDL_FilterEvents(filter, userdata);
    }

    /// <summary>Clear events of a specific type from the event queue.</summary>

    /// <param name="type">the type of event to be cleared; see SDL_EventType for details.</param>
    /// <remarks>
    /// This will unconditionally remove any events from the queue that match
    /// type. If you need to remove a range of event types, use
    /// SDL_FlushEvents() instead.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="FlushEvents"/>
    /// </remarks>

    public static void FlushEvent(uint type) {
        // Validate the event type to ensure it is within a valid range
        if (type == 0) {
            throw new ArgumentException("Event type cannot be zero.", nameof(type));
        }
        // Call the native method
        SDL_FlushEvent(type);
    }

    /// <summary>Clear events of a range of types from the event queue.</summary>

    /// <param name="minType">the low end of event type to be cleared, inclusive; see SDL_EventType for details.</param>
    /// <param name="maxType">the high end of event type to be cleared, inclusive; see SDL_EventType for details.</param>
    /// <remarks>
    /// This will unconditionally remove any events from the queue that are in the
    /// range of minType to maxType, inclusive. If you need to remove a single
    /// event type, use SDL_FlushEvent() instead.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="FlushEvent"/>
    /// </remarks>

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

    /// <summary>Query the current event filter.</summary>

    /// <param name="filter">the current callback function will be stored here.</param>
    /// <param name="userdata">the pointer that is passed to the current event filter will be stored here.</param>
    /// <remarks>
    /// This function can be used to &quot;chain&quot; filters, by saving the existing filter
    /// before replacing it with a function that will call that saved filter.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetEventFilter"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> if there is no event filter set.</returns>

    public static bool GetEventFilter(out SdlEventFilter filter, out nint userdata) {
        // Call the native method and check the result
        var result = SDL_GetEventFilter(out filter, out userdata);
        return result;
    }

    /// <summary>Get window associated with an event.</summary>

    /// <param name="event">an event containing a windowID.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PollEvent"/>
    /// <seealso cref="WaitEvent"/>
    /// <seealso cref="WaitEventTimeout"/>
    /// </remarks>
    /// <returns>(SDL_Window *) Returns the associated window on success or<see langword="null" /> if there is none.</returns>

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
                LogError(LogCategory.Error, "Failed to get window from event.");
            }
            return result;
        } catch (Exception ex) {
            throw new InvalidOperationException("Failed to copy event structure to unmanaged memory.", ex);
        }
    }

    /// <summary>Check for the existence of a certain event type in the event queue.</summary>

    /// <param name="type">the type of event to be queried; see SDL_EventType for details.</param>
    /// <remarks>
    /// If you need to check for a range of event types, use
    /// SDL_HasEvents() instead.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="HasEvents"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if events matching type are present, or <see langword="false" /> ifevents matching type are not present.</returns>

    public static bool HasEvent(uint type) {
        // Validate the event type to ensure it is within a valid range
        if (type == 0) {
            throw new ArgumentException("Event type cannot be zero.", nameof(type));
        }

        // Call the native method and return the result
        return SDL_HasEvent(type);
    }

    /// <summary>Check for the existence of certain event types in the event queue.</summary>

    /// <param name="minType">the low end of event type to be queried, inclusive; see SDL_EventType for details.</param>
    /// <param name="maxType">the high end of event type to be queried, inclusive; see SDL_EventType for details.</param>
    /// <remarks>
    /// If you need to check for a single event type, use
    /// SDL_HasEvent() instead.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="HasEvents"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if events with type &gt;= minType and &lt;= maxType arepresent, or <see langword="false" /> if not.</returns>

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

    /// <summary>Check the event queue for messages and optionally return them.</summary>

    /// <param name="events">destination buffer for the retrieved events, may be discarded to leave the events in the queue and return the number of events that would have been stored.</param>
    /// <param name="numevents">if action is SDL_ADDEVENT, the number of events to add back to the event queue; if action is SDL_PEEKEVENT or SDL_GETEVENT, the maximum number of events to retrieve.</param>
    /// <param name="action">action to take; see Remarks for details.</param>
    /// <param name="minType">minimum value of the event type to be considered; SDL_EVENT_FIRST is a safe choice.</param>
    /// <param name="maxType">maximum value of the event type to be considered; SDL_EVENT_LAST is a safe choice.</param>
    /// <remarks>
    /// action may be any of the following:
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PollEvent"/>
    /// <seealso cref="PumpEvents"/>
    /// <seealso cref="PushEvent"/>
    /// </remarks>
    /// <returns>Returns the number of events actually stored or -1 on failure; call <see cref="GetError()"/> for more information.</returns>

    public static int PeepEvents(ref Event[] events, int numevents, EventAction action, EventType minType, EventType maxType) {
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
        int result = SDL_PeepEvents(events, numevents, action, (uint)minType, (uint)maxType);
        return result;
    }

    /// <summary>Poll for currently pending events.</summary>

    /// <param name="event">the SDL_Event structure to be filled with the next event from the queue, or <see langword="null" />.</param>
    /// <remarks>
    /// If event is not <see langword="null" />, the next event is removed from the queue and stored
    /// in the SDL_Event structure pointed to by event. The 1
    /// returned refers to this event, immediately stored in the SDL Event
    /// structure -- not an event to follow.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PushEvent"/>
    /// <seealso cref="WaitEvent"/>
    /// <seealso cref="WaitEventTimeout"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if this got an event or <see langword="false" /> if there are noneavailable.</returns>

    public static bool PollEvent(out Event @event) {
        // Call the native method and check the result
        var result = SDL_PollEvent(out @event);

        if (!result) {
            return false;
        }

        return result;
    }

    /// <summary>Pump the event loop, gathering events from the input devices.</summary>
    /// <remarks>
    /// This function updates the event queue and internal input device state.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PollEvent"/>
    /// <seealso cref="WaitEvent"/>
    /// </remarks>

    public static void PumpEvents() {
        SDL_PumpEvents();
    }

    /// <summary>Add an event to the event queue.</summary>

    /// <param name="event">the SDL_Event to be added to the queue.</param>
    /// <remarks>
    /// The event queue can actually be used as a two way communication channel.
    /// Not only can events be read from the queue, but the user can also push
    /// their own events onto it. event is a pointer to the event structure you
    /// wish to push onto the queue. The event is copied into the queue, and the
    /// caller may dispose of the memory pointed to after
    /// SDL_PushEvent() returns.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PeepEvents"/>
    /// <seealso cref="PollEvent"/>
    /// <seealso cref="RegisterEvents"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success, <see langword="false" /> if the event was filtered or on failure; call <see cref="GetError()" /> for more information. A commonreason for error is the event queue being full.</returns>

    public static bool PushEvent(ref Event @event) {
        // Validate the event structure to ensure it is in a valid state
        if (@event.Type == 0) {
            throw new ArgumentException("Event type cannot be zero.", nameof(@event));
        }

        bool result = SDL_PushEvent(ref @event);

        if (!result) {
            // Failed to push the event, handle the error
            LogError(LogCategory.Error, "Failed to push event.");
        }

        return result;
    }

    /// <summary>Allocate a set of user-defined events, and return the beginning event number for that set of events.</summary>

    /// <param name="numevents">the number of events to be allocated.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PushEvent"/>
    /// </remarks>
    /// <returns>Returns the beginning event number, or 0 if numevents isinvalid or if there are not enough user-defined events left.</returns>

    public static uint RegisterEvents(int numevents) {
        // Validate the number of events to ensure it is greater than zero
        if (numevents <= 0) {
            throw new ArgumentOutOfRangeException(nameof(numevents), "Number of events must be greater than zero.");
        }
        // Call the native method and return the result
        return SDL_RegisterEvents(numevents);
    }

    /// <summary>Remove an event watch callback added with SDL_AddEventWatch().</summary>

    /// <param name="filter">the function originally passed to SDL_AddEventWatch().</param>
    /// <param name="userdata">the pointer originally passed to SDL_AddEventWatch().</param>
    /// <remarks>
    /// This function takes the same input as
    /// SDL_AddEventWatch() to identify and delete the
    /// corresponding callback.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="AddEventWatch"/>
    /// </remarks>

    public static void RemoveEventWatch(SdlEventFilter filter, nint userdata) {
        // Validate the filter function pointer to ensure it is not null
        if (filter == null) {
            throw new ArgumentNullException(nameof(filter), "Filter function pointer cannot be null.");
        }
        // Call the native method
        SDL_RemoveEventWatch(filter, userdata);
    }

    /// <summary>Set the state of processing events by type.</summary>

    /// <param name="type">the type of event; see SDL_EventType for details.</param>
    /// <param name="enabled">whether to process the event or not.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="EventEnabled"/>
    /// </remarks>

    public static void SetEventEnabled(uint type, bool enabled) {
        // Validate the event type to ensure it is within a valid range
        if (type == 0) {
            throw new ArgumentException("Event type cannot be zero.", nameof(type));
        }
        // Call the native method
        SDL_SetEventEnabled(type, enabled);
    }

    /// <summary>Set up a filter to process all events before they are added to the internal event queue.</summary>

    /// <param name="filter">an SDL_EventFilter function to call when an event happens.</param>
    /// <param name="userdata">a pointer that is passed to filter.</param>
    /// <remarks>
    /// If you just want to see events without modifying them or preventing them
    /// from being queued, you should use SDL_AddEventWatch()
    /// instead.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="AddEventWatch"/>
    /// <seealso cref="SetEventEnabled"/>
    /// <seealso cref="GetEventFilter"/>
    /// <seealso cref="PeepEvents"/>
    /// <seealso cref="PushEvent"/>
    /// </remarks>

    public static void SetEventFilter(SdlEventFilter filter, nint userdata) {
        // Validate the filter function pointer to ensure it is not null
        if (filter == null) {
            throw new ArgumentNullException(nameof(filter), "Filter function pointer cannot be null.");
        }
        // Call the native method
        SDL_SetEventFilter(filter, userdata);
    }

    /// <summary>Wait indefinitely for the next available event.</summary>

    /// <param name="event">the SDL_Event structure to be filled in with the next event from the queue, or <see langword="null" />.</param>
    /// <remarks>
    /// If event is not <see langword="null" />, the next event is removed from the queue and stored
    /// in the SDL_Event structure pointed to by event.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PollEvent"/>
    /// <seealso cref="PushEvent"/>
    /// <seealso cref="WaitEventTimeout"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> if there was an error while waitingfor events; call <see cref="GetError()" /> for more information.</returns>

    public static bool WaitEvent(out Event @event) {
        var result = SDL_WaitEvent(out @event);

        if (!result) {
            LogError(LogCategory.Error, "Failed to wait for event.");
        }

        return result;
    }

    /// <summary>Wait until the specified timeout (in milliseconds) for the next available event.</summary>

    /// <param name="event">the SDL_Event structure to be filled in with the next event from the queue, or <see langword="null" />.</param>
    /// <param name="timeoutMS">the maximum number of milliseconds to wait for the next available event.</param>
    /// <remarks>
    /// If event is not <see langword="null" />, the next event is removed from the queue and stored
    /// in the SDL_Event structure pointed to by event.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="PollEvent"/>
    /// <seealso cref="PushEvent"/>
    /// <seealso cref="WaitEvent"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if this got an event or <see langword="false" /> if the timeout elapsedwithout any events available.</returns>

    public static bool WaitEventTimeout(out Event @event, int timeoutMs) {
        // Initialize the event structure to ensure it is in a valid state
        @event = default;
        // Validate the timeout value to ensure it is non-negative
        if (timeoutMs < 0) {
            throw new ArgumentOutOfRangeException(nameof(timeoutMs), "Timeout value cannot be negative.");
        }

        var result = SDL_WaitEventTimeout(out @event, timeoutMs);
        if (!result) {
            LogError(LogCategory.Error, "Failed to wait for event with timeout.");
            return false;
        }

        return result;
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_AddEventWatch(SdlEventFilter filter, nint userdata);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_EventEnabled(uint type);

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
    private static partial SdlBool SDL_GetEventFilter(out SdlEventFilter filter, out nint userdata);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetWindowFromEvent(ref Event @event);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasEvent(uint type);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasEvents(uint minType, uint maxType);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_PeepEvents(Span<Event> events, int numevents, EventAction action, uint minType, uint maxType);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_PollEvent(out Event @event);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_PumpEvents();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_PushEvent(ref Event @event);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_RegisterEvents(int numevents);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_RemoveEventWatch(SdlEventFilter filter, nint userdata);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetEventEnabled(uint type, SdlBool enabled);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetEventFilter(SdlEventFilter filter, nint userdata);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WaitEvent(out Event @event);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WaitEventTimeout(out Event @event, int timeoutMs);
}