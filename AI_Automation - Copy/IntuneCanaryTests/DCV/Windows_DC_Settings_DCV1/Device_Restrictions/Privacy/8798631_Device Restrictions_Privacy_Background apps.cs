using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8798631_Device_Restrictions_Privacy_Background_apps : SecurityBaseline
    {
        [Test]
        public async Task Test_8798631()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8798631 completed");
        }
    }
}
