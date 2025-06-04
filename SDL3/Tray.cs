using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using static SharpSDL3.Delegates;

namespace SharpSDL3;

public static unsafe partial class Sdl {
    /// <summary>Simulate a click on a tray entry.</summary>

    /// <param name="entry">The entry to activate.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the tray.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void ClickTrayEntry(nint entry) {
        if (entry == nint.Zero) {
            throw new ArgumentNullException(nameof(entry), "Entry cannot be null.");
        }
        SDL_ClickTrayEntry(entry);
    }

    /// <summary>Create an icon to be placed in the operating system's tray, or equivalent.</summary>

    /// <param name="icon">a surface to be used as icon. May be <see langword="null" />.</param>
    /// <param name="tooltip">a tooltip to be displayed when the mouse hovers the icon in UTF-8 encoding. Not supported on all platforms. May be <see langword="null" />.</param>
    /// <remarks>
    /// Many platforms advise not using a system tray unless persistence is a
    /// necessary feature. Avoid needlessly creating a tray icon, as the user may
    /// feel like it clutters their interface.
    /// <para><strong>Thread Safety:</strong> This function should only be called on the main thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateTrayMenu"/>
    /// <seealso cref="GetTrayMenu"/>
    /// <seealso cref="DestroyTray"/>
    /// </remarks>
    /// <returns>(SDL_Tray *) Returns The newly created system tray icon.</returns>

    public static nint CreateTray(nint icon, string tooltip) {
        if (icon == nint.Zero) {
            throw new ArgumentNullException(nameof(icon), "Icon cannot be null.");
        }
        if (string.IsNullOrWhiteSpace(tooltip)) {
            throw new ArgumentException("Tooltip cannot be null or empty.", nameof(tooltip));
        }
        nint tray = SDL_CreateTray(icon, tooltip);
        if (tray == nint.Zero) {
            LogError(LogCategory.Error, "Failed to create tray.");
        }
        return tray;
    }

    /// <summary>Create a menu for a system tray.</summary>

    /// <param name="tray">the tray to bind the menu to.</param>
    /// <remarks>
    /// This should be called at most once per tray icon.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the tray.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateTray"/>
    /// <seealso cref="GetTrayMenu"/>
    /// <seealso cref="GetTrayMenuParentTray"/>
    /// </remarks>
    /// <returns>(SDL_TrayMenu *) Returns the newly created menu.</returns>

    public static nint CreateTrayMenu(nint tray) {
        if (tray == nint.Zero) {
            throw new ArgumentNullException(nameof(tray), "Tray cannot be null.");
        }
        nint menu = SDL_CreateTrayMenu(tray);
        if (menu == nint.Zero) {
            LogError(LogCategory.Error, "Failed to create tray menu.");
        }
        return menu;
    }

    /// <summary>Create a submenu for a system tray entry.</summary>

    /// <param name="entry">the tray entry to bind the menu to.</param>
    /// <remarks>
    /// This should be called at most once per tray entry.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the tray.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="InsertTrayEntryAt"/>
    /// <seealso cref="GetTraySubmenu"/>
    /// <seealso cref="GetTrayMenuParentEntry"/>
    /// </remarks>
    /// <returns>(SDL_TrayMenu *) Returns the newly created menu.</returns>

    public static nint CreateTraySubmenu(nint entry) {
        if (entry == nint.Zero) {
            throw new ArgumentNullException(nameof(entry), "Entry cannot be null.");
        }
        nint submenu = SDL_CreateTraySubmenu(entry);
        if (submenu == nint.Zero) {
            LogError(LogCategory.Error, "Failed to create tray submenu.");
        }
        return submenu;
    }

    /// <summary>Destroys a tray object.</summary>

    /// <param name="tray">the tray icon to be destroyed.</param>
    /// <remarks>
    /// This also destroys all associated menus and entries.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the tray.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateTray"/>
    /// </remarks>

    public static void DestroyTray(nint tray) {
        if (tray == nint.Zero) {
            throw new ArgumentNullException(nameof(tray), "Tray cannot be null.");
        }
        SDL_DestroyTray(tray);
    }

    /// <summary>Returns a list of entries in the menu, in order.</summary>

    /// <param name="menu">The menu to get entries from.</param>
    /// <param name="count">An optional pointer to obtain the number of entries in the menu.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the tray.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="RemoveTrayEntry"/>
    /// <seealso cref="InsertTrayEntryAt"/>
    /// </remarks>
    /// <returns>(const SDL_TrayEntry **) Returns a <see langword="null" />-terminated list ofentries within the given menu. The pointer becomes invalid when anyfunction that inserts or deletes entries in the menu is called.</returns>

    public static Span<nint> GetTrayEntries(nint menu) {
        nint result = SDL_GetTrayEntries(menu, out int size);

        if (result == nint.Zero) {
            LogError(LogCategory.Error, "Failed to get tray entries.");
            return [];
        }

        if (size < 0) {
            LogError(LogCategory.Error, "Invalid size returned for tray entries.");
            return [];
        }
        if (size == 0) {
            return [];
        }

        nint[] entries = new nint[size];

        Marshal.Copy(result, entries, 0, size);

        Span<nint> array = new(ref result);

        return array.ToArray();
    }

    /// <summary>Gets whether or not an entry is checked.</summary>

    /// <param name="entry">the entry to be read.</param>
    /// <remarks>
    /// The entry must have been created with the
    /// SDL_TRAYENTRY_CHECKBOX flag.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the tray.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetTrayEntries"/>
    /// <seealso cref="InsertTrayEntryAt"/>
    /// <seealso cref="SetTrayEntryChecked"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the entry is checked; <see langword="false" /> otherwise.</returns>

    public static bool GetTrayEntryChecked(nint entry) {
        if (entry == nint.Zero) {
            throw new ArgumentNullException(nameof(entry), "Entry cannot be null.");
        }
        bool check = SDL_GetTrayEntryChecked(entry);
        if (!check) {
            LogError(LogCategory.Error, "Failed to get tray entry checked state.");
        }
        return check;
    }

    /// <summary>Gets whether or not an entry is enabled.</summary>

    /// <param name="entry">the entry to be read.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the tray.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetTrayEntries"/>
    /// <seealso cref="InsertTrayEntryAt"/>
    /// <seealso cref="SetTrayEntryEnabled"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the entry is enabled; <see langword="false" /> otherwise.</returns>

    public static bool GetTrayEntryEnabled(nint entry) {
        if (entry == nint.Zero) {
            throw new ArgumentNullException(nameof(entry), "Entry cannot be null.");
        }
        bool enabled = SDL_GetTrayEntryEnabled(entry);
        if (!enabled) {
            LogError(LogCategory.Error, "Failed to get tray entry enabled state.");
        }
        return enabled;
    }

    /// <summary>Gets the label of an entry.</summary>

    /// <param name="entry">the entry to be read.</param>
    /// <remarks>
    /// If the returned value is <see langword="null" />, the entry is a separator.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the tray.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetTrayEntries"/>
    /// <seealso cref="InsertTrayEntryAt"/>
    /// <seealso cref="SetTrayEntryLabel"/>
    /// </remarks>
    /// <returns>(const char *) Returns the label of the entry in UTF-8 encoding.</returns>

    public static string GetTrayEntryLabel(nint entry) {
        if (entry == nint.Zero) {
            throw new ArgumentNullException(nameof(entry), "Entry cannot be null.");
        }
        string label = SDL_GetTrayEntryLabel(entry);
        if (string.IsNullOrEmpty(label)) {
            LogError(LogCategory.Error, "Failed to get tray entry label.");
        }
        return label;
    }

    /// <summary>Gets the menu containing a certain tray entry.</summary>

    /// <param name="entry">the entry for which to get the parent menu.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the tray.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="InsertTrayEntryAt"/>
    /// </remarks>
    /// <returns>(SDL_TrayMenu *) Returns the parent menu.</returns>

    public static nint GetTrayEntryParent(nint entry) {
        if (entry == nint.Zero) {
            throw new ArgumentNullException(nameof(entry), "Entry cannot be null.");
        }
        nint parent = SDL_GetTrayEntryParent(entry);
        if (parent == nint.Zero) {
            LogError(LogCategory.Error, "Failed to get tray entry parent.");
        }
        return parent;
    }

    /// <summary>Gets a previously created tray menu.</summary>

    /// <param name="tray">the tray entry to bind the menu to.</param>
    /// <remarks>
    /// You should have called SDL_CreateTrayMenu() on the
    /// tray object. This function allows you to fetch it again later.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the tray.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateTray"/>
    /// <seealso cref="CreateTrayMenu"/>
    /// </remarks>
    /// <returns>(SDL_TrayMenu *) Returns the newly created menu.</returns>

    public static nint GetTrayMenu(nint tray) {
        if (tray == nint.Zero) {
            throw new ArgumentNullException(nameof(tray), "Tray cannot be null.");
        }
        nint menu = SDL_GetTrayMenu(tray);
        if (menu == nint.Zero) {
            LogError(LogCategory.Error, "Failed to get tray menu.");
        }
        return menu;
    }

    /// <summary>Gets the entry for which the menu is a submenu, if the current menu is a submenu.</summary>

    /// <param name="menu">the menu for which to get the parent entry.</param>
    /// <remarks>
    /// Either this function or
    /// SDL_GetTrayMenuParentTray() will return
    /// non-<see langword="null" /> for any given menu.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the tray.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateTraySubmenu"/>
    /// <seealso cref="GetTrayMenuParentTray"/>
    /// </remarks>
    /// <returns>(SDL_TrayEntry *) Returns the parent entry, or <see langword="null" /> ifthis menu is not a submenu.</returns>

    public static nint GetTrayMenuParentEntry(nint menu) {
        if (menu == nint.Zero) {
            throw new ArgumentNullException(nameof(menu), "Menu cannot be null.");
        }
        nint parentEntry = SDL_GetTrayMenuParentEntry(menu);
        if (parentEntry == nint.Zero) {
            LogError(LogCategory.Error, "Failed to get tray menu parent entry.");
        }
        return parentEntry;
    }

    /// <summary>Gets the tray for which this menu is the first-level menu, if the current menu isn't a submenu.</summary>

    /// <param name="menu">the menu for which to get the parent enttrayry.</param>
    /// <remarks>
    /// Either this function or
    /// SDL_GetTrayMenuParentEntry() will return
    /// non-<see langword="null" /> for any given menu.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the tray.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateTrayMenu"/>
    /// <seealso cref="GetTrayMenuParentEntry"/>
    /// </remarks>
    /// <returns>(SDL_Tray *) Returns the parent tray, or <see langword="null" /> if this menu is asubmenu.</returns>

    public static nint GetTrayMenuParentTray(nint menu) {
        if (menu == nint.Zero) {
            throw new ArgumentNullException(nameof(menu), "Menu cannot be null.");
        }
        nint parentTray = SDL_GetTrayMenuParentTray(menu);
        if (parentTray == nint.Zero) {
            LogError(LogCategory.Error, "Failed to get tray menu parent tray.");
        }
        return parentTray;
    }

    /// <summary>Gets a previously created tray entry submenu.</summary>

    /// <param name="entry">the tray entry to bind the menu to.</param>
    /// <remarks>
    /// You should have called SDL_CreateTraySubmenu() on
    /// the entry object. This function allows you to fetch it again later.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the tray.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="InsertTrayEntryAt"/>
    /// <seealso cref="CreateTraySubmenu"/>
    /// </remarks>
    /// <returns>(SDL_TrayMenu *) Returns the newly created menu.</returns>

    public static nint GetTraySubmenu(nint entry) {
        if (entry == nint.Zero) {
            throw new ArgumentNullException(nameof(entry), "Entry cannot be null.");
        }
        nint submenu = SDL_GetTraySubmenu(entry);
        if (submenu == nint.Zero) {
            LogError(LogCategory.Error, "Failed to get tray submenu.");
        }
        return submenu;
    }

    /// <summary>Insert a tray entry at a given position.</summary>

    /// <param name="menu">the menu to append the entry to.</param>
    /// <param name="pos">the desired position for the new entry. Entries at or following this place will be moved. If pos is -1, the entry is appended.</param>
    /// <param name="label">the text to be displayed on the entry, in UTF-8 encoding, or <see langword="null" /> for a separator.</param>
    /// <param name="flags">a combination of flags, some of which are mandatory.</param>
    /// <remarks>
    /// If label is <see langword="null" />, the entry will be a separator. Many functions won't work
    /// for an entry that is a separator.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the tray.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="TrayEntryFlags"/>
    /// <seealso cref="GetTrayEntries"/>
    /// <seealso cref="RemoveTrayEntry"/>
    /// <seealso cref="GetTrayEntryParent"/>
    /// </remarks>
    /// <returns>(SDL_TrayEntry *) Returns the newly created entry, or <see langword="null" />if pos is out of bounds.</returns>

    public static nint InsertTrayEntryAt(nint menu, int pos, string label, TrayEntryFlags flags) {
        if (menu == nint.Zero) {
            throw new ArgumentNullException(nameof(menu), "Menu cannot be null.");
        }
        if (string.IsNullOrWhiteSpace(label)) {
            throw new ArgumentException("Label cannot be null or empty.", nameof(label));
        }
        nint entry = SDL_InsertTrayEntryAt(menu, pos, label, flags);
        if (entry == nint.Zero) {
            LogError(LogCategory.Error, $"Failed to insert tray entry at position {pos}.");
        }
        return entry;
    }

    /// <summary>Removes a tray entry.</summary>

    /// <param name="entry">The entry to be deleted.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the tray.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetTrayEntries"/>
    /// <seealso cref="InsertTrayEntryAt"/>
    /// </remarks>

    public static void RemoveTrayEntry(nint entry) {
        if (entry == nint.Zero) {
            throw new ArgumentNullException(nameof(entry), "Entry cannot be null.");
        }
        SDL_RemoveTrayEntry(entry);
    }

    /// <summary>Sets a callback to be invoked when the entry is selected.</summary>

    /// <param name="entry">the entry to be updated.</param>
    /// <param name="callback">a callback to be invoked when the entry is selected.</param>
    /// <param name="userdata">an optional pointer to pass extra data to the callback when it will be invoked.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the tray.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetTrayEntries"/>
    /// <seealso cref="InsertTrayEntryAt"/>
    /// </remarks>

    public static void SetTrayEntryCallback(nint entry, SdlTrayCallback callback, nint userdata) {
        if (entry == nint.Zero) {
            throw new ArgumentNullException(nameof(entry), "Entry cannot be null.");
        }
        if (callback == null) {
            throw new ArgumentNullException(nameof(callback), "Callback cannot be null.");
        }
        SDL_SetTrayEntryCallback(entry, callback, userdata);
    }

    /// <summary>Sets whether or not an entry is checked.</summary>

    /// <param name="entry">the entry to be updated.</param>
    /// <param name="checked"><see langword="true" /> if the entry should be checked; <see langword="false" /> otherwise.</param>
    /// <remarks>
    /// The entry must have been created with the
    /// SDL_TRAYENTRY_CHECKBOX flag.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the tray.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetTrayEntries"/>
    /// <seealso cref="InsertTrayEntryAt"/>
    /// <seealso cref="GetTrayEntryChecked"/>
    /// </remarks>

    public static void SetTrayEntryChecked(nint entry, bool check) {
        if (entry == nint.Zero) {
            throw new ArgumentNullException(nameof(entry), "Entry cannot be null.");
        }
        SDL_SetTrayEntryChecked(entry, check);
    }

    /// <summary>Sets whether or not an entry is enabled.</summary>

    /// <param name="entry">the entry to be updated.</param>
    /// <param name="enabled"><see langword="true" /> if the entry should be enabled; <see langword="false" /> otherwise.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the tray.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetTrayEntries"/>
    /// <seealso cref="InsertTrayEntryAt"/>
    /// <seealso cref="GetTrayEntryEnabled"/>
    /// </remarks>

    public static void SetTrayEntryEnabled(nint entry, bool enabled) {
        if (entry == nint.Zero) {
            throw new ArgumentNullException(nameof(entry), "Entry cannot be null.");
        }
        SDL_SetTrayEntryEnabled(entry, enabled);
    }

    /// <summary>Sets the label of an entry.</summary>

    /// <param name="entry">the entry to be updated.</param>
    /// <param name="label">the new label for the entry in UTF-8 encoding.</param>
    /// <remarks>
    /// An entry cannot change between a separator and an ordinary entry; that is,
    /// it is not possible to set a non-<see langword="null" /> label on an entry that has a <see langword="null" />
    /// label (separators), or to set a <see langword="null" /> label to an entry that has a non-<see langword="null" />
    /// label. The function will silently fail if that happens.
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the tray.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetTrayEntries"/>
    /// <seealso cref="InsertTrayEntryAt"/>
    /// <seealso cref="GetTrayEntryLabel"/>
    /// </remarks>

    public static void SetTrayEntryLabel(nint entry, string label) {
        if (entry == nint.Zero) {
            throw new ArgumentNullException(nameof(entry), "Entry cannot be null.");
        }
        if (string.IsNullOrWhiteSpace(label)) {
            throw new ArgumentException("Label cannot be null or empty.", nameof(label));
        }
        SDL_SetTrayEntryLabel(entry, label);
    }

    /// <summary>Updates the system tray icon's icon.</summary>

    /// <param name="tray">the tray icon to be updated.</param>
    /// <param name="icon">the new icon. May be <see langword="null" />.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the tray.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateTray"/>
    /// </remarks>

    public static void SetTrayIcon(nint tray, nint icon) {
        if (tray == nint.Zero) {
            throw new ArgumentNullException(nameof(tray), "Tray cannot be null.");
        }
        if (icon == nint.Zero) {
            throw new ArgumentNullException(nameof(icon), "Icon cannot be null.");
        }
        SDL_SetTrayIcon(tray, icon);
    }

    /// <summary>Updates the system tray icon's tooltip.</summary>

    /// <param name="tray">the tray icon to be updated.</param>
    /// <param name="tooltip">the new tooltip in UTF-8 encoding. May be <see langword="null" />.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function should be called on the thread that created the tray.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateTray"/>
    /// </remarks>

    public static void SetTrayTooltip(nint tray, string tooltip) {
        if (tray == nint.Zero) {
            throw new ArgumentNullException(nameof(tray), "Tray cannot be null.");
        }
        if (string.IsNullOrWhiteSpace(tooltip)) {
            throw new ArgumentException("Tooltip cannot be null or empty.", nameof(tooltip));
        }
        SDL_SetTrayTooltip(tray, tooltip);
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ClickTrayEntry(nint entry);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateTray(nint icon, string tooltip);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateTrayMenu(nint tray);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateTraySubmenu(nint entry);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyTray(nint tray);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetTrayEntries(nint menu, out int size);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetTrayEntryChecked(nint entry);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetTrayEntryEnabled(nint entry);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetTrayEntryLabel(nint entry);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetTrayEntryParent(nint entry);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetTrayMenu(nint tray);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetTrayMenuParentEntry(nint menu);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetTrayMenuParentTray(nint menu);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetTraySubmenu(nint entry);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_InsertTrayEntryAt(nint menu, int pos, string label, TrayEntryFlags flags);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_RemoveTrayEntry(nint entry);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetTrayEntryCallback(nint entry, SdlTrayCallback callback, nint userdata);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetTrayEntryChecked(nint entry, SdlBool check);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetTrayEntryEnabled(nint entry, SdlBool enabled);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetTrayEntryLabel(nint entry, string label);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetTrayIcon(nint tray, nint icon);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetTrayTooltip(nint tray, string tooltip);
}