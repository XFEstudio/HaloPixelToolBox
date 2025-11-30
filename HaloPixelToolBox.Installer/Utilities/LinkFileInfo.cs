namespace HaloPixelToolBox.Installer.Utilities
{
    /// <summary>
    /// 快捷方式的信息。
    /// </summary>
    public class LinkFileInfo(string targetPath, string? arguments = null,
        string? description = null, string? iconLocation = null)
    {
        public string? Arguments { get; set; } = arguments;

        /// <summary>
        /// 设置备注。
        /// </summary>
        public string? Description { get; set; } = description;

        /// <summary>
        /// 键代码（KeyCode）和System.Windows.Forms.Keys相同。位屏蔽为 0x000000ff。
        /// 修饰符（Modifiers）和System.Windows.Forms.Keys不同。位屏蔽为 0x0000ff00。
        /// <para>示例：</para>
        /// <![CDATA[
        /// Alt = 1 << 8
        /// Control = 2 << 8
        /// Shift = 4 << 8
        /// WindowsKey = 8 << 8
        /// Keys.Alt | Keys.Control | Keys.Shift | Keys.F1
        /// = (short)(((1 | 2 | 4) << 8) | (int)Keys.F1)
        /// = (short)(((1 | 2 | 4) << 8) | 112)
        /// ]]>
        /// </summary>
        public short Hotkey { get; set; }

        /// <summary>
        /// 设置图标路径。
        /// </summary>
        public string? IconLocation { get; set; } = iconLocation;

        /// <summary>
        /// 设置图标ID。
        /// </summary>
        public int IconLocationID { get; set; }

        /// <summary>
        /// 目标路径。
        /// </summary>
        public string TargetPath { get; set; } = targetPath;

        /// <summary>
        /// 设置运行方式，默认为常规窗口。
        /// https://learn.microsoft.com/zh-cn/windows/win32/api/winuser/nf-winuser-showwindow
        /// </summary>
        public int WindowStyle { get; set; } = -1;

        public string? WorkingDirectory { get; set; }
    }
}