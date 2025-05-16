using System.Runtime.InteropServices;
using static SharpSDL3.TTF.Ttf;

namespace SharpSDL3.TTF;
/// <summary>
/// A text engine used to create text objects.
///
/// This is a public interface that can be used by applications and libraries
/// to perform customized rendering with text objects. See
/// &lt;SDL3_ttf/SDL_textengine.h&gt; for details.
///
/// There are three text engines provided with the library:
/// <list type="bullet">
/// <item>Drawing to a Surface, created with <see cref="CreateSurfaceTextEngine"/></item>
/// <item>Drawing with an SDL 2D renderer, created with <see cref="CreateRendererTextEngine(nint)"/></item>
/// <item>Drawing with the SDL GPU API, created with <see cref="CreateGPUTextEngine(nint)"/></item>
/// </list>
///
/// <para>Since SDL_ttf 3.0.0.</para>
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct TextEngine {
    /// <summary>
    /// The version of this interface
    /// </summary>
    public int version;

    /// <summary>
    /// User data pointer passed to callbacks.
    /// </summary>
    public nint userdata;

    /// <summary>
    /// Create a text representation from draw instructions. 
    /// </summary>
    /// <remarks>
    /// <para>All fields of `text` except `internal->engine_text` will already be filled out.</para>
    /// <para>This function should set the `internal->engine_text` field to a non-NULL value.</para>
    /// </remarks>
    /// <param name="userdata">the userdata pointer in this interface.</param>
    /// <param name="text">the text object being created.</param>
    public delegate bool CreateText(nint userdata, TtfText text);

    /// <summary>
    /// Destroy a text representation.
    /// </summary>
    public delegate void DestroyText(nint userdata, TtfText text);
}