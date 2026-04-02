using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8787578_DC_Device_Restrictions_AppStore_game_DVR_desktop_only
    {
        [Test]
        public async Task Test_8787578_DC_Device_Restrictions_AppStore_game_DVR_desktop_only()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8787578 completed");
        }
    }
}
