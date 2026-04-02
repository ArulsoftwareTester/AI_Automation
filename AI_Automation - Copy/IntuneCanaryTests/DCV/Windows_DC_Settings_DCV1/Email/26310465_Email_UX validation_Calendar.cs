using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26310465_Email_UX_validation_Calendar : SecurityBaseline
    {
        [Test]
        public async Task Test_26310465_Email_UX_validation_Calendar()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_26310465 completed");
        }
    }
}
