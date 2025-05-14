namespace SharpSDL3.TTF;
/**
 * Direction flags
 *
 * The values here are chosen to match
 * [hb_direction_t](https://harfbuzz.github.io/harfbuzz-hb-common.html#hb-direction-t)
 * .
 *
 * \since This enum is available since SDL_ttf 3.0.0.
 *
 * \sa TTF_SetFontDirection
 */
public enum Direction {
    Invalid = 0,
    LTR = 4,        /**< Left to Right */
    RTL,            /**< Right to Left */
    TTB,            /**< Top to Bottom */
    BTT             /**< Bottom to Top */
}