using System.Runtime.InteropServices;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct Locale
{
	public byte* Language;
	public byte* Country;
}