using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct AssertData
{
	public SdlBool AlwaysIgnore;
	public uint TriggerCount;
	public byte* Condition;
	public byte* FileName;
	public int LineNum;
	public byte* Function;
	public AssertData* Next;
}