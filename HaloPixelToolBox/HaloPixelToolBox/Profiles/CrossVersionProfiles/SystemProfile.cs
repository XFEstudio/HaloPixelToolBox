using XFEExtension.NetCore.AutoConfig;
using XFEExtension.NetCore.WinUIHelper.Utilities.Helper;

namespace HaloPixelToolBox.Profiles.CrossVersionProfiles;

public partial class SystemProfile : XFEProfile
{
    public SystemProfile() => ProfilePath = $@"{AppPathHelper.LocalProfile}\{nameof(SystemProfile)}";

    /// <summary>
    /// 主题颜色
    /// </summary>
    [ProfileProperty]
    private ElementTheme theme = ElementTheme.Default;
    /// <summary>
    /// 是否自动启动
    /// </summary>
    [ProfileProperty]
    private bool autoStart = false;
    /// <summary>
    /// 启动时最小化到托盘
    /// </summary>
    [ProfileProperty]
    private bool minimizeWhenOpen = false;
    /// <summary>
    /// 关闭时最小化到托盘
    /// </summary>
    [ProfileProperty]
    private bool minimizeWhenClose = true;
    /// <summary>
    /// 关闭时不再询问
    /// </summary>
    [ProfileProperty]
    private bool neverAskAgainWhenClose = false;
    /// <summary>
    /// 忽略的版本号
    /// </summary>
    [ProfileProperty]
    private string ignoreVersion = string.Empty;

    static partial void SetThemeProperty(ref ElementTheme value) => AppThemeHelper.ChangeTheme(value);
}
