using HaloPixelToolBox.Installer.Profiles;
using HaloPixelToolBox.Installer.ViewModel.Windows;
using HaloPixelToolBox.Installer.Views.Pages;
using System.Windows;
using XFEExtension.NetCore.InputSimulator;

namespace HaloPixelToolBox.Installer.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow? Current { get; private set; }
        public MainWindowViewModel ViewModel { get; private set; }
        public MainWindow()
        {
            ViewModel = new MainWindowViewModel(Current = this);
            DataContext = ViewModel;
            InitializeComponent();
            Width = SystemProfile.MainWindowWidth;
            Height = SystemProfile.MainWindowHeight;
        }

        private void MinimizeImage_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) => ViewModel.Minimize();

        private void CloseWindowImage_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) => MainWindowViewModel.CloseWindow();

        private void DragTabBorder_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                if (WindowState == WindowState.Maximized)
                {
                    WindowState = WindowState.Normal;
                    var mousePosition = InputSimulator.GetMousePosition();
                    Left = mousePosition.X / SystemProfile.CurrentWindowDPIScale - Width / 2;
                    Top = mousePosition.Y / SystemProfile.CurrentWindowDPIScale - 10;
                }
                DragMove();
            }
        }

        private void DragTabBorder_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ViewModel.CheckDoubleClick(500))
                _ = WindowState == WindowState.Maximized ? WindowState = WindowState.Normal : WindowState = WindowState.Maximized;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.GetDPIScale();
            contentFrame.Content = SystemProfile.StartMode switch
            {
                "Upgrade" => new DownloadProgressPage(),
                _ => new InstallPage(),
            };
        }

        private void CornerBorder_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => ViewModel.InitializeToResize();
    }
}