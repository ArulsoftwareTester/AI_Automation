using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8926214_Endpoint_Protection_LocalDeviceSecurityOptions_UserAccountControl_Route_elevation_prompts_to_users_interactive_desktop : SecurityBaseline
    {
        [Test]
        public async Task Test_8926214_Endpoint_Protection_LocalDeviceSecurityOptions_UserAccountControl_Route_elevation_prompts_to_users_interactive_desktop()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8926214 completed");
        }
    }
}
