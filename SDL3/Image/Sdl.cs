using SharpSDL3.Image;
using SharpSDL3.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SharpSDL3;
public static unsafe partial class Sdl {
    private const string ImageLibName = "SDL3_image";
    /* WIKI CATEGORY: SDLImage */

    /**
     * # CategorySDLImage
     *
     * Header file for SDL_image library
     *
     * A simple library to load images of various formats as SDL surfaces
     */
    /**
     * Printable format: "%d.%d.%d", MAJOR, MINOR, MICRO
     */
    public const int ImageMajor = 3;
    public const int ImageMinor = 3;
    public const int ImageMicro = 0;

    /**
     * This is the version number macro for the current SDL_image version.
     */
    public static int ImageVersion() => SharpSDL3.Sdl.VersionNum(ImageMajor, ImageMinor, ImageMicro);

    /**
     * This macro will evaluate to true if compiled with SDL_image at least X.Y.Z.
     */
    public static bool ImageVersionAtLeast(int major, int minor, int patch) =>
        (ImageMajor >= major)
        && (ImageMajor > major || ImageMinor >= minor)
        && (ImageMajor > major || ImageMinor > minor || ImageMicro >= patch);

    /**
     * This function gets the version of the dynamically linked SDL_image library.
     *
     * \returns SDL_image version.
     *
     * \since This function is available since SDL_image 3.0.0.
     */
    [LibraryImport(ImageLibName)]
    private static partial int IMG_Version();

    public static int GetImageVersion() => IMG_Version();

    /**
     * Load an image from an SDL data source into a software surface.
     *
     * An Surface is a buffer of pixels in memory accessible by the CPU. Use
     * this if you plan to hand the data to something else or manipulate it
     * further in code.
     *
     * There are no guarantees about what format the new Surface data will be;
     * in many cases, SDL_image will attempt to supply a surface that exactly
     * matches the provided image, but in others it might have to convert (either
     * because the image is in a format that SDL doesn't directly support or
     * because it's compressed data that could reasonably uncompress to various
     * formats and SDL_image had to pick one). You can inspect an Surface for
     * its specifics, and use SDL_ConvertSurface to then migrate to any supported
     * format.
     *
     * If the image format supports a transparent pixel, SDL will set the colorkey
     * for the surface. You can enable RLE acceleration on the surface afterwards
     * by calling: SDL_SetSurfaceColorKey(image, SDL_RLEACCEL,
     * image->format->colorkey);
     *
     * If `closeio` is true, `src` will be closed before returning, whether this
     * function succeeds or not. SDL_image reads everything it needs from `src`
     * during this call in any case.
     *
     * Even though this function accepts a file type, SDL_image may still try
     * other decoders that are capable of detecting file type from the contents of
     * the image data, but may rely on the caller-provided type string for formats
     * that it cannot autodetect. If `type` is NULL, SDL_image will rely solely on
     * its ability to guess the format.
     *
     * There is a separate function to read files from disk without having to deal
     * with IOStream: `IMG_Load("filename.jpg")` will call this function and
     * manage those details for you, determining the file type from the filename's
     * extension.
     *
     * There is also IMG_Load_IO(), which is equivalent to this function except
     * that it will rely on SDL_image to determine what type of data it is
     * loading, much like passing a NULL for type.
     *
     * If you are using SDL's 2D rendering API, there is an equivalent call to
     * load images directly into an Texture for use by the GPU without using a
     * software surface: call IMG_LoadTextureTyped_IO() instead.
     *
     * When done with the returned surface, the app should dispose of it with a
     * call to SDL_DestroySurface().
     *
     * \param src an IOStream that data will be read from.
     * \param closeio true to close/free the IOStream before returning, false
     *                to leave it open.
     * \param type a filename extension that represent this data ("BMP", "GIF",
     *             "PNG", etc).
     * \returns a new SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_Load
     * \sa IMG_Load_IO
     * \sa SDL_DestroySurface
     */
    [LibraryImport(ImageLibName, StringMarshalling = StringMarshalling.Utf8)]
    private static partial Surface* IMG_LoadTyped_IO(IOStream* src, [MarshalAs(UnmanagedType.Bool)] bool closeio, string type);

    public static Surface* LoadTypedImage(IOStream* src, bool closeio, string type) {
        return IMG_LoadTyped_IO(src, closeio, type);
    }

    /**
     * Load an image from a filesystem path into a software surface.
     *
     * An Surface is a buffer of pixels in memory accessible by the CPU. Use
     * this if you plan to hand the data to something else or manipulate it
     * further in code.
     *
     * There are no guarantees about what format the new Surface data will be;
     * in many cases, SDL_image will attempt to supply a surface that exactly
     * matches the provided image, but in others it might have to convert (either
     * because the image is in a format that SDL doesn't directly support or
     * because it's compressed data that could reasonably uncompress to various
     * formats and SDL_image had to pick one). You can inspect an Surface for
     * its specifics, and use SDL_ConvertSurface to then migrate to any supported
     * format.
     *
     * If the image format supports a transparent pixel, SDL will set the colorkey
     * for the surface. You can enable RLE acceleration on the surface afterwards
     * by calling: SDL_SetSurfaceColorKey(image, SDL_RLEACCEL,
     * image->format->colorkey);
     *
     * There is a separate function to read files from an IOStream, if you
     * need an i/o abstraction to provide data from anywhere instead of a simple
     * filesystem read; that function is IMG_Load_IO().
     *
     * If you are using SDL's 2D rendering API, there is an equivalent call to
     * load images directly into an Texture for use by the GPU without using a
     * software surface: call IMG_LoadTexture() instead.
     *
     * When done with the returned surface, the app should dispose of it with a
     * call to
     * [SDL_DestroySurface](https://wiki.libsdl.org/SDL3/SDL_DestroySurface)
     * ().
     *
     * \param file a path on the filesystem to load an image from.
     * \returns a new SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadTyped_IO
     * \sa IMG_Load_IO
     * \sa SDL_DestroySurface
     */
    [LibraryImport(ImageLibName, StringMarshalling = StringMarshalling.Utf8)]
    private static partial Surface* IMG_Load(string file);

    public static Surface* LoadImage(string file) {
        if (string.IsNullOrWhiteSpace(file)) {
            throw new ArgumentException("File path cannot be null or empty.", nameof(file));
        }

        Surface* surface = IMG_Load(file);
        if (surface == null) {
            Logger.LogError(Enums.LogCategory.System, $"Failed to load image from file: {file}. SDL Error: {SharpSDL3.Sdl.GetError()}");
        }

        return surface;
    }

    /**
     * Load an image from an SDL data source into a software surface.
     *
     * An Surface is a buffer of pixels in memory accessible by the CPU. Use
     * this if you plan to hand the data to something else or manipulate it
     * further in code.
     *
     * There are no guarantees about what format the new Surface data will be;
     * in many cases, SDL_image will attempt to supply a surface that exactly
     * matches the provided image, but in others it might have to convert (either
     * because the image is in a format that SDL doesn't directly support or
     * because it's compressed data that could reasonably uncompress to various
     * formats and SDL_image had to pick one). You can inspect an Surface for
     * its specifics, and use SDL_ConvertSurface to then migrate to any supported
     * format.
     *
     * If the image format supports a transparent pixel, SDL will set the colorkey
     * for the surface. You can enable RLE acceleration on the surface afterwards
     * by calling: SDL_SetSurfaceColorKey(image, SDL_RLEACCEL,
     * image->format->colorkey);
     *
     * If `closeio` is true, `src` will be closed before returning, whether this
     * function succeeds or not. SDL_image reads everything it needs from `src`
     * during this call in any case.
     *
     * There is a separate function to read files from disk without having to deal
     * with IOStream: `IMG_Load("filename.jpg")` will call this function and
     * manage those details for you, determining the file type from the filename's
     * extension.
     *
     * There is also IMG_LoadTyped_IO(), which is equivalent to this function
     * except a file extension (like "BMP", "JPG", etc) can be specified, in case
     * SDL_image cannot autodetect the file format.
     *
     * If you are using SDL's 2D rendering API, there is an equivalent call to
     * load images directly into an Texture for use by the GPU without using a
     * software surface: call IMG_LoadTexture_IO() instead.
     *
     * When done with the returned surface, the app should dispose of it with a
     * call to SDL_DestroySurface().
     *
     * \param src an IOStream that data will be read from.
     * \param closeio true to close/free the IOStream before returning, false
     *                to leave it open.
     * \returns a new SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_Load
     * \sa IMG_LoadTyped_IO
     * \sa SDL_DestroySurface
     */
    [LibraryImport(ImageLibName, StringMarshalling = StringMarshalling.Utf8)]
    private static partial Surface* IMG_Load_IO(IOStream* src, [MarshalAs(UnmanagedType.Bool)] bool closeio);

    public static Surface* LoadImage(IOStream* src, bool closeio) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        Surface* surface = IMG_Load_IO(src, closeio);
        if (surface == null) {
            Logger.LogError(Enums.LogCategory.System, $"Failed to load image from IOStream. SDL Error: {SharpSDL3.Sdl.GetError()}");
        }
        return surface;
    }

    /**
     * Load an image from a filesystem path into a GPU texture.
     *
     * An Texture represents an image in GPU memory, usable by SDL's 2D Render
     * API. This can be significantly more efficient than using a CPU-bound
     * Surface if you don't need to manipulate the image directly after
     * loading it.
     *
     * If the loaded image has transparency or a colorkey, a texture with an alpha
     * channel will be created. Otherwise, SDL_image will attempt to create an
     * Texture in the most format that most reasonably represents the image
     * data (but in many cases, this will just end up being 32-bit RGB or 32-bit
     * RGBA).
     *
     * There is a separate function to read files from an IOStream, if you
     * need an i/o abstraction to provide data from anywhere instead of a simple
     * filesystem read; that function is IMG_LoadTexture_IO().
     *
     * If you would rather decode an image to an Surface (a buffer of pixels
     * in CPU memory), call IMG_Load() instead.
     *
     * When done with the returned texture, the app should dispose of it with a
     * call to SDL_DestroyTexture().
     *
     * \param renderer the SDL_Renderer to use to create the GPU texture.
     * \param file a path on the filesystem to load an image from.
     * \returns a new texture, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadTextureTyped_IO
     * \sa IMG_LoadTexture_IO
     */
    [LibraryImport(ImageLibName, StringMarshalling = StringMarshalling.Utf8)]
    private static partial Texture* IMG_LoadTexture(nint renderer, string file);

    public static Texture* LoadTexture(nint renderer, string file) {
        if (string.IsNullOrWhiteSpace(file)) {
            throw new ArgumentException("File path cannot be null or empty.", nameof(file));
        }
        Texture* texture = IMG_LoadTexture(renderer, file);
        if (texture == null) {
            Logger.LogError(Enums.LogCategory.System, $"Failed to load texture from file: {file}. SDL Error: {SharpSDL3.Sdl.GetError()}");
        }
        return texture;
    }

    /**
     * Load an image from an SDL data source into a GPU texture.
     *
     * An Texture represents an image in GPU memory, usable by SDL's 2D Render
     * API. This can be significantly more efficient than using a CPU-bound
     * Surface if you don't need to manipulate the image directly after
     * loading it.
     *
     * If the loaded image has transparency or a colorkey, a texture with an alpha
     * channel will be created. Otherwise, SDL_image will attempt to create an
     * Texture in the most format that most reasonably represents the image
     * data (but in many cases, this will just end up being 32-bit RGB or 32-bit
     * RGBA).
     *
     * If `closeio` is true, `src` will be closed before returning, whether this
     * function succeeds or not. SDL_image reads everything it needs from `src`
     * during this call in any case.
     *
     * There is a separate function to read files from disk without having to deal
     * with IOStream: `IMG_LoadTexture(renderer, "filename.jpg")` will call
     * this function and manage those details for you, determining the file type
     * from the filename's extension.
     *
     * There is also IMG_LoadTextureTyped_IO(), which is equivalent to this
     * function except a file extension (like "BMP", "JPG", etc) can be specified,
     * in case SDL_image cannot autodetect the file format.
     *
     * If you would rather decode an image to an Surface (a buffer of pixels
     * in CPU memory), call IMG_Load() instead.
     *
     * When done with the returned texture, the app should dispose of it with a
     * call to SDL_DestroyTexture().
     *
     * \param renderer the SDL_Renderer to use to create the GPU texture.
     * \param src an IOStream that data will be read from.
     * \param closeio true to close/free the IOStream before returning, false
     *                to leave it open.
     * \returns a new texture, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadTexture
     * \sa IMG_LoadTextureTyped_IO
     * \sa SDL_DestroyTexture
     */
    [LibraryImport(ImageLibName)]
    private static partial Texture* IMG_LoadTexture_IO(nint renderer, IOStream* src, [MarshalAs(UnmanagedType.Bool)] bool closeio);

    public static Texture* LoadTexture(nint renderer, IOStream* src, bool closeio) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        Texture* texture = IMG_LoadTexture_IO(renderer, src, closeio);
        if (texture == null) {
            Logger.LogError(Enums.LogCategory.System, $"Failed to load texture from IOStream. SDL Error: {SharpSDL3.Sdl.GetError()}");
        }
        return texture;
    }

    /**
     * Load an image from an SDL data source into a GPU texture.
     *
     * An Texture represents an image in GPU memory, usable by SDL's 2D Render
     * API. This can be significantly more efficient than using a CPU-bound
     * Surface if you don't need to manipulate the image directly after
     * loading it.
     *
     * If the loaded image has transparency or a colorkey, a texture with an alpha
     * channel will be created. Otherwise, SDL_image will attempt to create an
     * Texture in the most format that most reasonably represents the image
     * data (but in many cases, this will just end up being 32-bit RGB or 32-bit
     * RGBA).
     *
     * If `closeio` is true, `src` will be closed before returning, whether this
     * function succeeds or not. SDL_image reads everything it needs from `src`
     * during this call in any case.
     *
     * Even though this function accepts a file type, SDL_image may still try
     * other decoders that are capable of detecting file type from the contents of
     * the image data, but may rely on the caller-provided type string for formats
     * that it cannot autodetect. If `type` is NULL, SDL_image will rely solely on
     * its ability to guess the format.
     *
     * There is a separate function to read files from disk without having to deal
     * with IOStream: `IMG_LoadTexture("filename.jpg")` will call this
     * function and manage those details for you, determining the file type from
     * the filename's extension.
     *
     * There is also IMG_LoadTexture_IO(), which is equivalent to this function
     * except that it will rely on SDL_image to determine what type of data it is
     * loading, much like passing a NULL for type.
     *
     * If you would rather decode an image to an Surface (a buffer of pixels
     * in CPU memory), call IMG_LoadTyped_IO() instead.
     *
     * When done with the returned texture, the app should dispose of it with a
     * call to SDL_DestroyTexture().
     *
     * \param renderer the SDL_Renderer to use to create the GPU texture.
     * \param src an IOStream that data will be read from.
     * \param closeio true to close/free the IOStream before returning, false
     *                to leave it open.
     * \param type a filename extension that represent this data ("BMP", "GIF",
     *             "PNG", etc).
     * \returns a new texture, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadTexture
     * \sa IMG_LoadTexture_IO
     * \sa SDL_DestroyTexture
     */
    [LibraryImport(ImageLibName, StringMarshalling = StringMarshalling.Utf8)]
    private static partial Texture* IMG_LoadTextureTyped_IO(nint renderer, IOStream* src, [MarshalAs(UnmanagedType.Bool)] bool closeio, string type);

    public static Texture* LoadTexture(nint renderer, IOStream* src, bool closeio, string type) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        Texture* texture = IMG_LoadTextureTyped_IO(renderer, src, closeio, type);
        if (texture == null) {
            Logger.LogError(Enums.LogCategory.System, $"Failed to load texture from IOStream. SDL Error: {SharpSDL3.Sdl.GetError()}");
        }
        return texture;
    }

    /**
     * Detect AVIF image data on a readable/seekable IOStream.
     *
     * This function attempts to determine if a file is a given filetype, reading
     * the least amount possible from the IOStream (usually a few bytes).
     *
     * There is no distinction made between "not the filetype in question" and
     * basic i/o errors.
     *
     * This function will always attempt to seek `src` back to where it started
     * when this function was called, but it will not report any errors in doing
     * so, but assuming seeking works, this means you can immediately use this
     * with a different IMG_isTYPE function, or load the image without further
     * seeking.
     *
     * You do not need to call this function to load data; SDL_image can work to
     * determine file type in many cases in its standard load functions.
     *
     * \param src a seekable/readable IOStream to provide image data.
     * \returns non-zero if this is AVIF data, zero otherwise.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_isAVIF
     * \sa IMG_isICO
     * \sa IMG_isCUR
     * \sa IMG_isBMP
     * \sa IMG_isGIF
     * \sa IMG_isJPG
     * \sa IMG_isJXL
     * \sa IMG_isLBM
     * \sa IMG_isPCX
     * \sa IMG_isPNG
     * \sa IMG_isPNM
     * \sa IMG_isSVG
     * \sa IMG_isQOI
     * \sa IMG_isTIF
     * \sa IMG_isXCF
     * \sa IMG_isXPM
     * \sa IMG_isXV
     * \sa IMG_isWEBP
     */
    [LibraryImport(ImageLibName, StringMarshalling = StringMarshalling.Utf8)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IMG_isAVIF(IOStream* src);

    public static bool IsAVIF(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_isAVIF(src);
    }

    /**
     * Detect ICO image data on a readable/seekable IOStream.
     *
     * This function attempts to determine if a file is a given filetype, reading
     * the least amount possible from the IOStream (usually a few bytes).
     *
     * There is no distinction made between "not the filetype in question" and
     * basic i/o errors.
     *
     * This function will always attempt to seek `src` back to where it started
     * when this function was called, but it will not report any errors in doing
     * so, but assuming seeking works, this means you can immediately use this
     * with a different IMG_isTYPE function, or load the image without further
     * seeking.
     *
     * You do not need to call this function to load data; SDL_image can work to
     * determine file type in many cases in its standard load functions.
     *
     * \param src a seekable/readable IOStream to provide image data.
     * \returns non-zero if this is ICO data, zero otherwise.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_isAVIF
     * \sa IMG_isCUR
     * \sa IMG_isBMP
     * \sa IMG_isGIF
     * \sa IMG_isJPG
     * \sa IMG_isJXL
     * \sa IMG_isLBM
     * \sa IMG_isPCX
     * \sa IMG_isPNG
     * \sa IMG_isPNM
     * \sa IMG_isSVG
     * \sa IMG_isQOI
     * \sa IMG_isTIF
     * \sa IMG_isXCF
     * \sa IMG_isXPM
     * \sa IMG_isXV
     * \sa IMG_isWEBP
     */
    [LibraryImport(ImageLibName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IMG_isICO(IOStream* src);

    public static bool IsICO(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_isICO(src);
    }

    /**
     * Detect CUR image data on a readable/seekable IOStream.
     *
     * This function attempts to determine if a file is a given filetype, reading
     * the least amount possible from the IOStream (usually a few bytes).
     *
     * There is no distinction made between "not the filetype in question" and
     * basic i/o errors.
     *
     * This function will always attempt to seek `src` back to where it started
     * when this function was called, but it will not report any errors in doing
     * so, but assuming seeking works, this means you can immediately use this
     * with a different IMG_isTYPE function, or load the image without further
     * seeking.
     *
     * You do not need to call this function to load data; SDL_image can work to
     * determine file type in many cases in its standard load functions.
     *
     * \param src a seekable/readable IOStream to provide image data.
     * \returns non-zero if this is CUR data, zero otherwise.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_isAVIF
     * \sa IMG_isICO
     * \sa IMG_isBMP
     * \sa IMG_isGIF
     * \sa IMG_isJPG
     * \sa IMG_isJXL
     * \sa IMG_isLBM
     * \sa IMG_isPCX
     * \sa IMG_isPNG
     * \sa IMG_isPNM
     * \sa IMG_isSVG
     * \sa IMG_isQOI
     * \sa IMG_isTIF
     * \sa IMG_isXCF
     * \sa IMG_isXPM
     * \sa IMG_isXV
     * \sa IMG_isWEBP
     */
    [LibraryImport(ImageLibName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IMG_isCUR(IOStream* src);

    public static bool IsCUR(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_isCUR(src);
    }

    /**
     * Detect BMP image data on a readable/seekable IOStream.
     *
     * This function attempts to determine if a file is a given filetype, reading
     * the least amount possible from the IOStream (usually a few bytes).
     *
     * There is no distinction made between "not the filetype in question" and
     * basic i/o errors.
     *
     * This function will always attempt to seek `src` back to where it started
     * when this function was called, but it will not report any errors in doing
     * so, but assuming seeking works, this means you can immediately use this
     * with a different IMG_isTYPE function, or load the image without further
     * seeking.
     *
     * You do not need to call this function to load data; SDL_image can work to
     * determine file type in many cases in its standard load functions.
     *
     * \param src a seekable/readable IOStream to provide image data.
     * \returns non-zero if this is BMP data, zero otherwise.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_isAVIF
     * \sa IMG_isICO
     * \sa IMG_isCUR
     * \sa IMG_isGIF
     * \sa IMG_isJPG
     * \sa IMG_isJXL
     * \sa IMG_isLBM
     * \sa IMG_isPCX
     * \sa IMG_isPNG
     * \sa IMG_isPNM
     * \sa IMG_isSVG
     * \sa IMG_isQOI
     * \sa IMG_isTIF
     * \sa IMG_isXCF
     * \sa IMG_isXPM
     * \sa IMG_isXV
     * \sa IMG_isWEBP
     */
    [LibraryImport(ImageLibName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IMG_isBMP(IOStream* src);

    public static bool IsBMP(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_isBMP(src);
    }

    /**
     * Detect GIF image data on a readable/seekable IOStream.
     *
     * This function attempts to determine if a file is a given filetype, reading
     * the least amount possible from the IOStream (usually a few bytes).
     *
     * There is no distinction made between "not the filetype in question" and
     * basic i/o errors.
     *
     * This function will always attempt to seek `src` back to where it started
     * when this function was called, but it will not report any errors in doing
     * so, but assuming seeking works, this means you can immediately use this
     * with a different IMG_isTYPE function, or load the image without further
     * seeking.
     *
     * You do not need to call this function to load data; SDL_image can work to
     * determine file type in many cases in its standard load functions.
     *
     * \param src a seekable/readable IOStream to provide image data.
     * \returns non-zero if this is GIF data, zero otherwise.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_isAVIF
     * \sa IMG_isICO
     * \sa IMG_isCUR
     * \sa IMG_isBMP
     * \sa IMG_isJPG
     * \sa IMG_isJXL
     * \sa IMG_isLBM
     * \sa IMG_isPCX
     * \sa IMG_isPNG
     * \sa IMG_isPNM
     * \sa IMG_isSVG
     * \sa IMG_isQOI
     * \sa IMG_isTIF
     * \sa IMG_isXCF
     * \sa IMG_isXPM
     * \sa IMG_isXV
     * \sa IMG_isWEBP
     */


    [LibraryImport(ImageLibName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IMG_isGIF(IOStream* src);
    public static bool IsGIF(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_isGIF(src);
    }

    /**
     * Detect JPG image data on a readable/seekable IOStream.
     *
     * This function attempts to determine if a file is a given filetype, reading
     * the least amount possible from the IOStream (usually a few bytes).
     *
     * There is no distinction made between "not the filetype in question" and
     * basic i/o errors.
     *
     * This function will always attempt to seek `src` back to where it started
     * when this function was called, but it will not report any errors in doing
     * so, but assuming seeking works, this means you can immediately use this
     * with a different IMG_isTYPE function, or load the image without further
     * seeking.
     *
     * You do not need to call this function to load data; SDL_image can work to
     * determine file type in many cases in its standard load functions.
     *
     * \param src a seekable/readable IOStream to provide image data.
     * \returns non-zero if this is JPG data, zero otherwise.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_isAVIF
     * \sa IMG_isICO
     * \sa IMG_isCUR
     * \sa IMG_isBMP
     * \sa IMG_isGIF
     * \sa IMG_isJXL
     * \sa IMG_isLBM
     * \sa IMG_isPCX
     * \sa IMG_isPNG
     * \sa IMG_isPNM
     * \sa IMG_isSVG
     * \sa IMG_isQOI
     * \sa IMG_isTIF
     * \sa IMG_isXCF
     * \sa IMG_isXPM
     * \sa IMG_isXV
     * \sa IMG_isWEBP
     */
    [LibraryImport(ImageLibName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IMG_isJPG(IOStream* src);

    public static bool IsJPG(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_isJPG(src);
    }

    /**
     * Detect JXL image data on a readable/seekable IOStream.
     *
     * This function attempts to determine if a file is a given filetype, reading
     * the least amount possible from the IOStream (usually a few bytes).
     *
     * There is no distinction made between "not the filetype in question" and
     * basic i/o errors.
     *
     * This function will always attempt to seek `src` back to where it started
     * when this function was called, but it will not report any errors in doing
     * so, but assuming seeking works, this means you can immediately use this
     * with a different IMG_isTYPE function, or load the image without further
     * seeking.
     *
     * You do not need to call this function to load data; SDL_image can work to
     * determine file type in many cases in its standard load functions.
     *
     * \param src a seekable/readable IOStream to provide image data.
     * \returns non-zero if this is JXL data, zero otherwise.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_isAVIF
     * \sa IMG_isICO
     * \sa IMG_isCUR
     * \sa IMG_isBMP
     * \sa IMG_isGIF
     * \sa IMG_isJPG
     * \sa IMG_isLBM
     * \sa IMG_isPCX
     * \sa IMG_isPNG
     * \sa IMG_isPNM
     * \sa IMG_isSVG
     * \sa IMG_isQOI
     * \sa IMG_isTIF
     * \sa IMG_isXCF
     * \sa IMG_isXPM
     * \sa IMG_isXV
     * \sa IMG_isWEBP
     */
    [LibraryImport(ImageLibName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IMG_isJXL(IOStream* src);

    public static bool IsJXL(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_isJXL(src);
    }

    /**
     * Detect LBM image data on a readable/seekable IOStream.
     *
     * This function attempts to determine if a file is a given filetype, reading
     * the least amount possible from the IOStream (usually a few bytes).
     *
     * There is no distinction made between "not the filetype in question" and
     * basic i/o errors.
     *
     * This function will always attempt to seek `src` back to where it started
     * when this function was called, but it will not report any errors in doing
     * so, but assuming seeking works, this means you can immediately use this
     * with a different IMG_isTYPE function, or load the image without further
     * seeking.
     *
     * You do not need to call this function to load data; SDL_image can work to
     * determine file type in many cases in its standard load functions.
     *
     * \param src a seekable/readable IOStream to provide image data.
     * \returns non-zero if this is LBM data, zero otherwise.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_isAVIF
     * \sa IMG_isICO
     * \sa IMG_isCUR
     * \sa IMG_isBMP
     * \sa IMG_isGIF
     * \sa IMG_isJPG
     * \sa IMG_isJXL
     * \sa IMG_isPCX
     * \sa IMG_isPNG
     * \sa IMG_isPNM
     * \sa IMG_isSVG
     * \sa IMG_isQOI
     * \sa IMG_isTIF
     * \sa IMG_isXCF
     * \sa IMG_isXPM
     * \sa IMG_isXV
     * \sa IMG_isWEBP
     */
    [LibraryImport(ImageLibName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IMG_isLBM(IOStream* src);
    public static bool IsLBM(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_isLBM(src);
    }

    /**
     * Detect PCX image data on a readable/seekable IOStream.
     *
     * This function attempts to determine if a file is a given filetype, reading
     * the least amount possible from the IOStream (usually a few bytes).
     *
     * There is no distinction made between "not the filetype in question" and
     * basic i/o errors.
     *
     * This function will always attempt to seek `src` back to where it started
     * when this function was called, but it will not report any errors in doing
     * so, but assuming seeking works, this means you can immediately use this
     * with a different IMG_isTYPE function, or load the image without further
     * seeking.
     *
     * You do not need to call this function to load data; SDL_image can work to
     * determine file type in many cases in its standard load functions.
     *
     * \param src a seekable/readable IOStream to provide image data.
     * \returns non-zero if this is PCX data, zero otherwise.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_isAVIF
     * \sa IMG_isICO
     * \sa IMG_isCUR
     * \sa IMG_isBMP
     * \sa IMG_isGIF
     * \sa IMG_isJPG
     * \sa IMG_isJXL
     * \sa IMG_isLBM
     * \sa IMG_isPNG
     * \sa IMG_isPNM
     * \sa IMG_isSVG
     * \sa IMG_isQOI
     * \sa IMG_isTIF
     * \sa IMG_isXCF
     * \sa IMG_isXPM
     * \sa IMG_isXV
     * \sa IMG_isWEBP
     */
    [LibraryImport(ImageLibName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IMG_isPCX(IOStream* src);
    public static bool IsPCX(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_isPCX(src);
    }

    /**
     * Detect PNG image data on a readable/seekable IOStream.
     *
     * This function attempts to determine if a file is a given filetype, reading
     * the least amount possible from the IOStream (usually a few bytes).
     *
     * There is no distinction made between "not the filetype in question" and
     * basic i/o errors.
     *
     * This function will always attempt to seek `src` back to where it started
     * when this function was called, but it will not report any errors in doing
     * so, but assuming seeking works, this means you can immediately use this
     * with a different IMG_isTYPE function, or load the image without further
     * seeking.
     *
     * You do not need to call this function to load data; SDL_image can work to
     * determine file type in many cases in its standard load functions.
     *
     * \param src a seekable/readable IOStream to provide image data.
     * \returns non-zero if this is PNG data, zero otherwise.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_isAVIF
     * \sa IMG_isICO
     * \sa IMG_isCUR
     * \sa IMG_isBMP
     * \sa IMG_isGIF
     * \sa IMG_isJPG
     * \sa IMG_isJXL
     * \sa IMG_isLBM
     * \sa IMG_isPCX
     * \sa IMG_isPNM
     * \sa IMG_isSVG
     * \sa IMG_isQOI
     * \sa IMG_isTIF
     * \sa IMG_isXCF
     * \sa IMG_isXPM
     * \sa IMG_isXV
     * \sa IMG_isWEBP
     */
    [LibraryImport(ImageLibName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IMG_isPNG(IOStream* src);
    public static bool IsPNG(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_isPNG(src);
    }

    /**
     * Detect PNM image data on a readable/seekable IOStream.
     *
     * This function attempts to determine if a file is a given filetype, reading
     * the least amount possible from the IOStream (usually a few bytes).
     *
     * There is no distinction made between "not the filetype in question" and
     * basic i/o errors.
     *
     * This function will always attempt to seek `src` back to where it started
     * when this function was called, but it will not report any errors in doing
     * so, but assuming seeking works, this means you can immediately use this
     * with a different IMG_isTYPE function, or load the image without further
     * seeking.
     *
     * You do not need to call this function to load data; SDL_image can work to
     * determine file type in many cases in its standard load functions.
     *
     * \param src a seekable/readable IOStream to provide image data.
     * \returns non-zero if this is PNM data, zero otherwise.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_isAVIF
     * \sa IMG_isICO
     * \sa IMG_isCUR
     * \sa IMG_isBMP
     * \sa IMG_isGIF
     * \sa IMG_isJPG
     * \sa IMG_isJXL
     * \sa IMG_isLBM
     * \sa IMG_isPCX
     * \sa IMG_isPNG
     * \sa IMG_isSVG
     * \sa IMG_isQOI
     * \sa IMG_isTIF
     * \sa IMG_isXCF
     * \sa IMG_isXPM
     * \sa IMG_isXV
     * \sa IMG_isWEBP
     */
    [LibraryImport(ImageLibName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IMG_isPNM(IOStream* src);
    public static bool IsPNM(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_isPNM(src);
    }

    /**
     * Detect SVG image data on a readable/seekable IOStream.
     *
     * This function attempts to determine if a file is a given filetype, reading
     * the least amount possible from the IOStream (usually a few bytes).
     *
     * There is no distinction made between "not the filetype in question" and
     * basic i/o errors.
     *
     * This function will always attempt to seek `src` back to where it started
     * when this function was called, but it will not report any errors in doing
     * so, but assuming seeking works, this means you can immediately use this
     * with a different IMG_isTYPE function, or load the image without further
     * seeking.
     *
     * You do not need to call this function to load data; SDL_image can work to
     * determine file type in many cases in its standard load functions.
     *
     * \param src a seekable/readable IOStream to provide image data.
     * \returns non-zero if this is SVG data, zero otherwise.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_isAVIF
     * \sa IMG_isICO
     * \sa IMG_isCUR
     * \sa IMG_isBMP
     * \sa IMG_isGIF
     * \sa IMG_isJPG
     * \sa IMG_isJXL
     * \sa IMG_isLBM
     * \sa IMG_isPCX
     * \sa IMG_isPNG
     * \sa IMG_isPNM
     * \sa IMG_isQOI
     * \sa IMG_isTIF
     * \sa IMG_isXCF
     * \sa IMG_isXPM
     * \sa IMG_isXV
     * \sa IMG_isWEBP
     */
    [LibraryImport(ImageLibName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IMG_isSVG(IOStream* src);
    public static bool IsSVG(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_isSVG(src);
    }

    /**
     * Detect QOI image data on a readable/seekable IOStream.
     *
     * This function attempts to determine if a file is a given filetype, reading
     * the least amount possible from the IOStream (usually a few bytes).
     *
     * There is no distinction made between "not the filetype in question" and
     * basic i/o errors.
     *
     * This function will always attempt to seek `src` back to where it started
     * when this function was called, but it will not report any errors in doing
     * so, but assuming seeking works, this means you can immediately use this
     * with a different IMG_isTYPE function, or load the image without further
     * seeking.
     *
     * You do not need to call this function to load data; SDL_image can work to
     * determine file type in many cases in its standard load functions.
     *
     * \param src a seekable/readable IOStream to provide image data.
     * \returns non-zero if this is QOI data, zero otherwise.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_isAVIF
     * \sa IMG_isICO
     * \sa IMG_isCUR
     * \sa IMG_isBMP
     * \sa IMG_isGIF
     * \sa IMG_isJPG
     * \sa IMG_isJXL
     * \sa IMG_isLBM
     * \sa IMG_isPCX
     * \sa IMG_isPNG
     * \sa IMG_isPNM
     * \sa IMG_isSVG
     * \sa IMG_isTIF
     * \sa IMG_isXCF
     * \sa IMG_isXPM
     * \sa IMG_isXV
     * \sa IMG_isWEBP
     */
    [LibraryImport(ImageLibName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IMG_isQOI(IOStream* src);
    public static bool IsQOI(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_isQOI(src);
    }

    /**
     * Detect TIFF image data on a readable/seekable IOStream.
     *
     * This function attempts to determine if a file is a given filetype, reading
     * the least amount possible from the IOStream (usually a few bytes).
     *
     * There is no distinction made between "not the filetype in question" and
     * basic i/o errors.
     *
     * This function will always attempt to seek `src` back to where it started
     * when this function was called, but it will not report any errors in doing
     * so, but assuming seeking works, this means you can immediately use this
     * with a different IMG_isTYPE function, or load the image without further
     * seeking.
     *
     * You do not need to call this function to load data; SDL_image can work to
     * determine file type in many cases in its standard load functions.
     *
     * \param src a seekable/readable IOStream to provide image data.
     * \returns non-zero if this is TIFF data, zero otherwise.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_isAVIF
     * \sa IMG_isICO
     * \sa IMG_isCUR
     * \sa IMG_isBMP
     * \sa IMG_isGIF
     * \sa IMG_isJPG
     * \sa IMG_isJXL
     * \sa IMG_isLBM
     * \sa IMG_isPCX
     * \sa IMG_isPNG
     * \sa IMG_isPNM
     * \sa IMG_isSVG
     * \sa IMG_isQOI
     * \sa IMG_isXCF
     * \sa IMG_isXPM
     * \sa IMG_isXV
     * \sa IMG_isWEBP
     */
    [LibraryImport(ImageLibName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IMG_isTIF(IOStream* src);

    public static bool IsTIF(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_isTIF(src);
    }

    /**
     * Detect XCF image data on a readable/seekable IOStream.
     *
     * This function attempts to determine if a file is a given filetype, reading
     * the least amount possible from the IOStream (usually a few bytes).
     *
     * There is no distinction made between "not the filetype in question" and
     * basic i/o errors.
     *
     * This function will always attempt to seek `src` back to where it started
     * when this function was called, but it will not report any errors in doing
     * so, but assuming seeking works, this means you can immediately use this
     * with a different IMG_isTYPE function, or load the image without further
     * seeking.
     *
     * You do not need to call this function to load data; SDL_image can work to
     * determine file type in many cases in its standard load functions.
     *
     * \param src a seekable/readable IOStream to provide image data.
     * \returns non-zero if this is XCF data, zero otherwise.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_isAVIF
     * \sa IMG_isICO
     * \sa IMG_isCUR
     * \sa IMG_isBMP
     * \sa IMG_isGIF
     * \sa IMG_isJPG
     * \sa IMG_isJXL
     * \sa IMG_isLBM
     * \sa IMG_isPCX
     * \sa IMG_isPNG
     * \sa IMG_isPNM
     * \sa IMG_isSVG
     * \sa IMG_isQOI
     * \sa IMG_isTIF
     * \sa IMG_isXPM
     * \sa IMG_isXV
     * \sa IMG_isWEBP
     */
    [LibraryImport(ImageLibName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IMG_isXCF(IOStream* src);

    public static bool IsXCF(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_isXCF(src);
    }

    /**
     * Detect XPM image data on a readable/seekable IOStream.
     *
     * This function attempts to determine if a file is a given filetype, reading
     * the least amount possible from the IOStream (usually a few bytes).
     *
     * There is no distinction made between "not the filetype in question" and
     * basic i/o errors.
     *
     * This function will always attempt to seek `src` back to where it started
     * when this function was called, but it will not report any errors in doing
     * so, but assuming seeking works, this means you can immediately use this
     * with a different IMG_isTYPE function, or load the image without further
     * seeking.
     *
     * You do not need to call this function to load data; SDL_image can work to
     * determine file type in many cases in its standard load functions.
     *
     * \param src a seekable/readable IOStream to provide image data.
     * \returns non-zero if this is XPM data, zero otherwise.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_isAVIF
     * \sa IMG_isICO
     * \sa IMG_isCUR
     * \sa IMG_isBMP
     * \sa IMG_isGIF
     * \sa IMG_isJPG
     * \sa IMG_isJXL
     * \sa IMG_isLBM
     * \sa IMG_isPCX
     * \sa IMG_isPNG
     * \sa IMG_isPNM
     * \sa IMG_isSVG
     * \sa IMG_isQOI
     * \sa IMG_isTIF
     * \sa IMG_isXCF
     * \sa IMG_isXV
     * \sa IMG_isWEBP
     */
    [LibraryImport(ImageLibName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IMG_isXPM(IOStream* src);

    public static bool IsXPM(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_isXPM(src);
    }

    /**
     * Detect XV image data on a readable/seekable IOStream.
     *
     * This function attempts to determine if a file is a given filetype, reading
     * the least amount possible from the IOStream (usually a few bytes).
     *
     * There is no distinction made between "not the filetype in question" and
     * basic i/o errors.
     *
     * This function will always attempt to seek `src` back to where it started
     * when this function was called, but it will not report any errors in doing
     * so, but assuming seeking works, this means you can immediately use this
     * with a different IMG_isTYPE function, or load the image without further
     * seeking.
     *
     * You do not need to call this function to load data; SDL_image can work to
     * determine file type in many cases in its standard load functions.
     *
     * \param src a seekable/readable IOStream to provide image data.
     * \returns non-zero if this is XV data, zero otherwise.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_isAVIF
     * \sa IMG_isICO
     * \sa IMG_isCUR
     * \sa IMG_isBMP
     * \sa IMG_isGIF
     * \sa IMG_isJPG
     * \sa IMG_isJXL
     * \sa IMG_isLBM
     * \sa IMG_isPCX
     * \sa IMG_isPNG
     * \sa IMG_isPNM
     * \sa IMG_isSVG
     * \sa IMG_isQOI
     * \sa IMG_isTIF
     * \sa IMG_isXCF
     * \sa IMG_isXPM
     * \sa IMG_isWEBP
     */
    [LibraryImport(ImageLibName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IMG_isXV(IOStream* src);

    public static bool IsXV(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_isXV(src);
    }

    /**
     * Detect WEBP image data on a readable/seekable IOStream.
     *
     * This function attempts to determine if a file is a given filetype, reading
     * the least amount possible from the IOStream (usually a few bytes).
     *
     * There is no distinction made between "not the filetype in question" and
     * basic i/o errors.
     *
     * This function will always attempt to seek `src` back to where it started
     * when this function was called, but it will not report any errors in doing
     * so, but assuming seeking works, this means you can immediately use this
     * with a different IMG_isTYPE function, or load the image without further
     * seeking.
     *
     * You do not need to call this function to load data; SDL_image can work to
     * determine file type in many cases in its standard load functions.
     *
     * \param src a seekable/readable IOStream to provide image data.
     * \returns non-zero if this is WEBP data, zero otherwise.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_isAVIF
     * \sa IMG_isICO
     * \sa IMG_isCUR
     * \sa IMG_isBMP
     * \sa IMG_isGIF
     * \sa IMG_isJPG
     * \sa IMG_isJXL
     * \sa IMG_isLBM
     * \sa IMG_isPCX
     * \sa IMG_isPNG
     * \sa IMG_isPNM
     * \sa IMG_isSVG
     * \sa IMG_isQOI
     * \sa IMG_isTIF
     * \sa IMG_isXCF
     * \sa IMG_isXPM
     * \sa IMG_isXV
     */
    [LibraryImport(ImageLibName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IMG_isWEBP(IOStream* src);

    public static bool IsWEBP(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_isWEBP(src);
    }

    /**
     * Load a AVIF image directly.
     *
     * If you know you definitely have a AVIF image, you can call this function,
     * which will skip SDL_image's file format detection routines. Generally it's
     * better to use the abstract interfaces; also, there is only an IOStream
     * interface available here.
     *
     * \param src an IOStream to load image data from.
     * \returns SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadICO_IO
     * \sa IMG_LoadCUR_IO
     * \sa IMG_LoadBMP_IO
     * \sa IMG_LoadGIF_IO
     * \sa IMG_LoadJPG_IO
     * \sa IMG_LoadJXL_IO
     * \sa IMG_LoadLBM_IO
     * \sa IMG_LoadPCX_IO
     * \sa IMG_LoadPNG_IO
     * \sa IMG_LoadPNM_IO
     * \sa IMG_LoadSVG_IO
     * \sa IMG_LoadQOI_IO
     * \sa IMG_LoadTGA_IO
     * \sa IMG_LoadTIF_IO
     * \sa IMG_LoadXCF_IO
     * \sa IMG_LoadXPM_IO
     * \sa IMG_LoadXV_IO
     * \sa IMG_LoadWEBP_IO
     */
    [LibraryImport(ImageLibName)]
    private static partial Surface* IMG_LoadAVIF_IO(IOStream* src);

    public static Surface* LoadAvifIo(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_LoadAVIF_IO(src);
    }

    /**
     * Load a ICO image directly.
     *
     * If you know you definitely have a ICO image, you can call this function,
     * which will skip SDL_image's file format detection routines. Generally it's
     * better to use the abstract interfaces; also, there is only an IOStream
     * interface available here.
     *
     * \param src an IOStream to load image data from.
     * \returns SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadAVIF_IO
     * \sa IMG_LoadCUR_IO
     * \sa IMG_LoadBMP_IO
     * \sa IMG_LoadGIF_IO
     * \sa IMG_LoadJPG_IO
     * \sa IMG_LoadJXL_IO
     * \sa IMG_LoadLBM_IO
     * \sa IMG_LoadPCX_IO
     * \sa IMG_LoadPNG_IO
     * \sa IMG_LoadPNM_IO
     * \sa IMG_LoadSVG_IO
     * \sa IMG_LoadQOI_IO
     * \sa IMG_LoadTGA_IO
     * \sa IMG_LoadTIF_IO
     * \sa IMG_LoadXCF_IO
     * \sa IMG_LoadXPM_IO
     * \sa IMG_LoadXV_IO
     * \sa IMG_LoadWEBP_IO
     */
    [LibraryImport(ImageLibName)]
    private static partial Surface* IMG_LoadICO_IO(IOStream* src);

    public static Surface* LoadIcoIo(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_LoadICO_IO(src);
    }

    /**
     * Load a CUR image directly.
     *
     * If you know you definitely have a CUR image, you can call this function,
     * which will skip SDL_image's file format detection routines. Generally it's
     * better to use the abstract interfaces; also, there is only an IOStream
     * interface available here.
     *
     * \param src an IOStream to load image data from.
     * \returns SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadAVIF_IO
     * \sa IMG_LoadICO_IO
     * \sa IMG_LoadBMP_IO
     * \sa IMG_LoadGIF_IO
     * \sa IMG_LoadJPG_IO
     * \sa IMG_LoadJXL_IO
     * \sa IMG_LoadLBM_IO
     * \sa IMG_LoadPCX_IO
     * \sa IMG_LoadPNG_IO
     * \sa IMG_LoadPNM_IO
     * \sa IMG_LoadSVG_IO
     * \sa IMG_LoadQOI_IO
     * \sa IMG_LoadTGA_IO
     * \sa IMG_LoadTIF_IO
     * \sa IMG_LoadXCF_IO
     * \sa IMG_LoadXPM_IO
     * \sa IMG_LoadXV_IO
     * \sa IMG_LoadWEBP_IO
     */
    [LibraryImport(ImageLibName)]
    private static partial Surface* IMG_LoadCUR_IO(IOStream* src);

    public static Surface* LoadCurIo(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_LoadCUR_IO(src);
    }

    /**
     * Load a BMP image directly.
     *
     * If you know you definitely have a BMP image, you can call this function,
     * which will skip SDL_image's file format detection routines. Generally it's
     * better to use the abstract interfaces; also, there is only an IOStream
     * interface available here.
     *
     * \param src an IOStream to load image data from.
     * \returns SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadAVIF_IO
     * \sa IMG_LoadICO_IO
     * \sa IMG_LoadCUR_IO
     * \sa IMG_LoadGIF_IO
     * \sa IMG_LoadJPG_IO
     * \sa IMG_LoadJXL_IO
     * \sa IMG_LoadLBM_IO
     * \sa IMG_LoadPCX_IO
     * \sa IMG_LoadPNG_IO
     * \sa IMG_LoadPNM_IO
     * \sa IMG_LoadSVG_IO
     * \sa IMG_LoadQOI_IO
     * \sa IMG_LoadTGA_IO
     * \sa IMG_LoadTIF_IO
     * \sa IMG_LoadXCF_IO
     * \sa IMG_LoadXPM_IO
     * \sa IMG_LoadXV_IO
     * \sa IMG_LoadWEBP_IO
     */
    [LibraryImport(ImageLibName)]
    private static partial Surface* IMG_LoadBMP_IO(IOStream* src);

    public static Surface* LoadBmpIo(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_LoadBMP_IO(src);
    }

    /**
     * Load a GIF image directly.
     *
     * If you know you definitely have a GIF image, you can call this function,
     * which will skip SDL_image's file format detection routines. Generally it's
     * better to use the abstract interfaces; also, there is only an IOStream
     * interface available here.
     *
     * \param src an IOStream to load image data from.
     * \returns SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadAVIF_IO
     * \sa IMG_LoadICO_IO
     * \sa IMG_LoadCUR_IO
     * \sa IMG_LoadBMP_IO
     * \sa IMG_LoadJPG_IO
     * \sa IMG_LoadJXL_IO
     * \sa IMG_LoadLBM_IO
     * \sa IMG_LoadPCX_IO
     * \sa IMG_LoadPNG_IO
     * \sa IMG_LoadPNM_IO
     * \sa IMG_LoadSVG_IO
     * \sa IMG_LoadQOI_IO
     * \sa IMG_LoadTGA_IO
     * \sa IMG_LoadTIF_IO
     * \sa IMG_LoadXCF_IO
     * \sa IMG_LoadXPM_IO
     * \sa IMG_LoadXV_IO
     * \sa IMG_LoadWEBP_IO
     */
    [LibraryImport(ImageLibName)]
    private static partial Surface* IMG_LoadGIF_IO(IOStream* src);
    public static Surface* LoadGifIo(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_LoadGIF_IO(src);
    }

    /**
     * Load a JPG image directly.
     *
     * If you know you definitely have a JPG image, you can call this function,
     * which will skip SDL_image's file format detection routines. Generally it's
     * better to use the abstract interfaces; also, there is only an IOStream
     * interface available here.
     *
     * \param src an IOStream to load image data from.
     * \returns SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadAVIF_IO
     * \sa IMG_LoadICO_IO
     * \sa IMG_LoadCUR_IO
     * \sa IMG_LoadBMP_IO
     * \sa IMG_LoadGIF_IO
     * \sa IMG_LoadJXL_IO
     * \sa IMG_LoadLBM_IO
     * \sa IMG_LoadPCX_IO
     * \sa IMG_LoadPNG_IO
     * \sa IMG_LoadPNM_IO
     * \sa IMG_LoadSVG_IO
     * \sa IMG_LoadQOI_IO
     * \sa IMG_LoadTGA_IO
     * \sa IMG_LoadTIF_IO
     * \sa IMG_LoadXCF_IO
     * \sa IMG_LoadXPM_IO
     * \sa IMG_LoadXV_IO
     * \sa IMG_LoadWEBP_IO
     */
    [LibraryImport(ImageLibName)]
    private static partial Surface* IMG_LoadJPG_IO(IOStream* src);
    public static Surface* LoadJpgIo(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_LoadJPG_IO(src);
    }

    /**
     * Load a JXL image directly.
     *
     * If you know you definitely have a JXL image, you can call this function,
     * which will skip SDL_image's file format detection routines. Generally it's
     * better to use the abstract interfaces; also, there is only an IOStream
     * interface available here.
     *
     * \param src an IOStream to load image data from.
     * \returns SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadAVIF_IO
     * \sa IMG_LoadICO_IO
     * \sa IMG_LoadCUR_IO
     * \sa IMG_LoadBMP_IO
     * \sa IMG_LoadGIF_IO
     * \sa IMG_LoadJPG_IO
     * \sa IMG_LoadLBM_IO
     * \sa IMG_LoadPCX_IO
     * \sa IMG_LoadPNG_IO
     * \sa IMG_LoadPNM_IO
     * \sa IMG_LoadSVG_IO
     * \sa IMG_LoadQOI_IO
     * \sa IMG_LoadTGA_IO
     * \sa IMG_LoadTIF_IO
     * \sa IMG_LoadXCF_IO
     * \sa IMG_LoadXPM_IO
     * \sa IMG_LoadXV_IO
     * \sa IMG_LoadWEBP_IO
     */
    [LibraryImport(ImageLibName)]
    private static partial Surface* IMG_LoadJXL_IO(IOStream* src);
    public static Surface* LoadJxlIo(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_LoadJXL_IO(src);
    }

    /**
     * Load a LBM image directly.
     *
     * If you know you definitely have a LBM image, you can call this function,
     * which will skip SDL_image's file format detection routines. Generally it's
     * better to use the abstract interfaces; also, there is only an IOStream
     * interface available here.
     *
     * \param src an IOStream to load image data from.
     * \returns SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadAVIF_IO
     * \sa IMG_LoadICO_IO
     * \sa IMG_LoadCUR_IO
     * \sa IMG_LoadBMP_IO
     * \sa IMG_LoadGIF_IO
     * \sa IMG_LoadJPG_IO
     * \sa IMG_LoadJXL_IO
     * \sa IMG_LoadPCX_IO
     * \sa IMG_LoadPNG_IO
     * \sa IMG_LoadPNM_IO
     * \sa IMG_LoadSVG_IO
     * \sa IMG_LoadQOI_IO
     * \sa IMG_LoadTGA_IO
     * \sa IMG_LoadTIF_IO
     * \sa IMG_LoadXCF_IO
     * \sa IMG_LoadXPM_IO
     * \sa IMG_LoadXV_IO
     * \sa IMG_LoadWEBP_IO
     */
    [LibraryImport(ImageLibName)]
    private static partial Surface* IMG_LoadLBM_IO(IOStream* src);
    public static Surface* LoadLbmIo(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_LoadLBM_IO(src);
    }

    /**
     * Load a PCX image directly.
     *
     * If you know you definitely have a PCX image, you can call this function,
     * which will skip SDL_image's file format detection routines. Generally it's
     * better to use the abstract interfaces; also, there is only an IOStream
     * interface available here.
     *
     * \param src an IOStream to load image data from.
     * \returns SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadAVIF_IO
     * \sa IMG_LoadICO_IO
     * \sa IMG_LoadCUR_IO
     * \sa IMG_LoadBMP_IO
     * \sa IMG_LoadGIF_IO
     * \sa IMG_LoadJPG_IO
     * \sa IMG_LoadJXL_IO
     * \sa IMG_LoadLBM_IO
     * \sa IMG_LoadPNG_IO
     * \sa IMG_LoadPNM_IO
     * \sa IMG_LoadSVG_IO
     * \sa IMG_LoadQOI_IO
     * \sa IMG_LoadTGA_IO
     * \sa IMG_LoadTIF_IO
     * \sa IMG_LoadXCF_IO
     * \sa IMG_LoadXPM_IO
     * \sa IMG_LoadXV_IO
     * \sa IMG_LoadWEBP_IO
     */
    [LibraryImport(ImageLibName)]
    private static partial Surface* IMG_LoadPCX_IO(IOStream* src);
    public static Surface* LoadPcxIo(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_LoadPCX_IO(src);
    }

    /**
     * Load a PNG image directly.
     *
     * If you know you definitely have a PNG image, you can call this function,
     * which will skip SDL_image's file format detection routines. Generally it's
     * better to use the abstract interfaces; also, there is only an IOStream
     * interface available here.
     *
     * \param src an IOStream to load image data from.
     * \returns SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadAVIF_IO
     * \sa IMG_LoadICO_IO
     * \sa IMG_LoadCUR_IO
     * \sa IMG_LoadBMP_IO
     * \sa IMG_LoadGIF_IO
     * \sa IMG_LoadJPG_IO
     * \sa IMG_LoadJXL_IO
     * \sa IMG_LoadLBM_IO
     * \sa IMG_LoadPCX_IO
     * \sa IMG_LoadPNM_IO
     * \sa IMG_LoadSVG_IO
     * \sa IMG_LoadQOI_IO
     * \sa IMG_LoadTGA_IO
     * \sa IMG_LoadTIF_IO
     * \sa IMG_LoadXCF_IO
     * \sa IMG_LoadXPM_IO
     * \sa IMG_LoadXV_IO
     * \sa IMG_LoadWEBP_IO
     */
    [LibraryImport(ImageLibName)]
    private static partial Surface* IMG_LoadPNG_IO(IOStream* src);
    public static Surface* LoadPngIo(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_LoadPNG_IO(src);
    }

    /**
     * Load a PNM image directly.
     *
     * If you know you definitely have a PNM image, you can call this function,
     * which will skip SDL_image's file format detection routines. Generally it's
     * better to use the abstract interfaces; also, there is only an IOStream
     * interface available here.
     *
     * \param src an IOStream to load image data from.
     * \returns SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadAVIF_IO
     * \sa IMG_LoadICO_IO
     * \sa IMG_LoadCUR_IO
     * \sa IMG_LoadBMP_IO
     * \sa IMG_LoadGIF_IO
     * \sa IMG_LoadJPG_IO
     * \sa IMG_LoadJXL_IO
     * \sa IMG_LoadLBM_IO
     * \sa IMG_LoadPCX_IO
     * \sa IMG_LoadPNG_IO
     * \sa IMG_LoadSVG_IO
     * \sa IMG_LoadQOI_IO
     * \sa IMG_LoadTGA_IO
     * \sa IMG_LoadTIF_IO
     * \sa IMG_LoadXCF_IO
     * \sa IMG_LoadXPM_IO
     * \sa IMG_LoadXV_IO
     * \sa IMG_LoadWEBP_IO
     */
    [LibraryImport(ImageLibName)]
    private static partial Surface* IMG_LoadPNM_IO(IOStream* src);
    public static Surface* LoadPnmIo(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_LoadPNM_IO(src);
    }

    /**
     * Load a SVG image directly.
     *
     * If you know you definitely have a SVG image, you can call this function,
     * which will skip SDL_image's file format detection routines. Generally it's
     * better to use the abstract interfaces; also, there is only an IOStream
     * interface available here.
     *
     * \param src an IOStream to load image data from.
     * \returns SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadAVIF_IO
     * \sa IMG_LoadICO_IO
     * \sa IMG_LoadCUR_IO
     * \sa IMG_LoadBMP_IO
     * \sa IMG_LoadGIF_IO
     * \sa IMG_LoadJPG_IO
     * \sa IMG_LoadJXL_IO
     * \sa IMG_LoadLBM_IO
     * \sa IMG_LoadPCX_IO
     * \sa IMG_LoadPNG_IO
     * \sa IMG_LoadPNM_IO
     * \sa IMG_LoadQOI_IO
     * \sa IMG_LoadTGA_IO
     * \sa IMG_LoadTIF_IO
     * \sa IMG_LoadXCF_IO
     * \sa IMG_LoadXPM_IO
     * \sa IMG_LoadXV_IO
     * \sa IMG_LoadWEBP_IO
     */
    [LibraryImport(ImageLibName)]
    private static partial Surface* IMG_LoadSVG_IO(IOStream* src);
    public static Surface* LoadSvgIo(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_LoadSVG_IO(src);
    }

    /**
     * Load a QOI image directly.
     *
     * If you know you definitely have a QOI image, you can call this function,
     * which will skip SDL_image's file format detection routines. Generally it's
     * better to use the abstract interfaces; also, there is only an IOStream
     * interface available here.
     *
     * \param src an IOStream to load image data from.
     * \returns SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadAVIF_IO
     * \sa IMG_LoadICO_IO
     * \sa IMG_LoadCUR_IO
     * \sa IMG_LoadBMP_IO
     * \sa IMG_LoadGIF_IO
     * \sa IMG_LoadJPG_IO
     * \sa IMG_LoadJXL_IO
     * \sa IMG_LoadLBM_IO
     * \sa IMG_LoadPCX_IO
     * \sa IMG_LoadPNG_IO
     * \sa IMG_LoadPNM_IO
     * \sa IMG_LoadSVG_IO
     * \sa IMG_LoadTGA_IO
     * \sa IMG_LoadTIF_IO
     * \sa IMG_LoadXCF_IO
     * \sa IMG_LoadXPM_IO
     * \sa IMG_LoadXV_IO
     * \sa IMG_LoadWEBP_IO
     */
    [LibraryImport(ImageLibName)]
    private static partial Surface* IMG_LoadQOI_IO(IOStream* src);

    public static Surface* LoadQoiIo(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_LoadQOI_IO(src);
    }

    /**
     * Load a TGA image directly.
     *
     * If you know you definitely have a TGA image, you can call this function,
     * which will skip SDL_image's file format detection routines. Generally it's
     * better to use the abstract interfaces; also, there is only an IOStream
     * interface available here.
     *
     * \param src an IOStream to load image data from.
     * \returns SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadAVIF_IO
     * \sa IMG_LoadICO_IO
     * \sa IMG_LoadCUR_IO
     * \sa IMG_LoadBMP_IO
     * \sa IMG_LoadGIF_IO
     * \sa IMG_LoadJPG_IO
     * \sa IMG_LoadJXL_IO
     * \sa IMG_LoadLBM_IO
     * \sa IMG_LoadPCX_IO
     * \sa IMG_LoadPNG_IO
     * \sa IMG_LoadPNM_IO
     * \sa IMG_LoadSVG_IO
     * \sa IMG_LoadQOI_IO
     * \sa IMG_LoadTIF_IO
     * \sa IMG_LoadXCF_IO
     * \sa IMG_LoadXPM_IO
     * \sa IMG_LoadXV_IO
     * \sa IMG_LoadWEBP_IO
     */
    [LibraryImport(ImageLibName)]
    private static partial Surface* IMG_LoadTGA_IO(IOStream* src);

    public static Surface* LoadTgaIo(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_LoadTGA_IO(src);
    }

    /**
     * Load a TIFF image directly.
     *
     * If you know you definitely have a TIFF image, you can call this function,
     * which will skip SDL_image's file format detection routines. Generally it's
     * better to use the abstract interfaces; also, there is only an IOStream
     * interface available here.
     *
     * \param src an IOStream to load image data from.
     * \returns SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadAVIF_IO
     * \sa IMG_LoadICO_IO
     * \sa IMG_LoadCUR_IO
     * \sa IMG_LoadBMP_IO
     * \sa IMG_LoadGIF_IO
     * \sa IMG_LoadJPG_IO
     * \sa IMG_LoadJXL_IO
     * \sa IMG_LoadLBM_IO
     * \sa IMG_LoadPCX_IO
     * \sa IMG_LoadPNG_IO
     * \sa IMG_LoadPNM_IO
     * \sa IMG_LoadSVG_IO
     * \sa IMG_LoadQOI_IO
     * \sa IMG_LoadTGA_IO
     * \sa IMG_LoadXCF_IO
     * \sa IMG_LoadXPM_IO
     * \sa IMG_LoadXV_IO
     * \sa IMG_LoadWEBP_IO
     */
    [LibraryImport(ImageLibName)]
    private static partial Surface* IMG_LoadTIF_IO(IOStream* src);

    public static Surface* LoadTifIo(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_LoadTIF_IO(src);
    }

    /**
     * Load a XCF image directly.
     *
     * If you know you definitely have a XCF image, you can call this function,
     * which will skip SDL_image's file format detection routines. Generally it's
     * better to use the abstract interfaces; also, there is only an IOStream
     * interface available here.
     *
     * \param src an IOStream to load image data from.
     * \returns SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadAVIF_IO
     * \sa IMG_LoadICO_IO
     * \sa IMG_LoadCUR_IO
     * \sa IMG_LoadBMP_IO
     * \sa IMG_LoadGIF_IO
     * \sa IMG_LoadJPG_IO
     * \sa IMG_LoadJXL_IO
     * \sa IMG_LoadLBM_IO
     * \sa IMG_LoadPCX_IO
     * \sa IMG_LoadPNG_IO
     * \sa IMG_LoadPNM_IO
     * \sa IMG_LoadSVG_IO
     * \sa IMG_LoadQOI_IO
     * \sa IMG_LoadTGA_IO
     * \sa IMG_LoadTIF_IO
     * \sa IMG_LoadXPM_IO
     * \sa IMG_LoadXV_IO
     * \sa IMG_LoadWEBP_IO
     */
    [LibraryImport(ImageLibName)]
    private static partial Surface* IMG_LoadXCF_IO(IOStream* src);

    public static Surface* LoadXcfIo(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_LoadXCF_IO(src);
    }

    /**
     * Load a XPM image directly.
     *
     * If you know you definitely have a XPM image, you can call this function,
     * which will skip SDL_image's file format detection routines. Generally it's
     * better to use the abstract interfaces; also, there is only an IOStream
     * interface available here.
     *
     * \param src an IOStream to load image data from.
     * \returns SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadAVIF_IO
     * \sa IMG_LoadICO_IO
     * \sa IMG_LoadCUR_IO
     * \sa IMG_LoadBMP_IO
     * \sa IMG_LoadGIF_IO
     * \sa IMG_LoadJPG_IO
     * \sa IMG_LoadJXL_IO
     * \sa IMG_LoadLBM_IO
     * \sa IMG_LoadPCX_IO
     * \sa IMG_LoadPNG_IO
     * \sa IMG_LoadPNM_IO
     * \sa IMG_LoadSVG_IO
     * \sa IMG_LoadQOI_IO
     * \sa IMG_LoadTGA_IO
     * \sa IMG_LoadTIF_IO
     * \sa IMG_LoadXCF_IO
     * \sa IMG_LoadXV_IO
     * \sa IMG_LoadWEBP_IO
     */
    [LibraryImport(ImageLibName)]
    private static partial Surface* IMG_LoadXPM_IO(IOStream* src);

    public static Surface* LoadXpmIo(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_LoadXPM_IO(src);
    }

    /**
     * Load a XV image directly.
     *
     * If you know you definitely have a XV image, you can call this function,
     * which will skip SDL_image's file format detection routines. Generally it's
     * better to use the abstract interfaces; also, there is only an IOStream
     * interface available here.
     *
     * \param src an IOStream to load image data from.
     * \returns SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadAVIF_IO
     * \sa IMG_LoadICO_IO
     * \sa IMG_LoadCUR_IO
     * \sa IMG_LoadBMP_IO
     * \sa IMG_LoadGIF_IO
     * \sa IMG_LoadJPG_IO
     * \sa IMG_LoadJXL_IO
     * \sa IMG_LoadLBM_IO
     * \sa IMG_LoadPCX_IO
     * \sa IMG_LoadPNG_IO
     * \sa IMG_LoadPNM_IO
     * \sa IMG_LoadSVG_IO
     * \sa IMG_LoadQOI_IO
     * \sa IMG_LoadTGA_IO
     * \sa IMG_LoadTIF_IO
     * \sa IMG_LoadXCF_IO
     * \sa IMG_LoadXPM_IO
     * \sa IMG_LoadWEBP_IO
     */
    [LibraryImport(ImageLibName)]
    private static partial Surface* IMG_LoadXV_IO(IOStream* src);

    public static Surface* LoadXvIo(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_LoadXV_IO(src);
    }

    /**
     * Load a WEBP image directly.
     *
     * If you know you definitely have a WEBP image, you can call this function,
     * which will skip SDL_image's file format detection routines. Generally it's
     * better to use the abstract interfaces; also, there is only an IOStream
     * interface available here.
     *
     * \param src an IOStream to load image data from.
     * \returns SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadAVIF_IO
     * \sa IMG_LoadICO_IO
     * \sa IMG_LoadCUR_IO
     * \sa IMG_LoadBMP_IO
     * \sa IMG_LoadGIF_IO
     * \sa IMG_LoadJPG_IO
     * \sa IMG_LoadJXL_IO
     * \sa IMG_LoadLBM_IO
     * \sa IMG_LoadPCX_IO
     * \sa IMG_LoadPNG_IO
     * \sa IMG_LoadPNM_IO
     * \sa IMG_LoadSVG_IO
     * \sa IMG_LoadQOI_IO
     * \sa IMG_LoadTGA_IO
     * \sa IMG_LoadTIF_IO
     * \sa IMG_LoadXCF_IO
     * \sa IMG_LoadXPM_IO
     * \sa IMG_LoadXV_IO
     */
    [LibraryImport(ImageLibName)]
    private static partial Surface* IMG_LoadWEBP_IO(IOStream* src);

    public static Surface* LoadWebpIo(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_LoadWEBP_IO(src);
    }

    /**
     * Load an SVG image, scaled to a specific size.
     *
     * Since SVG files are resolution-independent, you specify the size you would
     * like the output image to be and it will be generated at those dimensions.
     *
     * Either width or height may be 0 and the image will be auto-sized to
     * preserve aspect ratio.
     *
     * When done with the returned surface, the app should dispose of it with a
     * call to SDL_DestroySurface().
     *
     * \param src an IOStream to load SVG data from.
     * \param width desired width of the generated surface, in pixels.
     * \param height desired height of the generated surface, in pixels.
     * \returns a new SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     */
    [LibraryImport(ImageLibName)]
    private static partial Surface* IMG_LoadSizedSVG_IO(IOStream* src, int width, int height);

    public static Surface* LoadSizedSvgIo(IOStream* src, int width, int height) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_LoadSizedSVG_IO(src, width, height);
    }

    /**
     * Load an XPM image from a memory array.
     *
     * The returned surface will be an 8bpp indexed surface, if possible,
     * otherwise it will be 32bpp. If you always want 32-bit data, use
     * IMG_ReadXPMFromArrayToRGB888() instead.
     *
     * When done with the returned surface, the app should dispose of it with a
     * call to SDL_DestroySurface().
     *
     * \param xpm a null-terminated array of strings that comprise XPM data.
     * \returns a new SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_ReadXPMFromArrayToRGB888
     */
    [LibraryImport(ImageLibName, StringMarshalling = StringMarshalling.Utf8)]
    private static partial Surface* IMG_ReadXPMFromArray(string[] xpm);

    public static Surface* ReadXpmFromArray(string[] xpm) {
        if (xpm == null) {
            throw new ArgumentNullException(nameof(xpm), "XPM data cannot be null.");
        }
        return IMG_ReadXPMFromArray(xpm);
    }

    /**
     * Load an XPM image from a memory array.
     *
     * The returned surface will always be a 32-bit RGB surface. If you want 8-bit
     * indexed colors (and the XPM data allows it), use IMG_ReadXPMFromArray()
     * instead.
     *
     * When done with the returned surface, the app should dispose of it with a
     * call to SDL_DestroySurface().
     *
     * \param xpm a null-terminated array of strings that comprise XPM data.
     * \returns a new SDL surface, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_ReadXPMFromArray
     */
    [LibraryImport(ImageLibName, StringMarshalling = StringMarshalling.Utf8)]
    private static partial Surface* IMG_ReadXPMFromArrayToRGB888(string[] xpm);

    public static Surface* ReadXpmFromArrayToRgb888(string[] xpm) {
        if (xpm == null) {
            throw new ArgumentNullException(nameof(xpm), "XPM data cannot be null.");
        }
        return IMG_ReadXPMFromArrayToRGB888(xpm);
    }

    /**
     * Save an Surface into a AVIF image file.
     *
     * If the file already exists, it will be overwritten.
     *
     * \param surface the SDL surface to save.
     * \param file path on the filesystem to write new file to.
     * \param quality the desired quality, ranging between 0 (lowest) and 100
     *                (highest).
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_SaveAVIF_IO
     */
    [LibraryImport(ImageLibName, StringMarshalling = StringMarshalling.Utf8)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IMG_SaveAVIF(Surface* surface, string file, int quality);

    public static bool SaveAvif(Surface* surface, string file, int quality) {
        if (surface == null) {
            throw new ArgumentNullException(nameof(surface), "Surface cannot be null.");
        }
        if (file == null) {
            throw new ArgumentNullException(nameof(file), "File path cannot be null.");
        }
        return IMG_SaveAVIF(surface, file, quality);
    }

    /**
     * Save an Surface into AVIF image data, via an IOStream.
     *
     * If you just want to save to a filename, you can use IMG_SaveAVIF() instead.
     *
     * If `closeio` is true, `dst` will be closed before returning, whether this
     * function succeeds or not.
     *
     * \param surface the SDL surface to save.
     * \param dst the IOStream to save the image data to.
     * \param closeio true to close/free the IOStream before returning, false
     *                to leave it open.
     * \param quality the desired quality, ranging between 0 (lowest) and 100
     *                (highest).
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_SaveAVIF
     */
    [LibraryImport(ImageLibName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IMG_SaveAVIF_IO(Surface* surface, IOStream* dst, [MarshalAs(UnmanagedType.Bool)] bool closeio, int quality);

    public static bool SaveAvifIo(Surface* surface, IOStream* dst, bool closeio, int quality) {
        if (surface == null) {
            throw new ArgumentNullException(nameof(surface), "Surface cannot be null.");
        }
        if (dst == null) {
            throw new ArgumentNullException(nameof(dst), "IOStream cannot be null.");
        }
        return IMG_SaveAVIF_IO(surface, dst, closeio, quality);
    }

    /**
     * Save an Surface into a PNG image file.
     *
     * If the file already exists, it will be overwritten.
     *
     * \param surface the SDL surface to save.
     * \param file path on the filesystem to write new file to.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_SavePNG_IO
     */
    [LibraryImport(ImageLibName, StringMarshalling = StringMarshalling.Utf8)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IMG_SavePNG(Surface* surface, string file);

    public static bool SavePng(Surface* surface, string file) {
        if (surface == null) {
            throw new ArgumentNullException(nameof(surface), "Surface cannot be null.");
        }
        if (file == null) {
            throw new ArgumentNullException(nameof(file), "File path cannot be null.");
        }
        return IMG_SavePNG(surface, file);
    }

    /**
     * Save an Surface into PNG image data, via an IOStream.
     *
     * If you just want to save to a filename, you can use IMG_SavePNG() instead.
     *
     * If `closeio` is true, `dst` will be closed before returning, whether this
     * function succeeds or not.
     *
     * \param surface the SDL surface to save.
     * \param dst the IOStream to save the image data to.
     * \param closeio true to close/free the IOStream before returning, false
     *                to leave it open.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_SavePNG
     */
    [LibraryImport(ImageLibName)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IMG_SavePNG_IO(Surface* surface, IOStream* dst, [MarshalAs(UnmanagedType.Bool)] bool closeio);
    public static bool SavePngIo(Surface* surface, IOStream* dst, bool closeio) {
        if (surface == null) {
            throw new ArgumentNullException(nameof(surface), "Surface cannot be null.");
        }
        if (dst == null) {
            throw new ArgumentNullException(nameof(dst), "IOStream cannot be null.");
        }
        return IMG_SavePNG_IO(surface, dst, closeio);
    }

    /**
     * Save an Surface into a JPEG image file.
     *
     * If the file already exists, it will be overwritten.
     *
     * \param surface the SDL surface to save.
     * \param file path on the filesystem to write new file to.
     * \param quality [0; 33] is Lowest quality, [34; 66] is Middle quality, [67;
     *                100] is Highest quality.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_SaveJPG_IO
     */
    [LibraryImport(ImageLibName, StringMarshalling = StringMarshalling.Utf8)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IMG_SaveJPG(Surface* surface, string file, int quality);

    public static bool SaveJpg(Surface* surface, string file, int quality) {
        if (surface == null) {
            throw new ArgumentNullException(nameof(surface), "Surface cannot be null.");
        }
        if (file == null) {
            throw new ArgumentNullException(nameof(file), "File path cannot be null.");
        }
        return IMG_SaveJPG(surface, file, quality);
    }

    /**
     * Save an Surface into JPEG image data, via an IOStream.
     *
     * If you just want to save to a filename, you can use IMG_SaveJPG() instead.
     *
     * If `closeio` is true, `dst` will be closed before returning, whether this
     * function succeeds or not.
     *
     * \param surface the SDL surface to save.
     * \param dst the IOStream to save the image data to.
     * \param closeio true to close/free the IOStream before returning, false
     *                to leave it open.
     * \param quality [0; 33] is Lowest quality, [34; 66] is Middle quality, [67;
     *                100] is Highest quality.
     * \returns true on success or false on failure; call SDL_GetError() for more
     *          information.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_SaveJPG
     */
    [LibraryImport(ImageLibName, StringMarshalling = StringMarshalling.Utf8)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IMG_SaveJPG_IO(Surface* surface, IOStream* dst, [MarshalAs(UnmanagedType.Bool)] bool closeio, int quality);

    public static bool SaveJpgIo(Surface* surface, IOStream* dst, bool closeio, int quality) {
        if (surface == null) {
            throw new ArgumentNullException(nameof(surface), "Surface cannot be null.");
        }
        if (dst == null) {
            throw new ArgumentNullException(nameof(dst), "IOStream cannot be null.");
        }
        return IMG_SaveJPG_IO(surface, dst, closeio, quality);
    }

    /**
     * Load an animation from a file.
     *
     * When done with the returned animation, the app should dispose of it with a
     * call to IMG_FreeAnimation().
     *
     * \param file path on the filesystem containing an animated image.
     * \returns a new IMG_Animation, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_FreeAnimation
     */
    [LibraryImport(ImageLibName, StringMarshalling = StringMarshalling.Utf8)]

    private static partial Animation* IMG_LoadAnimation(string file);

    public static Animation* LoadAnimation(string file) {
        if (file == null) {
            throw new ArgumentNullException(nameof(file), "File path cannot be null.");
        }
        return IMG_LoadAnimation(file);
    }

    /**
     * Load an animation from an IOStream.
     *
     * If `closeio` is true, `src` will be closed before returning, whether this
     * function succeeds or not. SDL_image reads everything it needs from `src`
     * during this call in any case.
     *
     * When done with the returned animation, the app should dispose of it with a
     * call to IMG_FreeAnimation().
     *
     * \param src an IOStream that data will be read from.
     * \param closeio true to close/free the IOStream before returning, false
     *                to leave it open.
     * \returns a new IMG_Animation, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_FreeAnimation
     */
    [LibraryImport(ImageLibName)]
    private static partial Animation* IMG_LoadAnimation_IO(IOStream* src, [MarshalAs(UnmanagedType.Bool)] bool closeio);

    public static Animation* LoadAnimationIo(IOStream* src, bool closeio) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_LoadAnimation_IO(src, closeio);
    }

    /**
     * Load an animation from an SDL datasource
     *
     * Even though this function accepts a file type, SDL_image may still try
     * other decoders that are capable of detecting file type from the contents of
     * the image data, but may rely on the caller-provided type string for formats
     * that it cannot autodetect. If `type` is NULL, SDL_image will rely solely on
     * its ability to guess the format.
     *
     * If `closeio` is true, `src` will be closed before returning, whether this
     * function succeeds or not. SDL_image reads everything it needs from `src`
     * during this call in any case.
     *
     * When done with the returned animation, the app should dispose of it with a
     * call to IMG_FreeAnimation().
     *
     * \param src an IOStream that data will be read from.
     * \param closeio true to close/free the IOStream before returning, false
     *                to leave it open.
     * \param type a filename extension that represent this data ("GIF", etc).
     * \returns a new IMG_Animation, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadAnimation
     * \sa IMG_LoadAnimation_IO
     * \sa IMG_FreeAnimation
     */
    [LibraryImport(ImageLibName, StringMarshalling = StringMarshalling.Utf8)]
    private static partial Animation* IMG_LoadAnimationTyped_IO(IOStream* src, [MarshalAs(UnmanagedType.Bool)] bool closeio, string type);

    public static Animation* LoadAnimationTypedIo(IOStream* src, bool closeio, string type) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        if (type == null) {
            throw new ArgumentNullException(nameof(type), "Type cannot be null.");
        }
        return IMG_LoadAnimationTyped_IO(src, closeio, type);
    }

    /**
     * Dispose of an IMG_Animation and free its resources.
     *
     * The provided `anim` pointer is not valid once this call returns.
     *
     * \param anim IMG_Animation to dispose of.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadAnimation
     * \sa IMG_LoadAnimation_IO
     * \sa IMG_LoadAnimationTyped_IO
     */
    [LibraryImport(ImageLibName)]
    private static partial void IMG_FreeAnimation(Animation* anim);

    public static void FreeAnimation(Animation* anim) {
        if (anim == null) {
            throw new ArgumentNullException(nameof(anim), "Animation cannot be null.");
        }
        IMG_FreeAnimation(anim);
    }

    /**
     * Load a GIF animation directly.
     *
     * If you know you definitely have a GIF image, you can call this function,
     * which will skip SDL_image's file format detection routines. Generally it's
     * better to use the abstract interfaces; also, there is only an IOStream
     * interface available here.
     *
     * \param src an IOStream that data will be read from.
     * \returns a new IMG_Animation, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadAnimation
     * \sa IMG_LoadAnimation_IO
     * \sa IMG_LoadAnimationTyped_IO
     * \sa IMG_FreeAnimation
     */
    [LibraryImport(ImageLibName)]
    private static partial Animation* IMG_LoadGIFAnimation_IO(IOStream* src);

    public static Animation* LoadGifAnimationIo(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_LoadGIFAnimation_IO(src);
    }

    /**
     * Load a WEBP animation directly.
     *
     * If you know you definitely have a WEBP image, you can call this function,
     * which will skip SDL_image's file format detection routines. Generally it's
     * better to use the abstract interfaces; also, there is only an IOStream
     * interface available here.
     *
     * \param src an IOStream that data will be read from.
     * \returns a new IMG_Animation, or NULL on error.
     *
     * \since This function is available since SDL_image 3.0.0.
     *
     * \sa IMG_LoadAnimation
     * \sa IMG_LoadAnimation_IO
     * \sa IMG_LoadAnimationTyped_IO
     * \sa IMG_FreeAnimation
     */
    [LibraryImport(ImageLibName)]
    private static partial Animation* IMG_LoadWEBPAnimation_IO(IOStream* src);

    public static Animation* LoadWebpAnimationIo(IOStream* src) {
        if (src == null) {
            throw new ArgumentNullException(nameof(src), "IOStream cannot be null.");
        }
        return IMG_LoadWEBPAnimation_IO(src);
    }
}
