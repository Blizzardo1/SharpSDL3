using SharpSDL3;
using SharpSDL3.Enums;
using SharpSDL3.Structs;
using Xunit;
using Xunit.Abstractions;

namespace SharpSDL3.Tests;

[Collection("SDL3")]
public class NativeSurfaceTests
{
    private readonly SdlFixture _sdl;
    private readonly ITestOutputHelper _output;

    public NativeSurfaceTests(SdlFixture sdl, ITestOutputHelper output)
    {
        _sdl = sdl;
        _output = output;
    }

    private bool RequireSdl() { if (!_sdl.Available) { _output.WriteLine("SKIPPED"); return false; } return true; }

    [Fact]
    public void Surface_CreateAndDestroy_MultipleFormats()
    {
        if (!RequireSdl()) return;
        var formats = new[] { PixelFormat.Rgba8888, PixelFormat.Argb8888, PixelFormat.Bgra8888 };
        foreach (var fmt in formats)
        {
            nint surface = Sdl.CreateSurface(64, 64, fmt);
            Assert.NotEqual(nint.Zero, surface);
            Sdl.DestroySurface(surface);
        }
    }

    [Fact]
    public void Surface_ClearWithColor()
    {
        if (!RequireSdl()) return;
        nint surface = Sdl.CreateSurface(32, 32, PixelFormat.Rgba8888);
        Assert.True(Sdl.ClearSurface(surface, 1.0f, 0.0f, 0.0f, 1.0f));
        Sdl.DestroySurface(surface);
    }

    [Fact]
    public void Surface_WriteAndReadPixel()
    {
        if (!RequireSdl()) return;
        nint surface = Sdl.CreateSurface(16, 16, PixelFormat.Rgba8888);

        var writeColor = new Color { R = 200, G = 100, B = 50, A = 255 };
        Assert.True(Sdl.WriteSurfacePixel(surface, 5, 5, writeColor));

        var readColor = Sdl.ReadSurfacePixel(surface, 5, 5);
        Assert.Equal(writeColor.R, readColor.R);
        Assert.Equal(writeColor.G, readColor.G);
        Assert.Equal(writeColor.B, readColor.B);
        Assert.Equal(writeColor.A, readColor.A);

        Sdl.DestroySurface(surface);
    }

    [Fact]
    public void Surface_WriteAndReadPixel_Components()
    {
        if (!RequireSdl()) return;
        nint surface = Sdl.CreateSurface(16, 16, PixelFormat.Rgba8888);

        Assert.True(Sdl.WriteSurfacePixel(surface, 0, 0, 255, 128, 64, 255));
        Assert.True(Sdl.ReadSurfacePixel(surface, 0, 0, out byte r, out byte g, out byte b, out byte a));
        Assert.Equal(255, r);
        Assert.Equal(128, g);
        Assert.Equal(64, b);
        Assert.Equal(255, a);

        Sdl.DestroySurface(surface);
    }

    [Fact]
    public void Surface_AlphaMod()
    {
        if (!RequireSdl()) return;
        nint surface = Sdl.CreateSurface(32, 32, PixelFormat.Rgba8888);

        Assert.True(Sdl.SetSurfaceAlphaMod(surface, 128));
        Assert.True(Sdl.GetSurfaceAlphaMod(surface, out byte alpha));
        Assert.Equal(128, alpha);

        Sdl.DestroySurface(surface);
    }

    [Fact]
    public void Surface_ColorMod()
    {
        if (!RequireSdl()) return;
        nint surface = Sdl.CreateSurface(32, 32, PixelFormat.Rgba8888);

        Assert.True(Sdl.SetSurfaceColorMod(surface, 200, 100, 50));
        Assert.True(Sdl.GetSurfaceColorMod(surface, out byte r, out byte g, out byte b));
        Assert.Equal(200, r);
        Assert.Equal(100, g);
        Assert.Equal(50, b);

        Sdl.DestroySurface(surface);
    }

    [Fact]
    public void Surface_ColorKey()
    {
        if (!RequireSdl()) return;
        nint surface = Sdl.CreateSurface(32, 32, PixelFormat.Rgba8888);

        uint key = Sdl.MapSurfaceRgba(surface, 255, 0, 255, 255);
        Assert.True(Sdl.SetSurfaceColorKey(surface, true, key));
        Assert.True(Sdl.SurfaceHasColorKey(surface));
        Assert.True(Sdl.GetSurfaceColorKey(surface, out uint readKey));
        Assert.Equal(key, readKey);

        Sdl.DestroySurface(surface);
    }

    [Fact]
    public void Surface_ClipRect()
    {
        if (!RequireSdl()) return;
        nint surface = Sdl.CreateSurface(64, 64, PixelFormat.Rgba8888);

        var clip = new Rect { X = 10, Y = 10, W = 20, H = 20 };
        Assert.True(Sdl.SetSurfaceClipRect(surface, ref clip));
        Assert.True(Sdl.GetSurfaceClipRect(surface, out Rect readClip));
        Assert.Equal(10, readClip.X);
        Assert.Equal(10, readClip.Y);
        Assert.Equal(20, readClip.W);
        Assert.Equal(20, readClip.H);

        Sdl.DestroySurface(surface);
    }

    [Fact]
    public void Surface_Blit_VariousScaleModes()
    {
        if (!RequireSdl()) return;
        nint src = Sdl.CreateSurface(32, 32, PixelFormat.Rgba8888);
        nint dst = Sdl.CreateSurface(64, 64, PixelFormat.Rgba8888);

        Assert.True(Sdl.BlitSurface(src, nint.Zero, dst, nint.Zero));
        Assert.True(Sdl.BlitSurfaceScaled(src, nint.Zero, dst, nint.Zero, ScaleMode.Linear));

        Sdl.DestroySurface(dst);
        Sdl.DestroySurface(src);
    }

    [Fact]
    public void Surface_FillRect()
    {
        if (!RequireSdl()) return;
        nint surface = Sdl.CreateSurface(64, 64, PixelFormat.Rgba8888);
        uint color = Sdl.MapSurfaceRgba(surface, 255, 0, 0, 255);

        var rect = new Rect { X = 0, Y = 0, W = 32, H = 32 };
        Assert.True(Sdl.FillSurfaceRect(surface, rect, color));

        Sdl.DestroySurface(surface);
    }

    [Fact]
    public void Surface_FillRects()
    {
        if (!RequireSdl()) return;
        nint surface = Sdl.CreateSurface(64, 64, PixelFormat.Rgba8888);
        uint color = Sdl.MapSurfaceRgba(surface, 0, 255, 0, 255);

        var rects = new Rect[]
        {
            new Rect { X = 0, Y = 0, W = 16, H = 16 },
            new Rect { X = 16, Y = 16, W = 16, H = 16 }
        };
        Assert.True(Sdl.FillSurfaceRects(surface, rects, color));

        Sdl.DestroySurface(surface);
    }

    [Fact]
    public void Surface_Duplicate()
    {
        if (!RequireSdl()) return;
        nint surface = Sdl.CreateSurface(32, 32, PixelFormat.Rgba8888);
        Sdl.WriteSurfacePixel(surface, 0, 0, 100, 200, 50, 255);

        nint copy = Sdl.DuplicateSurface(surface);
        Assert.NotEqual(nint.Zero, copy);

        var pixel = Sdl.ReadSurfacePixel(copy, 0, 0);
        Assert.Equal(100, pixel.R);
        Assert.Equal(200, pixel.G);
        Assert.Equal(50, pixel.B);

        Sdl.DestroySurface(copy);
        Sdl.DestroySurface(surface);
    }

    [Fact]
    public void Surface_ConvertFormat()
    {
        if (!RequireSdl()) return;
        nint surface = Sdl.CreateSurface(32, 32, PixelFormat.Rgba8888);
        nint converted = Sdl.ConvertSurface(surface, PixelFormat.Argb8888);
        Assert.NotEqual(nint.Zero, converted);

        Sdl.DestroySurface(converted);
        Sdl.DestroySurface(surface);
    }

    [Fact]
    public void Surface_FlipHorizontalAndVertical()
    {
        if (!RequireSdl()) return;
        nint surface = Sdl.CreateSurface(32, 32, PixelFormat.Rgba8888);
        Assert.True(Sdl.FlipSurface(surface, FlipMode.Horizontal));
        Assert.True(Sdl.FlipSurface(surface, FlipMode.Vertical));
        Sdl.DestroySurface(surface);
    }

    [Fact]
    public void Surface_LockUnlock()
    {
        if (!RequireSdl()) return;
        nint surface = Sdl.CreateSurface(32, 32, PixelFormat.Rgba8888);
        Assert.True(Sdl.LockSurface(surface));
        Sdl.UnlockSurface(surface);
        Sdl.DestroySurface(surface);
    }

    [Fact]
    public void Surface_GetProperties()
    {
        if (!RequireSdl()) return;
        nint surface = Sdl.CreateSurface(32, 32, PixelFormat.Rgba8888);
        uint props = Sdl.GetSurfaceProperties(surface);
        Assert.True(props > 0);
        Sdl.DestroySurface(surface);
    }

    [Fact]
    public void Palette_CreateAndDestroy()
    {
        if (!RequireSdl()) return;
        nint palette = Sdl.CreatePalette(256);
        Assert.NotEqual(nint.Zero, palette);
        Sdl.DestroyPalette(palette);
    }

    [Fact]
    public void PixelFormat_GetName()
    {
        if (!RequireSdl()) return;
        string name = Sdl.GetPixelFormatName(PixelFormat.Rgba8888);
        Assert.False(string.IsNullOrEmpty(name));
        Assert.Contains("RGBA", name, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void MapSurfaceRgba_ReturnsNonZero()
    {
        if (!RequireSdl()) return;
        nint surface = Sdl.CreateSurface(8, 8, PixelFormat.Rgba8888);
        uint pixel = Sdl.MapSurfaceRgba(surface, 255, 128, 64, 255);
        Assert.NotEqual(0u, pixel);
        Sdl.DestroySurface(surface);
    }
}
