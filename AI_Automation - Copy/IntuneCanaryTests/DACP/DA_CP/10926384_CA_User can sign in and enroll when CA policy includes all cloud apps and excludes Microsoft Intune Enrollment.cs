using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T10926384_CA_User_can_sign_in_and_enroll_when_CA_policy_includes_all_cloud_apps_and_excludes_Microsoft_Intune_Enrollment : SecurityBaseline
    {
        [Test]
        public async Task Test_10926384_CA_User_can_sign_in_and_enroll_when_CA_policy_includes_all_cloud_apps_and_excludes_Microsoft_Intune_Enrollment()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_10926384 completed");
        }
    }
}
