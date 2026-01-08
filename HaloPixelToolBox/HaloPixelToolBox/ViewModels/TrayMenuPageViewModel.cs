using CommunityToolkit.Mvvm.Input;
using HaloPixelToolBox.Interface.Services;
using HaloPixelToolBox.Utilities.Helpers;
using HaloPixelToolBox.Utilities.Helpers.Win32;
using HaloPixelToolBox.Views;
using Microsoft.UI.Dispatching;
using System.Runtime.InteropServices;
using XFEExtension.NetCore.WinUIHelper.Implements;
using XFEExtension.NetCore.WinUIHelper.Utilities;

namespace HaloPixelToolBox.ViewModels;

public partial class TrayMenuPageViewModel : ServiceBaseViewModelBase<string>
{
    IntPtr _mouseHook = new();
    LowLevelMouseProc? _proc;
    public Window MenuWindow { get; set; }
    public DispatcherQueue DispatcherQueue => MenuWindow.DispatcherQueue;

    public TrayMenuPageViewModel(Window window)
    {
        MenuWindow = window;
        AutoNavigationParameterService.ParameterChange += AutoNavigationParameterService_ParameterChange;
    }

    private void AutoNavigationParameterService_ParameterChange(object? sender, string? e)
    {
        if (AutoNavigationParameterService.CurrentPage is not null)
        {
            AutoNavigationParameterService.CurrentPage.Unloaded += CurrentPage_Unloaded;
        }
        InstallMouseHook();
    }

    private void CurrentPage_Unloaded(object sender, RoutedEventArgs e)
    {
        UnInstallMouseHook();
    }

    void UnInstallMouseHook()
    {
        if (_mouseHook != IntPtr.Zero)
        {
            Win32Helper.UnhookWindowsHookEx(_mouseHook);
            _mouseHook = IntPtr.Zero;
        }
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

    [RelayCommand]
    void Show()
    {
        ServiceManager.GetGlobalService<ITrayIconService>()?.ShowWindow();
        NavigationViewService?.NavigateTo<CloudMusicLyricsToolPage>();
        MenuWindow.Close();
    }

    [RelayCommand]
    void OpenSettings()
    {
        ServiceManager.GetGlobalService<ITrayIconService>()?.ShowWindow();
        NavigationViewService?.NavigateTo<SettingPage>();
        MenuWindow.Close();
    }

    [RelayCommand]
    static void Exit() => ServiceManager.GetGlobalService<ITrayIconService>()?.ExitApp();
}
