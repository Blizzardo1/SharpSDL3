using SharpSDL3;
using SharpSDL3.Enums;
using SharpSDL3.Structs;
using Xunit;
using Xunit.Abstractions;

namespace SharpSDL3.Tests;

[Collection("SDL3")]
public class NativeSystemTests
{
    private readonly SdlFixture _sdl;
    private readonly ITestOutputHelper _output;

    public NativeSystemTests(SdlFixture sdl, ITestOutputHelper output)
    {
        _sdl = sdl;
        _output = output;
    }

    private bool RequireSdl() { if (!_sdl.Available) { _output.WriteLine("SKIPPED"); return false; } return true; }

    // --- Logger ---

    [Fact]
    public void Log_AllLevels()
    {
        if (!RequireSdl()) return;
        Sdl.Log("test log message");
        Sdl.LogDebug(LogCategory.Test, "debug message");
        Sdl.LogInfo(LogCategory.Test, "info message");
        Sdl.LogWarn(LogCategory.Test, "warn message");
        Sdl.LogError(LogCategory.Test, "error message");
        Sdl.LogCritical(LogCategory.Test, "critical message");
        Sdl.LogTrace(LogCategory.Test, "trace message");
        Sdl.LogVerbose(LogCategory.Test, "verbose message");
    }

    [Fact]
    public void LogPriority_SetAndGet()
    {
        if (!RequireSdl()) return;
        Sdl.SetLogPriority(LogCategory.Application, LogPriority.Debug);
        var priority = Sdl.GetLogPriority(LogCategory.Application);
        Assert.Equal(LogPriority.Debug, priority);

        Sdl.SetLogPriority(LogCategory.Application, LogPriority.Warn);
        priority = Sdl.GetLogPriority(LogCategory.Application);
        Assert.Equal(LogPriority.Warn, priority);

        Sdl.ResetLogPriorities();
    }

    [Fact]
    public void LogPriorities_SetAll()
    {
        if (!RequireSdl()) return;
        Sdl.SetLogPriorities(LogPriority.Verbose);
        Assert.Equal(LogPriority.Verbose, Sdl.GetLogPriority(LogCategory.Application));
        Assert.Equal(LogPriority.Verbose, Sdl.GetLogPriority(LogCategory.System));
        Sdl.ResetLogPriorities();
    }

    // --- Timer ---

    [Fact]
    public void GetTicks_Increases()
    {
        if (!RequireSdl()) return;
        ulong t1 = Sdl.GetTicks();
        ulong t2 = Sdl.GetTicks();
        Assert.True(t2 >= t1);
    }

    [Fact]
    public void GetTicksNs_ReturnsNonZero()
    {
        if (!RequireSdl()) return;
        ulong ns = Sdl.GetTicksNs();
        Assert.True(ns > 0);
    }

    [Fact]
    public void GetPerformanceCounter_ReturnsNonZero()
    {
        if (!RequireSdl()) return;
        ulong counter = Sdl.GetPerformanceCounter();
        Assert.True(counter > 0);
    }

    [Fact]
    public void GetPerformanceFrequency_ReturnsNonZero()
    {
        if (!RequireSdl()) return;
        ulong freq = Sdl.GetPerformanceFrequency();
        Assert.True(freq > 0);
    }

    // --- CPU Info ---

    [Fact]
    public void GetNumLogicalCpuCores_ReturnsPositive()
    {
        if (!RequireSdl()) return;
        int cores = Sdl.GetNumLogicalCpuCores();
        Assert.True(cores > 0, $"Expected >0 cores, got {cores}");
        _output.WriteLine($"Logical CPU cores: {cores}");
    }

    [Fact]
    public void GetCpuCacheLineSize_ReturnsPositive()
    {
        if (!RequireSdl()) return;
        int size = Sdl.GetCpuCacheLineSize();
        Assert.True(size > 0);
        _output.WriteLine($"CPU cache line size: {size}");
    }

    [Fact]
    public void GetSystemRam_ReturnsPositive()
    {
        if (!RequireSdl()) return;
        int ram = Sdl.GetSystemRam();
        Assert.True(ram > 0);
        _output.WriteLine($"System RAM: {ram} MB");
    }

    [Fact]
    public void SimdFeatures_DoNotThrow()
    {
        if (!RequireSdl()) return;
        // Just verify these don't throw — values are platform-dependent
        _ = (bool)Sdl.HasSse();
        _ = (bool)Sdl.HasSse2();
        _ = (bool)Sdl.HasSse3();
        _ = (bool)Sdl.HasSse41();
        _ = (bool)Sdl.HasSse42();
        _ = (bool)Sdl.HasAvx();
        _ = (bool)Sdl.HasAvx2();
        _ = (bool)Sdl.HasNeon();
    }

    // --- Platform & Version ---

    [Fact]
    public void GetPlatform_ReturnsKnownValue()
    {
        if (!RequireSdl()) return;
        string platform = Sdl.GetPlatform();
        Assert.False(string.IsNullOrEmpty(platform));
        _output.WriteLine($"Platform: {platform}");
        Assert.Contains(platform, new[] { "Linux", "Windows", "macOS", "Mac OS X" });
    }

    [Fact]
    public void GetRevision_ReturnsNonEmpty()
    {
        if (!RequireSdl()) return;
        string rev = Sdl.GetRevision();
        Assert.False(string.IsNullOrEmpty(rev));
        _output.WriteLine($"SDL revision: {rev}");
    }

    // --- Screen saver ---

    [Fact]
    public void ScreenSaver_DisableEnable()
    {
        if (!RequireSdl()) return;
        Sdl.DisableScreenSaver();
        Assert.False(Sdl.ScreenSaverEnabled());
        Sdl.EnableScreenSaver();
        Assert.True(Sdl.ScreenSaverEnabled());
    }

    // --- Init flags ---

    [Fact]
    public void WasInit_ReportsInitializedSubsystems()
    {
        if (!RequireSdl()) return;
        var flags = Sdl.WasInit(InitFlags.Timer);
        Assert.True(flags.HasFlag(InitFlags.Timer));
    }

    // --- Thread ID ---

    [Fact]
    public void GetCurrentThreadId_ReturnsNonZero()
    {
        if (!RequireSdl()) return;
        ulong id = Sdl.GetCurrentThreadId();
        Assert.True(id > 0);
    }
}
