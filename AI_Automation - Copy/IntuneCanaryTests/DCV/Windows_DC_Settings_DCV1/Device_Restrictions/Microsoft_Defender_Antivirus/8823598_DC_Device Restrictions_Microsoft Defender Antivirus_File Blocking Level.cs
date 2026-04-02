using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8823598_DC_Device_Restrictions_Microsoft_Defender_Antivirus_File_Blocking_Level
    {
        [Test]
        public async Task Test_8823598_DC_Device_Restrictions_Microsoft_Defender_Antivirus_File_Blocking_Level()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8823598 completed");
        }
    }
}
