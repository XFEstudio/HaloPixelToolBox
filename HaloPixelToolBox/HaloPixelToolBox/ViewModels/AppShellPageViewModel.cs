using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Navigation;
using XFEExtension.NetCore.WinUIHelper.Interface.Services;
using XFEExtension.NetCore.WinUIHelper.Utilities;

namespace HaloPixelToolBox.ViewModels
{
    public partial class AppShellPageViewModel : ViewModelBase
    {
        [ObservableProperty]
        bool canGoBack;

        public IDialogService DialogService { get; set; } = ServiceManager.GetService<IDialogService>();
        public INavigationViewService NavigationViewService { get; set; } = ServiceManager.GetService<INavigationViewService>();
        public IMessageService MessageService { get; set; } = ServiceManager.GetService<IMessageService>();
        public ILoadingService LoadingService { get; set; } = ServiceManager.GetService<ILoadingService>();

        public AppShellPageViewModel()
        {
            NavigationViewService.NavigationService.Navigated += NavigationService_Navigated;
        }

        private void NavigationService_Navigated(object? sender, NavigationEventArgs e) => CanGoBack = NavigationViewService.NavigationService.CanGoBack;
    }
}