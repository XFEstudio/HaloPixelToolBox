using HaloPixelToolBox.Interface.Services;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Windowing;
using System.Drawing;
using System.Windows.Forms;
using XFEExtension.NetCore.WinUIHelper.Implements.Services;

namespace HaloPixelToolBox.Implements.Services;

public partial class TrayIconService : GlobalServiceBase, ITrayIconService
{
    private readonly NotifyIcon _notifyIcon;
    private DispatcherQueue? _dispatcher;

    public TrayIconService()
    {
        _notifyIcon = new NotifyIcon
        {
            Text = "花再音响工具箱",
            Icon = new Icon("Assets/appicon.ico"), // 一定要是 .ico
            Visible = true
        };

        _notifyIcon.MouseClick += (_, e) =>
        {
            if (e.Button == MouseButtons.Right)
            {
                ShowTrayMenu();
            }
            else
            {
                ShowWindow();
            }
        };

        _notifyIcon.Visible = true;
    }

    public void Initilize(DispatcherQueue dispatcherQueue)
    {
        _dispatcher = dispatcherQueue;
    }

    public void ShowWindow()
    {
        _dispatcher?.TryEnqueue(() =>
        {
            App.MainWindow.Activate();
        });
    }

    private void ShowTrayMenu()
    {
        _dispatcher?.TryEnqueue(() =>
        {
            var menu = new TrayMenuWindow();
            menu.AppWindow.MoveInZOrderAtTop();
            menu.AppWindow.SetPresenter(AppWindowPresenterKind.Overlapped);
            menu.AppWindow.IsShownInSwitchers = false;
            if (menu.AppWindow.Presenter is OverlappedPresenter presenter)
            {
                presenter.SetBorderAndTitleBar(true, false);
                presenter.IsResizable = false;
                presenter.IsMaximizable = false;
                presenter.IsMinimizable = false;
                presenter.IsAlwaysOnTop = true;
            }
            var displayArea = DisplayArea.GetFromPoint(new Windows.Graphics.PointInt32(Cursor.Position.X, Cursor.Position.Y), DisplayAreaFallback.Primary);
            menu.Activate();
        });
    }


    public void ExitApp()
    {
        _notifyIcon.Visible = false;
        Environment.Exit(0);
    }

    public void Dispose() => GC.SuppressFinalize(this);
}