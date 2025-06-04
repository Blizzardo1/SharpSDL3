using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct IoStreamInterface {
    public uint Version;
    public nint Size; // WARN_ANONYMOUS_FUNCTION_POINTER
    public nint Seek; // WARN_ANONYMOUS_FUNCTION_POINTER
    public nint Read; // WARN_ANONYMOUS_FUNCTION_POINTER
    public nint Write; // WARN_ANONYMOUS_FUNCTION_POINTER
    public nint Flush; // WARN_ANONYMOUS_FUNCTION_POINTER
    public nint Close; // WARN_ANONYMOUS_FUNCTION_POINTER
}