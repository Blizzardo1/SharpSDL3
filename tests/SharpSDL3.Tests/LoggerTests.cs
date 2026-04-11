using SharpSDL3;
using SharpSDL3.Enums;
using Xunit;

namespace SharpSDL3.Tests;

/// <summary>
/// Tests for Logger.cs validation guards.
/// </summary>
public class LoggerTests
{
    [Fact]
    public void Log_NullMessage_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Sdl.Log(null!));
    }

    [Fact]
    public void Log_EmptyMessage_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Sdl.Log(""));
    }

    [Fact]
    public void Log_WhitespaceMessage_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Sdl.Log("   "));
    }

    [Fact]
    public void LogCritical_NullMessage_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.LogCritical(LogCategory.Application, null!));
    }

    [Fact]
    public void LogCritical_EmptyMessage_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.LogCritical(LogCategory.Application, ""));
    }

    [Fact]
    public void GetLogPriority_InvalidCategory_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            Sdl.GetLogPriority(0));
    }
}
