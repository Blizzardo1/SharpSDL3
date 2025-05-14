using System.Runtime.InteropServices;

namespace SharpSDL3.Mixer;

/// <summary>
/// The internal format for an audio chunk
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct Chunk {
    public int Allocated;
    public byte* AudioBuffer;
    public uint AudioLength;
    public byte Volume;       /* Per-sample volume, 0-128 */
}