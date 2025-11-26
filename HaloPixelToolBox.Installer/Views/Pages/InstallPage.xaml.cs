using HaloPixelToolBox.Installer.ViewModel.Pages;
using System.Windows.Controls;

namespace HaloPixelToolBox.Installer.Views.Pages
{
    /// <summary>
    /// InstallPage.xaml 的交互逻辑
    /// </summary>
    public partial class InstallPage : Page
    {
        public static InstallPage? Current { get; set; }
        public InstallPageViewModel ViewModel { get; set; }
        public InstallPage()
        {
            ViewModel = new(Current = this);
            DataContext = ViewModel;
            InitializeComponent();
        }
    }
}
