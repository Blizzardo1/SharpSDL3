using SharpSDL3;
using Xunit;

namespace SharpSDL3.Tests;

/// <summary>
/// Tests for SdlException.
/// </summary>
public class SdlExceptionTests
{
    [Fact]
    public void SdlException_StoresMessage()
    {
        var ex = new SdlException("test error");
        Assert.Equal("test error", ex.Message);
    }

    [Fact]
    public void SdlException_IsException()
    {
        var ex = new SdlException("test");
        Assert.IsAssignableFrom<Exception>(ex);
    }

    [Fact]
    public void SdlException_CanBeThrown()
    {
        Assert.Throws<SdlException>(() => throw new SdlException("boom"));
    }

    [Fact]
    public void SdlException_CanBeCaught()
    {
        try
        {
            throw new SdlException("caught");
        }
        catch (SdlException ex)
        {
            Assert.Equal("caught", ex.Message);
            return;
        }

        Assert.Fail("Exception was not caught");
    }
}
