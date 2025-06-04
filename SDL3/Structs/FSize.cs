using System;

namespace SharpSDL3.Structs;

public struct FSize(float width, float height) {
    public float Width { get; set; } = width;
    public float Height { get; set; } = height;

    public override readonly bool Equals(object? obj) {
        return obj is FSize size && Equals(size);
    }

    public static bool operator ==(FSize left, FSize right) {
        return left.Equals(right);
    }

    public static bool operator !=(FSize left, FSize right) {
        return !(left == right);
    }

    public override readonly int GetHashCode() {
        return HashCode.Combine(Width, Height);
    }

    public override readonly string ToString() {
        return $"Width: {Width}, Height: {Height}";
    }

    public static implicit operator FSize((float width, float height) tuple) {
        return new FSize(tuple.width, tuple.height);
    }

    public static explicit operator FSize(Size size) {
        return new FSize(size.Width, size.Height);
    }
}