using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
