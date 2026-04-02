using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767986_MicrosoftDefenderATP_Sample_sharing_for_all_files
    {
        [Test]
        public async Task Test_8767986_MicrosoftDefenderATP_Sample_sharing_for_all_files()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767986 completed");
        }
    }
}
