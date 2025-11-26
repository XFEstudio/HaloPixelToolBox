using HaloPixelToolBox.Installer.Profiles;
using HaloPixelToolBox.Installer.Views.Windows;
using System.Windows;
using System.Windows.Interop;
using XFEExtension.NetCore.InputSimulator;

namespace HaloPixelToolBox.Installer.ViewModel.Windows
{
    public partial class MainWindowViewModel(MainWindow viewPage) : ViewModelBase
    {
        private bool isDragMouseDown = false;
        private bool isDoubleClickMouseDown = false;
        private int mouseOriginalX = 0;
        private int mouseOriginalY = 0;
        public MainWindow ViewPage { get; set; } = viewPage;

        /// <summary>
        /// 最小化窗体
        /// </summary>
        public void Minimize() => ViewPage!.WindowState = WindowState.Minimized;
        /// <summary>
        /// 关闭窗体
        /// </summary>
        public static void CloseWindow()
        {
            MainWindow.Current?.Close();
        }
        /// <summary>
        /// 获取窗体DPI缩放
        /// </summary>
        public void GetDPIScale() => SystemProfile.CurrentWindowDPIScale = InputSimulator.GetScalingFactorForWindow(new WindowInteropHelper(ViewPage).Handle);
        /// <summary>
        /// 初始化鼠标位置
        /// </summary>
        public void InitMousePosition()
        {
            var mousePosition = InputSimulator.GetMousePosition();
            mouseOriginalX = mousePosition.X;
            mouseOriginalY = mousePosition.Y;
        }
        /// <summary>
        /// 初始化调整窗体参数
        /// </summary>
        public void InitializeToResize()
        {
            isDragMouseDown = true;
            InitMousePosition();
            _ = Task.Run(Resize);
        }
        /// <summary>
        /// 调整窗体大小
        /// </summary>
        public void Resize()
        {
            while (isDragMouseDown && InputSimulator.GetMouseDown(MouseButton.Left))
            {
                var nowMousePoint = InputSimulator.GetMousePosition();
                ViewPage.Dispatcher.Invoke(() =>
                {
                    double newWidth = ViewPage.Width + (nowMousePoint.X - mouseOriginalX) / SystemProfile.CurrentWindowDPIScale;
                    double newHeight = ViewPage.Height + (nowMousePoint.Y - mouseOriginalY) / SystemProfile.CurrentWindowDPIScale;
                    if (newWidth > ViewPage.MinWidth)
                        ViewPage.Width = newWidth;
                    else
                        ViewPage.Width = ViewPage.MinWidth;
                    if (newHeight > ViewPage.MinHeight)
                        ViewPage.Height = newHeight;
                    else
                        ViewPage.Height = ViewPage.MinHeight;
                });
                mouseOriginalX = nowMousePoint.X;
                mouseOriginalY = nowMousePoint.Y;
            }
            ViewPage.Dispatcher.Invoke(() =>
            {
                SystemProfile.MainWindowWidth = ViewPage.Width;
                SystemProfile.MainWindowHeight = ViewPage.Height;
            });
        }
        /// <summary>
        /// 判断是否双击
        /// </summary>
        /// <param name="delay">间隔</param>
        /// <returns></returns>
        public bool CheckDoubleClick(int delay)
        {
            if (isDoubleClickMouseDown)
            {
                var nowMousePosition = InputSimulator.GetMousePosition();
                if (mouseOriginalX == nowMousePosition.X && mouseOriginalY == nowMousePosition.Y)
                    return true;
                else
                    return false;
            }
            else
            {
                isDoubleClickMouseDown = true;
                InitMousePosition();
                _ = Task.Run(async () =>
                {
                    await Task.Delay(delay);
                    isDoubleClickMouseDown = false;
                });
                return false;
            }
        }
    }
}
