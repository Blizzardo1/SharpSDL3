using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuTextureLocation {
    public nint Texture;
    public uint MipLevel;
    public uint Layer;
    public uint X;
    public uint Y;
    public uint Z;
}