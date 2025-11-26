using System.Runtime.Versioning;

namespace HaloPixelToolBox.Test
{
    [SupportedOSPlatform("windows")]
    internal class Program
    {
        [SMTest]
        static void TestMethod()
        {
            // This is a test method that will be executed by the test runner.
            // You can add your test logic here.
            Console.WriteLine("TestMethod executed successfully.");
        }
    }
}