using SharpSDL3;
using SharpSDL3.Enums;
using SharpSDL3.Structs;
using static SharpSDL3.Delegates;
using static SharpSDL3.Tests.TestHelpers;
using Xunit;

namespace SharpSDL3.Tests;

/// <summary>
/// Additional Sdl.cs validation guard tests for broader coverage.
/// </summary>
public class SdlValidationTests
{
    // --- Window creation guards ---

    [Fact]
    public void CreateWindow_NullTitle_ReturnsZero() =>
        AssertZeroOrNativeNotFound(() => Sdl.CreateWindow(null!, 800, 600, 0));

    [Fact]
    public void CreateWindow_EmptyTitle_ReturnsZero() =>
        AssertZeroOrNativeNotFound(() => Sdl.CreateWindow("", 800, 600, 0));

    [Fact]
    public void CreateWindowWithProperties_ZeroProps_ReturnsZero() =>
        AssertZeroOrNativeNotFound(() => Sdl.CreateWindowWithProperties(0));

    // --- Thread guards ---

    [Fact]
    public void CreateThreadRuntime_NullFunction_ReturnsZero() =>
        AssertZeroOrNativeNotFound(() => Sdl.CreateThreadRuntime(null!, "test", nint.Zero, nint.Zero, nint.Zero));

    [Fact]
    public void CreateThreadWithPropertiesRuntime_ZeroProps_ReturnsZero() =>
        AssertZeroOrNativeNotFound(() => Sdl.CreateThreadWithPropertiesRuntime(0, (nint)1, (nint)1));

    [Fact]
    public void CreateThreadWithPropertiesRuntime_NullBeginThread_ReturnsZero() =>
        AssertZeroOrNativeNotFound(() => Sdl.CreateThreadWithPropertiesRuntime(1, nint.Zero, (nint)1));

    [Fact]
    public void CreateThreadWithPropertiesRuntime_NullEndThread_ReturnsZero() =>
        AssertZeroOrNativeNotFound(() => Sdl.CreateThreadWithPropertiesRuntime(1, (nint)1, nint.Zero));

    // --- Surface operation guards ---

    [Fact]
    public void CreateSurfacePalette_NullSurface_ReturnsZero() =>
        AssertZeroOrNativeNotFound(() => Sdl.CreateSurfacePalette(nint.Zero));

    [Fact]
    public void DuplicateSurface_NullSurface_ReturnsZero() =>
        AssertZeroOrNativeNotFound(() => Sdl.DuplicateSurface(nint.Zero));

    [Fact]
    public void FlipSurface_NullSurface_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.FlipSurface(nint.Zero, FlipMode.Horizontal));

    [Fact]
    public void FillSurfaceRect_NullDst_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.FillSurfaceRect(nint.Zero, new Rect(), 0));

    [Fact]
    public void FillSurfaceRects_NullDst_ReturnsFalse()
    {
        var rects = new Rect[] { new Rect { X = 0, Y = 0, W = 10, H = 10 } };
        AssertFalseOrNativeNotFound(() => Sdl.FillSurfaceRects(nint.Zero, rects, 0));
    }

    [Fact]
    public void FillSurfaceRects_EmptyRects_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.FillSurfaceRects((nint)1, Span<Rect>.Empty, 0));

    // --- Window operation guards ---

    [Fact]
    public void FlashWindow_NullWindow_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.FlashWindow(nint.Zero, FlashOperation.Briefly));

    [Fact]
    public void DestroyWindowSurface_NullWindow_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.DestroyWindowSurface(nint.Zero));

    // --- EnumerateProperties guards ---

    [Fact]
    public void EnumerateProperties_ZeroProps_ReturnsFalse()
    {
        SdlEnumeratePropertiesCallback cb = (nint _, uint _, nint _) => { };
        AssertFalseOrNativeNotFound(() => Sdl.EnumerateProperties(0, cb, nint.Zero));
    }

    [Fact]
    public void EnumerateProperties_NullCallback_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.EnumerateProperties(1, null!, nint.Zero));

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
