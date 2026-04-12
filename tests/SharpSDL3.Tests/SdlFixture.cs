using SharpSDL3;
using SharpSDL3.Enums;
using Xunit;

namespace SharpSDL3.Tests;

/// <summary>
/// Shared fixture that initializes SDL3 once for all native integration tests.
/// If the native library is not available, tests using this fixture are skipped.
/// </summary>
public class SdlFixture : IDisposable
{
    public bool Available { get; }

    public SdlFixture()
    {
        try
        {
            Available = Sdl.Init(InitFlags.Timer | InitFlags.Events);
        }
        catch (DllNotFoundException)
        {
            Available = false;
        }
    }

    public void Dispose()
    {
        if (Available)
            Sdl.Quit();
    }
}

[CollectionDefinition("SDL3")]
public class SdlCollection : ICollectionFixture<SdlFixture> { }
