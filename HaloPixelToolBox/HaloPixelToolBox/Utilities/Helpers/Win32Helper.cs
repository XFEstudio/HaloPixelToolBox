using HaloPixelToolBox.Utilities.Helpers.Win32;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices;
using System.Text;

namespace HaloPixelToolBox.Utilities.Helpers;

public static partial class Win32Helper
{
    public const int SW_HIDE = 0;
    public const int SW_SHOW = 5;

    public const int WH_MOUSE_LL = 14;
    public const int WM_LBUTTONDOWN = 0x0201;

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool UnhookWindowsHookEx(IntPtr hhk);

    [LibraryImport("user32.dll")]
    public static partial IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("shell32.dll", EntryPoint = "#261",
               CharSet = CharSet.Unicode, PreserveSig = false)]
    public static extern void GetUserTilePath(string? username, uint whatever, StringBuilder picpath, int maxLength);

    public static string GetUserTilePath(string? username = null)
    {
        var sb = new StringBuilder(1000);
        GetUserTilePath(username, 0x80000000, sb, sb.Capacity);
        return sb.ToString();
    }

    public static ImageSource GetUserTile(string? username = null) => new BitmapImage(new Uri(GetUserTilePath(username)));
}
