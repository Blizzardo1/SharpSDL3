<<<<<<< HEAD
using SharpSDL3.Structs;
=======
﻿using SharpSDL3.Structs;
>>>>>>> main
using System.Runtime.InteropServices;

namespace SharpSDL3.Image;
/**
 * Animated image support
 *
 * Currently only animated GIFs and WEBP images are supported.
 */
<<<<<<< HEAD

=======
>>>>>>> main
[StructLayout(LayoutKind.Sequential)]
public struct Animation {
    public int W;                  /**< The width of the frames */
    public int H;                  /**< The height of the frames */
    public int Count;              /**< The number of frames */
    public Surface[] Frames;   /**< An array of frames */
    public int[] Delays;            /**< An array of frame delays, in milliseconds */
}