using System.Runtime.InteropServices;
using static SharpSDL3.Delegates;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public struct FtStream {
    public string Base;
    public ulong size;
    public ulong pos;
    
    public FtStreamDesc descriptor;
    public FtStreamDesc pathname;
    public FtStreamIoFunc read;
    public FtStreamCloseFunc close;

    public Memory memory;
    public string cursor;
    public string limit;

}
