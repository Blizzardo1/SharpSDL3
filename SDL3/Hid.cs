using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSDL3;

public static unsafe partial class Sdl {

    public static void BleScan(SdlBool active) {
        SDL_hid_ble_scan(active);
        LogInfo(LogCategory.System, $"HID BLE scan set to {active}");
    }

    public static int Close(nint dev) {
        var result = SDL_hid_close(dev);
        if (result < 0) {
            LogError(LogCategory.Error, $"Failed to close HID device: {GetError()}");
        }
        return result;
    }

    public static uint DeviceChangeCount() {
        var result = SDL_hid_device_change_count();
        if (result == 0) {
            LogError(LogCategory.Error, $"Failed to get HID device change count: {GetError()}");
        }
        return result;
    }

    public static Span<HidDeviceInfo> Enumerate(ushort vendorId, ushort productId) {
        nint result = SDL_hid_enumerate(vendorId, productId);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, $"Failed to enumerate HID devices: {GetError()}");
            return [];
        }

        List<HidDeviceInfo> devices = [];

        // Might break things
        HidDeviceInfo current = Marshal.PtrToStructure<HidDeviceInfo>(result);

        while (current.Next != nint.Zero) {
            devices.Add(current);
            current = Marshal.PtrToStructure<HidDeviceInfo>(current.Next);
        }

        return devices.ToArray();
    }

    public static int Exit() {
        var result = SDL_hid_exit();
        if (result != 0) {
            LogError(LogCategory.Error, $"Failed to exit HID API: {GetError()}");
        }
        return result;
    }

    public static void FreeEnumeration(nint devs) {
        if (devs == IntPtr.Zero) {
            LogWarn(LogCategory.System, "Attempted to free a null HID enumeration pointer.");
            return;
        }

        SDL_hid_free_enumeration(devs);
        LogInfo(LogCategory.System, "Successfully freed HID enumeration resources.");
    }

    public static Span<HidDeviceInfo> GetDeviceInfo(nint dev) {
        nint result = SDL_hid_get_device_info(dev);
        if (result == nint.Zero) {
            LogError(LogCategory.Error, $"Failed to get HID device info: {GetError()}");
            return [];
        }

        List<HidDeviceInfo> devices = [];

        // Might break things
        HidDeviceInfo current = Marshal.PtrToStructure<HidDeviceInfo>(result);

        while (current.Next != nint.Zero) {
            devices.Add(current);
            current = Marshal.PtrToStructure<HidDeviceInfo>(current.Next);
        }

        return devices.ToArray();
    }

    public static int GetFeatureReport(nint dev, nint data, nuint length) {
        var result = SDL_hid_get_feature_report(dev, data, length);
        if (result < 0) {
            LogError(LogCategory.Error, $"Failed to get feature report from HID device: {GetError()}");
        }
        return result;
    }

    public static int GetIndexedString(nint dev, int stringIndex, string @string, nuint maxlen) {
        var result = SDL_hid_get_indexed_string(dev, stringIndex, @string, maxlen);
        if (result < 0) {
            LogError(LogCategory.Error, $"Failed to get indexed string from HID device: {GetError()}");
        }
        return result;
    }

    public static int GetInputReport(nint dev, nint data, nuint length) {
        var result = SDL_hid_get_input_report(dev, data, length);
        if (result < 0) {
            LogError(LogCategory.Error, $"Failed to get input report from HID device: {GetError()}");
        }
        return result;
    }

    public static int GetManufacturerString(nint dev, string @string, nuint maxlen) {
        var result = SDL_hid_get_manufacturer_string(dev, @string, maxlen);
        if (result < 0) {
            LogError(LogCategory.Error, $"Failed to get manufacturer string from HID device: {GetError()}");
        }
        return result;
    }

    public static int GetProductString(nint dev, string @string, nuint maxlen) {
        var result = SDL_hid_get_product_string(dev, @string, maxlen);
        if (result < 0) {
            LogError(LogCategory.Error, $"Failed to get product string from HID device: {GetError()}");
        }
        return result;
    }

    public static int GetReportDescriptor(nint dev, nint buf, nuint bufSize) {
        var result = SDL_hid_get_report_descriptor(dev, buf, bufSize);
        if (result < 0) {
            LogError(LogCategory.Error, $"Failed to get report descriptor from HID device: {GetError()}");
        }
        return result;
    }

    public static int GetSerialNumberString(nint dev, string @string, nuint maxlen) {
        var result = SDL_hid_get_serial_number_string(dev, @string, maxlen);
        if (result < 0) {
            LogError(LogCategory.Error, $"Failed to get serial number string from HID device: {GetError()}");
        }
        return result;
    }

    public static int Init() {
        var result = SDL_hid_init();
        if (result != 0) {
            LogError(LogCategory.Error, $"Failed to initialize HID API: {GetError()}");
        }
        return result;
    }

    public static nint Open(ushort vendorId, ushort productId, string serialNumber) {
        nint result = SDL_hid_open(vendorId, productId, serialNumber);
        if (result == IntPtr.Zero) {
            LogError(LogCategory.Error, $"Failed to open HID device: {GetError()}");
        }
        return result;
    }

    public static nint OpenPath(string path) {
        nint result = SDL_hid_open_path(path);
        if (result == IntPtr.Zero) {
            LogError(LogCategory.Error, $"Failed to open HID device at path {path}: {GetError()}");
        }
        return result;
    }

    public static int Read(nint dev, nint data, nuint length) {
        var result = SDL_hid_read(dev, data, length);
        if (result < 0) {
            LogError(LogCategory.Error, $"Failed to read from HID device: {GetError()}");
        }
        return result;
    }

    public static int ReadTimeout(nint dev, nint data, nuint length, int milliseconds) {
        var result = SDL_hid_read_timeout(dev, data, length, milliseconds);
        if (result < 0) {
            LogError(LogCategory.Error, $"Failed to read from HID device: {GetError()}");
        }
        return result;
    }

    public static int SendFeatureReport(nint dev, nint data, nuint length) {
        var result = SDL_hid_send_feature_report(dev, data, length);
        if (result < 0) {
            LogError(LogCategory.Error, $"Failed to send feature report to HID device: {GetError()}");
        }
        return result;
    }

    public static int SetNonblocking(nint dev, int nonblock) {
        var result = SDL_hid_set_nonblocking(dev, nonblock);
        if (result < 0) {
            LogError(LogCategory.Error, $"Failed to set non-blocking mode for HID device: {GetError()}");
        }
        return result;
    }

    public static int Write(nint dev, nint data, nuint length) {
        var result = SDL_hid_write(dev, data, length);
        if (result < 0) {
            LogError(LogCategory.Error, $"Failed to write to HID device: {GetError()}");
        }
        return result;
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_hid_ble_scan(SdlBool active);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_close(nint dev);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_hid_device_change_count();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_hid_enumerate(ushort vendorId, ushort productId);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_exit();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_hid_free_enumeration(nint devs);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_hid_get_device_info(nint dev);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int
        SDL_hid_get_feature_report(nint dev, nint data, nuint length);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_indexed_string(nint dev, int stringIndex, string @string, nuint maxlen);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int
        SDL_hid_get_input_report(nint dev, nint data, nuint length);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_manufacturer_string(nint dev, string @string, nuint maxlen);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_product_string(nint dev, string @string, nuint maxlen);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int
        SDL_hid_get_report_descriptor(nint dev, nint buf, nuint bufSize);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_serial_number_string(nint dev, string @string, nuint maxlen);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_init();

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_hid_open(ushort vendorId, ushort productId, string serialNumber);

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_hid_open_path(string path);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_read(nint dev, nint data, nuint length);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int
        SDL_hid_read_timeout(nint dev, nint data, nuint length,
            int milliseconds);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int
        SDL_hid_send_feature_report(nint dev, nint data, nuint length);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_set_nonblocking(nint dev, int nonblock);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_write(nint dev, nint data, nuint length);
}