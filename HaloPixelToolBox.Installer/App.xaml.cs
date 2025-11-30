using HaloPixelToolBox.Installer.Profiles;
using System.Windows;

namespace HaloPixelToolBox.Installer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SystemProfile.Args = e.Args;
            if (e.Args.Length == 0)
            {
                SystemProfile.FirstInstall = true;
            }
            else
            {
                SystemProfile.StartMode = e.Args[0];
                SystemProfile.DownloadUrl = e.Args[1];
                SystemProfile.InstallPath = e.Args[2];
            }
        }
    }
}
