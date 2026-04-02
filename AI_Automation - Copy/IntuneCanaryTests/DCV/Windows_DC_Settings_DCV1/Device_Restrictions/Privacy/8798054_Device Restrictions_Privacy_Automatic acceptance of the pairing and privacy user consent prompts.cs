using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8798054_Device_Restrictions_Privacy_Automatic_acceptance_of_the_pairing_and_privacy_user_consent_prompts : SecurityBaseline
    {
        [Test]
        public async Task Test_8798054()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8798054 completed");
        }
    }
}
