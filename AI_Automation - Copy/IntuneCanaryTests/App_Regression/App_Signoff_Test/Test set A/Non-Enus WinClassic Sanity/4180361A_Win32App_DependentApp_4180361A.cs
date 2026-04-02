using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_4180361A_Non_Enus_WinClassic_Sanity : NonEnUsWinClassicSanityTestBase
    {
        protected override string TestId => @"4180361A";
        protected override string TestTitle => @"Test set A - Non-Enus WinClassic Sanity - Win32App_DependentApp_4180361A";
        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[DependentApp][Dependencies][E2E]:[WinClassicApp]:[Required][UserGroup]: A -> B. B then A is installed. B is updated to v2. At global re-eval, Bv2 is installed.";

        [Test]
        public async Task TestMethod_4180361A_Non_Enus_WinClassic_Sanity()
        {
            await RunTestAsync();
        }
    }
}

