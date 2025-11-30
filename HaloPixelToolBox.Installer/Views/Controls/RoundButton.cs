using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HaloPixelToolBox.Installer.Views.Controls
{
    public class RoundButton : Button
    {

        public CornerRadius RoundCornerRadius
        {
            get { return (CornerRadius)GetValue(RoundCornerRadiusProperty); }
            set { SetValue(RoundCornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty RoundCornerRadiusProperty = DependencyProperty.Register("RoundCornerRadius", typeof(CornerRadius), typeof(RoundButton), new PropertyMetadata(new CornerRadius(10)));

        public Brush RoundButtonVisualBackground
        {
            get { return (Brush)GetValue(RoundButtonVisualBackgroundProperty); }
            set { SetValue(RoundButtonVisualBackgroundProperty, value); }
        }
        public static readonly DependencyProperty RoundButtonVisualBackgroundProperty = DependencyProperty.Register("RoundButtonVisualBackground", typeof(Brush), typeof(RoundButton), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(152, 152, 231))));

        public Brush RoundButtonBackground
        {
            get { return (Brush)GetValue(RoundButtonBackgroundProperty); }
            set { SetValue(RoundButtonBackgroundProperty, value); }
        }
        public static readonly DependencyProperty RoundButtonBackgroundProperty = DependencyProperty.Register("RoundButtonBackground", typeof(Brush), typeof(RoundButton), new PropertyMetadata(new SolidColorBrush(new Color
        {
            A = 0,
            R = 255,
            G = 255,
            B = 255
        })));

        public Brush RoundButtonBorderBrush
        {
            get { return (Brush)GetValue(RoundButtonBorderBrushProperty); }
            set { SetValue(RoundButtonBorderBrushProperty, value); }
        }
        public static readonly DependencyProperty RoundButtonBorderBrushProperty = DependencyProperty.Register("RoundButtonBorderBrush", typeof(Brush), typeof(RoundButton), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(152, 152, 231))));

        public Thickness RoundButtonBorderThickness
        {
            get { return (Thickness)GetValue(RoundButtonBorderThicknessProperty); }
            set { SetValue(RoundButtonBorderThicknessProperty, value); }
        }
        public static readonly DependencyProperty RoundButtonBorderThicknessProperty = DependencyProperty.Register("RoundButtonBorderThickness", typeof(Thickness), typeof(RoundButton), new PropertyMetadata(new Thickness(1)));
    }
}
