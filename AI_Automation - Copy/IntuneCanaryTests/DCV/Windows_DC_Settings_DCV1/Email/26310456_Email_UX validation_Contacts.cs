using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26310456_Email_UX_validation_Contacts : SecurityBaseline
    {
        [Test]
        public async Task Test_26310456_Email_UX_validation_Contacts()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_26310456 completed");
        }
    }
}
