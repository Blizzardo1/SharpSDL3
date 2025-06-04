namespace SharpSDL3.Structs;

public struct Vector3(float x, float y, float z) {
    public float X { get; set; } = x;
    public float Y { get; set; } = y;
    public float Z { get; set; } = z;

    public override readonly string ToString() => $"({X}, {Y}, {Z})";
}