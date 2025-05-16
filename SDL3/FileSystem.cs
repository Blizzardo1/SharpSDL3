using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;

using static SharpSDL3.Sdl;
using static SharpSDL3.Delegates;

namespace SharpSDL3;

public static partial class FileSystem {
    // /usr/local/include/SDL3/SDL_filesystem.h
    public static SdlBool CopyFile(string oldpath, string newpath) {
        return SDL_CopyFile(oldpath, newpath);
    }

    public static SdlBool CreateDirectory(string path) {
        return SDL_CreateDirectory(path);
    }

    public static SdlBool EnumerateDirectory(string path, SdlEnumerateDirectoryCallback callback, nint userdata) {
        return SDL_EnumerateDirectory(path, callback, userdata);
    }

    public static string GetBasePath() {
        return SDL_GetBasePath();
    }

    public static string GetCurrentDirectory() {
        return SDL_GetCurrentDirectory();
    }

    public static SdlBool GetPathInfo(string path, out PathInfo info) {
        return SDL_GetPathInfo(path, out info);
    }

    public static string GetPrefPath(string org, string app) {
        return SDL_GetPrefPath(org, app);
    }

    public static string GetUserFolder(Folder folder) {
        return SDL_GetUserFolder(folder);
    }

    public static SdlBool RemovePath(string path) {
        return SDL_RemovePath(path);
    }

    public static SdlBool RenamePath(string oldpath, string newpath) {
        return SDL_RenamePath(oldpath, newpath);
    }

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CopyFile(string oldpath, string newpath);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CreateDirectory(string path);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_EnumerateDirectory(string path, SdlEnumerateDirectoryCallback callback,
        nint userdata);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetBasePath();

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CallerOwnedStringMarshaller))]
    private static partial string SDL_GetCurrentDirectory();

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetPathInfo(string path, out PathInfo info);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CallerOwnedStringMarshaller))]
    private static partial string SDL_GetPrefPath(string org, string app);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetUserFolder(Folder folder);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GlobDirectory(string path, string pattern, GlobFlags flags, out int count);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RemovePath(string path);

    [LibraryImport(NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenamePath(string oldpath, string newpath);
}