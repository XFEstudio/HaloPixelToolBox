using System.Diagnostics;
using System.IO.Compression;
using System.Reflection;
using System.Text;

if (Assembly.GetExecutingAssembly().GetManifestResourceStream("HaloPixelToolBox.Installer.Package.Source.zip") is Stream innerStream)
{
    try
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        using var zipArchive = new ZipArchive(innerStream, ZipArchiveMode.Read, false, Encoding.GetEncoding("GB2312"));
        foreach (var entry in zipArchive.Entries)
        {
            try
            {
                if (string.IsNullOrEmpty(entry.Name))
                    Directory.CreateDirectory(entry.FullName);
                else
                    entry.ExtractToFile(entry.FullName, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"安装时发生错误：{ex.Message}");
    }
}
else
{
    Console.WriteLine("未找到嵌入的资源文件：InstallerPackage.Source.zip");
}

Process.Start(new ProcessStartInfo
{
    FileName = "Installer.exe",
    Verb = "runas",
    UseShellExecute = true
})?.Dispose();