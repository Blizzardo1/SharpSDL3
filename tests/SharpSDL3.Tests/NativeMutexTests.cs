using SharpSDL3;
using SharpSDL3.Structs;
using Xunit;
using Xunit.Abstractions;

namespace SharpSDL3.Tests;

[Collection("SDL3")]
public class NativeMutexTests
{
    private readonly SdlFixture _sdl;
    private readonly ITestOutputHelper _output;

    public NativeMutexTests(SdlFixture sdl, ITestOutputHelper output)
    {
        _sdl = sdl;
        _output = output;
    }

    private bool RequireSdl() { if (!_sdl.Available) { _output.WriteLine("SKIPPED"); return false; } return true; }

    [Fact]
    public void Mutex_CreateLockUnlockDestroy()
    {
        if (!RequireSdl()) return;
        nint mutex = Sdl.CreateMutex();
        Assert.NotEqual(nint.Zero, mutex);

        Sdl.LockMutex(mutex);
        Sdl.UnlockMutex(mutex);

        Sdl.DestroyMutex(mutex);
    }

    [Fact]
    public void Mutex_TryLock_Succeeds()
    {
        if (!RequireSdl()) return;
        nint mutex = Sdl.CreateMutex();
        Assert.True((bool)Sdl.TryLockMutex(mutex));
        Sdl.UnlockMutex(mutex);
        Sdl.DestroyMutex(mutex);
    }

    [Fact]
    public void RwLock_CreateLockUnlockDestroy()
    {
        if (!RequireSdl()) return;
        nint rwlock = Sdl.CreateRwLock();
        Assert.NotEqual(nint.Zero, rwlock);

        Sdl.LockRwLockForReading(rwlock);
        Sdl.UnlockRwLock(rwlock);

        Sdl.LockRwLockForWriting(rwlock);
        Sdl.UnlockRwLock(rwlock);

        Sdl.DestroyRwLock(rwlock);
    }

    [Fact]
    public void RwLock_TryLock()
    {
        if (!RequireSdl()) return;
        nint rwlock = Sdl.CreateRwLock();

        Assert.True((bool)Sdl.TryLockRwLockForReading(rwlock));
        Sdl.UnlockRwLock(rwlock);

        Assert.True((bool)Sdl.TryLockRwLockForWriting(rwlock));
        Sdl.UnlockRwLock(rwlock);

        Sdl.DestroyRwLock(rwlock);
    }

    [Fact]
    public void Condition_CreateSignalDestroy()
    {
        if (!RequireSdl()) return;
        nint cond = Sdl.CreateCondition();
        Assert.NotEqual(nint.Zero, cond);

        Sdl.SignalCondition(cond);
        Sdl.BroadcastCondition(cond);

        Sdl.DestroyCondition(cond);
    }

    [Fact]
    public void Condition_WaitTimeout()
    {
        if (!RequireSdl()) return;
        nint cond = Sdl.CreateCondition();
        nint mutex = Sdl.CreateMutex();

        Sdl.LockMutex(mutex);
        // Should timeout immediately (1ms)
        SdlBool result = Sdl.WaitConditionTimeout(cond, mutex, 1);
        // result is false (timeout), which is expected
        Sdl.UnlockMutex(mutex);

        Sdl.DestroyCondition(cond);
        Sdl.DestroyMutex(mutex);
    }
}
