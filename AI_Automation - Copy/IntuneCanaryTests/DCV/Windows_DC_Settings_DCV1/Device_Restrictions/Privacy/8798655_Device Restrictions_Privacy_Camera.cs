using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8798655_Device_Restrictions_Privacy_Camera : SecurityBaseline
    {
        [Test]
        public async Task Test_8798655()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8798655 completed");
        }
    }
}
