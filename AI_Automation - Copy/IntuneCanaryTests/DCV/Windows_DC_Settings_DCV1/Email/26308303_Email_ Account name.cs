using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26308303_Email_Account_name : SecurityBaseline
    {
        [Test]
        public async Task Test_26308303_Email_Account_name()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_26308303 completed");
        }
    }
}
