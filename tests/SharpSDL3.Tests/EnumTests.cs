using SharpSDL3.Enums;
using Xunit;

namespace SharpSDL3.Tests;

/// <summary>
/// Tests that enum definitions have expected values and are consistent.
/// </summary>
public class EnumTests
{
    // --- LogCategory ---

    [Theory]
    [InlineData(LogCategory.Application, 0)]
    [InlineData(LogCategory.Error, 1)]
    [InlineData(LogCategory.Assert, 2)]
    [InlineData(LogCategory.System, 3)]
    [InlineData(LogCategory.Audio, 4)]
    [InlineData(LogCategory.Video, 5)]
    [InlineData(LogCategory.Render, 6)]
    [InlineData(LogCategory.Input, 7)]
    [InlineData(LogCategory.Test, 8)]
    [InlineData(LogCategory.Gpu, 9)]
    [InlineData(LogCategory.Custom, 19)]
    public void LogCategory_HasExpectedValues(LogCategory cat, int expected)
    {
        Assert.Equal(expected, (int)cat);
    }

    // --- LogPriority ---

    [Theory]
    [InlineData(LogPriority.Invalid, 0)]
    [InlineData(LogPriority.Trace, 1)]
    [InlineData(LogPriority.Verbose, 2)]
    [InlineData(LogPriority.Debug, 3)]
    [InlineData(LogPriority.Info, 4)]
    [InlineData(LogPriority.Warn, 5)]
    [InlineData(LogPriority.Error, 6)]
    [InlineData(LogPriority.Critical, 7)]
    [InlineData(LogPriority.Count, 8)]
    public void LogPriority_HasExpectedValues(LogPriority pri, int expected)
    {
        Assert.Equal(expected, (int)pri);
    }

    // --- EventType ---

    [Fact]
    public void EventType_First_IsZero()
    {
        Assert.Equal(0u, (uint)EventType.First);
    }

    [Fact]
    public void EventType_Quit_Is0x100()
    {
        Assert.Equal(0x100u, (uint)EventType.Quit);
    }

    [Fact]
    public void EventType_DisplayOrientation_Is0x151()
    {
        Assert.Equal(0x151u, (uint)EventType.DisplayOrientation);
    }

    // --- WindowFlags ---

    [Fact]
    public void WindowFlags_IsFlagsEnum()
    {
        Assert.True(typeof(WindowFlags).IsDefined(typeof(FlagsAttribute), false));
    }

    [Theory]
    [InlineData(WindowFlags.Fullscreen, 0x1UL)]
    [InlineData(WindowFlags.Opengl, 0x2UL)]
    [InlineData(WindowFlags.Hidden, 0x08UL)]
    [InlineData(WindowFlags.Borderless, 0x10UL)]
    [InlineData(WindowFlags.Resizable, 0x20UL)]
    [InlineData(WindowFlags.Minimized, 0x40UL)]
    [InlineData(WindowFlags.Maximized, 0x080UL)]
    [InlineData(WindowFlags.Vulkan, 0x10000000UL)]
    [InlineData(WindowFlags.Metal, 0x20000000UL)]
    public void WindowFlags_HasExpectedValues(WindowFlags flag, ulong expected)
    {
        Assert.Equal(expected, (ulong)flag);
    }

    [Fact]
    public void WindowFlags_CanCombine()
    {
        var combined = WindowFlags.Resizable | WindowFlags.Opengl;
        Assert.True(combined.HasFlag(WindowFlags.Resizable));
        Assert.True(combined.HasFlag(WindowFlags.Opengl));
        Assert.False(combined.HasFlag(WindowFlags.Vulkan));
    }
}
