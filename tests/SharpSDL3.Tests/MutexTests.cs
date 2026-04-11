using SharpSDL3;
using Xunit;

namespace SharpSDL3.Tests;

/// <summary>
/// Tests for Mutex.cs validation guards.
/// </summary>
public class MutexTests
{
    [Fact]
    public void BroadcastCondition_NullCond_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            Sdl.BroadcastCondition(nint.Zero));
    }

    [Fact]
    public void DestroyCondition_NullCond_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            Sdl.DestroyCondition(nint.Zero));
    }

    [Fact]
    public void DestroyMutex_NullMutex_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            Sdl.DestroyMutex(nint.Zero));
    }

    [Fact]
    public void DestroyRwLock_NullRwLock_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            Sdl.DestroyRwLock(nint.Zero));
    }
}
