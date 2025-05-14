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

    public FtDebugHookFunc[] DebugHooks; // [4]

    public string[] LcdWeights;      /* filter weights, if any */
    public FtBitmapLcdFilterFunc LcdFilterFunc;  /* filtering callback     */
    public LongVector[] LcdGeometry; // [3] /* RGB subpixel positions */
    public int RefCount;

}