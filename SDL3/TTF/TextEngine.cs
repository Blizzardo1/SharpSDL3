using System.Runtime.InteropServices;
using SharpSDL3.Structs;

namespace SharpSDL3.TTF;

/// <summary>
/// A text engine used to create text objects.
/// </summary>
/// <remarks>
/// <para>
///     This is a public interface that can be used by applications and libraries to perform customize rendering with text objects. See &lt;SDL3_ttf/SDL_textengine.h&gt; for details.
/// </para>
/// <para>
///     There are three text engines provided with the library:
///     <list type="bullet">
///     <item>Drawing to a <see cref="Surface"/>, created with <see cref="Ttf.CreateSurfaceTextEngine"/></item>
///     <item>Drawing with an SDL 2D renderer, created with <see cref="Ttf.CreateRendererTextEngine"/></item>
///     <item>Drawing with the SDL GPU API, created with <see cref="Ttf.CreateGPUTextEngine"/></item>
///     </list>
/// </para>
/// <para><strong>Version:</strong> This struct is available since SDL_ttf 3.0.0</para>
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
public struct TextEngine {

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
    /// This function should set the <c>internal-&gt;engine_text</c> field to a non-<see langword="null" /> value.
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
};