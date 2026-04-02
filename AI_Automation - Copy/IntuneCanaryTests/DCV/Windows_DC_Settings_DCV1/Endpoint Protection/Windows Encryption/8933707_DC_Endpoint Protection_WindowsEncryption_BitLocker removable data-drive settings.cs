using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8933707_DC_Endpoint_Protection_WindowsEncryption_BitLocker_removable_data_drive_settings
    {
        [Test]
        public async Task Test_8933707_DC_Endpoint_Protection_WindowsEncryption_BitLocker_removable_data_drive_settings()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8933707 completed");
        }
    }
}
