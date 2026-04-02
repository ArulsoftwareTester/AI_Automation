using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8798718_Device_Restrictions_Privacy_Phone : SecurityBaseline
    {
        [Test]
        public async Task Test_8798718()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8798718 completed");
        }
    }
}
