using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;

namespace SharpSDL3;

public static partial class Sdl {
    // /usr/local/include/SDL3/SDL_version.h

    /// <summary>Get the code revision of the SDL library that is linked against your program.</summary>
    /// <remarks>
    /// This value is the revision of the code you are linking against and may be
    /// different from the code you are compiling with, which is found in the
    /// constant SDL_REVISION.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetVersion"/>
    /// </remarks>
    /// <returns>Returns an arbitrary string, uniquely identifying the exactrevision of the SDL library in use.</returns>

    public static string GetRevision() {
        return SDL_GetRevision();
    }

    /// <summary>Get the version of SDL that is linked against your program.</summary>
    /// <remarks>
    /// If you are linking to SDL dynamically, then it is possible that the current
    /// version will be different than the version you compiled against. This
    /// function returns the current version, while SDL_VERSION is
    /// the version you compiled with.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetRevision"/>
    /// </remarks>
    /// <returns>Returns the version of the linked library.</returns>

    public static int GetVersion() {
        return SDL_GetVersion();
    }

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetRevision();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetVersion();
}