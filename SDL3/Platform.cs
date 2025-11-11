using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SharpSDL3;

public static partial class Sdl {
    // /usr/local/include/SDL3/SDL_platform.h

    /// <summary>Get the name of the platform.</summary>
    /// <remarks>
    /// Here are the names returned for some (but not all) supported platforms:
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns the name of the platform. If the correct platformname is not available, returns a string beginning with the text &quot;Unknown&quot;.</returns>

    public static string GetPlatform() {
        return SDL_GetPlatform();
    }

    /// <summary>Create a new process.</summary>

    /// <param name="args">the path and arguments for the new process.</param>
    /// <param name="pipe_stdio"><see langword="true" /> to create pipes to the process's standard input and from the process's standard output, <see langword="false" /> for the process to have no input and inherit the application's standard output.</param>
    /// <remarks>
    /// The path to the executable is supplied in args[0]. args[1..N] are
    /// additional arguments passed on the command line of the new process, and the
    /// argument list should be terminated with a <see langword="null" />, e.g.:
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateProcessWithProperties"/>
    /// <seealso cref="GetProcessProperties"/>
    /// <seealso cref="ReadProcess"/>
    /// <seealso cref="GetProcessInput"/>
    /// <seealso cref="GetProcessOutput"/>
    /// <seealso cref="KillProcess"/>
    /// <seealso cref="WaitProcess"/>
    /// <seealso cref="DestroyProcess"/>
    /// </remarks>
    /// <returns>(SDL_Process *) Returns the newly created and runningprocess, or <see langword="null" /> if the process couldn't be created.</returns>

    public static nint CreateProcess(nint args, SdlBool pipeStdio) {
        if (args == nint.Zero) {
            throw new ArgumentException("Arguments cannot be null.", nameof(args));
        }
        return SDL_CreateProcess(args, pipeStdio);
    }

    /// <summary>Create a new process with the specified properties.</summary>

    /// <param name="props">the properties to use.</param>
    /// <remarks>
    /// These are the supported properties:
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateProcess"/>
    /// <seealso cref="GetProcessProperties"/>
    /// <seealso cref="ReadProcess"/>
    /// <seealso cref="GetProcessInput"/>
    /// <seealso cref="GetProcessOutput"/>
    /// <seealso cref="KillProcess"/>
    /// <seealso cref="WaitProcess"/>
    /// <seealso cref="DestroyProcess"/>
    /// </remarks>
    /// <returns>(SDL_Process *) Returns the newly created and runningprocess, or <see langword="null" /> if the process couldn't be created.</returns>

    public static nint CreateProcessWithProperties(uint props) {
        if (props == 0) {
            throw new ArgumentException("Properties cannot be zero.", nameof(props));
        }

        nint processHandle = SDL_CreateProcessWithProperties(props);

        if (processHandle == nint.Zero) {
            throw new InvalidOperationException("Failed to create process with the specified properties.");
        }

        return processHandle;
    }

    /// <summary>Get the properties associated with a process.</summary>

    /// <param name="process">the process to query.</param>
    /// <remarks>
    /// The following read-only properties are provided by SDL:
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateProcess"/>
    /// <seealso cref="CreateProcessWithProperties"/>
    /// </remarks>
    /// <returns>Returns a valid property ID on success or 0 on failure; call <see cref="GetError()" /> for more information.</returns>

    public static uint GetProcessProperties(nint process) {
        if (process == nint.Zero) {
            throw new ArgumentException("Process handle cannot be null.", nameof(process));
        }
        return SDL_GetProcessProperties(process);
    }

    /// <summary>Read all the output from a process.</summary>

    /// <param name="process">The process to read.</param>
    /// <param name="datasize">a pointer filled in with the number of bytes read, may be discarded.</param>
    /// <param name="exitcode">a pointer filled in with the process exit code if the process has exited, may be discarded.</param>
    /// <remarks>
    /// If a process was created with I/O enabled, you can use this function to
    /// read the output. This function blocks until the process is complete,
    /// capturing all output, and providing the process exit code.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateProcess"/>
    /// <seealso cref="CreateProcessWithProperties"/>
    /// <seealso cref="DestroyProcess"/>
    /// </remarks>
    /// <returns>(void *) Returns the data or <see langword="null" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static nint ReadProcess(nint process, out nuint datasize, out int exitcode) {
        if (process == nint.Zero) {
            throw new ArgumentException("Process handle cannot be null.", nameof(process));
        }
        return SDL_ReadProcess(process, out datasize, out exitcode);
    }

    /// <summary>Get the SDL_IOStream associated with process standard input.</summary>

    /// <param name="process">The process to get the input stream for.</param>
    /// <remarks>
    /// The process must have been created with
    /// SDL_CreateProcess() and pipe_stdio set to <see langword="true" />, or
    /// with SDL_CreateProcessWithProperties()
    /// and
    /// SDL_PROP_PROCESS_CREATE_STDIN_NUMBER
    /// set to SDL_PROCESS_STDIO_APP.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateProcess"/>
    /// <seealso cref="CreateProcessWithProperties"/>
    /// <seealso cref="GetProcessOutput"/>
    /// </remarks>
    /// <returns>(SDL_IOStream *) Returns the input stream or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint GetProcessInput(nint process) {
        if (process == nint.Zero) {
            throw new ArgumentException("Process handle cannot be null.", nameof(process));
        }
        return SDL_GetProcessInput(process);
    }

    /// <summary>Get the SDL_IOStream associated with process standard output.</summary>

    /// <param name="process">The process to get the output stream for.</param>
    /// <remarks>
    /// The process must have been created with
    /// SDL_CreateProcess() and pipe_stdio set to <see langword="true" />, or
    /// with SDL_CreateProcessWithProperties()
    /// and
    /// SDL_PROP_PROCESS_CREATE_STDOUT_NUMBER
    /// set to SDL_PROCESS_STDIO_APP.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateProcess"/>
    /// <seealso cref="CreateProcessWithProperties"/>
    /// <seealso cref="GetProcessInput"/>
    /// </remarks>
    /// <returns>(SDL_IOStream *) Returns the output stream or <see langword="null" /> on failure; call <see cref="GetError()" /> for more information.</returns>

    public static nint GetProcessOutput(nint process) {
        if (process == nint.Zero) {
            throw new ArgumentException("Process handle cannot be null.", nameof(process));
        }
        return SDL_GetProcessOutput(process);
    }

    /// <summary>Stop a process.</summary>

    /// <param name="process">The process to stop.</param>
    /// <param name="force"><see langword="true" /> to terminate the process immediately, <see langword="false" /> to try to stop the process gracefully. In general you should try to stop the process gracefully first as terminating a process may leave it with half-written data or in some other unstable state.</param>
    /// <remarks>
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateProcess"/>
    /// <seealso cref="CreateProcessWithProperties"/>
    /// <seealso cref="WaitProcess"/>
    /// <seealso cref="DestroyProcess"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> on success or <see langword="false" /> on failure; call <see cref="GetError()"/> for more information.</returns>

    public static SdlBool KillProcess(nint process, SdlBool force) {
        if (process == nint.Zero) {
            throw new ArgumentException("Process handle cannot be null.", nameof(process));
        }
        return SDL_KillProcess(process, force);
    }

    /// <summary>Wait for a process to finish.</summary>

    /// <param name="process">The process to wait for.</param>
    /// <param name="block">If <see langword="true" />, block until the process finishes; otherwise, report on the process' status.</param>
    /// <param name="exitcode">a pointer filled in with the process exit code if the process has exited, may be discarded.</param>
    /// <remarks>
    /// This can be called multiple times to get the status of a process.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateProcess"/>
    /// <seealso cref="CreateProcessWithProperties"/>
    /// <seealso cref="KillProcess"/>
    /// <seealso cref="DestroyProcess"/>
    /// </remarks>
    /// <returns>Returns <see langword="true" /> if the process exited, <see langword="false" /> otherwise.</returns>

    public static SdlBool WaitProcess(nint process, SdlBool block, out int exitcode) {
        if (process == nint.Zero) {
            throw new ArgumentException("Process handle cannot be null.", nameof(process));
        }
        return SDL_WaitProcess(process, block, out exitcode);
    }

    /// <summary>Destroy a previously created process object.</summary>

    /// <param name="process">The process object to destroy.</param>
    /// <remarks>
    /// Note that this does not stop the process, just destroys the SDL object used
    /// to track it. If you want to stop the process you should use
    /// SDL_KillProcess().
    /// <para><strong>Thread Safety:</strong> This function is not thread safe.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="CreateProcess"/>
    /// <seealso cref="CreateProcessWithProperties"/>
    /// <seealso cref="KillProcess"/>
    /// </remarks>

    public static void DestroyProcess(nint process) {
        if (process == nint.Zero) {
            throw new ArgumentException("Process handle cannot be null.", nameof(process));
        }
        SDL_DestroyProcess(process);
    }

    [LibraryImport(NativeLibName, StringMarshalling = Marshalling),
     UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(OwnedStringMarshaller))]
    private static partial string SDL_GetPlatform();

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateProcess(nint args, SdlBool pipeStdio);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_CreateProcessWithProperties(uint props);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetProcessProperties(nint process);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_ReadProcess(nint process, out nuint datasize, out int exitcode);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetProcessInput(nint process);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetProcessOutput(nint process);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_KillProcess(nint process, SdlBool force);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlBool SDL_WaitProcess(nint process, SdlBool block, out int exitcode);

    [LibraryImport(NativeLibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyProcess(nint process);
}