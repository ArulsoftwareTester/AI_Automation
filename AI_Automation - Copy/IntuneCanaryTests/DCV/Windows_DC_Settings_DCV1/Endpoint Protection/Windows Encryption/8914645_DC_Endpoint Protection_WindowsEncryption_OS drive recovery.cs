using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8914645_DC_Endpoint_Protection_WindowsEncryption_OS_drive_recovery
    {
        [Test]
        public async Task Test_8914645_DC_Endpoint_Protection_WindowsEncryption_OS_drive_recovery()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8914645 completed");
        }
    }
}
