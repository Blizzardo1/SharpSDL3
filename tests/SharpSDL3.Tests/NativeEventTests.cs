using SharpSDL3;
using SharpSDL3.Enums;
using SharpSDL3.Structs;
using Xunit;
using Xunit.Abstractions;

namespace SharpSDL3.Tests;

[Collection("SDL3")]
public class NativeEventTests
{
    private readonly SdlFixture _sdl;
    private readonly ITestOutputHelper _output;

    public NativeEventTests(SdlFixture sdl, ITestOutputHelper output)
    {
        _sdl = sdl;
        _output = output;
    }

    private bool RequireSdl() { if (!_sdl.Available) { _output.WriteLine("SKIPPED"); return false; } return true; }

    [Fact]
    public void PollEvent_DoesNotThrow()
    {
        if (!RequireSdl()) return;
        Sdl.PumpEvents();
        Sdl.PollEvent(out Event evt);
        // May or may not have events, just verify no crash
    }

    [Fact]
    public void PushEvent_UserEvent()
    {
        if (!RequireSdl()) return;
        uint customType = Sdl.RegisterEvents(1);
        Assert.True(customType > 0);

        var evt = new Event();
        evt.Type = (EventType)customType;
        Assert.True(Sdl.PushEvent(ref evt));

        Sdl.PumpEvents();
        bool found = Sdl.HasEvent(customType);
        Assert.True(found);

        Sdl.FlushEvent(customType);
        Assert.False(Sdl.HasEvent(customType));
    }

    [Fact]
    public void RegisterEvents_ReturnsUniqueIds()
    {
        if (!RequireSdl()) return;
        uint id1 = Sdl.RegisterEvents(1);
        uint id2 = Sdl.RegisterEvents(1);
        Assert.NotEqual(id1, id2);
        Assert.True(id1 > 0);
        Assert.True(id2 > 0);
    }

    [Fact]
    public void EventEnabled_DefaultEnabled()
    {
        if (!RequireSdl()) return;
        Assert.True(Sdl.EventEnabled((uint)EventType.Quit));
    }

    [Fact]
    public void SetEventEnabled_DisableAndReenable()
    {
        if (!RequireSdl()) return;
        uint type = (uint)EventType.Quit;

        Sdl.SetEventEnabled(type, false);
        Assert.False(Sdl.EventEnabled(type));

        Sdl.SetEventEnabled(type, true);
        Assert.True(Sdl.EventEnabled(type));
    }

    [Fact]
    public void FlushEvents_Range()
    {
        if (!RequireSdl()) return;
        // Push two custom events
        uint t1 = Sdl.RegisterEvents(2);
        var evt1 = new Event { Type = (EventType)t1 };
        var evt2 = new Event { Type = (EventType)(t1 + 1) };
        Sdl.PushEvent(ref evt1);
        Sdl.PushEvent(ref evt2);

        Sdl.FlushEvents(t1, t1 + 1);
        Assert.False(Sdl.HasEvents(t1, t1 + 1));
    }

    [Fact]
    public void HasEvents_Range()
    {
        if (!RequireSdl()) return;
        uint t = Sdl.RegisterEvents(1);
        Assert.False(Sdl.HasEvents(t, t));

        var evt = new Event { Type = (EventType)t };
        Sdl.PushEvent(ref evt);
        Assert.True(Sdl.HasEvents(t, t));
        Sdl.FlushEvent(t);
    }
}
