using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSDL3;

public static partial class Sdl {
    // /usr/local/include/SDL3/SDL_misc.h

    /// <summary>Open a URL/URI in the browser or other appropriate external application.</summary>
    /// <param name="url">a valid URL/URI to open. Use file:///full/path/to/file for local files, if supported.</param>
    /// <remarks>
    /// Open a URL in a separate, system-provided application. How this works will
    /// vary wildly depending on the platform. This will likely launch what makes
    /// sense to handle a specific URL's protocol (a web browser for http://,
    /// etc), but it might also be able to launch file managers for directories and
    /// other things.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>
    public static bool OpenURL(string url) {
        if (string.IsNullOrWhiteSpace(url)) {
            throw new ArgumentException("URL cannot be null or empty.", nameof(url));
        }

        if (!Uri.IsWellFormedUriString(url, UriKind.Absolute)) {
            throw new ArgumentException("URL is not well-formed.", nameof(url));
        }

        var result = SDL_OpenURL(url);
        if (!result) {
            LogError(LogCategory.Error, $"Failed to open URL: {url}");
        }

        return result;
    }

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_OpenURL(string url);
}