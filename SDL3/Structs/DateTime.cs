using System.Runtime.InteropServices;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct DateTime
{
	public int Year;
	public int Month;
	public int Day;
	public int Hour;
	public int Minute;
	public int Second;
	public int NanoSecond;
	public int DayOfWeek;
	public int UtcOffset;
}