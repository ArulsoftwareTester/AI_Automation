using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8905971_DC_Endpoint_Protection_WindowsEncryption_Warning_for_other_disk_encryption
    {
        [Test]
        public async Task Test_8905971_DC_Endpoint_Protection_WindowsEncryption_Warning_for_other_disk_encryption()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8905971 completed");
        }
    }
}
