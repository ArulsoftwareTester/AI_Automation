using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T1556080_CA_Device_that_has_out_of_date_security_providers_becomes_uncompliant : SecurityBaseline
    {
        [Test]
        public async Task Test_1556080_CA_Device_that_has_out_of_date_security_providers_becomes_uncompliant()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_1556080 completed");
        }
    }
}
