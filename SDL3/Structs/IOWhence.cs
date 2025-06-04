namespace SharpSDL3.Structs;

public enum IOWhence {
    Set,  /**< Seek from the beginning of data */
    Current,  /**< Seek relative to current read point */
    End   /**< Seek relative to the end of data */
}