using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuDepthStencilTargetInfo {
    public nint Texture;
    public float ClearDepth;
    public GpuLoadOp LoadOp;
    public GpuStoreOp StoreOp;
    public GpuLoadOp StencilLoadOp;
    public GpuStoreOp StencilStoreOp;
    public SdlBool Cycle;
    public byte ClearStencil;
    public byte Padding1;
    public byte Padding2;
}