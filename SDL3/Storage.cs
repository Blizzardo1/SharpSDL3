using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static SharpSDL3.Delegates;

namespace SharpSDL3;

public static partial class Sdl {
    /// <summary>Closes and frees a storage container.</summary>

    /// <param name="storage">a storage container to close.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="OpenFileStorage"/>
    /// <seealso cref="OpenStorage"/>
    /// <seealso cref="OpenTitleStorage"/>
    /// <seealso cref="OpenUserStorage"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the container was freed with no errors, <see langword="false" />otherwise; call <see cref="GetError()" /> for more information. Evenif the function returns an error, the container data will be freed; theerror is only for informational purposes.</returns>

    public static void CloseStorage(nint storage) {
        if (storage == nint.Zero) {
            throw new ArgumentException("Storage handle must not be null.", nameof(storage));
        }
        SdlBool result = SDL_CloseStorage(storage);
        if (!result) {
            throw new InvalidOperationException("Failed to close storage.");
        }
    }

    /// <summary>Copy a file in a writable storage container.</summary>

    /// <param name="storage">a storage container.</param>
    /// <param name="oldpath">the old path.</param>
    /// <param name="newpath">the new path.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="StorageReady"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

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

    /// <summary>Create a directory in a writable storage container.</summary>

    /// <param name="storage">a storage container.</param>
    /// <param name="path">the path of the directory to create.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="StorageReady"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool CreateStorageDirectory(nint storage, string path) {
        if (storage == nint.Zero) {
            throw new ArgumentException("Storage handle must not be null.", nameof(storage));
        }
        if (string.IsNullOrEmpty(path)) {
            throw new ArgumentException("Path must not be null or empty.", nameof(path));
        }
        return SDL_CreateStorageDirectory(storage, path);
    }

    /// <summary>Enumerate a directory in a storage container through a callback function.</summary>

    /// <param name="storage">a storage container.</param>
    /// <param name="path">the path of the directory to enumerate, or <see langword="null" /> for the root.</param>
    /// <param name="callback">a function that is called for each entry in the directory.</param>
    /// <param name="userdata">a pointer that is passed to callback.</param>
    /// <remarks>
    /// This function provides every directory entry through an app-provided
    /// callback, called once for each directory entry, until all results have been
    /// provided or the callback returns either
    /// SDL_ENUM_SUCCESS or
    /// SDL_ENUM_FAILURE.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="StorageReady"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

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

    /// <summary>Query the size of a file within a storage container.</summary>

    /// <param name="storage">a storage container to query.</param>
    /// <param name="path">the relative path of the file to query.</param>
    /// <param name="length">a pointer to be filled with the file's length.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ReadStorageFile"/>
    /// <seealso cref="StorageReady"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the file could be queried or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

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

    /// <summary>Get information about a filesystem path in a storage container.</summary>

    /// <param name="storage">a storage container.</param>
    /// <param name="path">the path to query.</param>
    /// <param name="info">a pointer filled in with information about the path, or <see langword="null" /> to check for the existence of a file.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="StorageReady"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> if the file doesn't exist, oranother failure; call <see cref="GetError()" /> for more information.</returns>

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

    /// <summary>Queries the remaining space in a storage container.</summary>

    /// <param name="storage">a storage container to query.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="StorageReady"/>
    /// <seealso cref="WriteStorageFile"/>
    /// </remarks>
    /// <returns>Returns the amount of remaining space, in bytes.</returns>

    public static ulong GetStorageSpaceRemaining(nint storage) {
        if (storage == nint.Zero) {
            throw new ArgumentException("Storage handle must not be null.", nameof(storage));
        }
        return SDL_GetStorageSpaceRemaining(storage);
    }

    /// <summary>Enumerate a directory tree, filtered by pattern, and return a list.</summary>

    /// <param name="storage">a storage container.</param>
    /// <param name="path">the path of the directory to enumerate, or <see langword="null" /> for the root.</param>
    /// <param name="pattern">the pattern that files in the directory must match. Can be <see langword="null" />.</param>
    /// <param name="flags">SDL_GLOB_* bitflags that affect this search.</param>
    /// <param name="count">on return, will be set to the number of items in the returned array. Can be <see langword="null" />.</param>
    /// <remarks>
    /// Files are filtered out if they don't match the string in pattern, which
    /// may contain wildcard characters * (match everything) and ? (match one
    /// character). If pattern is <see langword="null" />, no filtering is done and all results are
    /// returned. Subdirectories are permitted, and are specified with a path
    /// separator of '/'. Wildcard characters * and ? never match a path
    /// separator.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, assuming the storageobject is thread-safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(char **) Returns an array of strings on success or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information. The caller should passthe returned pointer to SDL_free when done with it. This is asingle allocation that should be freed with <see cref="Free"/> when itis no longer needed.</returns>

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

    /// <summary>Opens up a container for local filesystem storage.</summary>

    /// <param name="path">the base path prepended to all storage paths, or <see langword="null" /> for no base path.</param>
    /// <remarks>
    /// This is provided for development and tools. Portable applications should
    /// use SDL_OpenTitleStorage() for access to game data
    /// and SDL_OpenUserStorage() for access to user data.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CloseStorage"/>
    /// <seealso cref="GetStorageFileSize"/>
    /// <seealso cref="GetStorageSpaceRemaining"/>
    /// <seealso cref="OpenTitleStorage"/>
    /// <seealso cref="OpenUserStorage"/>
    /// <seealso cref="ReadStorageFile"/>
    /// <seealso cref="WriteStorageFile"/>
    /// </remarks>
    /// <returns>(SDL_Storage *) Returns a filesystem storage container on success or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint OpenFileStorage(string path) {
        if (string.IsNullOrEmpty(path)) {
            throw new ArgumentException("Path must not be null or empty.", nameof(path));
        }
        return SDL_OpenFileStorage(path);
    }

    /// <summary>Opens up a container using a client-provided storage interface.</summary>

    /// <param name="iface">the interface that implements this storage, initialized using SDL_INIT_INTERFACE().</param>
    /// <param name="userdata">the pointer that will be passed to the interface functions.</param>
    /// <remarks>
    /// Applications do not need to use this function unless they are providing
    /// their own SDL_Storage implementation. If you just need an
    /// SDL_Storage, you should use the built-in implementations in
    /// SDL, like SDL_OpenTitleStorage() or
    /// SDL_OpenUserStorage().
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CloseStorage"/>
    /// <seealso cref="GetStorageFileSize"/>
    /// <seealso cref="GetStorageSpaceRemaining"/>
    /// <seealso cref="INIT_INTERFACE"/>
    /// <seealso cref="ReadStorageFile"/>
    /// <seealso cref="StorageReady"/>
    /// <seealso cref="WriteStorageFile"/>
    /// </remarks>
    /// <returns>(SDL_Storage *) Returns a storage container on success or<see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint OpenStorage(ref StorageInterface iface, nint userdata = default) {
        if (iface.Version < 1) {
            throw new ArgumentException("Storage interface version must be at least 1.", nameof(iface));
        }
        return SDL_OpenStorage(ref iface, userdata);
    }

    /// <summary>Opens up a read-only container for the application's filesystem.</summary>

    /// <param name="override">a path to override the backend's default title root.</param>
    /// <param name="props">a property list that may contain backend-specific information.</param>
    /// <remarks>
    /// By default, SDL_OpenTitleStorage uses the generic
    /// storage implementation. When the path override is not provided, the generic
    /// implementation will use the output of SDL_GetBasePath as
    /// the base path.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CloseStorage"/>
    /// <seealso cref="GetStorageFileSize"/>
    /// <seealso cref="OpenUserStorage"/>
    /// <seealso cref="ReadStorageFile"/>
    /// </remarks>
    /// <returns>(SDL_Storage *) Returns a title storage container on successor <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint OpenTitleStorage(string? @override = null, uint props = 0) {
        if (@override is not null && @override.Length > 0) {
            return SDL_OpenTitleStorage(@override, props);
        }
        return SDL_OpenTitleStorage(null, props);
    }

    /// <summary>Opens up a container for a user's unique read/write filesystem.</summary>

    /// <param name="org">the name of your organization.</param>
    /// <param name="app">the name of your application.</param>
    /// <param name="props">a property list that may contain backend-specific information.</param>
    /// <remarks>
    /// While title storage can generally be kept open throughout runtime, user
    /// storage should only be opened when the client is ready to read/write files.
    /// This allows the backend to properly batch file operations and flush them
    /// when the container has been closed; ensuring safe and optimal save I/O.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CloseStorage"/>
    /// <seealso cref="GetStorageFileSize"/>
    /// <seealso cref="GetStorageSpaceRemaining"/>
    /// <seealso cref="OpenTitleStorage"/>
    /// <seealso cref="ReadStorageFile"/>
    /// <seealso cref="StorageReady"/>
    /// <seealso cref="WriteStorageFile"/>
    /// </remarks>
    /// <returns>(SDL_Storage *) Returns a user storage container on successor <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint OpenUserStorage(string org, string app, uint props = 0) {
        if (org.Length == 0 || app.Length == 0) {
            throw new ArgumentException("Organization and application names must not be empty.");
        }
        return SDL_OpenUserStorage(org, app, props);
    }

    /// <summary>Synchronously read a file from a storage container into a client-provided buffer.</summary>

    /// <param name="storage">a storage container to read from.</param>
    /// <param name="path">the relative path of the file to read.</param>
    /// <param name="destination">a client-provided buffer to read the file into.</param>
    /// <param name="length">the length of the destination buffer.</param>
    /// <remarks>
    /// The value of length must match the length of the file exactly; call
    /// SDL_GetStorageFileSize() to get this value. This
    /// behavior may be relaxed in a future release.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetStorageFileSize"/>
    /// <seealso cref="StorageReady"/>
    /// <seealso cref="WriteStorageFile"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the file was read or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool ReadStorageFile(nint storage, string path, nint destination, ulong length) {
        if (storage == nint.Zero) {
            throw new ArgumentException("Storage handle must not be null.", nameof(storage));
        }
        if (string.IsNullOrEmpty(path)) {
            throw new ArgumentException("Path must not be null or empty.", nameof(path));
        }
        return SDL_ReadStorageFile(storage, path, destination, length);
    }

    /// <summary>Remove a file or an empty directory in a writable storage container.</summary>

    /// <param name="storage">a storage container.</param>
    /// <param name="path">the path of the directory to enumerate.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="StorageReady"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool RemoveStoragePath(nint storage, string path) {
        if (storage == nint.Zero) {
            throw new ArgumentException("Storage handle must not be null.", nameof(storage));
        }
        if (string.IsNullOrEmpty(path)) {
            throw new ArgumentException("Path must not be null or empty.", nameof(path));
        }
        return SDL_RemoveStoragePath(storage, path);
    }

    /// <summary>Rename a file or directory in a writable storage container.</summary>

    /// <param name="storage">a storage container.</param>
    /// <param name="oldpath">the old path.</param>
    /// <param name="newpath">the new path.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="StorageReady"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

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

    /// <summary>Synchronously write a file from client memory into a storage container.</summary>

    /// <param name="storage">a storage container to write to.</param>
    /// <param name="path">the relative path of the file to write.</param>
    /// <param name="source">a client-provided buffer to write from.</param>
    /// <param name="length">the length of the source buffer.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetStorageSpaceRemaining"/>
    /// <seealso cref="ReadStorageFile"/>
    /// <seealso cref="StorageReady"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the file was written or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool WriteStorageFile(nint storage, string path, nint source, ulong length) {
        if (storage == nint.Zero) {
            throw new ArgumentException("Storage handle must not be null.", nameof(storage));
        }
        if (string.IsNullOrEmpty(path)) {
            throw new ArgumentException("Path must not be null or empty.", nameof(path));
        }
        return SDL_WriteStorageFile(storage, path, source, length);
    }

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CloseStorage(nint storage);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CopyStorageFile(nint storage, string oldpath, string newpath);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CreateStorageDirectory(nint storage, string path);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_EnumerateStorageDirectory(nint storage, string path,
        SdlEnumerateDirectoryCallback callback, nint userdata);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetStorageFileSize(nint storage, string path, out ulong length);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetStoragePathInfo(nint storage, string path, out PathInfo info);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong SDL_GetStorageSpaceRemaining(nint storage);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GlobStorageDirectory(nint storage, string path, string pattern,
        GlobFlags flags, out int count);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenFileStorage(string path);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenStorage(ref StorageInterface iface, nint userdata);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenTitleStorage(string? @override, uint props);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenUserStorage(string org, string app, uint props);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadStorageFile(nint storage, string path, nint destination, ulong length);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RemoveStoragePath(nint storage, string path);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenameStoragePath(nint storage, string oldpath, string newpath);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_StorageReady(nint storage);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteStorageFile(nint storage, string path, nint source, ulong length);
}