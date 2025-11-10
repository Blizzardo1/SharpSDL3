using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static SharpSDL3.Delegates;

namespace SharpSDL3;

public static partial class Sdl {

    public static nint EglGetCurrentConfig() {
        nint result = SDL_EGL_GetCurrentConfig();
        if (result == nint.Zero) {
            throw new InvalidOperationException("Failed to get current EGL config.");
        }
        return result;
    }

    public static nint EglGetCurrentDisplay() {
        nint result = SDL_EGL_GetCurrentDisplay();
        if (result == nint.Zero) {
            throw new InvalidOperationException("Failed to get current EGL display.");
        }
        return result;
    }

    public static nint EglGetProcAddress(string proc) {
        if (string.IsNullOrWhiteSpace(proc)) {
            throw new ArgumentException("Procedure name cannot be null, empty, or whitespace.", nameof(proc));
        }
        nint result = SDL_EGL_GetProcAddress(proc);
        if (result == nint.Zero) {
            throw new InvalidOperationException($"Failed to get EGL procedure address for: {proc}");
        }
        return result;
    }

    public static nint EglGetWindowSurface(nint window) {
        if (window == nint.Zero) {
            throw new ArgumentNullException(nameof(window), "Window cannot be null.");
        }
        nint result = SDL_EGL_GetWindowSurface(window);
        if (result == nint.Zero) {
            throw new InvalidOperationException("Failed to get EGL window surface.");
        }
        return result;
    }

    public static void EglSetAttributeCallbacks(SdlEglAttribArrayCallback platformAttribCallback,
        SdlEglIntArrayCallback surfaceAttribCallback, SdlEglIntArrayCallback contextAttribCallback, nint userdata) {
        if (platformAttribCallback == null) {
            throw new ArgumentNullException(nameof(platformAttribCallback), "Platform attribute callback cannot be null.");
        }
        if (surfaceAttribCallback == null) {
            throw new ArgumentNullException(nameof(surfaceAttribCallback), "Surface attribute callback cannot be null.");
        }
        if (contextAttribCallback == null) {
            throw new ArgumentNullException(nameof(contextAttribCallback), "Context attribute callback cannot be null.");
        }

        SDL_EGL_SetAttributeCallbacks(platformAttribCallback, surfaceAttribCallback, contextAttribCallback, userdata);
    }

    public static nint GlCreateContext(nint window) {
        if (window == nint.Zero) {
            throw new ArgumentNullException(nameof(window), "Window cannot be null.");
        }
        nint result = SDL_GL_CreateContext(window);
        if (result == nint.Zero) {
            throw new InvalidOperationException("Failed to create OpenGL context.");
        }
        return result;
    }

    public static SdlBool GlDestroyContext(nint context) {
        if (context == nint.Zero) {
            throw new ArgumentNullException(nameof(context), "Context cannot be null.");
        }
        SdlBool result = SDL_GL_DestroyContext(context);
        if (!result) {
            throw new InvalidOperationException("Failed to destroy OpenGL context.");
        }
        return result;
    }

    public static SdlBool GlExtensionSupported(string extension) {
        if (string.IsNullOrWhiteSpace(extension)) {
            throw new ArgumentException("Extension name cannot be null, empty, or whitespace.", nameof(extension));
        }
        SdlBool result = SDL_GL_ExtensionSupported(extension);
        if (!result) {
            throw new InvalidOperationException($"OpenGL extension not supported: {extension}");
        }
        return result;
    }

    public static SdlBool GlGetAttribute(GlAttr attr, out int value) {
        if (attr < 0 || attr > GlAttr.EglPlatform) {
            throw new ArgumentOutOfRangeException(nameof(attr), "Invalid OpenGL attribute.");
        }
        SdlBool result = SDL_GL_GetAttribute(attr, out value);
        if (!result) {
            throw new InvalidOperationException($"Failed to get OpenGL attribute: {attr}");
        }
        return result;
    }

    public static nint GlGetCurrentContext() {
        nint result = SDL_GL_GetCurrentContext();
        if (result == nint.Zero) {
            throw new InvalidOperationException("Failed to get current OpenGL context.");
        }
        return result;
    }

    public static nint GlGetCurrentWindow() {
        nint result = SDL_GL_GetCurrentWindow();
        if (result == nint.Zero) {
            throw new InvalidOperationException("Failed to get current OpenGL window.");
        }
        return result;
    }

    public static nint GlGetProcAddress(string proc) {
        if (string.IsNullOrWhiteSpace(proc)) {
            throw new ArgumentException("Procedure name cannot be null, empty, or whitespace.", nameof(proc));
        }
        nint result = SDL_GL_GetProcAddress(proc);
        if (result == nint.Zero) {
            throw new InvalidOperationException($"Failed to get OpenGL procedure address for: {proc}");
        }
        return result;
    }

    public static SdlBool GlGetSwapInterval(out int interval) {
        SdlBool result = SDL_GL_GetSwapInterval(out interval);
        if (!result) {
            throw new InvalidOperationException("Failed to get OpenGL swap interval.");
        }
        return result;
    }

    public static SdlBool GlLoadLibrary(string path) {
        if (string.IsNullOrWhiteSpace(path)) {
            throw new ArgumentException("Path cannot be null, empty, or whitespace.", nameof(path));
        }

        SdlBool result = SDL_GL_LoadLibrary(path);
        if (!result) {
            throw new InvalidOperationException($"Failed to load OpenGL library from path: {path}");
        }

        return result;
    }

    public static SdlBool GlMakeCurrent(nint window, nint context) {
        if (window == nint.Zero) {
            throw new ArgumentNullException(nameof(window), "Window cannot be null.");
        }
        if (context == nint.Zero) {
            throw new ArgumentNullException(nameof(context), "Context cannot be null.");
        }
        SdlBool result = SDL_GL_MakeCurrent(window, context);
        if (!result) {
            throw new InvalidOperationException("Failed to make OpenGL context current.");
        }
        return result;
    }

    public static void GlResetAttributes() {
        SDL_GL_ResetAttributes();
    }

    public static SdlBool GlSetAttribute(GlAttr attr, int value) {
        if (attr < 0 || attr > GlAttr.EglPlatform) {
            throw new ArgumentOutOfRangeException(nameof(attr), "Invalid OpenGL attribute.");
        }
        if (value < 0) {
            throw new ArgumentOutOfRangeException(nameof(value), "Attribute value cannot be negative.");
        }
        SdlBool result = SDL_GL_SetAttribute(attr, value);
        if (!result) {
            throw new InvalidOperationException($"Failed to set OpenGL attribute: {attr}");
        }
        return result;
    }

    public static SdlBool GlSetSwapInterval(int interval) {
        if (interval < 0) {
            throw new ArgumentOutOfRangeException(nameof(interval), "Swap interval cannot be negative.");
        }
        SdlBool result = SDL_GL_SetSwapInterval(interval);
        if (!result) {
            throw new InvalidOperationException($"Failed to set OpenGL swap interval: {interval}");
        }
        return result;
    }

    public static SdlBool GlSwapWindow(nint window) {
        if (window == nint.Zero) {
            throw new ArgumentNullException(nameof(window), "Window cannot be null.");
        }
        SdlBool result = SDL_GL_SwapWindow(window);
        if (!result) {
            throw new InvalidOperationException("Failed to swap OpenGL window.");
        }
        return result;
    }

    public static void GlUnloadLibrary() {
        SDL_GL_UnloadLibrary();
    }

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_EGL_GetCurrentConfig();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_EGL_GetCurrentDisplay();

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_EGL_GetProcAddress(string proc);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_EGL_GetWindowSurface(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_EGL_SetAttributeCallbacks(SdlEglAttribArrayCallback platformAttribCallback,
        SdlEglIntArrayCallback surfaceAttribCallback, SdlEglIntArrayCallback contextAttribCallback, nint userdata);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GL_CreateContext(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GL_DestroyContext(nint context);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GL_ExtensionSupported(string extension);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GL_GetAttribute(GlAttr attr, out int value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GL_GetCurrentContext();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GL_GetCurrentWindow();

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GL_GetProcAddress(string proc);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GL_GetSwapInterval(out int interval);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GL_LoadLibrary(string path);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GL_MakeCurrent(nint window, nint context);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GL_ResetAttributes();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GL_SetAttribute(GlAttr attr, int value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GL_SetSwapInterval(int interval);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GL_SwapWindow(nint window);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GL_UnloadLibrary();
}