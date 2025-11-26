using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HaloPixelToolBox.Installer.Views.Controls
{
    public class HintTextBox : TextBox
    {
        public string HintText
        {
            get { return (string)GetValue(HintTextProperty); }
            set { SetValue(HintTextProperty, value); }
        }
        public static readonly DependencyProperty HintTextProperty = DependencyProperty.Register("HintText", typeof(string), typeof(HintTextBox), new PropertyMetadata("请输入文本"));

        public Brush HintForeground
        {
            get { return (Brush)GetValue(HintForegroundProperty); }
            set { SetValue(HintForegroundProperty, value); }
        }
        public static readonly DependencyProperty HintForegroundProperty = DependencyProperty.Register("HintForeground", typeof(Brush), typeof(HintTextBox), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public Brush HintBackground
        {
            get { return (Brush)GetValue(HintBackgroundProperty); }
            set { SetValue(HintBackgroundProperty, value); }
        }
        public static readonly DependencyProperty HintBackgroundProperty = DependencyProperty.Register("HintBackground", typeof(Brush), typeof(HintTextBox), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public double HintFontSize
        {
            get { return (double)GetValue(HintFontSizeProperty); }
            set { SetValue(HintFontSizeProperty, value); }
        }
        public static readonly DependencyProperty HintFontSizeProperty = DependencyProperty.Register("HintFontSize", typeof(double), typeof(HintTextBox), new PropertyMetadata(13d));

        public FontFamily HintFontFamily
        {
            get { return (FontFamily)GetValue(HintFontFamilyProperty); }
            set { SetValue(HintFontFamilyProperty, value); }
        }
        public static readonly DependencyProperty HintFontFamilyProperty = DependencyProperty.Register("HintFontFamily", typeof(FontFamily), typeof(HintTextBox), new PropertyMetadata(new FontFamily()));

        public FontWeight HintFontWeight
        {
            get { return (FontWeight)GetValue(HintFontWeightProperty); }
            set { SetValue(HintFontWeightProperty, value); }
        }
        public static readonly DependencyProperty HintFontWeightProperty = DependencyProperty.Register("HintFontWeight", typeof(FontWeight), typeof(HintTextBox), new PropertyMetadata(new FontWeight()));

        public Thickness HintTextMargin
        {
            get { return (Thickness)GetValue(HintTextMarginProperty); }
            set { SetValue(HintTextMarginProperty, value); }
        }
        public static readonly DependencyProperty HintTextMarginProperty = DependencyProperty.Register("HintTextMargin", typeof(Thickness), typeof(HintTextBox), new PropertyMetadata(new Thickness(10, 0, 0, 0)));

        public double HintTextOpacity
        {
            get { return (double)GetValue(HintTextOpacityProperty); }
            set { SetValue(HintTextOpacityProperty, value); }
        }
        public static readonly DependencyProperty HintTextOpacityProperty = DependencyProperty.Register("HintTextOpacity", typeof(double), typeof(HintTextBox), new PropertyMetadata(0.5d));

        public VerticalAlignment HintTextVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(HintTextVerticalAlignmentProperty); }
            set { SetValue(HintTextVerticalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty HintTextVerticalAlignmentProperty = DependencyProperty.Register("HintTextVerticalAlignment", typeof(VerticalAlignment), typeof(HintTextBox), new PropertyMetadata(VerticalAlignment.Center));

        public HorizontalAlignment HintTextHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(HintTextHorizontalAlignmentProperty); }
            set { SetValue(HintTextHorizontalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty HintTextHorizontalAlignmentProperty = DependencyProperty.Register("HintTextHorizontalAlignment", typeof(HorizontalAlignment), typeof(HintTextBox), new PropertyMetadata(HorizontalAlignment.Left));
    }
}
