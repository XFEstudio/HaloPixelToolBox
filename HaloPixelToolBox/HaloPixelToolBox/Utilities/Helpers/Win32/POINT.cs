using System.Runtime.InteropServices;

namespace HaloPixelToolBox.Utilities.Helpers.Win32;

[StructLayout(LayoutKind.Sequential)]
public struct POINT
{
    public int x;
    public int y;
}