using HaloPixelToolBox.Installer.Model;
using HaloPixelToolBox.Installer.ViewModel.Pages.Popups;
using HaloPixelToolBox.Installer.Views.Windows;
using System.Windows.Controls;

namespace HaloPixelToolBox.Installer.Views.Pages.Popups
{
    /// <summary>
    /// AgreementDialogPopupPage.xaml 的交互逻辑
    /// </summary>
    public partial class AgreementDialogPopupPage : Page, IPopupPage
    {
        public AgreementDialogPopupPageViewModel ViewModel { get; set; }
        public PopupWindow? PopupWindow { get; set; }
        public string Agreement { get => ViewModel.AgreementContent; set => ViewModel.AgreementContent = value; }
        public AgreementDialogPopupPage()
        {
            DataContext = ViewModel = new(this);
            InitializeComponent();
        }
    }
}
