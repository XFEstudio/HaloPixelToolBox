using CommunityToolkit.Mvvm.ComponentModel;
using HaloPixelToolBox.Core.Utilities;
using HaloPixelToolBox.Profiles.CrossVersionProfiles;
using XFEExtension.NetCore.WinUIHelper.Implements;

namespace HaloPixelToolBox.ViewModels;

public partial class CloudMusicLyricsToolPageViewModel : ServiceBaseViewModelBase<string>
{
    [ObservableProperty]
    private bool deviceReady;
    [ObservableProperty]
    private bool cloudMusicReady;
    [ObservableProperty]
    private bool enableCloudMusicLyrics = SystemProfile.EnableCloudMusicLyrics;
    public HaloPixelDevice Device { get; set; } = new();
    public CloudMusicLyricsReader Reader { get; set; } = new();

    partial void OnCloudMusicReadyChanged(bool value) => SystemProfile.EnableCloudMusicLyrics = value;

    public CloudMusicLyricsToolPageViewModel()
    {
        AutoNavigationParameterService.ParameterChange += AutoNavigationParameterService_ParameterChange;
    }

    private void AutoNavigationParameterService_ParameterChange(object? sender, string? e)
    {
        Task.Run(() =>
        {
            while (!DeviceReady)
            {
                AutoNavigationParameterService.CurrentPage?.DispatcherQueue.TryEnqueue(() =>
                {
                    DeviceReady = Device.Initialize();
                });
            }
        });
        Task.Run(() =>
        {
            while (!CloudMusicReady)
            {
                AutoNavigationParameterService.CurrentPage?.DispatcherQueue.TryEnqueue(() =>
                {
                    CloudMusicReady = Reader.Initialize();
                });
            }
        });
    }
}