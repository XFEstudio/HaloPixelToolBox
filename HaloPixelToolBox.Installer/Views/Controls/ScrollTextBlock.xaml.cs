using HaloPixelToolBox.Installer.Utilities;
using HaloPixelToolBox.Installer.Views.Behavior;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HaloPixelToolBox.Installer.Views.Controls
{
    /// <summary>
    /// ScrollTextBlock.xaml 的交互逻辑
    /// </summary>
    public partial class ScrollTextBlock : UserControl
    {
        #region DependencyProperty
        public double RollingTimeMillisecond
        {
            get { return (double)GetValue(RollingTimeMillisecondProperty); }
            set { SetValue(RollingTimeMillisecondProperty, value); }
        }
        public static readonly DependencyProperty RollingTimeMillisecondProperty = DependencyProperty.Register("RollingTimeMillisecond", typeof(double), typeof(ScrollTextBlock), new PropertyMetadata(5000d));

        public bool AutoAlignment
        {
            get { return (bool)GetValue(AutoAlignmentProperty); }
            set { SetValue(AutoAlignmentProperty, value); }
        }
        public static readonly DependencyProperty AutoAlignmentProperty = DependencyProperty.Register("AutoAlignment", typeof(bool), typeof(ScrollTextBlock), new PropertyMetadata(true));

        public bool RollingBack
        {
            get { return (bool)GetValue(RollingBackProperty); }
            set { SetValue(RollingBackProperty, value); }
        }
        public static readonly DependencyProperty RollingBackProperty = DependencyProperty.Register("RollingBack", typeof(bool), typeof(ScrollTextBlock), new PropertyMetadata(false));

        public bool NeedRolling
        {
            get { return (bool)GetValue(NeedRollingProperty); }
            set { SetValue(NeedRollingProperty, value); }
        }
        public static readonly DependencyProperty NeedRollingProperty = DependencyProperty.Register("NeedRolling", typeof(bool), typeof(UserControl), new PropertyMetadata(false));

        public bool IsRolling
        {
            get { return (bool)GetValue(IsRollingProperty); }
            set { SetValue(IsRollingProperty, value); if (IsRolling) StartRolling(); else EndRolling(); }
        }
        public static readonly DependencyProperty IsRollingProperty = DependencyProperty.Register("IsRolling", typeof(bool), typeof(ScrollTextBlock), new PropertyMetadata(false));

        public bool AutoRolling
        {
            get { return (bool)GetValue(AutoRollingProperty); }
            set { SetValue(AutoRollingProperty, value); }
        }
        public static readonly DependencyProperty AutoRollingProperty = DependencyProperty.Register("AutoRolling", typeof(bool), typeof(ScrollTextBlock), new PropertyMetadata(true));

        public string InnerText
        {
            get { return (string)GetValue(InnerTextProperty); }
            set { SetValue(InnerTextProperty, value); }
        }
        public static readonly DependencyProperty InnerTextProperty = DependencyProperty.Register("InnerText", typeof(string), typeof(ScrollTextBlock), new PropertyMetadata("请输入文本"));

        public Brush InnerForeground
        {
            get { return (Brush)GetValue(InnerForegroundProperty); }
            set { SetValue(InnerForegroundProperty, value); }
        }
        public static readonly DependencyProperty InnerForegroundProperty = DependencyProperty.Register("InnerForeground", typeof(Brush), typeof(ScrollTextBlock), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public Brush InnerBackground
        {
            get { return (Brush)GetValue(InnerBackgroundProperty); }
            set { SetValue(InnerBackgroundProperty, value); }
        }
        public static readonly DependencyProperty InnerBackgroundProperty = DependencyProperty.Register("InnerBackground", typeof(Brush), typeof(ScrollTextBlock), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public double InnerFontSize
        {
            get { return (double)GetValue(InnerFontSizeProperty); }
            set { SetValue(InnerFontSizeProperty, value); }
        }
        public static readonly DependencyProperty InnerFontSizeProperty = DependencyProperty.Register("InnerFontSize", typeof(double), typeof(ScrollTextBlock), new PropertyMetadata(13d));

        public FontFamily InnerFontFamily
        {
            get { return (FontFamily)GetValue(InnerFontFamilyProperty); }
            set { SetValue(InnerFontFamilyProperty, value); }
        }
        public static readonly DependencyProperty InnerFontFamilyProperty = DependencyProperty.Register("InnerFontFamily", typeof(FontFamily), typeof(ScrollTextBlock), new PropertyMetadata(new FontFamily()));

        public FontWeight InnerFontWeight
        {
            get { return (FontWeight)GetValue(InnerFontWeightProperty); }
            set { SetValue(InnerFontWeightProperty, value); }
        }
        public static readonly DependencyProperty InnerFontWeightProperty = DependencyProperty.Register("InnerFontWeight", typeof(FontWeight), typeof(ScrollTextBlock), new PropertyMetadata(new FontWeight()));

        public Thickness InnerTextMargin
        {
            get { return (Thickness)GetValue(InnerTextMarginProperty); }
            set { SetValue(InnerTextMarginProperty, value); }
        }
        public static readonly DependencyProperty InnerTextMarginProperty = DependencyProperty.Register("InnerTextMargin", typeof(Thickness), typeof(ScrollTextBlock), new PropertyMetadata(new Thickness(10, 0, 0, 0)));

        public double InnerTextOpacity
        {
            get { return (double)GetValue(InnerTextOpacityProperty); }
            set { SetValue(InnerTextOpacityProperty, value); }
        }
        public static readonly DependencyProperty InnerTextOpacityProperty = DependencyProperty.Register("InnerTextOpacity", typeof(double), typeof(ScrollTextBlock), new PropertyMetadata(0.5d));

        public VerticalAlignment InnerTextVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(InnerTextVerticalAlignmentProperty); }
            set { SetValue(InnerTextVerticalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty InnerTextVerticalAlignmentProperty = DependencyProperty.Register("InnerTextVerticalAlignment", typeof(VerticalAlignment), typeof(ScrollTextBlock), new PropertyMetadata(VerticalAlignment.Center));

        public HorizontalAlignment InnerTextHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(InnerTextHorizontalAlignmentProperty); }
            set { SetValue(InnerTextHorizontalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty InnerTextHorizontalAlignmentProperty = DependencyProperty.Register("InnerTextHorizontalAlignment", typeof(HorizontalAlignment), typeof(ScrollTextBlock), new PropertyMetadata(HorizontalAlignment.Center));

        public TextAlignment InnerTextAlignment
        {
            get { return (TextAlignment)GetValue(InnerTextAlignmentProperty); }
            set { SetValue(InnerTextAlignmentProperty, value); }
        }
        public static readonly DependencyProperty InnerTextAlignmentProperty = DependencyProperty.Register("InnerTextAlignment", typeof(TextAlignment), typeof(ScrollTextBlock), new PropertyMetadata(TextAlignment.Center));

        public TextDecorationCollection InnerTextDecorations
        {
            get { return (TextDecorationCollection)GetValue(InnerTextDecorationsProperty); }
            set { SetValue(InnerTextDecorationsProperty, value); }
        }
        public static readonly DependencyProperty InnerTextDecorationsProperty = DependencyProperty.Register("InnerTextDecorations", typeof(TextDecorationCollection), typeof(ScrollTextBlock), new PropertyMetadata(null));
        #endregion

        TextAlignment originalTextAlignment = TextAlignment.Center;

        public ScrollTextBlock()
        {
            InitializeComponent();
            originalTextAlignment = InnerTextAlignment;
        }

        private void ScrollTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            if (textBlock.ActualWidth > scrollViewer.ActualWidth)
            {
                if (AutoAlignment)
                {
                    InnerTextAlignment = TextAlignment.Left;
                    originalTextAlignment = InnerTextAlignment;
                }
                NeedRolling = true;
                if (AutoRolling || IsRolling)
                    StartRolling();
            }
            else
            {
                if (AutoAlignment)
                {
                    InnerTextAlignment = TextAlignment.Center;
                    originalTextAlignment = InnerTextAlignment;
                }
            }
        }

        public void StartRolling()
        {
            SetValue(IsRollingProperty, true);
            textBlock.TextAlignment = TextAlignment.Left;
            if (!RollingBack)
            {
                textBlock.Width = textBlock.ActualWidth + scrollViewer.ActualWidth;
                if (stackPanel.Children.Count > 1) stackPanel.Children.RemoveAt(1);
                var copyOfTextBlock = textBlock.Clone();
                stackPanel.Children.Add(copyOfTextBlock);
            }
            var animation = new DoubleAnimation()
            {
                From = 0,
                To = RollingBack ? textBlock.ActualWidth - scrollViewer.ActualWidth : textBlock.ActualWidth + scrollViewer.ActualWidth,
                Duration = TimeSpan.FromMilliseconds(RollingTimeMillisecond),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = RollingBack
            };
            scrollViewer.BeginAnimation(ScrollViewerBehavior.HorizontalOffsetProperty, animation);
        }

        public void EndRolling()
        {
            SetValue(IsRollingProperty, false);
            InnerTextAlignment = originalTextAlignment;
            if (!RollingBack)
            {
                textBlock.Width = textBlock.ActualWidth;
                if (stackPanel.Children.Count > 1) stackPanel.Children.RemoveAt(1);
            }
            scrollViewer.BeginAnimation(ScrollViewerBehavior.HorizontalOffsetProperty, null);
        }
    }
}
