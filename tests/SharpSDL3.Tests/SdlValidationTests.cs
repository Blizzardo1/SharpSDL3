using SharpSDL3;
using SharpSDL3.Enums;
using SharpSDL3.Structs;
using static SharpSDL3.Delegates;
using Xunit;

namespace SharpSDL3.Tests;

/// <summary>
/// Additional Sdl.cs validation guard tests for broader coverage.
/// Tests methods from the middle and end of Sdl.cs.
/// </summary>
public class SdlValidationTests
{
    // --- Window creation guards ---

    [Fact]
    public void CreateWindow_NullTitle_ReturnsZero()
    {
        Assert.Equal(nint.Zero, Sdl.CreateWindow(null!, 800, 600, 0));
    }

    [Fact]
    public void CreateWindow_EmptyTitle_ReturnsZero()
    {
        Assert.Equal(nint.Zero, Sdl.CreateWindow("", 800, 600, 0));
    }

    [Fact]
    public void CreateWindowWithProperties_ZeroProps_ReturnsZero()
    {
        Assert.Equal(nint.Zero, Sdl.CreateWindowWithProperties(0));
    }

    // --- Thread guards ---

    [Fact]
    public void CreateThreadRuntime_NullFunction_ReturnsZero()
    {
        Assert.Equal(nint.Zero,
            Sdl.CreateThreadRuntime(null!, "test", nint.Zero, nint.Zero, nint.Zero));
    }

    [Fact]
    public void CreateThreadWithPropertiesRuntime_ZeroProps_ReturnsZero()
    {
        Assert.Equal(nint.Zero,
            Sdl.CreateThreadWithPropertiesRuntime(0, (nint)1, (nint)1));
    }

    [Fact]
    public void CreateThreadWithPropertiesRuntime_NullBeginThread_ReturnsZero()
    {
        Assert.Equal(nint.Zero,
            Sdl.CreateThreadWithPropertiesRuntime(1, nint.Zero, (nint)1));
    }

    [Fact]
    public void CreateThreadWithPropertiesRuntime_NullEndThread_ReturnsZero()
    {
        Assert.Equal(nint.Zero,
            Sdl.CreateThreadWithPropertiesRuntime(1, (nint)1, nint.Zero));
    }

    // --- Surface operation guards ---

    [Fact]
    public void CreateSurfacePalette_NullSurface_ReturnsZero()
    {
        Assert.Equal(nint.Zero, Sdl.CreateSurfacePalette(nint.Zero));
    }

    [Fact]
    public void DuplicateSurface_NullSurface_ReturnsZero()
    {
        Assert.Equal(nint.Zero, Sdl.DuplicateSurface(nint.Zero));
    }

    [Fact]
    public void FlipSurface_NullSurface_ReturnsFalse()
    {
        Assert.False(Sdl.FlipSurface(nint.Zero, FlipMode.Horizontal));
    }

    [Fact]
    public void FillSurfaceRect_NullDst_ReturnsFalse()
    {
        Assert.False(Sdl.FillSurfaceRect(nint.Zero, new Rect(), 0));
    }

    [Fact]
    public void FillSurfaceRects_NullDst_ReturnsFalse()
    {
        var rects = new Rect[] { new Rect { X = 0, Y = 0, W = 10, H = 10 } };
        Assert.False(Sdl.FillSurfaceRects(nint.Zero, rects, 0));
    }

    [Fact]
    public void FillSurfaceRects_EmptyRects_ReturnsFalse()
    {
        Assert.False(Sdl.FillSurfaceRects((nint)1, Span<Rect>.Empty, 0));
    }

    // --- Window operation guards ---

    [Fact]
    public void FlashWindow_NullWindow_ReturnsFalse()
    {
        Assert.False(Sdl.FlashWindow(nint.Zero, FlashOperation.Briefly));
    }

    [Fact]
    public void DestroyWindowSurface_NullWindow_ReturnsFalse()
    {
        Assert.False(Sdl.DestroyWindowSurface(nint.Zero));
    }

    // --- EnumerateProperties guards ---

    [Fact]
    public void EnumerateProperties_ZeroProps_ReturnsFalse()
    {
        SdlEnumeratePropertiesCallback cb = (nint _, uint _, nint _) => { };
        Assert.False(Sdl.EnumerateProperties(0, cb, nint.Zero));
    }

    [Fact]
    public void EnumerateProperties_NullCallback_ReturnsFalse()
    {
        Assert.False(Sdl.EnumerateProperties(1, null!, nint.Zero));
    }

    // --- EnterAppMainCallbacks guards ---

    [Fact]
    public void EnterAppMainCallbacks_NullAppInit_ThrowsArgumentNullException()
    {
        SdlAppIterateFunc iter = (_) => AppResult.Continue;
        SdlAppEventFunc evt = (_, _) => AppResult.Continue;
        SdlAppQuitFunc quit = (_, _) => { };

        Assert.Throws<ArgumentNullException>(() =>
            Sdl.EnterAppMainCallbacks(0, nint.Zero, null!, iter, evt, quit));
    }

    [Fact]
    public void EnterAppMainCallbacks_NullAppIter_ThrowsArgumentNullException()
    {
        SdlAppInitFunc init = (_, _, _) => AppResult.Continue;
        SdlAppEventFunc evt = (_, _) => AppResult.Continue;
        SdlAppQuitFunc quit = (_, _) => { };

        Assert.Throws<ArgumentNullException>(() =>
            Sdl.EnterAppMainCallbacks(0, nint.Zero, init, null!, evt, quit));
    }
}
