using SharpSDL3.Structs;
using System.Runtime.InteropServices;

namespace SharpSDL3.Mixer;

[StructLayout(LayoutKind.Sequential)]
public struct Music {
    public nint Interface;
    public nint Context;
    public SdlBool Playing;
    public Fading Fading;
    public int FadeStep;
    public int FadeSteps;
    [MarshalAs(Sdl.StringType)] public string Filename;
}