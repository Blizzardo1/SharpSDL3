using SharpSDL3.Structs;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSDL3;

public static partial class Sdl {
    // /usr/local/include/SDL3/SDL_cpuinfo.h

    /// <summary>Determine the L1 cache line size of the CPU.</summary>
    /// <remarks>
    /// This is useful for determining multi-threaded structure padding or SIMD
    /// prefetch sizes.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the L1 cache line size of the CPU, in bytes.</returns>

    public static int GetCPUCacheLineSize() {
        return SDL_GetCPUCacheLineSize();
    }

    /// <summary>Get the number of logical CPU cores available.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the total number of logical CPU cores. On CPUs that includetechnologies such as hyperthreading, the number of logical cores may bemore than the number of physical cores.</returns>

    public static int GetNumLogicalCPUCores() {
        return SDL_GetNumLogicalCPUCores();
    }

    /// <summary>Report the alignment this system needs for SIMD allocations.</summary>
    /// <remarks>
    /// This will return the minimum number of bytes to which a pointer must be
    /// aligned to be compatible with SIMD instructions on the current machine. For
    /// example, if the machine supports SSE only, it will return 16, but if it
    /// supports AVX-512F, it'll return 64 (etc). This only reports values for
    /// instruction sets SDL knows about, so if your SDL build doesn't have
    /// SDL_HasAVX512F(), then it might return 16 for the SSE
    /// support it sees and not 64 for the AVX-512 instructions that exist but SDL
    /// doesn't know about. Plan accordingly.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="aligned_alloc"/>
    /// <seealso cref="aligned_free"/>
    /// </remarks>
    /// <returns>Returns the alignment in bytes needed for available, known SIMDinstructions.</returns>

    public static nuint GetSIMDAlignment() {
        return SDL_GetSIMDAlignment();
    }

    /// <summary>Get the amount of RAM configured in the system.</summary>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the amount of RAM configured in the system in MiB.</returns>

    public static int GetSystemRAM() {
        return SDL_GetSystemRAM();
    }

    /// <summary>Determine whether the CPU has AltiVec features.</summary>
    /// <remarks>
    /// This always returns <see langword="false" /> on CPUs that aren't using PowerPC instruction
    /// sets.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the CPU has AltiVec features or <see langword="false" /> if not.</returns>

    public static SdlBool HasAltiVec() {
        return SDL_HasAltiVec();
    }

    /// <summary>Determine whether the CPU has ARM SIMD (ARMv6) features.</summary>
    /// <remarks>
    /// This is different from ARM NEON, which is a different instruction set.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="HasNEON"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the CPU has ARM SIMD features or <see langword="false" /> if not.</returns>

    public static SdlBool HasARMSIMD() {
        return SDL_HasARMSIMD();
    }

    /// <summary>Determine whether the CPU has AVX features.</summary>
    /// <remarks>
    /// This always returns <see langword="false" /> on CPUs that aren't using Intel instruction sets.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="HasAVX2"/>
    /// <seealso cref="HasAVX512F"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the CPU has AVX features or <see langword="false" /> if not.</returns>

    public static SdlBool HasAVX() {
        return SDL_HasAVX();
    }

    /// <summary>Determine whether the CPU has AVX2 features.</summary>
    /// <remarks>
    /// This always returns <see langword="false" /> on CPUs that aren't using Intel instruction sets.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="HasAVX"/>
    /// <seealso cref="HasAVX512F"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the CPU has AVX2 features or <see langword="false" /> if not.</returns>

    public static SdlBool HasAVX2() {
        return SDL_HasAVX2();
    }

    /// <summary>Determine whether the CPU has AVX-512F (foundation) features.</summary>
    /// <remarks>
    /// This always returns <see langword="false" /> on CPUs that aren't using Intel instruction sets.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="HasAVX"/>
    /// <seealso cref="HasAVX2"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the CPU has AVX-512F features or <see langword="false" /> if not.</returns>

    public static SdlBool HasAVX512F() {
        return SDL_HasAVX512F();
    }

    /// <summary>Determine whether the CPU has LASX (LOONGARCH SIMD) features.</summary>
    /// <remarks>
    /// This always returns <see langword="false" /> on CPUs that aren't using LOONGARCH instruction
    /// sets.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the CPU has LOONGARCH LASX features or <see langword="false" /> if not.</returns>

    public static SdlBool HasLASX() {
        return SDL_HasLASX();
    }

    /// <summary>Determine whether the CPU has LSX (LOONGARCH SIMD) features.</summary>
    /// <remarks>
    /// This always returns <see langword="false" /> on CPUs that aren't using LOONGARCH instruction
    /// sets.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the CPU has LOONGARCH LSX features or <see langword="false" /> if not.</returns>

    public static SdlBool HasLSX() {
        return SDL_HasLSX();
    }

    /// <summary>Determine whether the CPU has MMX features.</summary>
    /// <remarks>
    /// This always returns <see langword="false" /> on CPUs that aren't using Intel instruction sets.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the CPU has MMX features or <see langword="false" /> if not.</returns>

    public static SdlBool HasMMX() {
        return SDL_HasMMX();
    }

    /// <summary>Determine whether the CPU has NEON (ARM SIMD) features.</summary>
    /// <remarks>
    /// This always returns <see langword="false" /> on CPUs that aren't using ARM instruction sets.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the CPU has ARM NEON features or <see langword="false" /> if not.</returns>

    public static SdlBool HasNEON() {
        return SDL_HasNEON();
    }

    /// <summary>Determine whether the CPU has SSE features.</summary>
    /// <remarks>
    /// This always returns <see langword="false" /> on CPUs that aren't using Intel instruction sets.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="HasSSE2"/>
    /// <seealso cref="HasSSE3"/>
    /// <seealso cref="HasSSE41"/>
    /// <seealso cref="HasSSE42"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the CPU has SSE features or <see langword="false" /> if not.</returns>

    public static SdlBool HasSSE() {
        return SDL_HasSSE();
    }

    /// <summary>Determine whether the CPU has SSE2 features.</summary>
    /// <remarks>
    /// This always returns <see langword="false" /> on CPUs that aren't using Intel instruction sets.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="HasSSE"/>
    /// <seealso cref="HasSSE3"/>
    /// <seealso cref="HasSSE41"/>
    /// <seealso cref="HasSSE42"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the CPU has SSE2 features or <see langword="false" /> if not.</returns>

    public static SdlBool HasSSE2() {
        return SDL_HasSSE2();
    }

    /// <summary>Determine whether the CPU has SSE3 features.</summary>
    /// <remarks>
    /// This always returns <see langword="false" /> on CPUs that aren't using Intel instruction sets.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="HasSSE"/>
    /// <seealso cref="HasSSE2"/>
    /// <seealso cref="HasSSE41"/>
    /// <seealso cref="HasSSE42"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the CPU has SSE3 features or <see langword="false" /> if not.</returns>

    public static SdlBool HasSSE3() {
        return SDL_HasSSE3();
    }

    /// <summary>Determine whether the CPU has SSE4.1 features.</summary>
    /// <remarks>
    /// This always returns <see langword="false" /> on CPUs that aren't using Intel instruction sets.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="HasSSE"/>
    /// <seealso cref="HasSSE2"/>
    /// <seealso cref="HasSSE3"/>
    /// <seealso cref="HasSSE42"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the CPU has SSE4.1 features or <see langword="false" /> if not.</returns>

    public static SdlBool HasSSE41() {
        return SDL_HasSSE41();
    }

    /// <summary>Determine whether the CPU has SSE4.2 features.</summary>
    /// <remarks>
    /// This always returns <see langword="false" /> on CPUs that aren't using Intel instruction sets.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="HasSSE"/>
    /// <seealso cref="HasSSE2"/>
    /// <seealso cref="HasSSE3"/>
    /// <seealso cref="HasSSE41"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the CPU has SSE4.2 features or <see langword="false" /> if not.</returns>

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