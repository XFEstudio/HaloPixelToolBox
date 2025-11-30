using HaloPixelToolBox.Installer.ViewModel.Pages;
using System.Windows;
using System.Windows.Controls;

namespace HaloPixelToolBox.Installer.Views.Pages
{
    /// <summary>
    /// DownloadProgressPage.xaml 的交互逻辑
    /// </summary>
    public partial class DownloadProgressPage : Page
    {
        public static DownloadProgressPage? Current { get; set; }
        public DownloadProgressPageViewModel ViewModel { get; set; }
        public DownloadProgressPage()
        {
            DataContext = ViewModel = new(Current = this);
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e) => await ViewModel.RetryCommand.ExecuteAsync(null);
    }
}
