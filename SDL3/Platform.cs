using SharpSDL3.Enums;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;

using static SharpSDL3.Sdl;
using System;

namespace SharpSDL3; 
public static partial class Platform {

    // /usr/local/include/SDL3/SDL_platform.h

    public static string GetPlatform() {
        return SDL_GetPlatform();
    }

    public static nint CreateProcess(nint args, SdlBool pipeStdio) {
        if (args == nint.Zero) {
            throw new ArgumentException("Arguments cannot be null.", nameof(args));
        }
        return SDL_CreateProcess(args, pipeStdio);
    }

    public static nint CreateProcessWithProperties(uint props) {
        if (props == 0) {
            throw new ArgumentException("Properties cannot be zero.", nameof(props));
        }

        nint processHandle = SDL_CreateProcessWithProperties(props);

        if (processHandle == nint.Zero) {
            throw new InvalidOperationException("Failed to create process with the specified properties.");
        }

        return processHandle;
    }

    public static uint GetProcessProperties(nint process) {
        if (process == nint.Zero) {
            throw new ArgumentException("Process handle cannot be null.", nameof(process));
        }
        return SDL_GetProcessProperties(process);
    }

    public static nint ReadProcess(nint process, out nuint datasize, out int exitcode) {
        if (process == nint.Zero) {
            throw new ArgumentException("Process handle cannot be null.", nameof(process));
        }
        return SDL_ReadProcess(process, out datasize, out exitcode);
    }

    public static nint GetProcessInput(nint process) {
        if (process == nint.Zero) {
            throw new ArgumentException("Process handle cannot be null.", nameof(process));
        }
        return SDL_GetProcessInput(process);
    }

    public static nint GetProcessOutput(nint process) {
        if (process == nint.Zero) {
            throw new ArgumentException("Process handle cannot be null.", nameof(process));
        }
        return SDL_GetProcessOutput(process);
    }

    public static SdlBool KillProcess(nint process, SdlBool force) {
        if (process == nint.Zero) {
            throw new ArgumentException("Process handle cannot be null.", nameof(process));
        }
        return SDL_KillProcess(process, force);
    }

    public static SdlBool WaitProcess(nint process, SdlBool block, out int exitcode) {
        if (process == nint.Zero) {
            throw new ArgumentException("Process handle cannot be null.", nameof(process));
        }
        return SDL_WaitProcess(process, block, out exitcode);
    }

    public static void DestroyProcess(nint process) {
        if (process == nint.Zero) {
            throw new ArgumentException("Process handle cannot be null.", nameof(process));
        }
        SDL_DestroyProcess(process);
    }


    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetPlatform();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateProcess(nint args, SdlBool pipeStdio);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateProcessWithProperties(uint props);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetProcessProperties(nint process);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_ReadProcess(nint process, out nuint datasize, out int exitcode);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetProcessInput(nint process);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetProcessOutput(nint process);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_KillProcess(nint process, SdlBool force);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WaitProcess(nint process, SdlBool block, out int exitcode);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyProcess(nint process);
}
