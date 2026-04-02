using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8787752_Device_Restrictions_Privacy_Privacy_experience : SecurityBaseline
    {
        [Test]
        public async Task Test_8787752_Device_Restrictions_Privacy_Privacy_experience()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8787752 completed");
        }
    }
}

