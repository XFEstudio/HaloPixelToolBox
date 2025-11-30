using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HaloPixelToolBox.Installer.Utilities;
using HaloPixelToolBox.Installer.Views.Controls;
using HaloPixelToolBox.Installer.Views.Pages.Popups;

namespace HaloPixelToolBox.Installer.ViewModel.Pages.Popups
{
    public partial class AgreementDialogPopupPageViewModel(AgreementDialogPopupPage viewPage) : ViewModelBase
    {
        [ObservableProperty]
        string agreementContent = "";
        public AgreementDialogPopupPage ViewPage { get; set; } = viewPage;

        [RelayCommand]
        void Confirm()
        {
            if (ViewPage.PopupWindow is not null)
            {
                ViewPage.PopupWindow.Result = System.Windows.MessageBoxResult.Yes;
                ViewPage.PopupWindow.Close();
            }
        }
    }
}
