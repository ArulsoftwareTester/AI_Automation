using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8914825_DC_Endpoint_Protection_WindowsEncryption_Certificate_based_data_recovery_agent
    {
        [Test]
        public async Task Test_8914825_DC_Endpoint_Protection_WindowsEncryption_Certificate_based_data_recovery_agent()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8914825 completed");
        }
    }
}
