using System.IO;
using System.IO.Compression;

namespace HaloPixelToolBox.Installer.Utilities
{
    public static class ZipHelper
    {
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
                try
                {
                    var filePath = Path.Combine(targetPath, entry.FullName);
                    if (string.IsNullOrEmpty(entry.Name))
                        Directory.CreateDirectory(filePath);
                    else
                        entry.ExtractToFile(filePath, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}