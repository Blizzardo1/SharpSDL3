using SharpSDL3;
using Xunit;

namespace SharpSDL3.Tests;

/// <summary>
/// Tests that Constants string properties are non-null, non-empty,
/// and follow the expected SDL property naming convention.
/// </summary>
public class ConstantsTests
{
    [Fact]
    public void AllConstants_AreNonNullAndNonEmpty()
    {
        var fields = typeof(Constants).GetFields(
            System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.Static);

        Assert.True(fields.Length > 0, "No constants found");

        foreach (var field in fields)
        {
            Assert.True(field.IsLiteral, $"{field.Name} should be a const");
            var value = (string?)field.GetValue(null);
            Assert.False(string.IsNullOrEmpty(value),
                $"Constant {field.Name} is null or empty");
        }
    }

    [Fact]
    public void AllConstants_StartWithSdlPrefix()
    {
        var fields = typeof(Constants).GetFields(
            System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.Static);

        foreach (var field in fields)
        {
            var value = (string?)field.GetValue(null);
            Assert.StartsWith("SDL.", value!,
                StringComparison.Ordinal);
        }
    }

    [Fact]
    public void AllConstants_ContainNoDuplicateValues()
    {
        var fields = typeof(Constants).GetFields(
            System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.Static);

        var values = new HashSet<string>();
        foreach (var field in fields)
        {
            var value = (string)field.GetValue(null)!;
            Assert.True(values.Add(value),
                $"Duplicate constant value: {value} (field {field.Name})");
        }
    }

    // Spot-check specific values
    [Fact]
    public void ThreadCreateEntryFunction_HasExpectedValue()
    {
        Assert.Equal("SDL.thread.create.entry_function",
            Constants.SdlPropThreadCreateEntryFunctionPointer);
    }

    [Fact]
    public void WindowCreateTitle_HasExpectedValue()
    {
        Assert.Equal("SDL.window.create.title",
            Constants.SdlPropWindowCreateTitleString);
    }

    [Fact]
    public void IoStreamWindowsHandle_HasExpectedValue()
    {
        Assert.Equal("SDL.iostream.windows.handle",
            Constants.SdlPropIoStreamWindowsHandlePointer);
    }

    [Fact]
    public void SurfaceSdrWhitePoint_HasExpectedValue()
    {
        Assert.Equal("SDL.surface.SDR_white_point",
            Constants.SdlPropSurfaceSdrWhitePointFloat);
    }
}
