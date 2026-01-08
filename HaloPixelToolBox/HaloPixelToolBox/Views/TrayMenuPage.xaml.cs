using HaloPixelToolBox.Interface.Services;
using HaloPixelToolBox.Utilities.Helpers;
using HaloPixelToolBox.Utilities.Helpers.Win32;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Windows.Graphics;
using XFEExtension.NetCore.WinUIHelper.Utilities;

namespace HaloPixelToolBox.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class TrayMenuPage : Page
{
    IntPtr _mouseHook = new();
    LowLevelMouseProc? _proc;
    public Window MenuWindow { get; set; }

    public TrayMenuPage(Window window)
    {
        MenuWindow = window;
        InitializeComponent();
        this.Loaded += (_, __) =>
        {
            DispatcherQueue.TryEnqueue(() =>
            {
                ResizeToContent();
            });
        };
        InstallMouseHook();
    }

    void ResizeToContent()
    {
        double width = panel.ActualWidth;
        double height = panel.ActualHeight;

        if (width <= 0 || height <= 0)
            return;

        double scale = Content.XamlRoot.RasterizationScale;

        int w = (int)Math.Ceiling(width * scale);
        int h = (int)Math.Ceiling(height * scale);

        MenuWindow.AppWindow.Resize(new SizeInt32(w, h));
        MenuWindow.AppWindow.Move(new PointInt32(Cursor.Position.X + 5, Cursor.Position.Y - h + 10));
    }

    private void Show_Click(object _, RoutedEventArgs __)
    {
        ServiceManager.GetGlobalService<ITrayIconService>()?.ShowWindow();
        MenuWindow.Close();
    }

    private void Exit_Click(object _, RoutedEventArgs __)
    {
        ServiceManager.GetGlobalService<ITrayIconService>()?.ExitApp();
    }

    void InstallMouseHook()
    {
        _proc = HookCallback;
        _mouseHook = Win32Helper.SetWindowsHookEx(Win32Helper.WH_MOUSE_LL, _proc, IntPtr.Zero, 0);
    }

    IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        try
        {
            if (nCode >= 0 && wParam == Win32Helper.WM_LBUTTONDOWN)
            {
                var hook = Marshal.PtrToStructure<MSLLHOOKSTRUCT>(lParam);
                var pt = hook.pt;

                var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(MenuWindow);
                Win32Helper.GetWindowRect(hwnd, out RECT rect);

                bool inside =
                    pt.x >= rect.Left && pt.x <= rect.Right &&
                    pt.y >= rect.Top && pt.y <= rect.Bottom;

                if (!inside)
                {
                    DispatcherQueue.TryEnqueue(() =>
                    {
                        MenuWindow.Close();
                    });
                }
            }
        }
        catch { }

        return Win32Helper.CallNextHookEx(_mouseHook, nCode, wParam, lParam);
    }
}
