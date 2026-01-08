using System.Runtime.InteropServices;

namespace HaloPixelToolBox.Utilities.Helpers.Win32;

public static partial class Win32Helper
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left, Top, Right, Bottom;
    }
}
