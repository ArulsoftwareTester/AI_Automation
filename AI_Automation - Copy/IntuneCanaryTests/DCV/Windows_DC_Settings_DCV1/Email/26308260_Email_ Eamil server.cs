using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26308260_Email_Eamil_server : SecurityBaseline
    {
        [Test]
        public async Task Test_26308260_Email_Eamil_server()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_26308260 completed");
        }
    }
}
