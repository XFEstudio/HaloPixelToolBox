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
        Console.WriteLine("网易云歌词工具页面初始化中...");
        Current = this;
        InitializeComponent();
        ViewModel.AutoNavigationParameterService.Initialize(this);
        ViewModel.SettingService.AddComboBox(defaultHaloPixelTextLayoutComboBox, ProfileHelper.GetEnumProfileSaveFunc<HaloPixelTextLayout>(), ProfileHelper.GetEnumProfileLoadFuncForComboBox());
        ViewModel.SettingService.AddComboBox(defaultHaloPixelUIModelComboBox, ProfileHelper.GetEnumProfileSaveFunc<HaloPixelUIModel>(), ProfileHelper.GetEnumProfileLoadFuncForComboBox());
        ViewModel.SettingService.Initialize();
        ViewModel.SettingService.RegisterEvents();
        NavigationCacheMode = NavigationCacheMode.Enabled;
        Console.WriteLine("网易云歌词工具页面初始化完成");
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        Console.WriteLine("导航至网易云歌词工具页面");
        ViewModel.AutoNavigationParameterService.OnParameterChange(e.Parameter);
    }
}
