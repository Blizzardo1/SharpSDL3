using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct Vertex {
    public FPoint Position;
    public FColor Color;
    public FPoint TexCoord;
}