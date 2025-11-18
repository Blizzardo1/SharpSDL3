using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SharpSDL3;

/// <summary>
///     Custom marshaller for caller-owned strings returned by SDL.
/// </summary>
[CustomMarshaller(typeof(string), MarshalMode.ManagedToUnmanagedOut, typeof(CallerOwnedStringMarshaller))]
public static unsafe class CallerOwnedStringMarshaller {

    /// <summary>
    ///     Converts an unmanaged string to a managed version.
    /// </summary>
    /// <returns>A managed string.</returns>
    public static string ConvertToManaged(nint unmanaged) {
        string? result = Marshal.PtrToStringUTF8(unmanaged);
        return result ?? "";
    }

    /// <summary>
    ///     Free the memory for a specified unmanaged string.
    /// </summary>
    public static void Free(nint mem) {
        Sdl.Free(mem);
    }
}