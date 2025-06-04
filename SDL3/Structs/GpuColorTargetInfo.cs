using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuColorTargetInfo
{
	public nint Texture;
	public uint MipLevel;
	public uint LayerOrDepthPlane;
	public FColor ClearColor;
	public GpuLoadOp LoadOp;
	public GpuStoreOp StoreOp;
	public nint ResolveTexture;
	public uint ResolveMipLevel;
	public uint ResolveLayer;
	public SdlBool Cycle;
	public SdlBool CycleResolveTexture;
	public byte Padding1;
	public byte Padding2;
}