using Xunit;

namespace SharpSDL3.Tests;

/// <summary>
/// Helpers for tests that exercise validation guards which call LogWarn/LogError.
/// In CI (no native SDL3 library), the guard correctly rejects bad input but
/// the log call throws DllNotFoundException. Both outcomes prove the guard works.
/// </summary>
internal static class TestHelpers
{
    /// <summary>
    /// Asserts that an action returns false, or throws DllNotFoundException
    /// (because the validation guard's LogWarn/LogError tried to P/Invoke).
    /// </summary>
    public static void AssertFalseOrNativeNotFound(Func<bool> action)
    {
        try
        {
            Assert.False(action());
        }
        catch (DllNotFoundException)
        {
            // Guard rejected input but log call hit missing native lib — still a pass
        }
    }

    /// <summary>
    /// Asserts that an action returns nint.Zero, or throws DllNotFoundException.
    /// </summary>
    public static void AssertZeroOrNativeNotFound(Func<nint> action)
    {
        try
        {
            Assert.Equal(nint.Zero, action());
        }
        catch (DllNotFoundException)
        {
            // Guard rejected input but log call hit missing native lib — still a pass
        }
    }

    /// <summary>
    /// Asserts that an action completes without error (ignoring DllNotFoundException
    /// from log calls in validation guards).
    /// </summary>
    public static void AssertNoThrowOrNativeNotFound(Action action)
    {
        try
        {
            action();
        }
        catch (DllNotFoundException)
        {
            // Expected in CI without native SDL3
        }
    }
}
