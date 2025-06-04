<<<<<<< HEAD
using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public struct TextEngine {

=======
﻿using System;
using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;
[StructLayout(LayoutKind.Sequential)]
public struct TextEngine {
>>>>>>> main
    /// <summary>
    /// The version of this interface
    /// </summary>
    public uint Version;

    /// <summary>
    /// User data pointer passed to callbacks
    /// </summary>
    public nint UserData;

    /// <summary>
    /// Create a text representation from draw instructions.
    /// <para>
    /// All fields of <c>text</c> except <c>internal-&gt;engine_text</c> will already be filled out.
    /// </para>
    /// <para>
<<<<<<< HEAD
    /// This function should set the <c>internal-&gt;engine_text</c> field to a non-<see langword="null" /> value.
=======
    /// This function should set the <c>internal-&gt;engine_text</c> field to a non-NULL value.
>>>>>>> main
    /// </para>
    /// <param name="userdata">The userdata pointer in this interface.</param>
    /// <param name="text">The text object being created.</param>
    /// </summary>
    public delegate bool CreateText(nint userdata, Text text);

    /// <summary
    /// Destroy a text representation.
    /// </summary>
    public delegate void DestroyText(nint userdata, Text text);

    /// <summary>
    /// Handle to this <see cref="TextEngine"/> structure.
    /// </summary>
    public nint Handle;
<<<<<<< HEAD
};
=======

};
>>>>>>> main
