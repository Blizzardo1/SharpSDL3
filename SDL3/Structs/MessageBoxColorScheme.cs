using System.Runtime.InteropServices;

namespace SDL3.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct MessageBoxColorScheme
{
	public MessageBoxColor Colors0;
	public MessageBoxColor Colors1;
	public MessageBoxColor Colors2;
	public MessageBoxColor Colors3;
	public MessageBoxColor Colors4;
}