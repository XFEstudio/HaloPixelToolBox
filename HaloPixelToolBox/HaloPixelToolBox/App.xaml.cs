using HaloPixelToolBox.Profiles.CrossVersionProfiles;
using XFEExtension.NetCore.WinUIHelper.Interface.Services;
using XFEExtension.NetCore.WinUIHelper.Utilities;
using XFEExtension.NetCore.WinUIHelper.Utilities.Helper;

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
            this.InitializeComponent();
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
                e.Handled = true;
            }
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            MainWindow.Content = new AppShellPage();
            MainWindow.Activate();
            AppThemeHelper.MainWindow = MainWindow;
        }
    }
}
