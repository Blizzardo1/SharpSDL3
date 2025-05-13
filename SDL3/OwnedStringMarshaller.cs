using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SharpSDL3;

/// <summary>
///     Custom marshaller for SDL-owned strings returned by SDL.
/// </summary>
[CustomMarshaller(typeof(string), MarshalMode.ManagedToUnmanagedOut, typeof(OwnedStringMarshaller))]
public static unsafe class OwnedStringMarshaller
{
	/// <summary>
	///     Converts an unmanaged string to a managed version.
	/// </summary>
	/// <returns>A managed string.</returns>
	public static string ConvertToManaged(byte* unmanaged)
	{
		return Marshal.PtrToStringUTF8((nint) unmanaged);
	}
}