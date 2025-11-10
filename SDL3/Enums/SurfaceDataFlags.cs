using System;

namespace SharpSDL3.Enums;

[Flags]
public enum SurfaceDataFlags : uint {
    DontFree = 0x00000001u,/** Surface is referenced internally */
    Stack = 0x00000002u,/** Surface is allocated on the stack */
    RleAccel = 0x00000004u/** Surface is RLE encoded */
}