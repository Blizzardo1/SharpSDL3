using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct FtOpenArgs {
    public uint flags;
    public string memory_base;
    public long memory_size;
    public string pathname;
    public FtStream stream;
    public FtModule driver;
    public int num_params;
    public nint Params;

  }
