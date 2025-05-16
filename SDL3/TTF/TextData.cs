using SharpSDL3.Structs;
using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

public static unsafe partial class Ttf {
    /**
     * Internal data for TTF_Text
     *
     * \since This struct is available since SDL_ttf 3.0.0.
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct TextData {
        public Font Font;             /**< The font used by this text, read-only. */
        public FColor Color;           /**< The color of the text, read-only. */

        public bool NeedsLayoutUpdate;   /**< True if the layout needs to be updated */
        public nint Layout;     /**< Cached layout information, read-only. */
        public int X;                      /**< The x offset of the upper left corner of this text, in pixels, read-only. */
        public int Y;                      /**< The y offset of the upper left corner of this text, in pixels, read-only. */
        public int W;                      /**< The width of this text, in pixels, read-only. */
        public int H;                      /**< The height of this text, in pixels, read-only. */
        public int NumOps;                /**< The number of drawing operations to render this text, read-only. */
        public nint Ops;     /**< The drawing operations used to render this text, read-only. */
        public int NumClusters;           /**< The number of substrings representing clusters of glyphs in the string, read-only */
        public nint Clusters;    /**< Substrings representing clusters of glyphs in the string, read-only */

        public int Props;     /**< Custom properties associated with this text, read-only. This field is created as-needed using TTF_GetTextProperties() and the properties may be then set and read normally */

        public bool NeedsEngineUpdate;   /**< True if the engine text needs to be updated */
        public TextEngine Engine;     /**< The engine used to render this text, read-only. */
        public TtfText EngineText;          /**< The implementation-specific representation of this text */
    };
}