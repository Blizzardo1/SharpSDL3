namespace SharpSDL3.Structs; 
public struct Vector2(float x, float y) {
    public float X { get; set; } = x;
    public float Y { get; set; } = y;

    public override readonly string ToString() => $"({X}, {Y})";
}
