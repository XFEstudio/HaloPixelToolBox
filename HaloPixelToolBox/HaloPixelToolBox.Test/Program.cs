using HaloPixelToolBox.Core.Models;
using HaloPixelToolBox.Core.Utilities;
using System.Runtime.Versioning;

namespace HaloPixelToolBox.Test;

[SupportedOSPlatform("windows")]
internal class Program
{
    [SMTest]
    public static void TestMethod()
    {
        var device = new HaloPixelDevice();
        device.Initialize();
    }
}