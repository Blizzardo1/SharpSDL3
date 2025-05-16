using System.Runtime.InteropServices;

using SharpSDL3.Enums;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct AssertData
{
	public SdlBool AlwaysIgnore;
	public uint TriggerCount;
	public nint Condition;
	public nint FileName;
	public int LineNum;
	public nint Function;
	public nint Next;
}