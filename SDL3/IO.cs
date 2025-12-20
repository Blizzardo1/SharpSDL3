using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using IoStatus = SharpSDL3.Enums.IoStatus;
using IoWhence = SharpSDL3.Enums.IoWhence;

namespace SharpSDL3;

public static partial class Sdl {

    public static bool CloseIo(IoStream context) {
        bool result = SDL_CloseIO(context.Handle);
        return !result ? throw new IOException($"closeIo failed: {GetError()}") : result;
    }

    public static bool FlushIo(IoStream context) {
        bool result = SDL_FlushIO(context.Handle);
        return !result ? throw new IOException($"FlushIO failed: {GetError()}") : result;
    }

    /// <summary>Get the properties associated with an <see cref="IoStream" />.</summary>
    /// <param name="context">a pointer to an <see cref="IoStream" /> structure.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns a valid property ID on success or 0 on failure; call <see cref="GetError()" /> for more information.</returns>
    public static uint GetIoProperties(IoStream context) {
        uint result = SDL_GetIOProperties(context.Handle);
        return result == 0 ? throw new IOException($"GetIOProperties failed: {GetError()}") : result;
    }

    /// <summary>Use this function to get the size of the data stream in an <see cref="IoStream" />.</summary>
    /// <param name="context">the <see cref="IoStream" /> to get the size of the data stream from.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the size of the data stream in the <see cref="IoStream" /> on success or a negative error code on failure; call <see cref="GetError()" /> for more information.</returns>
    public static long GetIoSize(IoStream context) {
        long result = SDL_GetIOSize(context.Handle);
        return result == 0 ? throw new IOException($"GetIOSize failed: {GetError()}") : result;
    }

    /// <summary>Query the stream status of an <see cref="IoStream" />.</summary>
    /// <param name="context">the <see cref="IoStream" /> to query.</param>
    /// <remarks>
    /// This information can be useful to decide if a short read or write was due
    /// to an error, an EOF, or a non-blocking operation that isn't yet ready to
    /// complete.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns an <see cref="IoStatus" /> enum with the current state.</returns>
    public static IoStatus GetIoStatus(IoStream context) {
        IoStatus result = SDL_GetIOStatus(context.Handle);
        return result == 0 ? throw new IOException($"GetIOStatus failed: {GetError()}") : result;
    }

    /// <summary>Use this function to prepare a read-only memory buffer for use with <see cref="IoStream" />.</summary>
    /// <param name="mem">a pointer to a read-only buffer to feed an <see cref="IoStream" /> stream.</param>
    /// <param name="size">the buffer size, in bytes.</param>
    /// <remarks>
    /// This function sets up an <see cref="IoStream" /> struct based on a
    /// memory area of a certain size. It assumes the memory area is not writable.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="IoFromMem" />
    /// <seealso cref="CloseIo" />
    /// <seealso cref="ReadIo" />
    /// <seealso cref="SeekIo" />
    /// <seealso cref="TellIo" />
    /// </remarks>
    /// <returns>(SDL_IOStream *) Returns a pointer to a new <see cref="IoStream" /> structure or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static IoStream IoFromConstMem(nint mem, nuint size) {
        nint result = SDL_IOFromConstMem(mem, size);
        if (result == nint.Zero) {
            throw new IOException($"IOFromConstMem failed: {GetError()}");
        }

        var stream = Marshal.PtrToStructure<IoStream>(result)!;
        stream.Handle = result;

        return stream;
    }

    /// <summary>Use this function to create an <see cref="IoStream" /> that is backed by dynamically allocated memory.</summary>
    /// <remarks>
    /// This supports the following properties to provide access to the memory and
    /// control over allocations:
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CloseIo" />
    /// <seealso cref="ReadIo" />
    /// <seealso cref="SeekIo" />
    /// <seealso cref="TellIo" />
    /// <seealso cref="WriteIo" />
    /// </remarks>
    /// <returns>(SDL_IOStream *) Returns a pointer to a new <see cref="IoStream" /> structure or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static IoStream IoFromDynamicMem() {
        nint result = SDL_IOFromDynamicMem();
        if (result == nint.Zero) {
            throw new IOException($"IOFromDynamicMem failed: {GetError()}");
        }

        var stream = Marshal.PtrToStructure<IoStream>(result)!;
        stream.Handle = result;

        return stream;
    }

    public static IoStream IoFromFile(string file, string mode) {
        nint result = SDL_IOFromFile(file, mode);
        if (result == nint.Zero) {
            throw new IOException($"IOFromFile failed: {GetError()}");
        }

        var stream = Marshal.PtrToStructure<IoStream>(result)!;
        stream.Handle = result;
        return stream;
    }

    /// <summary>Use this function to prepare a read-write memory buffer for use with <see cref="IoStream" />.</summary>
    /// <param name="mem">a pointer to a buffer to feed an <see cref="IoStream" /> stream.</param>
    /// <param name="size">the buffer size, in bytes.</param>
    /// <remarks>
    /// This function sets up an <see cref="IoStream" /> struct based on a
    /// memory area of a certain size, for both read and write access.
    /// <para><strong>Thread Safety</strong>: It is safe to call this function from any thread.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="IoFromConstMem" />
    /// <seealso cref="CloseIo" />
    /// <seealso cref="FlushIo" />
    /// <seealso cref="ReadIo" />
    /// <seealso cref="SeekIo" />
    /// <seealso cref="TellIo" />
    /// <seealso cref="WriteIo" />
    /// </remarks>
    /// <returns>(SDL_IOStream *) Returns a pointer to a new <see cref="IoStream" /> structure or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static IoStream IoFromMem(nint mem, nuint size) {
        nint result = SDL_IOFromMem(mem, size);
        if (result == nint.Zero) {
            throw new IOException($"IOFromMem failed: {GetError()}");
        }

        var stream = Marshal.PtrToStructure<IoStream>(result)!;
        stream.Handle = result;

        return stream;
    }

    /// <summary>Print to an <see cref="IoStream" /> data stream.</summary>
    /// <param name="context">a pointer to an <see cref="IoStream" /> structure.</param>
    /// <param name="fmt">a printf() style format string.</param>
    /// <remarks>
    /// This function does formatted printing to the stream.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="IOvprintf" />
    /// <seealso cref="WriteIo" />
    /// </remarks>
    /// <returns>Returns the number of bytes written or 0 on failure; call <see cref="GetError()" /> for more information.</returns>
    public static nuint IoPrintf(IoStream context, string fmt) {
        nuint result = SDL_IOprintf(context.Handle, fmt);
        return result == 0 ? throw new IOException($"IOprintf failed: {GetError()}") : result;
    }

    /// <summary>Load all the data from a file path.</summary>
    /// <param name="file">the path to read all available data from.</param>
    /// <param name="dataSize">if not <see langword="null" />, will store the number of bytes read.</param>
    /// <remarks>
    /// The data is allocated with a zero byte at the end (null terminated) for
    /// convenience. This extra byte is not included in the value reported via dataSize.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LoadFileIo" />
    /// <seealso cref="SaveFile(string, nint, nuint)" />
    /// </remarks>
    /// <returns>(void *) Returns the data or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static nint LoadFile(string file, out nuint dataSize) {
        // Initialize the variable to avoid CS0165
        nint result = SDL_LoadFile(file, out nuint ds);
        if (result == 0) {
            throw new IOException($"LoadFile failed: {GetError()}");
        }
        dataSize = ds;
        return result;
    }

    public static nint LoadFileIo(IoStream src, out nuint dataSize, bool closeIo) {
        // Initialize the variable to avoid CS0165
        nint result = SDL_LoadFile_IO(src.Handle, out nuint ds, closeIo);
        if (result == 0) {
            throw new IOException($"LoadFile_IO failed: {GetError()}");
        }

        dataSize = ds;
        return result;
    }

    public static IoStream OpenIo(ref IoStreamInterface iFace, nint userdata) {
        nint result = SDL_OpenIO(ref iFace, userdata);
        if (result == 0) {
            throw new IOException($"OpenIO failed: {GetError()}");
        }

        var stream = Marshal.PtrToStructure<IoStream>(result)!;
        stream.Handle = result;
        stream.Interface = iFace;

        return stream;
    }

    public static nuint ReadIo(IoStream context, nint ptr, nuint size) {
        nuint result = SDL_ReadIO(context.Handle, ptr, size);
        return result == 0 ? throw new IOException($"ReadIO failed: {GetError()}") : result;
    }

    /// <summary>Use this function to read 16 bits of big-endian data from an <see cref="IoStream" /> and return in native format.</summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// SDL byte swaps the data only if necessary, so the data returned will be in
    /// the native byte order.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool ReadS16Be(IoStream src, out short value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadS16BE(src.Handle, out short v);
        if (!result) {
            throw new IOException($"ReadS16BE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    /// <summary>Use this function to read 16 bits of little-endian data from an <see cref="IoStream" /> and return in native format.</summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// SDL byte swaps the data only if necessary, so the data returned will be in
    /// the native byte order.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool ReadS16Le(IoStream src, out short value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadS16LE(src.Handle, out short v);
        if (!result) {
            throw new IOException($"ReadS16LE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    /// <summary>Use this function to read 32 bits of big-endian data from an <see cref="IoStream" /> and return in native format.</summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// SDL byte swaps the data only if necessary, so the data returned will be in
    /// the native byte order.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool ReadS32Be(IoStream src, out int value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadS32BE(src.Handle, out int v);
        if (!result) {
            throw new IOException($"ReadS32BE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    /// <summary>Use this function to read 32 bits of little-endian data from an <see cref="IoStream" /> and return in native format.</summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// SDL byte swaps the data only if necessary, so the data returned will be in
    /// the native byte order.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool ReadS32Le(IoStream src, out int value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadS32LE(src.Handle, out int v);
        if (!result) {
            throw new IOException($"ReadS32LE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    /// <summary>Use this function to read 64 bits of big-endian data from an <see cref="IoStream" /> and return in native format.</summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// SDL byte swaps the data only if necessary, so the data returned will be in
    /// the native byte order.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool ReadS64Be(IoStream src, out long value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadS64BE(src.Handle, out long v);
        if (!result) {
            throw new IOException($"ReadS64BE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    /// <summary>Use this function to read 64 bits of little-endian data from an <see cref="IoStream" /> and return in native format.</summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// SDL byte swaps the data only if necessary, so the data returned will be in
    /// the native byte order.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool ReadS64Le(IoStream src, out long value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadS64LE(src.Handle, out long v);
        if (!result) {
            throw new IOException($"ReadS64LE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    /// <summary>Use this function to read a signed byte from an <see cref="IoStream" />.</summary>
    /// <param name="src">the <see cref="IoStream" /> to read from.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// <para>This function will return false when the data stream is completely read,
    /// and <see cref="GetIoStatus" /> will return <see cref="IoStatus.Eof" />.</para>
    /// <para>If <see langword="false" /> is returned and the stream
    /// is not at EOF, <see cref="GetIoStatus" /> will return a different
    /// error value and <see cref="GetError()" /> will offer a human-readable message.</para>
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool ReadS8(IoStream src, out sbyte value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadS8(src.Handle, out sbyte v);
        if (!result) {
            throw new IOException($"ReadS8 failed: {GetError()}");
        }
        value = v;
        return result;
    }

    /// <summary>Use this function to read 16 bits of big-endian data from an <see cref="IoStream" /> and return in native format.</summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// SDL byte swaps the data only if necessary, so the data returned will be in
    /// the native byte order.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool ReadU16Be(IoStream src, out ushort value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadU16BE(src.Handle, out ushort v);
        if (!result) {
            throw new IOException($"ReadU16BE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    /// <summary>Use this function to read 16 bits of little-endian data from an <see cref="IoStream" /> and return in native format.</summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// SDL byte swaps the data only if necessary, so the data returned will be in
    /// the native byte order.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool ReadU16Le(IoStream src, out ushort value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadU16LE(src.Handle, out ushort v);
        if (!result) {
            throw new IOException($"ReadU16LE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    /// <summary>Use this function to read 32 bits of big-endian data from an <see cref="IoStream" /> and return in native format.</summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// SDL byte swaps the data only if necessary, so the data returned will be in
    /// the native byte order.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool ReadU32Be(IoStream src, out uint value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadU32BE(src.Handle, out uint v);
        if (!result) {
            throw new IOException($"ReadU32BE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    /// <summary>Use this function to read 32 bits of little-endian data from an <see cref="IoStream" /> and return in native format.</summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// SDL byte swaps the data only if necessary, so the data returned will be in
    /// the native byte order.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool ReadU32Le(IoStream src, out uint value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadU32LE(src.Handle, out uint v);
        if (!result) {
            throw new IOException($"ReadU32LE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    /// <summary>Use this function to read 64 bits of big-endian data from an <see cref="IoStream" /> and return in native format.</summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// SDL byte swaps the data only if necessary, so the data returned will be in
    /// the native byte order.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool ReadU64Be(IoStream src, out ulong value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadU64BE(src.Handle, out ulong v);
        if (!result) {
            throw new IOException($"ReadU64BE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    /// <summary>Use this function to read 64 bits of little-endian data from an <see cref="IoStream" /> and return in native format.</summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// SDL byte swaps the data only if necessary, so the data returned will be in
    /// the native byte order.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool ReadU64Le(IoStream src, out ulong value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadU64LE(src.Handle, out ulong v);
        if (!result) {
            throw new IOException($"ReadU64LE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    /// <summary>Use this function to read a byte from an <see cref="IoStream" />.</summary>
    /// <param name="src">the SDL_IOStream to read from.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// This function will return false when the data stream is completely read,
    /// and <see cref="GetIoStatus" /> will return
    /// <see cref="IoStatus.Eof" />. If <see langword="false" /> is returned and the stream
    /// is not at EOF, <see cref="GetIoStatus" /> will return a different
    /// error value and <see cref="GetError()" /> will offer a human-readable
    /// message.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure or EOF; call <see cref="GetError()" /> for more information.</returns>
    public static bool ReadU8(IoStream src, out byte value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadU8(src.Handle, out byte v);
        if (!result) {
            throw new IOException($"ReadU8 failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static bool SaveFile(string file, nint data, nuint dataSize) {
        bool result = SDL_SaveFile(file, data, dataSize);
        return !result ? throw new IOException($"SaveFile failed: {GetError()}") : result;
    }

    public static bool SaveFileIo(nint src, nint data, nuint dataSize, bool closeIo) {
        bool result = SDL_SaveFile_IO(src, data, dataSize, closeIo);
        return !result ? throw new IOException($"SaveFile_IO failed: {GetError()}") : result;
    }

    public static long SeekIo(IoStream context, long offset, IoWhence whence) {
        long result = SDL_SeekIO(context.Handle, offset, whence);
        return result < 0 ? throw new IOException($"SeekIO failed: {GetError()}") : result;
    }

    public static long TellIo(IoStream context) {
        long result = SDL_TellIO(context.Handle);
        return result < 0 ? throw new IOException($"TellIO failed: {GetError()}") : result;
    }


    public static nuint WriteIo(IoStream context, nint ptr, nuint size) {
        nuint result = SDL_WriteIO(context.Handle, ptr, size);
        return result < size ? throw new IOException($"WriteIO failed: {GetError()}") : result;
    }

    /// <summary>Use this function to write 16 bits in native format to an <see cref="IoStream" /> as big-endian data.</summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <remarks>
    /// SDL byte swaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in big-endian format.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool WriteS16Be(IoStream dst, short value) {
        bool result = SDL_WriteS16BE(dst.Handle, value);
        return !result ? throw new IOException($"WriteS16BE failed: {GetError()}") : result;
    }

    /// <summary>Use this function to write 16 bits in native format to an <see cref="IoStream" /> as little-endian data.</summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <remarks>
    /// SDL byte swaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in little-endian
    /// format.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool WriteS16Le(IoStream dst, short value) {
        bool result = SDL_WriteS16LE(dst.Handle, value);
        return !result ? throw new IOException($"WriteS16LE failed: {GetError()}") : result;
    }

    /// <summary>Use this function to write 32 bits in native format to an <see cref="IoStream" /> as big-endian data.</summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <remarks>
    /// SDL byte swaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in big-endian format.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool WriteS32Be(IoStream dst, int value) {
        bool result = SDL_WriteS32BE(dst.Handle, value);
        return !result ? throw new IOException($"WriteS32BE failed: {GetError()}") : result;
    }

    /// <summary>Use this function to write 32 bits in native format to an <see cref="IoStream" /> as little-endian data.</summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <remarks>
    /// SDL byte swaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in little-endian
    /// format.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool WriteS32Le(IoStream dst, int value) {
        bool result = SDL_WriteS32LE(dst.Handle, value);
        return !result ? throw new IOException($"WriteS32LE failed: {GetError()}") : result;
    }

    /// <summary>Use this function to write 64 bits in native format to an <see cref="IoStream" /> as big-endian data.</summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <remarks>
    /// SDL byte swaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in big-endian format.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool WriteS64Be(IoStream dst, long value) {
        bool result = SDL_WriteS64BE(dst.Handle, value);
        return !result ? throw new IOException($"WriteS64BE failed: {GetError()}") : result;
    }

    /// <summary>Use this function to write 64 bits in native format to an <see cref="IoStream" /> as little-endian data.</summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <remarks>
    /// SDL byte swaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in little-endian
    /// format.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool WriteS64Le(IoStream dst, long value) {
        bool result = SDL_WriteS64LE(dst.Handle, value);
        return !result ? throw new IOException($"WriteS64LE failed: {GetError()}") : result;
    }

    /// <summary>Use this function to write a signed byte to an <see cref="IoStream" />.</summary>
    /// <param name="dst">the <see cref="IoStream" /> to write to.</param>
    /// <param name="value">the byte value to write.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool WriteS8(IoStream dst, sbyte value) {
        bool result = SDL_WriteS8(dst.Handle, value);
        return !result ? throw new IOException($"WriteS8 failed: {GetError()}") : result;
    }

    /// <summary>Use this function to write 16 bits in native format to an <see cref="IoStream" /> as big-endian data.</summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <remarks>
    /// SDL byte swaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in big-endian format.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool WriteU16Be(IoStream dst, ushort value) {
        bool result = SDL_WriteU16BE(dst.Handle, value);
        return !result ? throw new IOException($"WriteU16BE failed: {GetError()}") : result;
    }

    /// <summary>Use this function to write 16 bits in native format to an <see cref="IoStream" /> as little-endian data.</summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <remarks>
    /// SDL byte swaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in little-endian
    /// format.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool WriteU16Le(IoStream dst, ushort value) {
        bool result = SDL_WriteU16LE(dst.Handle, value);
        return !result ? throw new IOException($"WriteU16LE failed: {GetError()}") : result;
    }

    /// <summary>Use this function to write 32 bits in native format to an <see cref="IoStream" /> as big-endian data.</summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <remarks>
    /// SDL byte swaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in big-endian format.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool WriteU32Be(IoStream dst, uint value) {
        bool result = SDL_WriteU32BE(dst.Handle, value);
        return !result ? throw new IOException($"WriteU32BE failed: {GetError()}") : result;
    }

    /// <summary>Use this function to write 32 bits in native format to an <see cref="IoStream" /> as little-endian data.</summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <remarks>
    /// SDL byte swaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in little-endian
    /// format.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool WriteU32Le(IoStream dst, uint value) {
        bool result = SDL_WriteU32LE(dst.Handle, value);
        return !result ? throw new IOException($"WriteU32LE failed: {GetError()}") : result;
    }

    /// <summary>Use this function to write 64 bits in native format to an <see cref="IoStream" /> as big-endian data.</summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <remarks>
    /// SDL byte swaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in big-endian format.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool WriteU64Be(IoStream dst, ulong value) {
        bool result = SDL_WriteU64BE(dst.Handle, value);
        return !result ? throw new IOException($"WriteU64BE failed: {GetError()}") : result;
    }

    /// <summary>Use this function to write 64 bits in native format to an <see cref="IoStream" /> as little-endian data.</summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <remarks>
    /// SDL byte swaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in little-endian
    /// format.
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool WriteU64Le(IoStream dst, ulong value) {
        bool result = SDL_WriteU64LE(dst.Handle, value);
        return !result ? throw new IOException($"WriteU64LE failed: {GetError()}") : result;
    }

    /// <summary>Use this function to write a byte to an <see cref="IoStream" />.</summary>
    /// <param name="dst">the <see cref="IoStream" /> to write to.</param>
    /// <param name="value">the byte value to write.</param>
    /// <remarks>
    /// <para><strong>Thread Safety</strong>: This function is not thread safe.</para>
    /// <para><strong>Version</strong>: This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()" /> for more information.</returns>
    public static bool WriteU8(IoStream dst, byte value) {
        bool result = SDL_WriteU8(dst.Handle, value);
        return !result ? throw new IOException($"WriteU8 failed: {GetError()}") : result;
    }

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CloseIO(nint context);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_FlushIO(nint context);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetIOProperties(nint context);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long SDL_GetIOSize(nint context);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IoStatus SDL_GetIOStatus(nint context);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_IOFromConstMem(nint mem, nuint size);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_IOFromDynamicMem();

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_IOFromFile(string file, string mode);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_IOFromMem(nint mem, nuint size);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint SDL_IOprintf(nint context, string fmt);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_LoadFile(string file, out nuint dataSize);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_LoadFile_IO(nint src, out nuint dataSize, [MarshalAs(BoolType)] bool closeIo);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenIO(ref IoStreamInterface iFace, nint userdata);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint SDL_ReadIO(nint context, nint ptr, nuint size);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadS16BE(nint src, out short value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadS16LE(nint src, out short value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadS32BE(nint src, out int value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadS32LE(nint src, out int value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadS64BE(nint src, out long value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadS64LE(nint src, out long value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadS8(nint src, out sbyte value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadU16BE(nint src, out ushort value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadU16LE(nint src, out ushort value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadU32BE(nint src, out uint value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadU32LE(nint src, out uint value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadU64BE(nint src, out ulong value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadU64LE(nint src, out ulong value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadU8(nint src, out byte value);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SaveFile(string file, nint data, nuint dataSize);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SaveFile_IO(nint src, nint data, nuint dataSize, SdlBool closeIo);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long SDL_SeekIO(nint context, long offset, IoWhence whence);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long SDL_TellIO(nint context);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint SDL_WriteIO(nint context, nint ptr, nuint size);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteS16BE(nint dst, short value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteS16LE(nint dst, short value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteS32BE(nint dst, int value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteS32LE(nint dst, int value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteS64BE(nint dst, long value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteS64LE(nint dst, long value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteS8(nint dst, sbyte value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteU16BE(nint dst, ushort value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteU16LE(nint dst, ushort value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteU32BE(nint dst, uint value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteU32LE(nint dst, uint value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteU64BE(nint dst, ulong value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteU64LE(nint dst, ulong value);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteU8(nint dst, byte value);
}