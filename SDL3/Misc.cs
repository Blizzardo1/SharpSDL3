﻿using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static SharpSDL3.Sdl;

namespace SharpSDL3; 
 static partial class Sdl {
    // /usr/local/include/SDL3/SDL_misc.h

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
