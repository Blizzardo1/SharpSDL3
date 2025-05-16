using SharpSDL3.TTF;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SharpSDL3;

[CustomMarshaller(typeof(Font), MarshalMode.ManagedToUnmanagedIn, typeof(OwnedFontMarshaller))]
[CustomMarshaller(typeof(Font), MarshalMode.ManagedToUnmanagedOut, typeof(OwnedFontMarshaller))]
public unsafe static class OwnedFontMarshaller {
    // MarshalMode.ManagedToUnmanagedIn
    /// <summary>
    ///  Converts a managed Font to an unmanaged version.
    /// </summary>
    /// <param name="managed">The <see cref="Font"/> struct</param>
    /// <returns>An unmanaged <see cref="nint"/> pointer</returns>
    public static nint ConvertToUnmanaged(Font managed) {
        int size = Marshal.SizeOf<Font>();
        nint unmanaged = Marshal.AllocHGlobal(size);
        Marshal.StructureToPtr(managed, unmanaged, false);
        return unmanaged;
    }

    // MarshalMode.ManagedToUnmanagedOut
    /// <summary>
    ///   Converts an unmanaged Font to a managed version.
    /// </summary>
    /// <param name="unmanaged">An unmanaged <see cref="nint"/> poinrwe</param>
    /// <returns>A managed <see cref="Font"/> struct</returns>
    public static Font ConvertToManaged(nint unmanaged) {
        if (unmanaged == nint.Zero) {
            return default;
        }

        return Marshal.PtrToStructure<Font>(unmanaged)!;
    }
}
