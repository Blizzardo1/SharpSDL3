using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct TextEditingCandidatesEvent
{
	public EventType Type;
	public uint Reserved;
	public ulong Timestamp;
	public uint WindowId;
	public byte** Candidates;
	public int NumCandidates;
	public int SelectedCandidate;
	public SdlBool Horizontal;
	public byte Padding1;
	public byte Padding2;
	public byte Padding3;
}