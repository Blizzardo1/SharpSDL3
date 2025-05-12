using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static SDL3.Sdl;

namespace SDL3;

public static partial class Metal {
    // /usr/local/include/SDL3/SDL_metal.h

    public static nint CreateView(nint window) {
        if (window == nint.Zero) {
            throw new ArgumentException("Window handle cannot be null.", nameof(window));
        }
        return SDL_Metal_CreateView(window);
    }

    public static void DestroyView(nint view) {
        if (view == nint.Zero) {
            throw new ArgumentException("View handle cannot be null.", nameof(view));
        }
        SDL_Metal_DestroyView(view);
    }

    public static nint GetLayer(nint view) {
        if (view == nint.Zero) {
            throw new ArgumentException("View handle cannot be null.", nameof(view));
        }
        return SDL_Metal_GetLayer(view);
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_Metal_CreateView(nint window);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_Metal_DestroyView(nint view);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_Metal_GetLayer(nint view);
}