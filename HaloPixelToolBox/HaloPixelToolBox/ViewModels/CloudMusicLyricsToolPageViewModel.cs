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

    partial void OnEnableCloudMusicLyricsChanged(bool value) => SystemProfile.EnableCloudMusicLyrics = value;

    public CloudMusicLyricsToolPageViewModel()
    {
        _ = Task.WhenAll(new List<Task>
        {
            Task.Run(async () =>
            {
                while (!DeviceReady)
                {
                    AutoNavigationParameterService.CurrentPage?.DispatcherQueue.TryEnqueue(() =>
                    {
                        DeviceReady = Device.Initialize();
                    });
                    await Task.Delay(500);
                }
            }),
            Task.Run(async () =>
            {
                while (!CloudMusicReady)
                {
                    AutoNavigationParameterService.CurrentPage?.DispatcherQueue.TryEnqueue(() =>
                    {
                        CloudMusicReady = Reader.Initialize();
                    });
                    await Task.Delay(500);
                }
            }),
            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        if (DeviceReady && CloudMusicReady && EnableCloudMusicLyrics)
                        {
                            string lastRead = string.Empty;
                            while (true)
                            {
                                try
                                {
                                    if (!DeviceReady || !CloudMusicReady || !EnableCloudMusicLyrics)
                                        break;
                                    if (Reader.TryReadLyrics(out var lyrics) && lastRead != lyrics)
                                    {
                                        lastRead = lyrics;
                                        Device.ShowText(lyrics);
                                    }
                                    Console.WriteLine(lyrics);
                                    await Task.Delay(50);
                                }
                                catch(Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                            }
                        }
                        Console.WriteLine(DeviceReady && CloudMusicReady && EnableCloudMusicLyrics);
                        await Task.Delay(500);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            })
        });
    }
}