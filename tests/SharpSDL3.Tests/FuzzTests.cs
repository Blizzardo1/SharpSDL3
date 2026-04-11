using System.Runtime.InteropServices;
using SharpSDL3;
using SharpSDL3.Enums;
using SharpSDL3.Structs;
using Xunit;

namespace SharpSDL3.Tests;

/// <summary>
/// Fuzz tests that exercise key API points with random and boundary inputs.
/// These test the managed validation layer and struct marshalling without
/// requiring the native SDL3 library.
/// </summary>
public class FuzzTests
{
    private static readonly Random Rng = new(42); // Deterministic seed for reproducibility

    // --- VersionNum fuzzing ---

    [Fact]
    public void Fuzz_VersionNum_RandomInputs_NeverThrows()
    {
        for (int i = 0; i < 10_000; i++)
        {
            int major = Rng.Next(int.MinValue, int.MaxValue);
            int minor = Rng.Next(int.MinValue, int.MaxValue);
            int patch = Rng.Next(int.MinValue, int.MaxValue);

            // Should never throw, even with overflow
            _ = Sdl.VersionNum(major, minor, patch);
        }
    }

    [Theory]
    [InlineData(int.MaxValue, int.MaxValue, int.MaxValue)]
    [InlineData(int.MinValue, int.MinValue, int.MinValue)]
    [InlineData(0, 0, 0)]
    [InlineData(-1, -1, -1)]
    public void Fuzz_VersionNum_BoundaryValues_NeverThrows(int major, int minor, int patch)
    {
        _ = Sdl.VersionNum(major, minor, patch);
    }

    // --- StructureToPointer/PointerToStructure fuzzing ---

    [Fact]
    public unsafe void Fuzz_StructureToPointer_RandomRects()
    {
        for (int i = 0; i < 1_000; i++)
        {
            var rect = new Rect
            {
                X = Rng.Next(int.MinValue, int.MaxValue),
                Y = Rng.Next(int.MinValue, int.MaxValue),
                W = Rng.Next(int.MinValue, int.MaxValue),
                H = Rng.Next(int.MinValue, int.MaxValue)
            };

            nint ptr = Sdl.StructureToPointer(ref rect);
            try
            {
                Assert.NotEqual(nint.Zero, ptr);
                var result = Sdl.PointerToStructure<Rect>(ptr);
                Assert.Equal(rect.X, result.X);
                Assert.Equal(rect.Y, result.Y);
                Assert.Equal(rect.W, result.W);
                Assert.Equal(rect.H, result.H);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }

    [Fact]
    public unsafe void Fuzz_StructureToPointer_RandomColors()
    {
        for (int i = 0; i < 1_000; i++)
        {
            var color = new Color
            {
                R = (byte)Rng.Next(0, 256),
                G = (byte)Rng.Next(0, 256),
                B = (byte)Rng.Next(0, 256),
                A = (byte)Rng.Next(0, 256)
            };

            nint ptr = Sdl.StructureToPointer(ref color);
            try
            {
                var result = Sdl.PointerToStructure<Color>(ptr);
                Assert.Equal(color.R, result.R);
                Assert.Equal(color.G, result.G);
                Assert.Equal(color.B, result.B);
                Assert.Equal(color.A, result.A);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }

    [Fact]
    public unsafe void Fuzz_StructureToPointer_RandomFRects()
    {
        for (int i = 0; i < 1_000; i++)
        {
            var frect = new FRect
            {
                X = (float)(Rng.NextDouble() * 2e10 - 1e10),
                Y = (float)(Rng.NextDouble() * 2e10 - 1e10),
                W = (float)(Rng.NextDouble() * 1e10),
                H = (float)(Rng.NextDouble() * 1e10)
            };

            nint ptr = Sdl.StructureToPointer(ref frect);
            try
            {
                var result = Sdl.PointerToStructure<FRect>(ptr);
                Assert.Equal(frect.X, result.X);
                Assert.Equal(frect.Y, result.Y);
                Assert.Equal(frect.W, result.W);
                Assert.Equal(frect.H, result.H);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }

    [Fact]
    public unsafe void Fuzz_StructureToPointer_SpecialFloatValues()
    {
        float[] special = { float.NaN, float.PositiveInfinity, float.NegativeInfinity,
                           float.MinValue, float.MaxValue, float.Epsilon, 0f, -0f };

        foreach (float val in special)
        {
            var frect = new FRect { X = val, Y = val, W = val, H = val };
            nint ptr = Sdl.StructureToPointer(ref frect);
            try
            {
                var result = Sdl.PointerToStructure<FRect>(ptr);
                // NaN != NaN, so use BitConverter for exact comparison
                Assert.Equal(
                    BitConverter.SingleToInt32Bits(frect.X),
                    BitConverter.SingleToInt32Bits(result.X));
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }

    // --- Null-pointer guard fuzzing ---

    [Fact]
    public void Fuzz_NullPointerGuards_SurfaceFunctions_NeverThrow()
    {
        // These should all return false gracefully, never throw
        nint[] ptrs = { nint.Zero, (nint)1, (nint)(-1), nint.MaxValue };

        foreach (nint src in ptrs)
        {
            foreach (nint dst in ptrs)
            {
                if (src == nint.Zero || dst == nint.Zero)
                {
                    Assert.False(Sdl.BlitSurface(src, nint.Zero, dst, nint.Zero));
                    Assert.False(Sdl.BlitSurfaceScaled(src, nint.Zero, dst, nint.Zero, ScaleMode.Linear));
                    Assert.False(Sdl.BlitSurfaceTiled(src, nint.Zero, dst, nint.Zero));
                    Assert.False(Sdl.BlitSurfaceUnchecked(src, nint.Zero, dst, nint.Zero));
                }
            }
        }
    }

    [Fact]
    public void Fuzz_NullPointerGuards_SinglePointerFunctions_NeverThrow()
    {
        Assert.False(Sdl.ClearComposition(nint.Zero));
        Assert.False(Sdl.ClearSurface(nint.Zero, 0, 0, 0, 0));
        Assert.Equal(nint.Zero, Sdl.ConvertSurface(nint.Zero, PixelFormat.Rgba8888));
        Assert.Equal(nint.Zero, Sdl.DuplicateSurface(nint.Zero));
        Assert.False(Sdl.FlipSurface(nint.Zero, FlipMode.Horizontal));
        Assert.False(Sdl.FillSurfaceRect(nint.Zero, new Rect(), 0));
        Assert.False(Sdl.DestroyWindowSurface(nint.Zero));
        Assert.Equal(nint.Zero, Sdl.CreateSurfacePalette(nint.Zero));
    }

    // --- AtomicInt operator fuzzing ---

    [Fact]
    public void Fuzz_AtomicInt_Arithmetic_RandomValues()
    {
        for (int i = 0; i < 10_000; i++)
        {
            int v1 = Rng.Next(int.MinValue / 2, int.MaxValue / 2);
            int v2 = Rng.Next(1, 1000); // Avoid division by zero

            AtomicInt a = v1;
            Assert.Equal(v1, (int)a);

            var added = a + v2;
            Assert.Equal(v1 + v2, added.Value);

            var subtracted = a - v2;
            Assert.Equal(v1 - v2, subtracted.Value);
        }
    }

    [Fact]
    public void Fuzz_AtomicInt_BitwiseOps_RandomValues()
    {
        for (int i = 0; i < 10_000; i++)
        {
            int v1 = Rng.Next();
            int v2 = Rng.Next();

            AtomicInt a = v1;
            Assert.Equal(v1 & v2, (a & v2).Value);
            Assert.Equal(v1 | v2, (a | v2).Value);
            Assert.Equal(v1 ^ v2, (a ^ v2).Value);
        }
    }

    [Fact]
    public void Fuzz_AtomicInt_Comparison_RandomValues()
    {
        for (int i = 0; i < 10_000; i++)
        {
            int v1 = Rng.Next(int.MinValue, int.MaxValue);
            int v2 = Rng.Next(int.MinValue, int.MaxValue);

            AtomicInt a = v1;
            Assert.Equal(v1 == v2, a == v2);
            Assert.Equal(v1 != v2, a != v2);
            Assert.Equal(v1 > v2, a > v2);
            Assert.Equal(v1 < v2, a < v2);
            Assert.Equal(v1 >= v2, a >= v2);
            Assert.Equal(v1 <= v2, a <= v2);
        }
    }

    // --- SdlBool fuzzing ---

    [Fact]
    public void Fuzz_SdlBool_RoundTrip()
    {
        for (int i = 0; i < 10_000; i++)
        {
            bool val = Rng.Next(2) == 1;
            SdlBool sdlBool = val;
            bool result = sdlBool;
            Assert.Equal(val, result);
        }
    }

    // --- Color equality fuzzing ---

    [Fact]
    public void Fuzz_Color_Equality()
    {
        for (int i = 0; i < 10_000; i++)
        {
            byte r = (byte)Rng.Next(256);
            byte g = (byte)Rng.Next(256);
            byte b = (byte)Rng.Next(256);
            byte a = (byte)Rng.Next(256);

            var c1 = new Color { R = r, G = g, B = b, A = a };
            var c2 = new Color { R = r, G = g, B = b, A = a };
            var c3 = new Color { R = (byte)(255 - r), G = g, B = b, A = a };

            Assert.True(c1 == c2);
            Assert.Equal(c1.GetHashCode(), c2.GetHashCode());
            if (r != (byte)(255 - r))
            {
                Assert.True(c1 != c3);
            }
        }
    }

    // --- Event struct fuzzing ---

    [Fact]
    public void Fuzz_EventStruct_TypeField_AllEventTypes()
    {
        // Verify that setting the Type field on the union works for all defined event types
        foreach (EventType et in Enum.GetValues<EventType>())
        {
            var evt = new Event { Type = et };
            Assert.Equal(et, evt.Type);

            // The Common overlay should also see the same type
            Assert.Equal(et, evt.Common.Type);
        }
    }

    // --- CreateSurface boundary fuzzing ---

    [Fact]
    public void Fuzz_CreateSurface_InvalidDimensions()
    {
        int[] badValues = { 0, -1, -100, int.MinValue };

        foreach (int w in badValues)
        {
            Assert.Equal(nint.Zero, Sdl.CreateSurface(w, 100, PixelFormat.Rgba8888));
        }

        foreach (int h in badValues)
        {
            Assert.Equal(nint.Zero, Sdl.CreateSurface(100, h, PixelFormat.Rgba8888));
        }
    }

    // --- ClearProperty boundary fuzzing ---

    [Fact]
    public void Fuzz_ClearProperty_BoundaryInputs()
    {
        string[] badNames = { "", null! };

        foreach (string name in badNames)
        {
            Assert.False(Sdl.ClearProperty(1, name));
        }

        Assert.False(Sdl.ClearProperty(0, "validname"));
    }

    // --- Events validation boundary fuzzing ---

    [Fact]
    public void Fuzz_FlushEvents_BoundaryValues()
    {
        // Valid range should not throw
        // (can't test SDL_FlushEvents without native lib, but validation throws before that)
        Assert.Throws<ArgumentException>(() => Sdl.FlushEvents(0, 0));
        Assert.Throws<ArgumentException>(() => Sdl.FlushEvents(uint.MaxValue, 1));
    }

    // --- Multiple struct types marshal round-trip fuzzing ---

    [Fact]
    public unsafe void Fuzz_StructMarshal_AudioSpec_RandomValues()
    {
        var formats = Enum.GetValues<AudioFormat>();

        for (int i = 0; i < 1_000; i++)
        {
            var spec = new AudioSpec
            {
                Format = formats[Rng.Next(formats.Length)],
                Channels = Rng.Next(1, 256),
                Freq = Rng.Next(8000, 192001)
            };

            nint ptr = Sdl.StructureToPointer(ref spec);
            try
            {
                var result = Sdl.PointerToStructure<AudioSpec>(ptr);
                Assert.Equal(spec.Format, result.Format);
                Assert.Equal(spec.Channels, result.Channels);
                Assert.Equal(spec.Freq, result.Freq);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }

    [Fact]
    public unsafe void Fuzz_StructMarshal_FPoint_RandomValues()
    {
        for (int i = 0; i < 1_000; i++)
        {
            var pt = new FPoint
            {
                X = (float)(Rng.NextDouble() * 2e6 - 1e6),
                Y = (float)(Rng.NextDouble() * 2e6 - 1e6)
            };

            nint ptr = Sdl.StructureToPointer(ref pt);
            try
            {
                var result = Sdl.PointerToStructure<FPoint>(ptr);
                Assert.Equal(pt.X, result.X);
                Assert.Equal(pt.Y, result.Y);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }

    [Fact]
    public unsafe void Fuzz_StructMarshal_FColor_RandomValues()
    {
        for (int i = 0; i < 1_000; i++)
        {
            var fc = new FColor
            {
                R = (float)Rng.NextDouble(),
                G = (float)Rng.NextDouble(),
                B = (float)Rng.NextDouble(),
                A = (float)Rng.NextDouble()
            };

            nint ptr = Sdl.StructureToPointer(ref fc);
            try
            {
                var result = Sdl.PointerToStructure<FColor>(ptr);
                Assert.Equal(fc.R, result.R);
                Assert.Equal(fc.G, result.G);
                Assert.Equal(fc.B, result.B);
                Assert.Equal(fc.A, result.A);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }
}
