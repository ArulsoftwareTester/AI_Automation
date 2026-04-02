using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26308352_Email_SSL : SecurityBaseline
    {
        [Test]
        public async Task Test_26308352_Email_SSL()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_26308352 completed");
        }
    }
}
