using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8776679_DC_Device_Restrictions_Cloud_and_Storage_Microsoft_Account_sign_in_assistant
    {
        [Test]
        public async Task Test_8776679_DC_Device_Restrictions_Cloud_and_Storage_Microsoft_Account_sign_in_assistant()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8776679 completed");
        }
    }
}
