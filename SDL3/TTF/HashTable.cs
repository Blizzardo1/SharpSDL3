using System.Runtime.InteropServices;
using static SharpSDL3.Delegates;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct HashTable {
    public nint RwLock;  // NULL if not created threadsafe
    public nint Table;
    public SdlHashCallback Hash;
    public SdlHashKeyMatchCallback KeyMatch;
    public SdlHashDestroyCallback Destroy;
    public nint UserData;
    public int HashMask;
    public int MaxProbeLen;
    public int NumOccupiedSlots;
};