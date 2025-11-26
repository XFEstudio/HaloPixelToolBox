using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HaloPixelToolBox.Installer.Views.Controls
{
    /// <summary>
    /// ModernProgressBar.xaml 的交互逻辑
    /// </summary>
    public partial class ModernProgressBar : UserControl
    {
        public event EventHandler<double>? ProgressChanged;

        #region DependencyProperty
        public CornerRadius ProgressBarCornerRadius
        {
            get { return (CornerRadius)GetValue(ProgressBarCornerRadiusProperty); }
            set { SetValue(ProgressBarCornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty ProgressBarCornerRadiusProperty = DependencyProperty.Register("ProgressBarCornerRadius", typeof(CornerRadius), typeof(ModernProgressBar), new PropertyMetadata(new CornerRadius(10)));

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set
            {
                SetValue(IsBusyProperty, value);
                SetBusy();
            }
        }
        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register("IsBusy", typeof(bool), typeof(ModernProgressBar), new PropertyMetadata(false));

        public bool IsError
        {
            get { return (bool)GetValue(IsErrorProperty); }
            set
            {
                SetValue(IsErrorProperty, value);
                SetError();
            }
        }
        public static readonly DependencyProperty IsErrorProperty = DependencyProperty.Register("IsError", typeof(bool), typeof(ModernProgressBar), new PropertyMetadata(false));

        public bool IsPause
        {
            get { return (bool)GetValue(IsPauseProperty); }
            set
            {
                SetValue(IsPauseProperty, value);
                SetPause();
            }
        }
        public static readonly DependencyProperty IsPauseProperty = DependencyProperty.Register("IsPause", typeof(bool), typeof(ModernProgressBar), new PropertyMetadata(false));

        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set
            {
                SetValue(MaxValueProperty, value);
                SetTopWidth();
            }
        }
        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(double), typeof(ModernProgressBar), new PropertyMetadata(100d));

        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set
            {
                SetValue(MinValueProperty, value);
                SetTopWidth();
            }
        }
        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register("MinValue", typeof(double), typeof(ModernProgressBar), new PropertyMetadata(0d));

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set
            {
                SetValue(ValueProperty, value);
                ProgressChanged?.Invoke(this, value);
                SetTopWidth();
            }
        }
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(ModernProgressBar), new PropertyMetadata(0d));

        public Brush ProgressColor
        {
            get { return (Brush)GetValue(ProgressColorProperty); }
            set { SetValue(ProgressColorProperty, value); }
        }
        public static readonly DependencyProperty ProgressColorProperty = DependencyProperty.Register("ProgressColor", typeof(Brush), typeof(ModernProgressBar), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(152, 152, 231))));

        public Brush ProgressErrorColor
        {
            get { return (Brush)GetValue(ProgressErrorColorProperty); }
            set { SetValue(ProgressErrorColorProperty, value); }
        }
        public static readonly DependencyProperty ProgressErrorColorProperty = DependencyProperty.Register("ProgressErrorColor", typeof(Brush), typeof(ModernProgressBar), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(255, 72, 72))));

        public Brush ProgressPauseColor
        {
            get { return (Brush)GetValue(ProgressPauseColorProperty); }
            set { SetValue(ProgressPauseColorProperty, value); }
        }
        public static readonly DependencyProperty ProgressPauseColorProperty = DependencyProperty.Register("ProgressPauseColor", typeof(Brush), typeof(ModernProgressBar), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(255, 168, 72))));

        public Brush ProgressBackgroundColor
        {
            get { return (Brush)GetValue(ProgressBackgroundColorProperty); }
            set { SetValue(ProgressBackgroundColorProperty, value); }
        }
        public static readonly DependencyProperty ProgressBackgroundColorProperty = DependencyProperty.Register("ProgressBackgroundColor", typeof(Brush), typeof(ModernProgressBar), new PropertyMetadata(Brushes.Transparent));

        public Brush ProgressBarBorderBrush
        {
            get { return (Brush)GetValue(ProgressBarBorderBrushProperty); }
            set { SetValue(ProgressBarBorderBrushProperty, value); }
        }
        public static readonly DependencyProperty ProgressBarBorderBrushProperty = DependencyProperty.Register("ProgressBarBorderBrush", typeof(Brush), typeof(ModernProgressBar), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(152, 152, 231))));

        public Thickness ProgressBarBorderThickness
        {
            get { return (Thickness)GetValue(ProgressBarBorderThicknessProperty); }
            set { SetValue(ProgressBarBorderThicknessProperty, value); }
        }
        public static readonly DependencyProperty ProgressBarBorderThicknessProperty = DependencyProperty.Register("ProgressBarBorderThickness", typeof(Thickness), typeof(ModernProgressBar), new PropertyMetadata(new Thickness(1)));
        #endregion

        public ModernProgressBar()
        {
            InitializeComponent();
        }

        private double GetPercent() => Value / (MaxValue - MinValue);
        private double GetBottomWidth() => progressBarBottom.ActualWidth;
        private double GetTargetTopWidth() => GetBottomWidth() * GetPercent();
        private void SetTopWidth()
        {
            if (!IsBusy)
                progressBarTop.Width = GetTargetTopWidth();
        }

        public void Update() => SetTopWidth();

        private void ApplyBusyAnimation()
        {
            progressBarTop.Visibility = Visibility.Collapsed;
            progressBarAnimationTop.Visibility = Visibility.Visible;
            progressBarTranslateTransform.BeginAnimation(TranslateTransform.XProperty, null);
            progressBarScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, null);
            var animation = new DoubleAnimation
            {
                From = -25,
                To = GetBottomWidth() - 25,
                EasingFunction = new CubicEase
                {
                    EasingMode = EasingMode.EaseInOut
                },
                Duration = TimeSpan.FromMilliseconds(1000),
                RepeatBehavior = RepeatBehavior.Forever
            };
            var scaleAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                EasingFunction = new CubicEase
                {
                    EasingMode = EasingMode.EaseInOut
                },
                Duration = TimeSpan.FromMilliseconds(500),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
            progressBarTranslateTransform.BeginAnimation(TranslateTransform.XProperty, animation);
            progressBarScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
        }

        private void RemoveBusyAnimation()
        {
            progressBarTop.Visibility = Visibility.Visible;
            progressBarAnimationTop.Visibility = Visibility.Collapsed;
            progressBarTranslateTransform.BeginAnimation(TranslateTransform.XProperty, null);
            progressBarScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, null);
        }

        public void SetBusy()
        {
            if (IsBusy && !IsPause && !IsError)
            {
                ApplyBusyAnimation();
            }
            else if (!IsBusy)
            {
                if (IsPause)
                    SetPause();
                else if (IsError)
                    SetError();
                else
                    RemoveBusyAnimation();
            }
        }

        public void SetPause()
        {
            if (IsPause)
            {
                RemoveBusyAnimation();
                progressBarTop.SetBinding(Border.BackgroundProperty, new Binding
                {
                    Source = ProgressPauseColor,
                    Mode = BindingMode.OneWay
                });
            }
            else if (IsError)
            {
                RemoveBusyAnimation();
                progressBarTop.SetBinding(Border.BackgroundProperty, new Binding
                {
                    Source = ProgressErrorColor,
                    Mode = BindingMode.OneWay
                });
            }
            else
                progressBarTop.SetBinding(Border.BackgroundProperty, new Binding
                {
                    Source = ProgressColor,
                    Mode = BindingMode.OneWay
                });
        }

        public void SetError()
        {
            if (IsError)
            {
                RemoveBusyAnimation();
                progressBarTop.SetBinding(Border.BackgroundProperty, new Binding
                {
                    Source = ProgressErrorColor,
                    Mode = BindingMode.OneWay
                });
            }
            else if (IsPause)
            {
                RemoveBusyAnimation();
                progressBarTop.SetBinding(Border.BackgroundProperty, new Binding
                {
                    Source = ProgressPauseColor,
                    Mode = BindingMode.OneWay
                });
            }
            else
                progressBarTop.SetBinding(Border.BackgroundProperty, new Binding
                {
                    Source = ProgressColor,
                    Mode = BindingMode.OneWay
                });
        }

        private void ModernProgressBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!IsBusy)
                SetTopWidth();
        }

        private void ModernProgressBar_Loaded(object sender, RoutedEventArgs e)
        {
            SetTopWidth();
            SetBusy();
            SetPause();
            SetError();
        }
    }
}
