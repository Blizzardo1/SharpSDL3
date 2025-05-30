﻿using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static SharpSDL3.Sdl;

namespace SharpSDL3; 
public static partial class Sdl {

    public static bool CloseIO(IOStream context) {
        bool result = SDL_CloseIO(context.Handle);
        if (!result) {
            throw new IOException($"SDL_CloseIO failed: {GetError()}");
        }
        return result;
    }

    public static bool FlushIO(IOStream context) {
        bool result = SDL_FlushIO(context.Handle);
        if (!result) {
            throw new IOException($"SDL_FlushIO failed: {GetError()}");
        }
        return result;
    }

    public static uint GetIOProperties(IOStream context) {
        uint result = SDL_GetIOProperties(context.Handle);
        if (result == 0) {
            throw new IOException($"SDL_GetIOProperties failed: {GetError()}");
        }
        return result;
    }

    public static long GetIOSize(IOStream context) {
        long result = SDL_GetIOSize(context.Handle);
        if (result == 0) {
            throw new IOException($"SDL_GetIOSize failed: {GetError()}");
        }
        return result;
    }

    public static IoStatus GetIOStatus(IOStream context) {
        IoStatus result = SDL_GetIOStatus(context.Handle);
        if (result == 0) {
            throw new IOException($"SDL_GetIOStatus failed: {GetError()}");
        }
        return result;
    }

    public static IOStream IOFromConstMem(nint mem, nuint size) {
        nint result = SDL_IOFromConstMem(mem, size);
        if (result == nint.Zero) {
            throw new IOException($"SDL_IOFromConstMem failed: {GetError()}");
        }

        IOStream stream = Marshal.PtrToStructure<IOStream>(result)!;
        stream.Handle = result;

        return stream;
    }

    public static IOStream IOFromDynamicMem() {
        nint result = SDL_IOFromDynamicMem();
        if (result == nint.Zero) {
            throw new IOException($"SDL_IOFromDynamicMem failed: {GetError()}");
        }

        IOStream stream = Marshal.PtrToStructure<IOStream>(result)!;
        stream.Handle = result;

        return stream;
    }

    public static IOStream IOFromFile(string file, string mode) {
        nint result = SDL_IOFromFile(file, mode);
        if (result == nint.Zero) {
            throw new IOException($"SDL_IOFromFile failed: {GetError()}");
        }

        IOStream stream = Marshal.PtrToStructure<IOStream>(result)!;
        stream.Handle = result;
        return stream;
    }

    public static IOStream IOFromMem(nint mem, nuint size) {
        nint result = SDL_IOFromMem(mem, size);
        if (result == nint.Zero) {
            throw new IOException($"SDL_IOFromMem failed: {GetError()}");
        }

        IOStream stream = Marshal.PtrToStructure<IOStream>(result)!;
        stream.Handle = result;

        return stream;
    }

    public static nuint IOprintf(IOStream context, string fmt) {
        nuint result = SDL_IOprintf(context.Handle, fmt);
        if (result == 0) {
            throw new IOException($"SDL_IOprintf failed: {GetError()}");
        }
        return result;
    }

    public static nint LoadFile(string file, out nuint datasize) {
        // Initialize the variable to avoid CS0165
        nint result = SDL_LoadFile(file, out nuint ds);
        if (result == 0) {
            throw new IOException($"SDL_LoadFile failed: {GetError()}");
        }
        datasize = ds;
        return result;
    }

    public static nint LoadFileIO(IOStream src, out nuint datasize, bool closeio) {
        // Initialize the variable to avoid CS0165
        nint result = SDL_LoadFile_IO(src.Handle, out nuint ds, closeio);
        if (result == 0) {
            throw new IOException($"SDL_LoadFile_IO failed: {GetError()}");
        }

        datasize = ds;
        return result;
    }

    public static IOStream OpenIO(ref IOStreamInterface iface, nint userdata) {
        nint result = SDL_OpenIO(ref iface, userdata);
        if (result == 0) {
            throw new IOException($"SDL_OpenIO failed: {GetError()}");
        }

        IOStream stream = Marshal.PtrToStructure<IOStream>(result)!;
        stream.Handle = result;
        stream.Interface = iface;

        return stream;
    }

    public static nuint ReadIO(IOStream context, nint ptr, nuint size) {
        nuint result = SDL_ReadIO(context.Handle, ptr, size);
        if (result == 0) {
            throw new IOException($"SDL_ReadIO failed: {GetError()}");
        }
        return result;
    }

    public static bool ReadS16BE(IOStream src, out short value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadS16BE(src.Handle, out short v);
        if (!result) {
            throw new IOException($"SDL_ReadS16BE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static bool ReadS16LE(IOStream src, out short value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadS16LE(src.Handle, out short v);
        if (!result) {
            throw new IOException($"SDL_ReadS16LE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static bool ReadS32BE(IOStream src, out int value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadS32BE(src.Handle, out int v);
        if (!result) {
            throw new IOException($"SDL_ReadS32BE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static bool ReadS32LE(IOStream src, out int value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadS32LE(src.Handle, out int v);
        if (!result) {
            throw new IOException($"SDL_ReadS32LE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static bool ReadS64BE(IOStream src, out long value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadS64BE(src.Handle, out long v);
        if (!result) {
            throw new IOException($"SDL_ReadS64BE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static bool ReadS64LE(IOStream src, out long value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadS64LE(src.Handle, out long v);
        if (!result) {
            throw new IOException($"SDL_ReadS64LE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static bool ReadS8(IOStream src, out sbyte value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadS8(src.Handle, out sbyte v);
        if (!result) {
            throw new IOException($"SDL_ReadS8 failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static bool ReadU16BE(IOStream src, out ushort value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadU16BE(src.Handle, out ushort v);
        if (!result) {
            throw new IOException($"SDL_ReadU16BE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static bool ReadU16LE(IOStream src, out ushort value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadU16LE(src.Handle, out ushort v);
        if (!result) {
            throw new IOException($"SDL_ReadU16LE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static bool ReadU32BE(IOStream src, out uint value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadU32BE(src.Handle, out uint v);
        if (!result) {
            throw new IOException($"SDL_ReadU32BE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static bool ReadU32LE(IOStream src, out uint value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadU32LE(src.Handle, out uint v);
        if (!result) {
            throw new IOException($"SDL_ReadU32LE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static bool ReadU64BE(IOStream src, out ulong value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadU64BE(src.Handle, out ulong v);
        if (!result) {
            throw new IOException($"SDL_ReadU64BE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static bool ReadU64LE(IOStream src, out ulong value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadU64LE(src.Handle, out ulong v);
        if (!result) {
            throw new IOException($"SDL_ReadU64LE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static bool ReadU8(IOStream src, out byte value) {
        // Initialize the variable to avoid CS0165
        bool result = SDL_ReadU8(src.Handle, out byte v);
        if (!result) {
            throw new IOException($"SDL_ReadU8 failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static bool SaveFile(string file, nint data, nuint datasize) {
        bool result = SDL_SaveFile(file, data, datasize);
        if (!result) {
            throw new IOException($"SDL_SaveFile failed: {GetError()}");
        }
        return result;
    }

    public static bool SaveFileIO(nint src, nint data, nuint datasize, bool closeio) {
        bool result = SDL_SaveFile_IO(src, data, datasize, closeio);
        if (!result) {
            throw new IOException($"SDL_SaveFile_IO failed: {GetError()}");
        }
        return result;
    }

    public static long SeekIO(IOStream context, long offset, IoWhence whence) {
        long result = SDL_SeekIO(context.Handle, offset, whence);
        if (result == 0) {
            throw new IOException($"SDL_SeekIO failed: {GetError()}");
        }
        return result;
    }

    public static long TellIO(IOStream context) {
        long result = SDL_TellIO(context.Handle);
        if (result == 0) {
            throw new IOException($"SDL_TellIO failed: {GetError()}");
        }
        return result;
    }

    public static nuint WriteIO(IOStream context, nint ptr, nuint size) {
        nuint result = SDL_WriteIO(context.Handle, ptr, size);
        if (result == 0) {
            throw new IOException($"SDL_WriteIO failed: {GetError()}");
        }
        return result;
    }

    public static bool WriteS16BE(IOStream dst, short value) {
        bool result = SDL_WriteS16BE(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteS16BE failed: {GetError()}");
        }
        return result;
    }

    public static bool WriteS16LE(IOStream dst, short value) {
        bool result = SDL_WriteS16LE(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteS16LE failed: {GetError()}");
        }
        return result;
    }

    public static bool WriteS32BE(IOStream dst, int value) {
        bool result = SDL_WriteS32BE(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteS32BE failed: {GetError()}");
        }
        return result;
    }

    public static bool WriteS32LE(IOStream dst, int value) {
        bool result = SDL_WriteS32LE(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteS32LE failed: {GetError()}");
        }
        return result;
    }

    public static bool WriteS64BE(IOStream dst, long value) {
        bool result = SDL_WriteS64BE(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteS64BE failed: {GetError()}");
        }
        return result;
    }

    public static bool WriteS64LE(IOStream dst, long value) {
        bool result = SDL_WriteS64LE(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteS64LE failed: {GetError()}");
        }
        return result;
    }

    public static bool WriteS8(IOStream dst, sbyte value) {
        bool result = SDL_WriteS8(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteS8 failed: {GetError()}");
        }
        return result;
    }

    public static bool WriteU16BE(IOStream dst, ushort value) {
        bool result = SDL_WriteU16BE(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteU16BE failed: {GetError()}");
        }
        return result;
    }

    public static bool WriteU16LE(IOStream dst, ushort value) {
        bool result = SDL_WriteU16LE(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteU16LE failed: {GetError()}");
        }
        return result;
    }

    public static bool WriteU32BE(IOStream dst, uint value) {
        bool result = SDL_WriteU32BE(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteU32BE failed: {GetError()}");
        }
        return result;
    }

    public static bool WriteU32LE(IOStream dst, uint value) {
        bool result = SDL_WriteU32LE(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteU32LE failed: {GetError()}");
        }
        return result;
    }

    public static bool WriteU64BE(IOStream dst, ulong value) {
        bool result = SDL_WriteU64BE(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteU64BE failed: {GetError()}");
        }
        return result;
    }

    public static bool WriteU64LE(IOStream dst, ulong value) {
        bool result = SDL_WriteU64LE(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteU64LE failed: {GetError()}");
        }
        return result;
    }

    public static bool WriteU8(IOStream dst, byte value) {
        bool result = SDL_WriteU8(dst.Handle, value);
        if (!result) {
            throw new IOException($"SDL_WriteU8 failed: {GetError()}");
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
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_IOFromMem(nint mem, nuint size);
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
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long SDL_TellIO(nint context);
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint SDL_WriteIO(nint context, nint ptr, nuint size);
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
}
