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
                Console.WriteLine("正在搜索花再设备...");
                while (!DeviceReady)
                {
                    DeviceReady = Device.Initialize();
                    await Task.Delay(500);
                }
                Console.WriteLine("花再设备已连接");
            }),
            Task.Run(async () =>
            {
                Console.WriteLine("正在搜索云音乐...");
                while (!CloudMusicReady)
                {
                    CloudMusicReady = Reader.Initialize();
                    await Task.Delay(500);
                }
                Console.WriteLine("云音乐已准备就绪");
            }),
            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        if (DeviceReady && CloudMusicReady && EnableCloudMusicLyrics)
                        {
                            Console.WriteLine("[DEBUG]设备均在线，准备进入主循环");
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
                                        Console.WriteLine($"已读取到歌词：{lyrics}");
                                    }
                                    await Task.Delay(50);
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