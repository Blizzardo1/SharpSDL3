using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct HashItem {
    // TODO: Splitting off values into a separate array might be more cache-friendly
    public void* Key;
    public void* Value;
    public int Hash;
    public int ProbeLen;
    public int Live;
}