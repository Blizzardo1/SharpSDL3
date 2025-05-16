using SharpSDL3.Structs;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SharpSDL3;

[CustomMarshaller(typeof(VirtualJoystickDesc), MarshalMode.ManagedToUnmanagedIn, typeof(OwnedVirtualJoystickDescMarshaller))]
[CustomMarshaller(typeof(VirtualJoystickDesc), MarshalMode.ManagedToUnmanagedOut, typeof(OwnedVirtualJoystickDescMarshaller))]
[CustomMarshaller(typeof(VirtualJoystickDesc), MarshalMode.ManagedToUnmanagedRef, typeof(OwnedVirtualJoystickDescMarshaller))]
public static class OwnedVirtualJoystickDescMarshaller {
    public static nint ConvertToUnmanaged(VirtualJoystickDesc managed) {
        nint ptr = Marshal.AllocHGlobal(Marshal.SizeOf<VirtualJoystickDesc>());
        Marshal.StructureToPtr(managed, ptr, false);
        if (ptr == nint.Zero)
            return nint.Zero;
        return ptr;
    }

    public static VirtualJoystickDesc ConvertToManaged(nint unmanaged) {
        if (unmanaged == nint.Zero)
            return default;
        VirtualJoystickDesc joystickDesc = Marshal.PtrToStructure<VirtualJoystickDesc>(unmanaged);
        return joystickDesc;
    }
}
