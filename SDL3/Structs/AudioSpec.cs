using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct AudioSpec
{
	public AudioFormat Format;
	public int Channels;
	public int Freq;
}