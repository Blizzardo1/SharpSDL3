using System.Runtime.InteropServices;
using static SharpSDL3.Delegates;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public struct FtLibrary {
    public Memory memory;           /* library's memory manager */

    public int VersionMajor;
    public int VersionMinor;
    public int VersionPatch;

    public uint NumModules;
    public FtModule[] Modules; // [FT_MAX_MODULES];  /* module objects  */

    public nint Renderers;        /* list of renderers        */
    public nint CurRenderer;     /* current outline renderer */
    public FtModule AutoHinter;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public FtDebugHookFunc[] DebugHooks;

    public string[] LcdWeights;      /* filter weights, if any */
    public FtBitmapLcdFilterFunc LcdFilterFunc;  /* filtering callback     */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public LongVector[] LcdGeometry; /* RGB subpixel positions */
    public int RefCount;

}