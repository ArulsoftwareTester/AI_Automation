using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26310432_Email_UX_validation_Amount_of_email_to_synchronize : SecurityBaseline
    {
        [Test]
        public async Task Test_26310432_Email_UX_validation_Amount_of_email_to_synchronize()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_26310432 completed");
        }
    }
}
