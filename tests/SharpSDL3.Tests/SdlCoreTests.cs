using System.Runtime.InteropServices;
using SharpSDL3;
using SharpSDL3.Enums;
using SharpSDL3.Structs;
using static SharpSDL3.Delegates;
using static SharpSDL3.Tests.TestHelpers;
using Xunit;

namespace SharpSDL3.Tests;

/// <summary>
/// Tests for Sdl.cs core utility methods and validation guards.
/// These test managed-only code paths that don't require the native SDL3 library.
/// </summary>
public class SdlCoreTests
{
    // --- VersionNum ---

    [Theory]
    [InlineData(1, 2, 3, 1002003)]
    [InlineData(0, 0, 0, 0)]
    [InlineData(3, 2, 0, 3002000)]
    [InlineData(10, 999, 999, 10999999)]
    [InlineData(1, 0, 0, 1000000)]
    [InlineData(0, 1, 0, 1000)]
    [InlineData(0, 0, 1, 1)]
    public void VersionNum_ReturnsCorrectValue(int major, int minor, int patch, int expected)
    {
        Assert.Equal(expected, Sdl.VersionNum(major, minor, patch));
    }

    // --- StructureToPointer / PointerToStructure round-trip ---

    [Fact]
    public unsafe void StructureToPointer_And_PointerToStructure_RoundTrip_Rect()
    {
        var rect = new Rect { X = 10, Y = 20, W = 640, H = 480 };
        nint ptr = Sdl.StructureToPointer(ref rect);

        try
        {
            Assert.NotEqual(nint.Zero, ptr);
            var result = Sdl.PointerToStructure<Rect>(ptr);
            Assert.Equal(10, result.X);
            Assert.Equal(20, result.Y);
            Assert.Equal(640, result.W);
            Assert.Equal(480, result.H);
        }
        finally
        {
            Marshal.FreeHGlobal(ptr);
        }
    }

    [Fact]
    public unsafe void StructureToPointer_And_PointerToStructure_RoundTrip_Color()
    {
        var color = new Color { R = 255, G = 128, B = 0, A = 200 };
        nint ptr = Sdl.StructureToPointer(ref color);

        try
        {
            Assert.NotEqual(nint.Zero, ptr);
            var result = Sdl.PointerToStructure<Color>(ptr);
            Assert.Equal(255, result.R);
            Assert.Equal(128, result.G);
            Assert.Equal(0, result.B);
            Assert.Equal(200, result.A);
        }
        finally
        {
            Marshal.FreeHGlobal(ptr);
        }
    }

    [Fact]
    public unsafe void StructureToPointer_And_PointerToStructure_RoundTrip_FRect()
    {
        var frect = new FRect { X = 1.5f, Y = 2.5f, W = 100.0f, H = 200.0f };
        nint ptr = Sdl.StructureToPointer(ref frect);

        try
        {
            var result = Sdl.PointerToStructure<FRect>(ptr);
            Assert.Equal(1.5f, result.X);
            Assert.Equal(2.5f, result.Y);
            Assert.Equal(100.0f, result.W);
            Assert.Equal(200.0f, result.H);
        }
        finally
        {
            Marshal.FreeHGlobal(ptr);
        }
    }

    [Fact]
    public unsafe void StructureToPointer_And_PointerToStructure_RoundTrip_AudioSpec()
    {
        var spec = new AudioSpec { Format = AudioFormat.S16Le, Channels = 2, Freq = 44100 };
        nint ptr = Sdl.StructureToPointer(ref spec);

        try
        {
            var result = Sdl.PointerToStructure<AudioSpec>(ptr);
            Assert.Equal(AudioFormat.S16Le, result.Format);
            Assert.Equal(2, result.Channels);
            Assert.Equal(44100, result.Freq);
        }
        finally
        {
            Marshal.FreeHGlobal(ptr);
        }
    }

    [Fact]
    public void PointerToStructure_NullPointer_ReturnsDefault()
    {
        var result = Sdl.PointerToStructure<Rect>(nint.Zero);
        Assert.Equal(0, result.X);
        Assert.Equal(0, result.Y);
        Assert.Equal(0, result.W);
        Assert.Equal(0, result.H);
    }

    // --- Null-pointer validation guards in Sdl.cs ---
    // Guards call LogWarn/LogError which P/Invokes into SDL3. In CI without the
    // native library, DllNotFoundException is expected and still proves the guard works.

    [Fact]
    public void AddSurfaceAlternateImage_NullSurface_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.AddSurfaceAlternateImage(nint.Zero, (nint)1));

    [Fact]
    public void AddSurfaceAlternateImage_NullImage_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.AddSurfaceAlternateImage((nint)1, nint.Zero));

    [Fact]
    public void AddSurfaceAlternateImage_BothNull_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.AddSurfaceAlternateImage(nint.Zero, nint.Zero));

    [Fact]
    public void BlitSurface_NullSource_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.BlitSurface(nint.Zero, nint.Zero, (nint)1, nint.Zero));

    [Fact]
    public void BlitSurface_NullDest_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.BlitSurface((nint)1, nint.Zero, nint.Zero, nint.Zero));

    [Fact]
    public void BlitSurface9Grid_NullSource_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.BlitSurface9Grid(nint.Zero, nint.Zero, 0, 0, 0, 0, 1.0f, ScaleMode.Linear, (nint)1, nint.Zero));

    [Fact]
    public void BlitSurface9Grid_NullDest_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.BlitSurface9Grid((nint)1, nint.Zero, 0, 0, 0, 0, 1.0f, ScaleMode.Linear, nint.Zero, nint.Zero));

    [Fact]
    public void BlitSurfaceScaled_NullSource_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.BlitSurfaceScaled(nint.Zero, nint.Zero, (nint)1, nint.Zero, ScaleMode.Linear));

    [Fact]
    public void BlitSurfaceTiled_NullSource_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.BlitSurfaceTiled(nint.Zero, nint.Zero, (nint)1, nint.Zero));

    [Fact]
    public void BlitSurfaceTiledWithScale_NullDest_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.BlitSurfaceTiledWithScale((nint)1, nint.Zero, 1.0f, ScaleMode.Linear, nint.Zero, nint.Zero));

    [Fact]
    public void BlitSurfaceUnchecked_NullSource_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.BlitSurfaceUnchecked(nint.Zero, nint.Zero, (nint)1, nint.Zero));

    [Fact]
    public void BlitSurfaceUncheckedScaled_NullDest_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.BlitSurfaceUncheckedScaled((nint)1, nint.Zero, nint.Zero, nint.Zero, ScaleMode.Linear));

    [Fact]
    public void ClearComposition_NullWindow_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.ClearComposition(nint.Zero));

    [Fact]
    public void ClearProperty_ZeroProps_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.ClearProperty(0, "test"));

    [Fact]
    public void ClearProperty_NullName_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.ClearProperty(1, ""));

    [Fact]
    public void ClearProperty_EmptyName_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.ClearProperty(1, string.Empty));

    [Fact]
    public void ClearSurface_NullSurface_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.ClearSurface(nint.Zero, 0, 0, 0, 1));

    [Fact]
    public void ConvertPixels_NullSrc_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.ConvertPixels(10, 10, PixelFormat.Rgba8888, nint.Zero, 40, PixelFormat.Rgba8888, (nint)1, 40));

    [Fact]
    public void ConvertPixels_NullDst_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.ConvertPixels(10, 10, PixelFormat.Rgba8888, (nint)1, 40, PixelFormat.Rgba8888, nint.Zero, 40));

    [Fact]
    public void ConvertSurface_NullSurface_ReturnsZero() =>
        AssertZeroOrNativeNotFound(() => Sdl.ConvertSurface(nint.Zero, PixelFormat.Rgba8888));

    [Fact]
    public void ConvertSurfaceAndColorspace_NullSurface_ReturnsZero() =>
        AssertZeroOrNativeNotFound(() => Sdl.ConvertSurfaceAndColorspace(nint.Zero, PixelFormat.Rgba8888, nint.Zero, Colorspace.SrgbLinear, 0));

    [Fact]
    public void CopyProperties_ZeroSrc_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.CopyProperties(0, 1));

    [Fact]
    public void CopyProperties_ZeroDst_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.CopyProperties(1, 0));

    [Fact]
    public void CreatePopupWindow_NullParent_ReturnsZero() =>
        AssertZeroOrNativeNotFound(() => Sdl.CreatePopupWindow(nint.Zero, 0, 0, 100, 100, WindowFlags.Tooltip));

    [Fact]
    public void CreatePopupWindow_InvalidSize_ReturnsZero()
    {
        AssertZeroOrNativeNotFound(() => Sdl.CreatePopupWindow((nint)1, 0, 0, 0, 100, WindowFlags.Tooltip));
        AssertZeroOrNativeNotFound(() => Sdl.CreatePopupWindow((nint)1, 0, 0, 100, -1, WindowFlags.Tooltip));
    }

    [Fact]
    public void CreateSurface_InvalidDimensions_ReturnsZero()
    {
        AssertZeroOrNativeNotFound(() => Sdl.CreateSurface(0, 100, PixelFormat.Rgba8888));
        AssertZeroOrNativeNotFound(() => Sdl.CreateSurface(100, 0, PixelFormat.Rgba8888));
        AssertZeroOrNativeNotFound(() => Sdl.CreateSurface(-1, 100, PixelFormat.Rgba8888));
    }

    [Fact]
    public void CreateSurfaceFrom_NullPixels_ReturnsZero() =>
        AssertZeroOrNativeNotFound(() => Sdl.CreateSurfaceFrom(100, 100, PixelFormat.Rgba8888, nint.Zero, 400));

    [Fact]
    public void AddHintCallback_NullName_ReturnsFalse()
    {
        SdlHintCallback cb = (nint _, nint _, nint _, nint _) => { };
        AssertFalseOrNativeNotFound(() => Sdl.AddHintCallback("", cb, nint.Zero));
    }

    [Fact]
    public void AddHintCallback_NullCallback_ReturnsFalse() =>
        AssertFalseOrNativeNotFound(() => Sdl.AddHintCallback("test", null!, nint.Zero));
}
