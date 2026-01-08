using System.Runtime.InteropServices;

namespace HaloPixelToolBox.Utilities.Helpers.Win32;

[StructLayout(LayoutKind.Sequential)]
public struct RECT
{
    public int Left, Top, Right, Bottom;
}