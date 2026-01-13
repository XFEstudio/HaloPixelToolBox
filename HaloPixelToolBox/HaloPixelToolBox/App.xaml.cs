using HaloPixelToolBox.Interface.Services;
using HaloPixelToolBox.Profiles.CrossVersionProfiles;
using HaloPixelToolBox.Utilities;
using Microsoft.UI.Dispatching;
using Microsoft.Windows.AppLifecycle;
using XFEExtension.NetCore.WinUIHelper.Interface.Services;
using XFEExtension.NetCore.WinUIHelper.Utilities;
using XFEExtension.NetCore.WinUIHelper.Utilities.Helper;
using XFEExtension.NetCore.XFEConsole;

namespace HaloPixelToolBox;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    public ITrayIconService TrayIconService { get; } = ServiceManager.GetService<ITrayIconService>();
    public ICloseWindowService CloseWindowService { get; } = ServiceManager.GetService<ICloseWindowService>();
    /// <summary>
    /// 主页窗口
    /// </summary>
    public static MainWindow MainWindow { get; set; } = new();

    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        var keyInstance = AppInstance.FindOrRegisterForKey("MainInstance");
        if (!keyInstance.IsCurrent)
        {
            keyInstance.RedirectActivationToAsync(AppInstance.GetCurrent().GetActivatedEventArgs()).AsTask().Wait();
            Environment.Exit(0);
            return;
        }
        XFEConsole.UseXFEConsoleLog();
        XFEConsole.Log.LogPath = Path.Combine(AppPath.LogDictionary, XFEConsole.Log.LogPath);
        Console.WriteLine("正在初始化应用程序...");
        this.InitializeComponent();
        Console.WriteLine("应用程序初始化完成");
        AppThemeHelper.Theme = SystemProfile.Theme;
        PageManager.RegisterPage(typeof(AppShellPage));
        PageManager.RegisterPage(typeof(CloudMusicLyricsToolPage));
        PageManager.RegisterPage(typeof(MainPage));
        PageManager.RegisterPage(typeof(SettingPage));
        UnhandledException += App_UnhandledException;
        AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        AppInstance.GetCurrent().Activated += App_Activated;
        Console.WriteLine("事件订阅完成");
    }

    private void App_Activated(object? sender, AppActivationArguments e)
    {
        if (e.Kind == ExtendedActivationKind.Launch)
        {
            MainWindow.DispatcherQueue.TryEnqueue(() =>
            {
                if (SystemProfile.MinimizeWhenOpen)
                    MainWindow.AppWindow.Show();
                MainWindow.Activate();
            });
        }
    }

    private void CurrentDomain_UnhandledException(object sender, System.UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is Exception ex)
        {
            Console.WriteLine($"[ERROR]{ex.Message}");
            Console.WriteLine($"[TRACE]{ex.StackTrace}");
        }
        else
        {
            Console.WriteLine("[ERROR]发生未处理的异常，但无法获取异常信息");
            Console.WriteLine(e.ExceptionObject.ToString());
        }
    }

    private void CurrentDomain_ProcessExit(object? sender, EventArgs e)
    {
        Console.WriteLine("正在退出...");
        Console.WriteLine("正在保存日志...");
        var logs = Directory.GetFiles(AppPath.LogDictionary);
        if (logs.Length > 10)
        {
            foreach (var log in logs.OrderByDescending(x => x).Skip(10))
            {
                File.Delete(log);
            }
        }
        Console.WriteLine("日志保存成功");
    }

    private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        if (ServiceManager.GetService<IMessageService>() is IMessageService messageService)
        {
            messageService.ShowMessage(e.Message, "发生错误", InfoBarSeverity.Error);
            Console.WriteLine($"[ERROR]{e.Message}");
            Console.WriteLine($"[TRACE]{e.Exception.StackTrace}");
            e.Handled = true;
        }
    }

    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        Console.WriteLine("主窗体启动中...");
        TrayIconService.Initilize(DispatcherQueue.GetForCurrentThread());
        CloseWindowService.Initialize(MainWindow);
        MainWindow.Content = new AppShellPage();
        MainWindow.AppWindow.Resize(new(1900, 1400));
        if (SystemProfile.MinimizeWhenOpen)
            MainWindow.AppWindow.Hide();
        else
            MainWindow.Activate();
        AppThemeHelper.MainWindow = MainWindow;
        Console.WriteLine("主窗体启动完成");
    }
}
