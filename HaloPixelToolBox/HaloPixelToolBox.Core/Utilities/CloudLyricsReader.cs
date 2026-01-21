using System.Diagnostics;
using System.Text;
using XFEExtension.NetCore.MemoryEditor;
using XFEExtension.NetCore.StringExtension;

namespace HaloPixelToolBox.Core.Utilities;

public class CloudMusicLyricsReader
{
    public nint Address { get; set; }
    public FileVersionInfo? VersionInfo { get; set; }
    public Version Version { get; set; } = new();
    public MemoryEditor Editor { get; set; } = new();

    public bool Initialize()
    {
        if (GetCloudMusicLyricsProcess() is Process process)
        {
            Console.WriteLine($"[DEBUG]已找到进程：{process.ProcessName}({process.Id}|{process.Id:X}) - {process.MainWindowTitle}");
            Editor.CurrentProcess = process;
            VersionInfo = FileVersionInfo.GetVersionInfo(process.MainModule?.FileName ?? string.Empty);
            Version = new Version(VersionInfo?.FileVersion ?? "0.0.0.0");
            try
            {
                Console.WriteLine($"[DEBUG]版本信息：{VersionInfo?.FileVersion}");
                Console.WriteLine($"[DEBUG]版本信息缩略：{Version.ToString(3)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR]版本输出异常：{ex.Message}");
                Console.WriteLine($"[TRACE]{ex.StackTrace}");
            }
            return ReresolveAddress();
        }
        else
        {
            return false;
        }
    }

    public bool TryReadLyrics(out string lyrics)
    {
        lyrics = "无法读取歌词";
        if (Editor.ReadMemory(Address, 200, out var buffer))
        {
            lyrics = Encoding.Unicode.GetString(buffer, 0, GetValidLength(buffer));
            return true;
        }
        else return false;
    }

    public bool ReresolveAddress()
    {
        try
        {
            nint address = 0;
            switch (Version.ToString(3))
            {
                case "3.1.27":
                    address = Editor.ResolvePointerAddress("cloudmusic.dll", 0x01DDE290, 0xE0, 0x8, 0xE8, 0x38, 0x118, 0x8, 0x0);
                    break;
                case "3.1.26":
                    address = Editor.ResolvePointerAddress("cloudmusic.dll", 0x01DD5130, 0xE8, 0x38, 0x120, 0x18, 0x0);
                    break;
                case "3.1.25":
                    address = Editor.ResolvePointerAddress("cloudmusic.dll", 0x01DAFF60, 0xE0, 0x8, 0x128, 0x18, 0x0);
                    break;
                default:
                    break;
            }
            if (address != Address)
                Console.WriteLine($"[DEBUG]读取到新的地址：{address}({address:X})");
            if (address != 0)
            {
                Address = address;
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR]解析地址异常：{ex.Message}");
            Console.WriteLine($"[TRACE]{ex.StackTrace}");
            return false;
        }
    }

    public static int GetValidLength(byte[] buffer)
    {
        int length = 0;
        for (int i = 0; i < buffer.Length - 1; i += 2)
        {
            if (buffer[i] == 0 && buffer[i + 1] == 0)
                break;

            length += 2;
        }
        return length;
    }

    public static Process? GetCloudMusicLyricsProcess()
    {
        foreach (var process in Process.GetProcesses())
        {
            if (process.ProcessName == "cloudmusic" && (process.MainWindowTitle == "桌面歌词" || !process.MainWindowTitle.IsNullOrWhiteSpace()))
                return process;
        }
        return null;
    }
}
