using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static SharpSDL3.Delegates;

using static SharpSDL3.Sdl;

namespace SharpSDL3;

public static partial class FileDialog {

    public static void OpenFile(SdlDialogFileCallback callback, nint userdata, nint window,
        Span<DialogFileFilter> filters, int nfilters, string defaultLocation, SdlBool allowMany) {
        if (callback == null) {
            throw new ArgumentNullException(nameof(callback), "Callback cannot be null.");
        }

        if (filters.Length != nfilters) {
            throw new ArgumentException("The number of filters does not match the specified nfilters.", nameof(filters));
        }

        if (string.IsNullOrWhiteSpace(defaultLocation)) {
            throw new ArgumentException("Default location cannot be null or whitespace.", nameof(defaultLocation));
        }

        SDL_ShowOpenFileDialog(callback, userdata, window, filters, nfilters, defaultLocation, allowMany);
    }

    public static void OpenFolder(SdlDialogFileCallback callback, nint userdata, nint window,
        string defaultLocation, SdlBool allowMany) {
        if (callback == null) {
            throw new ArgumentNullException(nameof(callback), "Callback cannot be null.");
        }
        if (string.IsNullOrWhiteSpace(defaultLocation)) {
            throw new ArgumentException("Default location cannot be null or whitespace.", nameof(defaultLocation));
        }
        SDL_ShowOpenFolderDialog(callback, userdata, window, defaultLocation, allowMany);
    }

    public static void SaveFile(SdlDialogFileCallback callback, nint userdata, nint window,
        Span<DialogFileFilter> filters, int nfilters, string defaultLocation) {
        if (callback == null) {
            throw new ArgumentNullException(nameof(callback), "Callback cannot be null.");
        }
        if (filters.Length != nfilters) {
            throw new ArgumentException("The number of filters does not match the specified nfilters.", nameof(filters));
        }
        if (string.IsNullOrWhiteSpace(defaultLocation)) {
            throw new ArgumentException("Default location cannot be null or whitespace.", nameof(defaultLocation));
        }
        SDL_ShowSaveFileDialog(callback, userdata, window, filters, nfilters, defaultLocation);
    }

    public static void ShowFileDialogWithProperties(FileDialogType type, SdlDialogFileCallback callback, nint userdata,
        uint props) {
        if (callback == null) {
            throw new ArgumentNullException(nameof(callback), "Callback cannot be null.");
        }
        SDL_ShowFileDialogWithProperties(type, callback, userdata, props);
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ShowFileDialogWithProperties(FileDialogType type, SdlDialogFileCallback callback,
        nint userdata, uint props);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ShowOpenFileDialog(SdlDialogFileCallback callback, nint userdata, nint window,
Span<DialogFileFilter> filters, int nfilters, string defaultLocation, SdlBool allowMany);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ShowOpenFolderDialog(SdlDialogFileCallback callback, nint userdata, nint window,
        string defaultLocation, SdlBool allowMany);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ShowSaveFileDialog(SdlDialogFileCallback callback, nint userdata, nint window,
        Span<DialogFileFilter> filters, int nfilters, string defaultLocation);
}