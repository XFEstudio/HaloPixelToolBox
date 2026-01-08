using HaloPixelToolBox.Profiles.CrossVersionProfiles;
using HaloPixelToolBox.Utilities;
using XFEExtension.NetCore.FileExtension;
using XFEExtension.NetCore.WinUIHelper.Interface.Services;
using XFEExtension.NetCore.WinUIHelper.Utilities;
using XFEExtension.NetCore.WinUIHelper.Utilities.Helper;
using XFEExtension.NetCore.XFEConsole;

namespace HaloPixelToolBox
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
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
            MainWindow.Closed += MainWindow_Closed;
            MainWindow.Content = new AppShellPage();
            MainWindow.Activate();
            AppThemeHelper.MainWindow = MainWindow;
        }

        private void MainWindow_Closed(object sender, WindowEventArgs args)
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
            Current.Exit();
        }
    }
}
