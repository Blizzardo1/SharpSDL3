using System.Runtime.InteropServices;

using SDL3.Enums;

namespace SDL3.Structs;

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