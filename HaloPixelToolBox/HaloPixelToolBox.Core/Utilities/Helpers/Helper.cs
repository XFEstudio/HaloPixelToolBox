using System.Diagnostics;

namespace HaloPixelToolBox.Core.Utilities.Helpers
{
    public static class Helper
    {
        /// <summary>
        /// 执行异步等待
        /// </summary>
        /// <param name="func">等待表达式</param>
        /// <param name="delay">检测延迟</param>
        /// <returns></returns>
        public static async Task Wait(Func<bool> func, int delay = 100)
        {
            while (!func())
                await Task.Delay(delay);
        }

        /// <summary>
        /// 在浏览器中打开
        /// </summary>
        /// <param name="url">链接</param>
        public static void OpenInBrowser(string url) => Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });

        /// <summary>
        /// 在文件资源管理器中打开
        /// </summary>
        /// <param name="path">文件路径</param>
        public static void OpenPath(string path) => Process.Start("explorer.exe", path);
    }
}
