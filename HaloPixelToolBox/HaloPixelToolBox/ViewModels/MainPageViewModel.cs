using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using XFEExtension.NetCore.WinUIHelper.Interface.Services;
using XFEExtension.NetCore.WinUIHelper.Utilities;

namespace HaloPixelToolBox.ViewModels
{
    public partial class MainPageViewModel : ViewModelBase
    {
        [ObservableProperty]
        int clickCount = 0;

        public IAutoNavigationParameterService<string> AutoNavigationParameterService { get; set; } = ServiceManager.GetService<IAutoNavigationParameterService<string>>();
        public INavigationService? NavigationService { get; } = ServiceManager.GetGlobalService<INavigationService>();
        public IMessageService? MessageService { get; } = ServiceManager.GetGlobalService<IMessageService>();

        public MainPageViewModel()
        {
            AutoNavigationParameterService.ParameterChange += AutoNavigationParameterService_ParameterChange;
        }

        private void AutoNavigationParameterService_ParameterChange(object? sender, string? e)
        {
            MessageService?.ShowMessage($"Parameter: {e}", "Focus", InfoBarSeverity.Warning);
        }

        [RelayCommand]
        void IncrementClickCount()
        {
            ClickCount++;
            MessageService?.ShowMessage($"Click count: {ClickCount}", "Info");
        }
    }
}
