using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct AsyncIoOutcome
{
	public nint AsyncIo;
	public AsyncIoTaskType Type;
	public AsyncIoResult Result;
	public nint Buffer;
	public ulong Offset;
	public ulong BytesRequested;
	public ulong BytesTransferred;
	public nint UserData;
}