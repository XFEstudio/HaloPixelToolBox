using System.Runtime.InteropServices;

namespace HaloPixelToolBox.Utilities.Helpers;

public static partial class Win32Helper
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MSLLHOOKSTRUCT
    {
        public POINT pt;
        public uint mouseData;
        public uint flags;
        public uint time;
        public IntPtr dwExtraInfo;
    }
}
