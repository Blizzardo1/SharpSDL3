# SharpSDL3 Testing

## Overview

SharpSDL3 uses [xUnit](https://xunit.net/) for unit testing and
[Coverlet](https://github.com/coverlet-coverage/coverlet) for code coverage.
Tests run automatically on every push and pull request via GitHub Actions across
Linux, Windows, and macOS.

The test suite has two tiers:

1. **Unit tests** — exercise managed C# code (validation guards, struct
   marshalling, operator overloads, enums, constants) without requiring the
   native SDL3 library. These always pass regardless of environment.
2. **Native integration tests** — call through to the real SDL3 library to
   verify init, surface creation, blitting, logging, and more. These require
   SDL3 to be installed and gracefully pass (with a skip message) when it is not.

The CI pipeline builds SDL3 from source (configurable version) on all three
platforms so both tiers run end-to-end.

---

## Test Categories

### Unit Tests

| Test File | What It Covers | Methods |
|---|---|---|
| `SdlCoreTests.cs` | `VersionNum`, `StructureToPointer`/`PointerToStructure` round-trips, null-pointer validation guards across surface/window/property functions | ~30 |
| `SdlValidationTests.cs` | Window/thread/surface creation guards, `EnterAppMainCallbacks` null checks, `EnumerateProperties` | ~15 |
| `RenderTests.cs` | Renderer null guards, `CreateWindowAndRenderer` title validation, `ConvertEventToRenderCoordinates` | 7 |
| `EventsTests.cs` | Event type zero-check throws, filter null checks, `FlushEvents` range validation, `HasEvents` boundary checks | 12 |
| `GamePadTests.cs` | `AddGamepadMapping` string validation, file path checks, handle null guards | 8 |
| `StorageTests.cs` | Storage handle and path validation for `CloseStorage`, `CopyStorageFile`, `CreateStorageDirectory` | 8 |
| `MutexTests.cs` | Null-handle guards for `BroadcastCondition`, `DestroyCondition`, `DestroyMutex`, `DestroyRwLock` | 4 |
| `CameraTests.cs` | Null-camera guards for `AcquireCameraFrame`, `CloseCamera`, `GetCameraFormat` | 3 |
| `LoggerTests.cs` | Log message null/empty/whitespace validation, `GetLogPriority` invalid category | 6 |
| `MouseTests.cs` | `GetMouseNameForId` zero-ID check | 1 |
| `StructLayoutTests.cs` | `Marshal.SizeOf` checks for `Rect`, `FRect`, `FPoint`, `Color`, `FColor`, `AudioSpec`, `AtomicInt`; `Event` union layout (explicit, >= 128 bytes, Type at offset 0); `SdlBool` conversions and equality | ~15 |
| `AtomicIntTests.cs` | All 15 operator overloads (`+`, `-`, `*`, `/`, `%`, `&`, `\|`, `^`, `<<`, `>>`, `++`, `--`, `==`, `!=`, `>`, `<`, `>=`, `<=`), implicit conversions, `Equals`, `GetHashCode` | 22 |
| `ColorTests.cs` | Equality operators, `Equals(Color)`, `Equals(object)`, hash code consistency, default values | 9 |
| `EnumTests.cs` | Numeric values for `LogCategory`, `LogPriority`, `EventType`, `WindowFlags`; flags combination | ~15 |
| `ConstantsTests.cs` | All constants non-null, `SDL.` prefix, no duplicates, spot-check specific values | 7 |
| `SdlExceptionTests.cs` | Message storage, inheritance, throw/catch behavior | 4 |

### Native Integration Tests

`NativeIntegrationTests.cs` uses an `SdlFixture` that calls `Sdl.Init()` once
for the test collection. If SDL3 is not available, every test writes
"SKIPPED" to the test output and returns (passes silently).

| Test | What It Verifies |
|---|---|
| `Init_Succeeded` | SDL3 initializes without error |
| `GetVersion_ReturnsNonZero` | Native version call works |
| `VersionNum_MatchesGetVersion` | Version is >= 3.0.0 |
| `ClearError_ReturnsTrue` | Error state management |
| `GetError_AfterClear_ReturnsEmpty` | Error cleared to empty string |
| `CreateProperties_ReturnsNonZero` | Property group lifecycle |
| `ClearProperty_OnValidProps_ReturnsTrue` | Property clearing works |
| `CreateSurface_ValidArgs_ReturnsNonZero` | Surface allocation |
| `DuplicateSurface_ValidSurface_ReturnsNonZero` | Surface duplication |
| `ClearSurface_ValidSurface_ReturnsTrue` | Surface fill with color |
| `FlipSurface_ValidSurface_ReturnsTrue` | Surface flip |
| `BlitSurface_TwoValidSurfaces_ReturnsTrue` | Surface-to-surface blit |
| `CreatePalette_ValidCount_ReturnsNonZero` | Palette allocation |
| `Log_ValidMessage_DoesNotThrow` | Logging works end-to-end |
| `LogCritical_ValidMessage_DoesNotThrow` | Critical log level works |
| `GetLogPriority_ValidCategory_ReturnsNonInvalid` | Log priority query |
| `GetTicks_ReturnsNonZero` | Timer subsystem works |

### Fuzz Tests

`FuzzTests.cs` exercises key API points with random and boundary inputs using a
deterministic seed (`42`) for reproducibility.

| Fuzz Target | Iterations | What It Tests |
|---|---|---|
| `VersionNum` | 10,000 | Overflow safety with random `int` inputs |
| `StructureToPointer<Rect>` | 1,000 | Marshal round-trip with random coordinates |
| `StructureToPointer<Color>` | 1,000 | Marshal round-trip with random RGBA bytes |
| `StructureToPointer<FRect>` | 1,000 | Marshal round-trip with random floats |
| `StructureToPointer<FColor>` | 1,000 | Marshal round-trip with random float colors |
| `StructureToPointer<FPoint>` | 1,000 | Marshal round-trip with random float points |
| `StructureToPointer<AudioSpec>` | 1,000 | Marshal round-trip with random formats/channels/freq |
| Special float values | 8 values | NaN, ±Infinity, MinValue, MaxValue, Epsilon, ±0 |
| Null-pointer surface guards | all combos | `BlitSurface`, `BlitSurfaceScaled`, `BlitSurfaceTiled`, `BlitSurfaceUnchecked` |
| Single-pointer guards | 8 functions | `ClearComposition`, `ClearSurface`, `ConvertSurface`, etc. |
| `AtomicInt` arithmetic | 10,000 | Addition, subtraction with random half-range ints |
| `AtomicInt` bitwise | 10,000 | AND, OR, XOR with random ints |
| `AtomicInt` comparison | 10,000 | All 6 comparison operators vs. native `int` behavior |
| `SdlBool` round-trip | 10,000 | `bool` → `SdlBool` → `bool` identity |
| `Color` equality | 10,000 | Operator `==`/`!=` and hash code consistency |
| `Event` union Type field | all values | Every defined `EventType` written and read back |
| `CreateSurface` boundaries | 4 values | 0, -1, -100, `int.MinValue` |
| `ClearProperty` boundaries | 3 combos | Empty/null name, zero handle |
| `FlushEvents` boundaries | 2 combos | Zero types, max > min inversion |

**Total: ~191 test methods** across 18 test files, with fuzz tests executing
over **80,000 individual assertions**.

---

## Code Coverage

### How It Works

Coverage is collected by [Coverlet](https://github.com/coverlet-coverage/coverlet)
during the test run and output in Cobertura XML format.
[ReportGenerator](https://github.com/danielpalme/ReportGenerator) then produces:

- **HTML report** — browsable per-file line/branch coverage
- **Markdown summary** — posted to the GitHub Actions job summary
- **Badges** — SVG badge images for README use

### Coverage Scope

Coverage measures the managed C# code in the `SharpSDL3` assembly:

- **Included**: all public methods, validation guards, struct operators,
  enum definitions, constants, exception types, utility functions
- **Excluded**: `[GeneratedCode]`-attributed source-generated P/Invoke stubs
  (these are auto-generated `LibraryImport` marshalling code)

The native SDL3 call paths (lines after the validation guard that call
`SDL_*` functions) are **not** covered because the native library is not loaded
in CI. This is by design — those paths are thin wrappers that delegate directly
to SDL3.

### Coverage Target

No hard coverage threshold is enforced in CI. Coverlet reports line, branch,
and method coverage in the job summary and HTML report. The raw line coverage
percentage will be low (~3-7%) because the assembly contains thousands of
source-generated `LibraryImport` P/Invoke stubs that cannot execute without
the native SDL3 library. These inflate the denominator.

The tests achieve **full coverage of testable managed code**:

- All validation guards (null checks, range checks, string checks)
- All struct marshal round-trips
- All operator overloads on value types
- All enum value definitions
- All constant string values
- All exception types

To get a meaningful coverage number, filter the report to exclude generated
code when viewing locally.

### Viewing Coverage Locally

```bash
# Run tests with coverage
dotnet test tests/SharpSDL3.Tests/SharpSDL3.Tests.csproj \
  -c Release \
  /p:CollectCoverage=true \
  /p:CoverletOutputFormat=cobertura \
  /p:CoverletOutput=./TestResults/coverage.cobertura.xml \
  "/p:Include=[SharpSDL3]*"

# Generate HTML report (requires reportgenerator)
dotnet tool install --global dotnet-reportgenerator-globaltool
reportgenerator \
  -reports:./TestResults/coverage.cobertura.xml \
  -targetdir:./TestResults/CoverageReport \
  -reporttypes:Html

# Open the report
open ./TestResults/CoverageReport/index.html   # macOS
xdg-open ./TestResults/CoverageReport/index.html  # Linux
start ./TestResults/CoverageReport/index.html   # Windows
```

### Viewing Coverage in CI

After a workflow run:

1. Go to the **Actions** tab in GitHub
2. Click the workflow run
3. The **job summary** shows a coverage table inline
4. Download the `coverage-report` artifact for the full HTML report

---

## CI/CD Pipeline

### Workflow: `test.yml`

Triggered on push to `main`, `dev`, and `feature/**` tags, and on PRs to
`main`/`dev`.

The SDL3 version is set once at the top of the workflow:

```yaml
env:
  SDL3_VERSION: "3.4.4"    # change this to upgrade
```

```
┌─────────────────────────────────────────────────────────┐
│  test (matrix: ubuntu, windows, macos)                  │
│                                                         │
│  1. Checkout                                            │
│  2. Setup .NET 9                                        │
│  3. Install SDL3                                        │
│     ├─ Linux/macOS: build from source (cached)          │
│     └─ Windows: download pre-built DLL                  │
│  4. Restore + Build                                     │
│  5. Run tests with Coverlet coverage collection         │
│  6. [ubuntu only] Generate coverage report              │
│  7. [ubuntu only] Post coverage summary to job          │
│  8. Upload test results (.trx) as artifact              │
│  9. [ubuntu only] Upload coverage report as artifact    │
└─────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────┐
│  fuzz (ubuntu only)                                     │
│                                                         │
│  1. Checkout                                            │
│  2. Setup .NET 9                                        │
│  3. Build and install SDL3 from source (cached)         │
│  4. Restore + Build                                     │
│  5. Run fuzz tests (filtered by FuzzTests class)        │
└─────────────────────────────────────────────────────────┘
```

### Changing the SDL3 Version

Edit the `SDL3_VERSION` environment variable at the top of `.github/workflows/test.yml`.
The build cache is keyed on version + OS, so changing the version automatically
rebuilds on the next run.

### Artifacts Produced

| Artifact | Contents | Retention |
|---|---|---|
| `test-results-{os}` | `.trx` test result files (one per platform) | Default (90 days) |
| `coverage-report` | Cobertura XML + HTML report + badge SVGs | Default (90 days) |

---

## Project Structure

```
SharpSDL3/
├── SDL3/                          # Main library
│   ├── SharpSDL3.csproj
│   ├── Sdl.cs                     # Core SDL3 bindings (~7,900 lines)
│   ├── Render.cs                  # Renderer bindings
│   ├── Events.cs                  # Event system
│   ├── Audio.cs                   # Audio subsystem
│   ├── GamePad.cs                 # Gamepad/controller
│   ├── ...                        # Other subsystem files
│   ├── Structs/                   # 128 struct definitions
│   └── Enums/                     # 114 enum definitions
├── tests/
│   └── SharpSDL3.Tests/
│       ├── SharpSDL3.Tests.csproj
│       ├── SdlCoreTests.cs
│       ├── SdlValidationTests.cs
│       ├── RenderTests.cs
│       ├── EventsTests.cs
│       ├── GamePadTests.cs
│       ├── StorageTests.cs
│       ├── MutexTests.cs
│       ├── CameraTests.cs
│       ├── LoggerTests.cs
│       ├── MouseTests.cs
│       ├── StructLayoutTests.cs
│       ├── AtomicIntTests.cs
│       ├── ColorTests.cs
│       ├── EnumTests.cs
│       ├── ConstantsTests.cs
│       ├── SdlExceptionTests.cs
│       └── FuzzTests.cs
├── SharpSDL3.sln
└── .github/workflows/
    ├── test.yml                   # Test + coverage pipeline
    ├── ci.yml                     # Existing build-only CI
    └── build.yml                  # Existing SonarQube analysis
```

---

## Running Tests

### All tests

```bash
dotnet test tests/SharpSDL3.Tests/SharpSDL3.Tests.csproj -c Release
```

### Fuzz tests only

```bash
dotnet test tests/SharpSDL3.Tests/SharpSDL3.Tests.csproj -c Release \
  --filter "FullyQualifiedName~FuzzTests"
```

### Unit tests only (exclude fuzz)

```bash
dotnet test tests/SharpSDL3.Tests/SharpSDL3.Tests.csproj -c Release \
  --filter "FullyQualifiedName!~FuzzTests"
```

### Single test file

```bash
dotnet test tests/SharpSDL3.Tests/SharpSDL3.Tests.csproj -c Release \
  --filter "FullyQualifiedName~AtomicIntTests"
```

---

## Adding New Tests

1. Create a new `.cs` file in `tests/SharpSDL3.Tests/`
2. Use the `SharpSDL3.Tests` namespace
3. Add `[Fact]` for single-case tests or `[Theory]` with `[InlineData]` for parameterized tests
4. For validation guard tests: pass null/zero/empty inputs and assert the expected exception or return value
5. For struct tests: verify `Marshal.SizeOf`, field offsets, and round-trip marshalling
6. For fuzz tests: use a deterministic `Random` seed and run at least 1,000 iterations

### Test Naming Convention

```
{MethodUnderTest}_{Scenario}_{ExpectedBehavior}
```

Examples:
- `CreateWindow_NullTitle_ReturnsZero`
- `BlitSurface_NullSource_ReturnsFalse`
- `FlushEvents_MinGreaterThanMax_ThrowsArgumentException`
- `Fuzz_VersionNum_RandomInputs_NeverThrows`
