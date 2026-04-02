using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T5524134_DC_Add_Workloads_to_PolicySet
    {
        [Test]
        public async Task Test_5524134_DC_Add_Workloads_to_PolicySet()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_5524134 completed");
        }
    }
}
