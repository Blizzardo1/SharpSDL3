using SharpSDL3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using static SharpSDL3.Sdl;
using static SharpSDL3.Delegates;

namespace SharpSDL3;

public static partial class Version {
    // /usr/local/include/SDL3/SDL_version.h

    public static string GetRevision() {
        return SDL_GetRevision();
    }

    public static int GetVersion() {
        return SDL_GetVersion();
    }

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetRevision();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetVersion();
}