using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct AtomicInt {
    public int Value;

    public static implicit operator int(AtomicInt atomicInt) {
        return atomicInt.Value;
    }

    public static implicit operator AtomicInt(int value) {
        return new AtomicInt { Value = value };
    }

    public static AtomicInt operator ++(AtomicInt atomicInt) {
        atomicInt.Value++;
        return atomicInt;
    }

    public static AtomicInt operator --(AtomicInt atomicInt) {
        atomicInt.Value--;
        return atomicInt;
    }

    public static AtomicInt operator +(AtomicInt atomicInt, int value) {
        atomicInt.Value += value;
        return atomicInt;
    }

    public static AtomicInt operator -(AtomicInt atomicInt, int value) {
        atomicInt.Value -= value;
        return atomicInt;
    }

    public static AtomicInt operator *(AtomicInt atomicInt, int value) {
        atomicInt.Value *= value;
        return atomicInt;
    }

    public static AtomicInt operator /(AtomicInt atomicInt, int value) {
        atomicInt.Value /= value;
        return atomicInt;
    }

    public static AtomicInt operator %(AtomicInt atomicInt, int value) {
        atomicInt.Value %= value;
        return atomicInt;
    }

    public static AtomicInt operator &(AtomicInt atomicInt, int value) {
        atomicInt.Value &= value;
        return atomicInt;
    }

    public static AtomicInt operator |(AtomicInt atomicInt, int value) {
        atomicInt.Value |= value;
        return atomicInt;
    }

    public static AtomicInt operator ^(AtomicInt atomicInt, int value) {
        atomicInt.Value ^= value;
        return atomicInt;
    }

    public static AtomicInt operator <<(AtomicInt atomicInt, int value) {
        atomicInt.Value <<= value;
        return atomicInt;
    }

    public static AtomicInt operator >>(AtomicInt atomicInt, int value) {
        atomicInt.Value >>= value;
        return atomicInt;
    }

    public static bool operator ==(AtomicInt atomicInt, int value) {
        return atomicInt.Value == value;
    }

    public static bool operator !=(AtomicInt atomicInt, int value) {
        return atomicInt.Value != value;
    }

    public static bool operator >(AtomicInt atomicInt, int value) {
        return atomicInt.Value > value;
    }

    public static bool operator <(AtomicInt atomicInt, int value) {
        return atomicInt.Value < value;
    }

    public static bool operator >=(AtomicInt atomicInt, int value) {
        return atomicInt.Value >= value;
    }

    public static bool operator <=(AtomicInt atomicInt, int value) {
        return atomicInt.Value <= value;
    }

    public override readonly bool Equals(object? obj) {
        if (obj is AtomicInt other) {
            return Value == other.Value;
        }
        return false;
    }

    public override readonly int GetHashCode() {
        return Value.GetHashCode();
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> main
