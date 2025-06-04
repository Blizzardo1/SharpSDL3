using System.Runtime.InteropServices;

namespace SharpSDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct MessageBoxColorScheme {
    public MessageBoxColor Background;
    public MessageBoxColor Text;
    public MessageBoxColor ButtonBorder;
    public MessageBoxColor ButtonBackground;
    public MessageBoxColor ButtonSelected;
}