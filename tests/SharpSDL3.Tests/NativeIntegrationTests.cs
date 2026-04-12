using SharpSDL3;
using SharpSDL3.Enums;
using SharpSDL3.Structs;
using Xunit;
using Xunit.Abstractions;

namespace SharpSDL3.Tests;

/// <summary>
/// Integration tests that require the native SDL3 library.
/// Each test returns early (passes silently) when SDL3 is not installed.
/// When SDL3 is available, these exercise real native calls end-to-end.
/// </summary>
[Collection("SDL3")]
public class NativeIntegrationTests
{
    private readonly SdlFixture _sdl;
    private readonly ITestOutputHelper _output;

    public NativeIntegrationTests(SdlFixture sdl, ITestOutputHelper output)
    {
        _sdl = sdl;
        _output = output;
    }

    private bool RequireSdl()
    {
        if (!_sdl.Available)
        {
            _output.WriteLine("SKIPPED: SDL3 native library not available");
            return false;
        }
        return true;
    }

    // --- Init / Version ---

    [Fact]
    public void Init_Succeeded()
    {
        if (!RequireSdl()) return;
        Assert.True(_sdl.Available);
    }

    [Fact]
    public void GetVersion_ReturnsNonZero()
    {
        if (!RequireSdl()) return;
        int version = Sdl.GetVersion();
        Assert.True(version > 0, $"Expected positive version, got {version}");
    }

    [Fact]
    public void VersionNum_MatchesGetVersion()
    {
        if (!RequireSdl()) return;
        int version = Sdl.GetVersion();
        Assert.True(version >= Sdl.VersionNum(3, 0, 0),
            $"Version {version} is less than 3.0.0");
    }

    // --- Error handling ---

    [Fact]
    public void ClearError_ReturnsTrue()
    {
        if (!RequireSdl()) return;
        Assert.True(Sdl.ClearError());
    }

    [Fact]
    public void GetError_AfterClear_ReturnsEmpty()
    {
        if (!RequireSdl()) return;
        Sdl.ClearError();
        string error = Sdl.GetError();
        Assert.Equal("", error);
    }

    // --- Properties ---

    [Fact]
    public void CreateProperties_ReturnsNonZero()
    {
        if (!RequireSdl()) return;
        uint props = Sdl.CreateProperties();
        Assert.True(props > 0);
        Sdl.DestroyProperties(props);
    }

    [Fact]
    public void ClearProperty_OnValidProps_ReturnsTrue()
    {
        if (!RequireSdl()) return;
        uint props = Sdl.CreateProperties();
        bool result = Sdl.ClearProperty(props, "nonexistent");
        Assert.True(result);
        Sdl.DestroyProperties(props);
    }

    // --- Surface ---

    [Fact]
    public void CreateSurface_ValidArgs_ReturnsNonZero()
    {
        if (!RequireSdl()) return;
        nint surface = Sdl.CreateSurface(64, 64, PixelFormat.Rgba8888);
        Assert.NotEqual(nint.Zero, surface);
        Sdl.DestroySurface(surface);
    }

    [Fact]
    public void DuplicateSurface_ValidSurface_ReturnsNonZero()
    {
        if (!RequireSdl()) return;
        nint surface = Sdl.CreateSurface(32, 32, PixelFormat.Rgba8888);
        Assert.NotEqual(nint.Zero, surface);

        nint copy = Sdl.DuplicateSurface(surface);
        Assert.NotEqual(nint.Zero, copy);

        Sdl.DestroySurface(copy);
        Sdl.DestroySurface(surface);
    }

    [Fact]
    public void ClearSurface_ValidSurface_ReturnsTrue()
    {
        if (!RequireSdl()) return;
        nint surface = Sdl.CreateSurface(32, 32, PixelFormat.Rgba8888);
        Assert.NotEqual(nint.Zero, surface);

        bool result = Sdl.ClearSurface(surface, 1.0f, 0.0f, 0.0f, 1.0f);
        Assert.True(result);

        Sdl.DestroySurface(surface);
    }

    [Fact]
    public void FlipSurface_ValidSurface_ReturnsTrue()
    {
        if (!RequireSdl()) return;
        nint surface = Sdl.CreateSurface(32, 32, PixelFormat.Rgba8888);
        Assert.NotEqual(nint.Zero, surface);

        bool result = Sdl.FlipSurface(surface, FlipMode.Horizontal);
        Assert.True(result);

        Sdl.DestroySurface(surface);
    }

    [Fact]
    public void BlitSurface_TwoValidSurfaces_ReturnsTrue()
    {
        if (!RequireSdl()) return;
        nint src = Sdl.CreateSurface(32, 32, PixelFormat.Rgba8888);
        nint dst = Sdl.CreateSurface(64, 64, PixelFormat.Rgba8888);
        Assert.NotEqual(nint.Zero, src);
        Assert.NotEqual(nint.Zero, dst);

        bool result = Sdl.BlitSurface(src, nint.Zero, dst, nint.Zero);
        Assert.True(result);

        Sdl.DestroySurface(dst);
        Sdl.DestroySurface(src);
    }

    // --- Palette ---

    [Fact]
    public void CreatePalette_ValidCount_ReturnsNonZero()
    {
        if (!RequireSdl()) return;
        nint palette = Sdl.CreatePalette(256);
        Assert.NotEqual(nint.Zero, palette);
        Sdl.DestroyPalette(palette);
    }

    // --- Logging ---

    [Fact]
    public void Log_ValidMessage_DoesNotThrow()
    {
        if (!RequireSdl()) return;
        Sdl.Log("NativeIntegrationTests: test log message");
    }

    [Fact]
    public void LogCritical_ValidMessage_DoesNotThrow()
    {
        if (!RequireSdl()) return;
        Sdl.LogCritical(LogCategory.Test, "NativeIntegrationTests: critical test message");
    }

    [Fact]
    public void GetLogPriority_ValidCategory_ReturnsNonInvalid()
    {
        if (!RequireSdl()) return;
        var priority = Sdl.GetLogPriority(LogCategory.Application);
        Assert.NotEqual(LogPriority.Invalid, priority);
    }

    // --- Timer ---

    [Fact]
    public void GetTicks_ReturnsNonZero()
    {
        if (!RequireSdl()) return;
        ulong ticks = Sdl.GetTicks();
        Assert.True(ticks > 0);
    }
}
