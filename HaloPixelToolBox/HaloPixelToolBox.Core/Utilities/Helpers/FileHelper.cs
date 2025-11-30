using System.IO.Compression;

namespace HaloPixelToolBox.Core.Utilities.Helpers
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

        public static void ExtraZipFile(string zipPath, string targetPath)
        {
            using var zipArchive = ZipFile.OpenRead(zipPath);
            ExtraZip(zipArchive, targetPath);
        }

        public static void ExtraZipStream(Stream stream, string targetPath)
        {
            using var zipArchive = new ZipArchive(stream);
            ExtraZip(zipArchive, targetPath);
        }

        public static void ExtraZip(ZipArchive zipArchive, string targetPath)
        {
            foreach (var entry in zipArchive.Entries)
            {
                var filePath = Path.Combine(targetPath, entry.FullName);
                if (string.IsNullOrEmpty(entry.Name))
                    Directory.CreateDirectory(filePath);
                else
                    entry.ExtractToFile(filePath, true);
            }
        }
    }
}
