using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26310163_Email_UX_validation_Email_address_attribute_from_Microsoft_Entra_ID : SecurityBaseline
    {
        [Test]
        public async Task Test_26310163_Email_UX_validation_Email_address_attribute_from_Microsoft_Entra_ID()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_26310163 completed");
        }
    }
}
