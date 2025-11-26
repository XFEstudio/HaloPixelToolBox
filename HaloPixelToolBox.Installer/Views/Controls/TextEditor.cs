using System.Windows;
using System.Windows.Media;

namespace HaloPixelToolBox.Installer.Views.Controls
{
    public class TextEditor : HintTextBox
    {
        public CornerRadius EditorCornerRadius
        {
            get { return (CornerRadius)GetValue(EditorCornerRadiusProperty); }
            set { SetValue(EditorCornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty EditorCornerRadiusProperty = DependencyProperty.Register("EditorCornerRadius", typeof(CornerRadius), typeof(TextEditor), new PropertyMetadata(new CornerRadius(10)));

        public Thickness EditorBorderThickness
        {
            get { return (Thickness)GetValue(EditorBorderThicknessProperty); }
            set { SetValue(EditorBorderThicknessProperty, value); }
        }
        public static readonly DependencyProperty EditorBorderThicknessProperty = DependencyProperty.Register("EditorBorderThickness", typeof(Thickness), typeof(TextEditor), new PropertyMetadata(new Thickness(1)));

        public Brush EditorBorderBrush
        {
            get { return (Brush)GetValue(EditorBorderBrushProperty); }
            set { SetValue(EditorBorderBrushProperty, value); }
        }
        public static readonly DependencyProperty EditorBorderBrushProperty = DependencyProperty.Register("EditorBorderBrush", typeof(Brush), typeof(TextEditor), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(152, 152, 231))));
    }
}
