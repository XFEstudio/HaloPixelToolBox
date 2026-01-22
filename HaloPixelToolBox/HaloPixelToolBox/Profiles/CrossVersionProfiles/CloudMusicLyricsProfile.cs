using HaloPixelToolBox.Core.Models;
using XFEExtension.NetCore.AutoConfig;
using XFEExtension.NetCore.WinUIHelper.Utilities.Helper;

namespace HaloPixelToolBox.Profiles.CrossVersionProfiles;

public partial class CloudMusicLyricsProfile : XFEProfile
{
    public CloudMusicLyricsProfile() => ProfilePath = $@"{AppPathHelper.LocalProfile}\{nameof(CloudMusicLyricsProfile)}";
    /// <summary>
    /// 切换回默认显示内容的时间
    /// </summary>
    [ProfileProperty]
    private int switchBackTimeout = 60;
    /// <summary>
    /// 是否启用网易云音乐歌词
    /// </summary>
    [ProfileProperty]
    private bool enableCloudMusicLyrics = false;
    /// <summary>
    /// 当暂停时切换回默认显示内容
    /// </summary>
    [ProfileProperty]
    private bool switchBackWhenPause = true;
    /// <summary>
    /// 使用输入的内存地址
    /// </summary>
    [ProfileProperty]
    private bool useInputedAddress = false;
    /// <summary>
    /// 输入的内存地址
    /// </summary>
    [ProfileProperty]
    private string inputedAddress = string.Empty;
    /// <summary>
    /// 默认歌词显示布局
    /// </summary>
    [ProfileProperty]
    private HaloPixelTextLayout defaultHaloPixelTextLayout = HaloPixelTextLayout.Center;
    /// <summary>
    /// 默认暂停界面
    /// </summary>
    [ProfileProperty]
    private HaloPixelUIModel defaultHaloPixelUIModel = HaloPixelUIModel.Clock;
}
