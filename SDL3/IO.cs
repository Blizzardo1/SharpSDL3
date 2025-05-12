using SDL3.Enums;
using SDL3.Structs;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static SDL3.Sdl;

namespace SDL3; 
public static partial class IO {

    public static SdlBool CloseIO(nint context) {
        SdlBool result = SDL_CloseIO(context);
        if (!result) {
            throw new IOException($"SDL_CloseIO failed: {GetError()}");
        }
        return result;
    }

    public static SdlBool FlushIO(nint context) {
        SdlBool result = SDL_FlushIO(context);
        if (!result) {
            throw new IOException($"SDL_FlushIO failed: {GetError()}");
        }
        return result;
    }

    public static uint GetIOProperties(nint context) {
        uint result = SDL_GetIOProperties(context);
        if (result == 0) {
            throw new IOException($"SDL_GetIOProperties failed: {GetError()}");
        }
        return result;
    }

    public static long GetIOSize(nint context) {
        long result = SDL_GetIOSize(context);
        if (result == 0) {
            throw new IOException($"SDL_GetIOSize failed: {GetError()}");
        }
        return result;
    }

    public static IoStatus GetIOStatus(nint context) {
        IoStatus result = SDL_GetIOStatus(context);
        if (result == 0) {
            throw new IOException($"SDL_GetIOStatus failed: {GetError()}");
        }
        return result;
    }

    public static nint IOFromConstMem(nint mem, nuint size) {
        nint result = SDL_IOFromConstMem(mem, size);
        if (result == 0) {
            throw new IOException($"SDL_IOFromConstMem failed: {GetError()}");
        }
        return result;
    }

    public static nint IOFromDynamicMem() {
        nint result = SDL_IOFromDynamicMem();
        if (result == 0) {
            throw new IOException($"SDL_IOFromDynamicMem failed: {GetError()}");
        }
        return result;
    }

    public static nint IOFromFile(string file, string mode) {
        nint result = SDL_IOFromFile(file, mode);
        if (result == 0) {
            throw new IOException($"SDL_IOFromFile failed: {GetError()}");
        }
        return result;
    }

    public static nint IOFromMem(nint mem, nuint size) {
        nint result = SDL_IOFromMem(mem, size);
        if (result == 0) {
            throw new IOException($"SDL_IOFromMem failed: {GetError()}");
        }
        return result;
    }

    public static nuint IOprintf(nint context, string fmt) {
        nuint result = SDL_IOprintf(context, fmt);
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

    public static nint LoadFileIO(nint src, out nuint datasize, SdlBool closeio) {
        // Initialize the variable to avoid CS0165
        nint result = SDL_LoadFile_IO(src, out nuint ds, closeio);
        if (result == 0) {
            throw new IOException($"SDL_LoadFile_IO failed: {GetError()}");
        }

        datasize = ds;
        return result;
    }

    public static nint OpenIO(ref IoStreamInterface iface, nint userdata) {
        nint result = SDL_OpenIO(ref iface, userdata);
        if (result == 0) {
            throw new IOException($"SDL_OpenIO failed: {GetError()}");
        }
        return result;
    }

    public static nuint ReadIO(nint context, nint ptr, nuint size) {
        nuint result = SDL_ReadIO(context, ptr, size);
        if (result == 0) {
            throw new IOException($"SDL_ReadIO failed: {GetError()}");
        }
        return result;
    }

    public static SdlBool ReadS16BE(nint src, out short value) {
        // Initialize the variable to avoid CS0165
        SdlBool result = SDL_ReadS16BE(src, out short v);
        if (!result) {
            throw new IOException($"SDL_ReadS16BE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static SdlBool ReadS16LE(nint src, out short value) {
        // Initialize the variable to avoid CS0165
        SdlBool result = SDL_ReadS16LE(src, out short v);
        if (!result) {
            throw new IOException($"SDL_ReadS16LE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static SdlBool ReadS32BE(nint src, out int value) {
        // Initialize the variable to avoid CS0165
        SdlBool result = SDL_ReadS32BE(src, out int v);
        if (!result) {
            throw new IOException($"SDL_ReadS32BE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static SdlBool ReadS32LE(nint src, out int value) {
        // Initialize the variable to avoid CS0165
        SdlBool result = SDL_ReadS32LE(src, out int v);
        if (!result) {
            throw new IOException($"SDL_ReadS32LE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static SdlBool ReadS64BE(nint src, out long value) {
        // Initialize the variable to avoid CS0165
        SdlBool result = SDL_ReadS64BE(src, out long v);
        if (!result) {
            throw new IOException($"SDL_ReadS64BE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static SdlBool ReadS64LE(nint src, out long value) {
        // Initialize the variable to avoid CS0165
        SdlBool result = SDL_ReadS64LE(src, out long v);
        if (!result) {
            throw new IOException($"SDL_ReadS64LE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static SdlBool ReadS8(nint src, out sbyte value) {
        // Initialize the variable to avoid CS0165
        SdlBool result = SDL_ReadS8(src, out sbyte v);
        if (!result) {
            throw new IOException($"SDL_ReadS8 failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static SdlBool ReadU16BE(nint src, out ushort value) {
        // Initialize the variable to avoid CS0165
        SdlBool result = SDL_ReadU16BE(src, out ushort v);
        if (!result) {
            throw new IOException($"SDL_ReadU16BE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static SdlBool ReadU16LE(nint src, out ushort value) {
        // Initialize the variable to avoid CS0165
        SdlBool result = SDL_ReadU16LE(src, out ushort v);
        if (!result) {
            throw new IOException($"SDL_ReadU16LE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static SdlBool ReadU32BE(nint src, out uint value) {
        // Initialize the variable to avoid CS0165
        SdlBool result = SDL_ReadU32BE(src, out uint v);
        if (!result) {
            throw new IOException($"SDL_ReadU32BE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static SdlBool ReadU32LE(nint src, out uint value) {
        // Initialize the variable to avoid CS0165
        SdlBool result = SDL_ReadU32LE(src, out uint v);
        if (!result) {
            throw new IOException($"SDL_ReadU32LE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static SdlBool ReadU64BE(nint src, out ulong value) {
        // Initialize the variable to avoid CS0165
        SdlBool result = SDL_ReadU64BE(src, out ulong v);
        if (!result) {
            throw new IOException($"SDL_ReadU64BE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static SdlBool ReadU64LE(nint src, out ulong value) {
        // Initialize the variable to avoid CS0165
        SdlBool result = SDL_ReadU64LE(src, out ulong v);
        if (!result) {
            throw new IOException($"SDL_ReadU64LE failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static SdlBool ReadU8(nint src, out byte value) {
        // Initialize the variable to avoid CS0165
        SdlBool result = SDL_ReadU8(src, out byte v);
        if (!result) {
            throw new IOException($"SDL_ReadU8 failed: {GetError()}");
        }
        value = v;
        return result;
    }

    public static SdlBool SaveFile(string file, nint data, nuint datasize) {
        SdlBool result = SDL_SaveFile(file, data, datasize);
        if (!result) {
            throw new IOException($"SDL_SaveFile failed: {GetError()}");
        }
        return result;
    }

    public static SdlBool SaveFileIO(nint src, nint data, nuint datasize, SdlBool closeio) {
        SdlBool result = SDL_SaveFile_IO(src, data, datasize, closeio);
        if (!result) {
            throw new IOException($"SDL_SaveFile_IO failed: {GetError()}");
        }
        return result;
    }

    public static long SeekIO(nint context, long offset, IoWhence whence) {
        long result = SDL_SeekIO(context, offset, whence);
        if (result == 0) {
            throw new IOException($"SDL_SeekIO failed: {GetError()}");
        }
        return result;
    }

    public static long TellIO(nint context) {
        long result = SDL_TellIO(context);
        if (result == 0) {
            throw new IOException($"SDL_TellIO failed: {GetError()}");
        }
        return result;
    }

    public static nuint WriteIO(nint context, nint ptr, nuint size) {
        nuint result = SDL_WriteIO(context, ptr, size);
        if (result == 0) {
            throw new IOException($"SDL_WriteIO failed: {GetError()}");
        }
        return result;
    }

    public static SdlBool WriteS16BE(nint dst, short value) {
        SdlBool result = SDL_WriteS16BE(dst, value);
        if (!result) {
            throw new IOException($"SDL_WriteS16BE failed: {GetError()}");
        }
        return result;
    }

    public static SdlBool WriteS16LE(nint dst, short value) {
        SdlBool result = SDL_WriteS16LE(dst, value);
        if (!result) {
            throw new IOException($"SDL_WriteS16LE failed: {GetError()}");
        }
        return result;
    }

    public static SdlBool WriteS32BE(nint dst, int value) {
        SdlBool result = SDL_WriteS32BE(dst, value);
        if (!result) {
            throw new IOException($"SDL_WriteS32BE failed: {GetError()}");
        }
        return result;
    }

    public static SdlBool WriteS32LE(nint dst, int value) {
        SdlBool result = SDL_WriteS32LE(dst, value);
        if (!result) {
            throw new IOException($"SDL_WriteS32LE failed: {GetError()}");
        }
        return result;
    }

    public static SdlBool WriteS64BE(nint dst, long value) {
        SdlBool result = SDL_WriteS64BE(dst, value);
        if (!result) {
            throw new IOException($"SDL_WriteS64BE failed: {GetError()}");
        }
        return result;
    }

    public static SdlBool WriteS64LE(nint dst, long value) {
        SdlBool result = SDL_WriteS64LE(dst, value);
        if (!result) {
            throw new IOException($"SDL_WriteS64LE failed: {GetError()}");
        }
        return result;
    }

    public static SdlBool WriteS8(nint dst, sbyte value) {
        SdlBool result = SDL_WriteS8(dst, value);
        if (!result) {
            throw new IOException($"SDL_WriteS8 failed: {GetError()}");
        }
        return result;
    }

    public static SdlBool WriteU16BE(nint dst, ushort value) {
        SdlBool result = SDL_WriteU16BE(dst, value);
        if (!result) {
            throw new IOException($"SDL_WriteU16BE failed: {GetError()}");
        }
        return result;
    }

    public static SdlBool WriteU16LE(nint dst, ushort value) {
        SdlBool result = SDL_WriteU16LE(dst, value);
        if (!result) {
            throw new IOException($"SDL_WriteU16LE failed: {GetError()}");
        }
        return result;
    }

    public static SdlBool WriteU32BE(nint dst, uint value) {
        SdlBool result = SDL_WriteU32BE(dst, value);
        if (!result) {
            throw new IOException($"SDL_WriteU32BE failed: {GetError()}");
        }
        return result;
    }

    public static SdlBool WriteU32LE(nint dst, uint value) {
        SdlBool result = SDL_WriteU32LE(dst, value);
        if (!result) {
            throw new IOException($"SDL_WriteU32LE failed: {GetError()}");
        }
        return result;
    }

    public static SdlBool WriteU64BE(nint dst, ulong value) {
        SdlBool result = SDL_WriteU64BE(dst, value);
        if (!result) {
            throw new IOException($"SDL_WriteU64BE failed: {GetError()}");
        }
        return result;
    }

    public static SdlBool WriteU64LE(nint dst, ulong value) {
        SdlBool result = SDL_WriteU64LE(dst, value);
        if (!result) {
            throw new IOException($"SDL_WriteU64LE failed: {GetError()}");
        }
        return result;
    }

    public static SdlBool WriteU8(nint dst, byte value) {
        SdlBool result = SDL_WriteU8(dst, value);
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

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_IOFromFile(string file, string mode);
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_IOFromMem(nint mem, nuint size);
    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint SDL_IOprintf(nint context, string fmt);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_LoadFile(string file, out nuint datasize);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_LoadFile_IO(nint src, out nuint datasize, SdlBool closeio);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_OpenIO(ref IoStreamInterface iface, nint userdata);
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

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
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
