using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8798591_Device_Restrictions_Privacy_Account_information : SecurityBaseline
    {
        [Test]
        public async Task Test_8798591()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8798591 completed");
        }
    }
}
