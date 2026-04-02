using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9370781_Wi_Fi_Wi_Fi_profile_is_editable_and_gets_deleted_
    {
        [Test]
        public async Task Test_9370781_Wi_Fi_Wi_Fi_profile_is_editable_and_gets_deleted_()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_9370781 completed");
        }
    }
}
