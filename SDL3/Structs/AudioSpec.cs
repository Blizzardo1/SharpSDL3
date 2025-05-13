using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct AudioSpec
{
	public AudioFormat Format;
	public int Channels;
	public int Freq;
}