using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static SharpSDL3.Delegates;

namespace SharpSDL3;

public static unsafe partial class Sdl {
    /// <summary>Get the current assertion handler.</summary>

    /// <param name="puserdata">pointer which is filled with the &quot;userdata&quot; pointer that was passed to SDL_SetAssertionHandler().</param>
    /// <remarks>
    /// This returns the function pointer that is called when an assertion is
    /// triggered. This is either the value last passed to
    /// SDL_SetAssertionHandler(), or if no
    /// application-specified function is set, is equivalent to calling
    /// SDL_GetDefaultAssertionHandler().
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="SetAssertionHandler"/>
    /// </remarks>
    /// <returns>Returns theSDL_AssertionHandler that is called when an asserttriggers.</returns>

    public static SdlAssertionHandler GetAssertionHandler(out nint puserdata) {
        var handler = SDL_GetAssertionHandler(out puserdata);
        return handler ?? throw new InvalidOperationException("Failed to get assertion handler.");
    }

    /// <summary>Get a list of all assertion failures.</summary>
    /// <remarks>
    /// This function gets all assertions triggered since the last call to
    /// <see cref="ResetAssertionReport"/>, or the start of the
    /// program.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe. Other threads calling<see cref="ResetAssertionReport"/> simultaneously, mayrender the returned pointer invalid.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="ResetAssertionReport"/>
    /// </remarks>
    /// <returns>(const SDL_AssertData *) Returns a list of all failedassertions or <see langword="null" /> if the list is empty. This memory should not be modifiedor freed by the application. This pointer remains valid until the next callto <see cref="Quit"/> or <see cref="ResetAssertionReport"/>.</returns>
    public static nint GetAssertionReport() {
        var report = SDL_GetAssertionReport();
        if (report == nint.Zero) {
            throw new InvalidOperationException("Failed to get assertion report.");
        }
        return report;
    }

    /// <summary>Get the default assertion handler.</summary>
    /// <remarks>
    /// This returns the function pointer that is called by default when an
    /// assertion is triggered. This is an internal function provided by SDL, that
    /// is used for assertions when
    /// SDL_SetAssertionHandler() hasn't been used to
    /// provide a different function.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAssertionHandler"/>
    /// </remarks>
    /// <returns>Returns the defaultSDL_AssertionHandler that is called when an asserttriggers.</returns>

    public static SdlAssertionHandler GetDefaultAssertionHandler() {
        var handler = SDL_GetDefaultAssertionHandler() ?? throw new InvalidOperationException("Failed to get default assertion handler.");
        return handler;
    }

    /// <summary>Never call this directly.</summary>

    /// <param name="data">assert data structure.</param>
    /// <param name="func">function name.</param>
    /// <param name="file">file name.</param>
    /// <param name="line">line number.</param>
    /// <remarks>
    /// Use the SDL_assert macros instead.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// </remarks>
    /// <returns>Returns assert state.</returns>

    public static AssertState ReportAssertion(ref AssertData data, string func, string file, int line) {
        // Add validation or logging to make the wrapper less trivial
        if (data.TriggerCount == 0) {
            Console.WriteLine($"Assertion triggered in function '{func}' at {file}:{line}");
        }

        // Call the native method
        var result = SDL_ReportAssertion(ref data, func, file, line);

        // Handle the result or add additional logic
        switch (result) {
            case AssertState.Retry:
                LogInfo(LogCategory.System, "Retrying assertion...");
                break;

            case AssertState.Break:
                LogError(LogCategory.Error, "Breaking on assertion...");
                break;

            case AssertState.Abort:
                LogError(LogCategory.Error, "Aborting due to assertion...");
                break;

            case AssertState.Ignore:
                LogWarn(LogCategory.System, "Ignoring assertion...");
                break;

            case AssertState.AlwaysIgnore:
                LogWarn(LogCategory.System, "Always ignoring assertion...");
                break;
        }

        return result;
    }

    /// <summary>Clear the list of all assertion failures.</summary>
    /// <remarks>
    /// This function will clear the list of all assertions triggered up to that
    /// point. Immediately following this call,
    /// SDL_GetAssertionReport will return no items. In
    /// addition, any previously-triggered assertions will be reset to a
    /// trigger_count of zero, and their always_ignore state will be <see langword="false" />.
    /// <para><strong>Thread Safety:</strong> This function is not thread safe. Other threads triggering an assertion, orsimultaneously calling this function may cause memory leaks or crashes.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAssertionReport"/>
    /// </remarks>

    public static void ResetAssertionReport() {
        SDL_ResetAssertionReport();
    }

    /// <summary>Set an application-defined assertion handler.</summary>

    /// <param name="handler">the SDL_AssertionHandler function to call when an assertion fails or <see langword="null" /> for the default handler.</param>
    /// <param name="userdata">a pointer that is passed to handler.</param>
    /// <remarks>
    /// This function allows an application to show its own assertion UI and/or
    /// force the response to an assertion failure. If the application doesn't
    /// provide this, SDL will try to do the right thing, popping up a
    /// system-specific GUI dialog, and probably minimizing any fullscreen windows.
    /// <para><strong>Thread Safety:</strong> It is safe to call this function from any thread.</para>
    /// <para><strong>Version:</strong> This function is available since SDL 3.2.0.</para>
    /// <seealso cref="GetAssertionHandler"/>
    /// </remarks>

    public static void SetAssertionHandler(SdlAssertionHandler handler, nint userdata) {
        if (handler == null) {
            throw new ArgumentNullException(nameof(handler), "Assertion handler cannot be null.");
        }

        SDL_SetAssertionHandler(handler, userdata);
    }

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlAssertionHandler SDL_GetAssertionHandler(out nint puserdata);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint SDL_GetAssertionReport();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SdlAssertionHandler SDL_GetDefaultAssertionHandler();

    [LibraryImport(NativeLibName, StringMarshalling = marshalling)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial AssertState SDL_ReportAssertion(ref AssertData data, string func, string file, int line);

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ResetAssertionReport();

    [LibraryImport(NativeLibName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetAssertionHandler(SdlAssertionHandler handler, nint userdata);
}