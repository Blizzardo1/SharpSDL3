using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using SharpSDL3.Structs;

namespace SharpSDL3;

public static partial class AsyncIO {

    public static nint AsyncIOFromFile(string file, string mode) {
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

    public static SdlBool CloseAsyncIO(nint asyncio, SdlBool flush, nint queue, nint userdata) {
        if (asyncio == nint.Zero) {
            throw new ArgumentException("Invalid asyncio handle.", nameof(asyncio));
        }
        if (queue == nint.Zero) {
            throw new ArgumentException("Queue cannot be null.", nameof(queue));
        }
        return SDL_CloseAsyncIO(asyncio, flush, queue, userdata);
    }

    public static nint CreateAsyncIOQueue() {
        nint result = SDL_CreateAsyncIOQueue();
        return result == nint.Zero
            ? throw new InvalidOperationException("Failed to create AsyncIO queue.")
            : result;
    }

    public static void DestroyAsyncIOQueue(nint queue) {
        if (queue == nint.Zero) {
            throw new ArgumentException("Invalid queue handle.", nameof(queue));
        }
        SDL_DestroyAsyncIOQueue(queue);
    }

    public static SdlBool GetAsyncIOResult(nint queue, out AsyncIoOutcome outcome) {
        if (queue == nint.Zero) {
            throw new ArgumentException("Invalid queue handle.", nameof(queue));
        }
        SdlBool result = SDL_GetAsyncIOResult(queue, out outcome);
        if (!result) {
            throw new InvalidOperationException("Failed to get AsyncIO result.");
        }
        return result;
    }

    public static long GetAsyncIOSize(nint asyncio) {
        return asyncio == nint.Zero ? throw new ArgumentException("Invalid asyncio handle.", nameof(asyncio)) : SDL_GetAsyncIOSize(asyncio);
    }

    public static SdlBool LoadFileAsync(string file, nint queue, nint userdata) {
        if (string.IsNullOrEmpty(file)) {
            throw new ArgumentException("File path cannot be null or empty.", nameof(file));
        }
        if (queue == nint.Zero) {
            throw new ArgumentException("Queue cannot be null.", nameof(queue));
        }
        return SDL_LoadFileAsync(file, queue, userdata);
    }

    public static SdlBool ReadAsyncIO(nint asyncio, nint ptr, ulong offset, ulong size, nint queue, nint userdata) {
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

    public static void SignalAsyncIOQueue(nint queue) {
        if (queue == nint.Zero) {
            throw new ArgumentException("Invalid queue handle.", nameof(queue));
        }
        SDL_SignalAsyncIOQueue(queue);
    }

    public static SdlBool WaitAsyncIOResult(nint queue, out AsyncIoOutcome outcome, int timeoutMs) {
        if (queue == nint.Zero) {
            throw new ArgumentException("Invalid queue handle.", nameof(queue));
        }
        SdlBool result = SDL_WaitAsyncIOResult(queue, out outcome, timeoutMs);
        return !result ? throw new InvalidOperationException("Failed to wait for AsyncIO result.") : result;
    }

    public static SdlBool WriteAsyncIO(nint asyncio, nint ptr, ulong offset, ulong size, nint queue, nint userdata) {
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

    [LibraryImport(Sdl.NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_AsyncIOFromFile(string file, string mode);

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_CloseAsyncIO(nint asyncio, SdlBool flush, nint queue, nint userdata);

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateAsyncIOQueue();

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyAsyncIOQueue(nint queue);

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_GetAsyncIOResult(nint queue, out AsyncIoOutcome outcome);

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long SDL_GetAsyncIOSize(nint asyncio);

    [LibraryImport(Sdl.NativeLibName, StringMarshalling = Sdl.marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_LoadFileAsync(string file, nint queue, nint userdata);

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_ReadAsyncIO(nint asyncio, nint ptr, ulong offset, ulong size, nint queue,
        nint userdata);

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SignalAsyncIOQueue(nint queue);

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WaitAsyncIOResult(nint queue, out AsyncIoOutcome outcome, int timeoutMs);

    [LibraryImport(Sdl.NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WriteAsyncIO(nint asyncio, nint ptr, ulong offset, ulong size, nint queue,
        nint userdata);
}