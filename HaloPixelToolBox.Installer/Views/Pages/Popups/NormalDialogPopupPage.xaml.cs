using HaloPixelToolBox.Installer.Model;
using HaloPixelToolBox.Installer.ViewModel.Pages.Popups;
using HaloPixelToolBox.Installer.Views.Windows;
using System.Windows.Controls;

namespace HaloPixelToolBox.Installer.Views.Pages.Popups
{
    /// <summary>
    /// NormalDialogPopupPage.xaml 的交互逻辑
    /// </summary>
    public partial class NormalDialogPopupPage : Page, IPopupPage
    {
        public NormalDialogPopupPageViewModel ViewModel { get; set; }
        public PopupWindow? PopupWindow { get; set; }

        public NormalDialogPopupPage()
        {
            DataContext = ViewModel = new(this);
            InitializeComponent();
        }
    }
}
