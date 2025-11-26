using System.Diagnostics;
using System.Text;
using XFEExtension.NetCore.MemoryEditor;

namespace HaloPixelToolBox.Core.Utilities;

public class CloudMusicLyricsReader
{
    public nint Address { get; set; }
    public MemoryEditor Editor { get; set; } = new();

    public bool Initialize()
    {
        if (GetCloudMusicLyricsProcess() is Process process)
        {
            Editor.CurrentProcess = process;
            Address = Editor.ResolvePointerAddress("cloudmusic.dll", 0x01CE4A50, 0xE0, 0x8, 0x120, 0x8, 0x0);
            return true;
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
            if (process.MainWindowTitle == "桌面歌词")
                return process;
        }
        return null;
    }
}
