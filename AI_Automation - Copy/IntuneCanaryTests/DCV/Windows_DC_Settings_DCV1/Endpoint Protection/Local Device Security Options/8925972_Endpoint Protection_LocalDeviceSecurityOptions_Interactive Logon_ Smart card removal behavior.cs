using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8925972_Endpoint_Protection_LocalDeviceSecurityOptions_Interactive_Logon_Smart_card_removal_behavior : SecurityBaseline
    {
        [Test]
        public async Task Test_8925972_Endpoint_Protection_LocalDeviceSecurityOptions_Interactive_Logon_Smart_card_removal_behavior()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8925972 completed");
        }
    }
}
