using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct StorageInterface
{
	public uint Version;
	public nint Close; // WARN_ANONYMOUS_FUNCTION_POINTER
	public nint Ready; // WARN_ANONYMOUS_FUNCTION_POINTER
	public nint Enumerate; // WARN_ANONYMOUS_FUNCTION_POINTER
	public nint Info; // WARN_ANONYMOUS_FUNCTION_POINTER
	public nint ReadFile; // WARN_ANONYMOUS_FUNCTION_POINTER
	public nint WriteFile; // WARN_ANONYMOUS_FUNCTION_POINTER
	public nint MkDir; // WARN_ANONYMOUS_FUNCTION_POINTER
	public nint Remove; // WARN_ANONYMOUS_FUNCTION_POINTER
	public nint Rename; // WARN_ANONYMOUS_FUNCTION_POINTER
	public nint Copy; // WARN_ANONYMOUS_FUNCTION_POINTER
	public nint SpaceRemaining; // WARN_ANONYMOUS_FUNCTION_POINTER
}