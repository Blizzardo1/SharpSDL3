using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct HashItem {
    // TODO: Splitting off values into a separate array might be more cache-friendly
    public nint Key;
    public nint Value;
    public int Hash;
    public int ProbeLen;
    public int Live;
}