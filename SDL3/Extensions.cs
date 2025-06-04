namespace SharpSDL3;

internal static class Extensions {

    public static bool IsEmpty(this string s) => string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s);
}