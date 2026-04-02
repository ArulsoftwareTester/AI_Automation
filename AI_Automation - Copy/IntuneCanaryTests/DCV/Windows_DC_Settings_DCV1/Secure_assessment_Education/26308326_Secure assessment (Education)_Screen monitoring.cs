using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26308326_Secure_assessment_Education_Screen_monitoring
    {
        [Test]
        public async Task Test_26308326_Secure_assessment_Education_Screen_monitoring()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_26308326 completed");
        }
    }
}
