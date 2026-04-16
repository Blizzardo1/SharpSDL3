using SharpSDL3;
using Xunit;

namespace SharpSDL3.Tests;

/// <summary>
/// Tests for Mouse.cs validation guards.
/// </summary>
public class MouseTests
{
    [Fact]
    public void GetMouseNameForId_ZeroId_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.GetMouseNameForId(0));
    }
}
