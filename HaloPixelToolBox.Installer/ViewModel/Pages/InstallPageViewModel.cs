using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HaloPixelToolBox.Installer.Profiles;
using HaloPixelToolBox.Installer.Utilities;
using HaloPixelToolBox.Installer.Views.Pages;
using HaloPixelToolBox.Installer.Views.Pages.Popups;
using HaloPixelToolBox.Installer.Views.Windows;
using Microsoft.Win32;
using System.IO;
using System.Reflection;

namespace HaloPixelToolBox.Installer.ViewModel.Pages
{
    public partial class InstallPageViewModel(InstallPage viewPage) : ViewModelBase
    {
        [ObservableProperty]
        bool agreementChecked = false;
        [ObservableProperty]
        string installPath = @$"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)}\HaloPixelToolBox";
        public InstallPage ViewPage { get; set; } = viewPage;

        [RelayCommand]
        void GotoInstallProgressPage()
        {
            if (MainWindow.Current is not null)
                MainWindow.Current.contentFrame.Content = new InstallProgressPage();
        }

        [RelayCommand]
        void ChangeInstallPath()
        {
            var openFolderDialog = new OpenFolderDialog
            {
                RootDirectory = InstallPath,
                Multiselect = false
            };
            if (openFolderDialog.ShowDialog() == true)
            {
                if (FileHelper.IsRootPath(openFolderDialog.FolderName))
                    InstallPath = $@"{openFolderDialog.FolderName}HaloPixelToolBox";
                else
                    InstallPath = openFolderDialog.FolderName;
                SystemProfile.InstallPath = InstallPath;
            }
        }

        [RelayCommand]
        void ViewEULAAgreement()
        {
            if (Assembly.GetExecutingAssembly().GetManifestResourceStream("HaloPixelToolBox.Installer.Resources.Resource.EULA.txt") is Stream stream && new StreamReader(stream).ReadToEnd() is string agreementText)
                PopupHelper.ShowDialog(new AgreementDialogPopupPage
                {
                    Title = "软件最终用户许可协议",
                    Agreement = agreementText
                }, 480, 420);
        }

        [RelayCommand]
        void ViewUserPrivateAgreement()
        {
            if (Assembly.GetExecutingAssembly().GetManifestResourceStream("HaloPixelToolBox.Installer.Resources.Resource.PrivateService.txt") is Stream stream && new StreamReader(stream).ReadToEnd() is string agreementText)
                PopupHelper.ShowDialog(new AgreementDialogPopupPage
                {
                    Title = "用户隐私协议",
                    Agreement = agreementText
                }, 480, 420);
        }
    }
}
