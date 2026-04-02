using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8926108_Endpoint_Protection_LocalDeviceSecurityOptions_UserAccountControl_Elevation_prompt_for_admins : SecurityBaseline
    {
        [Test]
        public async Task Test_8926108_Endpoint_Protection_LocalDeviceSecurityOptions_UserAccountControl_Elevation_prompt_for_admins()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8926108 completed");
        }
    }
}
