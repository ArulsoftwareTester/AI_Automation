using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T5524129_DC_Basic_Policy_Set_Create
    {
        [Test]
        public async Task Test_5524129_DC_Basic_Policy_Set_Create()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_5524129 completed");
        }
    }
}
