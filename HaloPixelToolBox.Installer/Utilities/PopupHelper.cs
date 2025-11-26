using HaloPixelToolBox.Installer.Model;
using HaloPixelToolBox.Installer.Views.Pages.Popups;
using HaloPixelToolBox.Installer.Views.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace HaloPixelToolBox.Installer.Utilities
{
    public static class PopupHelper
    {
        private static NormalDialogPopupPage CreateNormalDialogPage(object content)
        {
            var dialogPage = new NormalDialogPopupPage();
            dialogPage.ViewModel.Content = content;
            return dialogPage;
        }

        private static ScrollViewer CreateTextContent(string text, Color textColor) => new()
        {
            Content = new TextBlock
            {
                Text = text,
                Foreground = new SolidColorBrush(textColor),
                Margin = new Thickness(20, 20, 20, 0),
                TextWrapping = TextWrapping.WrapWithOverflow
            },
            HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
            Resources = new ResourceDictionary
            {
                {
                    typeof(ScrollBar),
                    new Style
                    {
                        TargetType = typeof(ScrollBar),
                        BasedOn = (Style)Application.Current.FindResource("ConsoleScrollBar")
                    }
                }
            }
        };

        public static MessageBoxResult? ShowConfirmDialog(object content, bool showCancelButton = false, string confirmText = "确定", string cancelText = "取消")
        {
            var dialog = CreateNormalDialogPage(content);
            dialog.ViewModel.ConfirmText = confirmText;
            dialog.ViewModel.CancelText = cancelText;
            dialog.ViewModel.ConfirmGridLength = new GridLength(1, GridUnitType.Star);
            if (showCancelButton)
                dialog.ViewModel.CancelGridLength = new GridLength(1, GridUnitType.Star);
            return ShowDialog(dialog);
        }

        public static MessageBoxResult? ShowConfirmDialog(string text, Color textColor, bool showCancelButton = false, string confirmText = "确定", string cancelText = "取消") => ShowConfirmDialog(CreateTextContent(text, textColor), showCancelButton, confirmText, cancelText);

        public static MessageBoxResult? ShowConfirmDialog(string text, bool showCancelButton = false, string confirmText = "确定", string cancelText = "取消") => ShowConfirmDialog(text, Colors.Black, showCancelButton, confirmText, cancelText);

        public static MessageBoxResult? ShowYesOrNoDialog(object content, bool showCancelButton = false, string yesText = "是", string noText = "否")
        {
            var dialog = CreateNormalDialogPage(content);
            dialog.ViewModel.YesText = yesText;
            dialog.ViewModel.NoText = noText;
            dialog.ViewModel.YesGridLength = new GridLength(1, GridUnitType.Star);
            dialog.ViewModel.NoGridLength = new GridLength(1, GridUnitType.Star);
            if (showCancelButton)
                dialog.ViewModel.CancelGridLength = new GridLength(1, GridUnitType.Star);
            return ShowDialog(dialog);
        }

        public static MessageBoxResult? ShowYesOrNoDialog(string text, Color textColor, bool showCancelButton = false, string yesText = "是", string noText = "否") => ShowYesOrNoDialog(CreateTextContent(text, textColor), showCancelButton, yesText, noText);

        public static MessageBoxResult? ShowYesOrNoDialog(string text, bool showCancelButton = false, string yesText = "是", string noText = "否") => ShowYesOrNoDialog(text, Colors.Black, showCancelButton, yesText, noText);

        public static MessageBoxResult? ShowDialog(object content, double width = 320, double height = 230)
        {
            var popupWindow = new PopupWindow
            {
                Width = width,
                Height = height
            };
            popupWindow.ViewModel.Content = content;
            if (content is IPopupPage popupPage)
                popupPage.PopupWindow = popupWindow;
            popupWindow.ShowDialog();
            return popupWindow.Result;
        }
    }
}