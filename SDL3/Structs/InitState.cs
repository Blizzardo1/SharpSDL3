using System.Runtime.InteropServices;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct InitState
{
	public AtomicInt Status;
	public ulong Thread;
	public nint Reserved;
}