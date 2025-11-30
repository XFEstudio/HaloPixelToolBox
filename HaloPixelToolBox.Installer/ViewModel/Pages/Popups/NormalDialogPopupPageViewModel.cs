using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HaloPixelToolBox.Installer.Views.Pages.Popups;
using System.Windows;

namespace HaloPixelToolBox.Installer.ViewModel.Pages.Popups
{
    public partial class NormalDialogPopupPageViewModel(NormalDialogPopupPage viewPage) : ViewModelBase
    {
        [ObservableProperty]
        string confirmText = "确定";
        [ObservableProperty]
        string yesText = "是";
        [ObservableProperty]
        string noText = "否";
        [ObservableProperty]
        string cancelText = "取消";
        [ObservableProperty]
        GridLength confirmGridLength = new(0);
        [ObservableProperty]
        GridLength yesGridLength = new(0);
        [ObservableProperty]
        GridLength noGridLength = new(0);
        [ObservableProperty]
        GridLength cancelGridLength = new(0);
        [ObservableProperty]
        object? content;

        public NormalDialogPopupPage ViewPage { get; set; } = viewPage;
        [RelayCommand]
        void Confirm()
        {
            if (ViewPage.PopupWindow is null)
                throw new InvalidOperationException("无法找到父窗口，未在用户返回前设置PopupWindow");
            ViewPage.PopupWindow.Result = MessageBoxResult.OK;
            ViewPage.PopupWindow.Close();
        }

        [RelayCommand]
        void Yes()
        {
            if (ViewPage.PopupWindow is null)
                throw new InvalidOperationException("无法找到父窗口，未在用户返回前设置PopupWindow");
            ViewPage.PopupWindow.Result = MessageBoxResult.Yes;
            ViewPage.PopupWindow.Close();
        }

        [RelayCommand]
        void No()
        {
            if (ViewPage.PopupWindow is null)
                throw new InvalidOperationException("无法找到父窗口，未在用户返回前设置PopupWindow");
            ViewPage.PopupWindow.Result = MessageBoxResult.No;
            ViewPage.PopupWindow.Close();
        }

        [RelayCommand]
        void Cancel()
        {
            if (ViewPage.PopupWindow is null)
                throw new InvalidOperationException("无法找到父窗口，未在用户返回前设置PopupWindow");
            ViewPage.PopupWindow.Result = MessageBoxResult.Cancel;
            ViewPage.PopupWindow.Close();
        }
    }
}
