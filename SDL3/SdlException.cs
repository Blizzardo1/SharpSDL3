using SharpSDL3.Enums;
using SharpSDL3.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using static SharpSDL3.Mixer.Mixer;

namespace SharpSDL3;
/// <summary>
/// Represents an SDL3_mixer-related exception.
/// </summary>
public class SdlException : Exception {
    public SdlException(string message) : base(message) { }
}