<<<<<<< HEAD
using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuMultisampleState {
    public GpuSampleCount SampleCount;
    public uint SampleMask;
    public SdlBool EnableMask;
    public byte Padding1;
    public byte Padding2;
    public byte Padding3;
=======
using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuMultisampleState
{
	public GpuSampleCount SampleCount;
	public uint SampleMask;
	public SdlBool EnableMask;
	public byte Padding1;
	public byte Padding2;
	public byte Padding3;
>>>>>>> main
}