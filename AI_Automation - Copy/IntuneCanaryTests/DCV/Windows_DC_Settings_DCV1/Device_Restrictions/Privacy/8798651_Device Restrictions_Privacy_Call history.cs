using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8798651_Device_Restrictions_Privacy_Call_history : SecurityBaseline
    {
        [Test]
        public async Task Test_8798651()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8798651 completed");
        }
    }
}
