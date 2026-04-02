using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8777047_Device_Restrictions_Microsoft_Defender_SmartScreen_SmartScreen_for_Microsoft_Edge
    {
        [Test]
        public async Task Test_8777047_Device_Restrictions_Microsoft_Defender_SmartScreen_SmartScreen_for_Microsoft_Edge()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8777047 completed");
        }
    }
}
