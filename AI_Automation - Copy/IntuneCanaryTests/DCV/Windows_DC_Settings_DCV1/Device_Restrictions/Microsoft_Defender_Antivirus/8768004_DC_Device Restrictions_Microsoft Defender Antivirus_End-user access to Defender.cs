using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8768004_DC_Device_Restrictions_Microsoft_Defender_Antivirus_End_user_access_to_Defender
    {
        [Test]
        public async Task Test_8768004_DC_Device_Restrictions_Microsoft_Defender_Antivirus_End_user_access_to_Defender()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8768004 completed");
        }
    }
}
