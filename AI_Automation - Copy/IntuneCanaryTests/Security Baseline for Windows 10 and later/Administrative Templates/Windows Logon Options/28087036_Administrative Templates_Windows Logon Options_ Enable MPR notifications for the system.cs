using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28087036_Administrative_Templates_Windows_Logon_Options_Enable_MPR_notifications_for_the_system : SecurityBaseline
    {
        [Test]
        public async Task Test_28087036_Administrative_Templates_Windows_Logon_Options_Enable_MPR_notifications_for_the_system()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            await securityBaseline.createProfile_Win365(Page, "Security Baseline for Windows 10 and later", "Administrative Templates");
            await securityBaseline.MDMPolicySync(Page);
        }
    }
}
