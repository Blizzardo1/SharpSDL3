using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static SharpSDL3.Sdl;

namespace SharpSDL3; 
public static unsafe partial class Hid {
    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_init();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_exit();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_hid_device_change_count();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial HidDeviceInfo* SDL_hid_enumerate(ushort vendorId, ushort productId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_hid_free_enumeration(nint devs); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_hid_open(ushort vendorId, ushort productId, string serialNumber);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_hid_open_path(string path);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_write(nint dev, nint data, nuint length); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int
        SDL_hid_read_timeout(nint dev, nint data, nuint length,
            int milliseconds); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_read(nint dev, nint data, nuint length); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_set_nonblocking(nint dev, int nonblock);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int
        SDL_hid_send_feature_report(nint dev, nint data, nuint length); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int
        SDL_hid_get_feature_report(nint dev, nint data, nuint length); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int
        SDL_hid_get_input_report(nint dev, nint data, nuint length); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_close(nint dev);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_manufacturer_string(nint dev, string @string, nuint maxlen);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_product_string(nint dev, string @string, nuint maxlen);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_serial_number_string(nint dev, string @string, nuint maxlen);

    [LibraryImport(NativeLibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_indexed_string(nint dev, int stringIndex, string @string, nuint maxlen);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial HidDeviceInfo* SDL_hid_get_device_info(nint dev);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int
        SDL_hid_get_report_descriptor(nint dev, nint buf, nuint bufSize); // WARN_UNKNOWN_POINTER_PARAMETER

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_hid_ble_scan(SdlBool active);
}
