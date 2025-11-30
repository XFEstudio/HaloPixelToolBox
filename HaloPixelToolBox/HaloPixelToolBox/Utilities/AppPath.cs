using XFEExtension.NetCore.AutoPath;
using XFEExtension.NetCore.WinUIHelper.Utilities.Helper;

namespace HaloPixelToolBox.Utilities;

public partial class AppPath
{
    /// <summary>
    /// 应用程序日志路径
    /// </summary>
    [AutoPath]
    private static readonly string logDictionary = $@"{AppPathHelper.AppLocalData}\Log";
}
