using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct GpuGraphicsPipelineCreateInfo {
    public nint VertexShader;
    public nint FragmentShader;
    public GpuVertexInputState VertexInputState;
    public GpuPrimitiveType PrimitiveType;
    public GpuRasterizerState RasterizerState;
    public GpuMultisampleState MultisampleState;
    public GpuDepthStencilState DepthStencilState;
    public GpuGraphicsPipelineTargetInfo TargetInfo;
    public uint Props;
}