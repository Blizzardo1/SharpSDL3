namespace SharpSDL3.TTF;

public static unsafe partial class Ttf {
    /**
     * The winding order of the vertices returned by TTF_GetGPUTextDrawData
     *
     * \since This enum is available since SDL_ttf 3.0.0.
     */
    public enum GPUTextEngineWinding {
        Invalid = -1,
        Clockwise,
        CounterClockwise
    }
}