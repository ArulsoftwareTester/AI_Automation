using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8798027_Device_Restrictions_Privacy_Input_personalization : SecurityBaseline
    {
        [Test]
        public async Task Test_8798027_Device_Restrictions_Privacy_Input_personalization()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8798027 completed");
        }
    }
}


