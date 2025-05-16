using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;
using SharpSDL3.Structs;

namespace SharpSDL3; 
[CustomMarshaller(typeof(HapticEffect), MarshalMode.ManagedToUnmanagedRef, typeof(OwnedHapticEffectParamRefMarshaller))]
public static unsafe class OwnedHapticEffectParamRefMarshaller {
    /// <summary>
    ///     Converts an unmanaged pointer to a managed <see cref="HapticEffect"/> struct.
    /// </summary>
    /// <param name="unmanaged">Pointer to unmanaged <see cref="HapticEffect"/> struct.</param>
    /// <returns>A managed <see cref="HapticEffect"/> struct.</returns>
    public static HapticEffect ConvertToManaged(nint unmanaged) {
        if (unmanaged == nint.Zero)
            return default;

        HapticEffect hapticEffect = Marshal.PtrToStructure<HapticEffect>(unmanaged);
        return hapticEffect;
    }

    /// <summary>
    ///     Converts a managed <see cref="HapticEffect"/> struct to an unmanaged pointer.
    /// </summary>
    /// <param name="managed"></param>
    /// <returns></returns>
    public static nint ConvertToUnmanaged(HapticEffect managed) {
        nint hapticEffectPtr = Marshal.AllocHGlobal(Marshal.SizeOf<HapticEffect>());
        Marshal.StructureToPtr(managed, hapticEffectPtr, false);
        return hapticEffectPtr;
    }
}
