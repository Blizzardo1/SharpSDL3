using SharpSDL3;
using Xunit;

namespace SharpSDL3.Tests;

/// <summary>
/// Tests for Camera.cs validation guards.
/// </summary>
public class CameraTests
{
    [Fact]
    public void AcquireCameraFrame_NullCamera_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            Sdl.AcquireCameraFrame(nint.Zero, out _));
    }

    [Fact]
    public void CloseCamera_NullCamera_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            Sdl.CloseCamera(nint.Zero));
    }

    [Fact]
    public void GetCameraFormat_NullCamera_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            Sdl.GetCameraFormat(nint.Zero, out _));
    }
}
