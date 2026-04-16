using SharpSDL3;
using SharpSDL3.Enums;
using Xunit;
using Xunit.Abstractions;

namespace SharpSDL3.Tests;

[Collection("SDL3")]
public class NativePropertyTests
{
    private readonly SdlFixture _sdl;
    private readonly ITestOutputHelper _output;

    public NativePropertyTests(SdlFixture sdl, ITestOutputHelper output)
    {
        _sdl = sdl;
        _output = output;
    }

    private bool RequireSdl() { if (!_sdl.Available) { _output.WriteLine("SKIPPED"); return false; } return true; }

    [Fact]
    public void Properties_FullLifecycle()
    {
        if (!RequireSdl()) return;
        uint props = Sdl.CreateProperties();
        Assert.True(props > 0);

        Assert.True(Sdl.SetBooleanProperty(props, "test.bool", true));
        Assert.True(Sdl.GetBooleanProperty(props, "test.bool", false));
        Assert.True(Sdl.HasProperty(props, "test.bool"));

        Assert.True(Sdl.SetNumberProperty(props, "test.num", 42));
        Assert.Equal(42, Sdl.GetNumberProperty(props, "test.num", 0));

        Assert.True(Sdl.SetFloatProperty(props, "test.float", 3.14f));
        Assert.Equal(3.14f, Sdl.GetFloatProperty(props, "test.float", 0f), 0.001f);

        Assert.True(Sdl.SetStringProperty(props, "test.str", "hello"));
        Assert.Equal("hello", Sdl.GetStringProperty(props, "test.str", ""));

        Assert.True(Sdl.ClearProperty(props, "test.bool"));
        Assert.False(Sdl.HasProperty(props, "test.bool"));

        Sdl.DestroyProperties(props);
    }

    [Fact]
    public void Properties_DefaultValues()
    {
        if (!RequireSdl()) return;
        uint props = Sdl.CreateProperties();

        Assert.False(Sdl.GetBooleanProperty(props, "nonexistent", false));
        Assert.True(Sdl.GetBooleanProperty(props, "nonexistent", true));
        Assert.Equal(99, Sdl.GetNumberProperty(props, "nonexistent", 99));
        Assert.Equal(1.5f, Sdl.GetFloatProperty(props, "nonexistent", 1.5f));
        Assert.Equal("default", Sdl.GetStringProperty(props, "nonexistent", "default"));

        Sdl.DestroyProperties(props);
    }

    [Fact]
    public void Properties_LockUnlock()
    {
        if (!RequireSdl()) return;
        uint props = Sdl.CreateProperties();

        Assert.True(Sdl.LockProperties(props));
        Assert.True(Sdl.SetStringProperty(props, "locked.key", "value"));
        Sdl.UnlockProperties(props);

        Assert.Equal("value", Sdl.GetStringProperty(props, "locked.key", ""));
        Sdl.DestroyProperties(props);
    }

    [Fact]
    public void Hints_SetGetReset()
    {
        if (!RequireSdl()) return;
        Assert.True(Sdl.SetHint("SDL_TEST_HINT", "test_value"));
        string val = Sdl.GetHint("SDL_TEST_HINT");
        Assert.Equal("test_value", val);

        Assert.True(Sdl.ResetHint("SDL_TEST_HINT"));
    }

    [Fact]
    public void Error_SetClearGet()
    {
        if (!RequireSdl()) return;
        Sdl.ClearError();
        Assert.Equal("", Sdl.GetError());

        Sdl.SetError("test error %s", "details");
        string err = Sdl.GetError();
        Assert.Contains("test error", err);

        Sdl.ClearError();
        Assert.Equal("", Sdl.GetError());
    }

    [Fact]
    public void AppMetadata_SetAndGet()
    {
        if (!RequireSdl()) return;
        Assert.True(Sdl.SetAppMetadata("TestApp", "1.0.0", "com.test.app"));
        Assert.Equal("TestApp", Sdl.GetAppMetadataProperty("name"));
        Assert.Equal("1.0.0", Sdl.GetAppMetadataProperty("version"));
        Assert.Equal("com.test.app", Sdl.GetAppMetadataProperty("identifier"));
    }

    [Fact]
    public void Clipboard_TextRoundTrip()
    {
        if (!RequireSdl()) return;
        Assert.True(Sdl.SetClipboardText("test clipboard"));
        Assert.True(Sdl.HasClipboardText());
        Assert.Equal("test clipboard", Sdl.GetClipboardText());
        Assert.True(Sdl.ClearClipboardData());
    }
}
