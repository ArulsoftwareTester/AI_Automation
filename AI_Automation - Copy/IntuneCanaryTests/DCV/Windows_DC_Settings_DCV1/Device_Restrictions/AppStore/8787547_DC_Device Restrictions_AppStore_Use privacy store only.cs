using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8787547_DC_Device_Restrictions_AppStore_Use_privacy_store_only
    {
        [Test]
        public async Task Test_8787547_DC_Device_Restrictions_AppStore_Use_privacy_store_only()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8787547 completed");
        }
    }
}
