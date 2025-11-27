namespace HaloPixelToolBox.Installer.Profiles
{
    public class SystemProfile
    {
        /// <summary>
        /// 当前窗口DPI缩放
        /// </summary>
        public static double CurrentWindowDPIScale { get; set; } = 1.0;
        /// <summary>
        /// 主窗体宽度
        /// </summary>
        public static double MainWindowWidth { get; set; } = 720;
        /// <summary>
        /// 主窗体高度
        /// </summary>
        public static double MainWindowHeight { get; set; } = 450;
        /// <summary>
        /// 启动参数
        /// </summary>
        public static string[] Args { get; set; } = [];
        /// <summary>
        /// 启动模式
        /// </summary>
        public static string StartMode { get; set; } = string.Empty;
        /// <summary>
        /// 下载Url
        /// </summary>
        public static string DownloadUrl { get; set; } = string.Empty;
        /// <summary>
        /// 安装目录
        /// </summary>
        public static string InstallPath { get; set; } = @$"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)}\HaloPixelToolBox";
        /// <summary>
        /// 第一次安装
        /// </summary>
        public static bool FirstInstall { get; set; } = false;
    }
}
