using System.Runtime.InteropServices;

namespace SharpSDL3.Mixer;
[StructLayout(LayoutKind.Sequential)]
public unsafe struct Music {
        public MusicInterface* @interface;
        public void* Context;
        public bool Playing;
        public Fading Fading;
        public int FadeStep;
        public int FadeSteps;
        public string Filename;
}
