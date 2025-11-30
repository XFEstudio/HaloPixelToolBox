using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HaloPixelToolBox.Installer.Views.Controls
{
    public partial class PasswordHintTextBox : UserControl
    {
        private bool passwordVisibleChanging = false;
        #region DependencyProperty
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(PasswordHintTextBox), new PropertyMetadata(string.Empty));

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set
            {
                SetValue(PasswordProperty, value);
                passwordVisibleChanging = true;
                if (PasswordVisible)
                {
                    Text = Password;
                }
                else
                {
                    Text = GetMaskText(Password.Length);
                }
                PasswordChange?.Invoke(this, new(Password, default!));
                passwordVisibleChanging = false;
            }
        }
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register("Password", typeof(string), typeof(PasswordHintTextBox), new PropertyMetadata(string.Empty));

        public string HintText
        {
            get { return (string)GetValue(HintTextProperty); }
            set { SetValue(HintTextProperty, value); }
        }
        public static readonly DependencyProperty HintTextProperty = DependencyProperty.Register("HintText", typeof(string), typeof(PasswordHintTextBox), new PropertyMetadata("请输入密码"));

        public string SelectedText
        {
            get { return (string)GetValue(SelectedTextProperty); }
            set { SetValue(SelectedTextProperty, value); }
        }
        public static readonly DependencyProperty SelectedTextProperty = DependencyProperty.Register("SelectedText", typeof(string), typeof(PasswordHintTextBox), new PropertyMetadata(string.Empty));

        public string PasswordMask
        {
            get { return (string)GetValue(PasswordMaskProperty); }
            set { SetValue(PasswordMaskProperty, value); }
        }
        public static readonly DependencyProperty PasswordMaskProperty = DependencyProperty.Register("PasswordMask", typeof(string), typeof(PasswordHintTextBox), new PropertyMetadata("●"));

        public bool PasswordVisible
        {
            get { return (bool)GetValue(PasswordVisibleProperty); }
            set
            {
                SetValue(PasswordVisibleProperty, value);

            }
        }
        public static readonly DependencyProperty PasswordVisibleProperty = DependencyProperty.Register("PasswordVisible", typeof(bool), typeof(PasswordHintTextBox), new PropertyMetadata(false));

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }
        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(PasswordHintTextBox), new PropertyMetadata(false));

        public bool IsReadOnlyCaretVisible
        {
            get { return (bool)GetValue(IsReadOnlyCaretVisibleProperty); }
            set { SetValue(IsReadOnlyCaretVisibleProperty, value); }
        }
        public static readonly DependencyProperty IsReadOnlyCaretVisibleProperty = DependencyProperty.Register("IsReadOnlyCaretVisible", typeof(bool), typeof(PasswordHintTextBox), new PropertyMetadata(false));

        public bool IsInactiveSelectionHighlightEnabled
        {
            get { return (bool)GetValue(IsInactiveSelectionHighlightEnabledProperty); }
            set { SetValue(IsInactiveSelectionHighlightEnabledProperty, value); }
        }
        public static readonly DependencyProperty IsInactiveSelectionHighlightEnabledProperty = DependencyProperty.Register("IsInactiveSelectionHighlightEnabled", typeof(bool), typeof(PasswordHintTextBox), new PropertyMetadata(false));

        public bool AcceptsReturn
        {
            get { return (bool)GetValue(AcceptsReturnProperty); }
            set { SetValue(AcceptsReturnProperty, value); }
        }
        public static readonly DependencyProperty AcceptsReturnProperty = DependencyProperty.Register("AcceptsReturn", typeof(bool), typeof(PasswordHintTextBox), new PropertyMetadata(false));

        public bool AcceptsTab
        {
            get { return (bool)GetValue(AcceptsTabProperty); }
            set { SetValue(AcceptsTabProperty, value); }
        }
        public static readonly DependencyProperty AcceptsTabProperty = DependencyProperty.Register("AcceptsTab", typeof(bool), typeof(PasswordHintTextBox), new PropertyMetadata(false));

        public bool IsUndoEnabled
        {
            get { return (bool)GetValue(IsUndoEnabledProperty); }
            set { SetValue(IsUndoEnabledProperty, value); }
        }
        public static readonly DependencyProperty IsUndoEnabledProperty = DependencyProperty.Register("IsUndoEnabled", typeof(bool), typeof(PasswordHintTextBox), new PropertyMetadata(false));

        public double HintFontSize
        {
            get { return (double)GetValue(HintFontSizeProperty); }
            set { SetValue(HintFontSizeProperty, value); }
        }
        public static readonly DependencyProperty HintFontSizeProperty = DependencyProperty.Register("HintFontSize", typeof(double), typeof(PasswordHintTextBox), new PropertyMetadata(13d));

        public double HintTextOpacity
        {
            get { return (double)GetValue(HintTextOpacityProperty); }
            set { SetValue(HintTextOpacityProperty, value); }
        }
        public static readonly DependencyProperty HintTextOpacityProperty = DependencyProperty.Register("HintTextOpacity", typeof(double), typeof(PasswordHintTextBox), new PropertyMetadata(0.5d));

        public int UndoLimit
        {
            get { return (int)GetValue(UndoLimitProperty); }
            set { SetValue(UndoLimitProperty, value); }
        }
        public static readonly DependencyProperty UndoLimitProperty = DependencyProperty.Register("UndoLimit", typeof(int), typeof(PasswordHintTextBox), new PropertyMetadata(-1));

        public int CaretIndex
        {
            get { return (int)GetValue(CaretIndexProperty); }
            set { SetValue(CaretIndexProperty, value); }
        }
        public static readonly DependencyProperty CaretIndexProperty = DependencyProperty.Register("CaretIndex", typeof(int), typeof(PasswordHintTextBox), new PropertyMetadata(-1));

        public int SelectionStart
        {
            get { return (int)GetValue(SelectionStartProperty); }
            set { SetValue(SelectionStartProperty, value); }
        }
        public static readonly DependencyProperty SelectionStartProperty = DependencyProperty.Register("SelectionStart", typeof(int), typeof(PasswordHintTextBox), new PropertyMetadata(-1));

        public int SelectionLength
        {
            get { return (int)GetValue(SelectionLengthProperty); }
            set { SetValue(SelectionLengthProperty, value); }
        }
        public static readonly DependencyProperty SelectionLengthProperty = DependencyProperty.Register("SelectionLength", typeof(int), typeof(PasswordHintTextBox), new PropertyMetadata(-1));

        public FontFamily HintFontFamily
        {
            get { return (FontFamily)GetValue(HintFontFamilyProperty); }
            set { SetValue(HintFontFamilyProperty, value); }
        }
        public static readonly DependencyProperty HintFontFamilyProperty = DependencyProperty.Register("HintFontFamily", typeof(FontFamily), typeof(PasswordHintTextBox), new PropertyMetadata(new FontFamily()));

        public Brush HintForeground
        {
            get { return (Brush)GetValue(HintForegroundProperty); }
            set { SetValue(HintForegroundProperty, value); }
        }
        public static readonly DependencyProperty HintForegroundProperty = DependencyProperty.Register("HintForeground", typeof(Brush), typeof(PasswordHintTextBox), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public Brush HintBackground
        {
            get { return (Brush)GetValue(HintBackgroundProperty); }
            set { SetValue(HintBackgroundProperty, value); }
        }
        public static readonly DependencyProperty HintBackgroundProperty = DependencyProperty.Register("HintBackground", typeof(Brush), typeof(PasswordHintTextBox), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush EditorBorderBrush
        {
            get { return (Brush)GetValue(EditorBorderBrushProperty); }
            set { SetValue(EditorBorderBrushProperty, value); }
        }
        public static readonly DependencyProperty EditorBorderBrushProperty = DependencyProperty.Register("EditorBorderBrush", typeof(Brush), typeof(PasswordHintTextBox), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(152, 152, 231))));

        public Brush CaretBrush
        {
            get { return (Brush)GetValue(CaretBrushProperty); }
            set { SetValue(CaretBrushProperty, value); }
        }
        public static readonly DependencyProperty CaretBrushProperty = DependencyProperty.Register("CaretBrush", typeof(Brush), typeof(PasswordHintTextBox), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(152, 152, 231))));

        public Brush SelectionBrush
        {
            get { return (Brush)GetValue(SelectionBrushProperty); }
            set { SetValue(SelectionBrushProperty, value); }
        }
        public static readonly DependencyProperty SelectionBrushProperty = DependencyProperty.Register("SelectionBrush", typeof(Brush), typeof(PasswordHintTextBox), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(152, 152, 231))));

        public Brush SelectionTextBrush
        {
            get { return (Brush)GetValue(SelectionTextBrushProperty); }
            set { SetValue(SelectionTextBrushProperty, value); }
        }
        public static readonly DependencyProperty SelectionTextBrushProperty = DependencyProperty.Register("SelectionTextBrush", typeof(Brush), typeof(PasswordHintTextBox), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(152, 152, 231))));

        public TextDecorationCollection TextDecorations
        {
            get { return (TextDecorationCollection)GetValue(TextDecorationsProperty); }
            set { SetValue(TextDecorationsProperty, value); }
        }
        public static readonly DependencyProperty TextDecorationsProperty = DependencyProperty.Register("TextDecorations", typeof(TextDecorationCollection), typeof(PasswordHintTextBox), new PropertyMetadata(null));

        public FontWeight HintFontWeight
        {
            get { return (FontWeight)GetValue(HintFontWeightProperty); }
            set { SetValue(HintFontWeightProperty, value); }
        }
        public static readonly DependencyProperty HintFontWeightProperty = DependencyProperty.Register("HintFontWeight", typeof(FontWeight), typeof(PasswordHintTextBox), new PropertyMetadata(new FontWeight()));

        public Thickness HintTextMargin
        {
            get { return (Thickness)GetValue(HintTextMarginProperty); }
            set { SetValue(HintTextMarginProperty, value); }
        }
        public static readonly DependencyProperty HintTextMarginProperty = DependencyProperty.Register("HintTextMargin", typeof(Thickness), typeof(PasswordHintTextBox), new PropertyMetadata(new Thickness(10, 0, 0, 0)));

        public CornerRadius EditorCornerRadius
        {
            get { return (CornerRadius)GetValue(EditorCornerRadiusProperty); }
            set { SetValue(EditorCornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty EditorCornerRadiusProperty = DependencyProperty.Register("EditorCornerRadius", typeof(CornerRadius), typeof(PasswordHintTextBox), new PropertyMetadata(new CornerRadius(10)));

        public Thickness EditorBorderThickness
        {
            get { return (Thickness)GetValue(EditorBorderThicknessProperty); }
            set { SetValue(EditorBorderThicknessProperty, value); }
        }
        public static readonly DependencyProperty EditorBorderThicknessProperty = DependencyProperty.Register("EditorBorderThickness", typeof(Thickness), typeof(PasswordHintTextBox), new PropertyMetadata(new Thickness(1)));

        public VerticalAlignment HintTextVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(HintTextVerticalAlignmentProperty); }
            set { SetValue(HintTextVerticalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty HintTextVerticalAlignmentProperty = DependencyProperty.Register("HintTextVerticalAlignment", typeof(VerticalAlignment), typeof(PasswordHintTextBox), new PropertyMetadata(VerticalAlignment.Center));

        public HorizontalAlignment HintTextHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(HintTextHorizontalAlignmentProperty); }
            set { SetValue(HintTextHorizontalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty HintTextHorizontalAlignmentProperty = DependencyProperty.Register("HintTextHorizontalAlignment", typeof(HorizontalAlignment), typeof(PasswordHintTextBox), new PropertyMetadata(HorizontalAlignment.Left));

        public ScrollBarVisibility HorizontalScrollBarVisibility
        {
            get { return (ScrollBarVisibility)GetValue(HorizontalScrollBarVisibilityProperty); }
            set { SetValue(HorizontalScrollBarVisibilityProperty, value); }
        }
        public static readonly DependencyProperty HorizontalScrollBarVisibilityProperty = DependencyProperty.Register("HorizontalScrollBarVisibility", typeof(ScrollBarVisibility), typeof(PasswordHintTextBox), new PropertyMetadata(ScrollBarVisibility.Hidden));

        public ScrollBarVisibility VerticalScrollBarVisibility
        {
            get { return (ScrollBarVisibility)GetValue(VerticalScrollBarVisibilityProperty); }
            set { SetValue(VerticalScrollBarVisibilityProperty, value); }
        }
        public static readonly DependencyProperty VerticalScrollBarVisibilityProperty = DependencyProperty.Register("VerticalScrollBarVisibility", typeof(ScrollBarVisibility), typeof(PasswordHintTextBox), new PropertyMetadata(ScrollBarVisibility.Hidden));

        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }
        public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register("TextAlignment", typeof(TextAlignment), typeof(PasswordHintTextBox), new PropertyMetadata(TextAlignment.Left));

        public TextWrapping TextWrapping
        {
            get { return (TextWrapping)GetValue(TextWrappingProperty); }
            set { SetValue(TextWrappingProperty, value); }
        }
        public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register("TextWrapping", typeof(TextWrapping), typeof(PasswordHintTextBox), new PropertyMetadata(TextWrapping.NoWrap));
        #endregion

        public event EventHandler<PasswordChangeEventArgs?>? PasswordChange;

        public PasswordHintTextBox()
        {
            InitializeComponent();
            hintTextBox.TextChanged += HintTextBox_TextChanged;
            visibleButton.Click += VisibleButton_Click;
        }

        private string GetMaskText(int length)
        {
            string maskText = string.Empty;
            for (int i = 0; i < length; i++)
                maskText += PasswordMask;
            return maskText;
        }

        private void VisibleButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordVisible = !PasswordVisible;
            passwordVisibleChanging = true;
            if (PasswordVisible)
            {
                visibilityImage.Source = new BitmapImage(new("pack://application:,,,/Resources/Image/visible.png"));
                Text = Password;
            }
            else
            {
                visibilityImage.Source = new BitmapImage(new("pack://application:,,,/Resources/Image/invisible.png"));
                Text = GetMaskText(Text.Length);
            }
            passwordVisibleChanging = false;
        }

        private void HintTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!passwordVisibleChanging && e.OriginalSource is TextBox textBox && textBox.Name == "mainTextBox")
            {
                foreach (var change in e.Changes)
                {
                    SetValue(PasswordProperty, Password.Remove(change.Offset, change.RemovedLength));
                    SetValue(PasswordProperty, Password.Insert(change.Offset, Text.Substring(change.Offset, change.AddedLength)));
                    if (!PasswordVisible && change.AddedLength > 0)
                    {
                        passwordVisibleChanging = true;
                        Text = Text.Replace(Text.Substring(change.Offset, change.AddedLength), GetMaskText(change.AddedLength));
                        textBox.CaretIndex = change.Offset + change.AddedLength;
                        passwordVisibleChanging = false;
                    }
                }
                PasswordChange?.Invoke(this, new(Password, e));
            }
        }
    }

    public record PasswordChangeEventArgs(string Password, TextChangedEventArgs TextChangedEventArgs) { }
}