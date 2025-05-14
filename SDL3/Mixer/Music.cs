using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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
