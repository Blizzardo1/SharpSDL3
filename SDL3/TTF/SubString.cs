using SharpSDL3.Structs;
using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

/**
 * The representation of a substring within text.
 *
 * \since This struct is available since SDL_ttf 3.0.0.
 *
 * \sa TTF_GetNextTextSubString
 * \sa TTF_GetPreviousTextSubString
 * \sa TTF_GetTextSubString
 * \sa TTF_GetTextSubStringForLine
 * \sa TTF_GetTextSubStringForPoint
 * \sa TTF_GetTextSubStringsForRange
 */
[StructLayout(LayoutKind.Sequential)]
public struct SubString {
    public SubStringFlags Flags;   /**< The flags for this substring */
    public int Offset;                 /**< The byte offset from the beginning of the text */
    public int Length;                 /**< The byte length starting at the offset */
    public int LineIndex;             /**< The index of the line that contains this substring */
    public int ClusterIndex;          /**< The internal cluster index, used for quickly iterating */
    public Rect Rect;              /**< The rectangle, relative to the top left of the text, containing the substring */
}