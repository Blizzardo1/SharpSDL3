using System;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct Color : IEquatable<Color> {
    public readonly bool Equals(Color other) {
        return R == other.R && G == other.G && B == other.B && A == other.A;
    }

    public readonly override bool Equals(object? obj) {
        return obj is Color other && Equals(other);
    }

    public readonly override int GetHashCode() {
        return HashCode.Combine(R, G, B, A);
    }

    public byte R;
    public byte G;
    public byte B;
    public byte A;

    public static bool operator ==(Color a, Color b) => a.R == b.R && a.G == b.G && a.B == b.B && a.A == b.A;
    public static bool operator !=(Color a, Color b) => !(a == b);
}