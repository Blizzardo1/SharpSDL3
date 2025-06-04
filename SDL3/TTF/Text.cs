using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public struct Text {
    /// <summary>
    /// <para>A copy of the UTF-8 string that this text object represents, useful for layout, debugging and retrieving substring text.</para>
    /// <para>This is updated when the text object is modified and will be freed automatically when the object is destroyed.</para>
    /// </summary>
    public string TextStr;
    
    /// <summary>
    /// The number of lines in the text, 0 if it's empty
    /// </summary>
    public int NumLines;
    
    /// <summary>
    /// Application reference count, used when freeing surface
    /// </summary>
    public int RefCount;
    
    /// <summary>
    /// Private
    /// </summary>
    public TextData Internal;

    /// <summary>
    /// The Handle of the <see cref="Text"/> structure.
    /// </summary>
    public nint Handle;
}
