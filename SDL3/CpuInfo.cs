using SharpSDL3.Structs;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static SharpSDL3.Sdl;

namespace SharpSDL3; 
public static partial class Sdl {

    // /usr/local/include/SDL3/SDL_cpuinfo.h

    public static int GetCPUCacheLineSize() {
        return SDL_GetCPUCacheLineSize();
    }

    public static int GetNumLogicalCPUCores() {
        return SDL_GetNumLogicalCPUCores();
    }

    public static nuint GetSIMDAlignment() {
        return SDL_GetSIMDAlignment();
    }

    public static int GetSystemRAM() {
        return SDL_GetSystemRAM();
    }

    public static SdlBool HasAltiVec() {
        return SDL_HasAltiVec();
    }

    public static SdlBool HasARMSIMD() {
        return SDL_HasARMSIMD();
    }

    public static SdlBool HasAVX() {
        return SDL_HasAVX();
    }

    public static SdlBool HasAVX2() {
        return SDL_HasAVX2();
    }

    public static SdlBool HasAVX512F() {
        return SDL_HasAVX512F();
    }

    public static SdlBool HasLASX() {
        return SDL_HasLASX();
    }

    public static SdlBool HasLSX() {
        return SDL_HasLSX();
    }

    public static SdlBool HasMMX() {
        return SDL_HasMMX();
    }

    public static SdlBool HasNEON() {
        return SDL_HasNEON();
    }

    public static SdlBool HasSSE() {
        return SDL_HasSSE();
    }

    public static SdlBool HasSSE2() {
        return SDL_HasSSE2();
    }

    public static SdlBool HasSSE3() {
        return SDL_HasSSE3();
    }

    public static SdlBool HasSSE41() {
        return SDL_HasSSE41();
    }

    public static SdlBool HasSSE42() {
        return SDL_HasSSE42();
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetCPUCacheLineSize();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumLogicalCPUCores();
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint SDL_GetSIMDAlignment();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetSystemRAM();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasAltiVec();
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasARMSIMD();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasAVX();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasAVX2();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasAVX512F();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasLASX();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasLSX();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasMMX();
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasNEON();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasSSE();
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasSSE2();
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasSSE3();
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasSSE41();
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_HasSSE42();
}
