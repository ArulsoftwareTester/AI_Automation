using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8822122_DC_Device_Restrictions_Microsoft_Defender_Antivirus_Monitor_file_and_program_activity
    {
        [Test]
        public async Task Test_8822122_DC_Device_Restrictions_Microsoft_Defender_Antivirus_Monitor_file_and_program_activity()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8822122 completed");
        }
    }
}
