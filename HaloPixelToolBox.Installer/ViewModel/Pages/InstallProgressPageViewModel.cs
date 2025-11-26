using CommunityToolkit.Mvvm.Input;
using HaloPixelToolBox.Installer.Profiles;
using HaloPixelToolBox.Installer.ViewModel.Windows;
using HaloPixelToolBox.Installer.Views.Pages;
using System.Diagnostics;
using System.IO;

namespace HaloPixelToolBox.Installer.ViewModel.Pages
{
    public partial class InstallProgressPageViewModel(InstallProgressPage viewPage) : ViewModelBase
    {
        public InstallProgressPage ViewPage { get; set; } = viewPage;

        [RelayCommand]
        void ConfirmSuccess()
        {
            var startInfo = new ProcessStartInfo(Path.Combine(SystemProfile.InstallPath, "花再工具箱.exe"))
            {
                UseShellExecute = true,
                WorkingDirectory = SystemProfile.InstallPath
            };
            Process.Start(startInfo);
            MainWindowViewModel.CloseWindow();
        }
    }
}
