using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8926286_Endpoint_Protection_LocalDeviceSecurityOptions_UserAccountControl_Elevated_prompt_for_app_installations : SecurityBaseline
    {
        [Test]
        public async Task Test_8926286_Endpoint_Protection_LocalDeviceSecurityOptions_UserAccountControl_Elevated_prompt_for_app_installations()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8926286 completed");
        }
    }
}
