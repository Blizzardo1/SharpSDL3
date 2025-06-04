using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct HapticRamp {
    public ushort Type;
    public HapticDirection Direction;
    public uint Length;
    public ushort Delay;
    public ushort Button;
    public ushort Interval;
    public short Start;
    public short End;
    public ushort AttackLength;
    public ushort AttackLevel;
    public ushort FadeLength;
    public ushort FadeLevel;
}