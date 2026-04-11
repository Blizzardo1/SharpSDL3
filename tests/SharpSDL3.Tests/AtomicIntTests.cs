using SharpSDL3.Structs;
using Xunit;

namespace SharpSDL3.Tests;

/// <summary>
/// Tests for AtomicInt operator overloads and conversions.
/// </summary>
public class AtomicIntTests
{
    [Fact]
    public void ImplicitConversion_FromInt()
    {
        AtomicInt a = 42;
        Assert.Equal(42, a.Value);
    }

    [Fact]
    public void ImplicitConversion_ToInt()
    {
        AtomicInt a = new AtomicInt { Value = 99 };
        int val = a;
        Assert.Equal(99, val);
    }

    [Fact]
    public void Increment_Operator()
    {
        AtomicInt a = 5;
        a++;
        Assert.Equal(6, a.Value);
    }

    [Fact]
    public void Decrement_Operator()
    {
        AtomicInt a = 5;
        a--;
        Assert.Equal(4, a.Value);
    }

    [Fact]
    public void Addition_Operator()
    {
        AtomicInt a = 10;
        a = a + 5;
        Assert.Equal(15, a.Value);
    }

    [Fact]
    public void Subtraction_Operator()
    {
        AtomicInt a = 10;
        a = a - 3;
        Assert.Equal(7, a.Value);
    }

    [Fact]
    public void Multiplication_Operator()
    {
        AtomicInt a = 6;
        a = a * 7;
        Assert.Equal(42, a.Value);
    }

    [Fact]
    public void Division_Operator()
    {
        AtomicInt a = 42;
        a = a / 6;
        Assert.Equal(7, a.Value);
    }

    [Fact]
    public void Modulus_Operator()
    {
        AtomicInt a = 10;
        a = a % 3;
        Assert.Equal(1, a.Value);
    }

    [Fact]
    public void BitwiseAnd_Operator()
    {
        AtomicInt a = 0b1100;
        a = a & 0b1010;
        Assert.Equal(0b1000, a.Value);
    }

    [Fact]
    public void BitwiseOr_Operator()
    {
        AtomicInt a = 0b1100;
        a = a | 0b0011;
        Assert.Equal(0b1111, a.Value);
    }

    [Fact]
    public void BitwiseXor_Operator()
    {
        AtomicInt a = 0b1100;
        a = a ^ 0b1010;
        Assert.Equal(0b0110, a.Value);
    }

    [Fact]
    public void LeftShift_Operator()
    {
        AtomicInt a = 1;
        a = a << 4;
        Assert.Equal(16, a.Value);
    }

    [Fact]
    public void RightShift_Operator()
    {
        AtomicInt a = 16;
        a = a >> 2;
        Assert.Equal(4, a.Value);
    }

    [Fact]
    public void Equality_Operator()
    {
        AtomicInt a = 42;
        Assert.True(a == 42);
        Assert.False(a == 43);
    }

    [Fact]
    public void Inequality_Operator()
    {
        AtomicInt a = 42;
        Assert.True(a != 43);
        Assert.False(a != 42);
    }

    [Fact]
    public void GreaterThan_Operator()
    {
        AtomicInt a = 10;
        Assert.True(a > 5);
        Assert.False(a > 10);
        Assert.False(a > 15);
    }

    [Fact]
    public void LessThan_Operator()
    {
        AtomicInt a = 10;
        Assert.True(a < 15);
        Assert.False(a < 10);
        Assert.False(a < 5);
    }

    [Fact]
    public void GreaterThanOrEqual_Operator()
    {
        AtomicInt a = 10;
        Assert.True(a >= 10);
        Assert.True(a >= 5);
        Assert.False(a >= 15);
    }

    [Fact]
    public void LessThanOrEqual_Operator()
    {
        AtomicInt a = 10;
        Assert.True(a <= 10);
        Assert.True(a <= 15);
        Assert.False(a <= 5);
    }

    [Fact]
    public void Equals_Object_SameValue_ReturnsTrue()
    {
        AtomicInt a = 42;
        AtomicInt b = 42;
        Assert.True(a.Equals((object)b));
    }

    [Fact]
    public void Equals_Object_DifferentValue_ReturnsFalse()
    {
        AtomicInt a = 42;
        AtomicInt b = 99;
        Assert.False(a.Equals((object)b));
    }

    [Fact]
    public void Equals_Object_NonAtomicInt_ReturnsFalse()
    {
        AtomicInt a = 42;
        Assert.False(a.Equals("not an AtomicInt"));
        Assert.False(a.Equals(null));
    }

    [Fact]
    public void GetHashCode_SameValue_SameHash()
    {
        AtomicInt a = 42;
        AtomicInt b = 42;
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void GetHashCode_DifferentValue_DifferentHash()
    {
        AtomicInt a = 42;
        AtomicInt b = 99;
        Assert.NotEqual(a.GetHashCode(), b.GetHashCode());
    }
}
