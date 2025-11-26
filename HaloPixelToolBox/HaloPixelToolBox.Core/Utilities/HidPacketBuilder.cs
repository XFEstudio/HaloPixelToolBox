using System.Text;

namespace HaloPixelToolBox.Core.Utilities;

public class HidPacketBuilder
{
    /// <summary>
    /// 固定头 5 字节
    /// </summary>
    private static readonly byte[] Header =
    [
        0x2E, 0xAA, 0xEC, 0xE8, 0x00
    ];

    /// <summary>
    /// 固定包长度（64 bytes）
    /// </summary>
    private const int FixedPacketLength = 64;

    /// <summary>
    /// 构造 HID 协议包
    /// </summary>
    public static byte[] Build(string text)
    {
        var textBytes = Encoding.UTF8.GetBytes(text);
        byte textLen = (byte)textBytes.Length;
        // 有效载荷长度 = TextLen(1) + Text(N) + Checksum(1)
        ushort totalLen = (ushort)(1 + textLen + 1);

        var list = Header.ToList();

        // TotalLen (2 bytes, little-endian)
        list.AddRange(BitConverter.GetBytes(totalLen));

        // TextLen (1 byte)
        list.Add(textLen);

        // Text bytes
        list.AddRange(textBytes);

        // Checksum (1 byte)
        list.Add(byte.Parse(Checksum(textBytes).ToString()));

        // Padding 补 0 到固定长度（64 字节）
        while (list.Count < FixedPacketLength)
            list.Add(0x00);

        // 如果超长则截断（一般不应发生）
        if (list.Count > FixedPacketLength)
            return [.. list.Take(FixedPacketLength)];

        return [.. list];
    }

    /// <summary>
    /// 校验算法
    /// </summary>
    public static int Checksum(byte[] textBytes)
    {
        int acc = 128;
        foreach (char ch in textBytes)
        {
            acc += ch + 2;
        }
        return acc % 256;
    }

    /// <summary>
    /// 把包转成 hex 字符串（小写，不带空格）
    /// </summary>
    public static string ToHex(byte[] packet)
    {
        return BitConverter.ToString(packet).Replace("-", "").ToLower();
    }
}
