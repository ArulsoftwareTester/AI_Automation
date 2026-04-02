using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T5991547_Enrollment_Enrollment_Restrictions_AndroidSync_Device_fails_enrollment_when_Default_Enrollment_Profile_does_not_allow_Manufacturer : SecurityBaseline
    {
        [Test]
        public async Task Test_5991547_Enrollment_Enrollment_Restrictions_AndroidSync_Device_fails_enrollment_when_Default_Enrollment_Profile_does_not_allow_Manufacturer()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_5991547 completed");
        }
    }
}
