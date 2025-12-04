using System.Diagnostics;
using System.Text;
using XFEExtension.NetCore.MemoryEditor;
using XFEExtension.NetCore.StringExtension;

namespace HaloPixelToolBox.Core.Utilities;

public class CloudMusicLyricsReader
{
    public nint Address { get; set; }
    public MemoryEditor Editor { get; set; } = new();

    public bool Initialize()
    {
        if (GetCloudMusicLyricsProcess() is Process process)
        {
            Console.WriteLine($"[DEBUG]已找到进程：{process.ProcessName}({process.Id}|{process.Id:X}) - {process.MainWindowTitle}");
            Editor.CurrentProcess = process;
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
            var address = Editor.ResolvePointerAddress("cloudmusic.dll", 0x01CEBD70, 0xE0, 0x8, 0x120, 0x8, 0x0);
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
            Console.WriteLine($"[ERROR]{ex.Message}");
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
