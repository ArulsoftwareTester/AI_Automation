using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8797774_Shared_Multi_user_Device_Maintenance_start_time_in_minutes_from_midnight : SecurityBaseline
    {
        [Test]
        public async Task Test_8797774_Shared_Multi_user_Device_Maintenance_start_time_in_minutes_from_midnight()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8797774 completed");
        }
    }
}
