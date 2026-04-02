using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8798683_Device_Restrictions_Privacy_Messaging : SecurityBaseline
    {
        [Test]
        public async Task Test_8798683()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8798683 completed");
        }
    }
}
