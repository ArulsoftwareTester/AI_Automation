using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_4185879B_Non_Enus_WinClassic_Sanity : NonEnUsWinClassicSanityTestBase
    {
        protected override string TestId => @"4185879B";
        protected override string TestTitle => @"Test set A - Non-Enus WinClassic Sanity - Win32App_DependentApp_4185879B";
        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[DependentApp][Dependencies][E2E]:[WinClassicApp]:[Required][UserGroup]: Verify Deployment with Dependency returns Success when both Apps successfully install";

        [Test]
        public async Task TestMethod_4185879B_Non_Enus_WinClassic_Sanity()
        {
            await RunTestAsync();
        }
    }
}

