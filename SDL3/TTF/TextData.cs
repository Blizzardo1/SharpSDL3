using SharpSDL3.Structs;
using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public struct TextData {
    public Font Font;                // TTF_Font* font
    public FColor Color;             // SDL_FColor color

    public bool NeedsLayoutUpdate;   // True if the layout needs to be updated
    public nint Layout;              // TTF_TextLayout* layout
    public int X;                    // The x offset of the upper left corner of this text, in pixels
    public int Y;                    // The y offset of the upper left corner of this text, in pixels
    public int W;                    // The width of this text, in pixels
    public int H;                    // The height of this text, in pixels
    public int NumOps;               // The number of drawing operations to render this text
    public nint Ops;                 // TTF_DrawOperation* ops
    public int NumClusters;          // The number of substrings representing clusters of glyphs in the string
    public nint Clusters;            // TTF_SubString* clusters

    public ulong Props;              // SDL_PropertiesID props

    public bool NeedsEngineUpdate;   // True if the engine text needs to be updated
    public nint Engine;              // TTF_TextEngine* engine
    public nint EngineText;          // void* engine_text
}