using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8933741_DC_Endpoint_Protection_WindowsEncryption_Data_recovery_agent
    {
        [Test]
        public async Task Test_8933741_DC_Endpoint_Protection_WindowsEncryption_Data_recovery_agent()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8933741 completed");
        }
    }
}
