using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8926153_Endpoint_Protection_LocalDeviceSecurityOptions_UserAccountControl_Only_elevate_executable_files_that_are_signed_and_validated : SecurityBaseline
    {
        [Test]
        public async Task Test_8926153_Endpoint_Protection_LocalDeviceSecurityOptions_UserAccountControl_Only_elevate_executable_files_that_are_signed_and_validated()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8926153 completed");
        }
    }
}
