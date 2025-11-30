using HaloPixelToolBox.Core.Models;
using Microsoft.UI.Xaml.Navigation;
using XFEExtension.NetCore.WinUIHelper.Utilities.Helper;

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
        ViewModel.SettingService.AddComboBox(defaultHaloPixelTextLayoutComboBox, ProfileHelper.GetEnumProfileSaveFunc<HaloPixelTextLayout>(), ProfileHelper.GetEnumProfileLoadFuncForComboBox());
        ViewModel.SettingService.AddComboBox(defaultHaloPixelUIModelComboBox, ProfileHelper.GetEnumProfileSaveFunc<HaloPixelUIModel>(), ProfileHelper.GetEnumProfileLoadFuncForComboBox());
        ViewModel.SettingService.Initialize();
        ViewModel.SettingService.RegisterEvents();
        NavigationCacheMode = NavigationCacheMode.Enabled;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        ViewModel.AutoNavigationParameterService.OnParameterChange(e.Parameter);
    }
}
