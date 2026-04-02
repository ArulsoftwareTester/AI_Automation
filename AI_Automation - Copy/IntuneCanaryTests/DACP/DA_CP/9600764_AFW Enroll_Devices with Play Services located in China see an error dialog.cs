using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9600764_AFW_Enroll_Devices_with_Play_Services_located_in_China_see_an_error_dialog : SecurityBaseline
    {
        [Test]
        public async Task Test_9600764_AFW_Enroll_Devices_with_Play_Services_located_in_China_see_an_error_dialog()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_9600764 completed");
        }
    }
}
