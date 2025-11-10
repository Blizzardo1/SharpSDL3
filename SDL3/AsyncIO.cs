using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSDL3;

public static partial class Sdl {
    /// <summary>Use this function to create a new SDL_AsyncIO object for reading from and/or writing to a named file.</summary>

    /// <param name="file">a UTF-8 string representing the filename to open.</param>
    /// <param name="mode">an ASCII string representing the mode to be used for opening the file.</param>
    /// <remarks>
    /// The mode string understands the following values:
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CloseAsyncIo"/>
    /// <seealso cref="ReadAsyncIo"/>
    /// <seealso cref="WriteAsyncIo"/>
    /// </remarks>
    /// <returns>(SDL_AsyncIO *) Returns a pointer to theSDL_AsyncIO structure that is created or <see langword="null" /> on failure;call <see cref="GetError()" /> for more information.</returns>

    public static nint AsyncIoFromFile(string file, string mode) {
        if (string.IsNullOrEmpty(file)) {
            throw new ArgumentException("File path cannot be null or empty.", nameof(file));
        }

        if (string.IsNullOrEmpty(mode)) {
            throw new ArgumentException("Mode cannot be null or empty.", nameof(mode));
        }

        nint result = SDL_AsyncIOFromFile(file, mode);

        return result == nint.Zero
            ? throw new InvalidOperationException($"Failed to create AsyncIO from file: {file} with mode: {mode}")
            : result;
    }

    public static SdlBool CloseAsyncIo(nint asyncio, SdlBool flush, nint queue, nint userdata) {
        if (asyncio == nint.Zero) {
            throw new ArgumentException("Invalid asyncio handle.", nameof(asyncio));
        }
        if (queue == nint.Zero) {
            throw new ArgumentException("Queue cannot be null.", nameof(queue));
        }
        return SDL_CloseAsyncIO(asyncio, flush, queue, userdata);
    }

    /// <summary>Create a task queue for tracking multiple I/O operations.</summary>
    /// <remarks>
    /// Async I/O operations are assigned to a queue when started. The queue can be
    /// checked for completed tasks thereafter.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="DestroyAsyncIoQueue"/>
    /// <seealso cref="GetAsyncIoResult"/>
    /// <seealso cref="WaitAsyncIoResult"/>
    /// </remarks>
    /// <returns>(SDL_AsyncIOQueue *) Returns a new task queue object or<see langword="null" /> if there was an error; call <see cref="GetError()" /> for more information.</returns>

    public static nint CreateAsyncIoQueue() {
        nint result = SDL_CreateAsyncIOQueue();
        return result == nint.Zero
            ? throw new InvalidOperationException("Failed to create AsyncIO queue.")
            : result;
    }

    /// <summary>Destroy a previously-created async I/O task queue.</summary>

    /// <param name="queue">the task queue to destroy.</param>
    /// <remarks>
    /// If there are still tasks pending for this queue, this call will block until
    /// those tasks are finished. All those tasks will be deallocated. Their
    /// results will be lost to the app.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread, so long as no otherthread is waiting on the queue withSDL_WaitAsyncIOResult.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>

    public static void DestroyAsyncIoQueue(nint queue) {
        if (queue == nint.Zero) {
            throw new ArgumentException("Invalid queue handle.", nameof(queue));
        }
        SDL_DestroyAsyncIOQueue(queue);
    }

    /// <summary>Query an async I/O task queue for completed tasks.</summary>

    /// <param name="queue">the async I/O task queue to query.</param>
    /// <param name="outcome">details of a finished task will be written here. May not be <see langword="null" />.</param>
    /// <remarks>
    /// If a task assigned to this queue has finished, this will return true and
    /// fill in outcome with the details of the task. If no task in the queue has
    /// finished, this function will return false. This function does not block.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="WaitAsyncIoResult"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if a task has completed, <see langword="false" /> otherwise.</returns>

    public static SdlBool GetAsyncIoResult(nint queue, out AsyncIoOutcome outcome) {
        if (queue == nint.Zero) {
            throw new ArgumentException("Invalid queue handle.", nameof(queue));
        }
        SdlBool result = SDL_GetAsyncIOResult(queue, out outcome);
        if (!result) {
            throw new InvalidOperationException("Failed to get AsyncIO result.");
        }
        return result;
    }

    /// <summary>Use this function to get the size of the data stream in an SDL_AsyncIO.</summary>

    /// <param name="asyncio">the SDL_AsyncIO to get the size of the data stream from.</param>
    /// <remarks>
    /// This call is not asynchronous; it assumes that obtaining this info is a
    /// non-blocking operation in most reasonable cases.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the size of the data stream in theSDL_IOStream on success or a negative error code on failure; call <see cref="GetError()" /> for more information.</returns>

    public static long GetAsyncIoSize(nint asyncio) {
        return asyncio == nint.Zero ? throw new ArgumentException("Invalid asyncio handle.", nameof(asyncio)) : SDL_GetAsyncIOSize(asyncio);
    }

    /// <summary>Load all the data from a file path, asynchronously.</summary>

    /// <param name="file">the path to read all available data from.</param>
    /// <param name="queue">a queue to add the new SDL_AsyncIO to.</param>
    /// <param name="userdata">an app-defined pointer that will be provided with the task results.</param>
    /// <remarks>
    /// This function returns as quickly as possible; it does not wait for the read
    /// to complete. On a successful return, this work will continue in the
    /// background. If the work begins, even failure is asynchronous: a failing
    /// return value from this function only means the work couldn't start at all.
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="LoadFile_IO"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static SdlBool LoadFileAsync(string file, nint queue, nint userdata) {
        if (string.IsNullOrEmpty(file)) {
            throw new ArgumentException("File path cannot be null or empty.", nameof(file));
        }
        if (queue == nint.Zero) {
            throw new ArgumentException("Queue cannot be null.", nameof(queue));
        }
        return SDL_LoadFileAsync(file, queue, userdata);
    }

    public static SdlBool ReadAsyncIo(nint asyncio, nint ptr, ulong offset, ulong size, nint queue, nint userdata) {
        if (asyncio == nint.Zero) {
            throw new ArgumentException("Invalid asyncio handle.", nameof(asyncio));
        }
        if (ptr == nint.Zero) {
            throw new ArgumentException("Pointer cannot be null.", nameof(ptr));
        }
        if (queue == nint.Zero) {
            throw new ArgumentException("Queue cannot be null.", nameof(queue));
        }
        return SDL_ReadAsyncIO(asyncio, ptr, offset, size, queue, userdata);
    }

    /// <summary>Wake up any threads that are blocking in SDL_WaitAsyncIOResult().</summary>

    /// <param name="queue">the async I/O task queue to signal.</param>
    /// <remarks>
    /// This will unblock any threads that are sleeping in a call to
    /// SDL_WaitAsyncIOResult for the specified queue, and
    /// cause them to return from that function.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="WaitAsyncIoResult"/>
    /// </remarks>

    public static void SignalAsyncIoQueue(nint queue) {
        if (queue == nint.Zero) {
            throw new ArgumentException("Invalid queue handle.", nameof(queue));
        }
        SDL_SignalAsyncIOQueue(queue);
    }

    /// <summary>Block until an async I/O task queue has a completed task.</summary>

    /// <param name="queue">the async I/O task queue to wait on.</param>
    /// <param name="outcome">details of a finished task will be written here. May not be <see langword="null" />.</param>
    /// <param name="timeoutMS">the maximum time to wait, in milliseconds, or -1 to wait indefinitely.</param>
    /// <remarks>
    /// This function puts the calling thread to sleep until there a task assigned
    /// to the queue that has finished.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SignalAsyncIoQueue"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if task has completed, <see langword="false" /> otherwise.</returns>

    public static SdlBool WaitAsyncIoResult(nint queue, out AsyncIoOutcome outcome, int timeoutMs) {
        if (queue == nint.Zero) {
            throw new ArgumentException("Invalid queue handle.", nameof(queue));
        }
        SdlBool result = SDL_WaitAsyncIOResult(queue, out outcome, timeoutMs);
        return !result ? throw new InvalidOperationException("Failed to wait for AsyncIO result.") : result;
    }

    public static SdlBool WriteAsyncIo(nint asyncio, nint ptr, ulong offset, ulong size, nint queue, nint userdata) {
        if (asyncio == nint.Zero) {
            throw new ArgumentException("Invalid asyncio handle.", nameof(asyncio));
        }
        if (ptr == nint.Zero) {
            throw new ArgumentException("Pointer cannot be null.", nameof(ptr));
        }
        if (queue == nint.Zero) {
            throw new ArgumentException("Queue cannot be null.", nameof(queue));
        }
        return SDL_WriteAsyncIO(asyncio, ptr, offset, size, queue, userdata);
    }

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_AsyncIOFromFile(string file, string mode);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CloseAsyncIO(nint asyncio, SdlBool flush, nint queue, nint userdata);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateAsyncIOQueue();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyAsyncIOQueue(nint queue);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetAsyncIOResult(nint queue, out AsyncIoOutcome outcome);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long SDL_GetAsyncIOSize(nint asyncio);

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_LoadFileAsync(string file, nint queue, nint userdata);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadAsyncIO(nint asyncio, nint ptr, ulong offset, ulong size, nint queue,
        nint userdata);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SignalAsyncIOQueue(nint queue);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WaitAsyncIOResult(nint queue, out AsyncIoOutcome outcome, int timeoutMs);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteAsyncIO(nint asyncio, nint ptr, ulong offset, ulong size, nint queue,
        nint userdata);
}