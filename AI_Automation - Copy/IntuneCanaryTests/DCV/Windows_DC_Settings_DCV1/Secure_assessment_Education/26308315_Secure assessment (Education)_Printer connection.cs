using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26308315_Secure_assessment_Education_Printer_connection
    {
        [Test]
        public async Task Test_26308315_Secure_assessment_Education_Printer_connection()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_26308315 completed");
        }
    }
}
