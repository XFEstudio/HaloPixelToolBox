using HaloPixelToolBox.Installer.Views.Behavior;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace HaloPixelToolBox.Installer.Views.Controls
{
    public class SmoothScrollViewer : ScrollViewer
    {
        public long AnimateMilliseconds { get; set; } = 300;
        public double ScrollDistanceMultiplier { get; set; } = 1;
        private double lastLocation = 0;
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            double wheelChange = e.Delta;
            double newOffset = lastLocation - wheelChange * ScrollDistanceMultiplier;
            ScrollToVerticalOffset(lastLocation);
            if (newOffset < 0)
                newOffset = 0;
            if (newOffset > ScrollableHeight)
                newOffset = ScrollableHeight;
            AnimateScroll(newOffset);
            lastLocation = newOffset;
            e.Handled = true;
        }
        private void AnimateScroll(double toValue)
        {
            BeginAnimation(ScrollViewerBehavior.VerticalOffsetProperty, null);
            var animation = new DoubleAnimation
            {
                EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut },
                From = VerticalOffset,
                To = toValue,
                Duration = TimeSpan.FromMilliseconds(AnimateMilliseconds)
            };
            BeginAnimation(ScrollViewerBehavior.VerticalOffsetProperty, animation);
        }
    }
}
