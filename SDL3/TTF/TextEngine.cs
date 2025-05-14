namespace SharpSDL3.TTF;

public static unsafe partial class Ttf {
    /**
     * A text engine used to create text objects.
     *
     * This is a public interface that can be used by applications and libraries
     * to perform customize rendering with text objects. See
     * <SDL3_ttf/SDL_textengine.h> for details.
     *
     * There are three text engines provided with the library:
     *
     * - Drawing to an Surface, created with TTF_CreateSurfaceTextEngine()
     * - Drawing with an SDL 2D renderer, created with
     *   TTF_CreateRendererTextEngine()
     * - Drawing with the SDL GPU API, created with TTF_CreateGPUTextEngine()
     *
     * \since This struct is available since SDL_ttf 3.0.0.
     */
    public struct TextEngine {

    }
}