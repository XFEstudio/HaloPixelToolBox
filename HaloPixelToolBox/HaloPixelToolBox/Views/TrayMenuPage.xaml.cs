using System.Windows.Forms;
using Windows.Graphics;

namespace HaloPixelToolBox.Views;

/// <summary>
/// Õ–≈Ã≤Àµ•“≥√Ê
/// </summary>
public sealed partial class TrayMenuPage : Page
{
    public Window MenuWindow { get; set; }
    public TrayMenuPageViewModel ViewModel { get; set; }
    public static TrayMenuPage? Current { get; set; }

    public TrayMenuPage(Window window)
    {
        Current = this;
        MenuWindow = window;
        ViewModel = new(window);
        InitializeComponent();
        ViewModel.AutoNavigationParameterService.Initialize(this);
    }

    void ResizeToContent()
    {
        double width = panel.ActualWidth;
        double height = panel.ActualHeight;

        if (width <= 0 || height <= 0)
            return;

        double scale = Content.XamlRoot.RasterizationScale;

        int w = (int)Math.Ceiling(width * scale);
        int h = (int)Math.Ceiling(height * scale);

        MenuWindow.AppWindow.Resize(new SizeInt32(w, h));
        MenuWindow.AppWindow.Move(new PointInt32(Cursor.Position.X + 5, Cursor.Position.Y - h + 10));
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        DispatcherQueue.TryEnqueue(ResizeToContent);
        ViewModel.AutoNavigationParameterService.OnParameterChange(string.Empty);
    }
}
