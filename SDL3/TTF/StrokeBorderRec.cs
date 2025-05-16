using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct StrokeBorderRec {
    public uint num_points;
    public uint max_points;
    public nint points;
    public nint tags;
    public bool movable;  /* TRUE for ends of lineto borders */
    public int start;    /* index of current sub-path start point */
    public Memory memory;
    public bool valid;

}
