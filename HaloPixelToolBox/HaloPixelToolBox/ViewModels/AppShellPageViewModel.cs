using CommunityToolkit.Mvvm.ComponentModel;
using HaloPixelToolBox.Profiles.CrossVersionProfiles;
using HaloPixelToolBox.Utilities.Helpers;
using Microsoft.UI.Xaml.Navigation;
using XFEExtension.NetCore.WinUIHelper.Interface.Services;
using XFEExtension.NetCore.WinUIHelper.Utilities;

namespace HaloPixelToolBox.ViewModels
{
    public partial class AppShellPageViewModel : ViewModelBase
    {
        [ObservableProperty]
        bool canGoBack;
        [ObservableProperty]
        private string upgradeContentText = string.Empty;

        public IDialogService DialogService { get; set; } = ServiceManager.GetService<IDialogService>();
        public INavigationViewService NavigationViewService { get; set; } = ServiceManager.GetService<INavigationViewService>();
        public Interface.Services.IPageService PageService { get; } = ServiceManager.GetService<Interface.Services.IPageService>();
        public IMessageService MessageService { get; set; } = ServiceManager.GetService<IMessageService>();
        public ILoadingService LoadingService { get; set; } = ServiceManager.GetService<ILoadingService>();

        public AppShellPageViewModel()
        {
            NavigationViewService.NavigationService.Navigated += NavigationService_Navigated;
            PageService.CurrentPageLoaded += CurrentPage_Loaded;
        }

        private async void CurrentPage_Loaded(object sender, RoutedEventArgs e)
        {
            MessageService.ShowMessage("正在检查更新...", "检查更新", InfoBarSeverity.Informational);
            Console.WriteLine("正在检查更新...");
            var upgradeInfo = await UpgradeHelper.GetReleaseNotes();
            if (upgradeInfo.IsLatest)
            {
                MessageService.ShowMessage("当前已是最新版本", "检查更新", InfoBarSeverity.Success);
                Console.WriteLine("当前已是最新版本");
            }
            else
            {
                if (upgradeInfo.LatestVersion == SystemProfile.IgnoreVersion)
                {
                    MessageService.ShowMessage("当前版本已被忽略", "检查更新", InfoBarSeverity.Informational);
                    Console.WriteLine("当前版本已被忽略");
                }
                else
                {
                    MessageService.ShowMessage("检测到新版本", "检查更新", InfoBarSeverity.Informational);
                    Console.WriteLine("检测到新版本");
                    Console.WriteLine($"[DEBUG]最新版本: {upgradeInfo.LatestVersion}");
                    Console.WriteLine($"[DEBUG]当前版本: {UpgradeHelper.Version}");
                    Console.WriteLine($"[DEBUG]更新信息：{upgradeInfo.ReleaseNotes}");
                    UpgradeContentText = upgradeInfo.ReleaseNotes;
                    switch (await DialogService.ShowDialog("upgradeDialog"))
                    {
                        case ContentDialogResult.None:
                            Console.WriteLine("用户取消了更新");
                            break;
                        case ContentDialogResult.Primary:
                            Console.WriteLine("用户选择开始更新...");
                            UpgradeHelper.StartUpdate(upgradeInfo.DownloadUrl);
                            break;
                        case ContentDialogResult.Secondary:
                            Console.WriteLine("用户选择忽略当前版本");
                            SystemProfile.IgnoreVersion = upgradeInfo.LatestVersion;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void NavigationService_Navigated(object? sender, NavigationEventArgs e) => CanGoBack = NavigationViewService.NavigationService.CanGoBack;
    }
}