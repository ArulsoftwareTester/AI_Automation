using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767471_DC_Device_Restrictions_AppStore_shared_user_app_data
    {
        [Test]
        public async Task Test_8767471_DC_Device_Restrictions_AppStore_shared_user_app_data()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767471 completed");
        }
    }
}
