using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26310478_Email_UX_validation_Tasks : SecurityBaseline
    {
        [Test]
        public async Task Test_26310478_Email_UX_validation_Tasks()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_26310478 completed");
        }
    }
}
