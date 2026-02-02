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
    private bool useInputedAddress = CloudMusicLyricsProfile.UseInputedAddress;
    [ObservableProperty]
    private string inputedAddress = CloudMusicLyricsProfile.InputedAddress;
    [ObservableProperty]
    private int switchBackTimeout = CloudMusicLyricsProfile.SwitchBackTimeout;
    [ObservableProperty]
    private string cloudMusicVersion = string.Empty;
    [ObservableProperty]
    private string supportedVersion = CloudMusicLyricsReader.VersionResolverDictionary.Keys.FirstOrDefault() ?? string.Empty;
    public HaloPixelDevice Device { get; set; } = new();
    public CloudMusicLyricsReader Reader { get; set; }

    public ISettingService SettingService { get; } = ServiceManager.GetService<ISettingService>();

    /// <summary>
    /// Safely parse a hexadecimal address string to nint, returning 0 if parsing fails
    /// </summary>
    private static nint ParseHexAddress(string hexAddress)
    {
        if (string.IsNullOrWhiteSpace(hexAddress))
            return 0;
        
        try
        {
            return new nint(Convert.ToInt64(hexAddress, 16));
        }
        catch (Exception ex) when (ex is FormatException or OverflowException or ArgumentException)
        {
            Console.WriteLine($"[WARNING]无法解析内存地址 '{hexAddress}'，使用默认值 0。错误：{ex.Message}");
            return 0;
        }
    }

    partial void OnEnableCloudMusicLyricsChanged(bool value) => CloudMusicLyricsProfile.EnableCloudMusicLyrics = value;

    partial void OnUseInputedAddressChanged(bool value)
    {
        CloudMusicLyricsProfile.UseInputedAddress = value;
        Reader.UseInputedAddress = value;
    }

    partial void OnInputedAddressChanged(string value)
    {
        CloudMusicLyricsProfile.InputedAddress = value;
        Reader.Address = ParseHexAddress(value);
    }

    partial void OnSwitchBackWhenPauseChanged(bool value) => CloudMusicLyricsProfile.SwitchBackWhenPause = value;

    partial void OnSwitchBackTimeoutChanged(int value) => CloudMusicLyricsProfile.SwitchBackTimeout = value;

    public CloudMusicLyricsToolPageViewModel()
    {
        Console.WriteLine("初始化网易云歌词读取器");
        Reader = new CloudMusicLyricsReader
        {
            UseInputedAddress = UseInputedAddress,
            Address = ParseHexAddress(InputedAddress)
        };
        Console.WriteLine("准备启动网易云后台线程");
        Task.Run(async () =>
        {
            try
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR]搜索花再设备时发生错误：{ex.Message}");
                Console.WriteLine($"[TRACE]{ex.StackTrace}");
            }

        });
        Task.Run(async () =>
        {
            try
            {
                Console.WriteLine("正在搜索云音乐...");
                while (!CloudMusicReady)
                {
                    var ready = Reader.Initialize();
                    AutoNavigationParameterService.CurrentPage?.DispatcherQueue.TryEnqueue(() =>
                    {
                        CloudMusicReady = ready;
                        CloudMusicVersion = Reader.VersionInfo is not null ? $"{Reader.VersionInfo.FileVersion}" : "未检测到云音乐";
                    });
                    await Task.Delay(500);
                }
                Console.WriteLine("云音乐已准备就绪");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR]搜索云音乐时发生错误：{ex.Message}");
                Console.WriteLine($"[TRACE]{ex.StackTrace}");
            }
        });
        Task.Run(async () =>
        {
            Console.WriteLine("启动云音乐地址重解析线程");
            while (true)
            {
                try
                {
                    Reader.ReresolveAddress();
                    await Task.Delay(500);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR]云音乐地址重解析线程发生错误：{ex.Message}");
                    Console.WriteLine($"[TRACE]{ex.StackTrace}");
                }
            }
        });
        Task.Run(async () =>
        {
            Console.WriteLine("启动网易云歌词主线程");
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
                                    Console.WriteLine($"已读取到歌词：{lyrics}");
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
                            catch (Exception ex)
                            {
                                Console.WriteLine($"[ERROR]网易云歌词主循环发生错误：{ex.Message}");
                                Console.WriteLine($"[TRACE]{ex.StackTrace}");
                            }
                        }
                        Console.WriteLine($"[DEBUG]主循环已退出");
                    }
                    Console.WriteLine($"[DEBUG]状态\t音响：{DeviceReady} 软件：{CloudMusicReady} 启用状态：{EnableCloudMusicLyrics}");
                    await Task.Delay(500);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR]网易云歌词主线程发生错误：{ex.Message}");
                    Console.WriteLine($"[TRACE]{ex.StackTrace}");
                }
            }
        });
        Console.WriteLine("网易云后台线程启动完成");
    }
}