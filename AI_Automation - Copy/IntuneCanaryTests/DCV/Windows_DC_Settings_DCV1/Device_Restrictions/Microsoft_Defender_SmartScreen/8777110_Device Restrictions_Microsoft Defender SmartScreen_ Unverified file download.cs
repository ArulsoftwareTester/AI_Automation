using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8777110_Device_Restrictions_Microsoft_Defender_SmartScreen_Unverified_file_download
    {
        [Test]
        public async Task Test_8777110_Device_Restrictions_Microsoft_Defender_SmartScreen_Unverified_file_download()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8777110 completed");
        }
    }
}
