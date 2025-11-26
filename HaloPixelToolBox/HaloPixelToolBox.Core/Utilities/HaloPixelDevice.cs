using HidSharp;
using XFEExtension.NetCore.StringExtension;

namespace HaloPixelToolBox.Core.Utilities;

public class HaloPixelDevice
{
    public HidDevice? CurrentDevice { get; set; }

    public HaloPixelDevice()
    {
        DeviceList.Local.Changed += Local_Changed;
    }

    private void Local_Changed(object? sender, DeviceListChangedEventArgs e)
    {
        Console.WriteLine(sender.X());
    }

    public bool Initialize()
    {
        if (GetPixelDevice().FirstOrDefault() is HidDevice device)
        {
            CurrentDevice = device;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ShowText(string text)
    {
        using var stream = CurrentDevice?.Open();
        byte[] package = new byte[64];
        var data = HidPacketBuilder.Build(text);
        foreach (var per in data)
        {
            Console.Write($"{per:x}");
        }
        Console.WriteLine();
        Array.Copy(data, package, data.Length);
        stream?.Write(package);
        stream?.Close();
    }

    public static IEnumerable<HidDevice> GetPixelDevice()
    {
        foreach (var device in DeviceList.Local.GetHidDevices())
        {
            var name = string.Empty;
            try
            {
                name = device.GetFriendlyName();
            }
            catch { }
            if (device.GetMaxInputReportLength() == 64 && name.Contains("花再 Halo PixelBar"))
                yield return device;
        }
    }

    public static void PrintDeviceList()
    {
        foreach (var subDeivce in DeviceList.Local.GetHidDevices())
        {
            try
            {
                Console.WriteLine($"""
                    ----------------------
                    {subDeivce.GetFriendlyName()}
                    VendorID：{subDeivce.VendorID}
                    ProductID：{subDeivce.ProductID}
                    串口号：{subDeivce.GetSerialNumber()}
                    串口：{string.Join(',', subDeivce.GetSerialPorts())}
                    ReleaseNumberBcd：{subDeivce.ReleaseNumberBcd}
                    UsbPort：{subDeivce.GetUsbPort()}
                    ----------------------


                    """);
            }
            catch { }
        }
    }
}
