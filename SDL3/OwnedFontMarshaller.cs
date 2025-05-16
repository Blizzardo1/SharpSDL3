using SharpSDL3.TTF;
using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SharpSDL3;

[CustomMarshaller(typeof(Font), MarshalMode.ManagedToUnmanagedIn, typeof(OwnedFontMarshaller))]
[CustomMarshaller(typeof(Font), MarshalMode.ManagedToUnmanagedOut, typeof(OwnedFontMarshaller))]
public unsafe static class OwnedFontMarshaller {
    public static Font ConvertToManaged(nint unmanaged) {
        if (unmanaged == nint.Zero) {
            return default;
        }

        int size = Marshal.SizeOf<Font>();
        byte[] bytes = new byte[size];
        Marshal.Copy(unmanaged, bytes, 0, size);
        Font font = MemoryMarshal.Cast<byte, Font>(bytes)[0];

        return font;
    }

    public static nint ConvertToUnmanaged(Font managed) {
        int size = Marshal.SizeOf<Font>();
        byte[] bytes = new byte[size];
        MemoryMarshal.Cast<Font, byte>(new Span<Font>([managed])).CopyTo(bytes);
        nint unmanaged = Marshal.AllocHGlobal(size);
        Marshal.Copy(bytes, 0, unmanaged, size);
        return unmanaged;
    }
}
