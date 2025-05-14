using System.Runtime.InteropServices;
using static SharpSDL3.Delegates;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public struct FtModuleClass {
    public ulong module_flags;
    public long module_size;
    public string module_name;
    public long module_version;
    public long module_requires;
    public nint module_interface;
    public FtModuleConstructor module_init;
    public FtModuleDestructor module_done;
    public FtModuleRequester get_interface;

}
