using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8798755_Device_Restrictions_Privacy_Trusted_devices : SecurityBaseline
    {
        [Test]
        public async Task Test_8798755()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8798755 completed");
        }
    }
}
