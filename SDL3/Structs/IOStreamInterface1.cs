using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct IOStreamInterface {
    /* The version of this interface */
    public int Version;

    /**
     *  Return the number of bytes in this SDL_IOStream
     *
     *  \return the total size of the data stream, or -1 on error.
     */
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long Size(nint userdata);

    /**
     *  Seek to `offset` relative to `whence`, one of stdio's whence values:
     *  SDL_IO_SEEK_SET, SDL_IO_SEEK_CUR, SDL_IO_SEEK_END
     *
     *  \return the final offset in the data stream, or -1 on error.
     */
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long Seek(nint userdata, long offset, IOWhence whence);

    /**
     *  Read up to `size` bytes from the data stream to the area pointed
     *  at by `ptr`.
     *
     *  On an incomplete read, you should set `*status` to a value from the
     *  SDL_IOStatus enum. You do not have to explicitly set this on
     *  a complete, successful read.
     *
     *  \return the number of bytes read
     */
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Structs.Size Read(nint userdata, nint ptr, Structs.Size size, nint status);

    /**
     *  Write exactly `size` bytes from the area pointed at by `ptr`
     *  to data stream.
     *
     *  On an incomplete write, you should set `*status` to a value from the
     *  SDL_IOStatus enum. You do not have to explicitly set this on
     *  a complete, successful write.
     *
     *  \return the number of bytes written
     */
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Structs.Size Write(nint userdata, nint ptr, Structs.Size size, nint status);

    /**
     *  If the stream is buffering, make sure the data is written out.
     *
     *  On failure, you should set `*status` to a value from the
     *  SDL_IOStatus enum. You do not have to explicitly set this on
     *  a successful flush.
     *
     *  \return true if successful or false on write error when flushing data.
     */
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool Flush(nint userdata, nint status);

    /**
     *  Close and free any allocated resources.
     *
     *  This does not guarantee file writes will sync to physical media; they
     *  can be in the system's file cache, waiting to go to disk.
     *
     *  The SDL_IOStream is still destroyed even if this fails, so clean up anything
     *  even if flushing buffers, etc, returns an error.
     *
     *  \return true if successful or false on write error when flushing data.
     */
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool Close(nint userdata);

}