# Contributing to SharpSDL3

Thanks for taking the time to contribute! ❤️

SharpSDL3 is a C# mirror of SDL3, written in a C#-style API (no `SDL_` prefixes). The bindings are largely in place. What the project needs most is **documentation**, **optimization**, and **stable builds on Linux**. If you want to help with any of those, you're in the right place.

If you like the project but don't have time to contribute, you can still help by starring the repo, mentioning it in your own project's README, or telling a friend.

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [I Have a Question](#i-have-a-question)
- [Ways to Help](#ways-to-help)
- [Legal Notice](#legal-notice)
- [Reporting Bugs](#reporting-bugs)
- [Suggesting Enhancements](#suggesting-enhancements)
- [Your First Code Contribution](#your-first-code-contribution)
- [Improving the Documentation](#improving-the-documentation)
- [Style Guide](#style-guide)
- [Join the Project Team](#join-the-project-team)

## Code of Conduct

This project and everyone participating in it is governed by the
[SharpSDL3 Code of Conduct](https://github.com/Blizzardo1/SharpSDL3/blob/main/CODE_OF_CONDUCT.md).
By participating, you are expected to uphold it. Report unacceptable behavior to <conduct@blizzeta.net>.

## I Have a Question

- Search the [existing issues](https://github.com/Blizzardo1/SharpSDL3/issues) first. If one fits, comment there.
- Otherwise, ask on [Discord](https://discord.gg/TYDwY4ZP) or open a [new issue](https://github.com/Blizzardo1/SharpSDL3/issues/new) with as much context as you can: your OS, .NET SDK version, SDL3 version, and what you tried.
- Prefer IRC? Join `#programming` on `irc.geekshed.net`.

## Ways to Help

Not sure where to start? These are the most useful contributions right now:

- **Document a function.** Pick any public API with no XML doc comment and add one (see [Improving the Documentation](#improving-the-documentation)). A single documented function is a welcome PR.
- **Add a sample.** A minimal "open a window and handle events" program, or a small demo for a subsystem like audio, input, or rendering.
- **Add or improve tests** in `tests/SharpSDL3.Tests`. See [TESTING.md](TESTING.md).
- **Test on your platform** (Linux, macOS, Windows, or others) and report what breaks.
- **Optimize** hot paths and unnecessary allocations.
- **Pick something off [TODO.md](TODO.md)** or an issue labeled `good first issue` or `help wanted`.

For how the code is organized, read [ARCHITECTURE.md](ARCHITECTURE.md) first.

## Legal Notice

SharpSDL3 is released under the zlib license (see [LICENSE](LICENSE)). When contributing, you must agree that you authored 100% of the content, that you have the necessary rights to it, and that it may be provided under the project license.

## Reporting Bugs

### Before submitting

- Use the latest version of SharpSDL3.
- Check that it's really a bug and not an environment problem (incompatible .NET SDK or SDL3 version, missing native library).
- Search the [bug tracker](https://github.com/Blizzardo1/SharpSDL3/issues?q=label%3Abug) and the [SDL Wiki](https://wiki.libsdl.org/SDL3/FrontPage) for the same behavior.
- Collect: stack trace, OS and architecture, .NET SDK version (`dotnet --info`), SDL3 native library version, and the smallest code that reproduces the problem.

### How to submit

> **Never report security issues in public.** Email <bugz@blizzeta.net> instead.

Open an [issue](https://github.com/Blizzardo1/SharpSDL3/issues/new) and include:

- What you expected to happen and what actually happened.
- Step-by-step reproduction, ideally a reduced test case.
- The information you collected above.

A maintainer will label the issue. If it can't be reproduced, it gets `needs-repro` and won't be worked on until it can be. Reproducible bugs are marked `needs-fix`, plus other labels such as `critical` where it applies.

## Suggesting Enhancements

Before opening a suggestion:

- Check that you're on the latest version.
- Read the [README](https://github.com/Blizzardo1/SharpSDL3/blob/main/README.md) to see if it's already covered.
- Search [issues](https://github.com/Blizzardo1/SharpSDL3/issues) for existing suggestions and comment there rather than duplicating.
- Consider whether it fits the project's scope: a C#-friendly mirror of SDL3. Features useful to most users are a better fit than niche ones.

When you open one, include a clear title, a detailed description, the current behavior versus what you'd expect, and why it would help most SharpSDL3 users.

## Your First Code Contribution

### Setup

1. Fork the repository and clone your fork.
2. Install the .NET SDK version the project targets (check the `.csproj` files and `global.json` if present).
3. Install the SDL3 native library for your platform. The bindings load it at runtime.
4. Build and test:
   ```bash
   dotnet build SharpSDL3.sln
   dotnet test
   ```

### Workflow

1. Create a branch from `main` (for example `docs/sdl-window` or `fix/audio-leak`).
2. Make your change, keeping it focused on one thing.
3. Run the build and tests locally.
4. Open a pull request against `main` and describe what changed and why.
5. A maintainer will review it. Expect some back-and-forth.

### Generated code

Much of the binding code is generated from SDL's headers (see `generate_json.sh` and [ARCHITECTURE.md](ARCHITECTURE.md)). If a file is generated, change the generator rather than hand-editing its output. When updating the bindings, search generated files for `WARN_` to find definitions the generator couldn't handle and that need manual attention.

## Improving the Documentation

Documentation is the project's biggest gap, and the easiest way to start contributing.

- Add XML doc comments to public types and members.
- Link each member to its SDL3 Wiki page.
- Add examples to the README or a `docs/` page.

Example:

```csharp
/// <summary>
/// Creates a window with the given title, size, and flags.
/// </summary>
/// <param name="title">The window title, as UTF-8.</param>
/// <param name="width">The width of the window, in pixels.</param>
/// <param name="height">The height of the window, in pixels.</param>
/// <param name="flags">Window creation flags.</param>
/// <returns>The new window, or <c>null</c> on failure.</returns>
/// <remarks>
/// Wraps <c>SDL_CreateWindow</c>. See
/// <see href="https://wiki.libsdl.org/SDL3/SDL_CreateWindow">the SDL3 Wiki</see>.
/// </remarks>
```

Write descriptions in your own words. Don't copy SDL's documentation verbatim. Summarize it and link to the Wiki for the full details.

## Style Guide

- Public API uses C# naming (`PascalCase`) with the `SDL_` prefix dropped. The original SDL-style declarations stay private.
- Keep `nint` and pointer handling out of the public surface where a safer type or overload is possible.
- Follow the existing formatting in the file you're editing.
- Commit messages: a short imperative summary (about 72 characters or fewer), such as `Document SDL_Window functions`. Add a body if the change needs explanation.

## Join the Project Team

If you'd like to become a regular contributor, email <developer@blizzeta.net> with your GitHub username and a short note about yourself and your past work.

## Attribution

This guide is based on [contributing-gen](https://github.com/bttger/contributing-gen).
