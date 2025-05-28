using SharpSDL3.Enums;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;

using static SharpSDL3.Delegates;
using SharpSDL3.Structs;

namespace SharpSDL3; 
public static unsafe partial class Sdl {
    public static void ClickTrayEntry(nint entry) {
        if (entry == nint.Zero) {
            throw new ArgumentNullException(nameof(entry), "Entry cannot be null.");
        }
        SDL_ClickTrayEntry(entry);
    }

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

    public static void DestroyTray(nint tray) {
        if (tray == nint.Zero) {
            throw new ArgumentNullException(nameof(tray), "Tray cannot be null.");
        }
        SDL_DestroyTray(tray);
    }

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

    public static void RemoveTrayEntry(nint entry) {
        if (entry == nint.Zero) {
            throw new ArgumentNullException(nameof(entry), "Entry cannot be null.");
        }
        SDL_RemoveTrayEntry(entry);
    }

    public static void SetTrayEntryCallback(nint entry, SdlTrayCallback callback, nint userdata) {
        if (entry == nint.Zero) {
            throw new ArgumentNullException(nameof(entry), "Entry cannot be null.");
        }
        if (callback == null) {
            throw new ArgumentNullException(nameof(callback), "Callback cannot be null.");
        }
        SDL_SetTrayEntryCallback(entry, callback, userdata);
    }

    public static void SetTrayEntryChecked(nint entry, bool check) {
        if (entry == nint.Zero) {
            throw new ArgumentNullException(nameof(entry), "Entry cannot be null.");
        }
        SDL_SetTrayEntryChecked(entry, check);
    }

    public static void SetTrayEntryEnabled(nint entry, bool enabled) {
        if (entry == nint.Zero) {
            throw new ArgumentNullException(nameof(entry), "Entry cannot be null.");
        }
        SDL_SetTrayEntryEnabled(entry, enabled);
    }

    public static void SetTrayEntryLabel(nint entry, string label) {
        if (entry == nint.Zero) {
            throw new ArgumentNullException(nameof(entry), "Entry cannot be null.");
        }
        if (string.IsNullOrWhiteSpace(label)) {
            throw new ArgumentException("Label cannot be null or empty.", nameof(label));
        }
        SDL_SetTrayEntryLabel(entry, label);
    }

    public static void SetTrayIcon(nint tray, nint icon) {
        if (tray == nint.Zero) {
            throw new ArgumentNullException(nameof(tray), "Tray cannot be null.");
        }
        if (icon == nint.Zero) {
            throw new ArgumentNullException(nameof(icon), "Icon cannot be null.");
        }
        SDL_SetTrayIcon(tray, icon);
    }

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
