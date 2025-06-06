namespace SharpSDL3.TTF;

public enum ImageType {
    Invalid,
    Alpha,    /**< The color channels are white */
    Color,    /**< The color channels have image data */
    SignedDistanceField,      /**< The alpha channel has signed distance field information */
}