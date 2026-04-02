using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8926146_Endpoint_Protection_LocalDeviceSecurityOptions_UserAccountControl_UIA_integrity_without_secure_location : SecurityBaseline
    {
        [Test]
        public async Task Test_8926146_Endpoint_Protection_LocalDeviceSecurityOptions_UserAccountControl_UIA_integrity_without_secure_location()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8926146 completed");
        }
    }
}
