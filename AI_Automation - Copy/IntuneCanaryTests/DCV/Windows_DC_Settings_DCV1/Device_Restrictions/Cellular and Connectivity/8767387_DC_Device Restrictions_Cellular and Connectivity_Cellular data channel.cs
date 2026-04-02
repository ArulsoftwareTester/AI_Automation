using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767387_DC_Device_Restrictions_Cellular_and_Connectivity_Cellular_data_channel
    {
        [Test]
        public async Task Test_8767387_DC_Device_Restrictions_Cellular_and_Connectivity_Cellular_data_channel()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767387 completed");
        }
    }
}
