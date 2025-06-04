using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct HidDeviceInfo {
    public nint Path;
    public ushort VendorId;
    public ushort ProductId;
    public nint SerialNumber;
    public ushort ReleaseNumber;
    public nint ManufacturerString;
    public nint ProductString;
    public ushort UsagePage;
    public ushort Usage;
    public int InterfaceNumber;
    public int InterfaceClass;
    public int InterfaceSubclass;
    public int InterfaceProtocol;
    public HidBusType BusType;
    public nint Next;
}