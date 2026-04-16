using SharpSDL3.Structs;
using Xunit;

namespace SharpSDL3.Tests;

/// <summary>
/// Tests for Color struct equality, operators, and hash code.
/// </summary>
public class ColorTests
{
    [Fact]
    public void Color_Equality_SameValues_ReturnsTrue()
    {
        var a = new Color { R = 255, G = 128, B = 64, A = 32 };
        var b = new Color { R = 255, G = 128, B = 64, A = 32 };
        Assert.True(a == b);
        Assert.False(a != b);
        Assert.True(a.Equals(b));
        Assert.True(a.Equals((object)b));
    }

    [Fact]
    public void Color_Equality_DifferentValues_ReturnsFalse()
    {
        var a = new Color { R = 255, G = 128, B = 64, A = 32 };
        var b = new Color { R = 0, G = 128, B = 64, A = 32 };
        Assert.False(a == b);
        Assert.True(a != b);
        Assert.False(a.Equals(b));
    }

    [Fact]
    public void Color_Equality_DiffG_ReturnsFalse()
    {
        var a = new Color { R = 255, G = 128, B = 64, A = 32 };
        var b = new Color { R = 255, G = 0, B = 64, A = 32 };
        Assert.False(a == b);
    }

    [Fact]
    public void Color_Equality_DiffB_ReturnsFalse()
    {
        var a = new Color { R = 255, G = 128, B = 64, A = 32 };
        var b = new Color { R = 255, G = 128, B = 0, A = 32 };
        Assert.False(a == b);
    }

    [Fact]
    public void Color_Equality_DiffA_ReturnsFalse()
    {
        var a = new Color { R = 255, G = 128, B = 64, A = 32 };
        var b = new Color { R = 255, G = 128, B = 64, A = 0 };
        Assert.False(a == b);
    }

    [Fact]
    public void Color_Equals_NonColorObject_ReturnsFalse()
    {
        var a = new Color { R = 255, G = 128, B = 64, A = 32 };
        Assert.False(a.Equals("not a color"));
        Assert.False(a.Equals(null));
    }

    [Fact]
    public void Color_GetHashCode_SameValues_SameHash()
    {
        var a = new Color { R = 255, G = 128, B = 64, A = 32 };
        var b = new Color { R = 255, G = 128, B = 64, A = 32 };
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void Color_GetHashCode_DifferentValues_DifferentHash()
    {
        var a = new Color { R = 255, G = 128, B = 64, A = 32 };
        var b = new Color { R = 0, G = 0, B = 0, A = 0 };
        Assert.NotEqual(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void Color_DefaultValues_AllZero()
    {
        var c = new Color();
        Assert.Equal(0, c.R);
        Assert.Equal(0, c.G);
        Assert.Equal(0, c.B);
        Assert.Equal(0, c.A);
    }
}
