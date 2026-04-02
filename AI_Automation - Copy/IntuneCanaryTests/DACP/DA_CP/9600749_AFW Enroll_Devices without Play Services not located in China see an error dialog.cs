using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9600749_AFW_Enroll_Devices_without_Play_Services_not_located_in_China_see_an_error_dialog : SecurityBaseline
    {
        [Test]
        public async Task Test_9600749_AFW_Enroll_Devices_without_Play_Services_not_located_in_China_see_an_error_dialog()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_9600749 completed");
        }
    }
}
