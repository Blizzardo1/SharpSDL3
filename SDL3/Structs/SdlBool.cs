namespace SharpSDL3.Structs;

/// <summary>
///     Taken from https://github.com/ppy/SDL3-CS
///     C# bools are not blittable, so we need this workaround
/// </summary>
public readonly record struct SdlBool {
	internal const byte False = 0;
	internal const byte True = 1;
	private readonly byte _value;

	internal SdlBool(byte value) {
		_value = value;
	}

	public bool Equals(SdlBool other) {
		return other._value == _value;
	}

	public static implicit operator bool(SdlBool b) {
		return b._value != False;
	}

	public static implicit operator SdlBool(bool b) {
		return new SdlBool(b ? True : False);
	}

	public override int GetHashCode() {
		return _value.GetHashCode();
	}
}