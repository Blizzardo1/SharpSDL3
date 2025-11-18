using SharpSDL3.TTF;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SharpSDL3;

[CustomMarshaller(typeof(Font), MarshalMode.ManagedToUnmanagedOut, typeof(OwnedFontMarshaller))]
public static unsafe class OwnedFontMarshaller {

    public static Font ConvertToManaged(nint unmanaged) {
        if (unmanaged == nint.Zero) {
            return default;
        }
        var font = Marshal.PtrToStructure<Font>(unmanaged);
        font.Handle = unmanaged;
        return font;
    }
}