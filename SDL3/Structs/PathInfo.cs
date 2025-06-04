using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct PathInfo {
    public PathType Type;
    public ulong Size;
    public long CreateTime;
    public long ModifyTime;
    public long AccessTime;
}