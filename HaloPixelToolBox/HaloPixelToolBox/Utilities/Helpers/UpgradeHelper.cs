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
    public static async Task<bool> NeedUpgrade() => (await Upgrader.GetReleaseNotes("HaloPixelToolBox", Version.ToString())).IsLatest;

    /// <summary>
    /// 获取更新信息
    /// </summary>
    /// <returns></returns>
    public static async Task<UpgradeInfoNotes> GetReleaseNotes() => await Upgrader.GetReleaseNotes("HaloPixelToolBox", Version.ToString());

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