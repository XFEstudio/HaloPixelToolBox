using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace HaloPixelToolBox.Installer.Utilities
{
    public static class FileHelper
    {
        public static string[] RootPath { get; set; } = [@"C:\", @"D:\", @"E:\", @"F:\", @"G:\", @"H:\", @"I:\", @"J:\", @"K:\", @"L:\", @"M:\", @"N:\", @"O:\", @"P:\", @"Q:\", @"R:\", @"S:\", @"T:\", @"U:\", @"V:\", @"W:\", @"X:\", @"Y:\", @"Z:\",];
        public static long GetDirectorySize(DirectoryInfo directoryInfo)
        {
            long size = 0;
            FileInfo[] files = directoryInfo.GetFiles();
            foreach (FileInfo file in files)
            {
                size += file.Length;
            }
            DirectoryInfo[] directories = directoryInfo.GetDirectories();
            foreach (DirectoryInfo directory in directories)
            {
                size += GetDirectorySize(directory);
            }
            return size;
        }

        public static bool IsRootPath(string path) => RootPath.Any(rootPath => rootPath == path);

        public static bool CreateShortCut(string linkFilePath, string targetPath, string? arguments = null,
            string? description = null, string? iconLocation = null, string? workingDirectory = null)
        {
            var info = new LinkFileInfo(targetPath, arguments, description, iconLocation)
            {
                WorkingDirectory = workingDirectory
            };
            return Write(linkFilePath, info, false);
        }

        private static bool Write(string linkFilePath, LinkFileInfo info, bool useExistsFile)
        {
            bool isSuccessful = false;
            try
            {
                var shortcut = (IShellLink)new ShellLink();
                var persistFile = shortcut as IPersistFile;
                if (useExistsFile && System.IO.File.Exists(linkFilePath)) LoadPersistFile(persistFile, linkFilePath);

                if (info.Arguments != null) shortcut.SetArguments(info.Arguments);
                if (info.Description != null) shortcut.SetDescription(info.Description);
                if (info.Hotkey != 0) shortcut.SetHotkey(info.Hotkey);
                if (info.IconLocation != null) shortcut.SetIconLocation(info.IconLocation, info.IconLocationID);
                if (info.TargetPath != null) shortcut.SetPath(info.TargetPath);
                if (info.WindowStyle != -1) shortcut.SetShowCmd(info.WindowStyle);
                if (info.WorkingDirectory != null) shortcut.SetWorkingDirectory(info.WorkingDirectory);

                persistFile?.Save(linkFilePath, false);
                isSuccessful = persistFile != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LinkFile write exception:  {ex}");
            }
            return isSuccessful;
        }

        private static void LoadPersistFile(IPersistFile? persistFile, string linkFilePath)
        {
            try
            {
                const int STGM_SHARE_DENY_NONE = 0x00000040;
                persistFile?.Load(linkFilePath, STGM_SHARE_DENY_NONE);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LinkFile load persist file exception: {ex}");
            }
        }
    }
}