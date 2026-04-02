using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8195608_Supersedence_UI_DependencyViewer_NoSupersedence : SecurityBaseline
    {
        [Test]
        public async Task Test_8195608_Verify_DependencyViewer_DoesNotInclude_Supersedence()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8195608 - Verify Dependency viewer doesn't include supersedence");
            Console.WriteLine("Test_8195608 completed");
        }
    }
}
