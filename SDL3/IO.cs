<<<<<<< HEAD
using SharpSDL3.Enums;
=======
﻿using SharpSDL3.Enums;
>>>>>>> main
using SharpSDL3.Structs;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

<<<<<<< HEAD
namespace SharpSDL3;

=======
using static SharpSDL3.Sdl;

namespace SharpSDL3; 
>>>>>>> main
public static partial class Sdl {

    public static bool CloseIO(IOStream context) {
        bool result = SDL_CloseIO(context.Handle);
        if (!result) {
<<<<<<< HEAD
            throw new IOException($"CloseIO failed: {GetError()}");
=======
            throw new IOException($"SDL_CloseIO failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

    public static bool FlushIO(IOStream context) {
        bool result = SDL_FlushIO(context.Handle);
        if (!result) {
<<<<<<< HEAD
            throw new IOException($"FlushIO failed: {GetError()}");
=======
            throw new IOException($"SDL_FlushIO failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Get the properties associated with an SDL_IOStream.</summary>

    /// <param name="context">a pointer to an SDL_IOStream structure.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns a valid property ID on success or 0 on failure; call <see cref="GetError()" /> for more information.</returns>

    public static uint GetIOProperties(IOStream context) {
        uint result = SDL_GetIOProperties(context.Handle);
        if (result == 0) {
            throw new IOException($"GetIOProperties failed: {GetError()}");
=======
    public static uint GetIOProperties(IOStream context) {
        uint result = SDL_GetIOProperties(context.Handle);
        if (result == 0) {
            throw new IOException($"SDL_GetIOProperties failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to get the size of the data stream in an SDL_IOStream.</summary>

    /// <param name="context">the SDL_IOStream to get the size of the data stream from.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the size of the data stream in theSDL_IOStream on success or a negative error code on failure; call <see cref="GetError()" /> for more information.</returns>

    public static long GetIOSize(IOStream context) {
        long result = SDL_GetIOSize(context.Handle);
        if (result == 0) {
            throw new IOException($"GetIOSize failed: {GetError()}");
=======
    public static long GetIOSize(IOStream context) {
        long result = SDL_GetIOSize(context.Handle);
        if (result == 0) {
            throw new IOException($"SDL_GetIOSize failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Query the stream status of an SDL_IOStream.</summary>

    /// <param name="context">the SDL_IOStream to query.</param>
    /// <remarks>
    /// This information can be useful to decide if a short read or write was due
    /// to an error, an EOF, or a non-blocking operation that isn't yet ready to
    /// complete.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns an SDL_IOStatus enumwith the current state.</returns>

    public static IoStatus GetIOStatus(IOStream context) {
        IoStatus result = SDL_GetIOStatus(context.Handle);
        if (result == 0) {
            throw new IOException($"GetIOStatus failed: {GetError()}");
=======
    public static IoStatus GetIOStatus(IOStream context) {
        IoStatus result = SDL_GetIOStatus(context.Handle);
        if (result == 0) {
            throw new IOException($"SDL_GetIOStatus failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to prepare a read-only memory buffer for use with SDL_IOStream.</summary>

    /// <param name="mem">a pointer to a read-only buffer to feed an SDL_IOStream stream.</param>
    /// <param name="size">the buffer size, in bytes.</param>
    /// <remarks>
    /// This function sets up an SDL_IOStream struct based on a
    /// memory area of a certain size. It assumes the memory area is not writable.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="IOFromMem"/>
    /// <seealso cref="CloseIO"/>
    /// <seealso cref="ReadIO"/>
    /// <seealso cref="SeekIO"/>
    /// <seealso cref="TellIO"/>
    /// </remarks>
    /// <returns>(SDL_IOStream *) Returns a pointer to a newSDL_IOStream structure or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static IOStream IOFromConstMem(nint mem, nuint size) {
        nint result = SDL_IOFromConstMem(mem, size);
        if (result == nint.Zero) {
            throw new IOException($"IOFromConstMem failed: {GetError()}");
=======
    public static IOStream IOFromConstMem(nint mem, nuint size) {
        nint result = SDL_IOFromConstMem(mem, size);
        if (result == nint.Zero) {
            throw new IOException($"SDL_IOFromConstMem failed: {GetError()}");
>>>>>>> main
        }

        IOStream stream = Marshal.PtrToStructure<IOStream>(result)!;
        stream.Handle = result;

        return stream;
    }

<<<<<<< HEAD
    /// <summary>Use this function to create an SDL_IOStream that is backed by dynamically allocated memory.</summary>
    /// <remarks>
    /// This supports the following properties to provide access to the memory and
    /// control over allocations:
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CloseIO"/>
    /// <seealso cref="ReadIO"/>
    /// <seealso cref="SeekIO"/>
    /// <seealso cref="TellIO"/>
    /// <seealso cref="WriteIO"/>
    /// </remarks>
    /// <returns>(SDL_IOStream *) Returns a pointer to a newSDL_IOStream structure or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static IOStream IOFromDynamicMem() {
        nint result = SDL_IOFromDynamicMem();
        if (result == nint.Zero) {
            throw new IOException($"IOFromDynamicMem failed: {GetError()}");
=======
    public static IOStream IOFromDynamicMem() {
        nint result = SDL_IOFromDynamicMem();
        if (result == nint.Zero) {
            throw new IOException($"SDL_IOFromDynamicMem failed: {GetError()}");
>>>>>>> main
        }

        IOStream stream = Marshal.PtrToStructure<IOStream>(result)!;
        stream.Handle = result;

        return stream;
    }

    public static IOStream IOFromFile(string file, string mode) {
        nint result = SDL_IOFromFile(file, mode);
        if (result == nint.Zero) {
<<<<<<< HEAD
            throw new IOException($"IOFromFile failed: {GetError()}");
=======
            throw new IOException($"SDL_IOFromFile failed: {GetError()}");
>>>>>>> main
        }

        IOStream stream = Marshal.PtrToStructure<IOStream>(result)!;
        stream.Handle = result;
        return stream;
    }

<<<<<<< HEAD
    /// <summary>Use this function to prepare a read-write memory buffer for use with SDL_IOStream.</summary>

    /// <param name="mem">a pointer to a buffer to feed an SDL_IOStream stream.</param>
    /// <param name="size">the buffer size, in bytes.</param>
    /// <remarks>
    /// This function sets up an SDL_IOStream struct based on a
    /// memory area of a certain size, for both read and write access.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="IOFromConstMem"/>
    /// <seealso cref="CloseIO"/>
    /// <seealso cref="FlushIO"/>
    /// <seealso cref="ReadIO"/>
    /// <seealso cref="SeekIO"/>
    /// <seealso cref="TellIO"/>
    /// <seealso cref="WriteIO"/>
    /// </remarks>
    /// <returns>(SDL_IOStream *) Returns a pointer to a newSDL_IOStream structure or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static IOStream IOFromMem(nint mem, nuint size) {
        nint result = SDL_IOFromMem(mem, size);
        if (result == nint.Zero) {
            throw new IOException($"IOFromMem failed: {GetError()}");
=======
    public static IOStream IOFromMem(nint mem, nuint size) {
        nint result = SDL_IOFromMem(mem, size);
        if (result == nint.Zero) {
            throw new IOException($"SDL_IOFromMem failed: {GetError()}");
>>>>>>> main
        }

        IOStream stream = Marshal.PtrToStructure<IOStream>(result)!;
        stream.Handle = result;

        return stream;
    }

<<<<<<< HEAD
    /// <summary>Print to an SDL_IOStream data stream.</summary>

    /// <param name="context">a pointer to an SDL_IOStream structure.</param>
    /// <param name="fmt">a printf() style format string.</param>
    /// <param name="...">additional parameters matching % tokens in the fmt string, if any.</param>
    /// <remarks>
    /// This function does formatted printing to the stream.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="IOvprintf"/>
    /// <seealso cref="WriteIO"/>
    /// </remarks>
    /// <returns>Returns the number of bytes written or 0 on failure; call <see cref="GetError()"/> for more information.</returns>

    public static nuint IOprintf(IOStream context, string fmt) {
        nuint result = SDL_IOprintf(context.Handle, fmt);
        if (result == 0) {
            throw new IOException($"IOprintf failed: {GetError()}");
=======
    public static nuint IOprintf(IOStream context, string fmt) {
        nuint result = SDL_IOprintf(context.Handle, fmt);
        if (result == 0) {
            throw new IOException($"SDL_IOprintf failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Load all the data from a file path.</summary>

    /// <param name="file">the path to read all available data from.</param>
    /// <param name="datasize">if not <see langword="null" />, will store the number of bytes read.</param>
    /// <remarks>
    /// The data is allocated with a zero byte at the end (null terminated) for
    /// convenience. This extra byte is not included in the value reported via
    /// datasize.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LoadFile_IO"/>
    /// <seealso cref="SaveFile"/>
    /// </remarks>
    /// <returns>(void *) Returns the data or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static nint LoadFile(string file, out nuint datasize) {
        // Initialize the variable to avoid CS0165
        nint result = SDL_LoadFile(file, out nuint ds);
        if (result == 0) {
<<<<<<< HEAD
            throw new IOException($"LoadFile failed: {GetError()}");
=======
            throw new IOException($"SDL_LoadFile failed: {GetError()}");
>>>>>>> main
        }
        datasize = ds;
        return result;
    }

    public static nint LoadFileIO(IOStream src, out nuint datasize, bool closeio) {
        // Initialize the variable to avoid CS0165
        nint result = SDL_LoadFile_IO(src.Handle, out nuint ds, closeio);
        if (result == 0) {
<<<<<<< HEAD
            throw new IOException($"LoadFile_IO failed: {GetError()}");
=======
            throw new IOException($"SDL_LoadFile_IO failed: {GetError()}");
>>>>>>> main
        }

        datasize = ds;
        return result;
    }

    public static IOStream OpenIO(ref IOStreamInterface iface, nint userdata) {
        nint result = SDL_OpenIO(ref iface, userdata);
        if (result == 0) {
<<<<<<< HEAD
            throw new IOException($"OpenIO failed: {GetError()}");
=======
            throw new IOException($"SDL_OpenIO failed: {GetError()}");
>>>>>>> main
        }

        IOStream stream = Marshal.PtrToStructure<IOStream>(result)!;
        stream.Handle = result;
        stream.Interface = iface;

        return stream;
    }

    public static nuint ReadIO(IOStream context, nint ptr, nuint size) {
        nuint result = SDL_ReadIO(context.Handle, ptr, size);
        if (result == 0) {
<<<<<<< HEAD
            throw new IOException($"ReadIO failed: {GetError()}");
=======
            throw new IOException($"SDL_ReadIO failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to read 16 bits of big-endian data from an SDL_IOStream and return in native format.</summary>

    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static bool ReadS16BE(IOStream src, out short value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadS16BE(src.Handle, out short v);
        if (!result) {
<<<<<<< HEAD
            throw new IOException($"ReadS16BE failed: {GetError()}");
=======
            throw new IOException($"SDL_ReadS16BE failed: {GetError()}");
>>>>>>> main
        }
        value = v;
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to read 16 bits of little-endian data from an SDL_IOStream and return in native format.</summary>

    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static bool ReadS16LE(IOStream src, out short value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadS16LE(src.Handle, out short v);
        if (!result) {
<<<<<<< HEAD
            throw new IOException($"ReadS16LE failed: {GetError()}");
=======
            throw new IOException($"SDL_ReadS16LE failed: {GetError()}");
>>>>>>> main
        }
        value = v;
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to read 32 bits of big-endian data from an SDL_IOStream and return in native format.</summary>

    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static bool ReadS32BE(IOStream src, out int value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadS32BE(src.Handle, out int v);
        if (!result) {
<<<<<<< HEAD
            throw new IOException($"ReadS32BE failed: {GetError()}");
=======
            throw new IOException($"SDL_ReadS32BE failed: {GetError()}");
>>>>>>> main
        }
        value = v;
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to read 32 bits of little-endian data from an SDL_IOStream and return in native format.</summary>

    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static bool ReadS32LE(IOStream src, out int value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadS32LE(src.Handle, out int v);
        if (!result) {
<<<<<<< HEAD
            throw new IOException($"ReadS32LE failed: {GetError()}");
=======
            throw new IOException($"SDL_ReadS32LE failed: {GetError()}");
>>>>>>> main
        }
        value = v;
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to read 64 bits of big-endian data from an SDL_IOStream and return in native format.</summary>

    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static bool ReadS64BE(IOStream src, out long value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadS64BE(src.Handle, out long v);
        if (!result) {
<<<<<<< HEAD
            throw new IOException($"ReadS64BE failed: {GetError()}");
=======
            throw new IOException($"SDL_ReadS64BE failed: {GetError()}");
>>>>>>> main
        }
        value = v;
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to read 64 bits of little-endian data from an SDL_IOStream and return in native format.</summary>

    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static bool ReadS64LE(IOStream src, out long value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadS64LE(src.Handle, out long v);
        if (!result) {
<<<<<<< HEAD
            throw new IOException($"ReadS64LE failed: {GetError()}");
=======
            throw new IOException($"SDL_ReadS64LE failed: {GetError()}");
>>>>>>> main
        }
        value = v;
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to read a signed byte from an SDL_IOStream.</summary>

    /// <param name="src">the SDL_IOStream to read from.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// This function will return false when the data stream is completely read,
    /// and SDL_GetIOStatus() will return
    /// SDL_IO_STATUS_EOF. If <see langword="false" /> is returned and the stream
    /// is not at EOF, SDL_GetIOStatus() will return a different
    /// error value and <see cref="GetError()" /> will offer a human-readable
    /// message.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static bool ReadS8(IOStream src, out sbyte value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadS8(src.Handle, out sbyte v);
        if (!result) {
<<<<<<< HEAD
            throw new IOException($"ReadS8 failed: {GetError()}");
=======
            throw new IOException($"SDL_ReadS8 failed: {GetError()}");
>>>>>>> main
        }
        value = v;
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to read 16 bits of big-endian data from an SDL_IOStream and return in native format.</summary>

    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static bool ReadU16BE(IOStream src, out ushort value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadU16BE(src.Handle, out ushort v);
        if (!result) {
<<<<<<< HEAD
            throw new IOException($"ReadU16BE failed: {GetError()}");
=======
            throw new IOException($"SDL_ReadU16BE failed: {GetError()}");
>>>>>>> main
        }
        value = v;
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to read 16 bits of little-endian data from an SDL_IOStream and return in native format.</summary>

    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static bool ReadU16LE(IOStream src, out ushort value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadU16LE(src.Handle, out ushort v);
        if (!result) {
<<<<<<< HEAD
            throw new IOException($"ReadU16LE failed: {GetError()}");
=======
            throw new IOException($"SDL_ReadU16LE failed: {GetError()}");
>>>>>>> main
        }
        value = v;
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to read 32 bits of big-endian data from an SDL_IOStream and return in native format.</summary>

    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static bool ReadU32BE(IOStream src, out uint value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadU32BE(src.Handle, out uint v);
        if (!result) {
<<<<<<< HEAD
            throw new IOException($"ReadU32BE failed: {GetError()}");
=======
            throw new IOException($"SDL_ReadU32BE failed: {GetError()}");
>>>>>>> main
        }
        value = v;
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to read 32 bits of little-endian data from an SDL_IOStream and return in native format.</summary>

    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static bool ReadU32LE(IOStream src, out uint value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadU32LE(src.Handle, out uint v);
        if (!result) {
<<<<<<< HEAD
            throw new IOException($"ReadU32LE failed: {GetError()}");
=======
            throw new IOException($"SDL_ReadU32LE failed: {GetError()}");
>>>>>>> main
        }
        value = v;
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to read 64 bits of big-endian data from an SDL_IOStream and return in native format.</summary>

    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static bool ReadU64BE(IOStream src, out ulong value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadU64BE(src.Handle, out ulong v);
        if (!result) {
<<<<<<< HEAD
            throw new IOException($"ReadU64BE failed: {GetError()}");
=======
            throw new IOException($"SDL_ReadU64BE failed: {GetError()}");
>>>>>>> main
        }
        value = v;
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to read 64 bits of little-endian data from an SDL_IOStream and return in native format.</summary>

    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static bool ReadU64LE(IOStream src, out ulong value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadU64LE(src.Handle, out ulong v);
        if (!result) {
<<<<<<< HEAD
            throw new IOException($"ReadU64LE failed: {GetError()}");
=======
            throw new IOException($"SDL_ReadU64LE failed: {GetError()}");
>>>>>>> main
        }
        value = v;
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to read a byte from an SDL_IOStream.</summary>

    /// <param name="src">the SDL_IOStream to read from.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <remarks>
    /// This function will return false when the data stream is completely read,
    /// and SDL_GetIOStatus() will return
    /// SDL_IO_STATUS_EOF. If <see langword="false" /> is returned and the stream
    /// is not at EOF, SDL_GetIOStatus() will return a different
    /// error value and <see cref="GetError()" /> will offer a human-readable
    /// message.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure or EOF; call <see cref="GetError()"/> for more information.</returns>

=======
>>>>>>> main
    public static bool ReadU8(IOStream src, out byte value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadU8(src.Handle, out byte v);
        if (!result) {
<<<<<<< HEAD
            throw new IOException($"ReadU8 failed: {GetError()}");
=======
            throw new IOException($"SDL_ReadU8 failed: {GetError()}");
>>>>>>> main
        }
        value = v;
        return result;
    }

    public static bool SaveFile(string file, nint data, nuint datasize) {
        bool result = SDL_SaveFile(file, data, datasize);
        if (!result) {
<<<<<<< HEAD
            throw new IOException($"SaveFile failed: {GetError()}");
=======
            throw new IOException($"SDL_SaveFile failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

    public static bool SaveFileIO(nint src, nint data, nuint datasize, bool closeio) {
        bool result = SDL_SaveFile_IO(src, data, datasize, closeio);
        if (!result) {
<<<<<<< HEAD
            throw new IOException($"SaveFile_IO failed: {GetError()}");
=======
            throw new IOException($"SDL_SaveFile_IO failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

    public static long SeekIO(IOStream context, long offset, IoWhence whence) {
        long result = SDL_SeekIO(context.Handle, offset, whence);
        if (result == 0) {
<<<<<<< HEAD
            throw new IOException($"SeekIO failed: {GetError()}");
=======
            throw new IOException($"SDL_SeekIO failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

    public static long TellIO(IOStream context) {
        long result = SDL_TellIO(context.Handle);
        if (result == 0) {
<<<<<<< HEAD
            throw new IOException($"TellIO failed: {GetError()}");
=======
            throw new IOException($"SDL_TellIO failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

    public static nuint WriteIO(IOStream context, nint ptr, nuint size) {
        nuint result = SDL_WriteIO(context.Handle, ptr, size);
        if (result == 0) {
<<<<<<< HEAD
            throw new IOException($"WriteIO failed: {GetError()}");
=======
            throw new IOException($"SDL_WriteIO failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to write 16 bits in native format to an SDL_IOStream as big-endian data.</summary>

    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <remarks>
    /// SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in big-endian format.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool WriteS16BE(IOStream dst, short value) {
        bool result = SDL_WriteS16BE(dst.Handle, value);
        if (!result) {
            throw new IOException($"WriteS16BE failed: {GetError()}");
=======
    public static bool WriteS16BE(IOStream dst, short value) {
        bool result = SDL_WriteS16BE(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteS16BE failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to write 16 bits in native format to an SDL_IOStream as little-endian data.</summary>

    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <remarks>
    /// SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in little-endian
    /// format.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool WriteS16LE(IOStream dst, short value) {
        bool result = SDL_WriteS16LE(dst.Handle, value);
        if (!result) {
            throw new IOException($"WriteS16LE failed: {GetError()}");
=======
    public static bool WriteS16LE(IOStream dst, short value) {
        bool result = SDL_WriteS16LE(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteS16LE failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to write 32 bits in native format to an SDL_IOStream as big-endian data.</summary>

    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <remarks>
    /// SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in big-endian format.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool WriteS32BE(IOStream dst, int value) {
        bool result = SDL_WriteS32BE(dst.Handle, value);
        if (!result) {
            throw new IOException($"WriteS32BE failed: {GetError()}");
=======
    public static bool WriteS32BE(IOStream dst, int value) {
        bool result = SDL_WriteS32BE(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteS32BE failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to write 32 bits in native format to an SDL_IOStream as little-endian data.</summary>

    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <remarks>
    /// SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in little-endian
    /// format.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool WriteS32LE(IOStream dst, int value) {
        bool result = SDL_WriteS32LE(dst.Handle, value);
        if (!result) {
            throw new IOException($"WriteS32LE failed: {GetError()}");
=======
    public static bool WriteS32LE(IOStream dst, int value) {
        bool result = SDL_WriteS32LE(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteS32LE failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to write 64 bits in native format to an SDL_IOStream as big-endian data.</summary>

    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <remarks>
    /// SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in big-endian format.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool WriteS64BE(IOStream dst, long value) {
        bool result = SDL_WriteS64BE(dst.Handle, value);
        if (!result) {
            throw new IOException($"WriteS64BE failed: {GetError()}");
=======
    public static bool WriteS64BE(IOStream dst, long value) {
        bool result = SDL_WriteS64BE(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteS64BE failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to write 64 bits in native format to an SDL_IOStream as little-endian data.</summary>

    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <remarks>
    /// SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in little-endian
    /// format.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool WriteS64LE(IOStream dst, long value) {
        bool result = SDL_WriteS64LE(dst.Handle, value);
        if (!result) {
            throw new IOException($"WriteS64LE failed: {GetError()}");
=======
    public static bool WriteS64LE(IOStream dst, long value) {
        bool result = SDL_WriteS64LE(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteS64LE failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to write a signed byte to an SDL_IOStream.</summary>

    /// <param name="dst">the SDL_IOStream to write to.</param>
    /// <param name="value">the byte value to write.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool WriteS8(IOStream dst, sbyte value) {
        bool result = SDL_WriteS8(dst.Handle, value);
        if (!result) {
            throw new IOException($"WriteS8 failed: {GetError()}");
=======
    public static bool WriteS8(IOStream dst, sbyte value) {
        bool result = SDL_WriteS8(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteS8 failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to write 16 bits in native format to an SDL_IOStream as big-endian data.</summary>

    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <remarks>
    /// SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in big-endian format.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool WriteU16BE(IOStream dst, ushort value) {
        bool result = SDL_WriteU16BE(dst.Handle, value);
        if (!result) {
            throw new IOException($"WriteU16BE failed: {GetError()}");
=======
    public static bool WriteU16BE(IOStream dst, ushort value) {
        bool result = SDL_WriteU16BE(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteU16BE failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to write 16 bits in native format to an SDL_IOStream as little-endian data.</summary>

    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <remarks>
    /// SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in little-endian
    /// format.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool WriteU16LE(IOStream dst, ushort value) {
        bool result = SDL_WriteU16LE(dst.Handle, value);
        if (!result) {
            throw new IOException($"WriteU16LE failed: {GetError()}");
=======
    public static bool WriteU16LE(IOStream dst, ushort value) {
        bool result = SDL_WriteU16LE(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteU16LE failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to write 32 bits in native format to an SDL_IOStream as big-endian data.</summary>

    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <remarks>
    /// SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in big-endian format.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool WriteU32BE(IOStream dst, uint value) {
        bool result = SDL_WriteU32BE(dst.Handle, value);
        if (!result) {
            throw new IOException($"WriteU32BE failed: {GetError()}");
=======
    public static bool WriteU32BE(IOStream dst, uint value) {
        bool result = SDL_WriteU32BE(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteU32BE failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to write 32 bits in native format to an SDL_IOStream as little-endian data.</summary>

    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <remarks>
    /// SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in little-endian
    /// format.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool WriteU32LE(IOStream dst, uint value) {
        bool result = SDL_WriteU32LE(dst.Handle, value);
        if (!result) {
            throw new IOException($"WriteU32LE failed: {GetError()}");
=======
    public static bool WriteU32LE(IOStream dst, uint value) {
        bool result = SDL_WriteU32LE(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteU32LE failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to write 64 bits in native format to an SDL_IOStream as big-endian data.</summary>

    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <remarks>
    /// SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in big-endian format.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool WriteU64BE(IOStream dst, ulong value) {
        bool result = SDL_WriteU64BE(dst.Handle, value);
        if (!result) {
            throw new IOException($"WriteU64BE failed: {GetError()}");
=======
    public static bool WriteU64BE(IOStream dst, ulong value) {
        bool result = SDL_WriteU64BE(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteU64BE failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to write 64 bits in native format to an SDL_IOStream as little-endian data.</summary>

    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <remarks>
    /// SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in little-endian
    /// format.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool WriteU64LE(IOStream dst, ulong value) {
        bool result = SDL_WriteU64LE(dst.Handle, value);
        if (!result) {
            throw new IOException($"WriteU64LE failed: {GetError()}");
=======
    public static bool WriteU64LE(IOStream dst, ulong value) {
        bool result = SDL_WriteU64LE(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteU64LE failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

<<<<<<< HEAD
    /// <summary>Use this function to write a byte to an SDL_IOStream.</summary>

    /// <param name="dst">the SDL_IOStream to write to.</param>
    /// <param name="value">the byte value to write.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on successful write or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static bool WriteU8(IOStream dst, byte value) {
        bool result = SDL_WriteU8(dst.Handle, value);
        if (!result) {
            throw new IOException($"WriteU8 failed: {GetError()}");
=======
    public static bool WriteU8(IOStream dst, byte value) {
        bool result = SDL_WriteU8(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteU8 failed: {GetError()}");
>>>>>>> main
        }
        return result;
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CloseIO(nint context);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_FlushIO(nint context);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetIOProperties(nint context);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long SDL_GetIOSize(nint context);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IoStatus SDL_GetIOStatus(nint context);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_IOFromConstMem(nint mem, nuint size);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_IOFromDynamicMem();

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_IOFromFile(string file, string mode);
<<<<<<< HEAD

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_IOFromMem(nint mem, nuint size);

=======
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_IOFromMem(nint mem, nuint size);
>>>>>>> main
    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint SDL_IOprintf(nint context, string fmt);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_LoadFile(string file, out nuint datasize);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_LoadFile_IO(nint src, out nuint datasize, [MarshalAs(BoolType)] bool closeio);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenIO(ref IOStreamInterface iface, nint userdata);
<<<<<<< HEAD

=======
>>>>>>> main
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint SDL_ReadIO(nint context, nint ptr, nuint size);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadS16BE(nint src, out short value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadS16LE(nint src, out short value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadS32BE(nint src, out int value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadS32LE(nint src, out int value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadS64BE(nint src, out long value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadS64LE(nint src, out long value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadS8(nint src, out sbyte value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadU16BE(nint src, out ushort value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadU16LE(nint src, out ushort value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadU32BE(nint src, out uint value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadU32LE(nint src, out uint value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadU64BE(nint src, out ulong value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadU64LE(nint src, out ulong value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadU8(nint src, out byte value);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SaveFile(string file, nint data, nuint datasize);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_SaveFile_IO(nint src, nint data, nuint datasize, SdlBool closeio);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long SDL_SeekIO(nint context, long offset, IoWhence whence);
<<<<<<< HEAD

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long SDL_TellIO(nint context);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint SDL_WriteIO(nint context, nint ptr, nuint size);

=======
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long SDL_TellIO(nint context);
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint SDL_WriteIO(nint context, nint ptr, nuint size);
>>>>>>> main
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteS16BE(nint dst, short value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteS16LE(nint dst, short value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteS32BE(nint dst, int value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteS32LE(nint dst, int value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteS64BE(nint dst, long value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteS64LE(nint dst, long value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteS8(nint dst, sbyte value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteU16BE(nint dst, ushort value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteU16LE(nint dst, ushort value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteU32BE(nint dst, uint value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteU32LE(nint dst, uint value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteU64BE(nint dst, ulong value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteU64LE(nint dst, ulong value);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteU8(nint dst, byte value);
<<<<<<< HEAD
}
=======
}
>>>>>>> main
