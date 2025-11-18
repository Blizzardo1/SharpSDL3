![Social Preview](https://repository-images.githubusercontent.com/981861045/a9c02098-8b5d-48de-b9cf-13a8c6c6cf69)

This is SDL3-CS, C# bindings for SDL3. 

[![CI](https://github.com/Blizzardo1/SharpSDL3/actions/workflows/ci.yml/badge.svg)](https://github.com/Blizzardo1/SharpSDL3/actions/workflows/ci.yml)
![GitHub Release](https://img.shields.io/github/v/release/Blizzardo1/SharpSDL3)
![GitHub Downloads (all assets, all releases)](https://img.shields.io/github/downloads/Blizzardo1/SharpSDL3/total)
![GitHub commit activity (branch)](https://img.shields.io/github/commit-activity/w/Blizzardo1/SharpSDL3/main?label=commit%20activity%20(main))
![GitHub commit activity (branch)](https://img.shields.io/github/commit-activity/w/Blizzardo1/SharpSDL3/dev?label=commit%20activity%20(dev))
![GitHub repo size](https://img.shields.io/github/repo-size/Blizzardo1/SharpSDL3)

![GitHub Issues](https://img.shields.io/github/issues/Blizzardo1/SharpSDL3)
![GitHub Pull Requests](https://img.shields.io/github/issues-pr/Blizzardo1/SharpSDL3)



This is SharpSDL3, a C# wrapper for SDL3. Originally, this stems from [flibitijibibo's work](https://github.com/flibitijibibo/SDL3-CS), but since he dislikes adapting the APIs to a more C#-like style (dropping `SDL_` prefixes etc.), here's a fork that does adapt the style.

This project is also inspired by [GeorchW's SharpSDL2](https://github.com/GeorchW/SharpSDL2).  

## 🪪License

SDL3 and SDL3-CS are released under the zlib license. See LICENSE for details.  
  
## 🚀About SDL3

For more information about SDL3, visit the [SDL Wiki](https://wiki.libsdl.org/SDL3/FrontPage).

## ➡️Status
| Library   | Progress |
------------|-----------
|SDL3       | ⚠️ Works, needs updating|
|SDL3_image | ⚠️ Works, needs updating|
|SDL3_mixer | ⚠️ Works, needs updating|
|SDL3_net   | 🥲 Not Started|
|SDL3_ttf   | ⚠️ Works, needs updating|

## Documentation
|Library | Progress|
--------|----------
|SDL3 | 🧑🏻‍💻Started |
|SDL3_image | 🧑🏻‍💻Started |
|SDL3_mixer | 🧑🏻‍💻Started |
|SDL3_net | 🧑🏻‍💻Started |
|SDL3_ttf | 🧑🏻‍💻Started |



## About the C# bindings

The bindings are auto-generated from the GenerateBindings subproject.  
The generator depends on JSON output from the c2ffi project: https://github.com/rpav/c2ffi  
  
~SDL3.Legacy.cs contains legacy bindings that will work with any .NET project that supports at least C# language version 4.~  
Removed. If you would like to compile legacy, please pull from original Repo.

~SDL3.Core.cs~ This project contains CoreCLR-specific bindings that will only work with .NET 9+. These bindings may provide improved performance if you are able to use them.  

  
[SDL3-CS](https://github.com/flibitijibibo/SDL3-CS) is pure function-by-function interop with the C headers.  
SharpSDL3 aims to provide a full SDL framework.

You are encouraged to write your own wrapper objects if you care about "appropriate" C# style. <-- That's what this project is about. Making it C# Appropriate.  


The SDL headers themselves do not provide enough information to generate complete C# bindings.
If you update the bindings, search "WARN_" in generated files for unhandled definitions or those that require manual intervention.

All nint and nuint Pointer objects refer to managed Pointers. If you have problems deciphering between pointers, then please help provide overloads to their respective class, struct, or enum.

## Contributions
I welcome all contributions to make this as great as possible to provide an excellent stomping ground for new developers to get their feet wet, or seasoned developers to crank out new games and/or software that fits their general purpose needs.  

The most important part is getting the functions setup without the SDL_ labeling for public functions, but I've left the original declarations private and alone.
