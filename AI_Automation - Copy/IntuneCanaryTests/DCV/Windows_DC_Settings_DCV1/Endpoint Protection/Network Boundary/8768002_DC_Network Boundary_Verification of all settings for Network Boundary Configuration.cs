using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8768002_DC_Network_Boundary_Verification_of_all_settings_for_Network_Boundary_Configuration
    {
        [Test]
        public async Task Test_8768002_DC_Network_Boundary_Verification_of_all_settings_for_Network_Boundary_Configuration()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8768002 completed");
        }
    }
}
