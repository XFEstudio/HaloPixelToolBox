using System.Runtime.InteropServices;

namespace HaloPixelToolBox.Utilities.Helpers.Win32;

public static partial class Win32Helper
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;
    }
}
