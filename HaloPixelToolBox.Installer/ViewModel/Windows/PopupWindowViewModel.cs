using CommunityToolkit.Mvvm.ComponentModel;
using HaloPixelToolBox.Installer.Views.Windows;
using System.Windows;
using System.Windows.Media;

namespace HaloPixelToolBox.Installer.ViewModel.Windows
{
    public partial class PopupWindowViewModel(PopupWindow viewPage) : ViewModelBase
    {
        [ObservableProperty]
        object? content;
        [ObservableProperty]
        Brush popupBackground = new SolidColorBrush(Color.FromRgb(152, 152, 231));
        [ObservableProperty]
        Brush popupBorder = new SolidColorBrush(Color.FromRgb(80, 80, 183));
        [ObservableProperty]
        Brush popupContentBackground = new SolidColorBrush(Colors.White);
        [ObservableProperty]
        Thickness popupThickness = new(2);
        [ObservableProperty]
        Visibility closeButtonVisibility = Visibility.Visible;
        [ObservableProperty]
        Visibility moveButtonVisibility = Visibility.Visible;

        public PopupWindow ViewPage { get; set; } = viewPage;
    }
}
