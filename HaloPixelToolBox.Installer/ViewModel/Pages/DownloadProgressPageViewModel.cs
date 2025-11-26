using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HaloPixelToolBox.Installer.Profiles;
using HaloPixelToolBox.Installer.Utilities;
using HaloPixelToolBox.Installer.Views.Pages;
using HaloPixelToolBox.Installer.Views.Windows;
using System.IO;
using System.Windows;
using XFEExtension.NetCore.FileExtension;
using XFEExtension.NetCore.StringExtension;
using XFEExtension.NetCore.WebExtension;

namespace HaloPixelToolBox.Installer.ViewModel.Pages
{
    public partial class DownloadProgressPageViewModel(DownloadProgressPage viewPage) : ViewModelBase
    {
        [ObservableProperty]
        bool isBusy = false;
        [ObservableProperty]
        bool isPause = false;
        [ObservableProperty]
        bool isError = false;
        [ObservableProperty]
        bool pauseSwitchEnable = false;
        [ObservableProperty]
        double maxValue = 100;
        [ObservableProperty]
        double value = 0;
        [ObservableProperty]
        string pauseText = "暂停";
        [ObservableProperty]
        string downloadText = "0/0";
        private XFEDownloader? downloader;
        public DownloadProgressPage ViewPage { get; set; } = viewPage;
        public XFEDownloader? Downloader
        {
            get => downloader;
            set
            {
                PauseSwitchEnable = false;
                if (value is not null)
                {
                    if (downloader is not null)
                        downloader.BufferDownloaded -= Downloader_BufferDownloaded;
                    value.BufferDownloaded += Downloader_BufferDownloaded;
                    PauseSwitchEnable = true;
                }
                downloader = value;
            }
        }

        private async Task StartDownload()
        {
            try
            {
                if (Downloader is not null)
                    await Downloader.Download(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"下载出错：\n{ex.Message}");
                PauseText = "继续";
                IsError = true;
                ViewPage.progress.SetError();
            }
        }

        private void Downloader_BufferDownloaded(XFEDownloader sender, FileDownloadedEventArgs e)
        {
            DownloadText = $"{e.DownloadedBufferSize.FileSize()}/{(e.TotalBufferSize is not null ? e.TotalBufferSize.Value.FileSize() : "未知")}";
            Value = e.DownloadedBufferSize;
            if (e.TotalBufferSize is not null)
                MaxValue = e.TotalBufferSize.Value;
            ViewPage.Dispatcher.Invoke(ViewPage.progress.Update);
            if (e.Downloaded)
            {
                MainWindow.Current?.Dispatcher.Invoke(() => MainWindow.Current.contentFrame.Content = new InstallProgressPage());
            }
        }

        [RelayCommand]
        void PauseSwitch()
        {
            if (Downloader is not null)
            {
                if (Downloader.IsPaused)
                {
                    Downloader?.Continue();
                    IsPause = false;
                    ViewPage.progress.SetPause();
                    PauseText = "暂停";
                }
                else
                {
                    Downloader?.Pause();
                    IsPause = true;
                    ViewPage.progress.SetPause();
                    PauseText = "继续";
                }
            }
        }

        [RelayCommand]
        async Task Retry()
        {
            var url = SystemProfile.DownloadUrl;
            if (!url.IsNullOrEmpty())
            {
                Downloader = new()
                {
                    DownloadUrl = url,
                    SavePath = Path.Combine(SystemProfile.InstallPath, "InstallPackage.zip")
                };
            }
            await StartDownload();
        }
    }
}
