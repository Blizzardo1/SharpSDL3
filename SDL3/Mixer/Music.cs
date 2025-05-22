using System.Runtime.InteropServices;

namespace SharpSDL3.Mixer;

[StructLayout(LayoutKind.Sequential)]
public struct Music {
    public nint Interface;
    public nint Context;
    [MarshalAs(Sdl.BoolType)] public bool Playing;
    public Fading Fading;
    public int FadeStep;
    public int FadeSteps;
    [MarshalAs(Sdl.StringType)] public string Filename;
}
