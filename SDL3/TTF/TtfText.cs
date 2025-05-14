using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

public static unsafe partial class Ttf {
    /**
     * Text created with TTF_CreateText()
     *
     * \since This struct is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_CreateText
     * \sa TTF_GetTextProperties
     * \sa TTF_DestroyText
     */
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct TtfText {
        public string Text;             /**< A copy of the UTF-8 string that this text object represents, useful for layout, debugging and retrieving substring text. This is updated when the text object is modified and will be freed automatically when the object is destroyed. */
        public int NumLines;          /**< The number of lines in the text, 0 if it's empty */

        public int RefCount;           /**< Application reference count, used when freeing surface */

        public TextData* _internal; /**< Private */

    }
}