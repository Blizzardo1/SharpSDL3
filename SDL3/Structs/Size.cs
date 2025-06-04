using System;

namespace SharpSDL3.Structs;

public struct Size(int width, int height) : IEquatable<Size> {
    public int Width { readonly get => width; set => width = value; }
    public int Height { readonly get => height; set => height = value; }

    public override readonly bool Equals(object? obj) {
        return obj is Size size && Equals(size);
    }

    public readonly bool Equals(Size other) {
        return Width == other.Width;
    }

    public override readonly int GetHashCode() {
        return HashCode.Combine(Width);
    }

    public override readonly string ToString() {
        return $"Width: {Width}, Height: {Height}";
    }

    public static bool operator ==(Size left, Size right) {
        return left.Equals(right);
    }

    public static bool operator !=(Size left, Size right) {
        return !(left == right);
    }

    public static implicit operator Size((int width, int height) tuple) {
        return new Size(tuple.width, tuple.height);
    }

    public static explicit operator Size(FSize sizeF) {
        return new Size((int)sizeF.Width, (int)sizeF.Height);
    }
}