using SharpSDL3;
using SharpSDL3.Enums;
using SharpSDL3.Structs;
using static SharpSDL3.Delegates;
using Xunit;

namespace SharpSDL3.Tests;

/// <summary>
/// Tests for Events.cs validation guards and event type checks.
/// </summary>
public class EventsTests
{
    [Fact]
    public void AddEventWatch_NullFilter_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            Sdl.AddEventWatch(null!, nint.Zero));
    }

    [Fact]
    public void EventEnabled_ZeroType_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.EventEnabled(0));
    }

    [Fact]
    public void FilterEvents_NullFilter_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            Sdl.FilterEvents(null!, nint.Zero));
    }

    [Fact]
    public void FlushEvent_ZeroType_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.FlushEvent(0));
    }

    [Fact]
    public void FlushEvents_ZeroMinType_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.FlushEvents(0, 100));
    }

    [Fact]
    public void FlushEvents_ZeroMaxType_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.FlushEvents(100, 0));
    }

    [Fact]
    public void FlushEvents_MinGreaterThanMax_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.FlushEvents(200, 100));
    }

    [Fact]
    public void GetWindowFromEvent_ZeroEventType_ThrowsArgumentException()
    {
        var evt = new Event { Type = 0 };
        Assert.Throws<ArgumentException>(() =>
            Sdl.GetWindowFromEvent(ref evt));
    }

    [Fact]
    public void HasEvent_ZeroType_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.HasEvent(0));
    }

    [Fact]
    public void HasEvents_ZeroMinType_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.HasEvents(0, 100));
    }

    [Fact]
    public void HasEvents_ZeroMaxType_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.HasEvents(100, 0));
    }

    [Fact]
    public void HasEvents_MinGreaterThanMax_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.HasEvents(200, 100));
    }
}
