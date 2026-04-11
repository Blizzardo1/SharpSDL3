using System.Runtime.InteropServices;
using SharpSDL3.Enums;
using SharpSDL3.Structs;
using Xunit;

namespace SharpSDL3.Tests;

/// <summary>
/// Tests that struct layouts are correct and match expected sizes/alignment.
/// Validates StructLayout attributes and field offsets for ABI compatibility.
/// </summary>
public class StructLayoutTests
{
    // --- Sequential struct size checks ---

    [Fact]
    public void Rect_IsSequential_And_CorrectSize()
    {
        Assert.Equal(LayoutKind.Sequential,
            typeof(Rect).StructLayoutAttribute!.Value);
        Assert.Equal(16, Marshal.SizeOf<Rect>()); // 4 ints * 4 bytes
    }

    [Fact]
    public void FRect_IsSequential_And_CorrectSize()
    {
        Assert.Equal(LayoutKind.Sequential,
            typeof(FRect).StructLayoutAttribute!.Value);
        Assert.Equal(16, Marshal.SizeOf<FRect>()); // 4 floats * 4 bytes
    }

    [Fact]
    public void FPoint_IsSequential_And_CorrectSize()
    {
        Assert.Equal(LayoutKind.Sequential,
            typeof(FPoint).StructLayoutAttribute!.Value);
        Assert.Equal(8, Marshal.SizeOf<FPoint>()); // 2 floats * 4 bytes
    }

    [Fact]
    public void Color_IsSequential_And_CorrectSize()
    {
        Assert.Equal(LayoutKind.Sequential,
            typeof(Color).StructLayoutAttribute!.Value);
        Assert.Equal(4, Marshal.SizeOf<Color>()); // 4 bytes (R, G, B, A)
    }

    [Fact]
    public void FColor_IsSequential_And_CorrectSize()
    {
        Assert.Equal(LayoutKind.Sequential,
            typeof(FColor).StructLayoutAttribute!.Value);
        Assert.Equal(16, Marshal.SizeOf<FColor>()); // 4 floats * 4 bytes
    }

    [Fact]
    public void AudioSpec_IsSequential_And_CorrectSize()
    {
        Assert.Equal(LayoutKind.Sequential,
            typeof(AudioSpec).StructLayoutAttribute!.Value);
        // AudioFormat(4) + Channels(4) + Freq(4) = 12
        Assert.Equal(12, Marshal.SizeOf<AudioSpec>());
    }

    [Fact]
    public void AtomicInt_IsSequential_And_CorrectSize()
    {
        Assert.Equal(LayoutKind.Sequential,
            typeof(AtomicInt).StructLayoutAttribute!.Value);
        Assert.Equal(4, Marshal.SizeOf<AtomicInt>());
    }

    // --- Event union layout ---

    [Fact]
    public void Event_IsExplicitLayout()
    {
        Assert.Equal(LayoutKind.Explicit,
            typeof(Event).StructLayoutAttribute!.Value);
    }

    [Fact]
    public void Event_Size_IsAtLeast128Bytes()
    {
        // SDL3 Event union is padded to 128 bytes for ABI compatibility
        Assert.True(Marshal.SizeOf<Event>() >= 128,
            $"Event struct size is {Marshal.SizeOf<Event>()} bytes, expected >= 128");
    }

    [Fact]
    public void Event_Type_AtOffset0()
    {
        // The Type field should be at offset 0
        var offset = Marshal.OffsetOf<Event>(nameof(Event.Type));
        Assert.Equal(0, (int)offset);
    }

    // --- SdlBool ---

    [Fact]
    public void SdlBool_TrueConvertsToTrue()
    {
        SdlBool b = true;
        Assert.True((bool)b);
    }

    [Fact]
    public void SdlBool_FalseConvertsToFalse()
    {
        SdlBool b = false;
        Assert.False((bool)b);
    }

    [Fact]
    public void SdlBool_ImplicitConversion_RoundTrips()
    {
        SdlBool fromTrue = true;
        SdlBool fromFalse = false;

        bool backToTrue = fromTrue;
        bool backToFalse = fromFalse;

        Assert.True(backToTrue);
        Assert.False(backToFalse);
    }

    [Fact]
    public void SdlBool_Equality()
    {
        SdlBool a = true;
        SdlBool b = true;
        SdlBool c = false;

        Assert.Equal(a, b);
        Assert.NotEqual(a, c);
    }

    [Fact]
    public void SdlBool_GetHashCode_ConsistentWithEquality()
    {
        SdlBool a = true;
        SdlBool b = true;
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }
}
