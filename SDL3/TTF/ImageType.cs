namespace SharpSDL3.TTF;

public static unsafe partial class Ttf {
    /**
     * The type of data in a glyph image
     *
     * \since This enum is available since SDL_ttf 3.0.0.
     */
    public enum ImageType {
        Invalid,
        Alpha,    /**< The color channels are white */
        Color,    /**< The color channels have image data */
        SignedDistanceField,      /**< The alpha channel has signed distance field information */
    }
}