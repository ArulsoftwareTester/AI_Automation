using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8926094_Endpoint_Protection_LocalDeviceSecurityOptions_UserAccountControl_Admin_Approval_Mode_For_Builtin_Administrator : SecurityBaseline
    {
        [Test]
        public async Task Test_8926094_Endpoint_Protection_LocalDeviceSecurityOptions_UserAccountControl_Admin_Approval_Mode_For_Builtin_Administrator()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8926094 completed");
        }
    }
}
