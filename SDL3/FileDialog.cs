using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static SharpSDL3.Delegates;

namespace SharpSDL3;

public static partial class Sdl {

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

    /// <summary>Create and launch a file dialog with the specified properties.</summary>

    /// <param name="type">the type of file dialog.</param>
    /// <param name="callback">a function pointer to be invoked when the user selects a file and accepts, or cancels the dialog, or an error occurs.</param>
    /// <param name="userdata">an optional pointer to pass extra data to the callback when it will be invoked.</param>
    /// <param name="props">the properties to use.</param>
    /// <remarks>
    /// These are the supported properties:
    /// <para><strong>Thread Safety:</strong> This function should be called only from the main thread. The callback maybe invoked from the same thread or from a different one, depending on theOS's constraints.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="FileDialogType"/>
    /// <seealso cref="DialogFileCallback"/>
    /// <seealso cref="DialogFileFilter"/>
    /// <seealso cref="ShowOpenFileDialog"/>
    /// <seealso cref="ShowSaveFileDialog"/>
    /// <seealso cref="ShowOpenFolderDialog"/>
    /// </remarks>

    public static void ShowFileDialogWithProperties(FileDialogType type, SdlDialogFileCallback callback, nint userdata,
            uint props) {
        if (callback == null) {
            throw new ArgumentNullException(nameof(callback), "Callback cannot be null.");
        }
        SDL_ShowFileDialogWithProperties(type, callback, userdata, props);
    }

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ShowFileDialogWithProperties(FileDialogType type, SdlDialogFileCallback callback,
        nint userdata, uint props);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ShowOpenFileDialog(SdlDialogFileCallback callback, nint userdata, nint window,
Span<DialogFileFilter> filters, int nfilters, string defaultLocation, SdlBool allowMany);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ShowOpenFolderDialog(SdlDialogFileCallback callback, nint userdata, nint window,
        string defaultLocation, SdlBool allowMany);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ShowSaveFileDialog(SdlDialogFileCallback callback, nint userdata, nint window,
        Span<DialogFileFilter> filters, int nfilters, string defaultLocation);
}