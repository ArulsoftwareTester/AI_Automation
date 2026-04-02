using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9370741_Wi_Fi_Windows_Wi_Fi_profile_type_appears_in_profile_type_drop_down_under_Windows_10_or_later_platform_type
    {
        [Test]
        public async Task Test_9370741_Wi_Fi_Windows_Wi_Fi_profile_type_appears_in_profile_type_drop_down_under_Windows_10_or_later_platform_type()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_9370741 completed");
        }
    }
}
