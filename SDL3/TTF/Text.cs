<<<<<<< HEAD
using System.Runtime.InteropServices;
=======
﻿using System.Runtime.InteropServices;
>>>>>>> main

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public struct Text {
<<<<<<< HEAD

=======
>>>>>>> main
    /// <summary>
    /// <para>A copy of the UTF-8 string that this text object represents, useful for layout, debugging and retrieving substring text.</para>
    /// <para>This is updated when the text object is modified and will be freed automatically when the object is destroyed.</para>
    /// </summary>
    public string TextStr;
<<<<<<< HEAD

=======
    
>>>>>>> main
    /// <summary>
    /// The number of lines in the text, 0 if it's empty
    /// </summary>
    public int NumLines;
<<<<<<< HEAD

=======
    
>>>>>>> main
    /// <summary>
    /// Application reference count, used when freeing surface
    /// </summary>
    public int RefCount;
<<<<<<< HEAD

=======
    
>>>>>>> main
    /// <summary>
    /// Private
    /// </summary>
    public TextData Internal;

    /// <summary>
    /// The Handle of the <see cref="Text"/> structure.
    /// </summary>
    public nint Handle;
<<<<<<< HEAD
}
=======
}
>>>>>>> main
