using SharpSDL3;
using SharpSDL3.Structs;
using Xunit;

namespace SharpSDL3.Tests;

/// <summary>
/// Tests for GamePad.cs validation guards.
/// </summary>
public class GamePadTests
{
    [Fact]
    public void AddGamepadMapping_NullMapping_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.AddGamepadMapping(null!));
    }

    [Fact]
    public void AddGamepadMapping_EmptyMapping_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.AddGamepadMapping(""));
    }

    [Fact]
    public void AddGamepadMapping_WhitespaceMapping_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.AddGamepadMapping("   "));
    }

    [Fact]
    public void AddGamepadMappingsFromFile_NullFile_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.AddGamepadMappingsFromFile(null!));
    }

    [Fact]
    public void AddGamepadMappingsFromFile_EmptyFile_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.AddGamepadMappingsFromFile(""));
    }

    [Fact]
    public void AddGamepadMappingsFromIo_NullSrc_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.AddGamepadMappingsFromIo(nint.Zero, new SdlBool()));
    }

    [Fact]
    public void CloseGamepad_NullGamepad_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.CloseGamepad(nint.Zero));
    }

    [Fact]
    public void GamepadConnected_NullGamepad_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.GamepadConnected(nint.Zero));
    }
}
