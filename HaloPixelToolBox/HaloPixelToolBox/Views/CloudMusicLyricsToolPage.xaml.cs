using Microsoft.UI.Xaml.Navigation;

namespace HaloPixelToolBox.Views;

/// <summary>
/// 网易云歌词工具页面
/// </summary>
public sealed partial class CloudMusicLyricsToolPage : Page
{
    public static CloudMusicLyricsToolPage? Current { get; set; }
    public CloudMusicLyricsToolPageViewModel ViewModel { get; set; } = new();
    public CloudMusicLyricsToolPage()
    {
        Current = this;
        InitializeComponent();
        ViewModel.AutoNavigationParameterService.Initialize(this);
        NavigationCacheMode = NavigationCacheMode.Enabled;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        ViewModel.AutoNavigationParameterService.OnParameterChange(e.Parameter);
    }
}
