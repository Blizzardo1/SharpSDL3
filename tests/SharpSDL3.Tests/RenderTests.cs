using SharpSDL3;
using SharpSDL3.Enums;
using Xunit;

namespace SharpSDL3.Tests;

/// <summary>
/// Tests for Render.cs validation guards.
/// </summary>
public class RenderTests
{
    [Fact]
    public void AddVulkanRenderSemaphores_NullRenderer_ThrowsSdlException()
    {
        Assert.Throws<SdlException>(() =>
            Sdl.AddVulkanRenderSemaphores(nint.Zero, 0, 0, 0));
    }

    [Fact]
    public void CreateRenderer_NullWindow_ReturnsZero()
    {
        Assert.Equal(nint.Zero, Sdl.CreateRenderer(nint.Zero, null));
    }

    [Fact]
    public void CreateSoftwareRenderer_NullSurface_ReturnsZero()
    {
        Assert.Equal(nint.Zero, Sdl.CreateSoftwareRenderer(nint.Zero));
    }

    [Fact]
    public void CreateWindowAndRenderer_EmptyTitle_ReturnsFalse()
    {
        bool result = Sdl.CreateWindowAndRenderer("", 800, 600, WindowFlags.Hidden,
            out nint window, out nint renderer);
        Assert.False(result);
        Assert.Equal(nint.Zero, window);
        Assert.Equal(nint.Zero, renderer);
    }

    [Fact]
    public void CreateWindowAndRenderer_NullTitle_ReturnsFalse()
    {
        bool result = Sdl.CreateWindowAndRenderer(null!, 800, 600, WindowFlags.Hidden,
            out nint window, out nint renderer);
        Assert.False(result);
        Assert.Equal(nint.Zero, window);
        Assert.Equal(nint.Zero, renderer);
    }

    [Fact]
    public void CreateWindowAndRenderer_Overload_EmptyTitle_ReturnsZero()
    {
        nint window = Sdl.CreateWindowAndRenderer("", 800, 600, WindowFlags.Hidden,
            out nint renderer);
        Assert.Equal(nint.Zero, window);
        Assert.Equal(nint.Zero, renderer);
    }

    [Fact]
    public void ConvertEventToRenderCoordinates_NullRenderer_ThrowsSdlException()
    {
        var evt = new SharpSDL3.Structs.Event();
        Assert.Throws<SdlException>(() =>
            Sdl.ConvertEventToRenderCoordinates(nint.Zero, ref evt));
    }
}
