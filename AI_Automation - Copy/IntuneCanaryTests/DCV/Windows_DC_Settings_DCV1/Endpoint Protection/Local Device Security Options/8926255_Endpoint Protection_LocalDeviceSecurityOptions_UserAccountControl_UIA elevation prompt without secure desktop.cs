using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8926255_Endpoint_Protection_LocalDeviceSecurityOptions_UserAccountControl_UIA_elevation_prompt_without_secure_desktop : SecurityBaseline
    {
        [Test]
        public async Task Test_8926255_Endpoint_Protection_LocalDeviceSecurityOptions_UserAccountControl_UIA_elevation_prompt_without_secure_desktop()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8926255 completed");
        }
    }
}
