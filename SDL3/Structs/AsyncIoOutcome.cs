<<<<<<< HEAD
using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct AsyncIoOutcome {
    public nint AsyncIo;
    public AsyncIoTaskType Type;
    public AsyncIoResult Result;
    public nint Buffer;
    public ulong Offset;
    public ulong BytesRequested;
    public ulong BytesTransferred;
    public nint UserData;
=======
using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

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
>>>>>>> main
}