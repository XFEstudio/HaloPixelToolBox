using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HaloPixelToolBox.Installer.Views.Controls
{
    /// <summary>
    /// ToolButton.xaml 的交互逻辑
    /// </summary>
    public partial class MiniToolButton : UserControl
    {
        #region DependencyProperty
        public string ToolName
        {
            get { return (string)GetValue(ToolNameProperty); }
            set { SetValue(ToolNameProperty, value); }
        }
        public static readonly DependencyProperty ToolNameProperty = DependencyProperty.Register("ToolName", typeof(string), typeof(MiniToolButton), new PropertyMetadata("未命名工具"));

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(MiniToolButton), new PropertyMetadata(null));

        public ImageSource IconSource
        {
            get { return (ImageSource)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }
        public static readonly DependencyProperty IconSourceProperty = DependencyProperty.Register("IconSource", typeof(ImageSource), typeof(MiniToolButton), new PropertyMetadata(new BitmapImage(new("pack://application:,,,/Resources/Image/wrench_tool.png"))));

        public Brush TextColor
        {
            get { return (Brush)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }
        public static readonly DependencyProperty TextColorProperty = DependencyProperty.Register("TextColor", typeof(Brush), typeof(MiniToolButton), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public Brush ProgressForeground
        {
            get { return (Brush)GetValue(ProgressForegroundProperty); }
            set { SetValue(ProgressForegroundProperty, value); }
        }
        public static readonly DependencyProperty ProgressForegroundProperty = DependencyProperty.Register("ProgressForeground", typeof(Brush), typeof(MiniToolButton), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(255, 151, 88))));

        public Brush ProgressBackground
        {
            get { return (Brush)GetValue(ProgressBackgroundProperty); }
            set { SetValue(ProgressBackgroundProperty, value); }
        }
        public static readonly DependencyProperty ProgressBackgroundProperty = DependencyProperty.Register("ProgressBackground", typeof(Brush), typeof(MiniToolButton), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public Brush ProgressBorderBrush
        {
            get { return (Brush)GetValue(ProgressBorderBrushProperty); }
            set { SetValue(ProgressBorderBrushProperty, value); }
        }
        public static readonly DependencyProperty ProgressBorderBrushProperty = DependencyProperty.Register("ProgressBorderBrush", typeof(Brush), typeof(MiniToolButton), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(255, 151, 88))));

        public double ProgressLargeChange
        {
            get { return (double)GetValue(ProgressLargeChangeProperty); }
            set { SetValue(ProgressLargeChangeProperty, value); }
        }
        public static readonly DependencyProperty ProgressLargeChangeProperty = DependencyProperty.Register("ProgressLargeChange", typeof(double), typeof(MiniToolButton), new PropertyMetadata(1d));

        public double ProgressSmallChange
        {
            get { return (double)GetValue(ProgressSmallChangeProperty); }
            set { SetValue(ProgressSmallChangeProperty, value); }
        }
        public static readonly DependencyProperty ProgressSmallChangeProperty = DependencyProperty.Register("ProgressSmallChange", typeof(double), typeof(MiniToolButton), new PropertyMetadata(0.1d));

        public double ProgressMaximum
        {
            get { return (double)GetValue(ProgressMaximumProperty); }
            set { SetValue(ProgressMaximumProperty, value); }
        }
        public static readonly DependencyProperty ProgressMaximumProperty = DependencyProperty.Register("ProgressMaximum", typeof(double), typeof(MiniToolButton), new PropertyMetadata(100d));

        public double ProgressMinimum
        {
            get { return (double)GetValue(ProgressMinimumProperty); }
            set { SetValue(ProgressMinimumProperty, value); }
        }
        public static readonly DependencyProperty ProgressMinimumProperty = DependencyProperty.Register("ProgressMinimum", typeof(double), typeof(MiniToolButton), new PropertyMetadata(0d));

        public double ProgressValue
        {
            get { return (double)GetValue(ProgressValueProperty); }
            set { SetValue(ProgressValueProperty, value); }
        }
        public static readonly DependencyProperty ProgressValueProperty = DependencyProperty.Register("ProgressValue", typeof(double), typeof(MiniToolButton), new PropertyMetadata(0d));

        public Visibility ProgressVisibility
        {
            get { return (Visibility)GetValue(ProgressVisibilityProperty); }
            set { SetValue(ProgressVisibilityProperty, value); }
        }
        public static readonly DependencyProperty ProgressVisibilityProperty = DependencyProperty.Register("ProgressVisibility", typeof(Visibility), typeof(MiniToolButton), new PropertyMetadata(Visibility.Collapsed));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(MiniToolButton), new PropertyMetadata(null));
        #endregion

        public MiniToolButton()
        {
            InitializeComponent();
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e) => scrollTextBlock.IsRolling = scrollTextBlock.NeedRolling;

        private void Button_MouseLeave(object sender, MouseEventArgs e) => scrollTextBlock.IsRolling = false;
    }
}
