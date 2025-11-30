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
        device.SetUIModel(HaloPixelUIModel.Clock);
        device.SetUIModel(HaloPixelUIModel.Game);
        device.SetUIModel(HaloPixelUIModel.Work);
        device.SetUIModel(HaloPixelUIModel.Read);
        device.SetUIModel(HaloPixelUIModel.Cats);
        device.SetUIModel(HaloPixelUIModel.Dogs);
        device.SetUIModel(HaloPixelUIModel.Memes);
        device.SetUIModel(HaloPixelUIModel.Cyber);
        device.SetUIModel(HaloPixelUIModel.Waves);
    }
}