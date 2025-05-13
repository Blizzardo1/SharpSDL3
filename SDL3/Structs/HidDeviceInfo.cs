using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct HidDeviceInfo
{
	public byte* Path;
	public ushort VendorId;
	public ushort ProductId;
	public byte* SerialNumber;
	public ushort ReleaseNumber;
	public byte* ManufacturerString;
	public byte* ProductString;
	public ushort UsagePage;
	public ushort Usage;
	public int InterfaceNumber;
	public int InterfaceClass;
	public int InterfaceSubclass;
	public int InterfaceProtocol;
	public HidBusType BusType;
	public HidDeviceInfo* Next;
}