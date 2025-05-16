using SharpSDL3.Structs;
using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;
[StructLayout(LayoutKind.Sequential)]
public struct SubString {
    public SubStringFlags Flags;   // The flags for this substring
    public int Offset;                 // The byte offset from the beginning of the text
    public int Length;                 // The byte length starting at the offset
    public int LineIndex;             // The index of the line that contains this substring
    public int ClusterIndex;          // The internal cluster index, used for quickly iterating
    public Rect Rect;              // The rectangle, relative to the top left of the text, containing the substring

    public nint Handle;
}