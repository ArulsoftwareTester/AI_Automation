using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8776674_DC_Device_Restrictions_Cloud_and_Storage_Settings_synchronization_for_Microsoft_account
    {
        [Test]
        public async Task Test_8776674_DC_Device_Restrictions_Cloud_and_Storage_Settings_synchronization_for_Microsoft_account()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8776674 completed");
        }
    }
}
