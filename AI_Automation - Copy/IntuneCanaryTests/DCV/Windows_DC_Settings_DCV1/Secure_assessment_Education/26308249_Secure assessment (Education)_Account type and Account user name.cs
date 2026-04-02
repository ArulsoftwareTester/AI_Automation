using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26308249_Secure_assessment_Education_Account_type_and_Account_user_name
    {
        [Test]
        public async Task Test_26308249_Secure_assessment_Education_Account_type_and_Account_user_name()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_26308249 completed");
        }
    }
}
