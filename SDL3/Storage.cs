using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static SharpSDL3.Delegates;

namespace SharpSDL3;

public static partial class Sdl {

    public static void CloseStorage(nint storage) {
        if (storage == nint.Zero) {
            throw new ArgumentException("Storage handle must not be null.", nameof(storage));
        }
        SdlBool result = SDL_CloseStorage(storage);
        if (!result) {
            throw new InvalidOperationException("Failed to close storage.");
        }
    }

    public static bool CopyStorageFile(nint storage, string oldpath, string newpath) {
        if (storage == nint.Zero) {
            throw new ArgumentException("Storage handle must not be null.", nameof(storage));
        }
        if (string.IsNullOrEmpty(oldpath)) {
            throw new ArgumentException("Old path must not be null or empty.", nameof(oldpath));
        }
        if (string.IsNullOrEmpty(newpath)) {
            throw new ArgumentException("New path must not be null or empty.", nameof(newpath));
        }
        return SDL_CopyStorageFile(storage, oldpath, newpath);
    }

    public static bool CreateStorageDirectory(nint storage, string path) {
        if (storage == nint.Zero) {
            throw new ArgumentException("Storage handle must not be null.", nameof(storage));
        }
        if (string.IsNullOrEmpty(path)) {
            throw new ArgumentException("Path must not be null or empty.", nameof(path));
        }
        return SDL_CreateStorageDirectory(storage, path);
    }

    public static bool EnumerateStorageDirectory(nint storage, string path, SdlEnumerateDirectoryCallback callback, nint userdata = default) {
        if (storage == nint.Zero) {
            throw new ArgumentException("Storage handle must not be null.", nameof(storage));
        }
        if (string.IsNullOrEmpty(path)) {
            throw new ArgumentException("Path must not be null or empty.", nameof(path));
        }
        if (callback == null) {
            throw new ArgumentNullException(nameof(callback), "Callback cannot be null.");
        }
        return SDL_EnumerateStorageDirectory(storage, path, callback, userdata);
    }

    public static ulong GetStorageFileSize(nint storage, string path) {
        if (storage == nint.Zero) {
            throw new ArgumentException("Storage handle must not be null.", nameof(storage));
        }
        if (string.IsNullOrEmpty(path)) {
            throw new ArgumentException("Path must not be null or empty.", nameof(path));
        }
        SdlBool result = SDL_GetStorageFileSize(storage, path, out ulong length);
        if (!result) {
            throw new InvalidOperationException($"Failed to get file size for path: {path}");
        }
        return length;
    }

    public static PathInfo GetStoragePathInfo(nint storage, string path) {
        if (storage == nint.Zero) {
            throw new ArgumentException("Storage handle must not be null.", nameof(storage));
        }
        if (string.IsNullOrEmpty(path)) {
            throw new ArgumentException("Path must not be null or empty.", nameof(path));
        }
        SdlBool result = SDL_GetStoragePathInfo(storage, path, out PathInfo info);
        if (!result) {
            throw new InvalidOperationException($"Failed to get path info for: {path}");
        }
        return info;
    }

    public static ulong GetStorageSpaceRemaining(nint storage) {
        if (storage == nint.Zero) {
            throw new ArgumentException("Storage handle must not be null.", nameof(storage));
        }
        return SDL_GetStorageSpaceRemaining(storage);
    }

    public static nint GlobStorageDirectory(nint storage, string path, string pattern, GlobFlags flags, out int count) {
        if (storage == nint.Zero) {
            throw new ArgumentException("Storage handle must not be null.", nameof(storage));
        }
        if (string.IsNullOrEmpty(path)) {
            throw new ArgumentException("Path must not be null or empty.", nameof(path));
        }
        if (string.IsNullOrEmpty(pattern)) {
            throw new ArgumentException("Pattern must not be null or empty.", nameof(pattern));
        }
        nint result = SDL_GlobStorageDirectory(storage, path, pattern, flags, out count);
        if (result == nint.Zero) {
            throw new InvalidOperationException($"Failed to glob storage directory: {path}");
        }
        return result;
    }

    public static bool IsStorageReady(nint storage) {
        if (storage == nint.Zero) {
            throw new ArgumentException("Storage handle must not be null.", nameof(storage));
        }
        return SDL_StorageReady(storage);
    }

    public static nint OpenFileStorage(string path) {
        if (string.IsNullOrEmpty(path)) {
            throw new ArgumentException("Path must not be null or empty.", nameof(path));
        }
        return SDL_OpenFileStorage(path);
    }

    public static nint OpenStorage(ref StorageInterface iface, nint userdata = default) {
        if (iface.Version < 1) {
            throw new ArgumentException("Storage interface version must be at least 1.", nameof(iface));
        }
        return SDL_OpenStorage(ref iface, userdata);
    }

    public static nint OpenTitleStorage(string? @override = null, uint props = 0) {
        if (@override is not null && @override.Length > 0) {
            return SDL_OpenTitleStorage(@override, props);
        }
        return SDL_OpenTitleStorage(null, props);
    }

    public static nint OpenUserStorage(string org, string app, uint props = 0) {
        if (org.Length == 0 || app.Length == 0) {
            throw new ArgumentException("Organization and application names must not be empty.");
        }
        return SDL_OpenUserStorage(org, app, props);
    }

    public static bool ReadStorageFile(nint storage, string path, nint destination, ulong length) {
        if (storage == nint.Zero) {
            throw new ArgumentException("Storage handle must not be null.", nameof(storage));
        }
        if (string.IsNullOrEmpty(path)) {
            throw new ArgumentException("Path must not be null or empty.", nameof(path));
        }
        return SDL_ReadStorageFile(storage, path, destination, length);
    }

    public static bool RemoveStoragePath(nint storage, string path) {
        if (storage == nint.Zero) {
            throw new ArgumentException("Storage handle must not be null.", nameof(storage));
        }
        if (string.IsNullOrEmpty(path)) {
            throw new ArgumentException("Path must not be null or empty.", nameof(path));
        }
        return SDL_RemoveStoragePath(storage, path);
    }

    public static bool RenameStoragePath(nint storage, string oldpath, string newpath) {
        if (storage == nint.Zero) {
            throw new ArgumentException("Storage handle must not be null.", nameof(storage));
        }
        if (string.IsNullOrEmpty(oldpath)) {
            throw new ArgumentException("Old path must not be null or empty.", nameof(oldpath));
        }
        if (string.IsNullOrEmpty(newpath)) {
            throw new ArgumentException("New path must not be null or empty.", nameof(newpath));
        }
        return SDL_RenameStoragePath(storage, oldpath, newpath);
    }

    public static bool WriteStorageFile(nint storage, string path, nint source, ulong length) {
        if (storage == nint.Zero) {
            throw new ArgumentException("Storage handle must not be null.", nameof(storage));
        }
        if (string.IsNullOrEmpty(path)) {
            throw new ArgumentException("Path must not be null or empty.", nameof(path));
        }
        return SDL_WriteStorageFile(storage, path, source, length);
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CloseStorage(nint storage);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CopyStorageFile(nint storage, string oldpath, string newpath);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CreateStorageDirectory(nint storage, string path);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_EnumerateStorageDirectory(nint storage, string path,
        SdlEnumerateDirectoryCallback callback, nint userdata);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetStorageFileSize(nint storage, string path, out ulong length);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetStoragePathInfo(nint storage, string path, out PathInfo info);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong SDL_GetStorageSpaceRemaining(nint storage);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GlobStorageDirectory(nint storage, string path, string pattern,
        GlobFlags flags, out int count);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenFileStorage(string path);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenStorage(ref StorageInterface iface, nint userdata);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenTitleStorage(string? @override, uint props);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenUserStorage(string org, string app, uint props);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadStorageFile(nint storage, string path, nint destination, ulong length);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RemoveStoragePath(nint storage, string path);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenameStoragePath(nint storage, string oldpath, string newpath);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_StorageReady(nint storage);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteStorageFile(nint storage, string path, nint source, ulong length);
}