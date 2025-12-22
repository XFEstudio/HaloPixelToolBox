using CommunityToolkit.Mvvm.ComponentModel;
using HaloPixelToolBox.Core.Utilities;
using HaloPixelToolBox.Profiles.CrossVersionProfiles;
using System.Diagnostics;
using XFEExtension.NetCore.StringExtension;
using XFEExtension.NetCore.WinUIHelper.Implements;
using XFEExtension.NetCore.WinUIHelper.Interface.Services;
using XFEExtension.NetCore.WinUIHelper.Utilities;

namespace HaloPixelToolBox.ViewModels;

public partial class CloudMusicLyricsToolPageViewModel : ServiceBaseViewModelBase<string>
{
    [ObservableProperty]
    private bool deviceReady;
    [ObservableProperty]
    private bool cloudMusicReady;
    [ObservableProperty]
    private bool enableCloudMusicLyrics = CloudMusicLyricsProfile.EnableCloudMusicLyrics;
    [ObservableProperty]
    private bool switchBackWhenPause = CloudMusicLyricsProfile.SwitchBackWhenPause;
    [ObservableProperty]
    private int switchBackTimeout = CloudMusicLyricsProfile.SwitchBackTimeout;
    [ObservableProperty]
    private string cloudMusicVersion = string.Empty;
    public HaloPixelDevice Device { get; set; } = new();
    public CloudMusicLyricsReader Reader { get; set; } = new();

    public ISettingService SettingService { get; } = ServiceManager.GetService<ISettingService>();

    partial void OnEnableCloudMusicLyricsChanged(bool value) => CloudMusicLyricsProfile.EnableCloudMusicLyrics = value;

    partial void OnSwitchBackWhenPauseChanged(bool value) => CloudMusicLyricsProfile.SwitchBackWhenPause = value;

    partial void OnSwitchBackTimeoutChanged(int value) => CloudMusicLyricsProfile.SwitchBackTimeout = value;

    public CloudMusicLyricsToolPageViewModel()
    {
        _ = Task.WhenAll(new List<Task>
        {
            Task.Run(async () =>
            {
                Console.WriteLine("正在搜索花再设备...");
                while (!DeviceReady)
                {
                    var ready = Device.Initialize();
                    AutoNavigationParameterService.CurrentPage?.DispatcherQueue.TryEnqueue(() =>
                    {
                        DeviceReady = ready;
                    });
                    await Task.Delay(500);
                }
                Console.WriteLine("花再设备已连接");
            }),
            Task.Run(async () =>
            {
                Console.WriteLine("正在搜索云音乐...");
                while (!CloudMusicReady)
                {
                    var ready = Reader.Initialize();
                    AutoNavigationParameterService.CurrentPage?.DispatcherQueue.TryEnqueue(() =>
                    {
                        CloudMusicReady = ready;
                        CloudMusicVersion = ready && Reader.VersionInfo != null ? $"{Reader.VersionInfo}" : "未检测到云音乐";
                    });
                    await Task.Delay(500);
                }
                Console.WriteLine("云音乐已准备就绪");
            }),
            Task.Run(async () =>
            {
                while (true)
                {
                    Reader.ReresolveAddress();
                    await Task.Delay(500);
                }
            }),
            Task.Run(async () =>
            {
                while (true)
                {
                    bool isClockUI = false;
                    int time = 0;
                    try
                    {
                        if (DeviceReady && CloudMusicReady && EnableCloudMusicLyrics)
                        {
                            Console.WriteLine("[DEBUG]设备均在线，准备进入主循环");
                            string lastRead = string.Empty;
                            bool scrolled = false;
                            while (true)
                            {
                                try
                                {
                                    if (!DeviceReady || !CloudMusicReady || !EnableCloudMusicLyrics)
                                        break;
                                    if (Reader.TryReadLyrics(out var lyrics) && lastRead != lyrics)
                                    {
                                        lastRead = lyrics;
                                        isClockUI = false;
                                        time = 0;
                                        if (scrolled)
                                        {
                                            Device.ShowText(string.Empty);
                                            await Task.Delay(100);
                                            scrolled = false;
                                        }
                                        Device.SetTextLayout(CloudMusicLyricsProfile.DefaultHaloPixelTextLayout);
                                        Device.ShowText(lyrics);
                                        Debug.WriteLine(lyrics.DisplayLength());
                                        if (lyrics.DisplayLength() > 30)
                                        {
                                            scrolled = true;
                                            await Task.Delay(500);
                                            Device.SetTextLayout(Core.Models.HaloPixelTextLayout.ScrollRightToLeft);
                                        }
                                        Console.WriteLine($"已读取到歌词：{lyrics}");
                                    }
                                    await Task.Delay(50);
                                    time += 50;
                                    if (!isClockUI && time >= CloudMusicLyricsProfile.SwitchBackTimeout * 1000)
                                    {
                                        isClockUI = true;
                                        Device.SetUIModel(CloudMusicLyricsProfile.DefaultHaloPixelUIModel);
                                        Console.WriteLine("已切换至时钟界面");
                                    }
                                }
                                catch(Exception ex)
                                {
                                    Console.WriteLine($"[ERROR]{ex.Message}");
                                    Console.WriteLine($"[TRACE]{ex.StackTrace}");
                                }
                            }
                            Console.WriteLine($"[DEBUG]主循环已退出");
                        }
                        Console.WriteLine(DeviceReady && CloudMusicReady && EnableCloudMusicLyrics);
                        await Task.Delay(500);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR]{ex.Message}");
                        Console.WriteLine($"[TRACE]{ex.StackTrace}");
                    }
                }
            })
        });
    }
}