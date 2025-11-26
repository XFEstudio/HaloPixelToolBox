using HaloPixelToolBox.Installer.ViewModel.Windows;
using System.Windows;

namespace HaloPixelToolBox.Installer.Views.Windows
{
    /// <summary>
    /// PopupWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PopupWindow : Window
    {
        public static PopupWindow? Current { get; set; }
        public PopupWindowViewModel ViewModel { get; set; }
        public MessageBoxResult? Result { get; set; }
        public PopupWindow()
        {
            Current = this;
            DataContext = ViewModel = new(this);
            InitializeComponent();
        }

        private void CloseWindowImage_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Result = MessageBoxResult.None;
            DialogResult = false;
            Close();
        }

        private void DragTabBorder_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                DragMove();
        }
    }
}
