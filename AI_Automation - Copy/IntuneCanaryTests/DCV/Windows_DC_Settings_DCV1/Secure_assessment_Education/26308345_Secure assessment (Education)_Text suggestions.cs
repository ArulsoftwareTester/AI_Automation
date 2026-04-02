using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26308345_Secure_assessment_Education_Text_suggestions
    {
        [Test]
        public async Task Test_26308345_Secure_assessment_Education_Text_suggestions()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_26308345 completed");
        }
    }
}
