This is SDL3-CS, C# bindings for SDL3.  

This is SharpSDL3, a C# wrapper for SDL3. Originally, this stems from [flibitijibibo's work](https://github.com/flibitijibibo/SDL2-CS), but since he dislikes adapting the APIs to a more C#-like style (dropping `SDL_` prefixes etc.), here's a fork that does adapt the style.

This project is also inspired by [GeorchW's SharpSDL2](https://github.com/GeorchW/SharpSDL2).  

## License

SDL3 and SDL3-CS are released under the zlib license. See LICENSE for details.  
  
## About SDL3

For more information about SDL3, visit the [SDL Wiki](https://wiki.libsdl.org/SDL3/FrontPage).

## About the C# bindings

The bindings are auto-generated from the GenerateBindings subproject.  
The generator depends on JSON output from the c2ffi project: https://github.com/rpav/c2ffi  
  
SDL3.Legacy.cs contains legacy bindings that will work with any .NET project that supports at least C# language version 4.  
  
SDL3.Core.cs contains CoreCLR-specific bindings that will only work with .NET 8+. These bindings may provide improved performance if you are able to use them.  
  
SDL3-CS is pure function-by-function interop with the C headers.  
You are encouraged to write your own wrapper objects if you care about "appropriate" C# style. <-- That's what this project is about. Making it C# Appropriate.  

The SDL headers themselves do not provide enough information to generate complete C# bindings.
If you update the bindings, search "WARN_" in generated files for unhandled definitions or those that require manual intervention.

## Contributions
I welcome all contributions to make this as great as possible to provide an excellent stomping ground for new developers to get their feet wet, or seasoned developers to crank out new games and/or software that fits their general purpose needs.  

The most important part is getting the functions setup without the SDL_ labeling for public functions, but I've left the original declarations private and alone.
