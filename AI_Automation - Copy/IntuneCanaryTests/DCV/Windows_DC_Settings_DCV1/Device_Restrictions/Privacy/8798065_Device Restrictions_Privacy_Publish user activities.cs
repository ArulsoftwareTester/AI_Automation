using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8798065_Device_Restrictions_Privacy_Publish_user_activities : SecurityBaseline
    {
        [Test]
        public async Task Test_8798065()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8798065 completed");
        }
    }
}
