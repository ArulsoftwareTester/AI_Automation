using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8798070_Device_Restrictions_Privacy_Local_activities_only : SecurityBaseline
    {
        [Test]
        public async Task Test_8798070()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8798070 completed");
        }
    }
}
