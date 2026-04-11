using SharpSDL3;
using Xunit;

namespace SharpSDL3.Tests;

/// <summary>
/// Tests for Storage.cs validation guards.
/// </summary>
public class StorageTests
{
    [Fact]
    public void CloseStorage_NullHandle_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.CloseStorage(nint.Zero));
    }

    [Fact]
    public void CopyStorageFile_NullStorage_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.CopyStorageFile(nint.Zero, "/old", "/new"));
    }

    [Fact]
    public void CopyStorageFile_NullOldPath_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.CopyStorageFile((nint)1, null!, "/new"));
    }

    [Fact]
    public void CopyStorageFile_EmptyOldPath_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.CopyStorageFile((nint)1, "", "/new"));
    }

    [Fact]
    public void CopyStorageFile_NullNewPath_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.CopyStorageFile((nint)1, "/old", null!));
    }

    [Fact]
    public void CopyStorageFile_EmptyNewPath_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.CopyStorageFile((nint)1, "/old", ""));
    }

    [Fact]
    public void CreateStorageDirectory_NullStorage_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.CreateStorageDirectory(nint.Zero, "/dir"));
    }

    [Fact]
    public void CreateStorageDirectory_EmptyPath_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Sdl.CreateStorageDirectory((nint)1, ""));
    }
}
