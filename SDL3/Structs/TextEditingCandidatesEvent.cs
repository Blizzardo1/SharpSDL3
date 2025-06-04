<<<<<<< HEAD
using SharpSDL3.Enums;
using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct TextEditingCandidatesEvent {
    public EventType Type;
    public uint Reserved;
    public ulong Timestamp;
    public uint WindowId;
    public nint* Candidates;
    public int NumCandidates;
    public int SelectedCandidate;
    public SdlBool Horizontal;
    public byte Padding1;
    public byte Padding2;
    public byte Padding3;
=======
using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct TextEditingCandidatesEvent
{
	public EventType Type;
	public uint Reserved;
	public ulong Timestamp;
	public uint WindowId;
	public nint* Candidates;
	public int NumCandidates;
	public int SelectedCandidate;
	public SdlBool Horizontal;
	public byte Padding1;
	public byte Padding2;
	public byte Padding3;
>>>>>>> main
}