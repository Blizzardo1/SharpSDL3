using System;

namespace SharpSDL3;
/// <summary>
/// Represents an SDL3_mixer-related exception.
/// </summary>
public class SdlException : Exception {
    public SdlException(string message) : base(message) { }
}