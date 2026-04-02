using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8925966_Endpoint_Protection_LocalDeviceSecurityOptions_UserAccountControl_Run_all_admins_in_Admin_Approval_Mode : SecurityBaseline
    {
        [Test]
        public async Task Test_8925966_Endpoint_Protection_LocalDeviceSecurityOptions_UserAccountControl_Run_all_admins_in_Admin_Approval_Mode()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8925966 completed");
        }
    }
}
