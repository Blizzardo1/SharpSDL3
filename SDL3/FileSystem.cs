using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using static SharpSDL3.Delegates;

namespace SharpSDL3;

public static partial class Sdl {
    // /usr/local/include/SDL3/SDL_filesystem.h
    /// <summary>Copy a file.</summary>

    /// <param name="oldpath">the old path.</param>
    /// <param name="newpath">the new path.</param>
    /// <remarks>
    /// If the file at newpath already exists, it will be overwritten with the
    /// contents of the file at oldpath.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool CopyFile(string oldpath, string newpath) {
        return SDL_CopyFile(oldpath, newpath);
    }

    /// <summary>Create a directory, and any missing parent directories.</summary>

    /// <param name="path">the path of the directory to create.</param>
    /// <remarks>
    /// This reports success if path already exists as a directory.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool CreateDirectory(string path) {
        return SDL_CreateDirectory(path);
    }

    /// <summary>Enumerate a directory through a callback function.</summary>

    /// <param name="path">the path of the directory to enumerate.</param>
    /// <param name="callback">a function that is called for each entry in the directory.</param>
    /// <param name="userdata">a pointer that is passed to callback.</param>
    /// <remarks>
    /// This function provides every directory entry through an app-provided
    /// callback, called once for each directory entry, until all results have been
    /// provided or the callback returns either
    /// SDL_ENUM_SUCCESS or
    /// SDL_ENUM_FAILURE.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool EnumerateDirectory(string path, SdlEnumerateDirectoryCallback callback, nint userdata) {
        return SDL_EnumerateDirectory(path, callback, userdata);
    }

    /// <summary>Get the directory where the application was run from.</summary>
    /// <remarks>
    /// SDL caches the result of this call internally, but the first call to this
    /// function is not necessarily fast, so plan accordingly.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetPrefPath"/>
    /// </remarks>
    /// <returns>Returns an absolute path in UTF-8 encoding to theapplication data directory. <see langword="null" /> will be returned on error or when theplatform doesn't implement this functionality, call <see cref="GetError()"/> for more information.</returns>

    public static string GetBasePath() {
        return SDL_GetBasePath();
    }

    /// <summary>Get what the system believes is the &quot;current working directory.&quot;</summary>
    /// <remarks>
    /// For systems without a concept of a current working directory, this will
    /// still attempt to provide something reasonable.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>(char *) Returns a UTF-8 string of the current working directory inplatform-dependent notation. <see langword="null" /> if there's a problem. This should befreed with <see cref="Free"/> when it is no longer needed.</returns>

    public static string GetCurrentDirectory() {
        return SDL_GetCurrentDirectory();
    }

    /// <summary>Get information about a filesystem path.</summary>

    /// <param name="path">the path to query.</param>
    /// <param name="info">a pointer filled in with information about the path, or <see langword="null" /> to check for the existence of a file.</param>
    /// <remarks>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> if the file doesn't exist, oranother failure; call <see cref="GetError()" /> for more information.</returns>

    public static bool GetPathInfo(string path, out PathInfo info) {
        return SDL_GetPathInfo(path, out info);
    }

    /// <summary>Get the user-and-app-specific path where files can be written.</summary>

    /// <param name="org">the name of your organization.</param>
    /// <param name="app">the name of your application.</param>
    /// <remarks>
    /// Get the &quot;pref dir&quot;. This is meant to be where users can write personal
    /// files (preferences and save games, etc) that are specific to your
    /// application. This directory is unique per user, per application.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetBasePath"/>
    /// </remarks>
    /// <returns>(char *) Returns a UTF-8 string of the user directory in platform-dependentnotation. <see langword="null" /> if there's a problem (creating directory failed, etc.). Thisshould be freed with <see cref="Free"/> when it is no longer needed.</returns>

    public static string GetPrefPath(string org, string app) {
        return SDL_GetPrefPath(org, app);
    }

    /// <summary>Finds the most suitable user folder for a specific purpose.</summary>

    /// <param name="folder">the type of folder to find.</param>
    /// <remarks>
    /// Many OSes provide certain standard folders for certain purposes, such as
    /// storing pictures, music or videos for a certain user. This function gives
    /// the path for many of those special locations.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns either a null-terminated C string containing thefull path to the folder, or <see langword="null" /> if an error happened.</returns>

    public static string GetUserFolder(Folder folder) {
        return SDL_GetUserFolder(folder);
    }

    /// <summary>Remove a file or an empty directory.</summary>

    /// <param name="path">the path to remove from the filesystem.</param>
    /// <remarks>
    /// Directories that are not empty will fail; this function will not recursely
    /// delete directory trees.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool RemovePath(string path) {
        return SDL_RemovePath(path);
    }

    /// <summary>Rename a file or directory.</summary>

    /// <param name="oldpath">the old path.</param>
    /// <param name="newpath">the new path.</param>
    /// <remarks>
    /// If the file at newpath already exists, it will replaced.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool RenamePath(string oldpath, string newpath) {
        return SDL_RenamePath(oldpath, newpath);
    }

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CopyFile(string oldpath, string newpath);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CreateDirectory(string path);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_EnumerateDirectory(string path, SdlEnumerateDirectoryCallback callback,
        nint userdata);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetBasePath();

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CallerOwnedStringMarshaller))]
    private static partial string SDL_GetCurrentDirectory();

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetPathInfo(string path, out PathInfo info);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CallerOwnedStringMarshaller))]
    private static partial string SDL_GetPrefPath(string org, string app);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetUserFolder(Folder folder);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GlobDirectory(string path, string pattern, GlobFlags flags, out int count);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RemovePath(string path);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_RenamePath(string oldpath, string newpath);
}