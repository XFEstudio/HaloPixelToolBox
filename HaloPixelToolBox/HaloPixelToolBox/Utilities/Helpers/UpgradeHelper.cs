using ApplicationUpgradeManager.Core.Model;
using System.Diagnostics;
using System.Reflection;
using XFEExtension.NetCore.UpgradeHelper.Utilities;

namespace HaloPixelToolBox.Utilities.Helpers;

public static class UpgradeHelper
{
    public static string RequestAddress => "http://upgrade.api.xfegzs.com/upgrade";
    public static Upgrader Upgrader { get; set; } = new(RequestAddress);
    public static Version Version => Assembly.GetExecutingAssembly().GetName().Version ?? new Version(1, 0, 0);

    /// <summary>
    /// 检测是否需要更新
    /// </summary>
    /// <returns></returns>
    public static async Task<bool> NeedUpgrade()
    {
        try
        {
            return (await Upgrader.GetReleaseNotes("HaloPixelToolBox", Version.ToString())).IsLatest;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR]检查更新时发生错误：{ex.Message}");
            // 发生错误时默认返回 true（表示已是最新版本），避免影响用户使用
            return true;
        }
    }

    /// <summary>
    /// 获取更新信息
    /// </summary>
    /// <returns></returns>
    public static async Task<UpgradeInfoNotes?> GetReleaseNotes()
    {
        try
        {
            return await Upgrader.GetReleaseNotes("HaloPixelToolBox", Version.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR]获取更新信息时发生错误：{ex.Message}");
            // 返回 null 表示获取失败
            return null;
        }
    }

    /// <summary>
    /// 开始更新
    /// </summary>
    public static void StartUpdate(string downloadUrl)
    {
        var startInfo = new ProcessStartInfo("Installer.exe")
        {
            UseShellExecute = true,
            Verb = "runas"
        };
        startInfo.ArgumentList.Add("Upgrade");
        startInfo.ArgumentList.Add(downloadUrl);
        startInfo.ArgumentList.Add("");
        Process.Start(startInfo);
        Process.GetCurrentProcess().Kill();
        Application.Current.Exit();
    }
}