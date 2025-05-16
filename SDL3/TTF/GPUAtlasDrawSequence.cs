using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

public static unsafe partial class Ttf {
    /**
     * Draw sequence returned by TTF_GetGPUTextDrawData
     *
     * \since This struct is available since SDL_ttf 3.0.0.
     *
     * \sa TTF_GetGPUTextDrawData
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct GPUAtlasDrawSequence {
        public nint AtlasTexture;          /**< Texture atlas that stores the glyphs */
        public nint xy;                         /**< An array of vertex positions */
        public nint uv;                         /**< An array of normalized texture coordinates for each vertex */
        public int NumVertices;                       /**< Number of vertices */
        public nint Indices;                           /**< An array of indices into the 'vertices' arrays */
        public int NumIndices;                        /**< Number of indices */
        public ImageType ImageType;               /**< The image type of this draw sequence */

        public nint next;  /**< The next sequence (will be NULL in case of the last sequence) */
    }
}