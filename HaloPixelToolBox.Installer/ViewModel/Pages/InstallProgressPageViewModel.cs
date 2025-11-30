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
            var startInfo = new ProcessStartInfo(Path.Combine(SystemProfile.InstallPath, "HaloPixelToolBox.exe"))
            {
                UseShellExecute = true,
                WorkingDirectory = SystemProfile.InstallPath
            };
            Process.Start(startInfo);
            MainWindowViewModel.CloseWindow();
        }
    }
}
